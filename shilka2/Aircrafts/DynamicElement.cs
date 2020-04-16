using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace shilka2
{
    public class DynamicElement
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

        public bool positiveDirection = false;

        public bool oneWay = false;

        public static DynamicElement Clone(DynamicElement element)
        {
            DynamicElement newElement = new DynamicElement
            {
                elementName = element.elementName,
                x_left = element.x_left,
                x_right = element.x_right,
                y = element.y,
                rotateDegreeCurrent = element.rotateDegreeCurrent,
                startDegree = element.startDegree,
                mirror = element.mirror,
                background = element.background,
                slowRotation = element.slowRotation,
                backSide = element.backSide,
                currentSide = element.currentSide,
                movingType = element.movingType,
                oneWay = element.oneWay,
                positiveDirection = element.positiveDirection
            };

            return newElement;
        }
    }
}
