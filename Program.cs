using System;
using System.Collections.Generic;
using System.Threading;

class Program {
  static int playerY = Console.WindowHeight / 2;
  static int score = 0;
  static List < int > obstacles = new List < int > ();
  static int lowerBase = Console.WindowHeight - 1;
  static int upperBase = 3;
  static Random random = new Random();
  static int obstacleSpeed = 1;
  static bool isGameOver = false;

  static void Main() {
    Console.Title = "Nigger Game";
    Console.CursorVisible = false;
    Console.WindowHeight = 20;
    Console.WindowWidth = 40;

    Console.ForegroundColor = ConsoleColor.Red;

    ShowTitleScreen();
    InitializeObstacles();

    while (true) {
      if (!isGameOver) {
        if (Console.KeyAvailable) {
          var key = Console.ReadKey(true).Key;
          if (key == ConsoleKey.Escape) {
            Environment.Exit(0);
          } else if (key == ConsoleKey.W || key == ConsoleKey.UpArrow) {
            playerY = Math.Max(playerY - 1, upperBase + 2);
          } else if (key == ConsoleKey.S || key == ConsoleKey.DownArrow) {
            playerY = Math.Min(playerY + 1, lowerBase - 1);
          }
        }

        UpdateGame();
        DrawGame();
      } else {
        if (Console.KeyAvailable) {
          var key = Console.ReadKey(true).Key;
          if (key == ConsoleKey.Escape) {
            Environment.Exit(0);
          } else if (key == ConsoleKey.Y) {
            isGameOver = false;
            playerY = Console.WindowHeight / 2;
            score = 0;
            obstacleSpeed = 1;
            InitializeObstacles();
          } else if (key == ConsoleKey.N) {
            Environment.Exit(0);
          }
        }
      }

      Thread.Sleep(50);
    }
  }

  static void ShowTitleScreen() {
    Console.WriteLine();
    Console.WriteLine("        .--.       .--.");
    Console.WriteLine("    _  `    \\     /    `  _");
    Console.WriteLine("     `\\.===. \\.^./ .===./`");
    Console.WriteLine("            \\/`\"`\\/");
    Console.WriteLine("         ,  |     |  ,");
    Console.WriteLine("        / `\\|`-.-'|/` \\");
    Console.WriteLine("       /    |  \\  |    \\");
    Console.WriteLine("    .-' ,-'`|   ; |`'-, '-.");
    Console.WriteLine("        |   |    \\|   | ");
    Console.WriteLine("        |   |    ;|   |");
    Console.WriteLine("        |   \\    //   |");
    Console.WriteLine("        |    `._//'   |");
    Console.WriteLine("       .'             `.");
    Console.WriteLine("    _,'                 `,");
    Console.WriteLine("    `                     `");
    Thread.Sleep(5000);
  }

  static void InitializeObstacles() {
    obstacles.Clear();
    for (int i = 0; i < Console.WindowWidth; i += random.Next(10, 20)) {
      int obstacleHeight = random.Next(upperBase + 2, lowerBase - 2);
      obstacles.Add(i);
      obstacles.Add(obstacleHeight);
    }
  }

  static void UpdateGame() {
    if (isGameOver) {
      return;
    }

    List < int > newObstacles = new List < int > ();
    bool obstacleHit = false;

    for (int i = 0; i < obstacles.Count; i += 2) {
      int obstacleX = obstacles[i] - obstacleSpeed;
      int obstacleHeight = obstacles[i + 1];

      if (obstacleX > 0) {
        newObstacles.Add(obstacleX);
        newObstacles.Add(obstacleHeight);
      } else if (obstacleX <= 0) {
        score++;
      }
      if (obstacleX <= Console.WindowWidth / 2 && obstacleX >= Console.WindowWidth / 2 - 1 && playerY == obstacleHeight) {
        isGameOver = true;
        obstacleHit = true;
        break;
      }
    }

    obstacles = newObstacles;

    if (!isGameOver && random.Next(0, 10) == 0) {
      int obstacleHeight = random.Next(upperBase + 2, lowerBase - 2);
      obstacles.Add(Console.WindowWidth - 1);
      obstacles.Add(obstacleHeight);
    }
    if (score >= 50) {
      obstacleSpeed = 2;
    }
    if (score >= 100) {
      obstacleSpeed = 4;
    }
    if (obstacleHit && obstacleSpeed > 1) {
      isGameOver = true;
    } else if (obstacleHit) {
      isGameOver = true;
    }
  }

  static void DrawGame() {
    Console.Clear();
    for (int i = 0; i < Console.WindowWidth; i++) {
      Console.SetCursorPosition(i, upperBase);
      Console.Write("█");
      Console.SetCursorPosition(i, lowerBase);
      Console.Write("█");
    }

    for (int i = 0; i < obstacles.Count; i += 2) {
      int obstacleX = obstacles[i];
      int obstacleHeight = obstacles[i + 1];
      if (obstacleX < Console.WindowWidth) {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.SetCursorPosition(obstacleX, obstacleHeight);
        Console.Write("█");
        Console.ForegroundColor = ConsoleColor.White;
      }
    }

    if (playerY >= upperBase + 2 && playerY <= lowerBase - 1) {
      Console.SetCursorPosition(Console.WindowWidth / 2, playerY);
      Console.Write("█");
    }

    Console.SetCursorPosition(0, 0);

    if (!isGameOver) {
      Console.Write($"Score: {score}");
    } else {
      Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2);
      Console.Write("Game Over!");
      Console.SetCursorPosition(Console.WindowWidth / 2 - 10, Console.WindowHeight / 2 + 1);
      Console.Write($"Your Score: {score}");
      Console.SetCursorPosition(Console.WindowWidth / 2 - 10, Console.WindowHeight / 2 + 3);
      Console.Write("y/n");
    }
  }
}