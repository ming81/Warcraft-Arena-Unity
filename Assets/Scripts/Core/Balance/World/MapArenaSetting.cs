using System;
using System.Collections.Generic;
using Core.Scenario;
using JetBrains.Annotations;
using UnityEngine;

namespace Core
{
    public class MapArenaSetting : MapSettings
    {
        [Serializable]
        private class ArenaSpawnInfo
        {
            [SerializeField, UsedImplicitly] private Team team;
            [SerializeField, UsedImplicitly] private List<Transform> spawnPoints;

            public Team Team => team;
            public List<Transform> SpawnPoints => spawnPoints;
        }
        [Header("Instance Setting"), Space]
        [SerializeField, UsedImplicitly] private List<ArenaSpawnInfo> spawnInfos;
        [SerializeField, UsedImplicitly] private List<SpawnUnitOnTimer> spawnUnitOnTimers;

        [SerializeField, UsedImplicitly] private SpellInfo crystalSpellInfo;
        [SerializeField, UsedImplicitly] private SpellInfo killSpellInfo;
        [SerializeField, UsedImplicitly] private SpellInfo boxSpellInfo;
        [SerializeField, UsedImplicitly] private int instanceStep1Time;
        [SerializeField, UsedImplicitly] private int instanceStep2Time = int.MaxValue;

        internal List<SpawnUnitOnTimer> SpawnUnitOnTimers => spawnUnitOnTimers;
        
        internal SpellInfo CrystalSpellInfo => crystalSpellInfo;
        internal SpellInfo KillSpellInfo => killSpellInfo;
        internal SpellInfo BoxSpellInfo => boxSpellInfo;

        internal int InstanceStep1Time => instanceStep1Time;
        internal int InstanceStep2Time => instanceStep2Time;

#if UNITY_EDITOR
        [UsedImplicitly, ContextMenu("Collect Scenario Action")]
        private void CollectScenario()
        {
            spawnUnitOnTimers = new List<SpawnUnitOnTimer>(GetComponentsInChildren<SpawnUnitOnTimer>());
        }
#endif
        
    }
}