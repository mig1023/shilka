namespace shilka2
{
    public class Scripts
    {
        public enum scriptsNames {
            noScript,
            Vietnam,
            DesertStorm,
            Yugoslavia,
            IranIraq,
            Syria
        };

        public static int[] scriptFriendAircrafts(scriptsNames script)
        {

            if (script == scriptsNames.Vietnam)
                return new int[] {
                    4,      // <-- su17
                    11,     // <-- mig19
                    12,     // <-- mig21
                    16,     // <-- mi8
                };

            if (script == scriptsNames.Yugoslavia)
                return new int[] {
                    1,     // <-- mig23
                    2,     // <-- mig29
                    12,    // <-- mig21
                    13,    // <-- mig25
                    15,    // <-- mi24
                    16,    // <-- mi8
                };

            if (script == scriptsNames.DesertStorm)
                return new int[] {
                    1,     // <-- mig23
                    2,     // <-- mig29
                    6,     // <-- su25
                    13,    // <-- mig25
                    15,    // <-- mi24
                    16,    // <-- mi8
                };

            if (script == scriptsNames.IranIraq)
                return new int[] {
                    1,     // <-- mig23
                    4,     // <-- su17
                    11,    // <-- mig19
                    13,    // <-- mig25
                    15,    // <-- mi24
                    16,    // <-- mi8
                };

            if (script == scriptsNames.Syria)
                return new int[] {
                    1,     // <-- mig23
                    2,     // <-- mig29
                    4,     // <-- su17
                    14,    // <-- mi28
                    15,    // <-- mi24
                    16,    // <-- mi8
                };

            return new int[] { };
        }

        public static int[] scriptFriendHelicopterss(scriptsNames script)
        {

            if (script == scriptsNames.Vietnam)
                return new int[] {
                    3,     // <-- mi8
                };

            if (script == scriptsNames.Yugoslavia)
                return new int[] {
                    2,     // <-- mi24
                    3,     // <-- mi8
                };

            if (script == scriptsNames.DesertStorm)
                return new int[] {
                    2,     // <-- mi24
                    3,     // <-- mi8
                };

            if (script == scriptsNames.IranIraq)
                return new int[] {
                    2,     // <-- mi24
                    3,     // <-- mi8
                };

            if (script == scriptsNames.Syria)
                return new int[] {
                    1,     // <-- mi28
                    2,     // <-- mi24
                    3,     // <-- mi8
                };

            return new int[] { };
        }

        public static int[] scriptEnemyAircrafts(scriptsNames script)
        {

            if (script == scriptsNames.Vietnam)
                return new int[] {
                    3,      // <-- b52
                    5,      // <-- f14
                    10,     // <-- f4
                    21,     // <-- f8
                    22,     // <-- ac130
                    23,     // <-- a6
                    24,     // <-- f111
                    25,     // <-- f5
                };

            if (script == scriptsNames.Yugoslavia)
                return new int[] {
                    1,     // <-- a10
                    2,     // <-- b1
                    3,     // <-- b52
                    4,     // <-- f117
                    5,     // <-- f14
                    6,     // <-- f18
                    7,     // <-- f16
                    9,     // <-- f15
                    10,    // <-- f4
                    11,    // <-- tornado
                    12,    // <-- predator
                    15,    // <-- e3
                    18,    // <-- b2
                    20,    // <-- tomahawk
                };

            if (script == scriptsNames.DesertStorm)
                return new int[] {
                    1,     // <-- a10
                    2,     // <-- b1
                    3,     // <-- b52
                    4,     // <-- f117
                    5,     // <-- f14
                    6,     // <-- f18
                    7,     // <-- f16
                    9,     // <-- f15
                    10,    // <-- f4
                    11,    // <-- tornado
                    15,    // <-- e3
                    18,    // <-- b2
                    20,    // <-- tomahawk
                    22,    // <-- ac130
                    23,    // <-- a6
                    24,    // <-- f111
                    25,    // <-- f5
                    27,    // <-- ea6
                };

            if (script == scriptsNames.IranIraq)
                return new int[] {
                    5,     // <-- f14
                    10,    // <-- f4
                    22,    // <-- ac130
                    25,    // <-- f5
                };

            if (script == scriptsNames.Syria)
                return new int[] {
                    20,    // <-- tomahawk
                    12,    // <-- predator
                    13,    // <-- reaper
                    26,    // <-- scalp
                };

            return new int[] { };
        }

        public static int[] scriptEnemyHelicopters(scriptsNames script)
        {

            if (script == scriptsNames.Vietnam)
                return new int[] {
                    2,     // <-- ah1
                    4,     // <-- uh1
                };

            if (script == scriptsNames.Yugoslavia)
                return new int[] {
                    1,     // <-- ah64
                    2,     // <-- ah1
                    3,     // <-- uh60
                    4,     // <-- uh1
                };

            if (script == scriptsNames.DesertStorm)
                return new int[] {
                    1,     // <-- ah64
                    2,     // <-- ah1
                    3,     // <-- uh60
                    4,     // <-- uh1
                };

            if (script == scriptsNames.IranIraq)
                return new int[] {
                    2,     // <-- ah1
                };

            if (script == scriptsNames.Syria)
                return new int[] {
                    1,     // <-- ah64
                };

            return new int[] { };
        }
    }
}
