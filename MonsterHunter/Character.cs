using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter
{
    // Abstract class that represents a character (Hunter, Monster, etc.)
    public abstract class Character
    {
        // Properties representing the position of the character on the map
        public int X { get; set; }   // X coordinate
        public int Y { get; set; }   // Y coordinate

        // Maximum boundaries for the X and Y coordinates (if needed for validation)
        public int maxX { get; set; }
        public int maxY { get; set; }

        // Properties for the character's health, strength, armor, and freeze time
        public int MaxHP { get; set; }   // Maximum health points
        public int CurrentHP { get; set; }  // Current health points
        public int Strength { get; set; }  // Strength of the character, affecting attack damage
        public int Armor { get; set; }  // Armor value, affecting damage reduction
        public int FreezeTime { get; set; }  // Freeze time in milliseconds (e.g., for movement slowdown)

        // Abstract method for movement, to be implemented by derived classes
        public abstract bool Move(int newX, int newY, Map map);

        // Constructor to initialize the character's position and map boundaries
        public Character(int x, int y, int MaxX = 0, int MaxY = 0)
        {
            X = x;
            Y = y;
            maxX = MaxX;
            maxY = MaxY;
        }

        // Method to check if the character is dead (i.e., if their health is 0 or less)
        public bool IsDead()
        {
            return CurrentHP <= 0;
        }

        // Optional method for boundary validation (if boundaries are needed)
        // This method checks if the character's new position is within the allowed map boundaries
        public bool IsInBounds(int newX, int newY)
        {
            return newX >= 0 && newX <= maxX && newY >= 0 && newY <= maxY;
        }
    }
}