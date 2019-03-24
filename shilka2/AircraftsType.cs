using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace shilka2
{
    class AircraftsType
    {
        public string aircraftType;
        public int[] size;
        public List<DynamicElement> elements;
        public int hitPoint = 80;
        public string aircraftName = "";
        public int speed = 10;
        public int minAltitude = -1;
        public int maxAltitude = -1;
        public bool friend = false;
        public bool airliner = false;
        public bool cloud = false;
        public bool cantEscape = false;
        public double price = 0;
    }
}
