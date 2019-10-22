using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Weapon : Item
    {
        public int m_maximumDamage { get; set; }
        public int m_minimumDamage { get; set; }

        public Weapon(int id, string name, string namePlural, int minimumDamage, int maximumDamage) : base(id, name, namePlural)
        {
            m_minimumDamage = minimumDamage;
            m_maximumDamage = maximumDamage;
        }
    }
}
