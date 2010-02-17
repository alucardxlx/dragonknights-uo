using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "an amazon corpse" )]
	public class AmazonRider : BaseCreature
	{
		[Constructable]
		public AmazonRider() : base( AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "savage" );
			Body = 184;
			Title = "the amazon rider";
			Hue = Utility.RandomSkinHue();

			SetStr( 151, 170 );
			SetDex( 92, 130 );
			SetInt( 51, 65 );

			SetDamage( 29, 34 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetSkill( SkillName.Fencing, 72.5, 95.0 );
			SetSkill( SkillName.Healing, 60.3, 90.0 );
			SetSkill( SkillName.Macing, 72.5, 95.0 );
			SetSkill( SkillName.Poisoning, 60.0, 82.5 );
			SetSkill( SkillName.MagicResist, 72.5, 95.0 );
			SetSkill( SkillName.Swords, 72.5, 95.0 );
			SetSkill( SkillName.Tactics, 72.5, 95.0 );

			Fame = 1000;
			Karma = -1000;

			PackItem( new Bandage( Utility.RandomMinMax( 1, 15 ) ) );

			AddItem( new Spear() );
			AddItem( new BoneArms() );
			AddItem( new FemaleLeatherChest() );
			AddItem( new BoneLegs() );

			Utility.AssignRandomHair( this );

			new Horse().Rider = this;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override int Meat{ get{ return 1; } }
		public override bool ShowFameTitle{ get{ return false; } }

		public override bool OnBeforeDeath()
		{
			IMount mount = this.Mount;

			if ( mount != null )
				mount.Rider = null;

			return base.OnBeforeDeath();
		}


		public override bool IsEnemy( Mobile m )
		{
			if ( m.BodyMod == 183 || m.BodyMod == 184 )
				return false;

			return base.IsEnemy( m );
		}

		public override void AlterMeleeDamageTo( Mobile to, ref int damage )
		{
		}

		public AmazonRider( Serial serial ) : base( serial )
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