using Bolt;
using Common;
using Core;
using System;

namespace Server
{
    internal class GamePlayerListener : BaseGameListener
    {
        internal GamePlayerListener(WorldServer world) : base(world)
        {
            Common.EventHandler.RegisterEvent<Player, UnitMoveType, float>(GameEvents.ServerPlayerSpeedChanged, OnPlayerSpeedChanged);
            Common.EventHandler.RegisterEvent<Player, bool>(GameEvents.ServerPlayerRootChanged, OnPlayerRootChanged);
            Common.EventHandler.RegisterEvent<Player, bool>(GameEvents.ServerPlayerMovementControlChanged, OnPlayerMovementControlChanged);

            Common.EventHandler.RegisterEvent<Player, Creature>(GameEvents.ServerPickItem, OnPlayerPickItem);
            Common.EventHandler.RegisterEvent<Team>(GameEvents.ServerInstanceFinished, OnServerInstanceFinished);
        }

       

        internal void Dispose()
        {
            Common.EventHandler.UnregisterEvent<Player, UnitMoveType, float>(GameEvents.ServerPlayerSpeedChanged, OnPlayerSpeedChanged);
            Common.EventHandler.UnregisterEvent<Player, bool>(GameEvents.ServerPlayerRootChanged, OnPlayerRootChanged);
            Common.EventHandler.UnregisterEvent<Player, bool>(GameEvents.ServerPlayerMovementControlChanged, OnPlayerMovementControlChanged);

            Common.EventHandler.UnregisterEvent<Player, Creature>(GameEvents.ServerPickItem, OnPlayerPickItem);
            Common.EventHandler.RegisterEvent<Team>(GameEvents.ServerInstanceFinished, OnServerInstanceFinished);

        }

        private void OnPlayerSpeedChanged(Player player, UnitMoveType moveType, float rate)
        {
            if (player.BoltEntity.Controller != null)
            {
                PlayerSpeedRateChangedEvent speedChangeEvent = PlayerSpeedRateChangedEvent.Create(player.BoltEntity.Controller, ReliabilityModes.ReliableOrdered);
                speedChangeEvent.MoveType = (int) moveType;
                speedChangeEvent.SpeedRate = rate;
                speedChangeEvent.Send();
            }
        }

        private void OnPlayerRootChanged(Player player, bool applied)
        {
            if (player.BoltEntity.Controller != null)
            {
                PlayerRootChangedEvent rootChangedEvent = PlayerRootChangedEvent.Create(player.BoltEntity.Controller, ReliabilityModes.ReliableOrdered);
                rootChangedEvent.Applied = applied;
                rootChangedEvent.Send();
            }
        }

        private void OnPlayerMovementControlChanged(Player player, bool hasControl)
        {
            if (player.BoltEntity.Controller != null)
            {
                PlayerMovementControlChanged movementControlEvent = PlayerMovementControlChanged.Create(player.BoltEntity.Controller, ReliabilityModes.ReliableOrdered);
                movementControlEvent.PlayerHasControl = hasControl;
                movementControlEvent.LastServerPosition = player.Position;
                movementControlEvent.LastServerMovementFlags = (int)player.MovementFlags;
                movementControlEvent.Send();
            }
        }

        private void OnPlayerPickItem(Player player, Creature item)
        {
            if (player.BoltEntity.Controller != null)
            {
//                 PlayerMovementControlChanged movementControlEvent = PlayerMovementControlChanged.Create(player.BoltEntity.Controller, ReliabilityModes.ReliableOrdered);
//                 movementControlEvent.PlayerHasControl = hasControl;
//                 movementControlEvent.LastServerPosition = player.Position;
//                 movementControlEvent.LastServerMovementFlags = (int)player.MovementFlags;
//                 movementControlEvent.Send();
            }
        }

        private void OnServerInstanceFinished(Team team)
        {
            //World.InstanceManager.DisposeInstance();
        }
    }
}
