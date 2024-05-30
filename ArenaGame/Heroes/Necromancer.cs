using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ArenaGame.Heroes
{
    public class Necromancer:Hero
    {
        public int skill;
        private double theft_coef;
        private GameEngine args;
        public Hero defender;
        public Necromancer(GameEngine args, string name, double armor, double strength,IWeapon weapon):base(name, armor, strength, weapon)
        {
            this.args= args;
            skill = 1;
            theft_coef = 0.3;
        }

        public override double Attack()
        {
            double damage = base.Attack();

            void GetDefender(GameEngine.NotificationArgs args)
            {
                defender = args.Defender;
            }

            args.NotificationsCallBack = GetDefender;

            double probability = random.NextDouble();

            if (probability < 0.10)
            {
                double stolenHealth = defender.Health * theft_coef*skill;
                defender.ChangeHealth(-stolenHealth);

                ChangeHealth(stolenHealth);
            }
            return damage;
        }
    }
}
