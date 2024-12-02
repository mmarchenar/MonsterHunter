using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter
{
    public class Pickaxe
    {
        public bool BreakAfterUse()
        {
            return Random.Instance.Next(1, 4) == 1;  // 1 out of 3 chance
        }

    }

}
