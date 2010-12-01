using System;
using Server.Items;
using Server.Targeting;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName( "a medusa corpse" )]
	public class Medusa : BaseCreature
	{
		[Constructable]
		public Medusa() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Medusa";
			Body = 728; 

			SetStr( 1235, 1391 );
			SetDex( 128, 139 );
			SetInt( 537, 664 );

			SetHits( 70000 );

			SetDamage( 21, 28 );

			SetDamageType( ResistanceType.Physical, 60 );
			SetDamageType( ResistanceType.Fire, 20 );
			SetDamageType( ResistanceType.Energy, 20 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 55, 65 );
			SetResistance( ResistanceType.Cold, 55, 65 );
			SetResistance( ResistanceType.Poison, 80, 90 );
			SetResistance( ResistanceType.Energy, 60, 75 );

			SetSkill( SkillName.Anatomy, 110.6, 116.1 );
			SetSkill( SkillName.EvalInt, 100.0, 114.4 );
            SetSkill(SkillName.Healing, 100);
            SetSkill(SkillName.Hiding, 100);
			SetSkill( SkillName.Magery, 100.0 );
			SetSkill( SkillName.Meditation, 118.2, 127.8 );
			SetSkill( SkillName.MagicResist, 120.0 );
            SetSkill(SkillName.Poisoning, 100);
            SetSkill(SkillName.Stealth, 100);
			SetSkill( SkillName.Tactics, 111.9, 134.5 );
			SetSkill( SkillName.Wrestling, 119.7, 128.9 );
		}
        public override void OnDamage(int amount, Mobile from, bool willKill)
        {
            if (this.Hits <= 10000)
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
            else if (this.Hits >= 10001)
            {
                this.CantWalk = false;
                this.Hidden = false;
            }
        }

        public override int BreathColdDamage { get { return 100; } }
        public override int BreathFireDamage { get { return 0; } }
        public override double BreathDamageScalar { get { return (Core.AOS ? 0.10 : 0.05); } }

        public override bool HasAOEBreath { get { return true; } }
        public override string AOEBreathName { get { return "stone storm"; } }
        public override int AOEBreathRadius { get { return 4; } }
        public override int AOEBreathDuration { get { return 2; } }
        public override int AOEBreathEffectItemID { get { return 0x376A; } }
        public override int AOEBreathEffectHue { get { return 998; } }

        public override void AOESpecialEffect(Mobile m)
        {
            int duration = 5;
            if (m.ColdResistance > Utility.Random(50) + 40)
                duration = 4;

            m.Freeze(TimeSpan.FromSeconds(duration));
            m.SendMessage(37,"You are turned into stone.");
            m.HueMod = 998;
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

		public override int GetIdleSound() { return 1557; } 
		public override int GetAngerSound() { return 1554; } 
		public override int GetHurtSound() { return 1556; } 
		public override int GetDeathSound()	{ return 1555; }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 5 );
		}

		public Medusa( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
