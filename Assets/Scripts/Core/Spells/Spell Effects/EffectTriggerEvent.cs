using Common;
using JetBrains.Annotations;
using UnityEngine;

namespace Core
{
    [UsedImplicitly, CreateAssetMenu(fileName = "Effect Trigger Event", menuName = "Game Data/Spells/Effects/Trigger Event", order = 6)]
    public class EffectTriggerEvent : SpellEffectInfo
    {
        [Header("Trigger Event")]
        [SerializeField, UsedImplicitly]
        private GameEvents casterEvent;
        [SerializeField, UsedImplicitly]
        private GameEvents targetEvent;
        [SerializeField, UsedImplicitly]
        private GameEvents gloabalEvent;
        
        public GameEvents CasterEvent => casterEvent;
        public GameEvents TargetEvent => targetEvent;
        public GameEvents GloabalEvent => gloabalEvent;
        public override float Value => 1.0f;
        public override SpellEffectType EffectType => SpellEffectType.TriggerEvent;

        internal override void Handle(Spell spell, int effectIndex, Unit target, SpellEffectHandleMode mode)
        {
            spell.EffectTriggerEvent(this, target, mode);
        }
    }

    public partial class Spell
    {
        internal void EffectTriggerEvent(EffectTriggerEvent effect, Unit target, SpellEffectHandleMode mode)
        {
            if (mode != SpellEffectHandleMode.HitFinal || target == null || !target.IsAlive)
                return;

            if(effect.CasterEvent > 0)
                Common.EventHandler.ExecuteEvent(Caster, effect.CasterEvent, Caster, target);
            if (effect.TargetEvent > 0)
                Common.EventHandler.ExecuteEvent(target, effect.TargetEvent, Caster, target);
            if (effect.GloabalEvent > 0)
                Common.EventHandler.ExecuteEvent(effect.GloabalEvent, Caster, target);
        }
    }
}
