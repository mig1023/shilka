using System;

namespace shilka2
{
    public class FlyObject
    {
        public enum FlightDirectionType { Left, Right };

        public double x { get; set; }
        public double y { get; set; }
        public double sin { get; set; }
        public double cos { get; set; }
        public bool fly { get; set; }
        public double speed { get; set; }

        public FlightDirectionType flightDirection;

        public static Random rand;

        static FlyObject()
        {
            rand = new Random();
        }

        public static double SpeedInStorm(double flySpeed, ref FlightDirectionType flightDirect)
        {
            if ((flightDirect != Weather.stormDirection) && (flySpeed > 0))
                return flySpeed - rand.Next(4) * 0.1;
            else if ((flightDirect != Weather.stormDirection) && (flySpeed <= 0))
                flightDirect = Weather.stormDirection;
            else if ((flightDirect == Weather.stormDirection) && (flySpeed < Constants.CLOUD_SPEED))
                return flySpeed + rand.Next(4) * 0.1;

            return flySpeed;
        }
    }
}
