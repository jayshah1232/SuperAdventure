using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class InventoryItem
    {
        public Item m_details { get; set; }
        public int m_quantity { get; set; }

        public InventoryItem(Item details, int quantity)
        {
            m_details = details;
            m_quantity = quantity;
        }
    }
}
