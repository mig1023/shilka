namespace shilka2
{
    public class Scripts
    {
        public enum scriptsNames {
            noScript,
            Vietnam,
            KoreanBoeing,
            DesertStorm,
            Yugoslavia,
            IranIraq,
            Syria,
            Libya,
            Yemen,
            Rust,
            F117Hunt
        };

        public static int?[] scriptAircraft;
        public static int?[] scriptHelicopters;
        public static int?[] scriptAircraftFriend;
        public static int?[] scriptHelicoptersFriend;

        public static string scriptFlagName(scriptsNames script)
        {
            string flagName = null;

            switch (script)
            {
                case Scripts.scriptsNames.Vietnam:
                    flagName = "vn";
                    break;
                case Scripts.scriptsNames.IranIraq:
                    flagName = "in";
                    break;
                case Scripts.scriptsNames.DesertStorm:
                    flagName = "iq";
                    break;
                case Scripts.scriptsNames.Syria:
                    flagName = "sy";
                    break;
                case Scripts.scriptsNames.Yugoslavia:
                    flagName = "yu";
                    break;
                case Scripts.scriptsNames.KoreanBoeing:
                    flagName = "ki";
                    break;
                case Scripts.scriptsNames.Libya:
                    flagName = "ly";
                    break;
                case Scripts.scriptsNames.Yemen:
                    flagName = "ye";
                    break;
                case Scripts.scriptsNames.Rust:
                    flagName = "su";
                    break;
                case Scripts.scriptsNames.F117Hunt:
                    flagName = "yu";
                    break;
            };

            return flagName;
        }

        public static int?[] scriptFriendAircrafts(scriptsNames script)
        {

            if (script == scriptsNames.Vietnam)
                return new int?[] {
                    4,      // <-- su17
                    11,     // <-- mig19
                    12,     // <-- mig21
                };

            if (script == scriptsNames.Yugoslavia)
                return new int?[] {
                    1,     // <-- mig23
                    2,     // <-- mig29
                    12,    // <-- mig21
                    13,    // <-- mig25
                };

            if (script == scriptsNames.DesertStorm)
                return new int?[] {
                    1,     // <-- mig23
                    2,     // <-- mig29
                    6,     // <-- su25
                    13,    // <-- mig25
                };

            if (script == scriptsNames.IranIraq)
                return new int?[] {
                    1,     // <-- mig23
                    4,     // <-- su17
                    11,    // <-- mig19
                    13,    // <-- mig25
                };

            if (script == scriptsNames.Syria)
                return new int?[] {
                    1,     // <-- mig23
                    2,     // <-- mig29
                    4,     // <-- su17
                    14,    // <-- a50
                    16,    // <-- tu95
                };

            if (script == scriptsNames.Libya)
                return new int?[] {
                    1,     // <-- mig23
                    4,     // <-- su17
                    5,     // <-- su24
                    13,    // <-- mig25
                };

            if (script == scriptsNames.KoreanBoeing)
                return null;

            if (script == scriptsNames.Yemen)
                return null;

            if (script == scriptsNames.F117Hunt)
                return null;

            if (script == scriptsNames.Rust)
                return null;



            return new int?[] { };
        }

        public static int?[] scriptFriendHelicopterss(scriptsNames script)
        {

            if (script == scriptsNames.Vietnam)
                return null;

            if (script == scriptsNames.Yugoslavia)
                return null;

            if (script == scriptsNames.DesertStorm)
                return new int?[] {
                    2,     // <-- mi24
                    3,     // <-- mi8
                };

            if (script == scriptsNames.IranIraq)
                return new int?[] {
                    2,     // <-- mi24
                    3,     // <-- mi8
                };

            if (script == scriptsNames.Syria)
                return new int?[] {
                    1,     // <-- mi28
                    2,     // <-- mi24
                    3,     // <-- mi8
                    4,     // <-- ka52
                    5,     // <-- ka27
                };

            if (script == scriptsNames.Libya)
                return new int?[] {
                    2,     // <-- mi24
                    3,     // <-- mi8
                };

            if (script == scriptsNames.KoreanBoeing)
                return null;

            if (script == scriptsNames.Yemen)
                return null;

            if (script == scriptsNames.F117Hunt)
                return null;

            if (script == scriptsNames.Rust)
                return null;

            return new int?[] { };
        }

        public static int?[] scriptEnemyAircrafts(scriptsNames script)
        {

            if (script == scriptsNames.Vietnam)
                return new int?[] {
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
                return new int?[] {
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
                    32,    // <-- harrier
                };

            if (script == scriptsNames.DesertStorm)
                return new int?[] {
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
                    31,    // <-- sr71
                    32,    // <-- harrier
                };

            if (script == scriptsNames.IranIraq)
                return new int?[] {
                    5,     // <-- f14
                    10,    // <-- f4
                    22,    // <-- ac130
                    25,    // <-- f5
                };

            if (script == scriptsNames.Syria)
                return new int?[] {
                    20,    // <-- tomahawk
                    12,    // <-- predator
                    13,    // <-- reaper
                    26,    // <-- scalp
                };

            if (script == scriptsNames.KoreanBoeing)
                return new int?[] {
                    29,    // <-- rc135
                    30,    // <-- u2
                    31,    // <-- sr71
                };

            if (script == scriptsNames.Libya)
                return new int?[] {
                    1,     // <-- a10
                    3,     // <-- b52
                    6,     // <-- f18
                    7,     // <-- f16
                    9,     // <-- f15
                    11,    // <-- tornado
                    15,    // <-- e3
                    20,    // <-- tomahawk
                    23,    // <-- a6
                    27,    // <-- ea6
                    32,    // <-- harrier
                };

            if (script == scriptsNames.Yemen)
                return new int?[] {
                    7,     // <-- f16
                    9,     // <-- f15
                    11,    // <-- tornado
                    15,    // <-- e3
                    16,    // <-- eurofighter
                    22,    // <-- ac130
                    25,    // <-- f5
                };

            if (script == scriptsNames.F117Hunt)
                return new int?[] { };

            if (script == scriptsNames.Rust)
                return new int?[] {
                    33,    // <-- cessna
                };

            return new int?[] { };
        }

        public static int?[] scriptEnemyHelicopters(scriptsNames script)
        {

            if (script == scriptsNames.Vietnam)
                return new int?[] {
                    2,     // <-- ah1
                    4,     // <-- uh1
                    5,     // <-- ch46
                };

            if (script == scriptsNames.Yugoslavia)
                return new int?[] {
                    1,     // <-- ah64
                    2,     // <-- ah1
                    3,     // <-- uh60
                    4,     // <-- uh1
                    5,     // <-- ch46
                };

            if (script == scriptsNames.DesertStorm)
                return new int?[] {
                    1,     // <-- ah64
                    2,     // <-- ah1
                    3,     // <-- uh60
                    4,     // <-- uh1
                    5,     // <-- ch46
                };

            if (script == scriptsNames.IranIraq)
                return new int?[] {
                    2,     // <-- ah1
                    5,     // <-- ch46
                };

            if (script == scriptsNames.Syria)
                return null;

            if (script == scriptsNames.Libya)
                return new int?[] {
                    1,     // <-- ah64
                    3,     // <-- uh60
                    5,     // <-- ch46
                };

            if (script == scriptsNames.KoreanBoeing)
                return null;

            if (script == scriptsNames.Yemen)
                return new int?[] {
                    1,     // <-- ah64
                    2,     // <-- ah1
                    3,     // <-- uh60
                    5,     // <-- ch46
                };

            if (script == scriptsNames.F117Hunt)
                return null;

            if (script == scriptsNames.Rust)
                return null;

            return new int?[] { };
        }
    }
}
