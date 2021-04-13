using System;
using Common;
using JetBrains.Annotations;
using UnityEngine;


namespace Core.Scenario
{
    public class SpawnUnitOnTimer : SpawnUnit
    {
        private enum SpawnState
        {
            None = 0,
            Start = 1,
            Spawn = 2,
            End = 3,
        };

        [SerializeField, UsedImplicitly] private uint timerBegin = 0;
        [SerializeField, UsedImplicitly] private uint timerEnd = uint.MaxValue;
        [SerializeField, UsedImplicitly] private uint interval = 0;
        [SerializeField, UsedImplicitly] private uint spawnMaxTime = uint.MaxValue;
        [SerializeField, UsedImplicitly] private uint spawnLimit = uint.MaxValue;
        [SerializeField, UsedImplicitly] private uint spawnNumPerTime = 1;

        private SpawnState spawnState = SpawnState.None;
        private TimeTracker stateInterval = new TimeTracker();
        private TimeTracker spawnInterval = new TimeTracker();
        private uint spawnTime = 0;
        private uint spawnTotal = 0;


        private bool IsEnd => spawnState == SpawnState.End;
        private bool IsStart => spawnState == SpawnState.Start || spawnState == SpawnState.Spawn;
        private bool IsSpawn => spawnState == SpawnState.Spawn;


        private void End()
        {
            spawnState = SpawnState.End;
        }

        private uint SpawnCreatureTimesPlus()
        {
            spawnTime++;
            return spawnTime;
        }


        internal override void DoUpdate(int deltaTime)
        {
            if (IsEnd || !IsStart)
            {
                return;
            }

            var isSpawn = false;
            stateInterval.Update(deltaTime);
            switch (spawnState)
            {
                case SpawnState.Start:
                {
                    if (stateInterval.Passed)
                    {
                        spawnState = SpawnState.Spawn;
                        if (timerEnd == 0)
                        {
                            stateInterval.Reset(int.MaxValue);
                        }
                        else
                        {
                            stateInterval.Reset(timerEnd * 1000);
                        }

                        isSpawn = true;
                    }

                    break;
                }
                case SpawnState.Spawn when stateInterval.Passed:
                    End();
                    return;
                case SpawnState.Spawn:
                {
                    spawnInterval.Update(deltaTime);
                    if (spawnInterval.Passed)
                    {
                        spawnInterval.Reset(interval * 1000);
                        isSpawn = true;
                    }

                    break;
                }
                case SpawnState.None:
                    break;
                case SpawnState.End:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (isSpawn)
            {
                Show();
            }
        }

        private void Show()
        {
            if (IsEnd)
            {
                return;
            }

            SpawnCreatureTimesPlus();

            if (spawnTotal + spawnNumPerTime >= spawnLimit)
            {
                spawnNumPerTime = spawnLimit - spawnTotal;
                SpawnUnitByInfo(spawnNumPerTime);
                this.End();
            }
            else
            {
                SpawnUnitByInfo(spawnNumPerTime);
            }

            spawnTotal += spawnNumPerTime;

            if (spawnTotal >= spawnLimit)
            {
                this.End();
            }

            if (spawnTime >= spawnMaxTime)
            {
                this.End();
            }
        }

        internal override void Initialize(Map map)
        {
            base.Initialize(map);

            stateInterval.Reset(timerBegin * 1000);
            spawnInterval.Reset(interval * 1000);

            spawnState = SpawnState.Start;

            this.DoUpdate(0);
        }
    }
}