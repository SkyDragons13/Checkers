using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Model
{
    public class SaveData
    {
        public Board Board { get; set; }
        public bool JumpsEnabled { get; set; }
        public bool PlayerTurn { get; set; }
    }

}
