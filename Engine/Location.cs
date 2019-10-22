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

        public Location(int id, string name, string description)
        {
            m_ID = id;
            m_name = name;
            m_description = description;
        }
    }
}
