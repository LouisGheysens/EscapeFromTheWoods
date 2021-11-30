using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLayer {
    public class Player {

        public Player(int fc) {
            this.ForestReaper = fc;
        }

        public int ForestReaper { get; set; }

        public async Task<bool> LetPlayerStart(int games, int trees, int monkeys) {
            List<Task> tasks = new List<Task>();

            for(int i = 0; i < games; i++) {
                Task t = new Task(() => PlayGame(trees, monkeys));
                tasks.Add(t);
            }
            foreach(Task t in tasks) {
                t.Start();
            }
            int count = GetFileCount();
            while(count != games) {
                Thread.Sleep(1000);
                count = GetFileCount();
            }
            await Task.WhenAll(LogController.Log.TaskList);
            return true;
        }

        public void PlayGame(int trees, int monkeys) {
            Forest f = new Forest(ForestReaper++, trees, monkeys);
            for(int i = 0; i < f.Monkeys.Count; i++) {
                Monkey mk = f.Monkeys[i];
                mk.Jump(f);
            }
            LogController.Log.WriteImage(f.id, f.BitMap);
            LogController.Log.DefineTxtLog(f);
        }

        public int GetFileCount() {
            DirectoryInfo di = new DirectoryInfo(LogController.Log.pathtomypersonaldisk);
            int count = di.GetFiles().Where(x => x.Extension == ".jpg").Count();
            return count;
        }
    }
}
