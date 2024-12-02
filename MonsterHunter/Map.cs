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
            string mapsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Maps"); // Carpeta 'Maps' en el directorio actual

            if (Directory.Exists(mapsDirectory))
            {
                // Buscar todos los archivos que terminen en '.txt' dentro de la carpeta 'Maps'
                AvailableMaps = Directory.GetFiles(mapsDirectory, "*.txt")
                                         .Select(file => Path.GetFileNameWithoutExtension(file))  // Solo tomar el nombre del archivo sin la extensión
                                         .ToArray();

                Console.WriteLine("Mapas disponibles:");
                foreach (var map in AvailableMaps)
                {
                    Console.WriteLine(map); // Mostrar los mapas encontrados en la consola
                }
            }
            else
            {
                Console.WriteLine("La carpeta 'Maps' no existe en el directorio.");
                AvailableMaps = new string[0]; // Si no existe la carpeta, dejar la lista vacía
            }
        }
        public void LoadMap(string mapName, Monsters monsters, string name)
        {
            // Verificar que el mapa solicitado esté en los mapas disponibles
            if (!AvailableMaps.Contains(mapName))
            {
                Console.WriteLine($"El mapa '{mapName}' no está disponible.");
                return;
            }

            // Construir la ruta completa al archivo del mapa
            string mapFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Maps", mapName + ".txt");

            // Leer el mapa desde el archivo
            string[] lines = File.ReadAllLines(mapFilePath);
            Height = lines.Length;
            Width = lines.Max(line => line.Length);

            MapData = new char[Height, Width];

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    MapData[y, x] = lines[y][x];

                    // Encontrar la posición inicial del cazador
                    if (MapData[y, x] == 'H')
                    {
                        currentHunter = new Hunter(x, y, name, this); // No necesitas 'this' aquí
                        MapData[y, x] = ' ';  // Quitar al cazador del mapa
                    }

                    // Añadir monstruos
                    if (MapData[y, x] == 'M')
                    {
                        monsters.AddMonster(new Monster(x, y));
                        MapData[y, x] = ' ';  // Quitar el monstruo del mapa
                    }
                }
            }
        }

    }

}
