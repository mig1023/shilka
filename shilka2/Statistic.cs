using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Media;

namespace shilka2
{
    class Statistic
    {
        static string statisticFileName = Constants.STATISTIC_FILE_NAME;
        public static int statisticGridMargins = 120;
        public static double aircraftAveragePrice = 0;

        public static int statisticShellsFired = 0;
        public static int staticticInTarget = 0;
        public static int statisticAllAircraft = 0;
        public static double statisticPriceOfAllAircrafts = 0;
        public static int staticticAircraftShutdown = 0;
        public static int statisticHasGone = 0;
        public static int statisticDamaged = 0;
        public static int statisticFriendDamage = 0;
        public static int statisticAirlinerDamage = 0;
        public static double statisticAmountOfDamage = 0;

        public static double statisticLastDamagePrice;
        public static bool statisticShutdownFlag = false;
        public static bool seriousDamage = false;
        public static string statisticLastDamageType;
        public static string statisticLastDamageFriend;
        public static string statisticLastDamageAirliner;
        public static string statisticLastHasGone;

        public static int gameTimeSec = 0;
        public static int gameBadWeatherSec = 0;
        public static int shootingTimeSec = 0;
        public static int shootingNumber = 0;

        public static List<string> statisticScripts; 

        public enum statisticAircraftType { downed, damaged };

        public static Dictionary<string, int> downedAircrafts = new Dictionary<string, int>();
        public static Dictionary<string, int> damagedAircrafts = new Dictionary<string, int>();

        static void Calc(out double baseForPercent, out int shutdownPercent, out int damagedPercent,
            out int statisticWithoutDamage, out double chance, out int inTargetPercent, out int shellsForShutdown)
        {
            baseForPercent = (
                (statisticHasGone > 0 || staticticAircraftShutdown > 0) ? (100 / (double)(statisticHasGone + staticticAircraftShutdown)) : 100
            );

            shutdownPercent = (int)(staticticAircraftShutdown * baseForPercent);
            damagedPercent = (int)(statisticDamaged * baseForPercent);
            statisticWithoutDamage = (int)((statisticHasGone - statisticDamaged) * baseForPercent);
            inTargetPercent = ((statisticShellsFired > 0) ? staticticInTarget * 100 / statisticShellsFired : 0);

            chance = (double)statisticPriceOfAllAircrafts / (statisticAllAircraft * aircraftAveragePrice);

            if (double.IsNaN(chance))
                chance = 0;

            shellsForShutdown = (staticticAircraftShutdown > 0 ? (int)statisticShellsFired / staticticAircraftShutdown : 0);
        }

        public static double GetAveragePrice()
        {
            double AverageSum = 0;

            foreach (Aircraft aircraft in Aircrafts.aircraft)
                AverageSum += aircraft.price;

            foreach (Aircraft aircraft in Aircrafts.helicopters)
                AverageSum += aircraft.price;

            return (AverageSum / (Aircrafts.aircraft.Count + Aircrafts.helicopters.Count));
        }

        public static void Clean()
        {
            statisticShellsFired = 0;
            staticticInTarget = 0;
            statisticAllAircraft = 0;
            statisticPriceOfAllAircrafts = 0;
            staticticAircraftShutdown = 0;
            statisticHasGone = 0;
            statisticDamaged = 0;
            statisticFriendDamage = 0;
            statisticAirlinerDamage = 0;
            statisticAmountOfDamage = 0;
            statisticLastDamagePrice = 0;
            statisticShutdownFlag = false;
            statisticLastDamageType = String.Empty;
            statisticLastDamageFriend = String.Empty;
            statisticLastDamageAirliner = String.Empty;
            statisticLastHasGone = String.Empty;
            gameTimeSec = 0;
            gameBadWeatherSec = 0;
            shootingTimeSec = 0;
            shootingNumber = 0;
            downedAircrafts.Clear();
            damagedAircrafts.Clear();
    }

