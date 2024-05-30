using ArenaGame.Weapons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGame
{
    public abstract class Hero : IHero
    {
        protected Random random = new Random();
        public string Name { get; private set; }
        public double Health { get; private set; }
        public double Armor { get; private set; }
        public double Strength { get; private set; }
        public IWeapon Weapon { get;  set; }

        private int firstArrows;
        public bool IsAlive
        {
            get
            {
                return Health > 0;
            }
        }

        public Hero(string name, double armor, double strenght, IWeapon weapon)
        {
            Health = 100;
            firstArrows = 3;

            Name = name;
            Armor = armor;
            Strength = strenght;
            Weapon = weapon;
        }


        // returns actual damage
        public virtual double Attack()
        {
            double totalDamage = Strength + Weapon.AttackDamage;
            double coef = random.Next(80, 120 + 1);
            double realDamage = totalDamage * (coef / 100);
            return realDamage;
        }

        public double AttackWithBow(Bow bow)
        {
            double realDamage = 0;
            while (firstArrows > 0) //While the opponent is running
            {
                Attack();
                firstArrows--;
                realDamage += Attack();
                return realDamage;
            }

            double probability = random.NextDouble();
            if (probability < 0.10) { return 0; }

            if (bow.ArrowAmount > 0) {return Attack();}
            else  
            {
                bow.ArrowAmount += 5;
                return 0;
            }
        }


        public virtual double Defend(double damage)
        {
            double coef = random.Next(80, 120 + 1);
            double defendPower = (Armor + Weapon.BlockingPower) * (coef / 100);
            double realDamage = damage - defendPower;
            if (realDamage < 0)
                realDamage = 0;
            Health -= realDamage;
            return realDamage;
        }

        public void ChangeHealth(double amount)
        {
            Health += amount;
        }

        public override string ToString()
        {
            return $"{Name} with health {Math.Round(Health,2)}";
        }
    }
}
