using JetBrains.Annotations;
using UnityEngine;

namespace Core.Conditions
{
    [UsedImplicitly, CreateAssetMenu(fileName = "Target Unit Is ItemCrystal", menuName = "Game Data/Conditions/Unit/Target Is ItemCrystal", order = 5)]
    public sealed class TargetUnitIsItemCrystal : Condition
    {
        protected override bool IsApplicable => base.IsApplicable && TargetUnit != null;

        protected override bool IsValid => base.IsValid && TargetUnit is Creature && TargetUnit.HasCategoryFlag(UnitCategoryFlags.ItemCrystal);
    }
}
