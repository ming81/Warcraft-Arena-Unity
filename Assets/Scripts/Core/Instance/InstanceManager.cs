using System.Collections.Generic;
using System.Threading;
using Common;

namespace Core
{
    public class InstanceManager
    {
        private readonly Dictionary<uint, Instance> baseInstances = new Dictionary<uint, Instance>();
        private readonly Mutex instancesLock = new Mutex(true);

        private World world;

        internal InstanceManager(World world)
        {
            this.world = world;

        }

        internal void Dispose()
        {
            foreach (var mapEntry in baseInstances)
                mapEntry.Value.Dispose();

            baseInstances.Clear();

            world = null;
        }


        public Instance CreateInstance(Map map, MapSettings settings)
        {
            Instance instance = baseInstances.LookupEntry(map.MapId);

            if (instance == null)
            {
                instancesLock.WaitOne();

                switch (settings.Definition.MapType)
                {
                    case MapType.Instance:
                        instance = new Instance(world, map);
                        break;
                    case MapType.Raid:
                        instance = new Instance(world, map);
                        break;
                    case MapType.Battleground:
                        instance = new Instance(world, map);
                        break;
                    case MapType.Arena:
                        instance = new InstanceArena(world, map);
                        break;
                    case MapType.Scenario:
                        instance = new Instance(world, map);
                        break;
                }

                baseInstances[map.MapId] = instance;

                instancesLock.ReleaseMutex();
            }

            Assert.IsNotNull(instance);

            return instance;
        }

        public void DisposeInstance(Map map)
        {
            Instance instance = baseInstances.LookupEntry(map.MapId);
            Assert.IsNotNull(instance);

            instance.Dispose();
        }

        public void Attach(Unit entity, Map map)
        {
            Instance instance = baseInstances.LookupEntry(map.MapId);
            Assert.IsNotNull(instance);

            instance.Attach(entity);
        }

        public void Detach(Unit entity, Map map)
        {
            Instance instance = baseInstances.LookupEntry(map.MapId);
            Assert.IsNotNull(instance);
            instance.Detach(entity);
        }
    }
    
}