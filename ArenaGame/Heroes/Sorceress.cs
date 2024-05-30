using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGame.Heroes
{
    public class Sorceress: Hero
    {
        private double Lightning;
        public int skill;
        private double damage_coef;
        public Sorceress(string name, double armor, double strength, IWeapon weapon) : base(name, armor, strength, weapon)
        {
            Lightning = 40;
            skill = 1;
            damage_coef = 0.6;
        }

        public override double Attack()
        {
            double damage = base.Attack();

            double probability = random.NextDouble();

            if (probability < 0.10)
            {
                double realDamage = damage + Lightning * skill * damage_coef;
                return realDamage;
            }

            return damage;
        }
    }
}
