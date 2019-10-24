using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Engine;

namespace SuperAdventure
{
    public partial class SuperAdventure : Form
    {
        private Player player;
        private Monster currentMonster;
        public SuperAdventure()
        {
            InitializeComponent();


            player = new Player(10, 10, 20, 0, 1);
            MoveTo(World.locationByID(World.LOCATION_ID_HOME));
            player.Inventory.Add(new InventoryItem(World.itemByID(World.ITEM_ID_RUSTY_SWORD), 1));

            lblHitPoints.Text = player.m_currentHitPoints.ToString();
            lblGold.Text = player.m_gold.ToString();
            lblExperience.Text = player.m_experiencePoints.ToString();
            lblLevel.Text = player.m_level.ToString();
        }

        private void btnNorth_Click(object sender, EventArgs e)
        {
            MoveTo(player.CurrentLocation.m_locationToNorth);
        }

        private void btnSouth_Click(object sender, EventArgs e)
        {
            MoveTo(player.CurrentLocation.m_locationToSouth);
        }

        private void btnEast_Click(object sender, EventArgs e)
        {
            MoveTo(player.CurrentLocation.m_locationToEast);
        }

        private void btnWest_Click(object sender, EventArgs e)
        {
            MoveTo(player.CurrentLocation.m_locationToWest);
        }

        private void MoveTo(Location newLocation)
        {
            //Does the location have any required items
            if (!player.hasRequiredItemToEnterThisLocation(newLocation))
            {
                rtbMessages.Text += "You must have a " + newLocation.m_itemRequiredToEnter.m_name + " to enter this location." + Environment.NewLine;
                return;
            }

            // Update the player's current location
            player.CurrentLocation = newLocation;

            // Show/hide available movement buttons
            btnNorth.Visible = (newLocation.m_locationToNorth != null);
            btnEast.Visible = (newLocation.m_locationToEast != null);
            btnSouth.Visible = (newLocation.m_locationToSouth != null);
            btnWest.Visible = (newLocation.m_locationToWest != null);

            // Display current location name and description
            rtbLocation.Text = newLocation.m_name + Environment.NewLine;
            rtbLocation.Text += newLocation.m_description + Environment.NewLine;

            // Completely heal the player
            player.m_currentHitPoints = player.m_maximumHitPoints;

            // Update Hit Points in UI
            lblHitPoints.Text = player.m_currentHitPoints.ToString();

            // Does the location have a quest?
            if (newLocation.m_questAvailableHere != null)
            {
                // See if the player already has the quest, and if they've completed it
                bool playerAlreadyHasQuest = player.hasThisQuest(newLocation.m_questAvailableHere);
                bool playerAlreadyCompletedQuest = player.completedThisQuest(newLocation.m_questAvailableHere);

                // See if the player already has the quest
                if (playerAlreadyHasQuest)
                {
                    // If the player has not completed the quest yet
                    if (!playerAlreadyCompletedQuest)
                    {
                        // See if the player has all the items needed to complete the quest
                        bool playerHasAllItemsToCompleteQuest = player.hasAllQuestCompletionItems(newLocation.m_questAvailableHere);

                        // The player has all items required to complete the quest
                        if (playerHasAllItemsToCompleteQuest)
                        {
                            // Display message
                            rtbMessages.Text += Environment.NewLine;
                            rtbMessages.Text += "You complete the '" + newLocation.m_questAvailableHere.m_name + "' quest." + Environment.NewLine;

                            // Remove quest items from inventory
                            player.RemoveQuestCompletionItems(newLocation.m_questAvailableHere);

                            // Give quest rewards
                            rtbMessages.Text += "You receive: " + Environment.NewLine;
                            rtbMessages.Text += newLocation.m_questAvailableHere.m_rewardExperiencePoints.ToString() + " experience points" + Environment.NewLine;
                            rtbMessages.Text += newLocation.m_questAvailableHere.m_rewardGold.ToString() + " gold" + Environment.NewLine;
                            rtbMessages.Text += newLocation.m_questAvailableHere.m_rewardItem.m_name + Environment.NewLine;
                            rtbMessages.Text += Environment.NewLine;

                            player.m_experiencePoints += newLocation.m_questAvailableHere.m_rewardExperiencePoints;
                            player.m_gold += newLocation.m_questAvailableHere.m_rewardGold;

                            // Add the reward item to the player's inventory
                            player.AddItemToInventory(newLocation.m_questAvailableHere.m_rewardItem);

                            // Mark the quest as completed
                            player.MarkQuestCompleted(newLocation.m_questAvailableHere);
                        }
                    }
                }
                else
                {
                    // The player does not already have the quest

                    // Display the messages
                    rtbMessages.Text += "You receive the " + newLocation.m_questAvailableHere.m_name + " quest." + Environment.NewLine;
                    rtbMessages.Text += newLocation.m_questAvailableHere.m_description + Environment.NewLine;
                    rtbMessages.Text += "To complete it, return with:" + Environment.NewLine;
                    foreach (QuestCompletionItem qci in newLocation.m_questAvailableHere.QuestCompletionItems)
                    {
                        if (qci.m_quantity == 1)
                        {
                            rtbMessages.Text += qci.m_quantity.ToString() + " " + qci.m_details.m_name + Environment.NewLine;
                        }
                        else
                        {
                            rtbMessages.Text += qci.m_quantity.ToString() + " " + qci.m_details.m_namePlural + Environment.NewLine;
                        }
                    }
                    rtbMessages.Text += Environment.NewLine;

                    // Add the quest to the player's quest list
                    player.Quests.Add(new PlayerQuest(newLocation.m_questAvailableHere));
                }
            }

            // Does the location have a monster?
            if (newLocation.m_monsterLivingHere != null)
            {
                rtbMessages.Text += "You see a " + newLocation.m_monsterLivingHere.m_name + Environment.NewLine;

                // Make a new monster, using the values from the standard monster in the World.m_monster list
                Monster standardMonster = World.monsterByID(newLocation.m_monsterLivingHere.m_ID);

                currentMonster = new Monster(standardMonster.m_ID, standardMonster.m_name, standardMonster.m_maximumDamage,
                    standardMonster.m_rewardExperiencePoints, standardMonster.m_rewardGold, standardMonster.m_currentHitPoints, standardMonster.m_maximumHitPoints);

                foreach (LootItem lootItem in standardMonster.LootTable)
                {
                    currentMonster.LootTable.Add(lootItem);
                }

                cboWeapons.Visible = true;
                cboPotions.Visible = true;
                btnUseWeapon.Visible = true;
                btnUsePotion.Visible = true;
            }
            else
            {
                currentMonster = null;

                cboWeapons.Visible = false;
                cboPotions.Visible = false;
                btnUseWeapon.Visible = false;
                btnUsePotion.Visible = false;
            }

            // Refresh player's inventory list
            UpdateInventoryListInUI();

            // Refresh player's quest list
            UpdateQuestListInUI();

            // Refresh player's weapons combobox
            UpdateWeaponListInUI();

            // Refresh player's potions combobox
            UpdatePotionListInUI();
        }

