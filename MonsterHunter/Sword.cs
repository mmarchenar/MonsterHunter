using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter
{
    public class Sword
    {
        public int Strength { get; private set; }

        public Sword()
        {
            // Strength between 4 and 9
            Strength = Random.Instance.Next(4, 10);
        }

        public bool BreakAfterAttack()
        {
            return Random.Instance.Next(1, 6) == 1;  // 1 out of 5 chance
        }
    }

}
