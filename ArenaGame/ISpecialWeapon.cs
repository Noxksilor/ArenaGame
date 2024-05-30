using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArenaGame
{
    public interface ISpecialWeapon : IWeapon
    {
        public void AddSkill(Hero hero)
        {
        }
    }
}
