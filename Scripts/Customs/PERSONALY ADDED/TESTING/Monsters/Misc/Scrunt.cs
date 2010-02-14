using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;
using System.Collections;

namespace Server.Mobiles
{
[CorpseName( "a scrunt corpse" )]
	public class Scrunt : BaseMount
	{
		[Constructable]
		public Scrunt() : this( "a scrunt" )
		{
		}

		[Constructable]
		public Scrunt( string name ) : base( name, 277, 0x3e91, AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{			
			BaseSoundID = 0xE5;
			        
			Hue = 20000;//etheral

			SetStr( 756, 780 );
			SetDex( 226, 245 );
			SetInt( 26, 40 );

			SetHits( 454, 468 );
			SetMana( 0 );

			SetDamage( 19, 33 );

			SetDamageType( ResistanceType.Physical, 70 );
			SetDamageType( ResistanceType.Cold, 30 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.MagicResist, 15.1, 20.0 );
			SetSkill( SkillName.Tactics, 45.1, 60.0 );
			SetSkill( SkillName.Wrestling, 45.1, 60.0 );

			Fame = 11000;
			Karma = -11000;

			VirtualArmor = 75;
			
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );		
		}

		public override int GetAngerSound()
		{
			if ( !Controlled )
				return 0x16A;

			return base.GetAngerSound();
		}

		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }		
		
		public override WeaponAbility GetWeaponAbility()
		{
			switch ( Utility.Random( 2 ) )
			{
				default:
				case 0: return WeaponAbility.Dismount;
				case 1: return WeaponAbility.BleedAttack;
			}
		}

		public Scrunt( Serial serial ) : base( serial )
		{
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

			if ( BaseSoundID == 0x16A )
				BaseSoundID = 0xA8;
		}
	}
}
