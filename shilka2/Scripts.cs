﻿using System.Collections.Generic;

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
            Khmeimim,
            Belgrad
        };

        public static Dictionary<string, string> scriptsRuNames = new Dictionary<string, string>
        {
            ["noScript"] = "быстрая игра",
            ["Vietnam"] = "вьетнамская война",
            ["KoreanBoeing"] = "корейский боинг",
            ["DesertStorm"] = "буря в пустыни",
            ["Yugoslavia"] = "бомбардировки югославии",
            ["IranIraq"] = "ирано-иракская война",
            ["Syria"] = "гражданская война в сирии",
            ["Libya"] = "интервенция в ливии",
            ["Yemen"] = "интервенция в йемен",
            ["Rust"] = "полёт матиаса руста",
            ["F117Hunt"] = "охота на невидимку",
            ["Khmeimim"] = "оборона хмеймима",
            ["Belgrad"] = "налёт на белград",
        };

        public static int?[] scriptAircraft;
        public static int?[] scriptHelicopters;
        public static int?[] scriptAircraftFriend;
        public static int?[] scriptHelicoptersFriend;
        public static int?[] scriptAirliners;

        public static Weather.weatherTypes ScriptsWeather(scriptsNames script, Weather.weatherTypes weather)
        {
            if (script == scriptsNames.KoreanBoeing)
                return Weather.weatherTypes.snow;

            if (weather == Weather.weatherTypes.good)
                return weather;

            Weather.weatherTypes weatherRainOrStorm = Weather.weatherTypes.rain;
            if (FlyObject.rand.Next(2) == 0)
                weatherRainOrStorm = Weather.weatherTypes.storm;

            if ((script != Scripts.scriptsNames.DesertStorm) && (weather == Weather.weatherTypes.sand))
                return Weather.weatherTypes.good;

            switch (script)
            {
                case scriptsNames.Vietnam:
                    return weatherRainOrStorm;
                case scriptsNames.IranIraq:
                    return Weather.weatherTypes.good;
                case scriptsNames.DesertStorm:
                    return Weather.weatherTypes.sand;
                case scriptsNames.Syria:
                    return Weather.weatherTypes.good;
                case scriptsNames.Yugoslavia:
                    return weather;
                case scriptsNames.Libya:
                    return Weather.weatherTypes.good;
                case scriptsNames.Yemen:
                    return weatherRainOrStorm;
                case scriptsNames.Rust:
                    return weather;
                case scriptsNames.F117Hunt:
                    return weather;
                case scriptsNames.Khmeimim:
                    return Weather.weatherTypes.good;
                case scriptsNames.Belgrad:
                    return weather;
            };

            return weather;
        }

        public static string ScriptFlagName(scriptsNames script)
        {
            switch (script)
            {
                case scriptsNames.Vietnam:
                    return "vn";
                case scriptsNames.IranIraq:
                    return "ir";
                case scriptsNames.DesertStorm:
                    return "iq";
                case scriptsNames.Syria:
                    return "sy";
                case scriptsNames.Yugoslavia:
                    return "yu";
                case scriptsNames.KoreanBoeing:
                    return "ki";
                case scriptsNames.Libya:
                    return "ly";
                case scriptsNames.Yemen:
                    return "ye";
                case scriptsNames.Rust:
                    return "su";
                case scriptsNames.F117Hunt:
                    return "yu";
                case scriptsNames.Khmeimim:
                    return "sy";
                case scriptsNames.Belgrad:
                    return "yu";
            };

            return null;
        }

        public static int ScriptTemperature(scriptsNames script)
        {
            switch (script)
            {
                case scriptsNames.Vietnam:
                    return 35;
                case scriptsNames.IranIraq:
                    return 40;
                case scriptsNames.DesertStorm:
                    return 40;
                case scriptsNames.Syria:
                    return 40;
                case scriptsNames.Yugoslavia:
                    return 25;
                case scriptsNames.KoreanBoeing:
                    return -5;
                case scriptsNames.Libya:
                    return 40;
                case scriptsNames.Yemen:
                    return 20;
                case scriptsNames.Rust:
                    return 30;
                case scriptsNames.F117Hunt:
                    return 20;
                case scriptsNames.Khmeimim:
                    return 40;
                case scriptsNames.Belgrad:
                    return 25;
            };

            return 30;
        }

        public static int?[] ScriptFriendAircrafts(scriptsNames script)
        {

            if (script == scriptsNames.Vietnam)
                return new int?[] {
                    3,      // <-- su17
                    10,     // <-- mig19
                    11,     // <-- mig21
                };

            if (script == scriptsNames.Yugoslavia)
                return new int?[] {
                    0,     // <-- mig23
                    1,     // <-- mig29
                    11,    // <-- mig21
                    12,    // <-- mig25
                };

            if (script == scriptsNames.DesertStorm)
                return new int?[] {
                    0,     // <-- mig23
                    1,     // <-- mig29
                    5,     // <-- su25
                    12,    // <-- mig25
                };

            if (script == scriptsNames.IranIraq)
                return new int?[] {
                    0,     // <-- mig23
                    3,     // <-- su17
                    10,    // <-- mig19
                    12,    // <-- mig25
                    18,    // <-- tu16
                    19,    // <-- tu22
                };

            if (script == scriptsNames.Syria)
                return new int?[] {
                    0,     // <-- mig23
                    1,     // <-- mig29
                    3,     // <-- su17
                    13,    // <-- a50
                    15,    // <-- tu95
                };

            if (script == scriptsNames.Libya)
                return new int?[] {
                    0,     // <-- mig23
                    3,     // <-- su17
                    4,     // <-- su24
                    12,    // <-- mig25
                };

            if (script == scriptsNames.KoreanBoeing)
                return new int?[] { };

            if (script == scriptsNames.Yemen)
                return new int?[] { };

            if (script == scriptsNames.F117Hunt)
                return new int?[] { };

            if (script == scriptsNames.Rust)
                return new int?[] { };

            if (script == scriptsNames.Khmeimim)
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

            if (script == scriptsNames.Belgrad)
                return new int?[] {
                    0,     // <-- mig23
                    1,     // <-- mig29
                    11,    // <-- mig21
                    12,    // <-- mig25
                };

            return null;
        }

        public static int?[] ScriptFriendHelicopters(scriptsNames script)
        {

            if (script == scriptsNames.Vietnam)
                return new int?[] { };

            if (script == scriptsNames.Yugoslavia)
                return new int?[] { };

            if (script == scriptsNames.DesertStorm)
                return new int?[] {
                    1,     // <-- mi24
                    2,     // <-- mi8
                };

            if (script == scriptsNames.IranIraq)
                return new int?[] {
                    1,     // <-- mi24
                    2,     // <-- mi8
                };

            if (script == scriptsNames.Syria)
                return new int?[] {
                    0,     // <-- mi28
                    1,     // <-- mi24
                    2,     // <-- mi8
                    3,     // <-- ka52
                    4,     // <-- ka27
                    7,     // <-- ka31
                };

            if (script == scriptsNames.Libya)
                return new int?[] {
                    1,     // <-- mi24
                    2,     // <-- mi8
                };

            if (script == scriptsNames.KoreanBoeing)
                return new int?[] { };

            if (script == scriptsNames.Yemen)
                return new int?[] { };

            if (script == scriptsNames.F117Hunt)
                return new int?[] { };

            if (script == scriptsNames.Rust)
                return new int?[] { };

            if (script == scriptsNames.Khmeimim)
                return new int?[] {
                    0,     // <-- mi28
                    1,     // <-- mi24
                    2,     // <-- mi8
                    3,     // <-- ka52
                    7,     // <-- ka31
                };

            if (script == scriptsNames.Belgrad)
                return new int?[] { };

            return null;
        }

        public static int?[] ScriptEnemyAircrafts(scriptsNames script)
        {

            if (script == scriptsNames.Vietnam)
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

            if (script == scriptsNames.Yugoslavia)
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

            if (script == scriptsNames.DesertStorm)
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

            if (script == scriptsNames.IranIraq)
                return new int?[] {
                    4,      // <-- f14
                    9,      // <-- f4
                    21,     // <-- ac130
                    24,     // <-- f5
                };

            if (script == scriptsNames.Syria)
                return new int?[] {
                    11,     // <-- predator
                    12,     // <-- reaper
                    19,     // <-- tomahawk
                    25,     // <-- scalp
                    33,     // <-- hunter
                    37,     // <-- jassm
                };

            if (script == scriptsNames.KoreanBoeing)
                return new int?[] {
                    28,     // <-- rc135
                    29,     // <-- u2
                    30,     // <-- sr71
                };

            if (script == scriptsNames.Libya)
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

            if (script == scriptsNames.Yemen)
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

            if (script == scriptsNames.F117Hunt)
                return new int?[] { };

            if (script == scriptsNames.Rust)
                return new int?[] {
                    32,     // <-- cessna
                    1,      // <-- replace to airliner
                    2,      // <-- replace to airliner
                    3,      // <-- replace to airliner
                    4,      // <-- replace to airliner
                    5,      // <-- replace to airliner
                    6,      // <-- replace to airliner
                };

            if (script == scriptsNames.Khmeimim)
                return new int?[] { };

            if (script == scriptsNames.Belgrad)
                return new int?[] {
                    1,      // <-- b1
                    2,      // <-- b52
                    3,      // <-- f117
                    19,     // <-- tomahawk
                    41,     // <-- globemaster
                    43,     // <-- kc135
                };

            return null;
        }

        public static int?[] ScriptEnemyHelicopters(scriptsNames script)
        {

            if (script == scriptsNames.Vietnam)
                return new int?[] {
                    1,      // <-- ah1
                    3,      // <-- uh1
                    4,      // <-- ch46
                    13,     // <-- mh53
                    16,     // <-- oh58
                    22,     // <-- h34
                    23,     // <-- ch54
                };

            if (script == scriptsNames.Yugoslavia)
                return new int?[] {
                    0,      // <-- ah64
                    1,      // <-- ah1
                    2,      // <-- uh60
                    3,      // <-- uh1
                    4,      // <-- ch46
                    12,     // <-- puma
                    14,     // <-- as565
                };

            if (script == scriptsNames.DesertStorm)
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

            if (script == scriptsNames.IranIraq)
                return new int?[] {
                    1,      // <-- ah1
                    4,      // <-- ch46
                    16,     // <-- oh58
                };

            if (script == scriptsNames.Syria)
                return new int?[] { };

            if (script == scriptsNames.Libya)
                return new int?[] {
                    0,      // <-- ah64
                    2,      // <-- uh60
                    4,      // <-- ch46
                    14,     // <-- as565
                    16,     // <-- oh58
                    29,     // <-- raven
                };

            if (script == scriptsNames.KoreanBoeing)
                return new int?[] { };

            if (script == scriptsNames.Yemen)
                return new int?[] {
                    0,      // <-- ah64
                    1,      // <-- ah1
                    2,      // <-- uh60
                    4,      // <-- ch46
                    12,     // <-- puma
                    14,     // <-- as565
                    29,     // <-- raven
                };

            if (script == scriptsNames.F117Hunt)
                return new int?[] { };

            if (script == scriptsNames.Rust)
                return new int?[] { };

            if (script == scriptsNames.Khmeimim)
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
                    29,     // <-- raven
                };

            if (script == scriptsNames.Belgrad)
                return new int?[] { };

            return null;
        }

        public static int?[] ScriptAirliners(scriptsNames script)
        {

            if (script == scriptsNames.Vietnam)
                return new int?[] { };

            if (script == scriptsNames.Yugoslavia)
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

            if (script == scriptsNames.DesertStorm)
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

            if (script == scriptsNames.IranIraq)
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

            if (script == scriptsNames.Syria)
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

            if (script == scriptsNames.Libya)
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

            if (script == scriptsNames.KoreanBoeing)
                return new int?[] {
                    1,      // <-- b747
                };

            if (script == scriptsNames.Yemen)
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

            if (script == scriptsNames.F117Hunt)
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

            if (script == scriptsNames.Rust)
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

            if (script == scriptsNames.Khmeimim)
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

            if (script == scriptsNames.Belgrad)
                return new int?[] { };

            return null;
        }
    }
}
