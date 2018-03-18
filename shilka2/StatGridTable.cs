using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shilka2
{
    class StatGridTable
    {
        public string name { get; set; }
        public string shellsFired { get; set; }
        public string inTarget { get; set; }
        public string Shutdown { get; set; }
        public string inTargetPercent { get; set; }
        public string shutdownPercent { get; set; }
        public string damaged { get; set; }
        public string damagedPercent { get; set; }
        public string hasGone { get; set; }
        public string withoutDamage { get; set; }
        public string amountOfDamage { get; set; }
        public string friendDamage { get; set; }
        public string chance { get; set; }

        public StatGridTable(string name, string shellsFired, string inTarget,
              string aircraftShutdown, string inTargetPercent, string shutdownPercent,
              string damaged, string damagedPercent, string hasGone,
              string withoutDamage, string amountOfDamage, string friendDamage,
              string chance)
        {
            this.name = name;
            this.shellsFired = shellsFired;
            this.inTarget = inTarget;
            this.Shutdown = aircraftShutdown;
            this.inTargetPercent = inTargetPercent + "%";
            this.shutdownPercent = shutdownPercent + "%";
            this.damaged = damaged;
            this.damagedPercent = damagedPercent;
            this.hasGone = hasGone;
            this.withoutDamage = withoutDamage;
            this.amountOfDamage = amountOfDamage;
            this.friendDamage = friendDamage;
            this.chance = string.Format("{0:f2}", float.Parse(chance));
        }
    }
}
