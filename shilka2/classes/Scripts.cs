namespace shilka2
{
    public class Scripts
    {
        public enum scriptsNames {
            noScript,
            Vietnam,
            DesertStorm,
            Yugoslavia
        };

        public static int[] scriptFriendAircrafts(scriptsNames script)
        {

            if (script == scriptsNames.Vietnam)
                return new int[] {
                    4,      // <-- su17
                    11,     // <-- mig19
                    12      // <-- mig21
                };

            if (script == scriptsNames.Yugoslavia)
                return new int[] {
                    1,     // <-- mig23
                    2,     // <-- mig29
                    12,    // <-- mig21
                    13     // <-- mig25
                };

            if (script == scriptsNames.DesertStorm)
                return new int[] {
                    1,     // <-- mig23
                    2,     // <-- mig29
                    6,     // <-- su25
                    13     // <-- mig25
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
                        24      // <-- f111
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
                        24     // <-- f111
                    };

            return new int[] { };
        }
    }
}
