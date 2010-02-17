using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a cock fighting chicken corpse" )]
	public class CockFightChicken : BaseCreature
	{
		[Constructable]
		public CockFightChicken() : base( AIType.AI_Animal, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a cock fighting chicken";
			Body = 0xD0;
			BaseSoundID = 357;

			SetStr( 278 );
			SetDex( 56 );
			SetInt( 100 );

			SetHits( 140, 160 );
			SetMana( 0 );

			SetDamage( 5 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 0, 1 );
                        SetResistance( ResistanceType.Fire, 100, 100 );
			SetResistance( ResistanceType.Cold, 100, 100 );
			SetResistance( ResistanceType.Poison, 100, 100 );
			SetResistance( ResistanceType.Energy, 100, 100 );

			SetSkill( SkillName.MagicResist, 100.0 );
			SetSkill( SkillName.Tactics, 30.0 );
			SetSkill( SkillName.Wrestling, 30.0 );

			Fame = 15000;
			Karma = -15000;

			VirtualArmor = 2;

			Tamable = true;
			ControlSlots = 5;
			MinTameSkill = -0.9;
		}

		public override int Meat{ get{ return 1; } }
		public override MeatType MeatType{ get{ return MeatType.Bird; } }
		public override FoodType FavoriteFood{ get{ return FoodType.GrainsAndHay; } }

		public override int Feathers{ get{ return 1; } }

		public CockFightChicken(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}