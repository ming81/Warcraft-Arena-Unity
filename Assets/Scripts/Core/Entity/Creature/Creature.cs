﻿using Common;
using JetBrains.Annotations;
using UdpKit;
using UnityEngine;

namespace Core
{
    public class Creature : Unit
    {
        public new class CreateToken : Unit.CreateToken
        {
            public string CustomName { get; set; } = string.Empty;
            public int CreatureInfoId { get; set; }
            public GameEvents TriggerEvent{ get; set; }

            public override void Read(UdpPacket packet)
            {
                base.Read(packet);

                CustomName = packet.ReadString();
                CreatureInfoId = packet.ReadInt();
                TriggerEvent = (GameEvents)packet.ReadInt();
            }

            public override void Write(UdpPacket packet)
            {
                base.Write(packet);

                packet.WriteString(CustomName);
                packet.WriteInt(CreatureInfoId);
                packet.WriteInt((int)TriggerEvent);
            }

            public void Attached(Creature creature)
            {
                base.Attached(creature);

                creature.Name = CustomName;
                creature.creatureInfo = creature.Balance.CreatureInfoById[CreatureInfoId];
                creature.TriggerEvent = TriggerEvent;
            }
        }

        [SerializeField, UsedImplicitly, Header(nameof(Creature)), Space(10)]
        private CreatureAI creatureAI;

        private CreateToken createToken;
        private CreatureInfo creatureInfo;
        private string creatureName;
        private GameEvents TriggerEvent { get; set; }

        internal CreatureAI CreatureAI => creatureAI;
        internal CreatureInfo CreatureInfo => creatureInfo;
        internal override UnitAI AI => creatureAI;
        internal override bool AutoScoped => false;

        public override string Name { get => creatureName; internal set => creatureName = value; }

        protected override void HandleAttach()
        {
            base.HandleAttach();

            createToken = (CreateToken)entity.AttachToken;
            createToken.Attached(this);

            if (creatureInfo.VehicleInfo != null)
                CreateVehicle(creatureInfo.VehicleInfo, creatureInfo);

            Attributes.UpdateAvailablePowers();
        }

        protected override void HandleDetach()
        {
            createToken = null;
            creatureInfo = null;

            base.HandleDetach();
        }

        public void Accept(IUnitVisitor unitVisitor)
        {
            unitVisitor.Visit(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"OnTriggerEnter{this.CategoryFlags }");
            if (TriggerEvent == 0) return;

            if (!World.UnitManager.TryFind(other, out var unit)) return;
            if (!this.IsAlive || !this.HasCategoryFlag(UnitCategoryFlags.ItemCrystal) || (!(unit is Player))) return;
            
            EventHandler.ExecuteEvent(TriggerEvent, unit, this as Unit);
            Kill(this);
        }
    }
}