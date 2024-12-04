using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MonsterHunter  // Define a namespace for the MonsterHunter project
{
    public class FastState : IState  // Define the FastState class that implements the IState interface
    {
        private DateTime startTime;  // Declare a variable to track when the state started

        public FastState(Hunter hunter)  // Constructor for FastState that takes a Hunter object
        {
            startTime = DateTime.Now;  // Record the current time as the start time of the state
            ApplyState(hunter);  // Apply the effects of the Fast state to the hunter
        }

        bool isExpired = false;  // Flag to indicate whether the Fast state has expired

        public void ApplyState(Hunter hunter)  // Method to apply the Fast state effects to the hunter
        {
            hunter.FreezeTime = 500;  // Set the hunter's freeze time to 500 milliseconds (decreased)
            expired(hunter);  // Call the expired method to start checking for expiration
        }

        public async Task expired(Hunter hunter)  // Asynchronous method to check if the state has expired
        {
            while (!isExpired)  // Loop until the state is marked as expired
            {
                if (HasExpired())  // Check if the Fast state has expired
                {
                    isExpired = true;  // Mark the state as expired
                    Console.WriteLine($"Your Fast state expired");  // Notify that the Fast state has expired
                    hunter.FreezeTime = 1000;  // Reset the hunter's freeze time back to normal (1000 milliseconds)
                    hunter.State = new NormalState();  // Change the hunter's state to NormalState
                }
                await Task.Delay(1000);  // Wait for one second before checking again
            }
        }

        public bool HasExpired()  // Method to determine if the Fast state has expired
        {
            return DateTime.Now.Subtract(startTime).TotalSeconds > 10;  // Return true if more than 10 seconds have passed since start time
        }
    }
}