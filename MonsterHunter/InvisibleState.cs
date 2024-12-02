﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter
{
    public class InvisibleState : IState
    {
        private DateTime _startTime;

        public InvisibleState(Hunter hunter)
        {
            _startTime = DateTime.Now;
            ApplyState(hunter);
        }

        public void ApplyState(Hunter hunter)
        {
            hunter.isInvisible = true;
            expired(hunter);
        }
        bool isExpired=false;
        public async Task expired(Hunter hunter)
        {
            while (!isExpired)
            {
                if (HasExpired())
                {
                    isExpired = true;
                    Console.WriteLine($"Your Invisible state expired");
                    hunter.isInvisible = false;
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