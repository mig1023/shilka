using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace shilka2.classes
{
    class DynamicElement
    {
        public enum MovingType { zRotate, xRotate, yRotate };
        public MovingType movingType;

        public Image element;

        public string elementName;

        public double x_left { get; set; }
        public double x_right { get; set; }
        public double y { get; set; }

        public double rotateDegreeCurrent = 0;

        public double startDegree = 1;

        public bool mirror = false;
    }
}
