using System.Collections.Generic;

namespace shilka2
{
    class Constants
    {
        // screen
        public const double STAT_TEXT_TOP = 45;
        public const double STAT_TEXT_LEFT = 10;
        public const string END_COLOR = "#FF7E1C25";

        // aircrafts
        public const int STANDART_SPEED = 10;
        public const int MAX_FLIGHT_HEIGHT = 75;
        public const double ESCAPE_COEFFICIENT = 1.6;
        public const int TANGAGE_DELAY = 12;
        public const int TANGAGE_SPEED = 4;
        public const int TANGAGE_DEAD_SPEED = 15;
        public const int ROTATE_STEP = 25;
        public const double SLOW_ROTATION = 0.07;
        public const double FAST_ROTATION = 0.3;
        public const double ROTATION_REVERT = 0.01;
        public const double ANGLE_OF_ATTACK_CHANGE_HEAVY = 0.1;
        public const double ANGLE_OF_ATTACK_CHANGE_MIDDLE = 1;
        public const double ANGLE_OF_ATTACK_CHANGE_LIGHT = 4.5;
        public const double FREE_FALL_SPEED_FOR_AIRCRAFT = 0.3;
        public const double THROWS_UP_BY_HITS = 6;
        public const double THROWS_UP_BY_HITS_FOR_MIDDLE_AIRCRAFT = 4;

        // wrecks
        public const int WRECKS_RAND_RANGE = 10;
        public const int WRECKS_TYPE_NUM = 10;
        public const int WRECKS_MIN_SIZE = 2;
        public const int WRECKS_MIN_ROTATE_SPEED = 8;
        public const int WRECKS_MAX_ROTATE_SPEED = 36;
        public const int WRECKS_MICRO = 4;
        public const int WRECKS_LTL = 9;
        public const int WRECKS_BIG = 12;
        public const int WRECKS_GIGANT = 15;
        public const int WRECKS_SUSP_WRECKS_PART = 3;

        // cloud
        public const int CLOUD_SPEED = 5;
        public const int CLOUD_WIDTH_MIN = 200;
        public const int CLOUD_WIDTH_MAX = 501;
        public const int CLOUD_HEIGHT_MIN = 70;
        public const int CLOUD_HEIGHT_MAX = 171;

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
        public const int GUNS_HEATING_WARN = 280;
        public const int GUNS_OVERHEATING = 350;

        // statistic
        public static int STATISTIC_GRID_MARGIN = 120;
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

        // school
        public const string ENEMY_INFORMATION = "ВРАГ\n\nСейчас появится первый вражеский самолёт - все самолёты противника выделены на время обучения красным цветом. Нужно сбить их или хотя бы повредить. Шкала подскажет насколько повреждён каждый из них. Также указывается стоимость самолёта. Чем дороже самолёт, тем в большем приоритете нанесение повреждений именно ему. В реальном бою придётся определять самолёт по профилю, а степень их повреждения - на глаз и по ощущениям.\n\n[ OK ]";
        public const string FRIEND_INFORMATION = "СВОЙ\n\nСейчас появится первый дружественный самолёт - все подобные самолёты выделены на время обучения зелёным цветом. Нужно избегать повреждать свои самолёты. Если хоть один из них будет сбит, то игра будет провалена и сразу закончится. Шкала подскажет насколько они повреждены, но только во время обучения.\n\n[ OK ]";
        public const string AIRLINER_INFORMATION = "ПАССАЖИРСКИЙ САМОЛЁТ\n\nСейчас появится первый пассажирский самолёт - все они выделяются на время обучения синим цветом. Нужно избегать повреждать пассажирские самолёты. Если хоть один из них будет сбит, то игра будет провалена и сразу закончится - точно также как и с дружественными самолётами. Шкала подскажет насколько он повреждён, но только во время оубчения.\n\n[ OK ]";
        public const string MIX_INFORMATION = "ТЕПЕРЬ ВСЕ ВМЕСТЕ\n\nВ настоящей игре все типы самолётов летают вперемешку. Враги соседствуют с друзьями и пассажирскими самолётами. Необходимо отличать их прежде чем принять решение об открытии по ним огня. Помните, что сбить своего или пассажирский самолёт - намного хуже, чем даже пропустить врага.\n\n[ OK ]";
        public const string HEATING_INFORMATION = "ПЕРЕГРЕВ СТВОЛОВ\n\nПри стрельбе стволы зенитных пушек разогреваются. Это влияет на точность стрельбы: чем сильнее разогрет ствол, тем меньше точность. Если стрельба продолжится после того, как температура достигла очень больших значений, то пушки перегреются и заклинят. Потребуется некоторое время, чтобы они остыли - только после этого удастся возобновить стрельбу.\n\n[ OK ]";

