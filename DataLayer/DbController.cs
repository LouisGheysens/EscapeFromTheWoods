using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer {
    public class DbController {

        public void AddActionLog(ActionLog a) {
            using (var context = new MonkeyDbContext()) {
                context.ActionLogs.Add(a);
                context.SaveChanges();
            }
        }

            public void AddTreeLog(TreeLog x) {
            using (var context = new MonkeyDbContext()) {
                context.TreeLogs.Add(x);
                context.SaveChanges();
            }
        }

        public void AddMonkeyLog(MonkeyLog mk) {
            using(var context = new MonkeyDbContext()) {
                context.MonkeyLogs.Add(mk);
                context.SaveChanges();
            }
        }

        public int GetLastWoodId() {
            int id = 0;
            using(var ctx = new MonkeyDbContext()) {
                int log = ctx.ActionLogs.Count();
                if(log > 0) {
                    id = ctx.ActionLogs.ToList().Last().WoodId;
                }
                return id;
            }
        }

        public int GetLastMonkeyId() {
            int id = 0;
            using(var context = new MonkeyDbContext()) {
                int log = context.ActionLogs.Count();
                if(log > 0) {
                    id = context.ActionLogs.Select(x => x.MonkeyId).ToList().Max();
                }
                return id;
            }
        }

        }
    }
