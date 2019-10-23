using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Quest
    {
        public int m_ID { get; set; }
        public string m_name { get; set; }
        public string m_description { get; set; }
        public int m_rewardExperiencePoints { get; set; }
        public int m_rewardGold { get; set; }
        public Item m_rewardItem { get; set; }
        public List<QuestCompletionItem> QuestCompletionItems { get; set; }

        public Quest(int id, string name, string description, int rewardExperiencePoints, int rewardGold)
        {
            m_ID = id;
            m_name = name;
            m_description = description;
            m_rewardExperiencePoints = rewardExperiencePoints;
            m_rewardGold = rewardGold;
            QuestCompletionItems = new List<QuestCompletionItem>();
        }
    }
}
