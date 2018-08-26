using Hyper_Galaxy.others;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hyper_Galaxy.render
{
    class DrawManager
    {
   
        Matrix WorldMatrix = Matrix.CreateTranslation(0, 0, 0);
        Matrix ViewMatrix = Matrix.CreateLookAt(new Vector3(0, 0, 3), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
        Matrix ProjectionMatrix = Matrix.CreateOrthographicOffCenter(
                0, 1, -1, 0, 0, 1);
        Matrix HudMatrix = Matrix.CreateOrthographicOffCenter(
                0, 1, -1, 0, 0, 1);
        Vector3 CamPos;
        Vector3 CamDir;
        Vector3 CamAr;


        VertexPositionColorTexture[] _vertices;

        short[] _indices;
        int _vertexCount = 0;
        int _indexCount = 0;
        Texture2D _texture;
        Color color;
        GraphicsDevice _graphicsDevice;
        BasicEffect _basicEffect;

        public DrawManager(GraphicsDevice g)
        {
            _graphicsDevice = g;
            _basicEffect = new BasicEffect(_graphicsDevice);
            _vertices = new VertexPositionColorTexture[255];
            _indices = new short[_vertices.Length * 3 / 2];
            CamPos = Vector3.Zero;
            CamDir = new Vector3(0, 0, -1);
            CamAr = Vector3.One;
        }

        public GraphicsDevice getGraphicsDevice()
        {
            return _graphicsDevice;
        }

        public void ResetMatrices(int width, int height, float zoomFactor,Vector3 desloc)
        {
            WorldMatrix = Matrix.Identity;           
            ViewMatrix = Matrix.CreateLookAt(CamPos,CamDir, new Vector3(0, -1, 0));

            ProjectionMatrix = Matrix.CreateOrthographicOffCenter(
                ((-width/2) * (desloc.Z + 1)) + desloc.X,
                ((CamAr.X * width/2) * (desloc.Z + 1)) + desloc.X,
                ((- CamAr.Y * height/2) * (desloc.Z + 1)) + desloc.Y,
                ((height/2) * (desloc.Z + 1)) + desloc.Y,
                -100 * zoomFactor,
                100 * zoomFactor);

            HudMatrix = Matrix.CreateOrthographicOffCenter(
                -width / 2, width / 2, -height / 2, height / 2, -100, 100);
            _texture = null;
            color = Color.White;
        }

       

        public void drawRect(RectF r, Color cor)
        {

            EnsureSpace(6, 4);

            _indices[_indexCount++] = (short)(_vertexCount + 0);
            _indices[_indexCount++] = (short)(_vertexCount + 1);
            _indices[_indexCount++] = (short)(_vertexCount + 3);
            _indices[_indexCount++] = (short)(_vertexCount + 1);
            _indices[_indexCount++] = (short)(_vertexCount + 2);
            _indices[_indexCount++] = (short)(_vertexCount + 3);

            _vertices[_vertexCount++] = new VertexPositionColorTexture(
                new Vector3(1, -1, 0),
                cor,
  new Vector2(200, 80));
            _vertices[_vertexCount++] = new VertexPositionColorTexture(
                new Vector3(-1, -1, 0),
                 cor,
                new Vector2(200, 80));
            _vertices[_vertexCount++] = new VertexPositionColorTexture(
                new Vector3(-1, 1, 0),
                 cor,
                 new Vector2(200, 80));
            _vertices[_vertexCount++] = new VertexPositionColorTexture(
                new Vector3(1, 1, 0),
                cor,
               new Vector2(200, 80));

            r.x *= -1;

            Matrix world =
                Matrix.CreateScale(new Vector3(r.width, r.height, 1))
                * Matrix.CreateScale(.5f)

                * Matrix.CreateTranslation(new Vector3(r.x, r.y, 9))
                * WorldMatrix;
            for (int i = _vertexCount - 4; i < _vertexCount; i++)
                Vector3.Transform(ref _vertices[i].Position, ref world, out _vertices[i].Position);

        }       

        public void draw(Texture2D t, Vector3 pos, Vector3 scale, Vector3 rotate, Rectangle src, Color cor)
        {
            if ((_texture != null && _texture != t)) Flush();    
            _texture = t;

            EnsureSpace(6, 4);

            _indices[_indexCount++] = (short)(_vertexCount + 0);
            _indices[_indexCount++] = (short)(_vertexCount + 1);
            _indices[_indexCount++] = (short)(_vertexCount + 3);
            _indices[_indexCount++] = (short)(_vertexCount + 1);
            _indices[_indexCount++] = (short)(_vertexCount + 2);
            _indices[_indexCount++] = (short)(_vertexCount + 3);

            _vertices[_vertexCount++] = new VertexPositionColorTexture(
                new Vector3(1, -1, 0),
                cor,
                GetUV(src.Left, src.Top));
            _vertices[_vertexCount++] = new VertexPositionColorTexture(
                new Vector3(-1, -1, 0),
                 cor,
                GetUV(src.Right, src.Top));
            _vertices[_vertexCount++] = new VertexPositionColorTexture(
                new Vector3(-1, 1, 0),
                 cor,
                 GetUV(src.Right, src.Bottom));
            _vertices[_vertexCount++] = new VertexPositionColorTexture(
                new Vector3(1, 1, 0),
                cor,
                GetUV(src.Left, src.Bottom));

            pos.X *= -1;
            scale *= .5f;
            Matrix world =
                Matrix.CreateScale(new Vector3(src.Width, src.Height, 1))
                * Matrix.CreateScale(scale)
                * Matrix.CreateRotationX(rotate.X)
                * Matrix.CreateRotationY(rotate.Y)
                * Matrix.CreateRotationZ(rotate.Z)
                * Matrix.CreateTranslation(pos)
                * WorldMatrix;
            for (int i = _vertexCount - 4; i < _vertexCount; i++)
                Vector3.Transform(ref _vertices[i].Position, ref world, out _vertices[i].Position);



        }

        public void StaticDraw(Texture2D t, Vector3 pos, Vector3 scale, Vector3 rotate, Rectangle src, Color cor)
        {
            Flush();

            if ((_texture != null && _texture != t)) 
            _texture = t;

            EnsureSpace(6, 4);

            Matrix old = ProjectionMatrix;

            ProjectionMatrix = HudMatrix;

            _indices[_indexCount++] = (short)(_vertexCount + 0);
            _indices[_indexCount++] = (short)(_vertexCount + 1);
            _indices[_indexCount++] = (short)(_vertexCount + 3);
            _indices[_indexCount++] = (short)(_vertexCount + 1);
            _indices[_indexCount++] = (short)(_vertexCount + 2);
            _indices[_indexCount++] = (short)(_vertexCount + 3);

            _vertices[_vertexCount++] = new VertexPositionColorTexture(
                new Vector3(1, -1, 0),
                cor,
                GetUV(src.Left, src.Top));
            _vertices[_vertexCount++] = new VertexPositionColorTexture(
                new Vector3(-1, -1, 0),
                 cor,
                GetUV(src.Right, src.Top));
            _vertices[_vertexCount++] = new VertexPositionColorTexture(
                new Vector3(-1, 1, 0),
                 cor,
                 GetUV(src.Right, src.Bottom));
            _vertices[_vertexCount++] = new VertexPositionColorTexture(
                new Vector3(1, 1, 0),
                cor,
                GetUV(src.Left, src.Bottom));

            pos.X *= -1;
            scale *= .4f;
            Matrix world =
                Matrix.CreateScale(new Vector3(src.Width, src.Height, 1))
                                * Matrix.CreateScale(scale)
                * Matrix.CreateTranslation(pos)
                * WorldMatrix; 
            for (int i = _vertexCount - 4; i < _vertexCount; i++)
                Vector3.Transform(ref _vertices[i].Position, ref world, out _vertices[i].Position);

            Flush();
            ProjectionMatrix = old;

        }

        private Vector2 GetUV(int x, int y)
        {
            return new Vector2(x / (float)_texture.Width, y / (float)_texture.Height);
        }

        private void EnsureSpace(int indexSpace, int vertexSpace)
        {
            if (_indexCount + indexSpace >= _indices.Length)
                Array.Resize(ref _indices, Math.Max(_indexCount + indexSpace, _indices.Length * 2));
            if (_vertexCount + vertexSpace >= _vertices.Length)
                Array.Resize(ref _vertices, Math.Max(_vertexCount + vertexSpace, _vertices.Length * 2));
        }

        public void Flush()
        {
            if (_vertexCount == 0)
                return;
            _basicEffect.World = WorldMatrix;
            _basicEffect.View = ViewMatrix;
            _basicEffect.Projection = ProjectionMatrix;
       

            _basicEffect.Texture = _texture;
            _basicEffect.TextureEnabled = true;
            _basicEffect.VertexColorEnabled = true;



            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
           
            _graphicsDevice.RasterizerState = rasterizerState;

            foreach (EffectPass pass in _basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
               
                _graphicsDevice.DrawUserIndexedPrimitives(
                    PrimitiveType.TriangleList,
                    _vertices,
                    0,
                    _vertexCount,
                    _indices,
                    0,
                    _indexCount / 3);
               
            }
            _vertexCount = 0;
            _indexCount = 0;
        }
    }
}

