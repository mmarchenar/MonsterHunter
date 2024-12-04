using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter  // Define a namespace for the MonsterHunter project
{
    public class Sword  // Define the Sword class to represent a sword weapon
    {
        public int Strength { get; private set; }  // Property to store the strength of the sword, accessible only within the class

        public Sword()  // Constructor for the Sword class
        {
            // Strength is randomly assigned a value between 4 and 9 (inclusive)
            Strength = Random.Instance.Next(4, 10);  // Generate a random strength value between 4 and 9
        }

        public bool BreakAfterAttack()  // Method to determine if the sword breaks after an attack
        {
            return Random.Instance.Next(1, 6) == 1;  // Return true with a probability of 1 out of 5 (20% chance)
        }
    }
}