        public const int SCHOOL_CLOUD_AT_THE_START = 5;
        public const int SCHOOL_ENEMY_AT_THE_START = 20;
        public const int SCHOOL_FRIEND_AT_THE_START = 25;
        public const int SCHOOL_AIRLINER_AT_THE_START = 30;

        // training
        public const string SUSPENDED_TARGET_INFORMATION = "ТРЕНИРОВКА\n\nДля начальной подготовки зенитчиков применяются подвесные мишени, которые необходимо расстрелять из зенитных пушек. Для этого используют старые списанные самолёты, такие как МиГ-9, МиГ-15, Як-23 или Як-25. Это самый простой вид тренировки из всех существующих.\n\n[ OK ]";
        public const string TRAINING_TUG_INFORMATION = "ТРЕНИРОВКА\n\nОдин из самых популярных способов тренировки зенитчиков является стрельба по буксируемым мишеням. Их тянет за собой самолёт-буксировщик Ил-28БМ. Нужно стрелять по буксируемой цели, но ни в коем случае не попадайте по буксировщику.\n\n[ OK ]";
        public const string TRAINING_PLANE_INFORMATION = "ТРЕНИРОВКА\n\nДля отработки навыков стрельбы используются отработавшие свой ресурс самолёты Ту-16, которые модифицируются в радиоуправляемый вариант самолёта-мишени М-16К. Эти самолёты не могут маневрировать, но очень большие, прочные и надёжные, поэтому сбить их очень сложно.\n\n[ OK ]";
        public const string TRAINING_DRONE_INFORMATION = "ТРЕНИРОВКА\n\nДля совершенствования навыков зенитчиков используются мишени Ла-17ММ и Е-95. Это медленные самолёты-мишени, не способные к манёврам уклонения, поэтому они будут простыми целями. Так же используются более быстрые иностранные MQM-36A, AQM-34, D-21. Нужно сбить или повредить как можно больше мишеней.\n\n[ OK ]";

        public const int TRAINING_TIMEOUT_BEFORE_FIRST_INFO = 1;
        public const int TRAINING_CRANE_LEFT_CORRECTTION = 200;
        public const int TRAINING_CRANE_TOP_CORRECTTION = 5;
        public const int TRAINING_CRANE_ANGLE_MAX = 15;
        public const double TRAINING_CRANE_ANGLE_CHANGE_SPEED = 0.5;

        public const int TRAINING_LAUNCH_PROBABILITTY = 5;
        public const int TRAINING_IL28_AT_THE_START = 10;
        public const int TRAINING_IL28_INDEX = 0;
        public const int TRAINING_M16K_AT_THE_START = 16;
        public const int TRAINING_M16K_INDEX = 0;
        public const int TRAINING_IL28_WITHOUT_77bm2_INDEX = 1;
        public const int TRAINING_77bm2_INDEX = 2;
        public const int TRAINING_IL28_AIRCRAFT_LEN = 300;
        public const int TRAINING_IL28_TOW_LEN = 250;
        public const int TRAINING_IL28_TARGET_LEN = 156;

        // scripts
        public const int RADAR_DAMAGED = 20;
        public const int GUN_JAMMING_CHANCE = 4;
        public const double MAX_FRAGM_SIN_DAMAGED = 1;
        public const double MAX_FRAGM_COS_DAMAGED = 1;
        public const int MAX_SPEED_DAMAGED = 7;

