using Common;
using JetBrains.Annotations;
using UnityEngine;


namespace Core.Scenario
{
    public class SpawnUnit : ScenarioAction
    {
        [SerializeField, UsedImplicitly] private CreatureInfo creatureInfo;
        [SerializeField, UsedImplicitly] private CustomSpawnSettings customSpawnSettings;

        [SerializeField, UsedImplicitly] private Team team;
        
        internal virtual void SpawnUnitByInfo(uint count = 0)
        {
            Debug.Log("SpawnCreatureByInfo");

            switch (creatureInfo.EntityType)
            {
                case EntityType.Creature:
                    SpawnCreature(count);
                    break;
                case EntityType.Item:
                    SpawnItem(count);
                    break;
                case EntityType.Player:
                    SpawnPlayer(count);
                    break;
            }
        }

        private void SpawnPlayer(uint count = 0)
        {
            for (int i = 0; i < count; i++)
            {
                var playerCreateToken = new Player.CreateToken
                {
                    Position = customSpawnSettings.SpawnPoint.position,
                    Rotation = customSpawnSettings.SpawnPoint.rotation,
                    OriginalAIInfoId = customSpawnSettings.UnitInfoAI?.Id ?? 0,
                    DeathState = DeathState.Alive,
                    FreeForAll = true,
                    ModelId = 1,
                    ClassType = ClassType.Mage,
                    OriginalModelId = 1,
                    FactionId = Balance.DefaultFaction.FactionId,
                    PlayerName = customSpawnSettings.CustomNameId,
                    Scale = customSpawnSettings.CustomScale,
                    EntityType =  creatureInfo.EntityType
                };
                World.UnitManager.Create<Player>(BoltPrefabs.Player, playerCreateToken);
            }
        }
        private void SpawnItem(uint count = 0)
        {
            for (int i = 0; i < count; i++)
            {
                Creature creature = World.UnitManager.Create<Creature>(BoltPrefabs.Item, new Creature.CreateToken
                {
                    Position = customSpawnSettings.SpawnPoint.position +
                               new Vector3(
                                   Random.Range(-customSpawnSettings.MinSpawnRadius,
                                       customSpawnSettings.MaxSpawnRadius), 0,
                                   Random.Range(-customSpawnSettings.MinSpawnRadius,
                                       customSpawnSettings.MaxSpawnRadius)),
                    Rotation = customSpawnSettings.SpawnPoint.rotation,
                    OriginalAIInfoId = customSpawnSettings?.UnitInfoAI != null ? customSpawnSettings.UnitInfoAI.Id : 0,
                    DeathState = DeathState.Alive,
                    FreeForAll = true,
                    ClassType = ClassType.Warrior,
                    ModelId = creatureInfo.ModelId,
                    OriginalModelId = creatureInfo.ModelId,
                    FactionId = (int) team,
                    CreatureInfoId = creatureInfo.Id,
                    CustomName = string.IsNullOrEmpty(customSpawnSettings.CustomNameId)
                        ? creatureInfo.CreatureName
                        : customSpawnSettings.CustomNameId,
                    Scale = customSpawnSettings.CustomScale,
                    CategoryFlags = creatureInfo.CategoryFlags,
                    TriggerEvent = creatureInfo.TriggerEvent,
                    EntityType =  creatureInfo.EntityType,
                });
                ;

                creature.BoltEntity.TakeControl();
            }
        }
        private void SpawnCreature(uint count = 0)
        {
            for (int i = 0; i < count; i++)
            {
                Creature creature = World.UnitManager.Create<Creature>(BoltPrefabs.Creature, new Creature.CreateToken
                {
                    Position = customSpawnSettings.SpawnPoint.position +
                               new Vector3(
                                   Random.Range(-customSpawnSettings.MinSpawnRadius,
                                       customSpawnSettings.MaxSpawnRadius), 0,
                                   Random.Range(-customSpawnSettings.MinSpawnRadius,
                                       customSpawnSettings.MaxSpawnRadius)),
                    Rotation = customSpawnSettings.SpawnPoint.rotation,
                    OriginalAIInfoId = customSpawnSettings?.UnitInfoAI != null ? customSpawnSettings.UnitInfoAI.Id : 0,
                    DeathState = DeathState.Alive,
                    FreeForAll = true,
                    ClassType = ClassType.Warrior,
                    ModelId = creatureInfo.ModelId,
                    OriginalModelId = creatureInfo.ModelId,
                    FactionId = (int) team,
                    CreatureInfoId = creatureInfo.Id,
                    CustomName = string.IsNullOrEmpty(customSpawnSettings.CustomNameId)
                        ? creatureInfo.CreatureName
                        : customSpawnSettings.CustomNameId,
                    Scale = customSpawnSettings.CustomScale,
                    CategoryFlags = creatureInfo.CategoryFlags,
                    TriggerEvent = creatureInfo.TriggerEvent,
                    EntityType =  creatureInfo.EntityType,
                });
                ;

                creature.BoltEntity.TakeControl();
            }
        }
    }
}