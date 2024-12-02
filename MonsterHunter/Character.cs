using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter
{
    public abstract class Character
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int maxX { get; set; }
        public int maxY { get; set; }
        public int MaxHP { get; set; }
        public int CurrentHP { get; set; }
        public int Strength { get; set; }
        public int Armor { get; set; }
        public int FreezeTime { get; set; }  // in milliseconds

        public abstract bool Move(int newX, int newY, Map map);

        public Character(int x, int y, int MaxX = 0, int MaxY = 0)
        {
            X = x;
            Y = y;
            maxX = MaxX;
            maxY = MaxY;
        }

        public bool IsDead()
        {
            return CurrentHP <= 0;
        }
    }

}
