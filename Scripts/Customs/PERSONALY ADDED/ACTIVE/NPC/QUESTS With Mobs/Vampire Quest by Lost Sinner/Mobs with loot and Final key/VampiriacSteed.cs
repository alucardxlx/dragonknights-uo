/////////////////
///LostSinner///
///////////////


using System;
using System.Collections;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a tainted steed corpse" )]
	public class VampiriacSteed : BaseWarHorse
	{

		[Constructable]
        public VampiriacSteed()
            : base(0x78, 0x3EAF, AIType.AI_Mage, FightMode.Aggressor, 10, 1, 0.2, 0.4)
		{
			
			BaseSoundID = 0xA8;

			SetStr( 450, 750 );
			SetDex( 200, 260 );
			SetInt( 360, 600 );

			SetHits( 500, 650 );
			SetMana( 120, 160 );
			SetStam( 140, 170 );

			SetDamage( 15, 20 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Fire, 50 );
			
			SetResistance( ResistanceType.Physical, 49, 59 );
			SetResistance( ResistanceType.Poison, 49, 59 );
			SetResistance( ResistanceType.Energy, 10, 20 );
			SetResistance( ResistanceType.Cold, 70, 70 );
			SetResistance( ResistanceType.Fire, 10, 20 );
			
			SetSkill( SkillName.MagicResist, 40.0, 50.0 );
			SetSkill( SkillName.Tactics, 40.0, 50.0 );
            SetSkill(SkillName.Anatomy, 40.0, 50.0);
            SetSkill(SkillName.Wrestling, 80.0, 95.0);
			SetSkill( SkillName.Magery, 90.0, 100.0 );
			SetSkill( SkillName.EvalInt, 85.0, 90.0 );

			Fame = 15000;
			Karma = -15000;

			Tamable = true;
			ControlSlots = 3;
			MinTameSkill = 100.0;
			
			Name = "a tainted steed";
			Hue = 2206;
			
			VirtualArmor = 50;
		}
        public override bool ReacquireOnMovement { get { return true; } }
        public override bool AutoDispel { get { return true; } }
		public override bool Unprovokable{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Greater; } }
        public override int TreasureMapLevel { get { return 4; } }
        public override int Meat { get { return 19; } }
        public override int Hides { get { return 20; } }
        public override HideType HideType { get { return HideType.Barbed; } }
        public override FoodType FavoriteFood { get { return FoodType.Meat; } }
        public override bool CanAngerOnTame { get { return true; } }

		public void DrainLife()
		{
			ArrayList list = new ArrayList();

			foreach ( Mobile m in this.GetMobilesInRange( 3 ) )
			{
				if ( m == this || !CanBeHarmful( m ) )
					continue;
                if (m.AccessLevel > AccessLevel.Player)
                    continue;
				if ( m is BaseCreature && (((BaseCreature)m).Controlled || ((BaseCreature)m).Summoned || ((BaseCreature)m).Team != this.Team) )
					list.Add( m );
				if ( m is BaseCreature )
					list.Add( m );
				else if ( m.Player )
					list.Add( m );
				
			}

			foreach ( Mobile m in list )
			{
				DoHarmful( m );

				m.FixedParticles( 0x374A, 10, 15, 5013, 0x496, 0, EffectLayer.Waist );
				m.PlaySound( 0x231 );

				m.SendMessage( "You feel suffocated from the taint!" );

				int toDrain = Utility.RandomMinMax( 4, 8 );

				m.Damage( toDrain, this );
			}
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			if ( 0.2 >= Utility.RandomDouble() )
				DrainLife();
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( 0.2 >= Utility.RandomDouble() )
				DrainLife();
		}

		public VampiriacSteed( Serial serial ) : base( serial )
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
