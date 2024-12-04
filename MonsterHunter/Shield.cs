using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter  // Define a namespace for the MonsterHunter project
{
    public class Shield  // Define the Shield class to represent a shield item
    {
        public int Armor { get; private set; }  // Property to store the armor value of the shield, accessible only within the class

        public Shield()  // Constructor for the Shield class
        {
            // Armor is randomly assigned a value between 3 and 6 (inclusive)
            Armor = Random.Instance.Next(3, 7);  // Generate a random armor value between 3 and 6
        }

        public bool BreakAfterAttack()  // Method to determine if the shield breaks after an attack
        {
            return Random.Instance.Next(1, 5) == 1;  // Return true with a probability of 1 out of 4 (25% chance)
        }
    }
}