﻿namespace shilka2
{
    class StatTable
    {
        public string name { get; set; }
        public string script { get; set; }
        public int shellsFired { get; set; }
        public int inTarget { get; set; }
        public int Shutdown { get; set; }
        public int inTargetPercent { get; set; }
        public int shutdownPercent { get; set; }
        public int damaged { get; set; }
        public int damagedPercent { get; set; }
        public int hasGone { get; set; }
        public int withoutDamage { get; set; }
        public double amountOfDamage { get; set; }
        public int friendDamage { get; set; }
        public float chance { get; set; }

        public StatTable(string name, string script, string shellsFired, string inTarget,
              string aircraftShutdown, string inTargetPercent, string shutdownPercent,
              string damaged, string damagedPercent, string hasGone,
              string withoutDamage, string amountOfDamage, string friendDamage,
              string chance)
        {
            this.name = name;
            this.script = script;
            this.shellsFired = int.Parse(shellsFired);
            this.inTarget = int.Parse(inTarget);
            this.Shutdown = int.Parse(aircraftShutdown);
            this.inTargetPercent = int.Parse(inTargetPercent);
            this.shutdownPercent = int.Parse(shutdownPercent);
            this.damaged = int.Parse(damaged);
            this.damagedPercent = int.Parse(damagedPercent);
            this.hasGone = int.Parse(hasGone);
            this.withoutDamage = int.Parse(withoutDamage);
            this.amountOfDamage = double.Parse(amountOfDamage);
            this.friendDamage = int.Parse(friendDamage);
            this.chance = float.Parse(string.Format("{0:f2}", float.Parse(chance)));
        }
    }
}