        public static void Save(string player)
        {
            int shutdownPercent, damagedPercent, statisticWithoutDamage, inTargetPercent, shellsForShutdown;
            double chance, baseForPercent;

            Calc(out baseForPercent, out shutdownPercent, out damagedPercent, out statisticWithoutDamage,
                out chance, out inTargetPercent, out shellsForShutdown);

            double statisticAmountOfDamageRound = double.Parse(string.Format("{0:f2}", statisticAmountOfDamage));

            string downedAircraftsList = String.Empty;
            string damagedAircraftsList = String.Empty;

            if (downedAircrafts.Count > 0)
            {
                foreach (var aircraft in downedAircrafts.OrderByDescending(aircraft => aircraft.Value))
                    downedAircraftsList += aircraft.Key + "=" + aircraft.Value + ",";

                downedAircraftsList = downedAircraftsList.Substring(0, downedAircraftsList.Length - 1);
            }
            else
                downedAircraftsList = " ";

            if (damagedAircrafts.Count > 0)
            {
                foreach (var aircraft in damagedAircrafts.OrderByDescending(aircraft => aircraft.Value))
                    damagedAircraftsList += aircraft.Key + "=" + aircraft.Value + ",";

                damagedAircraftsList = damagedAircraftsList.Substring(0, damagedAircraftsList.Length - 1);
            }
            else
                damagedAircraftsList = " ";

            string stat = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}|{14}|{15}|{16}|{17}|{18}|{19}|{20}\n",
                  player, Shilka.currentScript, statisticShellsFired, staticticInTarget, staticticAircraftShutdown,
                  inTargetPercent, shutdownPercent, statisticDamaged, damagedPercent, statisticHasGone,
                  statisticWithoutDamage, statisticAmountOfDamageRound, statisticFriendDamage, statisticAirlinerDamage,
                  chance, gameTimeSec, gameBadWeatherSec, shootingTimeSec, shootingNumber, downedAircraftsList, damagedAircraftsList
            );

