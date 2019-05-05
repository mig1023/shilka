﻿using System;
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
        public const double FAST_ROTATION = 0.2;
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
        public const int SNOW_MIN_SIZE = 22;
        public const int SNOW_MAX_SIZE = 45;
        public const int SNOW_DIRECTION_FLY_SPEED = 5;
        public const int SNOW_DIRECTION_CHANGE_CHANCE = 50;
        public const int HEATING_IN_RAIN = -10;
        public const int HEATING_UNDER_SNOW = -25;

        //school
        public const string ENEMY_INFORMATION = "ВРАЖЕСКИЙ САМОЛЁТ\n\nСейчас появится первый вражеский самолёт " +
            "- все самолёты противника выделены на время обучения красным цветом. Вы должны сбить их или хотя бы повредить. " +
            "Шкала подскажет вам насколько повреждён каждый из них.\n\nOK";
        public const string FRIEND_INFORMATION = "ДРУЖЕСТВЕННЫЙ САМОЛЁТ\n\nСейчас появится первый дружественный самолёт " +
            "- все подобные самолёты выделены на время обучения зелёным цветом. Нужно избегать повреждать свои самолёты." +
            " Если вы собьёте один из них, то игра будет провалена. Шкала подскажет вам насколько они повреждены.\n\nOK";
        public const string AIRLINER_INFORMATION = "ПАССАЖИРСКИЙ САМОЛЁТ\n\nСейчас появится первый пассажирский самолёт " +
            "- все они выделяются на время обучения синим цветом. Нужно избегать повреждать пассажирские самолёты." +
            " Если вы собьёте его, то игра будет провалена. Шкала подскажет вам насколько он повреждён.\n\nOK";
        public const string HEATING_INFORMATION = "ПЕРЕГРЕВ СТВОЛОВ\n\nПри стрельбе стволы зенитных пушек разогреваются. " +
            "Если вы продолжите стрелять после того, как температура достигла очень больших значений, то пушки перегреются " +
            "и заклинят. Потребуется некоторое время, чтобы они остыли - только после этого удастся возобновить стрельбу.\n\nOK";

        public const int SCHOOL_CLOUD_AT_THE_START = 5;
        public const int SCHOOL_ENEMY_AT_THE_START = 20;
        public const int SCHOOL_FRIEND_AT_THE_START = 25;
        public const int SCHOOL_AIRLINER_AT_THE_START = 30;
    }
}
