using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunter
{
    public class Map
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public string[] AvailableMaps { get; private set; }
        public char[,] MapData { get; private set; }
        public int scoreHunter { get; private set; }
        public Hunter currentHunter { get; private set; }

        public Map()
        {
            LoadAvailableMaps();
        }

        private void LoadAvailableMaps()
        {
            // Load .map files from the current directory
            string[] AvailableMaps = {"map1","map2","map3"};
        }

        public void LoadMap(string mapFile, Monsters monsters, String name)
        {
            string[] lines = File.ReadAllLines(mapFile);
            Height = lines.Length;
            Width = lines.Max(line => line.Length);

            MapData = new char[Height, Width];

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    MapData[y, x] = lines[y][x];

                    // Find the Hunter's starting position
                    if (MapData[y, x] == 'H')
                    {
                        Hunter hunter = new Hunter(x, y, name, this);
                        this.currentHunter = hunter;
                        MapData[y, x] = ' ';  // Remove hunter from map
                    }

                    // Add Monsters
                    if (MapData[y, x] == 'M')
                    {
                        monsters.AddMonster(new Monster(x, y));
                        MapData[y, x] = ' ';  // Remove monster from map
                    }
                }
            }
        }

        
    }

}
