using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Player : LivingCreature
    {
        public int m_gold { get; set; }
        public int m_experiencePoints { get; set; }
        public int m_level { get; set; }
        public List<InventoryItem> Inventory { get; set; }
        public List<PlayerQuest> Quests { get; set; }
        public Location CurrentLocation { get; set; }

        public Player(int currentHitPoints, int maximumHitPoints, int gold, int experiencePoints, int level) : base(currentHitPoints, maximumHitPoints)
        {
            m_gold = gold;
            m_experiencePoints = experiencePoints;
            m_level = level;

            Inventory = new List<InventoryItem>();
            Quests = new List<PlayerQuest>();
        }

        public bool hasRequiredItemToEnterThisLocation(Location location)
        {
            if(location.m_itemRequiredToEnter == null)
            {
                return true;
            }

            foreach(InventoryItem ii in Inventory)
            {
                if(ii.m_details.m_ID == location.m_itemRequiredToEnter.m_ID)
                {
                    return true;
                }
            }

            return false;
        }

        public bool hasThisQuest(Quest quest)
        {
            foreach(PlayerQuest playerQuest in Quests)
            {
                if(playerQuest.m_details.m_ID == quest.m_ID)
                {
                    return true;
                }
            }

            return false;
        }

        public bool completedThisQuest(Quest quest)
        {
            foreach(PlayerQuest playerQuest in Quests)
            {
                if(playerQuest.m_details.m_ID == quest.m_ID)
                {
                    return playerQuest.m_isCompleted;
                }
            }

            return false;
        }

        public bool hasAllQuestCompletionItems(Quest quest)
        {
            foreach(QuestCompletionItem qci in quest.QuestCompletionItems)
            {
                bool foundItemInPlayersInventory = false;

                foreach(InventoryItem ii in Inventory)
                {
                    if(ii.m_details.m_ID == qci.m_details.m_ID)
                    {
                        foundItemInPlayersInventory = true;

                        if(ii.m_quantity < qci.m_quantity)
                        {
                            return false;
                        }
                    }
                }

                if(!foundItemInPlayersInventory)
                {
                    return false;
                }
            }
            return true;
        }

        public void RemoveQuestCompletionItems(Quest quest)
        {
            foreach (QuestCompletionItem qci in quest.QuestCompletionItems)
            {
                foreach (InventoryItem ii in Inventory)
                {
                    if (ii.m_details.m_ID == qci.m_details.m_ID)
                    {
                        // Subtract the quantity from the player's inventory that was needed to complete the quest
                        ii.m_quantity -= qci.m_quantity;
                        break;
                    }
                }
            }
        }

        public void AddItemToInventory(Item itemToAdd)
        {
            foreach (InventoryItem ii in Inventory)
            {
                if (ii.m_details.m_ID == itemToAdd.m_ID)
                {
                    // They have the item in their inventory, so increase the quantity by one
                    ii.m_quantity++;

                    return; // We added the item, and are done, so get out of this function
                }
            }

            // They didn't have the item, so add it to their inventory, with a quantity of 1
            Inventory.Add(new InventoryItem(itemToAdd, 1));
        }

        public void MarkQuestCompleted(Quest quest)
        {
            // Find the quest in the player's quest list
            foreach (PlayerQuest pq in Quests)
            {
                if (pq.m_details.m_ID == quest.m_ID)
                {
                    // Mark it as completed
                    pq.m_isCompleted = true;

                    return; // We found the quest, and marked it complete, so get out of this function
                }
            }
        }
    }
}
