using JetBrains.Annotations;
using UnityEngine;

namespace Core.Conditions
{
    [UsedImplicitly, CreateAssetMenu(fileName = "Target Unit Is ItemBox", menuName = "Game Data/Conditions/Unit/Target Is ItemBox", order = 6)]
    public sealed class TargetUnitIsItemBox : Condition
    {
        protected override bool IsApplicable => base.IsApplicable && TargetUnit != null;

        protected override bool IsValid => base.IsValid && TargetUnit is Creature && TargetUnit.HasCategoryFlag(UnitCategoryFlags.ItemBox);

    }
}
