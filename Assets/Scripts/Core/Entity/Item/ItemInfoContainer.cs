using System.Collections.Generic;
using Common;
using JetBrains.Annotations;
using UnityEngine;

namespace Core
{
    [UsedImplicitly, CreateAssetMenu(fileName = "Item Info Container", menuName = "Game Data/Containers/Item Info", order = 1)]
    public class ItemInfoContainer : ScriptableUniqueInfoContainer<ItemInfo>
    {
        [SerializeField, UsedImplicitly] private List<ItemInfo> itemInfos;

        protected override List<ItemInfo> Items => itemInfos;

        private readonly Dictionary<int, ItemInfo>  itemInfoById = new Dictionary<int, ItemInfo>();
 
        public IReadOnlyDictionary<int, ItemInfo> ItemInfoById => itemInfoById;

        public override void Register()
        {
            base.Register();

            itemInfos.ForEach(itemInfo => itemInfoById.Add(itemInfo.Id, itemInfo));
        }

        public override void Unregister()
        {
            itemInfoById.Clear();

            base.Unregister();
        }
    }
   
}