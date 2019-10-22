using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class LivingCreature
    {
        public int m_currentHitPoints { get; set; }
        public int m_maximumHitPoints { get; set; }

        public LivingCreature(int currentHitPoints, int maximumHitPoints)
        {
            m_currentHitPoints = currentHitPoints;
            m_maximumHitPoints = maximumHitPoints;
        }
    }
}
