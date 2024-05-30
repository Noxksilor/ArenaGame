using ArenaGame.Heroes;
using ArenaGame.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGame
{
    public class GameEngine
    {
        public class NotificationArgs
        {
            public Hero Attacker { get; set; }
            public Hero Defender { get; set; }
            public double Attack { get; set; }
            public double Damage { get; set; }
        }

        public delegate void GameNotifications(NotificationArgs args);

        private Random random = new Random();
        public Hero HeroA { get; set; }
        public Hero HeroB { get; set; }
        public Hero Winner { get; set; }
        public GameNotifications? NotificationsCallBack { get; set; }

        private Dictionary<string, string> weaponRestrictions = new Dictionary<string, string>
            {
                {"Wand", "Necromancer"},
                { "Staff", "Sorceress" }
            };

        public bool CheckWeapons()
        {
            bool heroAHasWeapon = weaponRestrictions.ContainsKey($"{HeroA.Weapon}") ;
            bool heroBHasWeapon = weaponRestrictions.ContainsKey($"{HeroB.Weapon}");

            ISpecialWeapon heroAWeapon = HeroA.Weapon as ISpecialWeapon;
            ISpecialWeapon heroBWeapon = HeroB.Weapon as ISpecialWeapon;

            if (heroAHasWeapon || heroBHasWeapon)
            {
                bool canTakeWeapon = false;

                if (heroAHasWeapon)
                {
                    canTakeWeapon = weaponRestrictions[$"{HeroA.Weapon}"] == $"{HeroA}";
                    if (canTakeWeapon) { heroAWeapon.AddSkill(HeroA); }
                }
                else if (heroBHasWeapon)
                {
                    canTakeWeapon = weaponRestrictions[$"{HeroB.Weapon}"] == $"{HeroB}";
                    if (canTakeWeapon) { heroBWeapon.AddSkill(HeroB); }
                }

                return canTakeWeapon;

            }

            return true;
        }

        public void Fight()
        {
            Hero attacker;
            Hero defender;

            double probability = random.NextDouble();
            if (probability < 0.5)
            {
                attacker = HeroA;
                defender = HeroB;
            } else
            {
                attacker = HeroB;
                defender = HeroA;
            }

            while (attacker.IsAlive)
            {
                double attack = attacker.Weapon is Bow ? attacker.AttackWithBow((Bow)attacker.Weapon) : attacker.Attack();
                double actualDamage = defender.Defend(attack);

                if (NotificationsCallBack != null)
                {

                    NotificationsCallBack(new NotificationArgs()
                    {
                        Attacker = attacker,
                        Defender = defender,
                        Attack = attack,
                        Damage = actualDamage
                    }); 
                }

                Hero tempHero = attacker;
                attacker = defender;
                defender = tempHero;
            }
            Winner = defender;
        }
    }
}
