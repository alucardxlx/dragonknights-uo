using System;
using Server.Mobiles;
using Server;
using Server.Misc;
using Server.Items;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName( "an lunar rodent corpse" )]
	public class LunarRodent : BaseCreature
	{
		[Constructable]
		public LunarRodent() : base( AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a lunar rodent";
			Body = 238;
			BaseSoundID = 0xCC;

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

			SetStr( 76, 100 );
			SetDex( 6, 25 );
			SetInt( 11, 20 );

			SetHits( 46, 60 );
			SetMana( 0 );

			SetDamage( 5, 17 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 20, 25 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Energy, 5, 10 );

			SetSkill( SkillName.MagicResist, 25.1, 40.0 );
			SetSkill( SkillName.Tactics, 40.1, 60.0 );
			SetSkill( SkillName.Wrestling, 40.1, 60.0 );

			Fame = 750;
			Karma = -750;

			VirtualArmor = 18;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 70.1;

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

		public LunarRodent(Serial serial) : base(serial)
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