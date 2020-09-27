using System.Collections.Generic;

namespace shilka2
{
    public class Scripts
    {
        public enum ScriptsNames {
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
            Khmeimim,
            Belgrad,
            Turkey
        };

        public static Dictionary<string, string> ruNames = new Dictionary<string, string>
        {
            ["noScript"] = "быстрая игра",
            ["Vietnam"] = "вьетнамская война",
            ["KoreanBoeing"] = "корейский боинг",
            ["DesertStorm"] = "буря в пустыни",
            ["Yugoslavia"] = "бомбардировки югославии",
            ["IranIraq"] = "ирано-иракская война",
            ["Syria"] = "гражданская война в сирии",
            ["Libya"] = "интервенция в ливии",
            ["Yemen"] = "война в йемене",
            ["Rust"] = "полёт матиаса руста",
            ["F117Hunt"] = "охота на невидимку",
            ["Khmeimim"] = "оборона хмеймима",
            ["Belgrad"] = "налёт на белград",
            ["Turkey"] = "турецкое вторжение",
        };

        public static Dictionary<ScriptsNames, string> imagesNames = new Dictionary<ScriptsNames, string>
        {
            [ScriptsNames.Rust] = "kremlin",
            [ScriptsNames.Libya] = "sahara",
            [ScriptsNames.Yemen] = "yemen_panorama"
        };

        public static int?[] scriptAircraft;
        public static int?[] scriptHelicopters;
        public static int?[] scriptAircraftFriend;
        public static int?[] scriptHelicoptersFriend;
        public static int?[] scriptAirliners;

        public static Weather.WeatherTypes SuitableWeather(ScriptsNames script, Weather.WeatherTypes weather)
        {
            if (script == ScriptsNames.KoreanBoeing)
                return Weather.WeatherTypes.snow;

            if (weather == Weather.WeatherTypes.good)
                return weather;

            Weather.WeatherTypes weatherRainOrStorm = Weather.WeatherTypes.rain;
            if (FlyObject.rand.Next(2) == 0)
                weatherRainOrStorm = Weather.WeatherTypes.storm;

            if ((script != Scripts.ScriptsNames.DesertStorm) && (weather == Weather.WeatherTypes.sand))
                return Weather.WeatherTypes.good;

            switch (script)
            {
                case ScriptsNames.Vietnam:
                    return weatherRainOrStorm;
                case ScriptsNames.IranIraq:
                    return Weather.WeatherTypes.good;
                case ScriptsNames.DesertStorm:
                    return Weather.WeatherTypes.sand;
                case ScriptsNames.Syria:
                    return Weather.WeatherTypes.good;
                case ScriptsNames.Yugoslavia:
                    return weather;
                case ScriptsNames.Libya:
                    return Weather.WeatherTypes.good;
                case ScriptsNames.Yemen:
                    return weatherRainOrStorm;
                case ScriptsNames.Rust:
                    return weather;
                case ScriptsNames.F117Hunt:
                    return weather;
                case ScriptsNames.Khmeimim:
                    return Weather.WeatherTypes.good;
                case ScriptsNames.Belgrad:
                    return weather;
                case ScriptsNames.Turkey:
                    return Weather.WeatherTypes.good;
            };

            return weather;
        }

        public static string FlagName(ScriptsNames script)
        {
            switch (script)
            {
                case ScriptsNames.Vietnam:
                    return "vn";
                case ScriptsNames.IranIraq:
                    return "ir";
                case ScriptsNames.DesertStorm:
                    return "iq";
                case ScriptsNames.Syria:
                    return "sy";
                case ScriptsNames.Yugoslavia:
                    return "yu";
                case ScriptsNames.KoreanBoeing:
                    return "ki";
                case ScriptsNames.Libya:
                    return "ly";
                case ScriptsNames.Yemen:
                    return "ye";
                case ScriptsNames.Rust:
                    return "su";
                case ScriptsNames.F117Hunt:
                    return "yu";
                case ScriptsNames.Khmeimim:
                    return "sy";
                case ScriptsNames.Belgrad:
                    return "yu";
                case ScriptsNames.Turkey:
                    return "sy";
            };

            return null;
        }

