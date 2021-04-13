using System.Collections.Generic;
using Common;

namespace Core
{
    public class InstanceArena : Instance
    {
        private readonly Timer instanceStep1Timer = new Timer();
        private readonly Timer instanceStep2Timer = new Timer();
        private readonly Timer loadingTimer = new Timer();
        
        // private uint perAddExp = 0;
        // private uint perAddMoney = 0;
        private Team victoryTeam = Team.Other;

        private readonly Dictionary<Team, Unit> baseHomes = new Dictionary<Team, Unit>();
        private readonly Dictionary<Team, int> itemCrystals = new Dictionary<Team, int>();
        private Unit baseDoor;

        private readonly MapArenaSetting settings;

        public InstanceArena(World world, Map map)
            : base(world, map)
        {
            EventHandler.RegisterEvent(World, GameEvents.ServerLaunched, OnServerLaunched);
            EventHandler.RegisterEvent<Unit, Unit>(GameEvents.ServerPickItem, OnPlayerPickItem);
            itemCrystals[Team.Alliance] = 0;
            itemCrystals[Team.Horde] = 0;

            this.settings = map.Settings as MapArenaSetting;
        }


        private void OnPlayerPickItem(Unit player, Unit creature)
        {
            //Item Crystal 
            if (creature.HasCategoryFlag(UnitCategoryFlags.ItemCrystal) &&
                player.Faction.FactionId == (int) Team.Alliance)
                itemCrystals[Team.Alliance]++;

            if (creature.HasCategoryFlag(UnitCategoryFlags.ItemCrystal) && player.Faction.FactionId == (int) Team.Horde)
                itemCrystals[Team.Horde]++;

            //Item Box
            if (creature.HasCategoryFlag(UnitCategoryFlags.ItemBox) && player.Faction.FactionId == (int) Team.Alliance)
                if (baseHomes.ContainsKey(Team.Alliance) && baseHomes[Team.Alliance] != null)
                    baseHomes[Team.Alliance].Spells.TriggerSpell(settings.BoxSpellInfo, baseHomes[Team.Alliance]);

            if (creature.HasCategoryFlag(UnitCategoryFlags.ItemBox) && player.Faction.FactionId == (int) Team.Horde)
                if (baseHomes.ContainsKey(Team.Horde) && baseHomes[Team.Horde] != null)
                    baseHomes[Team.Horde].Spells.TriggerSpell(settings.BoxSpellInfo, baseHomes[Team.Horde]);
        }

        private void OnServerLaunched()
        {
            loadingTimer.StartUp(10);
            this.IsStart = true;
        }


        public override void Dispose()
        {
            foreach (var scenarioAction in settings.SpawnUnitOnTimers)
                scenarioAction.DeInitialize();

            EventHandler.UnregisterEvent(World, GameEvents.ServerLaunched, OnServerLaunched);
            EventHandler.UnregisterEvent<Unit, Creature>(GameEvents.ServerPickItem, OnPlayerPickItem);
        }

        public override void Detach(Unit entity)
        {
            if (entity.HasCategoryFlag(UnitCategoryFlags.Home) && entity.Faction.FactionId == (int) Team.Alliance)
                baseHomes[Team.Alliance] = null;

            if (entity.HasCategoryFlag(UnitCategoryFlags.Home) && entity.Faction.FactionId == (int) Team.Horde)
                baseHomes[Team.Horde] = null;

            if (entity.HasCategoryFlag(UnitCategoryFlags.Door)) baseDoor = null;
        }

        public override void Attach(Unit entity)
        {
            if (entity.HasCategoryFlag(UnitCategoryFlags.Home) && entity.Faction.FactionId == (int) Team.Alliance)
                baseHomes[Team.Alliance] = entity;

            if (entity.HasCategoryFlag(UnitCategoryFlags.Home) && entity.Faction.FactionId == (int) Team.Horde)
                baseHomes[Team.Horde] = entity;

            if (entity.HasCategoryFlag(UnitCategoryFlags.Door)) baseDoor = entity;
        }

        public override void DoUpdate(int deltaTime)
        {
            if (IsEnd() || !IsStart) return;

            //Loading Finished
            if (loadingTimer.IsActive() && loadingTimer.GetRemain() > 0)
            {
                foreach (var scenarioAction in settings.SpawnUnitOnTimers)
                    scenarioAction.Initialize(Map);

                instanceStep1Timer.StartUp(settings.InstanceStep1Time);
                loadingTimer.Clear();
                return;
            }

            if (!loadingTimer.IsActive())
                foreach (var scenarioAction in settings.SpawnUnitOnTimers)
                    scenarioAction.DoUpdate(deltaTime);

            //Collect Crystal Finished
            if (instanceStep1Timer.IsActive() && instanceStep1Timer.GetRemain() <= 0)
            {
                //Open Door                
                if (baseDoor != null)
                    //baseHomes[Team.Alliance].Kill(baseDoor);
                    baseDoor.Spells.TriggerSpell(settings.KillSpellInfo, baseDoor);

                //Add Home Property
                if (baseHomes.ContainsKey(Team.Alliance) && baseHomes[Team.Alliance] != null)
                    for (var i = 0; i < itemCrystals[Team.Alliance]; i++)
                        baseHomes[Team.Alliance].Spells
                            .TriggerSpell(settings.CrystalSpellInfo, baseHomes[Team.Alliance]);

                //
                if (baseHomes.ContainsKey(Team.Horde) && baseHomes[Team.Horde] != null)
                    for (var i = 0; i < itemCrystals[Team.Horde]; i++)
                        baseHomes[Team.Horde].Spells.TriggerSpell(settings.CrystalSpellInfo, baseHomes[Team.Horde]);

                instanceStep1Timer.Clear();
                instanceStep2Timer.StartUp(settings.InstanceStep2Time);
            }

            if (!instanceStep2Timer.IsActive() || instanceStep2Timer.GetRemain() > 0) return;
            EndFlag = true;

            EventHandler.ExecuteEvent(GameEvents.ServerInstanceFinished, victoryTeam);
        }

        public override bool IsEnd()
        {
            if (EndFlag) return true;

            if (baseHomes.Count == 0) return false;

            if (baseHomes[Team.Alliance] == null)
            {
                EndFlag = true;
                victoryTeam = Team.Alliance;
                return true;
            }

            if (baseHomes[Team.Horde] == null)
            {
                EndFlag = true;
                victoryTeam = Team.Horde;
                return true;
            }

            return false;
        }
    }
}