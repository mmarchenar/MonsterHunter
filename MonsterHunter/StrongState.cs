using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter
{
    public class StrongState : IState
    {
        private DateTime _startTime;

        public StrongState(Hunter hunter)
        {
            _startTime = DateTime.Now;
        }
        bool isExpired = false;


        public void ApplyState(Hunter hunter)
        {
            hunter.Strength = hunter.Strength*2;  // Double attack
            hunter.Armor = (int)(hunter.Armor*1.5);      // 1.5x defense
            hunter.CurrentHP = hunter.MaxHP;  // Heal hunter
            expired(hunter);
        }

        public async Task expired(Hunter hunter)
        {
            while (!isExpired)
            {
                if (HasExpired())
                {
                    isExpired = true;
                    Console.WriteLine($"Your Strength state expired");
                    hunter.Strength = hunter.Strength / 2;
                    hunter.Armor = (int)(hunter.Armor / 1.5);
                    hunter.State = new NormalState();
                }
                await Task.Delay(1000);
            }
        }
            public bool HasExpired()
        {
            return DateTime.Now.Subtract(_startTime).TotalSeconds > 10;
        }
    }
}
