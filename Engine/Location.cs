using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Location
    {
        public int m_ID { get; set; }
        public string m_name { get; set; }
        public string m_description { get; set; }
        public Item m_itemRequiredToEnter { get; set; }
        public Quest m_questAvailableHere { get; set; }
        public Monster m_monsterLivingHere { get; set; }
        public Location m_locationToNorth { get; set; }
        public Location m_locationToEast { get; set; }
        public Location m_locationToSouth { get; set; }
        public Location m_locationToWest { get; set; }

        public Location(int id, string name, string description, Item itemRequiredToEnter = null, Quest questAvailableHere = null, Monster monsterLivingHere = null)
        {
            m_ID = id;
            m_name = name;
            m_description = description;
            m_itemRequiredToEnter = itemRequiredToEnter;
            m_questAvailableHere = questAvailableHere;
            m_monsterLivingHere = monsterLivingHere;
        }
    }
}
