using UnityEngine;

namespace Core
{
    public class Corpse : WorldEntity
    {
        internal override bool AutoScoped  => false;
        public override string Name { get; internal set; }
    }
}