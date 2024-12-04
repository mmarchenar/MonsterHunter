using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter
{
    public class InvisibleState : IState
    {
        // Store the time when the state was applied
        private DateTime _startTime;

        // Constructor that applies the invisible state to the hunter and tracks the start time
        public InvisibleState(Hunter hunter)
        {
            _startTime = DateTime.Now;  // Record the start time
            ApplyState(hunter);         // Apply the invisible state to the hunter
        }

        // Apply the invisible state to the hunter
        public void ApplyState(Hunter hunter)
        {
            hunter.isInvisible = true;  // Set the hunter's invisibility status to true
            expired(hunter);            // Start checking if the state has expired
        }

        bool isExpired = false; // Flag to track if the state has expired

        // Asynchronous method to check if the invisible state has expired
        public async Task expired(Hunter hunter)
        {
            while (!isExpired)  // Keep checking until the state expires
            {
                if (HasExpired())  // Check if the state has expired
                {
                    isExpired = true;  // Mark the state as expired
                    Console.WriteLine($"Your Invisible state expired");  // Inform the player
                    hunter.isInvisible = false;  // Set hunter's invisibility to false
                }
                await Task.Delay(1000);  // Wait for 1 second before checking again
            }
        }

        // Check if the invisible effect has expired (after 10 seconds)
        public bool HasExpired()
        {
            // The effect lasts for 10 seconds
            return DateTime.Now.Subtract(_startTime).TotalSeconds > 10;
        }
    }
}
