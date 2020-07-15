
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
        public const string ENEMY_INFORMATION = "ENEMY\n\nNow the first enemy aircraft will appear - all enemy aircraft are highlighted in red for training time. You need to shot them down, or at least damage them. The scale will tell how damaged each of them is. The cost of the aircraft is also indicated. The more expensive the plane, the greater the priority of causing damage to him. In a real battle, you will have to determine the aircraft according to its profile, and the degree of damage to them - by eye and sensations.\n\n[OK]";
        public const string FRIEND_INFORMATION = "FRIEND\n\nNow the first friendly aircraft will appear - all such aircraft are highlighted in green for the duration of training. Avoid damaging your planes. If at least one of them is shot down, the game will fail and immediately end. The scale will tell how damaged they are, but only during training.\n\n[ OK ]";
        public const string AIRLINER_INFORMATION = "AIRLINER\n\nNow the first passenger plane will appear - all of them are highlighted in blue for the duration of the training. Avoid damaging passenger aircraft. If at least one of them is shot down, the game will fail and immediately end - just like with friendly planes. The scale will tell you how damaged it is, but only during the training.\n\n[ OK ]";
        public const string MIX_INFORMATION = "NOW THEY ARE MIXED\n\nIn this game, all types of planes fly intermittently. Enemies coexist with friends and passenger aircraft. It is necessary to distinguish them before deciding to open fire on them. Remember that shooting down your own or a passenger plane is much worse than even missing an enemy.\n\n[ OK ]";
        public const string HEATING_INFORMATION = "OVERHEATING OF GUNS\n\nWhen fired, the trunks of anti-aircraft guns warm up. This affects the accuracy of shooting: the more the barrel warms up, the less accuracy. If the shooting continues after the temperature has reached very high values, the guns will overheat and jam. It will take some time for them to cool - only after that they will be able to resume shooting.\n\n[ OK ]";

        public const int SCHOOL_CLOUD_AT_THE_START = 5;
        public const int SCHOOL_ENEMY_AT_THE_START = 20;
        public const int SCHOOL_FRIEND_AT_THE_START = 25;
        public const int SCHOOL_AIRLINER_AT_THE_START = 30;

        // training
        public const string SUSPENDED_TARGET_INFORMATION = "TRAINING\n\nFor the initial preparation of anti-aircraft guns, hanging targets are used, which must be shot from anti-aircraft guns. To do this, use old decommissioned aircraft, such as the MiG-9, MiG-15, Yak-23 or Yak-25. This is the easiest type of workout of all.\n\n[ OK ]";
        public const string TRAINING_TUG_INFORMATION = "TRAINING\n\nOne of the most popular ways to train anti-aircraft gunners is to shoot at towed targets. They are pulled by an Il-28BM towing aircraft. You need to shoot at the towed target, but in no case do not hit the towbar.\n\n[ OK ]";
        public const string TRAINING_PLANE_INFORMATION = "TRAINING\n\nTo improve shooting skills, used Tu-16 aircraft, which are used up for their life, are modified into a radio-controlled version of the M-16K target aircraft. These aircraft cannot maneuver, but they are very large, durable and reliable, so it is very difficult to shot them down.\n\n[ OK ]";
        public const string TRAINING_DRONE_INFORMATION = "TRAINING\n\nTo improve the skills of anti-aircraft gunners used targets La-17MM and E-95. These are slow target aircraft, not capable of evasion maneuvers, so they will be simple targets. Faster foreign MQM-36A, AQM-34, D-21 are also used. You need to shoot down or damage as many targets as possible.\n\n[ OK ]";

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
    }
}
