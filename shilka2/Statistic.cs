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
        public static string statisticLastDamageType;

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

            chance = (double)statisticPriceOfAllAircrafts / (statisticAllAircraft * (double)Aircraft.AIRCRAFT_AVERAGE_PRICE);
            if (double.IsNaN(chance)) chance = 0;

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
            statisticLastDamageType = "";
        }

        public static void Save(string player)
        {
            int shutdownPercent, damagedPercent, statisticWithoutDamage, inTargetPercent, shellsForShutdown;
            double chance, baseForPercent;

            Calc(out baseForPercent, out shutdownPercent, out damagedPercent, out statisticWithoutDamage,
                out chance, out inTargetPercent, out shellsForShutdown);

            double statisticAmountOfDamageRound = double.Parse(string.Format("{0:f2}", statisticAmountOfDamage));

            string stat = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}\n",
                  player, Shilka.currentScript, statisticShellsFired, staticticInTarget, staticticAircraftShutdown,
                  inTargetPercent, shutdownPercent, statisticDamaged, damagedPercent, statisticHasGone,
                  statisticWithoutDamage, statisticAmountOfDamageRound, statisticFriendDamage, chance
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

            foreach (string statLine in statisticLines)
            {
                string[] stat = statLine.Split('|');

                Scripts.scriptsNames scriptFullName = (Scripts.scriptsNames)Enum.Parse(typeof(Scripts.scriptsNames), stat[1]);

                ImageSource flagSource = Aircraft.imageFromResources(Scripts.scriptFlagName(scriptFullName));

                result.Add(new StatTable(
                    stat[0], stat[1], stat[2], stat[3], stat[4], stat[5], stat[6],
                    stat[7], stat[8], stat[9], stat[10], stat[11], stat[12], stat[13], flagSource
                ));
            }

            return result;
        }

        public static void Show(object obj, ElapsedEventArgs e)
        {
            string stat = "";
            int shutdownPercent, damagedPercent, statisticWithoutDamage, inTargetPercent, shellsForShutdown;
            double chance, baseForPercent;

            Calc(out baseForPercent, out shutdownPercent, out damagedPercent, out statisticWithoutDamage,
                out chance, out inTargetPercent, out shellsForShutdown);

            if (statisticShellsFired > 0)
            {
                stat += "Выстрелов: " + statisticShellsFired;

                if (staticticAircraftShutdown > 0)
                    stat += " (" + shellsForShutdown + " выстр./сбитый)";

                stat += "\n";
            }

            if (staticticInTarget > 0)
                stat += "Попаданий: " + staticticInTarget + " ( " + inTargetPercent + "% )\n";

            if (staticticAircraftShutdown > 0)
                stat += "Сбито: " + staticticAircraftShutdown + " ( " + shutdownPercent + "% )";

            if (statisticDamaged > 0)
                stat += (staticticAircraftShutdown == 0 ? "П" : " +п") + "овреждено: " + statisticDamaged + " ( " + damagedPercent + "% )\n";
            else if (staticticAircraftShutdown > 0)
                stat += "\n";

            if (statisticHasGone > 0)
            {
                int hasGonePercent = (int)(statisticHasGone * baseForPercent);

                if ((hasGonePercent == 0) && (statisticHasGone > 0)) hasGonePercent = 1;

                stat += "Упущено: " + statisticHasGone + " ( " + hasGonePercent + "% )";

                if (statisticDamaged < statisticHasGone)
                    stat += ", в том числе неповредённых: " + (statisticHasGone - statisticDamaged) + " ( " + statisticWithoutDamage + "% )";

                stat += "\n";
            }

            if (statisticAmountOfDamage > 0)
            {
                stat += "Нанесён ущерб: ";

                if (statisticAmountOfDamage < 1000)
                    stat += string.Format("{0:f2}", statisticAmountOfDamage) + " млн $";
                else if (statisticAmountOfDamage < 1000000)
                    stat += string.Format("{0:f2}", (double)statisticAmountOfDamage / 1000) + " млрд $";
                else
                    stat += string.Format("{0:f2}", (double)statisticAmountOfDamage / 1000000) + " трлн $";

                if (statisticShutdownFlag)
                    stat += " ( +" + statisticLastDamagePrice + " млн $ сбит " + statisticLastDamageType + " )";
                else
                    stat += string.Format(" ( +{0:f2} млн $ повреждён ", statisticLastDamagePrice) + statisticLastDamageType + " )";

                stat += "\n";
            }

            if (statisticFriendDamage > 0) stat += "Повреждено своих: " + statisticFriendDamage;

            if (statisticAirlinerDamage > 0)
                stat += (statisticFriendDamage == 0 ? "П" : " +п") + "овреждено гражданских: " + statisticAirlinerDamage + "\n";
            else if (statisticFriendDamage > 0)
                stat += "\n";

            if (statisticAllAircraft > 0) stat += string.Format("Удача: {0:f2}", chance) + "\n";

            if (statisticShellsFired > 0)
            {
                stat += "Температура стволов: " + Shilka.degreeOfHeatingGunBurrels + "°C";

                if (Shilka.reheatingGunBurrels) stat += " - перегрев стволов!\n";

                stat += "\n";
            }

            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                MainWindow main = (MainWindow)Application.Current.MainWindow;
                main.statShells.Content = stat;
            }));
        }
    }
}
