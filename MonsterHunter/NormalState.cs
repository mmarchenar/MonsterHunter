using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter
{
    public class NormalState : IState
    {
        public void ApplyState(Hunter hunter)
        {
            // En el estado normal no modificamos los atributo
        }

        public bool HasExpired()
        {
            return false; // El estado normal no tiene expiración, es el estado predeterminado
        }
    }

}