            File.AppendAllText(Constants.STATISTIC_FILE_NAME, stat);
        }

        public static List<StatTable> Load()
        {
            if (!File.Exists(statisticFileName))
            {
                List<StatTable> emptyResult = new List<StatTable>(0);
                return emptyResult;
            }

            string statisticText = File.ReadAllText(statisticFileName);
            string[] statisticLines = statisticText.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            List<StatTable> result = new List<StatTable>(statisticLines.Length);

            statisticScripts = new List<string>();

            foreach (string statLine in statisticLines)
            {
                string[] stat = statLine.Split('|');

                Scripts.scriptsNames scriptFullName = (Scripts.scriptsNames)Enum.Parse(typeof(Scripts.scriptsNames), stat[1]);

                ImageSource flagSource = null;

                if (scriptFullName != Scripts.scriptsNames.noScript)
                    flagSource = Aircraft.ImageFromResources(Scripts.ScriptFlagName(scriptFullName), Aircraft.ImageType.Interface);
                statisticScripts.Add(Scripts.scriptsRuNames[scriptFullName.ToString()]);

                result.Add(new StatTable(
                    stat[0], stat[1], stat[2], stat[3], stat[4], stat[5], stat[6], stat[7],
                    stat[8], stat[9], stat[10], stat[11], stat[12], stat[13], stat[14],
                    flagSource, stat[15], stat[16], stat[17], stat[18], stat[19], stat[20]
                ));
            }

            return result;
        }

        public static string HumanReadableSumm(double statisticAmountOfDamage)
        {
            if (statisticAmountOfDamage < 1000)
                return string.Format("{0:f2}", statisticAmountOfDamage) + " млн $";
            else if (statisticAmountOfDamage < 1000000)
                return string.Format("{0:f2}", (double)statisticAmountOfDamage / 1000) + " млрд $";
            else
                return string.Format("{0:f2}", (double)statisticAmountOfDamage / 1000000) + " трлн $";
        }

        public static void AircraftToStatistic(string aircraft, statisticAircraftType type)
        {
            if (type == statisticAircraftType.downed)
            {
                if (downedAircrafts.ContainsKey(aircraft))
                    downedAircrafts[aircraft] += 1;
                else
                    downedAircrafts.Add(aircraft, 1);
            }
            else
            {
                if (damagedAircrafts.ContainsKey(aircraft))
                    damagedAircrafts[aircraft] += 1;
                else
                    damagedAircrafts.Add(aircraft, 1);
            }
        }

        public static void Show(object obj, ElapsedEventArgs e)
        {
            if (Shilka.training)
                ShowTraining(obj, e);
            else
                ShowGame(obj, e);
        }

        public static void ShowGame(object obj, ElapsedEventArgs e)
        {
            string stat = String.Empty;

            int shutdownPercent, damagedPercent, statisticWithoutDamage, inTargetPercent, shellsForShutdown;
            double chance, baseForPercent;

            Calc(out baseForPercent, out shutdownPercent, out damagedPercent, out statisticWithoutDamage,
                out chance, out inTargetPercent, out shellsForShutdown);

            if (statisticShellsFired > 0)
            {
                stat += String.Format("выстрелов: {0}", statisticShellsFired);

                if (staticticAircraftShutdown > 0)
                    stat += String.Format(" ( {0} выстр./сбитый )", shellsForShutdown);

                stat += "\n";
            }

            if (staticticInTarget > 0)
                stat += String.Format("попаданий: {0} ( {1}% )\n", staticticInTarget, inTargetPercent);

            if (staticticAircraftShutdown > 0)
                stat += String.Format("сбито: {0} ( {1}% )", staticticAircraftShutdown, shutdownPercent);

            if (statisticDamaged > 0)
                stat += String.Format("{0}повреждено: {1} ( {2}% )\n",
                    (staticticAircraftShutdown == 0 ? String.Empty : " +"), statisticDamaged, damagedPercent);
            else if (staticticAircraftShutdown > 0)
                stat += "\n";

            if (statisticHasGone > 0)
            {
                int hasGonePercent = (int)(statisticHasGone * baseForPercent);

                if ((hasGonePercent == 0) && (statisticHasGone > 0))
                    hasGonePercent = 1;

                stat += String.Format("упущено: {0} ( {1}%, последний {2} )",
                    statisticHasGone, hasGonePercent, statisticLastHasGone);

                if (statisticDamaged < statisticHasGone)
                    stat += String.Format(", в т.ч. неповредённых: {0} ( {1}% )",
                        (statisticHasGone - statisticDamaged), statisticWithoutDamage );

                stat += "\n";
            }

            if (statisticAmountOfDamage > 0 && !Shilka.school && !Shilka.training)
            {
                stat += String.Format("нанесён ущерб: {0}", HumanReadableSumm(statisticAmountOfDamage));

                if (statisticShutdownFlag)
                    stat += String.Format(" ( +{0} млн $ сбит {1} )",
                        statisticLastDamagePrice, statisticLastDamageType);
                else
                    stat += String.Format(" ( +{0:f2} млн ${1} повреждён {2} )",
                        statisticLastDamagePrice, (seriousDamage ? " серьёзно" : String.Empty), statisticLastDamageType);

                stat += "\n";
            }

            if (statisticFriendDamage > 0)
                stat += String.Format("повреждено своих: {0} ( повреждён {1} )\n",
                    statisticFriendDamage, statisticLastDamageFriend);

            if (statisticAirlinerDamage > 0)
                stat += String.Format("повреждено гражданских: {0} ( повреждён {1} )\n",
                    statisticAirlinerDamage, statisticLastDamageAirliner);

            if (statisticAllAircraft > 0 && !Shilka.training)
                stat += String.Format("удача: {0:f2}", chance) + "\n";

            if (statisticShellsFired > 0)
                stat += String.Format("температура стволов: {0}°C {1}",
                    Shilka.degreeOfHeatingGunBurrels, (Shilka.reheatingGunBurrels ? " - перегрев стволов!" : String.Empty));

            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                FirePlace main = (FirePlace)Application.Current.MainWindow;
                main.statShells.Content = stat;
            }));
        }

        public static void ShowTraining(object obj, ElapsedEventArgs e)
        {
            string stat = String.Empty;

            if (statisticShellsFired > 0)
                stat += String.Format("выстрелов: {0}\n", statisticShellsFired);

            if (staticticInTarget > 0)
                stat += String.Format("попаданий: {0}\n", staticticInTarget);

            if (staticticAircraftShutdown > 0)
                stat += String.Format("сбито: {0}\n", staticticAircraftShutdown);

            if (statisticDamaged > 0)
                stat += String.Format("повреждено: {0}\n", statisticDamaged);

            if (statisticHasGone > 0)
                stat += String.Format("упущено: {0}\n", statisticHasGone);

            if (statisticShellsFired > 0)
                stat += String.Format("температура стволов: {0}°C {1}",
                    Shilka.degreeOfHeatingGunBurrels, (Shilka.reheatingGunBurrels ? " - перегрев стволов!" : String.Empty));

            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                FirePlace main = (FirePlace)Application.Current.MainWindow;
                main.statShells.Content = stat;
            }));
        }
    }
}
