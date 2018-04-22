using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shilka2.classes
{
    class FlyObject
    {
        public double x { get; set; }
        public double y { get; set; }
        public double sin { get; set; }
        public double cos { get; set; }
        public bool fly { get; set; }

        public static Random rand;

        static FlyObject()
        {
            rand = new Random();
        }
    }
}
