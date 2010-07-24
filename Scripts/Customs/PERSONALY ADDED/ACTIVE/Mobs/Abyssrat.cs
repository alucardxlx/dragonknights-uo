using System;
using Server.Mobiles;
using Server;
using Server.Misc;
using Server.Items;
using System.Collections;


namespace Server.Mobiles
{
	[CorpseName( "an abyss vermin corpse" )]
	public class AbyssRat : BaseCreature
	{
		[Constructable]
		public AbyssRat() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a vermin of the abyss";
			Body = 0xD7;
			BaseSoundID = 0x188;

			     int randomHue = Utility.RandomMinMax( 1, 1020 );
			        
			        if(randomHue <= 950)
			                Hue = 1154;
			        else if(randomHue <= 962)
			                Hue = 1153;//white
			        else if(randomHue <= 967)
			                Hue = 1155;//forest green
			        else if(randomHue <= 972)
			                Hue = 1175;//darkness
			        else if(randomHue <= 987)
			                Hue = 2419;//dull copper
			        else if(randomHue <= 997)
			                Hue = 2413;//copper
			        else if(randomHue <= 1007)
			                Hue = 2418;//bronze
			        else if(randomHue <= 1008)
			                Hue = 1152;//glacial
				else if(randomHue <= 1010)
				Hue = 1;//black

			SetStr( 96, 120 );
			SetDex( 81, 105 );
			SetInt( 36, 60 );

			SetHits( 58, 72 );
			SetMana( 0 );

			SetDamage( 11, 17 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 20, 25 );
			SetResistance( ResistanceType.Fire, 10, 20 );
			SetResistance( ResistanceType.Cold, 5, 10 );
			SetResistance( ResistanceType.Poison, 5, 10 );
			SetResistance( ResistanceType.Energy, 10, 15 );

			SetSkill( SkillName.MagicResist, 57.6, 75.0 );
			SetSkill( SkillName.Tactics, 50.1, 70.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );

			Fame = 2500;
			Karma = -2500;

			VirtualArmor = 18;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 95.1;

			if ( 25.0 > Utility.RandomDouble() )
			{
				switch ( Utility.Random( 3 ) )
				{
					case 0:	PackItem( new LunarCheeseWheel() );	break;
					case 1:	PackItem( new LunarCheeseWedge() );	break;
					case 2:	PackItem( new LunarCheeseWedgeSmall() );	break;
				}
			}
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Poor );
		}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 7; } }
		public override HideType HideType{ get{ return HideType.Spined; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Fish | FoodType.Meat | FoodType.FruitsAndVegies | FoodType.Eggs; } }

		public AbyssRat(Serial serial) : base(serial)
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