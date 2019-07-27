using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shilka2
{
    class Constants
    {
        // screen
        public const double STAT_TEXT_TOP = 45;
        public const double STAT_TEXT_LEFT = 10;

        // aircrafts
        public const int MAX_FLIGHT_HEIGHT = 75;
        public const int AIRCRAFT_AVERAGE_PRICE = 61;
        public const double ESCAPE_COEFFICIENT = 1.6;
        public const int TANGAGE_DELAY = 12;
        public const int TANGAGE_SPEED = 4;
        public const int TANGAGE_DEAD_SPEED = 10;
        public const int ROTATE_STEP = 25;
        public const double SLOW_ROTATION = 0.07;
        public const double FAST_ROTATION = 0.3;
        public const double ANGLE_OF_ATTACK_CHANGE_SPEED = 0.1;
        public const int CLOUD_SPEED = 5;

        // case
        public const int CASE_LENGTH = 2;
        public const double MIN_FRAGM_SIN = 0.2;
        public const double MAX_FRAGM_SIN = 0.4;
        public const double MIN_FRAGM_COS = 0.4;
        public const double MAX_FRAGM_COS = 0.8;
        public const int MIN_SPEED = 3;
        public const int MAX_SPEED = 12;
        public const int EXTR_HEIGHT_CORRECTION = 21;
        public const double FREE_FALL_SPEED = 0.05;

        // shell
        public const int FIRE_WIDTH_CORRECTION = 140;
        public const int FIRE_HEIGHT_CORRECTION = 30;
        public const int FIRE_HEIGHT_POINT_CORRECTION = 70;
        public const int SHELL_THICKNESS = 1;
        public const int SHELL_LENGTH = 3;
        public const int SHELL_SPEED = 25;
        public const int SHELL_DELAY = 1;
        public const int FRAGMENTATION = 15;
        public const int VOLLEY = 3;
        public const int FLASH_SIZE = 4;
        public const string END_COLOR = "#FF7E1C25";

        // shilka
        public const double LAST_DEGREE_CORRECTION = 10;
        public const int GUNS_LENGTH = 30;
        public const int GUN_NOUNT_LENGTH = 10;
        public const double GUN_THICKNESS = 3;
        public const int GUN_RETURN_LEN = 5;
        public const int GUN_RETURN_TIMEOUT = 3;
        public const int GUN_MIDDLE_TIMEOUT = 2;
        public const int HEATING_COLOR_BASE = 200;
        public const int GUNS_HEAT_UP = 300;
        public const int GUNS_OVERHEATING = 350;

        // statistic
        public const string STATISTIC_FILE_NAME = "shilka2.stat";

        // weather
        public const int WEATHER_CYCLE = 700;
        public const int MAX_RAIN_TYPE = 4;
        public const int MAX_SNOW_TYPE = 4;
        public const int RAIN_MIN_WIDTH = 10;
        public const int RAIN_MAX_WIDTH = 30;
        public const int RAIN_MIN_HEIGHT = 15;
        public const int RAIN_MAX_HEIGHT = 40;
        public const int STORM_FLY_SPEED = 9;
        public const int SNOW_MIN_SIZE = 22;
        public const int SNOW_MAX_SIZE = 45;
        public const int SNOW_DIRECTION_FLY_SPEED = 5;
        public const int SNOW_DIRECTION_CHANGE_CHANCE = 50;
        public const int HEATING_IN_RAIN = -10;
        public const int HEATING_UNDER_SNOW = -25;

        //school
        public const string ENEMY_INFORMATION = "ВРАЖЕСКИЙ САМОЛЁТ\n\nСейчас появится первый вражеский самолёт " +
            "- все самолёты противника выделены на время обучения красным цветом. Вы должны сбить их или хотя бы повредить. " +
            "Шкала подскажет вам насколько повреждён каждый из них. В реальном бою придётся определять самолёт по профилю, " +
            "а степень их повреждения - на глаз и по ощущениям.\n\nOK";
        public const string FRIEND_INFORMATION = "ДРУЖЕСТВЕННЫЙ САМОЛЁТ\n\nСейчас появится первый дружественный самолёт " +
            "- все подобные самолёты выделены на время обучения зелёным цветом. Нужно избегать повреждать свои самолёты. " +
            "Если вы собьёте один из них, то игра будет провалена и сразу закончится. Шкала подскажет вам насколько они повреждены, " +
            "но только во время обучения.\n\nOK";
        public const string AIRLINER_INFORMATION = "ПАССАЖИРСКИЙ САМОЛЁТ\n\nСейчас появится первый пассажирский самолёт " +
            "- все они выделяются на время обучения синим цветом. Нужно избегать повреждать пассажирские самолёты. " +
            "Если вы собьёте его, то игра будет провалена и сразу закончится - точно также как и с дружественными самолётами. " +
            "Шкала подскажет вам насколько он повреждён, но только во время оубчения.\n\nOK";
        public const string HEATING_INFORMATION = "ПЕРЕГРЕВ СТВОЛОВ\n\nПри стрельбе стволы зенитных пушек разогреваются. " +
            "Это влияет на точность стрельбы: чем сильнее разогрет ствол, тем меньше точность. " +
            "Если вы продолжите стрелять после того, как температура достигла очень больших значений, то пушки перегреются " +
            "и заклинят. Потребуется некоторое время, чтобы они остыли - только после этого удастся возобновить стрельбу.\n\nOK";

        public const int SCHOOL_CLOUD_AT_THE_START = 5;
        public const int SCHOOL_ENEMY_AT_THE_START = 20;
        public const int SCHOOL_FRIEND_AT_THE_START = 25;
        public const int SCHOOL_AIRLINER_AT_THE_START = 30;

        // img size data
        public static Dictionary<string, int[]> IMG_SIZE = new Dictionary<string, int[]>()
        {
            { "a1",                 new int[] { 190, 67, 32 } },
            { "a10",                new int[] { 270, 68, 32 } },
            { "a320",               new int[] { 565, 173, 4 } },
            { "a380",               new int[] { 621, 191, 32 } },
            { "a50",                new int[] { 570, 175, 32 } },
            { "a50rls",             new int[] { 106, 17, 32 } },
            { "a50rls_back",        new int[] { 106, 17, 32 } },
            { "a6",                 new int[] { 270, 78, 32 } },
            { "ac130",              new int[] { 400, 154, 32 } },
            { "ah1",                new int[] { 209, 54, 32 } },
            { "ah6",                new int[] { 134, 58, 32 } },
            { "ah64",               new int[] { 209, 63, 32 } },
            { "air_prop",           new int[] { 17, 69, 32 } },
            { "as565",              new int[] { 199, 70, 32 } },
            { "atr42",              new int[] { 320, 101, 32 } },
            { "b1",                 new int[] { 510, 108, 8 } },
            { "b2",                 new int[] { 332, 76, 32 } },
            { "b52",                new int[] { 565, 155, 4 } },
            { "background",         new int[] { 1154, 696, 32 } },
            { "boeing707",          new int[] { 565, 177, 32 } },
            { "boeing737",          new int[] { 565, 184, 32 } },
            { "boeing737aewc",      new int[] { 565, 187, 32 } },
            { "boeing747",          new int[] { 565, 158, 32 } },
            { "boeing777",          new int[] { 585, 140, 32 } },
            { "c17",                new int[] { 545, 183, 32 } },
            { "c5",                 new int[] { 570, 152, 32 } },
            { "case",               new int[] { 2, 2, 32 } },
            { "cessna",             new int[] { 170, 61, 32 } },
            { "ch47",               new int[] { 270, 101, 32 } },
            { "ch54",               new int[] { 310, 87, 32 } },
            { "cloud1",             new int[] { 423, 153, 32 } },
            { "cloud2",             new int[] { 678, 198, 32 } },
            { "cloud3",             new int[] { 525, 213, 32 } },
            { "cloud4",             new int[] { 450, 222, 32 } },
            { "cloud5",             new int[] { 483, 219, 32 } },
            { "cloud6",             new int[] { 486, 174, 32 } },
            { "cloud7",             new int[] { 639, 285, 32 } },
            { "comanche",           new int[] { 210, 61, 32 } },
            { "concorde",           new int[] { 475, 100, 32 } },
            { "crj200",             new int[] { 400, 89, 32 } },
            { "dc8",                new int[] { 580, 118, 32 } },
            { "dhc8",               new int[] { 370, 90, 32 } },
            { "drone",              new int[] { 26, 9, 32 } },
            { "drone2",             new int[] { 30, 17, 32 } },
            { "drone3",             new int[] { 32, 20, 32 } },
            { "drone4",             new int[] { 75, 19, 32 } },
            { "e3",                 new int[] { 581, 164, 32 } },
            { "e3rls",              new int[] { 117, 19, 32 } },
            { "e3rls_back",         new int[] { 117, 19, 32 } },
            { "ea6",                new int[] { 285, 66, 32 } },
            { "emb120",             new int[] { 330, 94, 32 } },
            { "eurofighter",        new int[] { 270, 77, 32 } },
            { "f111",               new int[] { 285, 59, 32 } },
            { "f117",               new int[] { 270, 47, 32 } },
            { "f14",                new int[] { 275, 67, 8 } },
            { "f15",                new int[] { 270, 62, 32 } },
            { "f16",                new int[] { 270, 89, 32 } },
            { "f18",                new int[] { 270, 61, 8 } },
            { "f22",                new int[] { 270, 73, 32 } },
            { "f35",                new int[] { 270, 76, 32 } },
            { "f4",                 new int[] { 270, 64, 32 } },
            { "f5",                 new int[] { 270, 58, 32 } },
            { "f8",                 new int[] { 270, 93, 32 } },
            { "five_suppl",         new int[] { 39, 37, 32 } },
            { "f_suppl",            new int[] { 23, 21, 32 } },
            { "gazelle",            new int[] { 185, 64, 32 } },
            { "globalhawk",         new int[] { 265, 85, 32 } },
            { "gripen",             new int[] { 247, 68, 32 } },
            { "h34",                new int[] { 260, 72, 32 } },
            { "harrier",            new int[] { 275, 81, 32 } },
            { "hawkeye",            new int[] { 324, 96, 32 } },
            { "hawkeyerls",         new int[] { 132, 12, 32 } },
            { "hawkeyerls_back",    new int[] { 132, 12, 32 } },
            { "hunter",             new int[] { 172, 49, 32 } },
            { "il114",              new int[] { 420, 133, 32 } },
            { "i_suppl",            new int[] { 32, 38, 32 } },
            { "jassm",              new int[] { 108, 25, 32 } },
            { "ka26",               new int[] { 150, 56, 32 } },
            { "ka27",               new int[] { 197, 63, 32 } },
            { "ka31",               new int[] { 200, 50, 32 } },
            { "ka31rls",            new int[] { 92, 18, 32 } },
            { "ka31rls_back",       new int[] { 92, 18, 32 } },
            { "ka52",               new int[] { 232, 70, 32 } },
            { "kc135",              new int[] { 500, 157, 32 } },
            { "l1011",              new int[] { 500, 180, 32 } },
            { "l1049",              new int[] { 414, 119, 32 } },
            { "ltl_prop",           new int[] { 10, 44, 32 } },
            { "ltl_suppl",          new int[] { 21, 25, 32 } },
            { "m2000",              new int[] { 270, 79, 32 } },
            { "m2000ed",            new int[] { 270, 75, 32 } },
            { "mangusta",           new int[] { 215, 66, 32 } },
            { "mc21",               new int[] { 560, 154, 32 } },
            { "md11",               new int[] { 560, 157, 32 } },
            { "md90",               new int[] { 580, 111, 32 } },
            { "mh53",               new int[] { 375, 84, 32 } },
            { "mi10",               new int[] { 300, 77, 32 } },
            { "mi24",               new int[] { 210, 57, 32 } },
            { "mi26",               new int[] { 580, 146, 32 } },
            { "mi26_prop",          new int[] { 512, 20, 32 } },
            { "mi26_suppl",         new int[] { 130, 130, 32 } },
            { "mi28",               new int[] { 209, 62, 32 } },
            { "mi8",                new int[] { 220, 62, 32 } },
            { "micro_prop",         new int[] { 13, 4, 32 } },
            { "micro_prop_y",       new int[] { 4, 13, 32 } },
            { "mig19",              new int[] { 270, 81, 32 } },
            { "mig21",              new int[] { 270, 62, 32 } },
            { "mig23",              new int[] { 270, 71, 32 } },
            { "mig25",              new int[] { 270, 64, 32 } },
            { "mig29",              new int[] { 270, 65, 8 } },
            { "mig31",              new int[] { 270, 63, 32 } },
            { "mig35",              new int[] { 270, 72, 32 } },
            { "oh1",                new int[] { 205, 70, 32 } },
            { "oh58d",              new int[] { 209, 83, 32 } },
            { "pakfa",              new int[] { 270, 57, 32 } },
            { "predator",           new int[] { 140, 44, 32 } },
            { "prop_main",          new int[] { 170, 19, 32 } },
            { "prop_main_big",      new int[] { 200, 19, 32 } },
            { "prop_main_ltl",      new int[] { 100, 19, 32 } },
            { "puma",               new int[] { 215, 58, 32 } },
            { "r99",                new int[] { 350, 88, 32 } },
            { "radar",              new int[] { 25, 33, 32 } },
            { "rafale",             new int[] { 270, 86, 32 } },
            { "rain1",              new int[] { 20, 35, 32 } },
            { "rain2",              new int[] { 20, 35, 32 } },
            { "rain3",              new int[] { 20, 38, 32 } },
            { "rain4",              new int[] { 20, 36, 32 } },
            { "rc135",              new int[] { 528, 185, 32 } },
            { "reaper",             new int[] { 161, 52, 32 } },
            { "rooivalk",           new int[] { 265, 74, 32 } },
            { "scalp",              new int[] { 115, 23, 32 } },
            { "shell",              new int[] { 3, 1, 32 } },
            { "shilka",             new int[] { 185, 108, 32 } },
            { "snow1",              new int[] { 45, 46, 32 } },
            { "snow2",              new int[] { 45, 41, 32 } },
            { "snow3",              new int[] { 45, 41, 32 } },
            { "snow4",              new int[] { 45, 45, 32 } },
            { "sr71",               new int[] { 450, 71, 32 } },
            { "ssj100",             new int[] { 355, 124, 32 } },
            { "su17",               new int[] { 270, 61, 32 } },
            { "su24",               new int[] { 270, 67, 32 } },
            { "su25",               new int[] { 270, 81, 32 } },
            { "su27",               new int[] { 270, 77, 32 } },
            { "su30",               new int[] { 270, 66, 32 } },
            { "su34",               new int[] { 275, 56, 32 } },
            { "thunder1",           new int[] { 256, 421, 32 } },
            { "thunder2",           new int[] { 110, 419, 32 } },
            { "thunder3",           new int[] { 256, 419, 32 } },
            { "thunder4",           new int[] { 110, 420, 32 } },
            { "thunder5",           new int[] { 516, 415, 32 } },
            { "thunder6",           new int[] { 516, 415, 32 } },
            { "tiger-hap",          new int[] { 222, 76, 32 } },
            { "tiger",              new int[] { 209, 76, 32 } },
            { "tomahawk",           new int[] { 125, 29, 32 } },
            { "tornado",            new int[] { 270, 72, 32 } },
            { "tu134",              new int[] { 463, 108, 32 } },
            { "tu154",              new int[] { 509, 111, 32 } },
            { "tu160",              new int[] { 510, 108, 32 } },
            { "tu22m3",             new int[] { 434, 108, 4 } },
            { "tu95",               new int[] { 510, 116, 32 } },
            { "t_suppl",            new int[] { 39, 37, 32 } },
            { "u2",                 new int[] { 355, 103, 32 } },
            { "uh1",                new int[] { 210, 65, 32 } },
            { "uh60",               new int[] { 215, 59, 32 } },
            { "v22",                new int[] { 282, 103, 32 } },
            { "x_suppl",            new int[] { 38, 29, 32 } },
            { "y_suppl",            new int[] { 32, 36, 32 } },


        };
    }
}
