using System;

namespace EscapeFromTheWodsApp {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");

            GameGenerator gmr = new GameGenerator();
            gmr.setTitleGame();
            gmr.CreateActionWithUI();
        }
    }
}
