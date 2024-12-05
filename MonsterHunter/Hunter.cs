using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MonsterHunter
{
    // The Hunter class inherits from the Character class
    // It represents the player's character, including all attributes and actions related to the hunter
    public class Hunter : Character
    {
        // Hunter's unique properties
        public string Name { get; set; }    // Name of the Hunter
        public int Score { get; set; }   // Score of the Hunter, increased by actions
        public IState State { get; set; }   // Current state of the Hunter (Normal, Poisoned, etc.)
        private Pickaxe pickaxeH { get; set; }   // Hunter's pickaxe (if any)
        private Sword swordH { get; set; }    // Hunter's sword (if any)
        private Potions potionsH { get; set; }    // Hunter's potions (if any)
        public Shield shieldH { get; set; }   // Hunter's shield (if any)
        public bool isInvisible { get; set; }    // Whether the Hunter is invisible
        public Map currentMap { get; set; }    // The current map where the Hunter is located
        public bool Levelup { get; set; } // To check if the hunter reached the goal
        // Constructor for initializing the Hunter with position, name, and map
        public Hunter(int x, int y, string name, Map map) : base(x, y)
        {
            X = x;
            Y = y;
            Score = 0;
            Name = name;
            MaxHP = 30;   // Default max health points
            CurrentHP = MaxHP;   // Set current health to max health initially
            pickaxeH = null;
            swordH = null;
            shieldH = null;
            isInvisible = false;   // Hunter is not invisible by default
            Strength = 7;   // Default strength of the Hunter
            Armor = 4;  // Default armor of the Hunter
            FreezeTime = 1000;  // Default freeze time of 1 second
            State = new NormalState();  // The Hunter starts in a normal state
            Levelup = false;
        }

        // Method to move the Hunter to a new position, and interact with items/monsters on the way
        public override bool Move(int newX, int newY, Map map)
        {
            // Check if there are monsters at the new position
            Monster[] target = Monsters.FindMonstersAtPosition(newX, newY);
            if (target.Length > 0)  // If there are monsters, attack them
            {
                if (!isInvisible)
                {
                    foreach (Monster monster in target)
                    {
                        attack(monster, map);
                    }
                }
                else
                {
                    this.X = newX;
                    this.Y = newY;
                }
            }
            // If the position is a wall, try to break it if the Hunter has a pickaxe
            else if (map.MapData[newX, newY] == '#')
            {
                if (!isInvisible)
                {
                    if (hasPickaxe())  // Check if the Hunter has a pickaxe
                    {
                        breakWall(map, newX, newY);
                    }
                    else
                    {
                        map.info.Add("You don't have a pickaxe to break the wall.");
                        return false;
                    }
                }
                else
                {
                    this.X = newX;
                    this.Y = newY;
                }
            }
            // If the position has an item (e.g., pickaxe, sword), add it to the inventory
            else if ((map.MapData[newX, newY] == 'h') || (map.MapData[newX, newY] == 'w') ||
                     (map.MapData[newX, newY] == 'p') || (map.MapData[newX, newY] == 'x'))
            {
                AddToInventory(map.MapData[newX, newY], map);
                map.MapData[newX, newY] = ' ';
            } else if (map.MapData[newX,newY] == 'G')
            {
                map.info.Add("You reached the goal!! Level Up!");
                this.FreezeTime -= 50;
                this.CurrentHP += (int)((this.MaxHP - this.CurrentHP) / 2);
                this.Levelup = true;
                this.Score += 500;
            }
            // Otherwise, simply move to the new position
            else if (map.MapData[newX, newY] == ' ')
            {
                this.X = newX;
                this.Y = newY;

                return true;
            } 
            return false;
        }

        // Adds items to the Hunter's inventory and updates score
        private void AddToInventory(char item, Map map)
        {
            switch (item)
            {
                case 'x':   // Pickaxe
                    Pickaxe pickaxe = new Pickaxe();
                    pickaxeH = pickaxe;
                    Score += 50;   // Increase score when picking up an item
                    map.info.Add("You picked up a pickaxe!");
                    break;
                case 'w':   // Sword
                    if (swordH != null) { this.Strength -= swordH.Strength; }  // Remove the previous sword's strength
                    Sword sword = new Sword();
                    swordH = sword;
                    this.Strength += sword.Strength;  // Add new sword's strength
                    Score += 50;
                    map.info.Add("You picked up a Sword: +" + sword.Strength + " Strength!");
                    break;
                case 'h':   // Shield
                    if (shieldH != null) { this.Armor -= shieldH.Armor; }  // Remove previous shield's armor
                    Shield shield = new Shield();
                    shieldH = shield;
                    this.Armor += shield.Armor;  // Add new shield's armor
                    Score += 50;
                    map.info.Add("You picked up a Shield: +" + shield.Armor + " Defense!");
                    break;
                case 'p':   // Potion
                    Potions potion = new Potions();
                    this.drinkPotion(potion, map);  // Drink the potion and change state accordingly
                    Score += 25;
                    break;
            }
        }

        // Checks if the Hunter has a pickaxe
        private bool hasPickaxe()
        {
            return this.pickaxeH != null;
        }

        // Breaks the wall if the Hunter has a pickaxe and the pickaxe is usable
        private void breakWall(Map map, int newX, int newY)
        {
            if (this.pickaxeH.BreakAfterUse())  // If the pickaxe breaks after use
            {
                this.pickaxeH = null;  // Remove pickaxe from inventory
                map.info.Add("Your pickaxe broke!");
            }
            map.MapData[newX, newY] = ' ';
            map.info.Add("Wall broken!");
        }

        // Handles drinking a potion and changing the Hunter's state
        public void drinkPotion(Potions potion, Map map)
        {
            // Change state based on the potion type
            switch (potion.Type)
            {
                case PotionType.Strength:
                    State = new StrongState(this, map);  // Increase strength
                    map.info.Add("You drank a strength potion! Bonus damage, armor and full health!");
                    break;
                case PotionType.Poison:
                    State = new PoisonedState(this, map);  // Apply poison effect
                    map.info.Add("You drank poison! You recieve damage and struggle to move!");
                    break;
                case PotionType.Invisibility:
                    State = new InvisibleState(this, map);  // Make the Hunter invisible
                    map.info.Add("You drank an invisibility potion! You can now walk through enemies and walls!");
                    break;
                case PotionType.Speed:
                    State = new FastState(this, map);  // Increase speed
                    map.info.Add("You drank a speed potion! You can act faster now!");
                    break;
            }
        }

        // Attack a monster and deal damage
        public void attack(Monster target, Map map)
        {
            int hit = this.Strength - target.Armor;  // Calculate damage based on strength and monster armor
            target.CurrentHP -= hit;  // Reduce monster's health
            map.info.Add($"You dealt {hit} damage");

            hit = target.Strength - this.Armor;
            this.CurrentHP -= hit;  // Apply damage to the hunter
            map.info.Add($"The monster dealt {hit} damage.");

            // If the sword breaks after an attack, remove it
            if (this.swordH != null)
            {
                if (this.swordH.BreakAfterAttack())
                {
                    this.Strength -= swordH.Strength;
                    Sword broken = new Sword();
                    broken.Strength = 0;
                    this.swordH = broken;
                    map.info.Add("Your sword broke!!");
                }
            }


            // Check if the hunter's shield breaks after the attack
            if (this.shieldH != null && this.shieldH.BreakAfterAttack())
            {
                // If the shield breaks, reduce the hunter's armor and nullify the shield
                this.Armor -= this.shieldH.Armor;
                this.shieldH = null;
                map.info.Add("Your shield broke!"); // Inform the hunter that the shield broke
            }

            // If the monster is dead, remove it and increase the Hunter's score
            if (target.IsDead())
            { 
                target.X = 0;
                target.Y = 0;
                target = null;
                GC.Collect();
                this.Score += 100;
                map.info.Add("The monster died!!");
            }
        }
    }
}
