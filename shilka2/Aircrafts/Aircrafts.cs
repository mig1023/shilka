using System.Collections.Generic;
using System.Windows;

namespace shilka2
{
    class Aircrafts
    {
        public static int minAltitudeGlobal { get; set; }
        public static int minAltitudeForLargeAircraft = (int)SystemParameters.PrimaryScreenHeight / 2;
        public static int minAltitudeForAerostat = (int)SystemParameters.PrimaryScreenHeight / 8;
        public static int maxAltitudeForHelicopters = minAltitudeForLargeAircraft;

        public static List<Aircraft> aircraft = new List<Aircraft>()
        {
            new Aircraft
            {
                aircraftType = "a10",
                aircraftName = "A-10 Thunderbolt",
                hitPoint = 200,
                size = new int[] { 270, 68 },
                price = 12,
                speed = 5,
                cantEscape = true
            },

            new Aircraft
            {
                aircraftType = "b1",
                aircraftName = "B-1 Lancer",
                hitPoint = 100,
                size = new int[] { 510, 114 },
                price = 283,
                speed = 12,
                wrecksMaxSize = Constants.WRECKS_GIGANT
            },

            new Aircraft
            {
                aircraftType = "b52",
                aircraftName = "B-52 Stratofortress",
                hitPoint = 120,
                size = new int[] { 565, 133 },
                price = 53,
                speed = 8,
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true,
                wrecksMaxSize = Constants.WRECKS_GIGANT
            },

            new Aircraft
            {
                aircraftType = "f117",
                aircraftName = "F-117 Nighthawk",
                hitPoint = 50,
                size = new int[] { 270, 47 },
                price = 112,
                wrecksNumber = 3
            },

            new Aircraft
            {
                aircraftType = "f14",
                aircraftName = "F-14 Tomcat",
                size = new int[] { 275, 66 },
                price = 38
            },

            new Aircraft
            {
                aircraftType = "f18",
                aircraftName = "F-18 Hornet",
                size = new int[] { 270, 66 },
                price = 57
            },

            new Aircraft
            {
                aircraftType = "f16",
                aircraftName = "F-16 Fighting Falcon",
                size = new int[] { 270, 89 },
                price = 34
            },

            new Aircraft
            {
                aircraftType = "f22",
                aircraftName = "F-22 Raptor",
                size = new int[] { 270, 65 },
                price = 146,
                speed = 14,
                doesNotFlyInBadWeather = true,
                wrecksNumber = 3
            },

            new Aircraft
            {
                aircraftType = "f15",
                aircraftName = "F-15 Eagle",
                hitPoint = 100,
                size = new int[] { 270, 81 },
                price = 29
            },

            new Aircraft
            {
                aircraftType = "f4",
                aircraftName = "F-4 Fantom",
                hitPoint = 150,
                size = new int[] { 270, 64 },
                price = 3,
                speed = 8
            },

            new Aircraft
            {
                aircraftType = "tornado",
                aircraftName = "Panavia Tornado",
                hitPoint = 100,
                size = new int[] { 270, 72 },
                price = 111
            },

            new Aircraft
            {
                aircraftType = "predator",
                aircraftName = "MQ-1 Predator",
                hitPoint = 30,
                size = new int[] { 140, 44 },
                price = 4,
                speed = 5,
                cantEscape = true,
                deadSprite = true,
                weight = Aircraft.WeightType.Middle,
                wrecksMaxSize = Constants.WRECKS_LTL,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "ltl_prop",
                        y = -15,
                        x_left = 130,
                        x_right = -5,
                        movingType = DynamicElement.MovingType.yRotate
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "reaper",
                aircraftName = "MQ-9 Reaper",
                hitPoint = 50,
                size = new int[] { 161, 52 },
                price = 16,
                speed = 5,
                cantEscape = true,
                deadSprite = true,
                weight = Aircraft.WeightType.Middle,
                wrecksMaxSize = Constants.WRECKS_LTL,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "ltl_prop",
                        y = -3,
                        x_left = 143,
                        x_right = 5,
                        movingType = DynamicElement.MovingType.yRotate,
                        background = true,
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "f35",
                aircraftName = "F-35 Lightning II",
                size = new int[] { 270, 69 },
                price = 108,
                doesNotFlyInBadWeather = true,
                wrecksNumber = 3
            },

            new Aircraft
            {
                aircraftType = "e3",
                aircraftName = "E-3 Centry",
                hitPoint = 150,
                size = new int[] { 581, 164 },
                price = 270,
                speed = 8,
                deadSprite = true,
                minAltitude = minAltitudeForLargeAircraft,
                wrecksMaxSize = Constants.WRECKS_GIGANT,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "e3rls",
                        y = 34,
                        x_left = 312,
                        x_right = 152,
                        movingType = DynamicElement.MovingType.xRotate,
                        slowRotation = true,
                        backSide = true,
                        oneWay = true,
                    },
                }
            },

            new Aircraft
            {
                aircraftType = "eurofighter",
                aircraftName = "Eurofighter Typhoon",
                size = new int[] { 270, 77 },
                price = 123
            },

            new Aircraft
            {
                aircraftType = "rafale",
                aircraftName = "Rafale",
                size = new int[] { 270, 86 },
                price = 85,
                speed = 11
            },

            new Aircraft
            {
                aircraftType = "b2",
                aircraftName = "B-2 Spirit",
                hitPoint = 125,
                size = new int[] { 332, 76 },
                price = 2100,
                speed = 18,
                wrecksMaxSize = Constants.WRECKS_BIG
            },

            new Aircraft
            {
                aircraftType = "globalhawk",
                aircraftName = "RQ-4 Global Hawk",
                hitPoint = 100,
                size = new int[] { 265, 85 },
                price = 70,
                speed = 7,
                cantEscape = true,
                minAltitude = minAltitudeForLargeAircraft
            },

            new Aircraft
            {
                aircraftType = "tomahawk",
                aircraftName = "Tomahawk",
                hitPoint = 20,
                size = new int[] { 125, 29 },
                price = 2,
                speed = 5,
                cantEscape = true,
                weight = Aircraft.WeightType.Middle,
                wrecksMaxSize = Constants.WRECKS_LTL
            },

            new Aircraft
            {
                aircraftType = "f8",
                aircraftName = "F-8 Crusader",
                size = new int[] { 270, 93 },
                price = 6,
                speed = 8
            },

            new Aircraft
            {
                aircraftType = "ac130",
                aircraftName = "AC-130 Spectre",
                hitPoint = 120,
                size = new int[] { 400, 150 },
                price = 190,
                speed = 7,
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true,
                deadSprite = true,
                wrecksMaxSize = Constants.WRECKS_GIGANT,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "air_prop",
                        y = 56,
                        x_left = 107,
                        x_right = 270,
                        movingType = DynamicElement.MovingType.yRotate
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "a6",
                aircraftName = "A-6 Intruder",
                size = new int[] { 270, 81 },
                price = 43,
                speed = 7
            },

            new Aircraft
            {
                aircraftType = "f111",
                aircraftName = "F-111",
                size = new int[] { 285, 59 },
                price = 72
            },

            new Aircraft
            {
                aircraftType = "f5",
                aircraftName = "F-5 Tiger",
                size = new int[] { 270, 58 },
                price = 2
            },

            new Aircraft
            {
                aircraftType = "scalp",
                aircraftName = "SCALP",
                hitPoint = 20,
                size = new int[] { 115, 23 },
                price = 2,
                speed = 5,
                cantEscape = true,
                wrecksMaxSize = Constants.WRECKS_MICRO,
                weight = Aircraft.WeightType.Light,
            },

            new Aircraft
            {
                aircraftType = "ea6",
                aircraftName = "EA-6 Prowler",
                size = new int[] { 285, 66 },
                price = 52,
                speed = 7
            },

