using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class LootItem
    {
        public Item m_details { get; set; }
        public int m_dropPercentage { get; set; }
        public bool m_isDefaultItem { get; set; }
        
        public LootItem(Item details, int dropPercentage, bool isDefaultItem)
        {
            m_details = details;
            m_dropPercentage = dropPercentage;
            m_isDefaultItem = isDefaultItem;
        }
    }
}
