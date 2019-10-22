using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Monster : LivingCreature
    {
        public int m_ID { get; set; }
        public string m_name { get; set; }
        public int m_maximumDamage { get; set; }
        public int m_rewardExperiencePoints { get; set; }
        public int m_rewardGold { get; set; }

        public Monster(int id, string name, int maximumDamage, int rewardExperiencePoints, int rewardGold, int currentHitPoints, int maximumHitPoints) : base(currentHitPoints, maximumHitPoints)
        {
            m_ID = id;
            m_name = name;
            m_maximumDamage = maximumHitPoints;
            m_rewardExperiencePoints = rewardExperiencePoints;
            m_rewardGold = rewardGold;
        }
    }
}
