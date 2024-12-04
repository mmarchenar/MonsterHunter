using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter
{
    // Represents the default state of the Hunter (Normal state)
    public class NormalState : IState
    {
        // Applies the normal state to the hunter (no attribute changes)
        public void ApplyState(Hunter hunter, Map map)
        {
            // In the normal state, no changes are made to the hunter's attributes.
            // This is the default state where the hunter is not affected by any special conditions.
        }

        // Checks if the normal state has expired (it never does)
        public bool HasExpired()
        {
            return false; // The normal state does not expire, it is the default state.
        }
    }
}
