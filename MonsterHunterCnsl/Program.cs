using MonsterHunter;
using System;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Collections.Generic;

bool GameOver = false;
int level = 1;
int monFreezeTime = 2000;

void UpdateLeaderboard(string playerName, int playerScore)
{
    try
    {
        string filePath = "leaderboard.txt";

        // List to hold player scores
        List<(string Name, int Score)> scores = new List<(string, int)>();

        // Check if leaderboard file exists and read scores
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split('|');
                if (parts.Length == 2 && int.TryParse(parts[1], out int score))
                {
                    scores.Add((parts[0], score));
                }
            }
        }

        // Add the new score
        scores.Add((playerName, playerScore));

        // Sort scores in descending order and keep top 10
        scores = scores.OrderByDescending(s => s.Score).Take(10).ToList();

        // Write updated scores back to file
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var (Name, Score) in scores)
            {
                writer.WriteLine($"{Name}|{Score}");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error updating leaderboard: {ex.Message}");
    }
}

void StartPlayerMovement(Map map, Hunter hunter)
{
    try
    {
        while (!GameOver)
        {
            PlayerMove(map, hunter);
            Thread.Sleep(hunter.FreezeTime); // Control player movement speed
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error in player movement: {ex.Message}");
    }
}

void PlayerMove(Map map, Hunter hunter)
{
    try
    {
        int newX = hunter.X;
        int newY = hunter.Y;

        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    newY = (newY - 1 >= 0) ? newY - 1 : newY;
                    break;
                case ConsoleKey.DownArrow:
                    newY = (newY + 1 < map.Width) ? newY + 1 : newY;
                    break;
                case ConsoleKey.LeftArrow:
                    newX = (newX - 1 >= 0) ? newX - 1 : newX;
                    break;
                case ConsoleKey.RightArrow:
                    newX = (newX + 1 < map.Height) ? newX + 1 : newX;
                    break;
            }

            hunter.Move(newX, newY, map);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error processing player input: {ex.Message}");
    }
}

void MonsterMove(Map map, Monsters monsters)
{
    try
    {
        Monster[] monList = Monsters._monsters.ToArray();
        foreach (Monster mon in monList)
        {
            int direction = MonsterHunter.Random.Instance.Next(0, 4);
            int newX = mon.X;
            int newY = mon.Y;

            switch (direction)
            {
                case 0:
                    newY = (newY - 1 >= 0) ? newY - 1 : newY;
                    break;
                case 1:
                    newY = (newY + 1 < map.Width) ? newY + 1 : newY;
                    break;
                case 2:
                    newX = (newX - 1 >= 0) ? newX - 1 : newX;
                    break;
                case 3:
                    newX = (newX + 1 < map.Height) ? newX + 1 : newX;
                    break;
            }

            mon.Move(newX, newY, map);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error moving monsters: {ex.Message}");
    }
}

void StartMonsterMovement(Map map, Monsters monsters)
{
    try
    {
        while (!GameOver)
        {
            MonsterMove(map, monsters);
            Thread.Sleep(monFreezeTime);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error in monster movement: {ex.Message}");
    }
}

void DisplayMap(Map map, Hunter hunter, Monsters monsters)
{
    try
    {
        for (int y = 0; y < map.Width; y++)
        {
            for (int x = 0; x < map.Height; x++)
            {
                if (hunter.X == x && hunter.Y == y)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write('H');
                }
                else if (Monsters.FindMonstersAtPosition(x, y).Length != 0 && (x != 0 && y != 0))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write('M');
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(map.MapData[x, y]);
                }
            }
            Console.WriteLine();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error displaying map: {ex.Message}");
    }
}

try
{
    Console.WriteLine("Enter your name: ");
    string name = Console.ReadLine();

    while (string.IsNullOrEmpty(name) || name.Length > 19)
    {
        Console.WriteLine("Invalid name, try again");
        name = Console.ReadLine();
    }

    Map map = new Map();
    Monsters monsters = new Monsters();

    Console.WriteLine("Enter map name: ");
    string mapChosen = Console.ReadLine();

    while (!map.AvailableMaps.Contains(mapChosen))
    {
        Console.WriteLine("Map doesn't exist");
        mapChosen = Console.ReadLine();
    }

    map.LoadMap(mapChosen, monsters, name);

    Thread monsterMovementThread = new Thread(() => StartMonsterMovement(map, monsters));
    Thread playerMovementThread = new Thread(() => StartPlayerMovement(map, map.currentHunter));

    monsterMovementThread.Start();
    playerMovementThread.Start();

    while (!GameOver)
    {
        Thread.Sleep(map.currentHunter.FreezeTime);
        Console.Clear();
        Console.WriteLine("=============================================");
        DisplayMap(map, map.currentHunter, monsters);
        Console.WriteLine("=============================================");
        Console.WriteLine($"Player: {name}             Map: {mapChosen}.map\n" +
                          $"HP:{map.currentHunter.CurrentHP}             Level: {level}\n" +
                          $"Score: {map.currentHunter.Score}             ");
        Console.WriteLine("=============================================");
        Console.WriteLine("Infos:");
        Console.WriteLine($"{map.info[^3]}\n{map.info[^2]}\n{map.info[^1]}");
        Console.WriteLine("=============================================");

        if (map.currentHunter.IsDead())
        {
            GameOver = true;
            UpdateLeaderboard(map.currentHunter.Name, map.currentHunter.Score);

            Console.WriteLine("Leaderboard updated:");
            if (File.Exists("leaderboard.txt"))
            {
                string[] leaderboard = File.ReadAllLines("leaderboard.txt");
                foreach (var line in leaderboard)
                {
                    Console.WriteLine(line.Replace("|", ": "));
                }
            }
        }
        else if (map.currentHunter.Levelup)
        {
            map.currentHunter.Levelup = false;
            level++;
            map.LoadMap(mapChosen, monsters, name);
            monFreezeTime -= 100;
        }
    }

    monsterMovementThread.Join();
    playerMovementThread.Join();
    Console.WriteLine("Game Over!");
}
catch (Exception ex)
{
    Console.WriteLine($"Critical error: {ex.Message}");
}





