﻿using System;
using System.Windows.Media;

namespace shilka2
{
    class StatTable
    {
        public string name { get; set; }
        public ImageSource scriptFlag { get; set; }
        public int shellsFired { get; set; }
        public int inTarget { get; set; }
        public int shutdown { get; set; }
        public int inTargetPercent { get; set; }
        public int shutdownPercent { get; set; }
        public int damaged { get; set; }
        public int damagedPercent { get; set; }
        public int hasGone { get; set; }
        public int withoutDamage { get; set; }
        public double amountOfDamage { get; set; }
        public int friendDamage { get; set; }
        public int airlinerDamage { get; set; }
        public float chance { get; set; }
        public string time { get; set; }
        public string badTime { get; set; }
        public int shootTime { get; set; }
        public int shootNumber { get; set; }
        public string aircrafts { get; set; }
        public string aircraftsDamaged { get; set; }
        public double amountOfDamageWithChance { get; set; }

        private static string TimeFormat(string timeInSecond)
        {
            TimeSpan time = TimeSpan.FromSeconds(int.Parse(timeInSecond));
            return time.ToString();
        }

        private static double ChanceAmount(string amountOfDamage, string chance)
        {
            double amountParam = double.Parse(amountOfDamage);
            double chanceParam = double.Parse(chance);

            if (chanceParam < 0.01)
                return double.Parse(string.Format("{0:f2}", amountParam));
            else
                return double.Parse(string.Format("{0:f2}", amountParam / chanceParam));
        }

        public StatTable(string name, string script, string shellsFired, string inTarget,
              string aircraftShutdown, string inTargetPercent, string shutdownPercent,
              string damaged, string damagedPercent, string hasGone,
              string withoutDamage, string amountOfDamage, string friendDamage,
              string airlinerDamage, string chance, ImageSource flag, string time, string badTime,
              string shootTime, string shootNumber, string aicrafts, string aircraftsDamaged)
        {
            this.name = name;
            this.shellsFired = int.Parse(shellsFired);
            this.inTarget = int.Parse(inTarget);
            this.shutdown = int.Parse(aircraftShutdown);
            this.inTargetPercent = int.Parse(inTargetPercent);
            this.shutdownPercent = int.Parse(shutdownPercent);
            this.damaged = int.Parse(damaged);
            this.damagedPercent = int.Parse(damagedPercent);
            this.hasGone = int.Parse(hasGone);
            this.withoutDamage = int.Parse(withoutDamage);
            this.amountOfDamage = double.Parse(amountOfDamage);
            this.friendDamage = int.Parse(friendDamage);
            this.airlinerDamage = int.Parse(airlinerDamage);
            this.chance = float.Parse(string.Format("{0:f2}", float.Parse(chance)));
            this.scriptFlag = flag;
            this.time = TimeFormat(time);
            this.badTime = TimeFormat(badTime);
            this.shootTime = int.Parse(shootTime);
            this.shootNumber = int.Parse(shootNumber);
            this.aircrafts = aicrafts;
            this.aircraftsDamaged = aircraftsDamaged;
            this.amountOfDamageWithChance = ChanceAmount(amountOfDamage, chance);
        }
    }
}
