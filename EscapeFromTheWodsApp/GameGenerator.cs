using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeFromTheWodsApp {
    public class GameGenerator {


        private void DeleteBlackspaces() {
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
            Console.WriteLine("                                                ");
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
        }

        public void setTitleGame() {
            Console.SetCursorPosition(Console.WindowWidth / 2 - 10, 0);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Be ready for............");
            Console.WriteLine("Escape from the woods");

            Console.WriteLine();
            Console.WriteLine();
            Console.ResetColor();
        }

        public void CreateActionWithUI() {
            int go;

            do {
                DeleteBlackspaces();
                Console.WriteLine("Hoeveel keer wenst u te spelen? ");
            } while (!int.TryParse(Console.ReadLine(), out go) && go < 20 && go > 0);

            int trees;

            do {
                DeleteBlackspaces();
                Console.WriteLine("Hoeveel bomen wilt u in het spel? ");
            } while (!int.TryParse(Console.ReadLine(), out trees) && trees < 150 && trees > 10);

            int monkeys;

            do {
                DeleteBlackspaces();
                Console.WriteLine("Hoeveel apen wilt u in het spel? ");
            } while (!int.TryParse(Console.ReadLine(), out monkeys) && monkeys < 23 && monkeys > 0);

            DeleteBlackspaces();
            GroundPlacementGame(go, trees, monkeys);
        }

        public void GroundPlacementGame(int go, int trees, int monkeys) {
            Console.WriteLine($"U hebt uw keuze gemaakt!");
            Console.WriteLine($"Het spel wordt gestart met {go} spelletjes met {trees} bomen en {monkeys} apen");
            Console.WriteLine();
            Player player = new Player();
            if (player.LetPlayerStart(go, trees, monkeys).Result) {
                Console.WriteLine("Spel wordt afgesloten!");
                Console.ReadLine();
            }
            
        }
    }
}