        public static int Temperature(ScriptsNames script)
        {
            switch (script)
            {
                case ScriptsNames.Vietnam:
                    return 35;
                case ScriptsNames.IranIraq:
                    return 40;
                case ScriptsNames.DesertStorm:
                    return 40;
                case ScriptsNames.Syria:
                    return 40;
                case ScriptsNames.Yugoslavia:
                    return 25;
                case ScriptsNames.KoreanBoeing:
                    return -5;
                case ScriptsNames.Libya:
                    return 40;
                case ScriptsNames.Yemen:
                    return 20;
                case ScriptsNames.Rust:
                    return 30;
                case ScriptsNames.F117Hunt:
                    return 20;
                case ScriptsNames.Khmeimim:
                    return 40;
                case ScriptsNames.Belgrad:
                    return 25;
                case ScriptsNames.Turkey:
                    return 40;
            };

            return 30;
        }

        public static int?[] FriendAircrafts(ScriptsNames script)
        {

            if (script == ScriptsNames.Vietnam)
                return new int?[] {
                    3,      // <-- su17
                    10,     // <-- mig19
                    11,     // <-- mig21
                };

            if (script == ScriptsNames.Yugoslavia)
                return new int?[] {
                    0,     // <-- mig23
                    1,     // <-- mig29
                    11,    // <-- mig21
                    12,    // <-- mig25
                };

            if (script == ScriptsNames.DesertStorm)
                return new int?[] {
                    0,     // <-- mig23
                    1,     // <-- mig29
                    5,     // <-- su25
                    12,    // <-- mig25
                };

            if (script == ScriptsNames.IranIraq)
                return new int?[] {
                    0,     // <-- mig23
                    3,     // <-- su17
                    10,    // <-- mig19
                    12,    // <-- mig25
                    18,    // <-- tu16
                    19,    // <-- tu22
                };

            if (script == ScriptsNames.Syria)
                return new int?[] {
                    0,     // <-- mig23
                    1,     // <-- mig29
                    3,     // <-- su17
                    13,    // <-- a50
                    15,    // <-- tu95
                };

            if (script == ScriptsNames.Libya)
                return new int?[] {
                    0,     // <-- mig23
                    3,     // <-- su17
                    4,     // <-- su24
                    12,    // <-- mig25
                };

            if (script == ScriptsNames.KoreanBoeing)
                return new int?[] { };

            if (script == ScriptsNames.Yemen)
                return new int?[] { };

            if (script == ScriptsNames.F117Hunt)
                return new int?[] { };

            if (script == ScriptsNames.Rust)
                return new int?[] { };

            if (script == ScriptsNames.Khmeimim)
                return new int?[] {
                    1,     // <-- mig29
                    2,     // <-- mig31
                    4,     // <-- su24
                    5,     // <-- su25
                    6,     // <-- su27
                    7,     // <-- su34
                    8,     // <-- pakfa
                    9,    // <-- tu160
                    13,    // <-- a50
                    14,    // <-- tu95
                    16,    // <-- su30
                    17,    // <-- tu22m3
                };

            if (script == ScriptsNames.Belgrad)
                return new int?[] {
                    0,     // <-- mig23
                    1,     // <-- mig29
                    11,    // <-- mig21
                    12,    // <-- mig25
                };

            if (script == ScriptsNames.Turkey)
                return new int?[] {
                    0,     // <-- mig23
                    1,     // <-- mig29
                    3,     // <-- su17
                    4,     // <-- su24
                    5,     // <-- su25
                    7,     // <-- su34
                    13,    // <-- a50
                    15,    // <-- tu95
                    16,    // <-- su30
                };

            return null;
        }

        public static int?[] FriendHelicopters(ScriptsNames script)
        {

            if (script == ScriptsNames.Vietnam)
                return new int?[] { };

            if (script == ScriptsNames.Yugoslavia)
                return new int?[] { };

            if (script == ScriptsNames.DesertStorm)
                return new int?[] {
                    1,     // <-- mi24
                    2,     // <-- mi8
                };

            if (script == ScriptsNames.IranIraq)
                return new int?[] {
                    1,     // <-- mi24
                    2,     // <-- mi8
                };

            if (script == ScriptsNames.Syria)
                return new int?[] {
                    0,     // <-- mi28
                    1,     // <-- mi24
                    2,     // <-- mi8
                    3,     // <-- ka52
                    4,     // <-- ka27
                    7,     // <-- ka31
                };

            if (script == ScriptsNames.Libya)
                return new int?[] {
                    1,     // <-- mi24
                    2,     // <-- mi8
                };

            if (script == ScriptsNames.KoreanBoeing)
                return new int?[] { };

            if (script == ScriptsNames.Yemen)
                return new int?[] { };

            if (script == ScriptsNames.F117Hunt)
                return new int?[] { };

            if (script == ScriptsNames.Rust)
                return new int?[] { };

            if (script == ScriptsNames.Khmeimim)
                return new int?[] {
                    0,     // <-- mi28
                    1,     // <-- mi24
                    2,     // <-- mi8
                    3,     // <-- ka52
                    7,     // <-- ka31
                };

            if (script == ScriptsNames.Belgrad)
                return new int?[] { };

            if (script == ScriptsNames.Turkey)
                return new int?[] {
                    0,     // <-- mi28
                    1,     // <-- mi24
                    2,     // <-- mi8
                    3,     // <-- ka52
                };

            return null;
        }

