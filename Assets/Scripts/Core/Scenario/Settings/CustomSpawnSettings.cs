using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Core.Scenario
{
    [Serializable]
    public class CustomSpawnSettings
    {
        [SerializeField, UsedImplicitly] private Transform spawnPoint;
        [SerializeField, UsedImplicitly] private float minSpawnRadius;
        [SerializeField, UsedImplicitly] private float maxSpawnRadius;
        [SerializeField, UsedImplicitly] private UnitInfoAI unitInfoAI;
        [SerializeField, UsedImplicitly] private string customNameId;
        [SerializeField, UsedImplicitly] private float customScale = 1.0f;

        public Transform SpawnPoint => spawnPoint;
        public float MinSpawnRadius => minSpawnRadius;
        public float MaxSpawnRadius => maxSpawnRadius;
        public UnitInfoAI UnitInfoAI => unitInfoAI;
        public string CustomNameId => customNameId;
        public float CustomScale => customScale;
    }
}
