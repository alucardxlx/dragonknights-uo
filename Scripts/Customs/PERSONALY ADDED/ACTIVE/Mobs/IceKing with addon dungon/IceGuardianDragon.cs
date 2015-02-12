using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a ice guardian dragon corpse")]
    public class IceGuardianDragon : BaseCreature
    {
        [Constructable]
        public IceGuardianDragon() : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a guardian ice dragon";
            Body = Core.AOS ? 198 : 49;
            Hue = 2063;
            BaseSoundID = 362;
            Tamable = false;

            SetStr(796, 825);
            SetDex(106, 300);
            SetInt(636, 675);

//            SetHits(2500, 3000);
            SetHits(35000);
            SetStam(100, 600);

            SetDamage(21, 28);

            SetDamageType(ResistanceType.Cold, 100);
            SetDamageType(ResistanceType.Physical, 40);

            SetResistance(ResistanceType.Physical, 55, 65);
            SetResistance(ResistanceType.Cold, 90, 100);
            SetResistance(ResistanceType.Fire, 0, 1);
            SetResistance(ResistanceType.Poison, 45, 55);
            SetResistance(ResistanceType.Energy, 45, 55);

            SetSkill(SkillName.EvalInt, 45.1, 55.0);
            SetSkill(SkillName.Magery, 45.1, 55.0);
            SetSkill(SkillName.MagicResist, 50.1, 100.0);
            SetSkill(SkillName.Tactics, 97.6, 100.0);
            SetSkill(SkillName.Wrestling, 90.1, 92.5);

            SetSkill(SkillName.Healing, 90, 100);
            SetSkill( SkillName.Meditation, 90, 100);
            
            

            Fame = 25000;
            Karma = -25000;

            VirtualArmor = 70;
        }
        
        public override void OnDamage(int amount, Mobile from, bool willKill)
        {
            if (this.Hits <= 100)
            {
                if (from is BaseCreature)
                {
                    BaseCreature pet = (BaseCreature)from;
                    if (pet.ControlMaster != null)
                    {
                        from = pet.ControlMaster;
                        this.FocusMob = pet.ControlMaster;
                    }
                }

                this.Hidden = true;

                switch (Utility.Random(8))
                {
                    case 0:

                        this.X = from.X;
                        this.Y = from.Y;
                        this.Hits = this.Hits + Utility.RandomMinMax(20, 27);

                        break;
                    case 1:

                        this.X = from.X;
                        this.Y = from.Y;
                        this.Hits = this.Hits + Utility.RandomMinMax(50, 90);

                        break;
                    case 2:

                        this.X = from.X;
                        this.Y = from.Y;
                        this.Hits = this.Hits + Utility.RandomMinMax(50, 90);

                        break;
                    case 3:

                        this.X = from.X;
                        this.Y = from.Y;
                        this.Hits = this.Hits + Utility.RandomMinMax(4, 12);

                        break;
                    case 4:

                        this.X = from.X;
                        this.Y = from.Y;
                        this.Hits = this.Hits + Utility.RandomMinMax(5, 10);

                        break;
                    case 5:

                        this.X = from.X;
                        this.Y = from.Y;
                        this.Hits = this.Hits + Utility.RandomMinMax(18, 28);

                        break;
                    case 6:

                        this.X = from.X;
                        this.Y = from.Y;
                        this.Hits = this.Hits + Utility.RandomMinMax(2, 3);

                        break;
                    case 7:

                        this.X = from.X;
                        this.Y = from.Y;
                        this.Hits = this.Hits + Utility.RandomMinMax(10, 20);

                        break;
                }

            }
            else if (this.Hits >= 101)
            {
                this.CantWalk = false;
                this.Hidden = false;
            }
        }
        
        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 6);
            AddLoot(LootPack.Gems, 10);
        }
        
        public override int BreathColdDamage { get { return 100; } }
        public override int BreathFireDamage { get { return 0; } }
        public override double BreathDamageScalar { get { return (Core.AOS ? 0.10 : 0.05); } }
        public override bool HasAOEBreath { get { return true; } }
        public override string AOEBreathName { get { return "ice storm"; } }
        public override int AOEBreathRadius { get { return 4; } }
        public override int AOEBreathDuration { get { return 2; } }
        public override int AOEBreathEffectItemID { get { return 0x376A; } }
        public override int AOEBreathEffectHue { get { return 2063; } }
        
        public override void AOESpecialEffect(Mobile m)
        {
            int duration = 5;
            if (m.ColdResistance > Utility.Random(50) + 40)
                duration = 4;

            m.Freeze(TimeSpan.FromSeconds(duration));
            m.SendMessage("You are chilled to the bones.");
            m.HueMod = 2063;
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
        public override int TreasureMapLevel { get { return 6; } }
        public override int Meat { get { return 25; } }
        public override int Hides { get { return 30; } }
        public override HideType HideType { get { return HideType.Spined; } }
        public override int Scales { get { return 15; } }
        public override ScaleType ScaleType { get { return ScaleType.White; } }


        public IceGuardianDragon(Serial serial) : base(serial)
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