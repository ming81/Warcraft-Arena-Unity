using System;
using System.Collections.Generic;
using Core.Scenario;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core
{
    public class MapSettings : MonoBehaviour
    {
        [SerializeField, UsedImplicitly, Range(2.0f, 50.0f)] private float gridCellSize;
        [SerializeField, UsedImplicitly] private Transform defaultSpawnPoint;
        [SerializeField, UsedImplicitly] private BoxCollider boundingBox;
        [SerializeField, UsedImplicitly] private BalanceReference balance;
        [SerializeField, UsedImplicitly] private MapDefinition mapDefinition;
        
        internal float GridCellSize => gridCellSize;
        internal BoxCollider BoundingBox => boundingBox;
        internal Transform DefaultSpawnPoint => defaultSpawnPoint;
        internal BalanceReference Balance => balance;
        internal MapDefinition Definition => mapDefinition;
        
        public virtual Transform FindSpawnPoints(Team team)
        {
            return defaultSpawnPoint;
        }
    }
}