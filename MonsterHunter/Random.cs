using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter  // Define a namespace for the MonsterHunter project
{
    public class Random  // Define the Random class to provide a singleton instance for random number generation
    {
        private static Random _instance;  // Static variable to hold the singleton instance of the Random class
        private System.Random _random;  // Instance of the built-in System.Random class for generating random numbers

        private Random()  // Private constructor to prevent instantiation from outside the class
        {
            _random = new System.Random();  // Initialize the _random instance of System.Random
        }

        public static Random Instance  // Public property to provide access to the singleton instance
        {
            get
            {
                if (_instance == null)  // Check if the instance has not been created yet
                {
                    _instance = new Random();  // Create a new instance of the Random class
                }
                return _instance;  // Return the singleton instance
            }
        }

        public int Next(int minValue, int maxValue)  // Method to generate a random integer between minValue (inclusive) and maxValue (exclusive)
        {
            return _random.Next(minValue, maxValue);  // Call the Next method of the System.Random instance to get a random number
        }
    }
}
