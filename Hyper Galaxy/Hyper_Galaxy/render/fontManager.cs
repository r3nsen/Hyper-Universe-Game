using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hyper_Galaxy.render
{
    class fontManager
    {
        private Texture2D fontTexture;

        public void DrawText(string text, Vector3 pos, float size, Vector3 rotate, Color cor, DrawManager d, bool movel)
        {
            char[] subText = text.ToCharArray();
            int[] sText = new int[subText.Length];

            for (int i = 0; i < text.Length; i++)
            {
                sText[i] = subText[i];
            }
            for (int i = 0; i < sText.Length; i++)
            {
                //pos.X += ((size - 4) * g.zoomfactor.X);

                ////g.getSpriteBatch().Draw(g.font, pos, new Rectangle(((sText[i]%16))*16, ((sText[i] / 16))*16,16,16), cor, 0, g.desloc,
                ////    new Vector2(size/16,size/16)*g.zoomfactor, g.spriteEffects, 0);
                //g.getObjDraw().getSpriteDraw().draw2dTexture(
                //    g.font,
                //    pos,
                //    new Vector3(size / 16 * g.zoomfactor.X, size / 16 * g.zoomfactor.Y, 0),
                //    rotate,
                //    new Rectangle(((sText[i] % 16)) * 16, ((sText[i] / 16)) * 16, 16, 16),
                //     cor);
                if (i > 0)
                {
                    switch (subText[i - 1])
                    {
                        case 'l':
                        case 'i':
                        case 'j':
                        case 't':
                            pos.X += 4 * size/*(((4 + 1) * size * g.zoomfactor.X) + g.desloc.X)*/;
                            break;
                        case 'm':
                            pos.X += 10 * size/*(((10 + 3) * size * g.zoomfactor.X) + g.desloc.X)*/;
                            break;
                        case ' ':
                            pos.X += 5 * size;
                            break;

                        case '.':
                        case ',':
                        case ';':
                        case ':':
                            pos.X += 3 * size;
                            break;
                        default:
                            pos.X += 8 * size/*(((8 + 2) * size * g.zoomfactor.X) + g.desloc.X)*/;
                            break;
                    }
                }


                if (movel)
                {
                    d.draw(
                        fontTexture,
                        pos,
                        new Vector3(size/**g.zoomfactor.X*/, size/* * g.zoomfactor.Y*/, 0),
                        rotate,
                        new Rectangle((int)(Math.Floor((decimal)(sText[i] % 16))) * 8, (int)(Math.Floor((decimal)(sText[i] / 16)) * 8), 8, 8),
                         cor);
                    return;
                }                
                d.StaticDraw(
                     fontTexture,
                     pos,
                     new Vector3(size/**g.zoomfactor.X*/, size/* * g.zoomfactor.Y*/, 0),
                     rotate,
                     new Rectangle((int)(Math.Floor((decimal)(sText[i] % 16))) * 8, (int)(Math.Floor((decimal)(sText[i] / 16)) * 8), 8, 8),
                      cor);

            }
        }

        public void setFontTexture(Texture2D t)
        {
            fontTexture = t;
        }

    }

}