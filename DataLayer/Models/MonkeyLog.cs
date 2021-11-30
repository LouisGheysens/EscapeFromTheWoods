using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models {
    public class MonkeyLog {
        public int Id { get; set; }

        public int MonkeyId { get; set; }

        public string MonkeyName { get; set; }

        public int WoodId { get; set; }

        public int SequenceNumber { get; set; }

        public int TreeId { get; set; }

        public int X { get; set; }

        public int Y { get; set; }
    }
}
