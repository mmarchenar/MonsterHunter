using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter
{
    // Enum representing the different types of potions in the game
    public enum PotionType
    {
        Strength,       // Increases strength temporarily
        Poison,         // Poisons the hunter
        Invisibility,   // Makes the hunter invisible
        Speed           // Increases the speed of the hunter
    }

    // Represents a potion in the game
    public class Potions
    {
        // The type of the potion (Strength, Poison, Invisibility, Speed)
        public PotionType Type { get; private set; }

        // Constructor that randomly assigns a type to the potion
        public Potions()
        {
            // Randomly roll a number between 1 and 6
            int roll = Random.Instance.Next(1, 7);

            // Based on the roll, assign a potion type
            Type = roll switch
            {
                // If the roll is 1, the potion is Poison
                1 => PotionType.Poison,

                // If the roll is 2 or 3, the potion is Speed
                2 or 3 => PotionType.Speed,

                // If the roll is 4 or 5, the potion is Invisibility
                4 or 5 => PotionType.Invisibility,

                // Default case (when the roll is 6), the potion is Strength
                _ => PotionType.Strength
            };
        }
    }
}
