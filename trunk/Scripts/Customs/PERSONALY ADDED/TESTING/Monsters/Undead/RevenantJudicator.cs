using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a revenant judicator corpse" )]
	public class RevenantJudicator : BaseCreature
	{
		[Constructable]
		public RevenantJudicator() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a revenant judicator";
			Body = 155;
			BaseSoundID = 471;
			Hue = 950;

			SetStr( 301, 350 );
			SetDex( 75 );
			SetInt( 151, 200 );

			SetHits( 500 );
			SetStam( 150 );
			SetMana( 0 );

			SetDamage( 10, 15 );

			SetDamageType( ResistanceType.Physical, 0 );
			SetDamageType( ResistanceType.Cold, 50 );
			SetDamageType( ResistanceType.Poison, 50 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 20, 30 );
			SetResistance( ResistanceType.Cold, 50, 70 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 20, 30 );

			SetSkill( SkillName.MagicResist, 250.0 );
			SetSkill( SkillName.Tactics, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );

			Fame = 6000;
			Karma = -6000;

			PackItem( new GreaterExplosionPotion() );

			VirtualArmor = 40;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
		}

		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override int TreasureMapLevel{ get{ return 5; } }

		private DateTime m_NextBomb;
		private int m_Thrown;

		public override void OnActionCombat()
		{
			Mobile combatant = Combatant;

			if ( combatant == null || combatant.Deleted || combatant.Map != Map || !InRange( combatant, 12 ) || !CanBeHarmful( combatant ) || !InLOS( combatant ) )
				return;

			if ( DateTime.Now >= m_NextBomb )
			{
				ThrowBomb( combatant );

				m_Thrown++;

				if ( 0.75 >= Utility.RandomDouble() && (m_Thrown % 2) == 1 ) // 75% chance to quickly throw another bomb
					m_NextBomb = DateTime.Now + TimeSpan.FromSeconds( 3.0 );
				else
					m_NextBomb = DateTime.Now + TimeSpan.FromSeconds( 5.0 + (10.0 * Utility.RandomDouble()) ); // 5-15 seconds
			}
		}

		public void ThrowBomb( Mobile m )
		{
			DoHarmful( m );

			this.MovingParticles( m, 0x1C19, 1, 0, false, true, 0, 0, 9502, 6014, 0x11D, EffectLayer.Waist, 0 );

			new InternalTimer( m, this ).Start();
		}

		private class InternalTimer : Timer
		{
			private Mobile m_Mobile, m_From;

			public InternalTimer( Mobile m, Mobile from ) : base( TimeSpan.FromSeconds( 1.0 ) )
			{
				m_Mobile = m;
				m_From = from;
				Priority = TimerPriority.TwoFiftyMS;
			}

			protected override void OnTick()
			{
				m_Mobile.PlaySound( 0x11D );
				AOS.Damage( m_Mobile, m_From, Utility.RandomMinMax( 10, 20 ), 0, 100, 0, 0, 0 );
			}
		}

		public RevenantJudicator( Serial serial ) : base( serial )
		{
		}

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
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