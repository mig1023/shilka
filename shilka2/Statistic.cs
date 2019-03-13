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
        static string statisticFileName = "statistic.dat";
        public static int statisticGridMargins = 120;

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
        public static int shootingTimeSec = 0;
        public static int shootingNumber = 0;

        public static List<string> statisticScripts; 

        public static Dictionary<string, int> downedAircrafts = new Dictionary<string, int>();

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

            chance = (double)statisticPriceOfAllAircrafts / (statisticAllAircraft * (double)Constants.AIRCRAFT_AVERAGE_PRICE);

            if (double.IsNaN(chance))
                chance = 0;

            shellsForShutdown = (staticticAircraftShutdown > 0 ? (int)statisticShellsFired / staticticAircraftShutdown : 0);
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
            shootingTimeSec = 0;
            shootingNumber = 0;
            downedAircrafts.Clear();
    }

        public static void Save(string player)
        {
            int shutdownPercent, damagedPercent, statisticWithoutDamage, inTargetPercent, shellsForShutdown;
            double chance, baseForPercent;

            Calc(out baseForPercent, out shutdownPercent, out damagedPercent, out statisticWithoutDamage,
                out chance, out inTargetPercent, out shellsForShutdown);

            double statisticAmountOfDamageRound = double.Parse(string.Format("{0:f2}", statisticAmountOfDamage));

            string downedAircraftsList = String.Empty;

            if (downedAircrafts.Count > 0)
                foreach(var aircraft in downedAircrafts.OrderByDescending(aircraft => aircraft.Value))
                    downedAircraftsList += aircraft.Key + " - " + aircraft.Value + ", ";

            string stat = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}|{14}|{15}|{16}|{17}|{18}\n",
                  player, Shilka.currentScript, statisticShellsFired, staticticInTarget, staticticAircraftShutdown,
                  inTargetPercent, shutdownPercent, statisticDamaged, damagedPercent, statisticHasGone,
                  statisticWithoutDamage, statisticAmountOfDamageRound, statisticFriendDamage, statisticAirlinerDamage,
                  chance, gameTimeSec, shootingTimeSec, shootingNumber, downedAircraftsList
            );

            File.AppendAllText("statistic.dat", stat);
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

                ImageSource flagSource = Aircraft.ImageFromResources(Scripts.ScriptFlagName(scriptFullName));
                statisticScripts.Add(Scripts.scriptsRuNames[scriptFullName.ToString()]);

                result.Add(new StatTable(
                    stat[0], stat[1], stat[2], stat[3], stat[4], stat[5], stat[6],
                    stat[7], stat[8], stat[9], stat[10], stat[11], stat[12], stat[13],
                    stat[14], flagSource, stat[15], stat[16], stat[17], stat[18]
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

        public static void AircraftToStatistic(string aircraft)
        {
            if (downedAircrafts.ContainsKey(aircraft))
                downedAircrafts[aircraft] += 1;
            else
                downedAircrafts.Add(aircraft, 1);
        }

        public static void Show(object obj, ElapsedEventArgs e)
        {
            string stat = String.Empty;

            int shutdownPercent, damagedPercent, statisticWithoutDamage, inTargetPercent, shellsForShutdown;
            double chance, baseForPercent;

            Calc(out baseForPercent, out shutdownPercent, out damagedPercent, out statisticWithoutDamage,
                out chance, out inTargetPercent, out shellsForShutdown);

            if (statisticShellsFired > 0)
            {
                stat += "выстрелов: " + statisticShellsFired;

                if (staticticAircraftShutdown > 0)
                    stat += " ( " + shellsForShutdown + " выстр./сбитый )";

                stat += "\n";
            }

            if (staticticInTarget > 0)
                stat += "попаданий: " + staticticInTarget + " ( " + inTargetPercent + "% )\n";

            if (staticticAircraftShutdown > 0)
                stat += "сбито: " + staticticAircraftShutdown + " ( " + shutdownPercent + "% )";

            if (statisticDamaged > 0)
                stat += (staticticAircraftShutdown == 0 ? String.Empty : " +") +"повреждено: " + statisticDamaged + " ( " + damagedPercent + "% )\n";
            else if (staticticAircraftShutdown > 0)
                stat += "\n";

            if (statisticHasGone > 0)
            {
                int hasGonePercent = (int)(statisticHasGone * baseForPercent);

                if ((hasGonePercent == 0) && (statisticHasGone > 0))
                    hasGonePercent = 1;

                stat += "упущено: " + statisticHasGone + " ( " + hasGonePercent + "% )";

                if (statisticDamaged < statisticHasGone)
                    stat += ", в т.ч. неповредённых: " + (statisticHasGone - statisticDamaged) + " ( " + statisticWithoutDamage + "% )";

                stat += ", последний " + statisticLastHasGone + "\n";
            }

            if (statisticAmountOfDamage > 0 && !Shilka.school)
            {
                stat += "нанесён ущерб: " + HumanReadableSumm(statisticAmountOfDamage);

                if (statisticShutdownFlag)
                    stat += " ( +" + statisticLastDamagePrice + " млн $ сбит " + statisticLastDamageType + " )";
                else
                    stat += string.Format(" ( +{0:f2} млн $" + (seriousDamage ? " серьёзно" : String.Empty) +
                        " повреждён ", statisticLastDamagePrice) + statisticLastDamageType + " )";

                stat += "\n";
            }

            if (statisticFriendDamage > 0)
                stat += "повреждено своих: " + statisticFriendDamage + " ( повреждён " + statisticLastDamageFriend + " )\n";

            if (statisticAirlinerDamage > 0)
                stat += "повреждено гражданских: " + statisticAirlinerDamage + " ( повреждён " + statisticLastDamageAirliner + " )\n";

            if (statisticAllAircraft > 0)
                stat += string.Format("удача: {0:f2}", chance) + "\n";

            if (statisticShellsFired > 0)
                stat += "температура стволов: " + Shilka.degreeOfHeatingGunBurrels + "°C" +
                    (Shilka.reheatingGunBurrels ? " - перегрев стволов!" : String.Empty);

            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                FirePlace main = (FirePlace)Application.Current.MainWindow;
                main.statShells.Content = stat;
            }));
        }
    }
}
