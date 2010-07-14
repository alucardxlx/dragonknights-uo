// Rocko's AOE Dragon.

using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an ice dragon corpse")]
    public class IceDragon : BaseCreature
    {
        [Constructable]
        public IceDragon()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "an ice dragon";
            Body = Core.AOS ? 180 : 49;
            Hue = 1266;
            BaseSoundID = 362;

            SetStr(796, 825);
            SetDex(106, 300);
            SetInt(636, 675);

            SetHits(2500, 3000);
            SetStam(100, 600);

            SetDamage(20, 26);

            SetDamageType(ResistanceType.Cold, 60);
            SetDamageType(ResistanceType.Physical, 40);

            SetResistance(ResistanceType.Physical, 55, 65);
            SetResistance(ResistanceType.Cold, 75, 85);
            SetResistance(ResistanceType.Fire, 35, 45);
            SetResistance(ResistanceType.Poison, 45, 55);
            SetResistance(ResistanceType.Energy, 45, 55);

            SetSkill(SkillName.EvalInt, 45.1, 55.0);
            SetSkill(SkillName.Magery, 45.1, 55.0);
            SetSkill(SkillName.MagicResist, 99.1, 100.0);
            SetSkill(SkillName.Tactics, 97.6, 100.0);
            SetSkill(SkillName.Wrestling, 90.1, 92.5);

            Fame = 25000;
            Karma = -25000;

            VirtualArmor = 70;
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 2);
            AddLoot(LootPack.Gems, 8);
        }

        public override int BreathColdDamage { get { return 100; } }
        public override int BreathFireDamage { get { return 0; } }
        public override double BreathDamageScalar { get { return (Core.AOS ? 0.10 : 0.05); } }

        public override bool HasAOEBreath { get { return true; } }
        public override string AOEBreathName { get { return "ice storm"; } }
        public override int AOEBreathRadius { get { return 4; } }
        public override int AOEBreathDuration { get { return 2; } }
        public override int AOEBreathEffectItemID { get { return 0x376A; } }
        public override int AOEBreathEffectHue { get { return 1152; } }

        public override void AOESpecialEffect(Mobile m)
        {
            int duration = 5;
            if (m.ColdResistance > Utility.Random(50) + 40)
                duration = 4;

            m.Freeze(TimeSpan.FromSeconds(duration));
            m.SendMessage("You are chilled to the bones.");
            m.HueMod = 1152;
            Timer.DelayCall(TimeSpan.FromSeconds(duration), new TimerStateCallback(m_RestoreHue), m);
        }

        private void m_RestoreHue(object m)
        {
            Mobile from = (Mobile)m;
            if (from!=null && !from.Deleted)
            from.HueMod = -1;
        }

        public override bool ReacquireOnMovement { get { return true; } }
        public override bool HasBreath { get { return true; } } // fire breath enabled
        public override bool AutoDispel { get { return true; } }
        public override int TreasureMapLevel { get { return 5; } }
        public override int Meat { get { return 25; } }
        public override int Hides { get { return 30; } }
        public override HideType HideType { get { return HideType.Barbed; } }
        public override int Scales { get { return 15; } }
        public override ScaleType ScaleType { get { return ScaleType.White; } }

        public IceDragon(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}