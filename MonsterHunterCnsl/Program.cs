using MonsterHunter;
using System;
using System.Threading;
using System.Diagnostics;

bool GameOver = false;
int level = 1;

void StartPlayerMovement(Map map, Hunter hunter)
{
    while (!GameOver)
    {
        // Check if a key is pressed and move the player accordingly
        PlayerMove(map, hunter);
        Thread.Sleep(hunter.FreezeTime); // Delay for freeze time to control player movement speed
    }
}

void PlayerMove(Map map, Hunter hunter)
{
    int newX = hunter.X;
    int newY = hunter.Y;

    // Check if a key is pressed (non-blocking)
    if (Console.KeyAvailable)
    {
        ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true); // Read key without displaying it

        switch (keyInfo.Key)
        {
            case ConsoleKey.UpArrow:
                newY = newY = (newY - 1 >= 0) ? newY - 1 : newY;  // Ensure it doesn't go out of bounds ; 
                break;
            case ConsoleKey.DownArrow:
                newY = (newY + 1 < map.Width) ? newY + 1 : newY;  // Ensure it doesn't go out of bounds
                break;
            case ConsoleKey.LeftArrow:
                newX = (newX - 1 >= 0) ? newX - 1 : newX;  // Ensure it doesn't go out of bounds
                break;
            case ConsoleKey.RightArrow:
                newX = (newX + 1 < map.Height) ? newX + 1 : newX;  // Ensure it doesn't go out of bounds
                break;
        }
        // Move the hunter based on the new position
        hunter.Move(newX, newY, map);
    }
}

void MonsterMove(Map map, Monsters monsters)
{
    Monster[] monList = Monsters._monsters.ToArray();
    foreach (Monster mon in monList)
    {
        int direction = MonsterHunter.Random.Instance.Next(0, 4); // 0 = up, 1 = down, 2 = left, 3 = right
        int newX = mon.X;
        int newY = mon.Y;
        switch (direction)
        {
            case 0: // Move up
                newY = (newY - 1 >= 0) ? newY - 1 : newY;  // Ensure it doesn't go out of bounds
                break;
            case 1: // Move down
                newY = (newY + 1 < map.Width) ? newY + 1 : newY;  // Ensure it doesn't go out of bounds
                break;
            case 2: // Move left
                newX = (newX - 1 >= 0) ? newX - 1 : newX;  // Ensure it doesn't go out of bounds
                break;
            case 3: // Move right
                newX = (newX + 1 < map.Height) ? newX + 1 : newX;  // Ensure it doesn't go out of bounds
                break;
        }
        mon.Move(newX, newY, map);

    }

}
// Function to run the monster movement in a separate thread
void StartMonsterMovement(Map map, Monsters monsters)
{
    // Run the movement in an infinite loop until the game is over
    while (!GameOver)
    {
        MonsterMove(map, monsters); // Move the monsters
        Thread.Sleep(2000);
    }
}


void DisplayMap(Map map, Hunter hunter, Monsters monsters)
{
    for (int y = 0; y < map.Width; y++)
    {
        for (int x = 0; x < map.Height; x++)
        {
            // Display hunter position
            if (hunter.X == x && hunter.Y == y)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write('H');
            }
            // Display monster position
            else if (Monsters.FindMonstersAtPosition(x, y).Length != 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write('M');
            }
            // Display other map elements
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(map.MapData[x, y]);
            }
        }
        Console.WriteLine(); // New line after each row
    }
}

Console.WriteLine("Enter your name: ");
String name = Console.ReadLine();

while (name == "" || name.Length > 19)
{
    Console.WriteLine("Invalid name, try again");
    Console.WriteLine("Enter your name: ");
    name = Console.ReadLine();
}

Map map = new Map();
Monsters monsters = new Monsters();
String mapChosen = Console.ReadLine();

while (!map.AvailableMaps.Contains(mapChosen))
{
    Console.WriteLine("Map doesn't exist");
    mapChosen = Console.ReadLine();
}

map.LoadMap(mapChosen, monsters, name);



// Start monster movement in a new thread
Thread monsterMovementThread = new Thread(() => StartMonsterMovement(map, monsters));
Thread playerMovementThread = new Thread(() => StartPlayerMovement(map, map.currentHunter));

// Start both threads
monsterMovementThread.Start();
playerMovementThread.Start();


while (!GameOver)
{
    int newX = map.currentHunter.X;
    int newY = map.currentHunter.Y;
    // Ensure game loop updates regularly without blocking
    Thread.Sleep(map.currentHunter.FreezeTime); // Adjust the delay for game loop refresh rate

    // Clear the console before drawing the new state
    Console.Clear();

    // Display the map and current game state
    Console.WriteLine("=============================================");
    DisplayMap(map, map.currentHunter, monsters);
    Console.WriteLine("=============================================");
    Console.WriteLine($"Player: {name}             Map: {mapChosen}.map\n" +
                      $"HP:{map.currentHunter.CurrentHP}             Level: {level}\n" +
                      $"Score: {map.currentHunter.Score}             ");
    Console.WriteLine("=============================================");
    Console.WriteLine("Infos:");
    Console.WriteLine($"{map.info[map.info.Count - 1]}\n" +
        $"{map.info[map.info.Count - 2]}\n" +
        $"{map.info[map.info.Count - 3]}");
    
    Console.WriteLine("=============================================");
    if (map.currentHunter.IsDead())
    {
        GameOver = true;
    }
    
}

// Wait for the player and monster movement threads to finish before ending the game
monsterMovementThread.Join();
playerMovementThread.Join();
Console.WriteLine("Game Over!");





