using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter  // Define a namespace for the MonsterHunter project
{
    public class Monsters  // Define the Monsters class to manage a collection of Monster objects
    {
        private static List<Monster> _monsters = new List<Monster>();  // Static list to hold all Monster instances

        public void AddMonster(Monster monster)  // Method to add a new Monster to the collection
        {
            _monsters.Add(monster);  // Add the specified monster to the _monsters list
        }

        public static Monster[] FindMonstersAtPosition(int x, int y)  // Static method to find monsters at a specific (x, y) position
        {
            return _monsters.Where(m => m.X == x && m.Y == y).ToArray();  // Filter monsters based on their position and return as an array
        }
    }
}