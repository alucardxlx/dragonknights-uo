//////////////////////////
//CREATED BY FMKaraokeRadio//
////////////////////////
using System;
using Server;

namespace Server.Items
{
	public class PsychoChest : LeatherChest
	{
		public override int ArtifactRarity{ get{ return 69; } }

		public override int BasePhysicalResistance{ get{ return 12; } }
		public override int BaseFireResistance{ get{ return 8; } }
		public override int BaseColdResistance{ get{ return 13; } }
		public override int BasePoisonResistance{ get{ return 9; } }
		public override int BaseEnergyResistance{ get{ return 11; } }

		public override int InitMinHits{ get{ return 100; } }
		public override int InitMaxHits{ get{ return 300; } }

		[Constructable]
		public PsychoChest()
		{
			Name = "Chest Of The Psycho";
			Hue = 1172;
			ArmorAttributes.SelfRepair = 10;
                  Attributes.BonusDex = 5;
                  Attributes.BonusHits = 5;
                  Attributes.BonusStam = 10;
                  Attributes.LowerManaCost = 15;
                  Attributes.LowerRegCost = 20;
                  Attributes.ReflectPhysical = 25;
		  Attributes.AttackChance = 50;
		}

		public PsychoChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( Hue == 0x55A )
				Hue = 0x4F6;
		}
	}
}