        public static int?[] EnemyAircrafts(ScriptsNames script)
        {

            if (script == ScriptsNames.Vietnam)
                return new int?[] {
                    2,      // <-- b52
                    4,      // <-- f14
                    9,      // <-- f4
                    20,     // <-- f8
                    21,     // <-- ac130
                    22,     // <-- a6
                    23,     // <-- f111
                    24,     // <-- f5
                    27,     // <-- hawkeye
                    42,     // <-- a1
                    43,     // <-- kc135
                    46,     // <-- bronco
                    48,     // <-- f104
                    51,     // <-- a26
                };

            if (script == ScriptsNames.Yugoslavia)
                return new int?[] {
                    0,      // <-- a10
                    1,      // <-- b1
                    2,      // <-- b52
                    3,      // <-- f117
                    4,      // <-- f14
                    5,      // <-- f18
                    6,      // <-- f16
                    8,      // <-- f15
                    9,      // <-- f4
                    10,     // <-- tornado
                    11,     // <-- predator
                    14,     // <-- e3
                    17,     // <-- b2
                    19,     // <-- tomahawk
                    31,     // <-- harrier
                    33,     // <-- hunter
                    40,     // <-- galaxy
                    41,     // <-- globemaster
                    43,     // <-- kc135
                    44,     // <-- jaguar
                    48,     // <-- f104
                    49,     // <-- harrier gr3
                    50,     // <-- harrier mk1
                };

            if (script == ScriptsNames.DesertStorm)
                return new int?[] {
                    0,      // <-- a10
                    1,      // <-- b1
                    2,      // <-- b52
                    3,      // <-- f117
                    4,      // <-- f14
                    5,      // <-- f18
                    6,      // <-- f16
                    8,      // <-- f15
                    9,      // <-- f4
                    10,     // <-- tornado
                    14,     // <-- e3
                    17,     // <-- b2
                    19,     // <-- tomahawk
                    21,     // <-- ac130
                    22,     // <-- a6
                    23,     // <-- f111
                    24,     // <-- f5
                    26,     // <-- ea6
                    27,     // <-- hawkeye
                    30,     // <-- sr71
                    31,     // <-- harrier
                    35,     // <-- m2000
                    36,     // <-- m2000ed
                    40,     // <-- galaxy
                    43,     // <-- kc135
                    44,     // <-- jaguar
                    46,     // <-- bronco
                    49,     // <-- harrier gr3
                    50,     // <-- harrier mk1
                };

            if (script == ScriptsNames.IranIraq)
                return new int?[] {
                    4,      // <-- f14
                    9,      // <-- f4
                    21,     // <-- ac130
                    24,     // <-- f5
                };

            if (script == ScriptsNames.Syria)
                return new int?[] {
                    11,     // <-- predator
                    12,     // <-- reaper
                    19,     // <-- tomahawk
                    25,     // <-- scalp
                    33,     // <-- hunter
                    37,     // <-- jassm
                };

            if (script == ScriptsNames.KoreanBoeing)
                return new int?[] {
                    28,     // <-- rc135
                    29,     // <-- u2
                    30,     // <-- sr71
                };

            if (script == ScriptsNames.Libya)
                return new int?[] {
                    0,      // <-- a10
                    2,      // <-- b52
                    5,      // <-- f18
                    6,      // <-- f16
                    8,      // <-- f15
                    10,     // <-- tornado
                    14,     // <-- e3
                    19,     // <-- tomahawk
                    22,     // <-- a6
                    26,     // <-- ea6
                    31,     // <-- harrier
                    33,     // <-- hunter
                    34,     // <-- r99
                    35,     // <-- m2000
                    36,     // <-- m2000ed
                    40,     // <-- galaxy
                    41,     // <-- globemaster
                    43,     // <-- kc135
                    49,     // <-- harrier gr3
                    50,     // <-- harrier mk1
                };

            if (script == ScriptsNames.Yemen)
                return new int?[] {
                    6,      // <-- f16
                    8,      // <-- f15
                    10,     // <-- tornado
                    14,     // <-- e3
                    15,     // <-- eurofighter
                    21,     // <-- ac130
                    24,     // <-- f5
                    35,     // <-- m2000
                    36,     // <-- m2000ed
                    43,     // <-- kc135
                };

            if (script == ScriptsNames.F117Hunt)
                return new int?[] { };

            if (script == ScriptsNames.Rust)
                return new int?[] {
                    32,     // <-- cessna
                    1,      // \
                    2,      //  \
                    3,      //   \ ___ replace to airliner
                    4,      //   /
                    5,      //  /
                    6,      // /
                };

            if (script == ScriptsNames.Khmeimim)
                return new int?[] { };

            if (script == ScriptsNames.Belgrad)
                return new int?[] {
                    1,      // <-- b1
                    2,      // <-- b52
                    6,      // <-- f16
                    8,      // <-- f15
                    3,      // <-- f117
                    10,     // <-- tornado
                    19,     // <-- tomahawk
                };

            if (script == ScriptsNames.Turkey)
                return new int?[] {
                    52,      // <-- anka
                    53,      // <-- bayraktar
                };

            return null;
        }

