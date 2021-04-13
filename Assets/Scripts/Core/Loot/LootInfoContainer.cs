using System.Collections.Generic;
using Common;
using JetBrains.Annotations;
using UnityEngine;

namespace Core
{
    [UsedImplicitly, CreateAssetMenu(fileName = "Loot Info Container", menuName = "Game Data/Containers/Loot Info", order = 1)]
    public class LootInfoContainer : ScriptableUniqueInfoContainer<LootInfo>
    {
        [SerializeField, UsedImplicitly] private List<LootInfo> lootInfos;

        protected override List<LootInfo> Items => lootInfos;

        private readonly Dictionary<int, LootInfo> lootInfoById = new Dictionary<int, LootInfo>();
 
        public IReadOnlyDictionary<int, LootInfo> LootInfoById => lootInfoById;

        public override void Register()
        {
            base.Register();

            lootInfos.ForEach(lootInfo => lootInfoById.Add(lootInfo.Id, lootInfo));
        }

        public override void Unregister()
        {
            lootInfoById.Clear();

            base.Unregister();
        }
    }
   
}