using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter
{
    public class Monster : Character
    {
        public Monster(int x, int y) : base(x,y)
        {
            X = x;
            Y = y;
            MaxHP = 20;
            CurrentHP = MaxHP;
            Strength = 5;
            Armor = 2;
            FreezeTime = 2000; // Default freeze time 2 seconds
        }

        public override bool Move(int newX, int newY, Map map)
        {
            // Add monster movement logic here
            X = newX;
            Y = newY;
            return true;
        }
    }

}
