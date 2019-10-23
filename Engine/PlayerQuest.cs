using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class PlayerQuest
    {
        public Quest m_details { get; set; }
        public bool m_isCompleted { get; set; }
        
        public PlayerQuest(Quest details)
        {
            m_details = details;
            m_isCompleted = false;
        }
    }
}
