using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter  // Define a namespace for the MonsterHunter project
{
    public class StrongState : IState  // Define the StrongState class that implements the IState interface
    {
        private DateTime _startTime;  // Variable to track when the Strong state started

        public StrongState(Hunter hunter)  // Constructor for StrongState that takes a Hunter object
        {
            _startTime = DateTime.Now;  // Record the current time as the start time of the state
        }

        bool isExpired = false;  // Flag to indicate whether the Strong state has expired

        public void ApplyState(Hunter hunter)  // Method to apply the effects of the Strong state to the hunter
        {
            hunter.Strength = hunter.Strength * 2;  // Double the hunter's attack strength
            hunter.Armor = (int)(hunter.Armor * 1.5);  // Increase the hunter's armor by 1.5 times
            hunter.CurrentHP = hunter.MaxHP;  // Heal the hunter to full health
            expired(hunter);  // Call the expired method to start checking for expiration
        }

        public async Task expired(Hunter hunter)  // Asynchronous method to check if the state has expired
        {
            while (!isExpired)  // Loop until the state is marked as expired
            {
                if (HasExpired())  // Check if the Strong state has expired
                {
                    isExpired = true;  // Mark the state as expired
                    Console.WriteLine($"Your Strength state expired");  // Notify that the Strong state has expired
                    hunter.Strength = hunter.Strength / 2;  // Reset the hunter's strength back to normal
                    hunter.Armor = (int)(hunter.Armor / 1.5);  // Reset the hunter's armor back to normal
                    hunter.State = new NormalState();  // Change the hunter's state to NormalState
                }
                await Task.Delay(1000);  // Wait for one second before checking again
            }
        }

        public bool HasExpired()  // Method to determine if the Strong state has expired
        {
            return DateTime.Now.Subtract(_startTime).TotalSeconds > 10;  // Return true if more than 10 seconds have passed since start time
        }
    }
}