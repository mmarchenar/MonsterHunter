using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MonsterHunter
{
    public class FastState : IState
    {
        private DateTime startTime;

        public FastState(Hunter hunter)
        {
            startTime = DateTime.Now;
            ApplyState(hunter);
        }

        bool isExpired = false;

        public void ApplyState(Hunter hunter)
        {
            hunter.FreezeTime = 500;  // Decrease freeze time
            expired(hunter);
        }

        public async Task expired(Hunter hunter)
        {
            while (!isExpired)
            {
                if (HasExpired())
                {
                    isExpired = true;
                    Console.WriteLine($"Your Fast state expired");
                    hunter.FreezeTime = 1000;
                    hunter.State = new NormalState();

                }
                await Task.Delay (1000);
            }
        }

        public bool HasExpired()
        {
            return DateTime.Now.Subtract(startTime).TotalSeconds > 10;
        }
    }
}
