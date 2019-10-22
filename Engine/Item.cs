using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Item
    {
        public int m_ID { get; set; }
        public string m_name { get; set; }
        public string m_namePlural { get; set; }

        public Item(int id, string name, string namePlural)
        {
            m_ID = id;
            m_name = name;
            m_namePlural = namePlural;
        }
    }
}
