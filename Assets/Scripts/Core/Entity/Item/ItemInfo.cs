using Common;
using JetBrains.Annotations;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "Item Info", menuName = "Game Data/Entities/Item Info", order = 2)]
    public sealed class ItemInfo : ScriptableUniqueInfo<ItemInfo>
    {
        [SerializeField, UsedImplicitly] private ItemInfoContainer container;
        [SerializeField, UsedImplicitly] private uint displayId;
        [SerializeField, UsedImplicitly] private string itemName;
        
        protected override ScriptableUniqueInfoContainer<ItemInfo> Container => container;
        protected override ItemInfo Data => this;
        public new int Id => base.Id;
        public uint DisplayId => displayId;
        public string ItemName => itemName;
    }
}