using Hyper_Galaxy.others;
using Hyper_Galaxy.render;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hyper_Galaxy
{
    class Planets
    {
        //caracteristicas básicas
        public int ID;
        public Vector3 Speed;
        public float _mass;
        public int _maxHab;
        public int _currentHab;
        public int _temp;
        public Vector3 _position;
        public int _texCode;
        public int orbit;
        public int subOrbit;
        public int health;

        public int XP;
        public int Level;

        //recursos
        public int MaxWater;
        public int CurrentWater;
        public int RestoreWater = 10;

        public int MaxWood;
        public int CurrentWood;
        public int RestoreWood = 10;
        private int MakeWood;

        public int MaxIron;
        public int CurrentIron;
        public int RestoreIron =1; 
        private int MakeIron;

       // public int MaxPetroill;
      //  public int CurrentPetroill;
      //  public int RestorePetroil;

       // public int MaxUranium;
      //  public int CurrentUrânium;
       // public int RestoreUranium;

        //public int MaxFood;
       // public int CurrentFood;
      //  public int ProduceFood;

        public int MaxMoney = 1000;
        public int CurrentMoney;
        public int GetMoney = 1;

        int countM;
        int countDM;
        public bool captured;
       

        internal void Update(GameTime gameTime)
        {
            countM += gameTime.ElapsedGameTime.Milliseconds;
            countDM += gameTime.ElapsedGameTime.Milliseconds;
            _position += Speed;
            if(countM > 1000) {
                if (captured)
                {
                    _currentHab += 1;
                    float temp = _currentHab * 1.15f;
                    _currentHab =(int)temp;
                }

                if (CurrentMoney < MaxMoney && captured)
                {
                    CurrentMoney++;
                    if(CurrentMoney > MaxMoney)
                    {
                        CurrentMoney = MaxMoney;
                    }
                }

                if (MakeWood > 0)
                {
                    CurrentWood++;
                        MakeWood--;
                    if (CurrentWood > MaxWood)
                    {
                        CurrentWood = MaxWood;
                    }
                }
                countM -= 1000;
            }
            if (countDM > 2000) {

                if (MakeIron > 0)
                {
                    CurrentIron++;
                    MakeIron--;
                    if (CurrentIron > MaxIron)
                    {
                        CurrentIron = MaxIron;
                    }
                }
                countDM -= 2000;
            }
            
        }

        internal void Draw(DrawManager _DM,Texture2D t,fontManager f,Vector3 desloc)
        {
            _DM.draw(t, _position, Vector3.One, Vector3.Zero, new Rectangle((_texCode%4) * 48, (_texCode/4) * 48, 48, 48), Color.White);
          //  status(f, _DM,desloc);
            

        }

        public void status(fontManager f,DrawManager _DM,Vector3 desloc)
        {
            f.DrawText("planet ID: ", _position + new Vector3(10, 10, 0), 1, Vector3.Zero, Color.White, _DM, true);
            f.DrawText(ID.ToString(), _position + new Vector3(100, 10, 0), 1, Vector3.Zero, Color.White, _DM, true);

            f.DrawText("Population: ", _position + new Vector3(10, 20, 0) , 1, Vector3.Zero, Color.White, _DM, true);
            f.DrawText(_currentHab.ToString(), _position + new Vector3(100, 20, 0), 1, Vector3.Zero, Color.White, _DM, true);

            f.DrawText("Health", _position + new Vector3(10, 30, 0), 1, Vector3.Zero, Color.White, _DM, true);
            f.DrawText(health.ToString(), _position + new Vector3(100, 30, 0), 1, Vector3.Zero, Color.White, _DM, true);

            f.DrawText("Money: ", _position + new Vector3(10, 40, 0), 1, Vector3.Zero, Color.White, _DM, true);
            f.DrawText(CurrentMoney.ToString(), _position + new Vector3(100, 40, 0), 1, Vector3.Zero, Color.White, _DM, true);
        }
    }
}
