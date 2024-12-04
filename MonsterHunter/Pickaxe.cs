using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter
{
    // Represents a Pickaxe in the game
    public class Pickaxe
    {
        // Determines whether the pickaxe breaks after being used
        public bool BreakAfterUse()
        {
            // Returns true with a 1 in 3 chance (randomly returns 1, 2, or 3)
            // If the result is 1, the pickaxe breaks; otherwise, it doesn't
            return Random.Instance.Next(1, 4) == 1;  // 1 out of 3 chance for the pickaxe to break
        }
    }
}
