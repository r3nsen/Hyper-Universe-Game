using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hyper_Galaxy
{
    class Status
    {
        struct planets
        {
            public int id;
            public int money;
            public int habitants;
            public int wood;
            public int iron;
            public int health;
            public int water;
            public int trained;
        }
        struct stats
        {
            int totalmoney;
            int planetsNum;
        }
        planets[] p;
        stats st;
        int count;
        public Status(World w)
        {
            p = new planets[1];
            p[0].id = w.center.ID;
            p[0].money = w.center.ID;
            p[0].habitants = w.center.ID;
            p[0].wood = w.center.ID;
            p[0].iron = w.center.ID;
            p[0].health = w.center.ID;
            p[0].water = w.center.ID;
            p[0].trained = w.center.ID;
        }
        public void update(World w,GameTime t)
        {
            count += t.ElapsedGameTime.Milliseconds;
            if (count > 1000)
            {

            }
        }
    }
}
