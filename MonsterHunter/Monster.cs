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
            Hunter hunter1 = map.currentHunter;
            if (map.MapData[newX, newY] == '#')
            {
            } else if (hunter1.X == newX && hunter1.Y == newY)
            {
                attack(hunter1);
            }
            else
            {
                this.X = newX;
                this.Y = newY;

            }
            return true;
        }

        public void attack(Hunter target)
        {
            int hit = this.Strength - target.Armor;
            target.CurrentHP -= hit;
            Console.WriteLine($"You dealt {hit} damage");
            if (target.shieldH.BreakAfterAttack())
            {
                target.Armor -= target.shieldH.Armor;
                target.shieldH = null;
                Console.WriteLine("Your sword broke!!");
            }
            if (target.IsDead())
            {
                target = null;
                Console.WriteLine("You died :(");
            }


            




        }
    }

}
