using System;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "incapacitated tentacles" )]
	public class BermudaTitanTentacles : BaseCreature
	{
		[Constructable]
		public BermudaTitanTentacles() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "bermuda titan's tentacles";
			Body = 8;
			Hue = 141;
			BaseSoundID = 352;

			SetStr( 756, 780 );
			SetDex( 226, 245 );
			SetInt( 26, 40 );

			SetHits( 100, 112 );
			SetMana( 0 );

			SetDamage( 5-10 );

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

			VirtualArmor = 50;

			CanSwim = true;
			CantWalk = true;
		}

		public override void GenerateLoot()
		{
		}

		public BermudaTitanTentacles( Serial serial ) : base( serial )
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
