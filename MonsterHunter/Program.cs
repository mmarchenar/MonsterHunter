using MonsterHunter;

Map map = new Map();
Monsters monsters = new Monsters();
map.LoadMap("map1.txt",monsters , "pepe");
char[,] currentMap = map.mapData;
Hunter h1 = map.currentHunter;
currentMap[h1.X, h1.Y];

