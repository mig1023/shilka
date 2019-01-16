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
            F117Hunt,
            Khmeimim
        };

        public static int?[] scriptAircraft;
        public static int?[] scriptHelicopters;
        public static int?[] scriptAircraftFriend;
        public static int?[] scriptHelicoptersFriend;
        public static int?[] scriptAirliners;

        public static string ScriptFlagName(scriptsNames script)
        {
            switch (script)
            {
                case Scripts.scriptsNames.Vietnam: return "vn";
                case Scripts.scriptsNames.IranIraq: return "ir";
                case Scripts.scriptsNames.DesertStorm: return "iq";
                case Scripts.scriptsNames.Syria: return "sy";
                case Scripts.scriptsNames.Yugoslavia: return "yu";
                case Scripts.scriptsNames.KoreanBoeing: return "ki";
                case Scripts.scriptsNames.Libya: return "ly";
                case Scripts.scriptsNames.Yemen: return "ye";
                case Scripts.scriptsNames.Rust: return "su";
                case Scripts.scriptsNames.F117Hunt: return "yu";
                case Scripts.scriptsNames.Khmeimim: return "sy";
            };

            return null;
        }

        public static int?[] ScriptFriendAircrafts(scriptsNames script)
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

            if (script == scriptsNames.Khmeimim)
                return new int?[] {
                    2,     // <-- mig29
                    3,     // <-- mig31
                    5,     // <-- su24
                    6,     // <-- su25
                    7,     // <-- su27
                    8,     // <-- su34
                    9,     // <-- pakfa
                    10,    // <-- tu160
                    14,    // <-- a50
                    15,    // <-- tu95
                };

            return new int?[] { };
        }

        public static int?[] ScriptFriendHelicopters(scriptsNames script)
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

            if (script == scriptsNames.Khmeimim)
                return new int?[] {
                    1,     // <-- mi28
                    2,     // <-- mi24
                    3,     // <-- mi8
                    4,     // <-- ka52
                };

            return new int?[] { };
        }

        public static int?[] ScriptEnemyAircrafts(scriptsNames script)
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
                    34,    // <-- hunter
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
                    34,    // <-- hunter
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
                    34,    // <-- hunter
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

            if (script == scriptsNames.Khmeimim)
                return new int?[] { };

            return new int?[] { };
        }

        public static int?[] ScriptEnemyHelicopters(scriptsNames script)
        {

            if (script == scriptsNames.Vietnam)
                return new int?[] {
                    2,     // <-- ah1
                    4,     // <-- uh1
                    5,     // <-- ch46
                    14,    // <-- mh53
                };

            if (script == scriptsNames.Yugoslavia)
                return new int?[] {
                    1,     // <-- ah64
                    2,     // <-- ah1
                    3,     // <-- uh60
                    4,     // <-- uh1
                    5,     // <-- ch46
                    13,    // <-- puma
                    15,    // <-- as565
                };

            if (script == scriptsNames.DesertStorm)
                return new int?[] {
                    1,     // <-- ah64
                    2,     // <-- ah1
                    3,     // <-- uh60
                    4,     // <-- uh1
                    5,     // <-- ch46
                    13,    // <-- puma
                    15,    // <-- as565
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
                    15,    // <-- as565
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

            if (script == scriptsNames.Khmeimim)
                return new int?[] {
                    8,     // <-- drone
                    16,    // <-- drone2
                };

            return new int?[] { };
        }

        public static int?[] ScriptAirliners(scriptsNames script)
        {

            if (script == scriptsNames.Vietnam)
                return new int?[] {
                    2,     // <-- b747
                    7,     // <-- b707
                    8,     // <-- l1049
                };

            if (script == scriptsNames.Yugoslavia)
                return new int?[] {
                    1,     // <-- a320
                    2,     // <-- b747
                    3,     // <-- md11
                    4,     // <-- atr42
                    5,     // <-- dhc8
                    11,    // <-- b777
                    12,    // <-- il114
                };

            if (script == scriptsNames.DesertStorm)
                return new int?[] {
                    1,     // <-- a320
                    2,     // <-- b747
                    3,     // <-- md11
                    4,     // <-- atr42
                    5,     // <-- dhc8
                };

            if (script == scriptsNames.IranIraq)
                return new int?[] {
                    2,     // <-- b747
                    4,     // <-- atr42
                    5,     // <-- dhc8
                    7,     // <-- b707
                };

            if (script == scriptsNames.Syria)
                return new int?[] {
                    1,     // <-- a320
                    2,     // <-- b747
                    3,     // <-- md11
                    4,     // <-- atr42
                    5,     // <-- dhc8
                    6,     // <-- ssj100
                    10,    // <-- a380
                    11,    // <-- b777
                    12,    // <-- il114
                };

            if (script == scriptsNames.Libya)
                return new int?[] {
                    1,     // <-- a320
                    2,     // <-- b747
                    3,     // <-- md11
                    4,     // <-- atr42
                    5,     // <-- dhc8
                    6,     // <-- ssj100
                    10,    // <-- a380
                    11,    // <-- b777
                    12,    // <-- il114
                };

            if (script == scriptsNames.KoreanBoeing)
                return new int?[] {
                    2,     // <-- b747
                };

            if (script == scriptsNames.Yemen)
                return new int?[] {
                    1,     // <-- a320
                    2,     // <-- b747
                    4,     // <-- atr42
                    5,     // <-- dhc8
                    6,     // <-- ssj100
                    10,    // <-- a380
                    11,    // <-- b777
                    12,    // <-- il114
                };

            if (script == scriptsNames.F117Hunt)
                return new int?[] {
                    1,     // <-- a320
                    2,     // <-- b747
                    3,     // <-- md11
                    4,     // <-- atr42
                    5,     // <-- dhc8
                    11,    // <-- b777
                    12,    // <-- il114
                };

            if (script == scriptsNames.Rust)
                return new int?[] {
                    2,     // <-- b747
                    4,     // <-- atr42
                    5,     // <-- dhc8
                };

            if (script == scriptsNames.Khmeimim)
                return new int?[] {
                    1,     // <-- a320
                    2,     // <-- b747
                    4,     // <-- atr42
                    5,     // <-- dhc8
                    6,     // <-- ssj100
                    9,     // <-- mc21
                    10,    // <-- a380
                    11,    // <-- b777
                    12,    // <-- il114
                };

            return new int?[] { };
        }
    }
}
