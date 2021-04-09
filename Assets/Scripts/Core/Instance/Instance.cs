namespace Core
{
    public class Instance
    {
        protected World World { get; }
        protected Map Map { get; }
        private uint InstanceId { get; }
        protected bool EndFlag { get; set; }
        protected bool IsStart { get; set; }

        public Instance(World world, Map map)
        {
            this.World = world;
            this.InstanceId = map.MapId;
            this.Map = map;
            this.EndFlag = false;
            this.IsStart = false;
        }

        public virtual void Dispose()
        {
        }

        public virtual void Detach(Unit entity)
        {
        }

        public virtual void Attach(Unit entity)
        {
        }

        public virtual void DoUpdate(int deltaTime)
        {
        }

        public virtual bool IsEnd()
        { 
            return false;
        }
    }
}