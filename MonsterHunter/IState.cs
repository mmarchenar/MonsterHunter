using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter
{
    // Interface representing a state that can be applied to a Hunter character
    public interface IState
    {
        // Method to apply the state to the given hunter
        // This will modify the hunter's properties or behavior based on the state
        void ApplyState(Hunter hunter, Map map);

        // Method to check if the state has expired
        // Returns a boolean indicating whether the state has lasted long enough
        bool HasExpired();
    }
}