        public static int?[] EnemyHelicopters(ScriptsNames script)
        {

            if (script == ScriptsNames.Vietnam)
                return new int?[] {
                    1,      // <-- ah1
                    3,      // <-- uh1
                    4,      // <-- ch46
                    13,     // <-- mh53
                    16,     // <-- oh58
                    22,     // <-- h34
                    23,     // <-- ch54
                };

            if (script == ScriptsNames.Yugoslavia)
                return new int?[] {
                    0,      // <-- ah64
                    1,      // <-- ah1
                    2,      // <-- uh60
                    3,      // <-- uh1
                    4,      // <-- ch46
                    12,     // <-- puma
                    14,     // <-- as565
                };

            if (script == ScriptsNames.DesertStorm)
                return new int?[] {
                    0,      // <-- ah64
                    1,      // <-- ah1
                    2,      // <-- uh60
                    3,      // <-- uh1
                    4,      // <-- ch46
                    12,     // <-- puma
                    14,     // <-- as565
                    16,     // <-- oh58
                };

            if (script == ScriptsNames.IranIraq)
                return new int?[] {
                    1,      // <-- ah1
                    4,      // <-- ch46
                    16,     // <-- oh58
                };

            if (script == ScriptsNames.Syria)
                return new int?[] { };

            if (script == ScriptsNames.Libya)
                return new int?[] {
                    0,      // <-- ah64
                    2,      // <-- uh60
                    4,      // <-- ch46
                    14,     // <-- as565
                    16,     // <-- oh58
                    29,     // <-- raven
                    30,     // <-- lynx
                };

            if (script == ScriptsNames.KoreanBoeing)
                return new int?[] { };

            if (script == ScriptsNames.Yemen)
                return new int?[] {
                    0,      // <-- ah64
                    1,      // <-- ah1
                    2,      // <-- uh60
                    4,      // <-- ch46
                    12,     // <-- puma
                    14,     // <-- as565
                    29,     // <-- raven
                };

            if (script == ScriptsNames.F117Hunt)
                return new int?[] { };

            if (script == ScriptsNames.Rust)
                return new int?[] { };

            if (script == ScriptsNames.Khmeimim)
                return new int?[] {
                    7,      // <-- drone
                    15,     // <-- drone2
                    19,     // <-- drone3
                    20,     // <-- drone4
                    24,     // <-- drone5
                    25,     // <-- drone6
                    26,     // <-- drone7
                    27,     // <-- drone8
                    28,     // <-- drone9
                };

            if (script == ScriptsNames.Belgrad)
                return new int?[] { };

            if (script == ScriptsNames.Turkey)
                return new int?[] { };

            return null;
        }

