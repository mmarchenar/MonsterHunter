using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter  // Define a namespace for the MonsterHunter project
{
    public class Map  // Define the Map class to represent the game map
    {
        public int Width { get; private set; }  // Property to store the width of the map
        public int Height { get; private set; }  // Property to store the height of the map
        public string[] AvailableMaps { get; private set; }  // Array to hold names of available maps
        public char[,] MapData { get; private set; }  // 2D array to store the map data (characters representing terrain, etc.)
        public int scoreHunter { get; private set; }  // Property to track the hunter's score
        public Hunter currentHunter { get; private set; }  // Property to store the current hunter instance

        public Map()  // Constructor for the Map class
        {
            LoadAvailableMaps();  // Call method to load available maps from the directory
        }

        private void LoadAvailableMaps()  // Method to load available maps from a specified directory
        {
            string mapsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Maps");  // Define path to 'Maps' directory in current directory

            if (Directory.Exists(mapsDirectory))  // Check if the 'Maps' directory exists
            {
                // Find all files that end with '.txt' in the 'Maps' folder
                AvailableMaps = Directory.GetFiles(mapsDirectory, "*.txt")
                                         .Select(file => Path.GetFileNameWithoutExtension(file))  // Get only the file names without extensions
                                         .ToArray();  // Convert result to an array

                Console.WriteLine("Mapas disponibles:");  // Print header for available maps
                foreach (var map in AvailableMaps)  // Iterate through each available map
                {
                    Console.WriteLine(map);  // Print the name of each available map
                }
            }
            else  // If the 'Maps' directory does not exist
            {
                Console.WriteLine("La carpeta 'Maps' no existe en el directorio.");  // Notify that the directory is missing
                AvailableMaps = new string[0];  // Initialize AvailableMaps as an empty array
            }
        }

        public void LoadMap(string mapName, Monsters monsters, string name)  // Method to load a specific map by name and initialize monsters and hunter
        {
            if (!AvailableMaps.Contains(mapName))  // Check if the specified map is available
            {
                Console.WriteLine($"El mapa '{mapName}' no está disponible.");  // Notify that the map is not available
                return;  // Exit method if map is not found
            }

            // Build the full path to the map file
            string mapFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Maps", mapName + ".txt");

            // Read the map from the file into an array of strings, where each string is a line in the file
            string[] lines = File.ReadAllLines(mapFilePath);
            Height = lines.Length;  // Set Height based on number of lines in file
            Width = lines.Max(line => line.Length);  // Set Width based on length of longest line

            MapData = new char[Height, Width];  // Initialize MapData as a 2D array with dimensions Height x Width

            for (int y = 0; y < Height; y++)  // Loop through each row of the map data
            {
                for (int x = 0; x < lines[y].Length; x++)  // Loop through each character in the current row
                {
                    MapData[y, x] = lines[y][x];  // Store character in MapData at corresponding position

                    // Find initial position of the hunter represented by 'H'
                    if (MapData[y, x] == 'H')
                    {
                        currentHunter = new Hunter(x, y, name, this);  // Create a new Hunter instance at position (x, y)
                        MapData[y, x] = ' ';  // Remove hunter from map representation by replacing with space character
                    }

                    // Add monsters represented by 'M'
                    if (MapData[y, x] == 'M')
                    {
                        monsters.AddMonster(new Monster(x, y));  // Create and add a new Monster instance at position (x, y)
                        MapData[y, x] = ' ';  // Remove monster from map representation by replacing with space character
                    }
                }
            }
        }
    }
}
