using UnityEngine;

namespace Core
{
    public class DynamicObject : WorldEntity
    {
        internal override bool AutoScoped  => false;
        public override string Name { get; internal set; }
    }
}