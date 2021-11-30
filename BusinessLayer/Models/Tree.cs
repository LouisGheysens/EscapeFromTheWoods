using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models {
    public class Tree {

        public Tree() { }

        public Tree(int id) {

        }

        public int Id { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public Monkey Monkey { get; set; }
    }
}
