using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter
{
    public class PoisonedState : IState
    {
        private DateTime _startTime;

        public PoisonedState(Hunter hunter)
        {
            _startTime = DateTime.Now;
            ApplyState(hunter);
        }

        public void ApplyState(Hunter hunter)
        {
            // Reducir fuerza y defensa
            hunter.Strength = hunter.Strength/2;  // Half attack
            hunter.Armor = hunter.Armor/2;     // Half defense
            hunter.CurrentHP -= 5;
            hunter.FreezeTime += (int)(hunter.FreezeTime*0.25);   // Increase freeze time
            expired(hunter);
        }
        bool isExpired = false;
        public async Task expired(Hunter hunter)
        {
            while (!isExpired)
            {
                if (HasExpired())
                {
                    isExpired = true;
                    Console.WriteLine($"Your Poisoned state expired");
                    hunter.Strength= hunter.Strength*2 ;
                    hunter.Armor = hunter.Armor*2 ;
                    hunter.FreezeTime -= (int)(hunter.FreezeTime * 0.25);
                    hunter.State = new NormalState();
                }
                await Task.Delay(1000);
            }
        }

        public bool HasExpired()
        {
            // El efecto dura 10 segundos
            return DateTime.Now.Subtract(_startTime).TotalSeconds > 10;
        }
    }
}
