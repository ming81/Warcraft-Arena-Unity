using Common;
using JetBrains.Annotations;
using UnityEngine;


namespace Core.Scenario
{
    public class SpawnCreature : ScenarioAction
    {
        [SerializeField, UsedImplicitly] private CreatureInfo creatureInfo;
        [SerializeField, UsedImplicitly] private CustomSpawnSettings customSpawnSettings;

        [SerializeField, UsedImplicitly] private Team team;
        
        internal virtual void SpawnCreatureByInfo(uint creatureCount = 0)
        {
            Debug.Log("SpawnCreatureByInfo");

            for (int i = 0; i < creatureCount; i++)
            {
                Creature creature = World.UnitManager.Create<Creature>(creatureInfo.PrefabId, new Creature.CreateToken
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
                    CategoryFlags = creatureInfo.CategoryFlags
                });
                ;

                creature.BoltEntity.TakeControl();
            }
        }
    }
}