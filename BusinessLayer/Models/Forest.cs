using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BusinessLayer.Models {
    public class Forest {
        public int id { get; set; }

        public int XMaxValue { get; set; }

        public int YMaxValue { get; set; }

        public int MonkeyCounter { get; set; }

        public List<Tree> Trees { get; set; } = new List<Tree>();

        public List<Monkey> Monkeys { get; set; } = new List<Monkey>();

        public List<string> Logs { get; set; } = new List<string>();

        public Image BitMap { get; set; }

        public Forest(int id, int trees, int monkeys) {
            this.id = id;
            this.XMaxValue = (int)(trees * 10);
            this.YMaxValue = XMaxValue;
            MonkeyCounter = LogController.DB.GetLastMonkeyId();
            GenerateTrees(trees);
            GenerateMonkeyCharacters(monkeys);
        }

        private List<string> NameList = new List<string>
        {
            "Louis",
            "Jan",
            "Pieter",
            "Henri",
            "Jeroen",
            "Milan",
            "Yorge"
        };

        private void GenerateTrees(int trees) {
            int counter = 0;
            Random r = new Random();
            for (int i = 0; i < trees; i++) {
                int r1 = r.Next(1, XMaxValue - 1);
                int r2 = r.Next(1, YMaxValue - 1);
                Tree tree = new Tree { Id = counter++, X = r1, Y = r2 };

                if (!Trees.Contains(tree)) {
                    LogController.Log.TaskList.Add(Task.Run(() => LogController.Log.TreeLog(this, tree)));
                    Console.WriteLine($"{id}: creating a tree at {tree.X},{tree.Y} with Id: {tree.Id}");
                    Trees.Add(tree);
                }
                else
                    i--;
            }
                Console.WriteLine($"{id}: Generated forest with {Trees.Count} amount of trees with Id: {tree.Id}");
                LogController.Log.CreateBitMap(this);
        }


        private void GenerateMonkeyCharacters(int monkeys) {
            Random rnd = new Random();
            if(id % 2 == 0)
                NameList.Reverse();
                for (int i = 0; i < monkeys + 1; i++) {
                    int random = rnd.Next(Trees.Count - 1);
                    Monkey monkeyOne = new Monkey(MonkeyCounter++, NameList[i]);
                    if (this.Trees[random].Monkey != null) {
                        i--;
                    }
                    else {
                        monkeyOne.passedTrees.Add(monkeyOne.Hops, Trees[random]);
                        Trees[random].Monkey = monkeyOne;
                        Monkeys.Add(monkeyOne);
                        Console.WriteLine($"{id}: Placing my new monkey: {monkeyOne.Naam} with ID {monkeyOne.Id} in tree: {Trees[random].Id}");
                        LogController.Log.WriteMonkeyToImage(this, Trees[random]);
                    }
                }
        }


        public Tree GetClosestTree(Tree t, Monkey m) {
            double n = XMaxValue;
            Tree tr = null;

            for(int i = 0; i < Trees.Count; i++) {
                if(Trees[i] != t && !m.passedTrees.ContainsValue(Trees[i])) {
                    if(GetDistance(t, Trees[i]) < n) {
                        n = GetDistance(t, Trees[i]);
                        tr = Trees[i];
                    }
                }
            }
            return tr;
        }

        public double GetDistance(Tree t1, Tree t2) {
            return Math.Sqrt(Math.Pow(t1.X - t2.X, 2) + Math.Pow(t1.Y - t2.Y, 2));
        }

        public double GetDistanceToBorder(Tree t) {
            double dist = (new List<double>()
            {
                XMaxValue - t.X,
                YMaxValue - t.Y,
                t.X - 0,
                t.Y - 0
            }).Min();
            return dist;
        }

    }
}
