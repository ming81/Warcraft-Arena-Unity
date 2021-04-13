using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Core
{
    
    public enum LootType
    {
        LootCorpse         = 1,
        LootPickpocketing  = 2,
        LootFishing        = 3,
        LootDisenchanting  = 4,
        LootSkinning       = 6,
        LootProspecting    = 7,
        LootMilling        = 8,
        LootFish           = 20,
        LootFishingFail    = 21,
        LootInsignia       = 22,
        LootMail           = 23,
        LootSpell          = 24,
        LootDebug          = 100
    };
    
    public  enum CreatureLootStatus
    {
        CreatureLootStatusNone           = 0,
        CreatureLootStatusPickpocket     = 1,
        CreatureLootStatusLooted         = 2,
        CreatureLootStatusSkinAvailable  = 3,
        CreatureLootStatusSkinned        = 4
    };

    public class LootItem
    {
        private uint itemId;
        private LootInfo.LootItemType itemType;
        private uint count;
    }

    public class Loot
    {       
        internal Loot(Player player, Creature creature, LootType type)
        {
            throw new NotImplementedException();
        }
        
        internal Loot(Player player, ulong networkId , LootType type)
        {
            throw new NotImplementedException();
        }

        private void FillLoot(int lootId, Player player)
        {
            
        }

        private void Roll()
        {
            
        }

        public bool GetLootItemsListFor(Player player, List<LootItem> lootList)
        {
            return true;
        }

        public void Dispose()
        {
            
        }
    }
    
    public class LootManager
    {
        private readonly Dictionary<ulong, Loot> baseInstances = new Dictionary<ulong, Loot>();
        private readonly World world;

        internal LootManager(World world)
        {
            this.world = world;
        }

        public LootInfo GetLoot(Player player, ulong networkId)
        {
            var unit = world.UnitManager.Find(networkId);
            LootInfo loot = null;
            switch (unit.EntityType)
            {
                case EntityType.Item:
                    loot = null;
                    break;
                case EntityType.Corpse:
                    loot = null;
                    break;
                case EntityType.Creature:
                    loot = (unit as Creature)?.CreatureInfo.LootInfo;
                    break;
                default:
                    loot = null;
                    break;
            }
            return loot;
        }

        public Loot CreateLoot(Player player, Creature creature, LootType type)
        {
            return new Loot(player, creature, type);
        }
        
        public Loot CreateLoot(Player player, ulong networkId , LootType type)
        {
            return new Loot(player, networkId, type);
        }

        internal void DoUpdate(int timeDiff)
        {
            
        }
        
        public void Dispose()
        {
            
        }
    }
}