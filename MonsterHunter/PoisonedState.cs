using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter  // Define a namespace for the MonsterHunter project
{
    public class PoisonedState : IState  // Define the PoisonedState class that implements the IState interface
    {
        private DateTime _startTime;  // Variable to track when the Poisoned state started

        public PoisonedState(Hunter hunter)  // Constructor for PoisonedState that takes a Hunter object
        {
            _startTime = DateTime.Now;  // Record the current time as the start time of the state
            ApplyState(hunter);  // Apply the effects of the Poisoned state to the hunter
        }

        public void ApplyState(Hunter hunter)  // Method to apply the effects of the Poisoned state to the hunter
        {
            // Reduce strength and defense
            hunter.Strength = hunter.Strength / 2;  // Halve the hunter's attack strength
            hunter.Armor = hunter.Armor / 2;  // Halve the hunter's armor (defense)
            hunter.CurrentHP -= 5;  // Decrease the hunter's current health by 5 points
            hunter.FreezeTime += (int)(hunter.FreezeTime * 0.25);  // Increase freeze time by 25%
            expired(hunter);  // Call the expired method to start checking for expiration
        }

        bool isExpired = false;  // Flag to indicate whether the Poisoned state has expired

        public async Task expired(Hunter hunter)  // Asynchronous method to check if the state has expired
        {
            while (!isExpired)  // Loop until the state is marked as expired
            {
                if (HasExpired())  // Check if the Poisoned state has expired
                {
                    isExpired = true;  // Mark the state as expired
                    Console.WriteLine($"Your Poisoned state expired");  // Notify that the Poisoned state has expired
                    hunter.Strength = hunter.Strength * 2;  // Reset the hunter's strength back to normal (double it)
                    hunter.Armor = hunter.Armor * 2;  // Reset the hunter's armor back to normal (double it)
                    hunter.FreezeTime -= (int)(hunter.FreezeTime * 0.25);  // Decrease freeze time by 25% to revert back
                    hunter.State = new NormalState();  // Change the hunter's state to NormalState
                }
                await Task.Delay(1000);  // Wait for one second before checking again
            }
        }

        public bool HasExpired()  // Method to determine if the Poisoned state has expired
        {
            // The effect lasts for 10 seconds
            return DateTime.Now.Subtract(_startTime).TotalSeconds > 10;  // Return true if more than 10 seconds have passed since start time
        }
    }
}
