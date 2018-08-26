using Hyper_Galaxy.render;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hyper_Galaxy
{
    class World
    {
        const int MAXORBITS = 4;
        const int MAXPLANETS = 4;
        const int SEEDSIZE = 15;
        Planets[] _planets;
        public Planets center;
        Texture2D _tex;
        int orbits;
        float gravity = .7f;
        //int focus = 0;
        public int planetFocus = 0;

        public Vector3 position;
        
        struct Orbt
        {
            public int planetsNum;
            internal int radius;
        }
        Orbt[] _orbt;

        public World(long seed)
        {
            GenerateWorld(seed);
            position = new Vector3(0,0,0);
        }

        public void Update(GameTime gameTime)
        {
            
            GravityCalc(_planets);
            for (int i = 0; i < _planets.Length; i++)
            {
                _planets[i].Update(gameTime);
            }
            center.Update(gameTime);
        }

        public void Draw(DrawManager _DM,fontManager f)
        {
            for (int i = 0; i < _planets.Length; i++)
            {
                _planets[i].Draw(_DM, _tex,f,position);
            }
            center.Draw(_DM, _tex,f, position);
        }

        private void GenerateWorld(long seed)
        {
            _planets = new Planets[0];
            int planetIndex = 0;
            long[] num = new long[SEEDSIZE];
            int j;
            for (int i = 0; i < SEEDSIZE; i++)
            {
                j = i;
                num[i] = (long)(seed / Math.Pow(10, SEEDSIZE - (i + 1)));
                while (j > 0)
                {
                    j--;
                    long power = (long)(Math.Pow(10, i - j));
                    num[i] -= num[j] * power;

                }
            }
            int sum = 0;
            for (int i = 0; i < num.Length; i++)
            {
                sum += (int)num[i];
            }
            if (sum < 0) sum *= -1;

            center = new Planets();
            center.ID = 0;
            center.Speed = Vector3.Zero;
            center._position = position;
            center._mass = 100;
            center._texCode = 1;
            center.captured = true;

            orbits = sum % MAXORBITS + 3;

            _orbt = new Orbt[orbits];
            for (int o = 0; o < orbits; o++)
            {

                _orbt[o] = new Orbt();
                _orbt[o].planetsNum = (int)((sum + (sum/2)* (2 * Math.Pow(o, 2))) % MAXPLANETS + 2);

                _orbt[o].radius = (int)((sum * 597 + sum * (2 * Math.Pow(o, 2))) % 9);
                R: for(int i = 0; i < o; i++) {
                    if (_orbt[o].radius == ((_orbt[i].radius - 100) / 150))
                    {
                        _orbt[o].radius++;
                        if (_orbt[o].radius > 9) _orbt[o].radius = 0;
                        goto R;
                    }
                }
                _orbt[o].radius = (_orbt[o].radius* 150) + 100;
                Array.Resize(ref _planets, _planets.Length + _orbt[o].planetsNum);
                for (int p = 0; p < _orbt[o].planetsNum; p++)
                {
                    int pn = _orbt[o].planetsNum;
                    _planets[planetIndex] = new Planets();
                    _planets[planetIndex].ID = (o * 10) + p + 1;
                    // v = sqrt(G * M / r)
                    _planets[planetIndex]._mass = .1f;//((sum + o + (pn * p)) % 1 + 5) + 0;
                    double v = Math.Sqrt(gravity * center._mass / _orbt[o].radius);
                    _planets[planetIndex].Speed = new Vector3(
                       (float)(v * Math.Sin((MathHelper.TwoPi / _orbt[o].planetsNum) * p)),
                    (float)(v * Math.Cos((MathHelper.TwoPi / _orbt[o].planetsNum) * p)),
                    0);
                   
                    _planets[planetIndex]._maxHab = (sum + o + (pn * p)) % 50 + 50;
                    _planets[planetIndex]._temp = (sum + o + (pn * p)) % 50 + 50;
                    // x' = x * cos(theta) - y * sin(theta)
                    // y' = x * sin(theta) + y * cos(theta)
                    _planets[planetIndex]._position = new Vector3(
                        -(float)(_orbt[o].radius * Math.Cos((MathHelper.TwoPi / _orbt[o].planetsNum) * p)) + center._position.X,
                         (float)(_orbt[o].radius * Math.Sin((MathHelper.TwoPi / _orbt[o].planetsNum) * p)) + center._position.Y,
                         0);
                    _planets[planetIndex]._texCode = 2;// (sum + 2 * p * o) % 2;
                    _planets[planetIndex].orbit = o;
                    _planets[planetIndex].subOrbit = (sum + o + (pn * p)) % 2 - 1;

                    _planets[planetIndex].XP = 0;
                    _planets[planetIndex].Level = 0;

                    // recursos
                    _planets[planetIndex].MaxWater = (sum + o + (pn * p)) % 50 + 50;
                    _planets[planetIndex].CurrentWater = 0;
                    _planets[planetIndex].RestoreWater = (sum + o + (pn * p)) % 5 + 3;

                    _planets[planetIndex].MaxWood = (sum + o + (pn * p)) % 50 + 50;
                    _planets[planetIndex].CurrentWood = 0;
                    _planets[planetIndex].RestoreWood = (sum + o + (pn * p)) % 5 + 3;

                    _planets[planetIndex].MaxIron = (sum + o + (pn * p)) % 50 + 50;
                    _planets[planetIndex].CurrentIron = 0;
                    _planets[planetIndex].RestoreIron = (sum + o + (pn * p)) % 5 + 3;

                    // _planets[planetIndex].MaxPetroill = (sum + o + (pn * p)) % 50 + 50;
                    // _planets[planetIndex].CurrentPetroill = 0;
                    // _planets[planetIndex].RestorePetroil = (sum + o + (pn * p)) % 5 + 3;

                    // _planets[planetIndex].MaxUranium = (sum + o + (pn * p)) % 50 + 50;
                    // _planets[planetIndex].CurrentUrânium = 0;
                    // _planets[planetIndex].RestoreUranium = (sum + o + (pn * p)) % 5 + 3;

                    // _planets[planetIndex].MaxFood = (sum + o + (pn * p)) % 50 + 50;
                    // _planets[planetIndex].CurrentFood = 0;
                    // _planets[planetIndex].ProduceFood = (sum + o + (pn * p)) % 50 + 20;

                    _planets[planetIndex].MaxMoney = 10000;
                    _planets[planetIndex].CurrentMoney = 0;
                    _planets[planetIndex].GetMoney = 1;
                    planetIndex++;
                }
            }


        }

        public void SetTex(Texture2D t)
        {
            _tex = t;
        }

        public void GravityCalc(Planets[] p)
        {
            float dir;
            float d;
            float Dpow;
            float Aceleration;
            float force;

            for (int i = 0; i < p.Length; i++)
            {
                for (int j = 0; j < p.Length; j++)
                {
                    if (i == j) break;
                    //F = G *(m1 * m2)/D^2
                    //Fx = G * (m1 * m2)/Dx^2
                    //Fy = G * (m1 * m2)/Dy^2
                    dir = gravity * p[i]._mass * p[j]._mass;
                    d = (float)Math.Sqrt(Math.Pow((p[i]._position.X - p[j]._position.X), 2) + Math.Pow((p[i]._position.Y - p[j]._position.Y), 2));
                    if (d == 0) break;
                    Dpow = (2*d) * (2*d);

                    force = dir / Dpow;

                    double angulo = Math.Atan((p[i]._position.Y - p[j]._position.Y) / (p[i]._position.X - p[j]._position.X));

                    Aceleration = force / p[i]._mass;
                    float X = (float)(Aceleration * Math.Cos(angulo));
                    float Y = (float)(Aceleration * Math.Sin(angulo));

                    p[i].Speed.X += X;
                    p[i].Speed.Y -= Y;
                    p[j].Speed.X -= X;
                    p[j].Speed.Y += Y;

                }
            }

            for (int x = 0; x < p.Length; x++)
            {

                //F = G *(m1 * m2)/D^2
                //Fx = G * (m1 * m2)/Dx^2
                //Fy = G * (m1 * m2)/Dy^2
                dir = gravity * p[x]._mass * center._mass;
                d = (float)Math.Sqrt(
                    Math.Pow((p[x]._position.X - center._position.X), 2)
                    + Math.Pow((p[x]._position.Y - center._position.Y), 2));

                if (d == 0) break;
                Dpow = d * d;

                force = dir / Dpow;

                double angulo = Math.Atan((p[x]._position.Y - center._position.Y) / (p[x]._position.X - center._position.X));

                Aceleration = force / p[x]._mass;
                float X = (float)(Aceleration * Math.Cos(angulo));
                float Y = (float)(Aceleration * Math.Sin(angulo));

                if (p[x]._position.X > center._position.X)
                {
                    p[x].Speed.X -= X;
                }
                else
                {
                    p[x].Speed.X += X;
                }

                if (p[x]._position.X > center._position.X)
                {
                    p[x].Speed.Y -= Y;
                }
                else
                {
                    p[x].Speed.Y += Y;
                }

                // p[j].Speed.X += X;
                // p[j].Speed.Y += Y;

            }
        }

        public bool ColisionTest(Vector2 a, Vector2 b, int radius)
        {
            return Math.Sqrt(((b.X - a.X) * (b.X - a.X)) + ((b.Y - a.Y) * (b.Y - a.Y))) <= radius;
        }

        public Planets[] getPlanets()
        {
            return _planets;
        }
    }
}
