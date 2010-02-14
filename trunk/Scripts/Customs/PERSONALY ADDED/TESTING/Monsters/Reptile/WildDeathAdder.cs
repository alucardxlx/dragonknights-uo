using System;
using Server.Mobiles;
using Server.Factions;

namespace Server.Mobiles
{
	[CorpseName("a death adder corpse")]
	public class WildDeathAdder : BaseCreature
	{

		[Constructable]
		public WildDeathAdder() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a death adder";
			Body = 0x15;
			Hue = 0x455;
			BaseSoundID = 219;

			SetStr( 70 );
			SetDex( 150 );
			SetInt( 100 );

			SetHits( 50 );
			SetStam( 150 );
			SetMana( 0 );

			SetDamage( 1, 4 );
			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 10 );
			SetResistance( ResistanceType.Poison, 100 );

			SetSkill( SkillName.Wrestling, 90.0 );
			SetSkill( SkillName.Tactics, 50.0 );
			SetSkill( SkillName.MagicResist, 100.0 );
			SetSkill( SkillName.Poisoning, 150.0 );

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 90.1;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Gems, 2 );
		}

		public override Poison HitPoison{ get{ return (0.8 >= Utility.RandomDouble() ? Poison.Greater : Poison.Deadly); } }

		public WildDeathAdder( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}
