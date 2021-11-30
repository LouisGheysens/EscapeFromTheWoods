using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models {
    public class Monkey {

        public Monkey(int id, string naam) {
            this.Id = id;
            this.Naam = naam;
        }

        public int Id { get; set; }
        public string Naam { get; set; }
        public int Hops { get; set; }
        public SortedList<int, Tree> passedTrees { get; set; } = new SortedList<int, Tree>();
        public SortedList<int, Tree> CalculatedPath { get; set; } = new SortedList<int, Tree>();

        public bool Jump(Forest f) {
            do {
                Tree tree = f.GetClosestTree(passedTrees.Last().Value, this);
                double distanceToBorder = f.GetDistanceToBorder(passedTrees.Last().Value);
                double distanceToClosestTree = f.GetDistance(passedTrees.Last().Value, tree);
                if (distanceToBorder < distanceToClosestTree) {
                    CalculatedPath.Add(++Hops, new Tree(9696));

                    Console.WriteLine($"{Id}: Monkey: {Naam} jumped out the forest!");
                    LogController.Log.TaskList.Add(Task.Run(() => LogController.Log.ActionLog(f.id, this.Id, $"{f.id}: Monkey: {Naam} jumped out of the forest")));
                    f.TextLogs($"{f.id}: Monkey: {Naam} jumped out of the forest");
                }
                else {
                    Tree trel = passedTrees[Hops];
                    CalculatedPath.Add(++Hops, tree);
                    passedTrees.Add(Hops, tree);

                    Console.WriteLine($"{f.id}: Monkey: {Naam} jumps from {trel.Id} to {tree.Id} at {tree.X}, {tree.Y}");
                    LogController.Log.TaskList.Add(Task.Run(() => LogController.Log.ActionLog(f.id, this.Id, $"{f.id}: Monkey: {Naam} jumps from {trel.Id} to {tree.Id} at {tree.X}, {tree.Y}"));
                    LogController.Log.TaskList.Add(Task.Run(() => LogController.Log.MonkeyLog(f, this, tree)));
                    f.Logs.Add($"{f.id}:  Monkey: {Naam} jumps from {trel.Id} to {tree.Id} at {tree.X}, {tree.Y}");
                    LogController.Log.LetMonkeyJumpInTheWood(f, trel, tree);
                }
            } while (CalculatedPath.Last().Value.Id != 9696);
            return true;
        }
        


    }
}
