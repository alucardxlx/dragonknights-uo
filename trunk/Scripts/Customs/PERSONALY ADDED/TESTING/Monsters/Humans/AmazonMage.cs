using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "an amazon corpse" )]
	public class AmazonMage : BaseCreature
	{
		[Constructable]
		public AmazonMage() : base( AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "savage" );
			Body = 184;
			Title = "the amazon mage";
			Hue = Utility.RandomSkinHue();

			SetStr( 126, 145 );
			SetDex( 91, 110 );
			SetInt( 161, 185 );

			SetDamage( 4, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 40 );
			SetResistance( ResistanceType.Fire, 20, 30 );
			SetResistance( ResistanceType.Cold, 20, 30 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.EvalInt, 77.5, 100.0 );
			SetSkill( SkillName.Fencing, 62.5, 85.0 );
			SetSkill( SkillName.Macing, 62.5, 85.0 );
			SetSkill( SkillName.Magery, 72.5, 95.0 );
			SetSkill( SkillName.Meditation, 77.5, 100.0 );
			SetSkill( SkillName.MagicResist, 77.5, 100.0 );
			SetSkill( SkillName.Swords, 62.5, 85.0 );
			SetSkill( SkillName.Tactics, 62.5, 85.0 );
			SetSkill( SkillName.Wrestling, 62.5, 85.0 );

			Fame = 1000;
			Karma = -1000;

			PackReg( 10, 15 );
			PackItem( new Bandage( Utility.RandomMinMax( 1, 15 ) ) );

			AddItem( new GnarledStaff() );
			AddItem( new BoneArms() );
			AddItem( new LeatherBustierArms() );
			AddItem( new LeatherSkirt() );
			AddItem( new BoneLegs() );
			AddItem( new BearMask() );

			Utility.AssignRandomHair( this );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override int Meat{ get{ return 1; } }
		public override bool ShowFameTitle{ get{ return false; } }

		public override bool IsEnemy( Mobile m )
		{
			if ( m.BodyMod == 183 || m.BodyMod == 184 )
				return false;

			return base.IsEnemy( m );
		}

		public override void AlterMeleeDamageTo( Mobile to, ref int damage )
		{
		}

		public AmazonMage( Serial serial ) : base( serial )
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
