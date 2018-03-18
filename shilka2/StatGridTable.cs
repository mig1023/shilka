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
        public int shellsFired { get; set; }
        public int inTarget { get; set; }
        public int Shutdown { get; set; }
        public int inTargetPercent { get; set; }
        public int shutdownPercent { get; set; }
        public int damaged { get; set; }
        public int damagedPercent { get; set; }
        public int hasGone { get; set; }
        public int withoutDamage { get; set; }
        public int amountOfDamage { get; set; }
        public int friendDamage { get; set; }
        public float chance { get; set; }

        public StatGridTable(string name, string shellsFired, string inTarget,
              string aircraftShutdown, string inTargetPercent, string shutdownPercent,
              string damaged, string damagedPercent, string hasGone,
              string withoutDamage, string amountOfDamage, string friendDamage,
              string chance)
        {
            this.name = name;
            this.shellsFired = int.Parse(shellsFired);
            this.inTarget = int.Parse(inTarget);
            this.Shutdown = int.Parse(aircraftShutdown);
            this.inTargetPercent = int.Parse(inTargetPercent);
            this.shutdownPercent = int.Parse(shutdownPercent);
            this.damaged = int.Parse(damaged);
            this.damagedPercent = int.Parse(damagedPercent);
            this.hasGone = int.Parse(hasGone);
            this.withoutDamage = int.Parse(withoutDamage);
            this.amountOfDamage = int.Parse(amountOfDamage);
            this.friendDamage = int.Parse(friendDamage);
            this.chance = float.Parse(string.Format("{0:f2}", float.Parse(chance)));
        }
    }
}
