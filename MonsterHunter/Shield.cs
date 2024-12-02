using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter
{
    public class Shield
    {
        public int Armor { get; private set; }

        public Shield()
        {
            // Armor between 3 and 6
            Armor = Random.Instance.Next(3, 7);
        }

        public bool BreakAfterAttack()
        {
            return Random.Instance.Next(1, 5) == 1;  // 1 out of 4 chance
        }
    }
}
