using System;
using Server;
using Server.Misc;
using Server.Network;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "the incredible hulk's corpse" )]
	public class IncredibleHulk : BaseCreature
		{
		public override bool ShowFameTitle{ get{ return false; } }
		private static bool m_Talked; // flag to prevent spam
		string[] kfcsay = new string[] // things to say while greating
			{
			"HULK SMASH!",
			};
		[Constructable]
		public IncredibleHulk () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
			{
			Name = "the incredible hulk";
			Body = 83;
			BaseSoundID = 0x16a;//was 427
			Hue = 2212;
			SetStr( 666, 666 );
			SetDex( 545, 566 );
			SetInt( 46, 70 );
			SetHits( 1400, 4200 );
			SetDamage( 30, 35 );
			SetDamageType( ResistanceType.Physical, 100 );
			SetResistance( ResistanceType.Physical, 100 );
			SetResistance( ResistanceType.Fire, 80 );
			SetResistance( ResistanceType.Cold, 80 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 90 );
			SetSkill( SkillName.MagicResist, 125.1, 140.0 );
			SetSkill( SkillName.Tactics, 90.1, 100.0 );
			SetSkill( SkillName.Wrestling, 100.0, 120.0 );
			Fame = 20000;
			Karma = -20000;
			VirtualArmor = 80;
			PackGem(10);
			Timer SelfDeleteTimer = new InternalSelfDeleteTimer(this);
			SelfDeleteTimer.Start();
		}

		public IncredibleHulk( Serial serial ) : base( serial )
			{
			}
		public override void OnMovement( Mobile m, Point3D oldLocation )
			{
			if( m_Talked == false )
			{
				if ( m.InRange( this, 5 ) )
				{
					m_Talked = true;
					Effects.PlaySound( m.Location, m.Map, 0x16a);//Can change number of sound played 0x16a = DragonScream
					SayRandom( kfcsay, this );
					this.Move( GetDirectionTo( m.Location ) );
					// Start timer to prevent spam
					SpamTimer t = new SpamTimer();
					t.Start();
				}
			}
		}
		private class SpamTimer : Timer
		{
			public SpamTimer() : base( TimeSpan.FromSeconds( 4 ) )
			{
				Priority = TimerPriority.OneSecond;
			}
			protected override void OnTick()
			{
				m_Talked = false;
			}
      	}
		private static void SayRandom( string[] say, Mobile m )
		{
			m.Say( say[Utility.Random( say.Length )] );
		}
		public override bool OnBeforeDeath()
			{
			PackItem(new HulkPotion());
			PackItem(new HulkPotion());
			PackItem(new HulkPotion());
			PackItem(new HulkPotion());
			PackItem(new HulkPotion());
			PackItem(new IncredibleHulkStatuette());
			PackGold(1000, 1500);
			return base.OnBeforeDeath();//NOT SURE?????
		}
		
		
		public override void GenerateLoot()
			{
			AddLoot( LootPack.Rich, 2 );
			}
		public override bool CanRummageCorpses{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Regular; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override int Meat{ get{ return 2; } }
		
		
		
		
		
		
		
		public class InternalSelfDeleteTimer: Timer
		{
			private IncredibleHulk Mare;
			//Line Below This: (TimeSpan.FromSeconds (this is where u adjust how long it takes till the egg drops)
			public InternalSelfDeleteTimer(Mobile p): base( TimeSpan.FromSeconds( 3600.0), TimeSpan.FromSeconds( 3601.0))//time 1.0 - 1800.0 meens will drop evey 1/2 hour
      		{
				Priority = TimerPriority.FiftyMS;
				Mare = ((IncredibleHulk) p);
			}
			protected override void OnTick()
			{
				if (Mare.Map != Map.Internal)
				{
					Effects.PlaySound( Mare.Location, Mare.Map, 0x16a);//Can change number of sound played 0x16a = DragonScream
					Mare.PublicOverheadMessage( MessageType.Regular, 62, true, "YOU ARE WEAK!" );//FYI: Mare.PublicOverheadMessage( MessageType.Regular, 62 this is for the hue colot, true is for cursive, "Cluck" );
					Mare.Delete();
					this.Stop();
				}
				else
					{
					Effects.PlaySound( Mare.Location, Mare.Map, 130);//Can change number of sound played, 130 = Moose sound
					Mare.PublicOverheadMessage( MessageType.Regular, 38, true, "Spawning on internal Map. Tell a GM." );
				}
			}
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
			Timer SelfDeleteTimer = new InternalSelfDeleteTimer(this);
			SelfDeleteTimer.Start();
		}
	}
}


























		