        public const int VIETNAM_PALM_START_POSITION = -100;
        public const int VIETNAM_PALM_HEIGHT_RANDOM = 100;
        public const int VIETNAM_PALM_HEIGHT_MIN = 100;
        public const int VIETNAM_PALM_DISTANCE = 80;

        public const int SCRIPT_SINGLE_HEIGHT = 300;
        public const double SCRIPT_SINGLE_RIGHT_POSITION = 0.65;

        public const int RADAR_MALFUNC_X = 4;
        public const int RADAR_MALFUNC_Y = 20;
        public const int RADAR_MALFUNC_BACKMOVE = 5;
        public const int RADAR_MALFUNC_MAX_DELAY = 10;
        public const int RADAR_MALFUNC_MIN_ANGLE = 0;
        public const int RADAR_MALFUNC_MAX_ANGLE = -130;
        public const int RADAR_MALFUNC_DIRECT_CHNG = 10;

        public const int GUN_MALFUNC_RANGE = 200;

        public const int UAV_SWARM_MAX = 21;
        public const int UAV_SWARM_DISTANCE = 150;
        public const int UAC_SWARM_SPEED = 11;

        public static Dictionary<Scripts.ScriptsNames, string> SCRIPT_INFORMATION = new Dictionary<Scripts.ScriptsNames, string>
        {
            [Scripts.ScriptsNames.Vietnam] = "ВОЙНА ВО ВЬЕТНАМЕ\n\nОдин из крупнейших военных конфликтов второй половины XX века, оставивший заметный след в культуре и занимающий существенное место в новейшей истории Вьетнама, а также США и СССР, сыгравших в нём немаловажную роль. Развивалась с 1955 года до падения Сайгона в 1975 году.\n\n[ OK ]",
            [Scripts.ScriptsNames.DesertStorm] = "БУРЯ В ПУСТЫНЕ\n\nЧасть войны в Персидском заливе 1990-1991 годов, операция многонациональных сил против вооружённых сил Ирака, после вторжения последних в Кувейт.\n\n[ OK ]",
            [Scripts.ScriptsNames.Yugoslavia] = "БОМБАРДИРОВКИ ЮГОСЛАВИИ\n\nВоенная агрессия НАТО против Югославии, совершённая в 1999 году под предлогом этнических чисток.\n\n[ OK ]",
            [Scripts.ScriptsNames.IranIraq] = "ирано-иракская война",
            [Scripts.ScriptsNames.Syria] = "гражданская война в сирии",
            [Scripts.ScriptsNames.Libya] = "интервенция в ливии",
            [Scripts.ScriptsNames.Yemen] = "война в йемене",
            [Scripts.ScriptsNames.Rust] = "ПОЛЁТ МАТИАСА РУСТА\n\nВ мае 1987 немецкий пилот Матиас Руст, вылетев из Хельсинки на лёгком одномоторном самолёте Cessna, нарушил границу СССР и приземлился на Красной площади. Случившийся инцидент имел большой резонанс и широко использовался в политических целях.\n\n[ OK ]",
            [Scripts.ScriptsNames.F117Hunt] = "ОХОТА НА НЕВИДИМКУ\n\nВ марте 1999 недалеко от Белграда был сбит F-117 из состава американских ВВС.\n\n[ OK ]",
            [Scripts.ScriptsNames.Khmeimim] = "оборона хмеймима",
            [Scripts.ScriptsNames.Belgrad] = "НАЛЁТ НА БЕЛГРАД\n\nБомбардировки Югославии в 1999 году характеризовались массовыми ударами по гражданским и инфраструктурным объектам. Большому количеству ударов подвергся и Белград, где был уничтожен телецентр, мосты и другие гражданские объекты.\n\n[ OK ]",
            [Scripts.ScriptsNames.Turkey] = "турецкое вторжение",
        };
    }
}
