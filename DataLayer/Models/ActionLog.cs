using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models {
    public class ActionLog {

        public int Id { get; set; }

        public int WoodId { get; set; }

        public int MonkeyId { get; set; }

        public string Message { get; set; }

    }
}
