using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;

namespace Monopoly
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            GameController game = new GameController();
            bool startGame = false;

            while (!startGame)
            {
                Console.WriteLine("\nEnter player name or type 'start' to begin the game:");

                string input = Console.ReadLine();

                if (input.ToLower() == "start")
                {
                    if (game.GetPlayers().Count < 2)
                    {
                        Console.WriteLine("\nPlease add at least 2 players to start the game.");
                        continue;
                    }

                    startGame = true;
                    Console.WriteLine("\nGame started!");
                }
                else
                {
                    game.AddPlayer(input, 2000);
                    Console.WriteLine($"\n{input} has joined the game with $2000.");
                }
            }

            game.PlayGame();
        }
    }
}