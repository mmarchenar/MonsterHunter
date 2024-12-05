using System; // Importing system functionalities
using MonsterHunter; // Importing the MonsterHunter namespace
using System.Collections.Generic; // Importing collections for using lists, dictionaries, etc.
using System.Linq; // Importing LINQ functionalities for data manipulation
using System.Text; // Importing functionalities for string manipulation
using System.Threading.Tasks; // Importing functionalities for asynchronous programming

namespace MonsterHunterFrm // Defining the namespace for the form
{
    public static class Core // Static class to hold core game data and state
    {
        public static string[] availableMaps = new string[0]; // Array to hold available maps
        public static string chosenMap = ""; // Stores the name of the chosen map
        public static string Name = ""; // Stores the player's name
        public static Map mapCore; // Holds the current game map instance
        public static bool gameOver = false; // Indicates if the game is over
        public static int level = 1; // Player's current level
        public static Form3 inForm = null; // Holds a reference to Form3 instance

        // The core static class is used to access common variables throughout the different forms.
    }
}