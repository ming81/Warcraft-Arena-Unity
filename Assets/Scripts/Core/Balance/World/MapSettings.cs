﻿using System;
using System.Collections.Generic;
using Core.Scenario;
using JetBrains.Annotations;
using UnityEngine;

namespace Core
{
    public class MapSettings : MonoBehaviour
    {
        [Serializable]
        private class ArenaSpawnInfo
        {
            [SerializeField, UsedImplicitly] private Team team;
            [SerializeField, UsedImplicitly] private List<Transform> spawnPoints;

            public Team Team => team;
            public List<Transform> SpawnPoints => spawnPoints;
        }

        [SerializeField, UsedImplicitly, Range(2.0f, 50.0f)] private float gridCellSize;
        [SerializeField, UsedImplicitly] private Transform defaultSpawnPoint;
        [SerializeField, UsedImplicitly] private BoxCollider boundingBox;
        [SerializeField, UsedImplicitly] private BalanceReference balance;
        [SerializeField, UsedImplicitly] private MapDefinition mapDefinition;
        [SerializeField, UsedImplicitly] private List<ArenaSpawnInfo> spawnInfos;
        [SerializeField, UsedImplicitly] private List<SpawnCreatureOnTimer> spawnCreatureOnTimers;
        [SerializeField, UsedImplicitly] private List<SpawnCreatureOnServerLaunched> spawnCreatureOnServerLauncheds;

        [SerializeField, UsedImplicitly] private SpellInfo crystalSpellInfo;
        [SerializeField, UsedImplicitly] private SpellInfo killSpellInfo;
        [SerializeField, UsedImplicitly] private SpellInfo boxSpellInfo;
        [SerializeField, UsedImplicitly] private int instanceStep1Time;
        [SerializeField, UsedImplicitly] private int instanceStep2Time = int.MaxValue;

        internal float GridCellSize => gridCellSize;
        internal BoxCollider BoundingBox => boundingBox;
        internal Transform DefaultSpawnPoint => defaultSpawnPoint;
        internal BalanceReference Balance => balance;
        internal MapDefinition Definition => mapDefinition;
        internal List<SpawnCreatureOnTimer> SpawnCreatureOnTimers => spawnCreatureOnTimers;
        internal List<SpawnCreatureOnServerLaunched> SpawnCreatureOnServerLaunched => spawnCreatureOnServerLauncheds;
        internal SpellInfo CrystalSpellInfo => crystalSpellInfo;
        internal SpellInfo KillSpellInfo => killSpellInfo;
        internal SpellInfo BoxSpellInfo => boxSpellInfo;

        internal int InstanceStep1Time => instanceStep1Time;
        internal int InstanceStep2Time => instanceStep2Time;

        public List<Transform> FindSpawnPoints(Team team)
        {
            return spawnInfos.Find(spawnInfo => spawnInfo.Team == team).SpawnPoints;
        }

#if UNITY_EDITOR
        [UsedImplicitly, ContextMenu("Collect Scenario Action")]
        private void CollectScenario()
        {
            spawnCreatureOnTimers = new List<SpawnCreatureOnTimer>(GetComponentsInChildren<SpawnCreatureOnTimer>());
            spawnCreatureOnServerLauncheds = new List<SpawnCreatureOnServerLaunched>(GetComponentsInChildren<SpawnCreatureOnServerLaunched>());
        }
#endif
    }
}