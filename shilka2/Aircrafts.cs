﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace shilka2
{
    class Aircrafts
    {
        public static int minAltitudeGlobal { get; set; }
        public static int minAltitudeForLargeAircraft = (int)SystemParameters.PrimaryScreenHeight / 2;
        public static int maxAltitudeForHelicopters = minAltitudeForLargeAircraft;

        public static List<AircraftsType> aircraft = new List<AircraftsType>()
        {
            new AircraftsType {
                aircraftType = "a10",
                aircraftName = "A-10 Thunderbolt",
                hitPoint = 200,
                size = new int[] { 270, 68 },
                price = 12,
                speed = 5,
                cantEscape = true
            },

            new AircraftsType {
                aircraftType = "b1",
                aircraftName = "B-1 Lancer",
                hitPoint = 100,
                size = new int[] { 510, 108 },
                price = 283,
                speed = 12
            },

            new AircraftsType {
                aircraftType = "b52",
                aircraftName = "B-52 Stratofortress",
                hitPoint = 120,
                size = new int[] { 565, 155 },
                price = 53,
                speed = 8,
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true
            },

            new AircraftsType {
                aircraftType = "f117",
                aircraftName = "F-117 Nighthawk",
                hitPoint = 50,
                size = new int[] { 270, 47 },
                price = 112
            },

            new AircraftsType {
                aircraftType = "f14",
                aircraftName = "F-14 Tomcat",
                size = new int[] { 275, 67 },
                price = 38
            },

            new AircraftsType {
                aircraftType = "f18",
                aircraftName = "F-18 Hornet",
                size = new int[] { 270, 61 },
                price = 57
            },

            new AircraftsType {
                aircraftType = "f16",
                aircraftName = "F-16 Fighting Falcon",
                size = new int[] { 270, 89 },
                price = 34
            },

            new AircraftsType {
                aircraftType = "f22",
                aircraftName = "F-22 Raptor",
                size = new int[] { 270, 73 },
                price = 146,
                speed = 14
            },

            new AircraftsType {
                aircraftType = "f15",
                aircraftName = "F-15 Eagle",
                hitPoint = 100,
                size = new int[] { 270, 62 },
                price = 29
            },

            new AircraftsType {
                aircraftType = "f4",
                aircraftName = "F-4 Fantom",
                hitPoint = 150,
                size = new int[] { 270, 64 },
                price = 3,
                speed = 8
            },

            new AircraftsType {
                aircraftType = "tornado",
                aircraftName = "Panavia Tornado",
                hitPoint = 100,
                size = new int[] { 270, 72 },
                price = 111
            },

            new AircraftsType {
                aircraftType = "predator",
                aircraftName = "MQ-1 Predator",
                hitPoint = 30,
                size = new int[] { 140, 44 },
                price = 4,
                speed = 5,
                cantEscape = true,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "ltl_prop",
                        y = -15,
                        x_left = 130,
                        x_right = -5,
                        movingType = DynamicElement.MovingType.yRotate
                    }
                }
            },

            new AircraftsType {
                aircraftType = "reaper",
                aircraftName = "MQ-9 Reaper",
                hitPoint = 50,
                size = new int[] { 161, 52 },
                price = 16,
                speed = 5,
                cantEscape = true,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "ltl_prop",
                        y = -3,
                        x_left = 143,
                        x_right = 5,
                        movingType = DynamicElement.MovingType.yRotate
                    }
                }
            },

            new AircraftsType {
                aircraftType = "f35",
                aircraftName = "F-35 Lightning II",
                size = new int[] { 270, 76 },
                price = 108
            },

            new AircraftsType {
                aircraftType = "e3",
                aircraftName = "E-3 Centry",
                hitPoint = 150,
                size = new int[] { 581, 164 },
                price = 270,
                speed = 8,
                minAltitude = minAltitudeForLargeAircraft
            },

            new AircraftsType {
                aircraftType = "eurofighter",
                aircraftName = "Eurofighter Typhoon",
                size = new int[] { 270, 77 },
                price = 123
            },

            new AircraftsType {
                aircraftType = "rafale",
                aircraftName = "Rafale",
                size = new int[] { 270, 86 },
                price = 85,
                speed = 11
            },

            new AircraftsType {
                aircraftType = "b2",
                aircraftName = "B-2 Spirit",
                hitPoint = 125,
                size = new int[] { 332, 76 },
                price = 2100,
                speed = 18
            },

            new AircraftsType {
                aircraftType = "globalhawk",
                aircraftName = "RQ-4 Global Hawk",
                hitPoint = 100,
                size = new int[] { 265, 85 },
                price = 70,
                speed = 7,
                cantEscape = true,
                minAltitude = minAltitudeForLargeAircraft
            },

            new AircraftsType {
                aircraftType = "tomahawk",
                aircraftName = "Tomahawk",
                hitPoint = 20,
                size = new int[] { 125, 29 },
                price = 2,
                speed = 5,
                cantEscape = true
            },

            new AircraftsType {
                aircraftType = "f8",
                aircraftName = "F-8 Crusader",
                size = new int[] { 270, 93 },
                price = 6,
                speed = 8
            },

            new AircraftsType {
                aircraftType = "ac130",
                aircraftName = "AC-130 Spectre",
                hitPoint = 120,
                size = new int[] { 400, 154 },
                price = 190,
                speed = 7,
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "air_prop",
                        y = 50,
                        x_left = 105,
                        x_right = 275,
                        movingType = DynamicElement.MovingType.yRotate
                    }
                }
            },

            new AircraftsType {
                aircraftType = "a6",
                aircraftName = "A-6 Intruder",
                size = new int[] { 270, 78 },
                price = 43,
                speed = 7
            },

            new AircraftsType {
                aircraftType = "f111",
                aircraftName = "F-111",
                size = new int[] { 285, 59 },
                price = 72
            },

            new AircraftsType {
                aircraftType = "f5",
                aircraftName = "F-5 Tiger",
                size = new int[] { 270, 58 },
                price = 2
            },

            new AircraftsType {
                aircraftType = "scalp",
                aircraftName = "SCALP",
                hitPoint = 20,
                size = new int[] { 115, 23 },
                price = 2,
                speed = 5,
                cantEscape = true
            },

            new AircraftsType {
                aircraftType = "ea6",
                aircraftName = "EA-6 Prowler",
                size = new int[] { 285, 66 },
                price = 52,
                speed = 7
            },

            new AircraftsType {
                aircraftType = "hawkeye",
                aircraftName = "E-2 Hawkeye",
                hitPoint = 100,
                size = new int[] { 324, 96 },
                price = 80,
                speed = 8,
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "air_prop",
                        y = 13,
                        x_left = 75,
                        x_right = 225,
                        movingType = DynamicElement.MovingType.yRotate
                    }
                }
            },

            new AircraftsType {
                aircraftType = "rc135",
                aircraftName = "RC-135",
                hitPoint = 120,
                size = new int[] { 528, 185 },
                price = 90,
                speed = 8,
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true
            },

            new AircraftsType {
                aircraftType = "u2",
                aircraftName = "U-2",
                size = new int[] { 355, 103 },
                price = 6,
                speed = 8,
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true
            },

            new AircraftsType {
                aircraftType = "sr71",
                aircraftName = "SR-71 Blackbird",
                size = new int[] { 450, 71 },
                price = 34,
                speed = 18,
                minAltitude = minAltitudeForLargeAircraft
            },

            new AircraftsType {
                aircraftType = "harrier",
                aircraftName = "BAE Sea Harrier",
                size = new int[] { 275, 81 },
                price = 24,
                speed = 7
            },

            new AircraftsType {
                aircraftType = "cessna",
                aircraftName = "Cessna 172",
                hitPoint = 30,
                size = new int[] { 170, 61 },
                speed = 6,
                price = 0.3,
                cantEscape = true,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "ltl_prop",
                        y = 2,
                        x_left = -6,
                        x_right = 164,
                        movingType = DynamicElement.MovingType.yRotate,
                        mirror = true
                    }
                }
            },

            new AircraftsType {
                aircraftType = "hunter",
                aircraftName = "RQ-5 Hunter",
                hitPoint = 30,
                size = new int[] { 172, 49 },
                speed = 5,
                price = 2,
                cantEscape = true,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "ltl_prop",
                        y = -10,
                        x_left = 105,
                        x_right = 53,
                        movingType = DynamicElement.MovingType.yRotate,
                        background = true
                    }
                }
            },

            new AircraftsType {
                aircraftType = "r99",
                aircraftName = "Embraer R-99",
                hitPoint = 100,
                size = new int[] { 350, 88 },
                price = 80,
                speed = 8,
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true
            },

            new AircraftsType {
                aircraftType = "m2000",
                aircraftName = "Mirage 2000",
                size = new int[] { 270, 79 },
                price = 25
            },

            new AircraftsType {
                aircraftType = "m2000ed",
                aircraftName = "Mirage 2000ED",
                size = new int[] { 270, 75 },
                price = 35
            },

            new AircraftsType {
                aircraftType = "jassm",
                aircraftName = "JASSM",
                hitPoint = 20,
                size = new int[] { 108, 25 },
                price = 0.85,
                speed = 5,
                cantEscape = true
            },

            new AircraftsType {
                aircraftType = "gripen",
                aircraftName = "Saab Gripen",
                size = new int[] { 247, 72 },
                price = 14
            },

            new AircraftsType {
                aircraftType = "boeing737aewc",
                aircraftName = "Боинг 737 AEW&C",
                size = new int[] { 565, 187 },
                price = 490,
                speed = 7,
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true
            },

            new AircraftsType {
                aircraftType = "c5",
                aircraftName = "C-5 Galaxy",
                hitPoint = 150,
                size = new int[] { 570, 152 },
                speed = 7,
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true
            },
        };

        public static List<AircraftsType> helicopters = new List<AircraftsType>()
        {
            new AircraftsType {
                aircraftType = "ah64",
                aircraftName = "AH-64 Apache",
                hitPoint = 120,
                size = new int[] { 209, 63 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                price = 61,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "prop_main",
                        y = -8,
                        x_left = -41,
                        x_right = 27,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement {
                        elementName = "x_suppl",
                        y = -5,
                        x_left = 170,
                        x_right = -10,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new AircraftsType {
                aircraftType = "ah1",
                aircraftName = "AH-1 Cobra",
                hitPoint = 100,
                size = new int[] { 209, 54 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                price = 11,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "prop_main",
                        y = -22,
                        x_left = -41,
                        x_right = 27,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement {
                        elementName = "i_suppl",
                        y = -9,
                        x_left = 175,
                        x_right = -12,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new AircraftsType {
                aircraftType = "uh60",
                aircraftName = "UH-60 Black Hawk",
                size = new int[] { 210, 65 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                price = 25,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "prop_main",
                        y = -5,
                        x_left = -48,
                        x_right = 36,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement {
                        elementName = "t_suppl",
                        y = -11,
                        x_left = 175,
                        x_right = -12,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new AircraftsType {
                aircraftType = "uh1",
                aircraftName = "UH-1 Iroquois",
                size = new int[] { 210, 65 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                price = 5,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "prop_main",
                        y = -13,
                        x_left = -39,
                        x_right = 25,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement {
                        elementName = "i_suppl",
                        y = -11,
                        x_left = 175,
                        x_right = -12,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new AircraftsType {
                aircraftType = "ch47",
                aircraftName = "CH-47 Chinook",
                size = new int[] { 270, 101 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                price = 30,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "prop_main",
                        y = 6,
                        x_left = -68,
                        x_right = 110,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement {
                        elementName = "prop_main",
                        y = -20,
                        x_left = 125,
                        x_right = -78,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                }
            },

            new AircraftsType {
                aircraftType = "v22",
                aircraftName = "V-22 Ospray",
                size = new int[] { 282, 103 },
                speed = 7,
                maxAltitude = maxAltitudeForHelicopters,
                price = 116,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "prop_main",
                        y = -18,
                        x_left = -5,
                        x_right = 60,
                        movingType = DynamicElement.MovingType.xRotate,
                        startDegree = 0.5,
                    },
                }
            },

            new AircraftsType {
                aircraftType = "tiger",
                aircraftName = "Eurocopter Tiger HAC",
                size = new int[] { 209, 76 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                price = 39,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "prop_main",
                        y = -3,
                        x_left = -35,
                        x_right = 20,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement {
                        elementName = "y_suppl",
                        y = 5,
                        x_left = 170,
                        x_right = -7,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new AircraftsType {
                aircraftType = "drone",
                aircraftName = "дрон-разведчик (тип 1)",
                hitPoint = 1,
                size = new int[] { 26, 9 },
                speed = 3,
                maxAltitude = maxAltitudeForHelicopters,
                price = 0.01,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "micro_prop",
                        y = -5,
                        x_left = -6,
                        x_right = -6,
                        movingType = DynamicElement.MovingType.xRotate,
                        startDegree = 0.5,
                    },
                    new DynamicElement {
                        elementName = "micro_prop",
                        y = -5,
                        x_left = 15,
                        x_right = 15,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                }
            },

            new AircraftsType {
                aircraftType = "gazelle",
                aircraftName = "Aerospatiale Gazelle",
                hitPoint = 60,
                size = new int[] { 185, 64 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                price = 0.5,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "prop_main",
                        y = -3,
                        x_left = -53,
                        x_right = 12,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement {
                        elementName = "f_suppl",
                        y = 19,
                        x_left = 155,
                        x_right = 7,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new AircraftsType {
                aircraftType = "comanche",
                aircraftName = "RAH-66 Comanche",
                size = new int[] { 210, 61 },
                speed = 6,
                maxAltitude = maxAltitudeForHelicopters,
                price = 100,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "prop_main",
                        y = -10,
                        x_left = -29,
                        x_right = 12,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement {
                        elementName = "f_suppl",
                        y = 26,
                        x_left = 175,
                        x_right = 12,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new AircraftsType {
                aircraftType = "oh1",
                aircraftName = "OH-1 Ninja",
                size = new int[] { 205, 69 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                price = 24,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "prop_main",
                        y = -10,
                        x_left = -29,
                        x_right = 12,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement {
                        elementName = "f_suppl",
                        y = 34,
                        x_left = 172,
                        x_right = 10,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new AircraftsType {
                aircraftType = "mangusta",
                aircraftName = "T-129 Mangusta",
                hitPoint = 100,
                size = new int[] { 215, 66 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                price = 52,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "prop_main",
                        y = -14,
                        x_left = -35,
                        x_right = 22,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement {
                        elementName = "i_suppl",
                        y = -14,
                        x_left = 179,
                        x_right = -4,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new AircraftsType {
                aircraftType = "puma",
                aircraftName = "Aerospatiale Puma",
                size = new int[] { 215, 58 },
                price = 15,
                speed = 5,
                minAltitude = maxAltitudeForHelicopters,
                cantEscape = true,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "prop_main",
                        y = -19,
                        x_left = -52,
                        x_right = 40,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement {
                        elementName = "t_suppl",
                        y = -16,
                        x_left = 177,
                        x_right = -11,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new AircraftsType {
                aircraftType = "mh53",
                aircraftName = "Сикорский MH-53",
                hitPoint = 100,
                size = new int[] { 375, 84 },
                price = 53,
                speed = 5,
                minAltitude = maxAltitudeForHelicopters,
                cantEscape = true,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "prop_main_big",
                        y = 5,
                        x_left = 40,
                        x_right = 69,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement {
                        elementName = "t_suppl",
                        y = -16,
                        x_left = 327,
                        x_right = -11,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new AircraftsType {
                aircraftType = "as565",
                aircraftName = "Eurocopter AS565",
                size = new int[] { 199, 70 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                price = 10,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "prop_main",
                        y = -4,
                        x_left = -33,
                        x_right = 3,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement {
                        elementName = "f_suppl",
                        y = 34,
                        x_left = 167,
                        x_right = 10,
                        movingType = DynamicElement.MovingType.zRotate,
                        background = true
                    }
                }
            },

            new AircraftsType {
                aircraftType = "drone2",
                aircraftName = "дрон-разведчик (тип 2)",
                hitPoint = 1,
                size = new int[] { 30, 17 },
                speed = 3,
                maxAltitude = maxAltitudeForHelicopters,
                price = 0.01,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "micro_prop",
                        y = -2,
                        x_left = -6,
                        x_right = -6,
                        movingType = DynamicElement.MovingType.xRotate,
                        startDegree = 0.5,
                    },
                    new DynamicElement {
                        elementName = "micro_prop",
                        y = -2,
                        x_left = 19,
                        x_right = 19,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                }
            },

            new AircraftsType {
                aircraftType = "oh58d",
                aircraftName = "Белл OH-58D",
                size = new int[] { 209, 83 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                price = 11,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "prop_main",
                        y = 5,
                        x_left = -43,
                        x_right = 29,
                        movingType = DynamicElement.MovingType.xRotate,
                        background = true
                    },
                    new DynamicElement {
                        elementName = "ltl_suppl",
                        y = 24,
                        x_left = 165,
                        x_right = 14,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new AircraftsType {
                aircraftType = "rooivalk",
                aircraftName = "Denel AH-2 Rooivalk",
                hitPoint = 100,
                size = new int[] { 265, 74 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                price = 40,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "prop_main",
                        y = -10,
                        x_left = -20,
                        x_right = 58,
                        movingType = DynamicElement.MovingType.xRotate,
                        background = true
                    },
                    new DynamicElement {
                        elementName = "five_suppl",
                        y = -2,
                        x_left = 205,
                        x_right = 10,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new AircraftsType {
                aircraftType = "ah6",
                aircraftName = "Боинг AH-6",
                size = new int[] { 134, 58 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                price = 2,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "prop_main_ltl",
                        y = -12,
                        x_left = -20,
                        x_right = 20,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement {
                        elementName = "ltl_suppl",
                        y = 7,
                        x_left = 110,
                        x_right = -3,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new AircraftsType {
                aircraftType = "drone3",
                aircraftName = "дрон-разведчик (тип 3)",
                hitPoint = 1,
                size = new int[] { 32, 20 },
                speed = 3,
                maxAltitude = maxAltitudeForHelicopters,
                price = 0.01,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "micro_prop",
                        y = -4,
                        x_left = -5,
                        x_right = -6,
                        movingType = DynamicElement.MovingType.xRotate,
                        startDegree = 0.5,
                    },
                    new DynamicElement {
                        elementName = "micro_prop",
                        y = -4,
                        x_left = 20,
                        x_right = 19,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                }
            },

            new AircraftsType {
                aircraftType = "drone4",
                aircraftName = "дрон-разведчик (тип 4)",
                hitPoint = 1,
                size = new int[] { 75, 19 },
                speed = 3,
                maxAltitude = maxAltitudeForHelicopters,
                price = 0.01,
                elements = new List<DynamicElement> {
                    new DynamicElement {
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

            new AircraftsType {
                aircraftType = "tiger-hap",
                aircraftName = "Eurocopter Tiger HAP",
                size = new int[] { 222, 76 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                price = 28,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "prop_main",
                        y = -3,
                        x_left = -21,
                        x_right = 15,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement {
                        elementName = "y_suppl",
                        y = 4,
                        x_left = 181,
                        x_right = -1,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },
        };

        public static List<AircraftsType> aircraftFriend = new List<AircraftsType>()
        {
            new AircraftsType {
                aircraftType = "mig23",
                aircraftName = "МиГ-23",
                size = new int[] { 270, 71 },
                friend = true
            },

            new AircraftsType {
                aircraftType = "mig29",
                aircraftName = "МиГ-29",
                size = new int[] { 270, 65 },
                friend = true
            },

            new AircraftsType {
                aircraftType = "mig31",
                aircraftName = "МиГ-31",
                size = new int[] { 270, 63 },
                speed = 14,
                friend = true
            },

            new AircraftsType {
                aircraftType = "su17",
                aircraftName = "Су-17",
                size = new int[] { 270, 61 },
                speed = 5,
                friend = true
            },

            new AircraftsType {
                aircraftType = "su24",
                aircraftName = "Су-24",
                size = new int[] { 270, 67 },
                speed = 8,
                friend = true
            },

            new AircraftsType {
                aircraftType = "su25",
                aircraftName = "Су-25",
                hitPoint = 180,
                size = new int[] { 270, 81 },
                speed = 5,
                friend = true,
                cantEscape = true
            },

            new AircraftsType {
                aircraftType = "su27",
                aircraftName = "Су-27",
                size = new int[] { 270, 77 },
                friend = true
            },

            new AircraftsType {
                aircraftType = "su34",
                aircraftName = "Су-34",
                hitPoint = 120,
                size = new int[] { 275, 56 },
                friend = true
            },

            new AircraftsType {
                aircraftType = "pakfa",
                aircraftName = "Су-57",
                size = new int[] { 270, 57 },
                speed = 12,
                friend = true
            },

            new AircraftsType {
                aircraftType = "tu160",
                aircraftName = "Ту-160",
                hitPoint = 140,
                size = new int[] { 510, 108 },
                speed = 18,
                minAltitude = minAltitudeForLargeAircraft,
                friend = true
            },

            new AircraftsType {
                aircraftType = "mig19",
                aircraftName = "МиГ-19",
                size = new int[] { 270, 81 },
                friend = true
            },

            new AircraftsType {
                aircraftType = "mig21",
                aircraftName = "МиГ-21",
                size = new int[] { 270, 62 },
                friend = true
            },

            new AircraftsType {
                aircraftType = "mig25",
                aircraftName = "МиГ-25",
                size = new int[] { 270, 64 },
                speed = 14,
                friend = true
            },

            new AircraftsType {
                aircraftType = "a50",
                aircraftName = "А-50",
                hitPoint = 150,
                size = new int[] { 570, 175 },
                speed = 8,
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true,
                friend = true
            },

            new AircraftsType {
                aircraftType = "tu95",
                aircraftName = "Ту-95",
                hitPoint = 120,
                size = new int[] { 510, 116 },
                speed = 5,
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true,
                friend = true,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "ltl_prop",
                        y = 68,
                        x_left = 118,
                        x_right = 340,
                        movingType = DynamicElement.MovingType.yRotate,
                        mirror = true
                    },
                    new DynamicElement {
                        elementName = "ltl_prop",
                        y = 68,
                        x_left = 111,
                        x_right = 347,
                        startDegree = 0.5,
                        movingType = DynamicElement.MovingType.yRotate,
                        mirror = true
                    },
                    new DynamicElement {
                        elementName = "ltl_prop",
                        y = 68,
                        x_left = 151,
                        x_right = 380,
                        movingType = DynamicElement.MovingType.yRotate,
                        mirror = true
                    },
                    new DynamicElement {
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

            new AircraftsType {
                aircraftType = "mig35",
                aircraftName = "МиГ-35",
                size = new int[] { 270, 72 },
                friend = true
            },

            new AircraftsType {
                aircraftType = "su30",
                aircraftName = "Су-30",
                size = new int[] { 270, 66 },
                friend = true
            },

            new AircraftsType {
                aircraftType = "tu22m3",
                aircraftName = "Ту-22М3",
                size = new int[] { 434, 108 },
                friend = true
            },
        };

        public static List<AircraftsType> helicoptersFriend = new List<AircraftsType>()
        {
            new AircraftsType {
                aircraftType = "mi28",
                aircraftName = "Ми-28",
                hitPoint = 120,
                size = new int[] { 209, 62 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                friend = true,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "prop_main",
                        y = -7,
                        x_left = -39,
                        x_right = 25,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement {
                        elementName = "x_suppl",
                        y = -11,
                        x_left = 165,
                        x_right = -8,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new AircraftsType {
                aircraftType = "mi24",
                aircraftName = "Ми-24",
                hitPoint = 120,
                size = new int[] { 210, 57 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                friend = true,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "prop_main",
                        y = -11,
                        x_left = -39,
                        x_right = 25,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement {
                        elementName = "y_suppl",
                        y = -15,
                        x_left = 180,
                        x_right = -10,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new AircraftsType {
                aircraftType = "mi8",
                aircraftName = "Ми-8",
                size = new int[] { 220, 62 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                friend = true,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "prop_main",
                        y = -11,
                        x_left = -47,
                        x_right = 40,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement {
                        elementName = "y_suppl",
                        y = -19,
                        x_left = 190,
                        x_right = -15,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new AircraftsType {
                aircraftType = "ka52",
                aircraftName = "Ка-52",
                hitPoint = 120,
                size = new int[] { 232, 70 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                friend = true,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "prop_main",
                        y = -13,
                        x_left = -25,
                        x_right = 30,
                        movingType = DynamicElement.MovingType.xRotate,
                        startDegree = 0.5,
                    },
                    new DynamicElement {
                        elementName = "prop_main",
                        y = -2,
                        x_left = -25,
                        x_right = 30,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                }
            },

            new AircraftsType {
                aircraftType = "ka27",
                aircraftName = "Ка-27",
                size = new int[] { 197, 63 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                friend = true,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "prop_main",
                        y = -32,
                        x_left = -30,
                        x_right = 0,
                        movingType = DynamicElement.MovingType.xRotate,
                        startDegree = 0.5,
                    },
                    new DynamicElement {
                        elementName = "prop_main",
                        y = -21,
                        x_left = -30,
                        x_right = 0,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                }
            },

            new AircraftsType {
                aircraftType = "mi10",
                aircraftName = "Ми-10",
                size = new int[] { 300, 77 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                friend = true,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "prop_main_big",
                        y = -20,
                        x_left = -35,
                        x_right = 70,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement {
                        elementName = "t_suppl",
                        y = -15,
                        x_left = 260,
                        x_right = -15,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new AircraftsType {
                aircraftType = "mi26",
                aircraftName = "Ми-26",
                size = new int[] { 580, 146 },
                speed = 5,
                friend = true,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "mi26_prop",
                        y = -3,
                        x_left = -35,
                        x_right = 105,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement {
                        elementName = "mi26_suppl",
                        y = -54,
                        x_left = 480,
                        x_right = -35,
                        movingType = DynamicElement.MovingType.zRotate
                    }
                }
            },

            new AircraftsType {
                aircraftType = "ka31",
                aircraftName = "Ка-31",
                size = new int[] { 200, 50 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                friend = true,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "prop_main",
                        y = -34,
                        x_left = -30,
                        x_right = 0,
                        movingType = DynamicElement.MovingType.xRotate,
                        startDegree = 0.5,
                    },
                    new DynamicElement {
                        elementName = "prop_main",
                        y = -23,
                        x_left = -30,
                        x_right = 0,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                    new DynamicElement {
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

            new AircraftsType {
                aircraftType = "ka26",
                aircraftName = "Ка-26",
                size = new int[] { 150, 56 },
                speed = 5,
                maxAltitude = maxAltitudeForHelicopters,
                friend = true,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "prop_main_ltl",
                        y = -34,
                        x_left = -10,
                        x_right = 24,
                        movingType = DynamicElement.MovingType.xRotate,
                        startDegree = 0.5,
                    },
                    new DynamicElement {
                        elementName = "prop_main_ltl",
                        y = -23,
                        x_left = -10,
                        x_right = 24,
                        movingType = DynamicElement.MovingType.xRotate,
                    },
                }
            },
        };

        public static List<AircraftsType> airliners = new List<AircraftsType>()
        {
            new AircraftsType {
                aircraftType = "a320",
                aircraftName = "Аэробус А320",
                hitPoint = 100,
                size = new int[] { 565, 173 },
                speed = 8,
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true,
                airliner = true
            },

            new AircraftsType {
                aircraftType = "boeing747",
                aircraftName = "Боинг 747",
                hitPoint = 100,
                size = new int[] { 565, 158 },
                speed = 8,
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true,
                airliner = true
            },

            new AircraftsType {
                aircraftType = "md11",
                aircraftName = "MD-11",
                hitPoint = 100,
                size = new int[] { 560, 157 },
                speed = 8,
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true,
                airliner = true
            },

            new AircraftsType {
                aircraftType = "atr42",
                aircraftName = "ATR 42",
                size = new int[] { 320, 110 },
                speed = 5,
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true,
                airliner = true,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "ltl_prop",
                        y = 34,
                        x_left = 95,
                        x_right = 212,
                        movingType = DynamicElement.MovingType.yRotate,
                        mirror = true
                    }
                }
            },

            new AircraftsType {
                aircraftType = "dhc8",
                aircraftName = "Bombardier DHC-8",
                size = new int[] { 370, 90 },
                speed = 5,
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true,
                airliner = true,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "ltl_prop",
                        y = 27,
                        x_left = 122,
                        x_right = 234,
                        movingType = DynamicElement.MovingType.yRotate,
                        mirror = true
                    }
                }
            },

            new AircraftsType {
                aircraftType = "ssj100",
                aircraftName = "Суперджет 100",
                size = new int[] { 355, 124 },
                speed = 8,
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true,
                airliner = true
            },

            new AircraftsType {
                aircraftType = "boeing707",
                aircraftName = "Боинг 707",
                hitPoint = 100,
                size = new int[] { 470, 116 },
                speed = 9,
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true,
                airliner = true
            },

            new AircraftsType {
                aircraftType = "l1049",
                aircraftName = "Локхид L-1049",
                hitPoint = 60,
                size = new int[] { 414, 119 },
                speed = 6,
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true,
                airliner = true,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "ltl_prop",
                        y = 43,
                        x_left = 126,
                        x_right = 275,
                        movingType = DynamicElement.MovingType.yRotate,
                        mirror = true
                    },
                    new DynamicElement {
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

            new AircraftsType {
                aircraftType = "mc21",
                aircraftName = "Иркут МС-21",
                hitPoint = 100,
                size = new int[] { 560, 154 },
                speed = 8,
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true,
                airliner = true
            },

            new AircraftsType {
                aircraftType = "a380",
                aircraftName = "Аэробус А380",
                hitPoint = 120,
                size = new int[] { 621, 191 },
                speed = 8,
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true,
                airliner = true
            },

            new AircraftsType {
                aircraftType = "boeing777",
                aircraftName = "Боинг 777",
                hitPoint = 100,
                size = new int[] { 585, 140 },
                speed = 8,
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true,
                airliner = true
            },

            new AircraftsType {
                aircraftType = "il114",
                aircraftName = "Ил-114",
                size = new int[] { 420, 133 },
                speed = 5,
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true,
                airliner = true,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "air_prop",
                        y = 62,
                        x_left = 118,
                        x_right = 279,
                        movingType = DynamicElement.MovingType.yRotate,
                    }
                }
            },

            new AircraftsType {
                aircraftType = "boeing737",
                aircraftName = "Боинг 737",
                size = new int[] { 565, 184 },
                speed = 8,
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true,
                airliner = true
            },

            new AircraftsType {
                aircraftType = "md90",
                aircraftName = "MD 90",
                size = new int[] { 580, 111 },
                speed = 8,
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true,
                airliner = true
            },

            new AircraftsType {
                aircraftType = "dc8",
                aircraftName = "DC 8",
                size = new int[] { 580, 118 },
                speed = 8,
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true,
                airliner = true
            },

            new AircraftsType {
                aircraftType = "l1011",
                aircraftName = "Локхид L-1011",
                size = new int[] { 500, 180 },
                speed = 8,
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true,
                airliner = true
            },

            new AircraftsType {
                aircraftType = "crj200",
                aircraftName = "Bombardier CRJ200",
                size = new int[] { 400, 89 },
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true,
                airliner = true
            },

            new AircraftsType {
                aircraftType = "emb120",
                aircraftName = "Embraer EMB 120",
                size = new int[] { 330, 94 },
                minAltitude = minAltitudeForLargeAircraft,
                cantEscape = true,
                airliner = true,
                elements = new List<DynamicElement> {
                    new DynamicElement {
                        elementName = "ltl_prop",
                        y = 33,
                        x_left = 86,
                        x_right = 231,
                        movingType = DynamicElement.MovingType.yRotate,
                        mirror = true
                    }
                }
            },
        };
    }
}
