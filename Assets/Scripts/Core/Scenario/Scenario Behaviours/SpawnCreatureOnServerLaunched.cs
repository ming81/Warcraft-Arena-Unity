using Common;
using JetBrains.Annotations;
using System;
using UnityEngine;


namespace Core.Scenario
{
    public class SpawnCreatureOnServerLaunched : SpawnCreature
    {
        internal override void Initialize(Map map)
        {
            base.Initialize(map);

            Common.EventHandler.RegisterEvent(World, GameEvents.ServerLaunched, OnServerLaunched);
        }

        private void OnServerLaunched()
        {
            SpawnCreatureByInfo();
        }

        internal override void DeInitialize()
        {
            Common.EventHandler.UnregisterEvent(World, GameEvents.ServerLaunched, OnServerLaunched);

            base.DeInitialize();
        }
    }
}