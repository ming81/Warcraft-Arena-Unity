using System;
using System.Collections.Generic;
using Common;
using JetBrains.Annotations;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "Loot Info", menuName = "Game Data/Entities/Loot Info", order = 2)]
    public sealed class LootInfo : ScriptableUniqueInfo<LootInfo>
    {
        
        public enum LootItemType
        {
            Item         = 1,
            Gold         = 2,
        };

        [Serializable]
        public class LootItemInfo
        {
            [SerializeField, UsedImplicitly] private uint itemId;
            [SerializeField, UsedImplicitly] private LootItemType itemType;
            [SerializeField, UsedImplicitly] private float chance;
            [SerializeField, UsedImplicitly] private int minCount;
            [SerializeField, UsedImplicitly] private int maxCount;
            
            public uint ItemId => itemId;
            public LootItemType ItemType => itemType;
            public float Chance => chance;
            public int MinCount => minCount;
            public int MaxCount => maxCount;
        }

        [SerializeField, UsedImplicitly] private LootInfoContainer container;
        [SerializeField, UsedImplicitly] private List<LootItemInfo> lootItemInfos;
        
        protected override ScriptableUniqueInfoContainer<LootInfo> Container => container;
        protected override LootInfo Data => this;
        public new int Id => base.Id;

        public List<LootItemInfo> LootItemInfos => lootItemInfos;
    }
}