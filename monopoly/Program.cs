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
            game.StartGame();
        }
    }
}