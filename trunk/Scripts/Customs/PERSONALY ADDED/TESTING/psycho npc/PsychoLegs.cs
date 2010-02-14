//////////////////////////
//Created By FMKaraokeRadio//
////////////////////////
using System;
using Server;

namespace Server.Items
{
	public class PsychoLegs : PlateLegs
	{
		public override int ArtifactRarity{ get{ return 70; } }

		public override int BasePhysicalResistance{ get{ return 10; } }
		public override int BaseFireResistance{ get{ return 9; } }
		public override int BaseColdResistance{ get{ return 16; } }
		public override int BasePoisonResistance{ get{ return 11; } }
		public override int BaseEnergyResistance{ get{ return 8; } }

		public override int InitMinHits{ get{ return 100; } }
		public override int InitMaxHits{ get{ return 300; } }

		[Constructable]
		public PsychoLegs()
		{
			Name = "Legs Of The Psycho";
			Hue = 1172;
			ArmorAttributes.SelfRepair = 10;
                  Attributes.BonusDex = 5;
                  Attributes.BonusHits = 5;
                  Attributes.CastRecovery = 1;
                  Attributes.CastSpeed = 1;
                  Attributes.LowerRegCost = 20;
                  Attributes.ReflectPhysical = 25;
                  Attributes.WeaponSpeed = 15;
		}

		public PsychoLegs( Serial serial ) : base( serial )
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
