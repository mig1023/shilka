using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace shilka2
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

        public bool background = false;

        public bool slowRotation = false;

        public bool backSide = false;

        public bool currentSide = false;

        public bool stopRotation = false;

        public static DynamicElement Clone(DynamicElement element)
        {
            DynamicElement newElement = new DynamicElement();

            newElement.elementName = element.elementName;
            newElement.x_left = element.x_left;
            newElement.x_right = element.x_right;
            newElement.y = element.y;
            newElement.rotateDegreeCurrent = element.rotateDegreeCurrent;
            newElement.startDegree = element.startDegree;
            newElement.mirror = element.mirror;
            newElement.background = element.background;
            newElement.slowRotation = element.slowRotation;
            newElement.backSide = element.backSide;
            newElement.currentSide = element.currentSide;
            newElement.stopRotation = element.stopRotation;
            newElement.movingType = element.movingType;

            return newElement;
        }
    }
}