            new Aircraft
            {
                aircraftType = "hawkeye",
                aircraftName = "E-2 Hawkeye",
                hitPoint = 100,
                size = new int[] { 324, 96 },
                price = 80,
                speed = 8,
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true,
                deadSprite = true,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "air_prop",
                        y = 13,
                        x_left = 75,
                        x_right = 225,
                        movingType = DynamicElement.MovingType.yRotate
                    },
                    new DynamicElement
                    {
                        elementName = "hawkeyerls",
                        y = 4,
                        x_left = 118,
                        x_right = 70,
                        movingType = DynamicElement.MovingType.xRotate,
                        slowRotation = true,
                        backSide = true,
                        oneWay = true,
                    },
                }
            },

            new Aircraft
            {
                aircraftType = "rc135",
                aircraftName = "RC-135",
                hitPoint = 120,
                size = new int[] { 528, 185 },
                price = 90,
                speed = 8,
                minAltitude = minAltitudeForLargeAircraft,
                wrecksMaxSize = Constants.WRECKS_GIGANT,
                cantEscape = true
            },

            new Aircraft
            {
                aircraftType = "u2",
                aircraftName = "U-2",
                size = new int[] { 355, 103 },
                price = 6,
                speed = 8,
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true,
            },

            new Aircraft
            {
                aircraftType = "sr71",
                aircraftName = "SR-71 Blackbird",
                size = new int[] { 530, 93 },
                price = 534,
                speed = 18,
                minAltitude = minAltitudeForLargeAircraft,
                wrecksMaxSize = Constants.WRECKS_GIGANT,
            },

            new Aircraft
            {
                aircraftType = "harrier",
                aircraftName = "BAE Sea Harrier",
                size = new int[] { 275, 81 },
                price = 24,
                speed = 7
            },

            new Aircraft
            {
                aircraftType = "cessna",
                aircraftName = "Cessna 172",
                hitPoint = 30,
                size = new int[] { 173, 61 },
                speed = 6,
                price = 0.3,
                cantEscape = true,
                deadSprite = true,
                weight = Aircraft.WeightType.Middle,
                wrecksMaxSize = Constants.WRECKS_LTL,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "ltl_prop",
                        y = 2,
                        x_left = -3,
                        x_right = 164,
                        movingType = DynamicElement.MovingType.yRotate,
                        mirror = true
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "hunter",
                aircraftName = "RQ-5 Hunter",
                hitPoint = 30,
                size = new int[] { 172, 49 },
                speed = 5,
                price = 2,
                cantEscape = true,
                deadSprite = true,
                weight = Aircraft.WeightType.Middle,
                wrecksMaxSize = Constants.WRECKS_LTL,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "ltl_prop",
                        y = -10,
                        x_left = 105,
                        x_right = 53,
                        movingType = DynamicElement.MovingType.yRotate,
                        background = true
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "r99",
                aircraftName = "Embraer R-99",
                hitPoint = 100,
                size = new int[] { 350, 88 },
                price = 80,
                speed = 8,
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true
            },

            new Aircraft
            {
                aircraftType = "m2000",
                aircraftName = "Mirage 2000",
                size = new int[] { 270, 79 },
                price = 25
            },

            new Aircraft
            {
                aircraftType = "m2000ed",
                aircraftName = "Mirage 2000ED",
                size = new int[] { 270, 75 },
                price = 35
            },

            new Aircraft
            {
                aircraftType = "jassm",
                aircraftName = "JASSM",
                hitPoint = 20,
                size = new int[] { 108, 25 },
                price = 0.85,
                speed = 5,
                cantEscape = true,
                weight = Aircraft.WeightType.Light,
                wrecksMaxSize = Constants.WRECKS_MICRO,
            },

            new Aircraft
            {
                aircraftType = "gripen",
                aircraftName = "Saab Gripen",
                size = new int[] { 247, 72 },
                price = 14
            },

            new Aircraft
            {
                aircraftType = "boeing737aewc",
                aircraftName = "Боинг 737 AEW&C",
                size = new int[] { 565, 187 },
                price = 490,
                speed = 7,
                minAltitude = minAltitudeForLargeAircraft,
                wrecksMaxSize = Constants.WRECKS_GIGANT,
                cantEscape = true
            },

            new Aircraft
            {
                aircraftType = "c5",
                aircraftName = "C-5 Galaxy",
                hitPoint = 150,
                size = new int[] { 570, 152 },
                speed = 7,
                minAltitude = minAltitudeForLargeAircraft,
                wrecksMaxSize = Constants.WRECKS_GIGANT,
                price = 262,
                cantEscape = true
            },

            new Aircraft
            {
                aircraftType = "c17",
                aircraftName = "C-17 Globemaster III",
                hitPoint = 120,
                size = new int[] { 462, 150 },
                speed = 8,
                minAltitude = minAltitudeForLargeAircraft,
                wrecksMaxSize = Constants.WRECKS_GIGANT,
                price = 316,
                cantEscape = true
            },

            new Aircraft
            {
                aircraftType = "a1",
                aircraftName = "A-1 Skyraider",
                hitPoint = 80,
                size = new int[] { 195, 67 },
                speed = 7,
                price = 0.4,
                cantEscape = true,
                deadSprite = true,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "ltl_prop",
                        y = 19,
                        x_left = -1,
                        x_right = 183,
                        movingType = DynamicElement.MovingType.yRotate,
                        mirror = true
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "kc135",
                aircraftName = "KC-135 Stratotanker",
                hitPoint = 120,
                size = new int[] { 500, 157 },
                speed = 8,
                minAltitude = minAltitudeForLargeAircraft,
                wrecksMaxSize = Constants.WRECKS_BIG,
                price = 39,
                cantEscape = true
            },

            new Aircraft
            {
                aircraftType = "jaguar",
                aircraftName = "SEPECAT Jaguar",
                size = new int[] { 275, 81 },
                price = 8
            },

            new Aircraft
            {
                aircraftType = "argus",
                aircraftName = "Saab S100B Argus",
                hitPoint = 100,
                size = new int[] { 300, 105 },
                speed = 7,
                price = 110,
                cantEscape = true,
                deadSprite = true,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "ltl_prop",
                        y = 50,
                        x_left = 80,
                        x_right = 207,
                        movingType = DynamicElement.MovingType.yRotate,
                        mirror = true
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "ov10",
                aircraftName = "OV-10 Bronco",
                hitPoint = 100,
                size = new int[] { 270, 88 },
                speed = 7,
                price = 5,
                deadSprite = true,
                wrecksMaxSize = Constants.WRECKS_LTL,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "ltl_prop",
                        y = 29,
                        x_left = 78,
                        x_right = 178,
                        movingType = DynamicElement.MovingType.yRotate,
                        mirror = true
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "s3",
                aircraftName = "Lockheed S-3 Viking",
                hitPoint = 100,
                speed = 9,
                size = new int[] { 320, 132 },
                price = 27,
            },

            new Aircraft
            {
                aircraftType = "f104",
                aircraftName = "Lockheed F-104 Starfighter",
                speed = 12,
                size = new int[] { 283, 67 },
                price = 35,
            },

            new Aircraft
            {
                aircraftType = "harriergr3",
                aircraftName = "Harrier GR.3",
                size = new int[] { 275, 73 },
                price = 12,
                speed = 6
            },

             new Aircraft
            {
                aircraftType = "harriermk1",
                aircraftName = "Harrier MK1",
                size = new int[] { 275, 77 },
                price = 6,
                speed = 5
            },

            new Aircraft
            {
                aircraftType = "a26",
                aircraftName = "A-26 Invader",
                hitPoint = 100,
                size = new int[] { 283, 102 },
                speed = 6,
                price = 0.2,
                deadSprite = true,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "mdl_prop",
                        y = 34,
                        x_left = 25,
                        x_right = 235,
                        movingType = DynamicElement.MovingType.yRotate,
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "anka",
                aircraftName = "TAI Anka-S",
                hitPoint = 30,
                size = new int[] { 147, 44 },
                speed = 8,
                price = 18,
                cantEscape = true,
                deadSprite = true,
                weight = Aircraft.WeightType.Middle,
                wrecksMaxSize = Constants.WRECKS_LTL,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "ltl_prop",
                        y = -3,
                        x_left = 135,
                        x_right = -2,
                        movingType = DynamicElement.MovingType.yRotate
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "bayraktar",
                aircraftName = "Bayraktar BT2",
                hitPoint = 20,
                size = new int[] { 140, 43 },
                speed = 5,
                price = 5,
                cantEscape = true,
                deadSprite = true,
                weight = Aircraft.WeightType.Middle,
                wrecksMaxSize = Constants.WRECKS_LTL,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "ltl_prop",
                        y = -6,
                        x_left = 90,
                        x_right = 36,
                        movingType = DynamicElement.MovingType.yRotate,
                        background = true
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "hammerhead",
                aircraftName = "Piaggio Aerospace Hammerhead",
                hitPoint = 50,
                size = new int[] { 200, 59 },
                speed = 7,
                price = 31,
                cantEscape = true,
                deadSprite = true,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "ltl_prop",
                        y = 3,
                        x_left = 134,
                        x_right = 53,
                        movingType = DynamicElement.MovingType.yRotate,
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "supertucano",
                aircraftName = "Embraer EMB 314 Super Tucano",
                hitPoint = 60,
                size = new int[] { 205, 62 },
                speed = 6,
                price = 14,
                deadSprite = true,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "ltl_prop",
                        y = 12,
                        x_left = -3,
                        x_right = 193,
                        movingType = DynamicElement.MovingType.yRotate,
                        mirror = true,
                    }
                }
            },
        };

        public static List<Aircraft> helicopters = new List<Aircraft>()
        {
            new Aircraft
            {
                aircraftType = "ah64",
                aircraftName = "AH-64 Apache",
                hitPoint = 120,
                size = new int[] { 209, 63 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                wrecksMaxSize = Constants.WRECKS_LTL,
                price = 61,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "prop_main",
                        y = -8,
                        x_left = -41,
                        x_right = 27,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement
                    {
                        elementName = "x_suppl",
                        y = -5,
                        x_left = 170,
                        x_right = -10,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "ah1",
                aircraftName = "AH-1 Cobra",
                hitPoint = 100,
                size = new int[] { 209, 54 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                wrecksMaxSize = Constants.WRECKS_LTL,
                price = 11,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "prop_main",
                        y = -22,
                        x_left = -41,
                        x_right = 27,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement
                    {
                        elementName = "i_suppl",
                        y = -9,
                        x_left = 175,
                        x_right = -12,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "uh60",
                aircraftName = "UH-60 Black Hawk",
                size = new int[] { 210, 65 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                wrecksMaxSize = Constants.WRECKS_LTL,
                price = 25,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "prop_main",
                        y = -5,
                        x_left = -48,
                        x_right = 36,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement
                    {
                        elementName = "t_suppl",
                        y = -11,
                        x_left = 175,
                        x_right = -12,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "uh1",
                aircraftName = "UH-1 Iroquois",
                size = new int[] { 210, 64 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                wrecksMaxSize = Constants.WRECKS_LTL,
                price = 5,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "prop_main",
                        y = -16,
                        x_left = -41,
                        x_right = 25,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement
                    {
                        elementName = "i_suppl",
                        y = -16,
                        x_left = 178,
                        x_right = -14,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "ch47",
                aircraftName = "CH-47 Chinook",
                size = new int[] { 270, 101 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                price = 30,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "prop_main",
                        y = 6,
                        x_left = -68,
                        x_right = 110,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement
                    {
                        elementName = "prop_main",
                        y = -20,
                        x_left = 125,
                        x_right = -78,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                }
            },

            new Aircraft
            {
                aircraftType = "v22",
                aircraftName = "V-22 Ospray",
                size = new int[] { 282, 103 },
                speed = 7,
                maxAltitude = maxAltitudeForHelicopters,
                price = 116,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "prop_main",
                        y = -18,
                        x_left = -5,
                        x_right = 60,
                        movingType = DynamicElement.MovingType.xRotate,
                        startDegree = 0.5,
                    },
                }
            },

            new Aircraft
            {
                aircraftType = "tiger",
                aircraftName = "Eurocopter Tiger UHT",
                size = new int[] { 209, 76 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                wrecksMaxSize = Constants.WRECKS_LTL,
                price = 39,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "prop_main",
                        y = -3,
                        x_left = -35,
                        x_right = 20,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement
                    {
                        elementName = "y_suppl",
                        y = 5,
                        x_left = 170,
                        x_right = -7,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "drone",
                aircraftName = "дрон-разведчик (тип 1)",
                hitPoint = 1,
                size = new int[] { 26, 9 },
                speed = 3,
                maxAltitude = maxAltitudeForHelicopters,
                wrecksMaxSize = Constants.WRECKS_MICRO,
                wrecksNumber = 4,
                price = 0.01,
                deadSprite = true,
                cantEscape = true,
                weight = Aircraft.WeightType.Light,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "micro_prop",
                        y = -5,
                        x_left = -6,
                        x_right = -6,
                        movingType = DynamicElement.MovingType.xRotate,
                        startDegree = 0.5,
                    },
                    new DynamicElement
                    {
                        elementName = "micro_prop",
                        y = -5,
                        x_left = 15,
                        x_right = 15,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                }
            },

            new Aircraft
            {
                aircraftType = "gazelle",
                aircraftName = "Aerospatiale Gazelle",
                hitPoint = 60,
                size = new int[] { 185, 64 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                wrecksMaxSize = Constants.WRECKS_LTL,
                price = 0.5,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "prop_main",
                        y = -3,
                        x_left = -53,
                        x_right = 12,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement
                    {
                        elementName = "f_suppl",
                        y = 19,
                        x_left = 155,
                        x_right = 7,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "comanche",
                aircraftName = "RAH-66 Comanche",
                size = new int[] { 210, 61 },
                speed = 6,
                maxAltitude = maxAltitudeForHelicopters,
                wrecksMaxSize = Constants.WRECKS_LTL,
                price = 100,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "prop_main",
                        y = -10,
                        x_left = -29,
                        x_right = 12,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement
                    {
                        elementName = "f_suppl",
                        y = 26,
                        x_left = 175,
                        x_right = 12,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "oh1",
                aircraftName = "OH-1 Ninja",
                size = new int[] { 205, 70 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                wrecksMaxSize = Constants.WRECKS_LTL,
                price = 24,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "prop_main_ltl",
                        y = -1,
                        x_left = 10,
                        x_right = 60,
                        movingType = DynamicElement.MovingType.xRotate,
                        background = true
                    },
                    new DynamicElement
                    {
                        elementName = "f_suppl",
                        y = 29,
                        x_left = 161,
                        x_right = 22,
                        movingType = DynamicElement.MovingType.zRotate,
                        background = true
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "mangusta",
                aircraftName = "T-129 Mangusta",
                hitPoint = 100,
                size = new int[] { 215, 66 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                wrecksMaxSize = Constants.WRECKS_LTL,
                price = 52,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "prop_main",
                        y = -14,
                        x_left = -35,
                        x_right = 22,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement
                    {
                        elementName = "i_suppl",
                        y = -14,
                        x_left = 179,
                        x_right = -4,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "puma",
                aircraftName = "Aerospatiale Puma",
                size = new int[] { 215, 58 },
                price = 15,
                speed = 5,
                minAltitude = maxAltitudeForHelicopters,
                wrecksMaxSize = Constants.WRECKS_LTL,
                cantEscape = true,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "prop_main",
                        y = -19,
                        x_left = -52,
                        x_right = 40,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement
                    {
                        elementName = "t_suppl",
                        y = -16,
                        x_left = 177,
                        x_right = -11,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "mh53",
                aircraftName = "Сикорский MH-53",
                hitPoint = 100,
                size = new int[] { 375, 84 },
                price = 53,
                speed = 5,
                minAltitude = maxAltitudeForHelicopters,
                cantEscape = true,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "prop_main_big",
                        y = 5,
                        x_left = 40,
                        x_right = 69,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement
                    {
                        elementName = "t_suppl",
                        y = -16,
                        x_left = 327,
                        x_right = -11,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new Aircraft {
                aircraftType = "as565",
                aircraftName = "Eurocopter AS565",
                size = new int[] { 199, 70 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                wrecksMaxSize = Constants.WRECKS_LTL,
                price = 10,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "prop_main",
                        y = -4,
                        x_left = -33,
                        x_right = 3,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement
                    {
                        elementName = "f_suppl",
                        y = 34,
                        x_left = 167,
                        x_right = 10,
                        movingType = DynamicElement.MovingType.zRotate,
                        background = true
                    }
                }
            },

            new Aircraft {
                aircraftType = "drone2",
                aircraftName = "дрон-разведчик (тип 2)",
                hitPoint = 1,
                size = new int[] { 30, 17 },
                speed = 3,
                maxAltitude = maxAltitudeForHelicopters,
                wrecksMaxSize = Constants.WRECKS_MICRO,
                wrecksNumber = 4,
                price = 0.01,
                deadSprite = true,
                cantEscape = true,
                weight = Aircraft.WeightType.Light,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "micro_prop",
                        y = -2,
                        x_left = -6,
                        x_right = -6,
                        movingType = DynamicElement.MovingType.xRotate,
                        startDegree = 0.5,
                    },
                    new DynamicElement
                    {
                        elementName = "micro_prop",
                        y = -2,
                        x_left = 19,
                        x_right = 19,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                }
            },

            new Aircraft
            {
                aircraftType = "oh58d",
                aircraftName = "Белл OH-58D",
                size = new int[] { 209, 83 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                wrecksMaxSize = Constants.WRECKS_LTL,
                price = 11,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "prop_main",
                        y = 5,
                        x_left = -43,
                        x_right = 29,
                        movingType = DynamicElement.MovingType.xRotate,
                        background = true
                    },
                    new DynamicElement
                    {
                        elementName = "ltl_suppl",
                        y = 24,
                        x_left = 165,
                        x_right = 14,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "rooivalk",
                aircraftName = "Denel AH-2 Rooivalk",
                hitPoint = 100,
                size = new int[] { 265, 74 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                wrecksMaxSize = Constants.WRECKS_LTL,
                price = 40,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "prop_main",
                        y = -10,
                        x_left = -20,
                        x_right = 58,
                        movingType = DynamicElement.MovingType.xRotate,
                        background = true
                    },
                    new DynamicElement
                    {
                        elementName = "five_suppl",
                        y = -2,
                        x_left = 205,
                        x_right = 10,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "ah6",
                aircraftName = "Боинг AH-6",
                size = new int[] { 134, 58 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                wrecksMaxSize = Constants.WRECKS_MICRO,
                price = 2,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "prop_main_ltl",
                        y = -12,
                        x_left = -20,
                        x_right = 20,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement
                    {
                        elementName = "ltl_suppl",
                        y = 7,
                        x_left = 110,
                        x_right = -3,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "drone3",
                aircraftName = "дрон-разведчик (тип 3)",
                hitPoint = 1,
                size = new int[] { 32, 20 },
                speed = 3,
                maxAltitude = maxAltitudeForHelicopters,
                wrecksMaxSize = Constants.WRECKS_MICRO,
                wrecksNumber = 4,
                price = 0.01,
                deadSprite = true,
                cantEscape = true,
                weight = Aircraft.WeightType.Light,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "micro_prop",
                        y = -4,
                        x_left = -5,
                        x_right = -6,
                        movingType = DynamicElement.MovingType.xRotate,
                        startDegree = 0.5,
                    },
                    new DynamicElement
                    {
                        elementName = "micro_prop",
                        y = -4,
                        x_left = 20,
                        x_right = 19,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                }
            },

            new Aircraft
            {
                aircraftType = "drone4",
                aircraftName = "дрон-разведчик (тип 4)",
                hitPoint = 1,
                size = new int[] { 75, 22 },
                speed = 3,
                maxAltitude = maxAltitudeForHelicopters,
                wrecksMaxSize = Constants.WRECKS_MICRO,
                wrecksNumber = 4,
                price = 0.01,
                deadSprite = true,
                cantEscape = true,
                weight = Aircraft.WeightType.Middle,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "micro_prop_y",
                        y = 5,
                        x_left = 2,
                        x_right = 67,
                        movingType = DynamicElement.MovingType.yRotate,
                        startDegree = 0.5,
                        background = true
                    },
                }
            },

            new Aircraft
            {
                aircraftType = "tiger-hap",
                aircraftName = "Eurocopter Tiger HAP",
                size = new int[] { 222, 76 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                wrecksMaxSize = Constants.WRECKS_LTL,
                price = 28,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "prop_main",
                        y = -3,
                        x_left = -21,
                        x_right = 15,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement
                    {
                        elementName = "y_suppl",
                        y = 4,
                        x_left = 181,
                        x_right = -1,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "h34",
                aircraftName = "Сикорский H-34",
                hitPoint = 140,
                size = new int[] { 260, 72 },
                speed = 4,
                maxAltitude = maxAltitudeForHelicopters,
                price = 2,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "prop_main",
                        y = -23,
                        x_left = -50,
                        x_right = 85,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement
                    {
                        elementName = "y_suppl",
                        y = -14,
                        x_left = 221,
                        x_right = -2,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "ch54",
                aircraftName = "Сикорский CH-54",
                hitPoint = 140,
                size = new int[] { 310, 87 },
                speed = 4,
                maxAltitude = maxAltitudeForHelicopters,
                price = 2,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "prop_main",
                        y = -13,
                        x_left = 0,
                        x_right = 85,
                        movingType = DynamicElement.MovingType.xRotate
                    },
                    new DynamicElement
                    {
                        elementName = "t_suppl",
                        y = -16,
                        x_left = 268,
                        x_right = -12,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "drone5",
                aircraftName = "дрон-разведчик (тип 5)",
                hitPoint = 1,
                size = new int[] { 35, 25 },
                speed = 3,
                maxAltitude = maxAltitudeForHelicopters,
                wrecksMaxSize = Constants.WRECKS_MICRO,
                wrecksNumber = 4,
                price = 0.01,
                deadSprite = true,
                cantEscape = true,
                weight = Aircraft.WeightType.Light,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "micro_prop",
                        y = 0,
                        x_left = -6,
                        x_right = -6,
                        movingType = DynamicElement.MovingType.xRotate,
                        startDegree = 0.5,
                    },
                    new DynamicElement
                    {
                        elementName = "micro_prop",
                        y = 0,
                        x_left = 24,
                        x_right = 25,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                }
            },

            new Aircraft
            {
                aircraftType = "drone6",
                aircraftName = "дрон-разведчик (тип 6)",
                hitPoint = 1,
                size = new int[] { 30, 20 },
                speed = 3,
                maxAltitude = maxAltitudeForHelicopters,
                wrecksMaxSize = Constants.WRECKS_MICRO,
                wrecksNumber = 4,
                price = 0.01,
                deadSprite = true,
                cantEscape = true,
                weight = Aircraft.WeightType.Light,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "micro_prop",
                        y = -3,
                        x_left = -6,
                        x_right = -6,
                        movingType = DynamicElement.MovingType.xRotate,
                        startDegree = 0.5,
                    },
                    new DynamicElement
                    {
                        elementName = "micro_prop",
                        y = -3,
                        x_left = 19,
                        x_right = 19,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                }
            },

            new Aircraft
            {
                aircraftType = "drone7",
                aircraftName = "дрон-разведчик (тип 7)",
                hitPoint = 1,
                size = new int[] { 85, 35 },
                speed = 6,
                deadSprite = true,
                cantEscape = true,
                weight = Aircraft.WeightType.Middle,
                maxAltitude = maxAltitudeForHelicopters,
                wrecksMaxSize = Constants.WRECKS_MICRO,
                wrecksNumber = 4,
                price = 0.01,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "micro_prop",
                        y = 5,
                            x_left = 0,
                        x_right = 26,
                        movingType = DynamicElement.MovingType.xRotate,
                        startDegree = 0.5,
                    },
                    new DynamicElement
                    {
                        elementName = "micro_prop",
                        y = 3,
                            x_left = 16,
                        x_right = 51,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement
                    {
                        elementName = "micro_prop",
                        y = 5,
                            x_left = 41,
                        x_right = 67,
                        movingType = DynamicElement.MovingType.xRotate,
                        startDegree = 0.5,
                    },
                    new DynamicElement
                    {
                        elementName = "micro_prop_y",
                        y = 11,
                        x_left = 84,
                        x_right = -3,
                        movingType = DynamicElement.MovingType.yRotate,
                        startDegree = 0.5,
                    },
                }
            },

            new Aircraft
            {
                aircraftType = "drone8",
                aircraftName = "дрон-разведчик (тип 8)",
                hitPoint = 1,
                size = new int[] { 35, 12 },
                speed = 3,
                maxAltitude = maxAltitudeForHelicopters,
                wrecksMaxSize = Constants.WRECKS_MICRO,
                wrecksNumber = 4,
                price = 0.01,
                deadSprite = true,
                cantEscape = true,
                weight = Aircraft.WeightType.Light,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "micro_prop",
                        y = -4,
                        x_left = -7,
                        x_right = -7,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement
                    {
                        elementName = "micro_prop",
                        y = -4,
                        x_left = 24,
                        x_right = 24,
                        movingType = DynamicElement.MovingType.xRotate,
                        startDegree = 0.7,
                    },
                    new DynamicElement
                    {
                        elementName = "micro_prop",
                        y = 5,
                        x_left = -7,
                        x_right = -7,
                        movingType = DynamicElement.MovingType.xRotate,
                        startDegree = 0.2,
                    },
                    new DynamicElement
                    {
                        elementName = "micro_prop",
                        y = 5,
                        x_left = 24,
                        x_right = 24,
                        movingType = DynamicElement.MovingType.xRotate,
                        startDegree = 0.5,
                    },
                }
            },

            new Aircraft
            {
                aircraftType = "drone9",
                aircraftName = "дрон-разведчик (тип 9)",
                hitPoint = 1,
                size = new int[] { 35, 22 },
                speed = 3,
                maxAltitude = maxAltitudeForHelicopters,
                wrecksMaxSize = Constants.WRECKS_MICRO,
                wrecksNumber = 4,
                price = 0.01,
                deadSprite = true,
                cantEscape = true,
                weight = Aircraft.WeightType.Light,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "micro_prop",
                        y = -3,
                        x_left = -6,
                        x_right = -7,
                        movingType = DynamicElement.MovingType.xRotate,
                        startDegree = 0.5,
                    },
                    new DynamicElement
                    {
                        elementName = "micro_prop",
                        y = -3,
                        x_left = 24,
                        x_right = 23,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                }
            },

            new Aircraft
            {
                aircraftType = "raven",
                aircraftName = "RQ-11 Raven",
                hitPoint = 15,
                size = new int[] { 100, 38 },
                speed = 4,
                price = 0.1,
                cantEscape = true,
                deadSprite = true,
                weight = Aircraft.WeightType.Middle,
                wrecksMaxSize = Constants.WRECKS_MICRO,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "micro_prop_y",
                        y = 7,
                        x_left = 44,
                        x_right = 51,
                        movingType = DynamicElement.MovingType.yRotate,
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "lynx",
                aircraftName = "Westland Lynx",
                hitPoint = 80,
                size = new int[] { 199, 50 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                wrecksMaxSize = Constants.WRECKS_LTL,
                price = 3,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "prop_main_ltl",
                        y = -22,
                        x_left = 5,
                        x_right = 60,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement
                    {
                        elementName = "t_suppl",
                        y = -19,
                        x_left = 160,
                        x_right = -12,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },
        };

        public static List<Aircraft> aerostat = new List<Aircraft>()
        {
            new Aircraft
            {
                aircraftType = "aerostat",
                aircraftName = "аэростат-разведчик (тип 1)",
                size = new int[] { 82, 228 },
                hitPoint = 20,
                speed = 5,
                price = 0.1,
                aerostat = true,
                fallLikeAStone = true,
                cantEscape = true,
                minAltitude = minAltitudeForAerostat,
                wrecksMaxSize = Constants.WRECKS_MICRO,
                weight = Aircraft.WeightType.Light
            },

            new Aircraft
            {
                aircraftType = "aerostat2",
                aircraftName = "аэростат-разведчик (тип 2)",
                size = new int[] { 60, 132 },
                hitPoint = 10,
                speed = 4,
                price = 0.1,
                aerostat = true,
                fallLikeAStone = true,
                cantEscape = true,
                minAltitude = minAltitudeForAerostat,
                wrecksMaxSize = Constants.WRECKS_MICRO,
                weight = Aircraft.WeightType.Light
            },

            new Aircraft
            {
                aircraftType = "aerostat3",
                aircraftName = "аэростат-разведчик (тип 3)",
                size = new int[] { 70, 118 },
                hitPoint = 10,
                speed = 4,
                price = 0.1,
                aerostat = true,
                fallLikeAStone = true,
                cantEscape = true,
                minAltitude = minAltitudeForAerostat,
                wrecksMaxSize = Constants.WRECKS_MICRO,
                weight = Aircraft.WeightType.Light
            },
        };

        public static List<Aircraft> aircraftFriend = new List<Aircraft>()
        {
            new Aircraft
            {
                aircraftType = "mig23",
                aircraftName = "МиГ-23",
                size = new int[] { 270, 71 },
                friend = true
            },

            new Aircraft
            {
                aircraftType = "mig29",
                aircraftName = "МиГ-29",
                size = new int[] { 270, 66 },
                friend = true
            },

            new Aircraft
            {
                aircraftType = "mig31",
                aircraftName = "МиГ-31",
                size = new int[] { 270, 63 },
                speed = 14,
                friend = true
            },

            new Aircraft
            {
                aircraftType = "su17",
                aircraftName = "Су-17",
                size = new int[] { 270, 61 },
                speed = 5,
                friend = true
            },

            new Aircraft
            {
                aircraftType = "su24",
                aircraftName = "Су-24",
                size = new int[] { 270, 67 },
                speed = 8,
                friend = true
            },

            new Aircraft
            {
                aircraftType = "su25",
                aircraftName = "Су-25",
                hitPoint = 180,
                size = new int[] { 270, 81 },
                speed = 5,
                friend = true,
                cantEscape = true
            },

            new Aircraft
            {
                aircraftType = "su27",
                aircraftName = "Су-27",
                size = new int[] { 270, 77 },
                friend = true
            },

            new Aircraft
            {
                aircraftType = "su34",
                aircraftName = "Су-34",
                hitPoint = 120,
                size = new int[] { 275, 56 },
                friend = true
            },

            new Aircraft
            {
                aircraftType = "pakfa",
                aircraftName = "Су-57",
                size = new int[] { 270, 57 },
                speed = 12,
                friend = true
            },

            new Aircraft
            {
                aircraftType = "tu160",
                aircraftName = "Ту-160",
                hitPoint = 140,
                size = new int[] { 510, 108 },
                speed = 18,
                minAltitude = minAltitudeForLargeAircraft,
                wrecksMaxSize = Constants.WRECKS_BIG,
                friend = true
            },

            new Aircraft
            {
                aircraftType = "mig19",
                aircraftName = "МиГ-19",
                size = new int[] { 270, 81 },
                friend = true
            },

            new Aircraft
            {
                aircraftType = "mig21",
                aircraftName = "МиГ-21",
                size = new int[] { 270, 62 },
                friend = true
            },

            new Aircraft
            {
                aircraftType = "mig25",
                aircraftName = "МиГ-25",
                size = new int[] { 270, 64 },
                speed = 14,
                friend = true
            },

            new Aircraft
            {
                aircraftType = "a50",
                aircraftName = "А-50",
                hitPoint = 150,
                size = new int[] { 570, 175 },
                speed = 8,
                minAltitude = minAltitudeForLargeAircraft,
                wrecksMaxSize = Constants.WRECKS_GIGANT,
                cantEscape = true,
                friend = true,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "a50rls",
                        y = 45,
                        x_left = 265,
                        x_right = 200,
                        movingType = DynamicElement.MovingType.xRotate,
                        slowRotation = true,
                        backSide = true,
                        oneWay = true,
                    },
                }
            },

            new Aircraft
            {
                aircraftType = "tu95",
                aircraftName = "Ту-95",
                hitPoint = 120,
                size = new int[] { 510, 116 },
                speed = 5,
                minAltitude = minAltitudeForLargeAircraft,
                wrecksMaxSize = Constants.WRECKS_BIG,
                cantEscape = true,
                friend = true,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "ltl_prop",
                        y = 68,
                        x_left = 118,
                        x_right = 340,
                        movingType = DynamicElement.MovingType.yRotate,
                        mirror = true
                    },
                    new DynamicElement
                    {
                        elementName = "ltl_prop",
                        y = 68,
                        x_left = 111,
                        x_right = 347,
                        startDegree = 0.5,
                        movingType = DynamicElement.MovingType.yRotate,
                        mirror = true
                    },
                    new DynamicElement
                    {
                        elementName = "ltl_prop",
                        y = 68,
                        x_left = 151,
                        x_right = 380,
                        movingType = DynamicElement.MovingType.yRotate,
                        mirror = true
                    },
                    new DynamicElement
                    {
                        elementName = "ltl_prop",
                        y = 68,
                        x_left = 158,
                        x_right = 387,
                        startDegree = 0.5,
                        movingType = DynamicElement.MovingType.yRotate,
                        mirror = true
                    },
                }
            },

            new Aircraft
            {
                aircraftType = "mig35",
                aircraftName = "МиГ-35",
                size = new int[] { 270, 72 },
                friend = true
            },

            new Aircraft
            {
                aircraftType = "su30",
                aircraftName = "Су-30",
                size = new int[] { 270, 66 },
                friend = true
            },

            new Aircraft
            {
                aircraftType = "tu22m3",
                aircraftName = "Ту-22М3",
                size = new int[] { 434, 108 },
                speed = 12,
                minAltitude = minAltitudeForLargeAircraft,
                wrecksMaxSize = Constants.WRECKS_BIG,
                friend = true
            },

            new Aircraft
            {
                aircraftType = "tu16",
                aircraftName = "Ту-16",
                size = new int[] { 430, 105 },
                minAltitude = minAltitudeForLargeAircraft,
                wrecksMaxSize = Constants.WRECKS_BIG,
                friend = true
            },

            new Aircraft
            {
                aircraftType = "tu22",
                aircraftName = "Ту-22",
                size = new int[] { 450, 98 },
                minAltitude = minAltitudeForLargeAircraft,
                wrecksMaxSize = Constants.WRECKS_BIG,
                friend = true
            },
        };

        public static List<Aircraft> helicoptersFriend = new List<Aircraft>()
        {
            new Aircraft
            {
                aircraftType = "mi28",
                aircraftName = "Ми-28",
                hitPoint = 120,
                size = new int[] { 215, 66 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                wrecksMaxSize = Constants.WRECKS_LTL,
                friend = true,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "prop_main",
                        y = 0,
                        x_left = -43,
                        x_right = 30,
                        movingType = DynamicElement.MovingType.xRotate,
                        background = true,
                    },
                    new DynamicElement
                    {
                        elementName = "x_suppl",
                        y = -7,
                        x_left = 180,
                        x_right = -16,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "mi24",
                aircraftName = "Ми-24",
                hitPoint = 120,
                size = new int[] { 210, 57 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                wrecksMaxSize = Constants.WRECKS_LTL,
                friend = true,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "prop_main",
                        y = -11,
                        x_left = -39,
                        x_right = 25,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement
                    {
                        elementName = "y_suppl",
                        y = -15,
                        x_left = 180,
                        x_right = -10,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "mi8",
                aircraftName = "Ми-8",
                size = new int[] { 220, 62 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                wrecksMaxSize = Constants.WRECKS_LTL,
                friend = true,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "prop_main",
                        y = -11,
                        x_left = -47,
                        x_right = 40,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement
                    {
                        elementName = "y_suppl",
                        y = -19,
                        x_left = 190,
                        x_right = -15,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "ka52",
                aircraftName = "Ка-52",
                hitPoint = 120,
                size = new int[] { 232, 70 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                wrecksMaxSize = Constants.WRECKS_LTL,
                friend = true,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "prop_main",
                        y = -13,
                        x_left = -25,
                        x_right = 30,
                        movingType = DynamicElement.MovingType.xRotate,
                        startDegree = 0.5,
                    },
                    new DynamicElement
                    {
                        elementName = "prop_main",
                        y = -2,
                        x_left = -25,
                        x_right = 30,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                }
            },

            new Aircraft
            {
                aircraftType = "ka27",
                aircraftName = "Ка-27",
                size = new int[] { 197, 63 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                wrecksMaxSize = Constants.WRECKS_LTL,
                friend = true,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "prop_main",
                        y = -32,
                        x_left = -30,
                        x_right = 0,
                        movingType = DynamicElement.MovingType.xRotate,
                        startDegree = 0.5,
                    },
                    new DynamicElement
                    {
                        elementName = "prop_main",
                        y = -21,
                        x_left = -30,
                        x_right = 0,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                }
            },

            new Aircraft
            {
                aircraftType = "mi10",
                aircraftName = "Ми-10",
                size = new int[] { 300, 77 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                wrecksMaxSize = Constants.WRECKS_LTL,
                friend = true,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "prop_main_big",
                        y = -20,
                        x_left = -35,
                        x_right = 70,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement
                    {
                        elementName = "t_suppl",
                        y = -15,
                        x_left = 260,
                        x_right = -15,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "mi26",
                aircraftName = "Ми-26",
                size = new int[] { 580, 146 },
                wrecksMaxSize = Constants.WRECKS_GIGANT,
                speed = 5,
                friend = true,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "mi26_prop",
                        y = -3,
                        x_left = -35,
                        x_right = 105,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement
                    {
                        elementName = "mi26_suppl",
                        y = -54,
                        x_left = 480,
                        x_right = -35,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "ka31",
                aircraftName = "Ка-31",
                size = new int[] { 200, 50 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                wrecksMaxSize = Constants.WRECKS_LTL,
                friend = true,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "prop_main",
                        y = -34,
                        x_left = -30,
                        x_right = 0,
                        movingType = DynamicElement.MovingType.xRotate,
                        startDegree = 0.5,
                    },
                    new DynamicElement
                    {
                        elementName = "prop_main",
                        y = -23,
                        x_left = -30,
                        x_right = 0,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement
                    {
                        elementName = "ka31rls",
                        y = 50,
                        x_left = 20,
                        x_right = 56,
                        movingType = DynamicElement.MovingType.xRotate,
                        slowRotation = true,
                        backSide = true,
                    },
                }
            },

            new Aircraft
            {
                aircraftType = "ka26",
                aircraftName = "Ка-26 Hoodlum",
                size = new int[] { 150, 56 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                wrecksMaxSize = Constants.WRECKS_LTL,
                friend = true,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "prop_main_ltl",
                        y = -34,
                        x_left = -10,
                        x_right = 24,
                        movingType = DynamicElement.MovingType.xRotate,
                        startDegree = 0.5,
                    },
                    new DynamicElement
                    {
                        elementName = "prop_main_ltl",
                        y = -23,
                        x_left = -10,
                        x_right = 24,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                }
            },

            new Aircraft
            {
                aircraftType = "mi38",
                aircraftName = "Ми-38",
                size = new int[] { 235, 72 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                wrecksMaxSize = Constants.WRECKS_LTL,
                friend = true,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "prop_main",
                        y = -4,
                        x_left = -37,
                        x_right = 46,
                        movingType = DynamicElement.MovingType.xRotate,
                        background = true,
                    },
                    new DynamicElement
                    {
                        elementName = "x_suppl",
                        y = -14,
                        x_left = 195,
                        x_right = -12,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "ansat",
                aircraftName = "Ансат",
                size = new int[] { 210, 61 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                wrecksMaxSize = Constants.WRECKS_LTL,
                friend = true,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "prop_main",
                        y = -22,
                        x_left = -37,
                        x_right = 22,
                        movingType = DynamicElement.MovingType.xRotate,
                        background = true,
                    },
                    new DynamicElement
                    {
                        elementName = "ltl_suppl",
                        y = 1,
                        x_left = 185,
                        x_right = -4,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "ka60",
                aircraftName = "Ка-60",
                size = new int[] { 240, 89 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                wrecksMaxSize = Constants.WRECKS_LTL,
                friend = true,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "prop_main",
                        y = 3,
                        x_left = -33,
                        x_right = 47,
                        movingType = DynamicElement.MovingType.xRotate,
                        background = true,
                    },
                    new DynamicElement
                    {
                        elementName = "f_suppl",
                        y = 40,
                        x_left = 204,
                        x_right = 12,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },
        };

        public static List<Aircraft> airliners = new List<Aircraft>()
        {
            new Aircraft
            {
                aircraftType = "a320",
                aircraftName = "Аэробус A320",
                hitPoint = 100,
                size = new int[] { 565, 173 },
                speed = 8,
                minAltitude = minAltitudeForLargeAircraft,
                wrecksMaxSize = Constants.WRECKS_BIG,
                cantEscape = true,
                airliner = true
            },

            new Aircraft
            {
                aircraftType = "boeing747",
                aircraftName = "Боинг 747",
                hitPoint = 100,
                size = new int[] { 565, 158 },
                speed = 8,
                minAltitude = minAltitudeForLargeAircraft,
                wrecksMaxSize = Constants.WRECKS_GIGANT,
                cantEscape = true,
                airliner = true
            },

            new Aircraft
            {
                aircraftType = "md11",
                aircraftName = "MD-11",
                hitPoint = 100,
                size = new int[] { 560, 157 },
                speed = 8,
                minAltitude = minAltitudeForLargeAircraft,
                wrecksMaxSize = Constants.WRECKS_BIG,
                cantEscape = true,
                airliner = true
            },

            new Aircraft
            {
                aircraftType = "atr42",
                aircraftName = "ATR 42",
                size = new int[] { 320, 110 },
                speed = 5,
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true,
                airliner = true,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "ltl_prop",
                        y = 34,
                        x_left = 92,
                        x_right = 214,
                        movingType = DynamicElement.MovingType.yRotate,
                        mirror = true
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "dhc8",
                aircraftName = "Bombardier DHC-8",
                size = new int[] { 370, 90 },
                speed = 5,
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true,
                airliner = true,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "ltl_prop",
                        y = 27,
                        x_left = 122,
                        x_right = 234,
                        movingType = DynamicElement.MovingType.yRotate,
                        mirror = true
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "ssj100",
                aircraftName = "Суперджет 100",
                size = new int[] { 355, 124 },
                speed = 8,
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true,
                airliner = true
            },

            new Aircraft
            {
                aircraftType = "boeing707",
                aircraftName = "Боинг 707",
                hitPoint = 100,
                size = new int[] { 565, 136 },
                speed = 9,
                minAltitude = minAltitudeForLargeAircraft,
                wrecksMaxSize = Constants.WRECKS_BIG,
                cantEscape = true,
                airliner = true
            },

            new Aircraft
            {
                aircraftType = "l1049",
                aircraftName = "Локхид L-1049",
                hitPoint = 60,
                size = new int[] { 414, 119 },
                speed = 6,
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true,
                airliner = true,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "ltl_prop",
                        y = 43,
                        x_left = 126,
                        x_right = 275,
                        movingType = DynamicElement.MovingType.yRotate,
                        mirror = true
                    },
                    new DynamicElement
                    {
                        elementName = "ltl_prop",
                        y = 48,
                        x_left = 119,
                        x_right = 282,
                        movingType = DynamicElement.MovingType.yRotate,
                        mirror = true,
                        startDegree = 0.5,
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "mc21",
                aircraftName = "Иркут МС-21",
                hitPoint = 100,
                size = new int[] { 560, 146 },
                speed = 8,
                minAltitude = minAltitudeForLargeAircraft,
                wrecksMaxSize = Constants.WRECKS_BIG,
                cantEscape = true,
                airliner = true
            },

            new Aircraft
            {
                aircraftType = "a380",
                aircraftName = "Аэробус A380",
                hitPoint = 120,
                size = new int[] { 621, 191 },
                speed = 8,
                minAltitude = minAltitudeForLargeAircraft,
                wrecksMaxSize = Constants.WRECKS_GIGANT,
                cantEscape = true,
                airliner = true
            },

            new Aircraft
            {
                aircraftType = "boeing777",
                aircraftName = "Боинг 777",
                hitPoint = 100,
                size = new int[] { 585, 140 },
                speed = 8,
                minAltitude = minAltitudeForLargeAircraft,
                wrecksMaxSize = Constants.WRECKS_GIGANT,
                cantEscape = true,
                airliner = true
            },

            new Aircraft {
                aircraftType = "il114",
                aircraftName = "Ил-114",
                size = new int[] { 420, 133 },
                speed = 5,
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true,
                airliner = true,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "mdl_prop",
                        y = 71,
                        x_left = 118,
                        x_right = 279,
                        movingType = DynamicElement.MovingType.yRotate,
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "boeing737",
                aircraftName = "Боинг 737",
                size = new int[] { 565, 184 },
                speed = 8,
                minAltitude = minAltitudeForLargeAircraft,
                wrecksMaxSize = Constants.WRECKS_BIG,
                cantEscape = true,
                airliner = true
            },

            new Aircraft
            {
                aircraftType = "md90",
                aircraftName = "MD 90",
                size = new int[] { 580, 111 },
                speed = 8,
                minAltitude = minAltitudeForLargeAircraft,
                wrecksMaxSize = Constants.WRECKS_BIG,
                cantEscape = true,
                airliner = true
            },

            new Aircraft
            {
                aircraftType = "dc8",
                aircraftName = "DC 8",
                size = new int[] { 580, 118 },
                speed = 8,
                minAltitude = minAltitudeForLargeAircraft,
                wrecksMaxSize = Constants.WRECKS_BIG,
                cantEscape = true,
                airliner = true
            },

            new Aircraft
            {
                aircraftType = "l1011",
                aircraftName = "Локхид L-1011",
                size = new int[] { 500, 180 },
                speed = 8,
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true,
                airliner = true
            },

            new Aircraft
            {
                aircraftType = "crj200",
                aircraftName = "Bombardier CRJ200",
                size = new int[] { 400, 89 },
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true,
                airliner = true
            },

            new Aircraft
            {
                aircraftType = "emb120",
                aircraftName = "Embraer EMB 120",
                size = new int[] { 330, 94 },
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true,
                airliner = true,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "ltl_prop",
                        y = 33,
                        x_left = 86,
                        x_right = 231,
                        movingType = DynamicElement.MovingType.yRotate,
                        mirror = true
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "concorde",
                aircraftName = "Concorde",
                size = new int[] { 475, 100 },
                speed = 18,
                minAltitude = minAltitudeForLargeAircraft,
                wrecksMaxSize = Constants.WRECKS_BIG,
                cantEscape = true,
                airliner = true,
            },

            new Aircraft
            {
                aircraftType = "tu134",
                aircraftName = "Ту-134",
                size = new int[] { 463, 108 },
                speed = 12,
                minAltitude = minAltitudeForLargeAircraft,
                wrecksMaxSize = Constants.WRECKS_BIG,
                cantEscape = true,
                airliner = true
            },

            new Aircraft
            {
                aircraftType = "tu154",
                aircraftName = "Ту-154",
                size = new int[] { 509, 111 },
                speed = 12,
                minAltitude = minAltitudeForLargeAircraft,
                wrecksMaxSize = Constants.WRECKS_BIG,
                cantEscape = true,
                airliner = true
            },
        };

        public static List<Aircraft> targetDrones = new List<Aircraft>()
        {
            new Aircraft
            {
                aircraftType = "la17mm",
                aircraftName = "самолёт-мишень Ла-17ММ",
                hitPoint = 30,
                size = new int[] { 125, 47 },
                speed = 5,
                weight = Aircraft.WeightType.Middle,
                wrecksMaxSize = Constants.WRECKS_LTL,
                cantEscape = true,
            },

            new Aircraft
            {
                aircraftType = "e95",
                aircraftName = "самолёт-мишень E-95",
                hitPoint = 2,
                size = new int[] { 60, 13 },
                speed = 3,
                weight = Aircraft.WeightType.Light,
                wrecksMaxSize = Constants.WRECKS_MICRO,
                cantEscape = true,
            },

            new Aircraft
            {
                aircraftType = "mqm36",
                aircraftName = "самолёт-мишень MQM-36A",
                hitPoint = 30,
                size = new int[] { 200, 46 },
                speed = 4,
                weight = Aircraft.WeightType.Middle,
                wrecksMaxSize = Constants.WRECKS_LTL,
                cantEscape = true,
                deadSprite = true,
                elements = new List<DynamicElement>
                {
                    new DynamicElement
                    {
                        elementName = "ltl_prop",
                        y = 0,
                        x_left = 0,
                        x_right = 188,
                        movingType = DynamicElement.MovingType.yRotate,
                        mirror = true
                    }
                }
            },

            new Aircraft
            {
                aircraftType = "dan3",
                aircraftName = "самолёт-мишень Дань",
                hitPoint = 40,
                size = new int[] { 145, 33 },
                speed = 8,
                weight = Aircraft.WeightType.Middle,
                wrecksMaxSize = Constants.WRECKS_MICRO,
                cantEscape = true,
            },

            new Aircraft
            {
                aircraftType = "d21",
                aircraftName = "самолёт-мишень D-21",
                hitPoint = 40,
                size = new int[] { 208, 41 },
                speed = 12,
                cantEscape = true,
            },
        };

        public static List<Aircraft> targetPlane = new List<Aircraft>()
        {
            new Aircraft
            {
                aircraftType = "m16k",
                aircraftName = "самолёт-мишень М-16К",
                hitPoint = 250,
                size = new int[] { 430, 105 },
                cantEscape = true,
                minAltitude = minAltitudeForLargeAircraft,
                wrecksMaxSize = Constants.WRECKS_BIG,
            },
        };

        public static List<Aircraft> targetTugs = new List<Aircraft>()
        {
            new Aircraft
            {
                aircraftType = "il28bm_77bm2",
                aircraftName = "буксировщик мишени Ил-28БМ",
                hitPoint = 120,
                size = new int[] { 706, 100 },
                speed = 9,
                cantEscape = true,
                friend = true,
                trainingTug = true,
                minAltitude = minAltitudeForLargeAircraft,
                wrecksMaxSize = Constants.WRECKS_LTL,
            },

            new Aircraft
            {
                aircraftType = "il28bm",
                aircraftName = "буксировщик мишени Ил-28БМ",
                hitPoint = 120,
                size = new int[] { 319, 100 },
                speed = 9,
                cantEscape = true,
                trainingTug = true,
                friend = true,
                wrecksMaxSize = Constants.WRECKS_BIG,
            },

            new Aircraft
            {
                aircraftType = "77bm2",
                aircraftName = "мишень 77БМ7",
                size = new int[] { 147, 42 },
                speed = 9,
                weight = Aircraft.WeightType.Middle,
                cantEscape = true,
                canPlaneForALongTime = true,
                wrecksMaxSize = Constants.WRECKS_LTL,
            },
        };

        public static List<Aircraft> suspendedTargets = new List<Aircraft>()
        {
            new Aircraft
            {
                aircraftType = "old_mig15",
                aircraftName = "списанный МиГ-15",
                hitPoint = 200,
                size = new int[] { 200, 78 },
                weight = Aircraft.WeightType.Heavy,
                cantEscape = true,
                fallLikeAStone = true,
                zeroSpeed = true,
                wrecksMaxSize = Constants.WRECKS_LTL,
                suspendedTopCorrection = 168,
                suspendedLeftCorrection = 75,
            },
            new Aircraft
            {
                aircraftType = "old_mig9",
                aircraftName = "списанный МиГ-9",
                hitPoint = 200,
                size = new int[] { 205, 68 },
                weight = Aircraft.WeightType.Heavy,
                cantEscape = true,
                fallLikeAStone = true,
                zeroSpeed = true,
                wrecksMaxSize = Constants.WRECKS_LTL,
                suspendedTopCorrection = 178,
                suspendedLeftCorrection = 75,
            },
            new Aircraft
            {
                aircraftType = "old_su11",
                aircraftName = "списанный Су-11",
                hitPoint = 200,
                size = new int[] { 205, 74 },
                weight = Aircraft.WeightType.Heavy,
                cantEscape = true,
                fallLikeAStone = true,
                zeroSpeed = true,
                wrecksMaxSize = Constants.WRECKS_LTL,
                suspendedTopCorrection = 179,
                suspendedLeftCorrection = 75,
            },
            new Aircraft
            {
                aircraftType = "old_yak25",
                aircraftName = "списанный Як-25",
                hitPoint = 200,
                size = new int[] { 205, 60 },
                weight = Aircraft.WeightType.Heavy,
                cantEscape = true,
                fallLikeAStone = true,
                zeroSpeed = true,
                wrecksMaxSize = Constants.WRECKS_MICRO,
                suspendedTopCorrection = 178,
                suspendedLeftCorrection = 75,
            },
            new Aircraft
            {
                aircraftType = "old_yak27",
                aircraftName = "списанный Як-27",
                hitPoint = 200,
                size = new int[] { 210, 57 },
                weight = Aircraft.WeightType.Heavy,
                cantEscape = true,
                fallLikeAStone = true,
                zeroSpeed = true,
                wrecksMaxSize = Constants.WRECKS_MICRO,
                suspendedTopCorrection = 180,
                suspendedLeftCorrection = 75,
            },
        };

        public static Aircraft FindEnemyAircraft(string name)
        {
            foreach (List<Aircraft> aircrafts in new List<List<Aircraft>> { aircraft, helicopters, aerostat })
                foreach (Aircraft aircraft in aircrafts)
                    if (name == aircraft.aircraftName)
                        return aircraft;

            return null;
        }

        public static Aircraft Cloud()
        {
            return new Aircraft
            {
                aircraftType = "cloud" + (Aircraft.rand.Next(1, 8)),
                size = new int[]
                {
                    Aircraft.rand.Next(Constants.CLOUD_WIDTH_MIN, Constants.CLOUD_WIDTH_MAX),
                    Aircraft.rand.Next(Constants.CLOUD_HEIGHT_MIN, Constants.CLOUD_HEIGHT_MAX)
                },
                speed = Constants.CLOUD_SPEED,
                friend = true,
                cloud = true
            };
        }
    }
}