        public static int?[] Airliners(ScriptsNames script)
        {

            if (script == ScriptsNames.Vietnam)
                return new int?[] { };

            if (script == ScriptsNames.Yugoslavia)
                return new int?[] {
                    0,      // <-- a320
                    1,      // <-- b747
                    2,      // <-- md11
                    3,      // <-- atr42
                    4,      // <-- dhc8
                    10,     // <-- b777
                    11,     // <-- il114
                    12,     // <-- b737
                    13,     // <-- md90
                    14,     // <-- l1011
                    16,     // <-- crj200
                    17,     // <-- emb120
                    18,     // <-- concorde
                    19,     // <-- tu134
                    20,     // <-- tu154
                };

            if (script == ScriptsNames.DesertStorm)
                return new int?[] {
                    0,      // <-- a320
                    1,      // <-- b747
                    2,      // <-- md11
                    3,      // <-- atr42
                    4,      // <-- dhc8
                    12,     // <-- b737
                    14,     // <-- dc8
                    15,     // <-- l1011
                    17,     // <-- emb120
                    18,     // <-- concorde
                    19,     // <-- tu134
                    20,     // <-- tu154
                };

            if (script == ScriptsNames.IranIraq)
                return new int?[] {
                    1,      // <-- b747
                    3,      // <-- atr42
                    4,      // <-- dhc8
                    6,      // <-- b707
                    12,     // <-- b737
                    14,     // <-- dc8
                    15,     // <-- l1011
                    18,     // <-- concorde
                    19,     // <-- tu134
                    20,     // <-- tu154
                };

            if (script == ScriptsNames.Syria)
                return new int?[] {
                    0,      // <-- a320
                    1,      // <-- b747
                    2,      // <-- md11
                    3,      // <-- atr42
                    4,      // <-- dhc8
                    5,      // <-- ssj100
                    9,      // <-- a380
                    10,     // <-- b777
                    11,     // <-- il114
                    12,     // <-- b737
                    13,     // <-- md90
                    16,     // <-- crj200
                    17,     // <-- emb120
                    20,     // <-- tu154
                };

            if (script == ScriptsNames.Libya)
                return new int?[] {
                    0,      // <-- a320
                    1,      // <-- b747
                    2,      // <-- md11
                    3,      // <-- atr42
                    4,      // <-- dhc8
                    5,      // <-- ssj100
                    9,      // <-- a380
                    10,     // <-- b777
                    11,     // <-- il114
                    12,     // <-- b737
                    13,     // <-- md90
                    16,     // <-- crj200
                    17,     // <-- emb120
                    20,     // <-- tu154
                };

            if (script == ScriptsNames.KoreanBoeing)
                return new int?[] {
                    1,      // <-- b747
                };

            if (script == ScriptsNames.Yemen)
                return new int?[] {
                    0,      // <-- a320
                    1,      // <-- b747
                    3,      // <-- atr42
                    4,      // <-- dhc8
                    5,      // <-- ssj100
                    9,      // <-- a380
                    10,     // <-- b777
                    11,     // <-- il114
                    12,     // <-- b737
                    13,     // <-- md90
                    16,     // <-- crj200
                    17,     // <-- emb120
                    20,     // <-- tu154
                };

            if (script == ScriptsNames.F117Hunt)
                return new int?[] {
                    0,      // <-- a320
                    1,      // <-- b747
                    2,      // <-- md11
                    3,      // <-- atr42
                    4,      // <-- dhc8
                    10,     // <-- b777
                    11,     // <-- il114
                    12,     // <-- b737
                    13,     // <-- md90
                    15,     // <-- l1011
                    16,     // <-- crj200
                    17,     // <-- emb120
                    18,     // <-- concorde
                    19,     // <-- tu134
                    20,     // <-- tu154
                };

            if (script == ScriptsNames.Rust)
                return new int?[] {
                    0,      // <-- b747
                    3,      // <-- atr42
                    4,      // <-- dhc8
                    12,     // <-- b737
                    14,     // <-- dc8
                    15,     // <-- l1011
                    18,     // <-- concorde
                    19,     // <-- tu134
                    20,     // <-- tu154
                };

            if (script == ScriptsNames.Khmeimim)
                return new int?[] {
                    0,      // <-- a320
                    1,      // <-- b747
                    3,      // <-- atr42
                    4,      // <-- dhc8
                    5,      // <-- ssj100
                    8,      // <-- mc21
                    9,      // <-- a380
                    10,     // <-- b777
                    11,     // <-- il114
                    12,     // <-- b737
                    13,     // <-- md90
                    16,     // <-- crj200
                    17,     // <-- emb120
                    20,     // <-- tu154
                };

            if (script == ScriptsNames.Belgrad)
                return new int?[] { };

            if (script == ScriptsNames.Turkey)
                return new int?[] {
                    0,      // <-- a320
                    5,      // <-- ssj100
                    10,     // <-- b777
                    12,     // <-- b737
                    20,     // <-- tu154
                };

            return null;
        }
    }
}
