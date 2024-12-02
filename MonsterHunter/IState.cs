using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter
{
  
    public interface IState
    {
        void ApplyState(Hunter hunter);
        bool HasExpired();
    }
    

}