        private void updateInventoryListInUI()
        {
            dgvInventory.RowHeadersVisible = false;

            dgvInventory.ColumnCount = 22;
            dgvInventory.Columns[0].Name = "Name";
            dgvInventory.Columns[0].Width = 197;
            dgvInventory.Columns[1].Name = "Quantity";

            dgvInventory.Rows.Clear();

            foreach(InventoryItem inventoryItem in player.Inventory)
            {
                if(inventoryItem.m_quantity > 0)
                {
                    dgvInventory.Rows.Add(new[] { inventoryItem.m_details.m_name, inventoryItem.m_quantity.ToString() });
                }
            }
        }

        private void UpdateQuestListInUI()
        {
            dgvQuests.RowHeadersVisible = false;

            dgvQuests.ColumnCount = 2;
            dgvQuests.Columns[0].Name = "Name";
            dgvQuests.Columns[0].Width = 197;
            dgvQuests.Columns[1].Name = "Done?";

            dgvQuests.Rows.Clear();

            foreach (PlayerQuest playerQuest in player.Quests)
            {
                dgvQuests.Rows.Add(new[] { playerQuest.m_details.m_name, playerQuest.m_isCompleted.ToString() });
            }
        }

        private void UpdateWeaponListInUI()
        {
            List<Weapon> weapons = new List<Weapon>();

            foreach (InventoryItem inventoryItem in player.Inventory)
            {
                if (inventoryItem.m_details is Weapon)
                {
                    if (inventoryItem.m_quantity > 0)
                    {
                        weapons.Add((Weapon)inventoryItem.m_details);
                    }
                }
            }

            if (weapons.Count == 0)
            {
                // The player doesn't have any weapons, so hide the weapon combobox and "Use" button
                cboWeapons.Visible = false;
                btnUseWeapon.Visible = false;
            }
            else
            {
                cboWeapons.DataSource = weapons;
                cboWeapons.DisplayMember = "Name";
                cboWeapons.ValueMember = "ID";

                cboWeapons.SelectedIndex = 0;
            }
        }

        private void UpdatePotionListInUI()
        {
            List<HealingPotion> healingPotions = new List<HealingPotion>();

            foreach (InventoryItem inventoryItem in player.Inventory)
            {
                if (inventoryItem.m_details is HealingPotion)
                {
                    if (inventoryItem.m_quantity > 0)
                    {
                        healingPotions.Add((HealingPotion)inventoryItem.m_details);
                    }
                }
            }

            if (healingPotions.Count == 0)
            {
                // The player doesn't have any potions, so hide the potion combobox and "Use" button
                cboPotions.Visible = false;
                btnUsePotion.Visible = false;
            }
            else
            {
                cboPotions.DataSource = healingPotions;
                cboPotions.DisplayMember = "Name";
                cboPotions.ValueMember = "ID";

                cboPotions.SelectedIndex = 0;
            }
        }

        private void btnUseWeapon_Click(object sender, EventArgs e)
        {

        }

        private void btnUsePotion_Click(object sender, EventArgs e)
        {

        }

        /*private void SuperAdventure_Load(object sender, EventArgs e)
        {

        }

        private void HitPoints_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }*/
    }
}
