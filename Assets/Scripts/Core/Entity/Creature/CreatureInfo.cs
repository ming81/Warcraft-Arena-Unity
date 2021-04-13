using Bolt;
using Common;
using JetBrains.Annotations;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "Creature Info", menuName = "Game Data/Entities/Creature Info", order = 2)]
    internal sealed class CreatureInfo : ScriptableUniqueInfo<CreatureInfo>
    {
        [SerializeField, UsedImplicitly] private CreatureInfoContainer container;
        [SerializeField, UsedImplicitly] private VehicleInfo vehicleInfo;
        [SerializeField, UsedImplicitly] private LootInfo lootInfo;
        [UsedImplicitly, SerializeField] private string creatureName;
        [UsedImplicitly, SerializeField] private int modelId;
        [UsedImplicitly, SerializeField] private UnitCategoryFlags categoryFlags;
        [UsedImplicitly, SerializeField] private EntityType entityType;
        [UsedImplicitly, SerializeField] private GameEvents triggerEvent;
        
        protected override ScriptableUniqueInfoContainer<CreatureInfo> Container => container;
        protected override CreatureInfo Data => this;

        public new int Id => base.Id;
        public int ModelId => modelId;
        public string CreatureName => creatureName;
        public VehicleInfo VehicleInfo => vehicleInfo;
        public UnitCategoryFlags CategoryFlags => categoryFlags;
        public EntityType EntityType => entityType;
        public GameEvents TriggerEvent => triggerEvent;
        public LootInfo LootInfo => lootInfo;
    }
}
