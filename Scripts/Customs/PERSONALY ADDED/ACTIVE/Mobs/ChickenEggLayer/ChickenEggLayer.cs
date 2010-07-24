// Originaly Created By Lucid Nagual - Admin of The Conjuring
// I'd like to thank all the wonderful people for sharing they're scripts & support.
// I hope by submitting this I can at least partially pay back the Runuo Community.
//Edited By: AlphaDragon to make a chicken that drops eggs. Thanks and with the help
//of Soteric, Lord_Greywolf, daat99, and KevinEvans.

using System;
using Server.Mobiles;
using Server.Items;
using Server.Misc;
using Server.Multis;
using Server.Network;
using Server.Targeting;
using System.Collections;


namespace Server.Mobiles
{
	[CorpseName( "a chicken corpse" )]
	public class ChickenEggLayer : BaseCreature
	{
		[Constructable]
		public ChickenEggLayer() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "egg laying chicken";
			Body = 0xD0;
			BaseSoundID = 0x6E;

			SetStr( 5 );
			SetDex( 15 );
			SetInt( 5 );

			SetHits( 3 );
			SetMana( 0 );

			SetDamage( 1 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 1, 5 );

			SetSkill( SkillName.MagicResist, 4.0 );
			SetSkill( SkillName.Tactics, 5.0 );
			SetSkill( SkillName.Wrestling, 5.0 );

			Fame = 150;
			Karma = 0;

			VirtualArmor = 2;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = -0.9;
	
		Timer PondTimer = new InternalTimer(this);
		PondTimer.Start();
		}
		
		public override int Meat{ get{ return 1; } }
		public override MeatType MeatType{ get{ return MeatType.Bird; } }
		public override FoodType FavoriteFood{ get{ return FoodType.GrainsAndHay; } }
		public override int Feathers{ get{ return 25; } }

		
		public ChickenEggLayer( Serial serial ) : base( serial )
		{
		}

		public class InternalTimer: Timer
		{
			private ChickenEggLayer Mare;
			//Line Below This: (TimeSpan.FromSeconds (this is where u adjust how long it takes till the egg drops)
			public InternalTimer(Mobile p): base( TimeSpan.FromSeconds( 1.0), TimeSpan.FromSeconds( 1800.0))//time 1.0 - 1800.0 meens will drop evey 1/2 hour
		{
			Priority = TimerPriority.FiftyMS;
			Mare = ((ChickenEggLayer) p);
		}

		protected override void OnTick()
		{
			if (Mare.Map != Map.Internal)
				{
				Eggs eggs = new Eggs();
				eggs.MoveToWorld( Mare.Location, Mare.Map );
				Effects.PlaySound( Mare.Location, Mare.Map, 110);//Can change number of sound played 110 = chicken cluck
				Mare.PublicOverheadMessage( MessageType.Regular, 62, true, "Cluck" );//FYI: Mare.PublicOverheadMessage( MessageType.Regular, 62 this is for the hue colot, true is for cursive, "Cluck" );
				
								
				}
			else
				{
				Effects.PlaySound( Mare.Location, Mare.Map, 130);//Can change number of sound played, 130 = Moose sound
				Mare.PublicOverheadMessage( MessageType.Regular, 38, true, "Eggs spawning on internal Map. Tell a GM." );
				}
			}
		}
			
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			Timer PondTimer = new InternalTimer(this);
			PondTimer.Start();
		}
	}
}
