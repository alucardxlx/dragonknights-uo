//////////////////////////
//Created By FMKaraokeRadio//
////////////////////////
using System;
using Server;

namespace Server.Items
{
	public class PsychoArms : PlateArms
	{
		public override int ArtifactRarity{ get{ return 25; } }

		public override int BasePhysicalResistance{ get{ return 10; } }
		public override int BaseFireResistance{ get{ return 10; } }
		public override int BaseColdResistance{ get{ return 7; } }
		public override int BasePoisonResistance{ get{ return 15; } }
		public override int BaseEnergyResistance{ get{ return 6; } }

		public override int InitMinHits{ get{ return 100; } }
		public override int InitMaxHits{ get{ return 300; } }

		[Constructable]
		public PsychoArms()
		{
			Name = "Arms Of The Psycho";
			Hue = 1172;
                  ArmorAttributes.SelfRepair = 10;
                  Attributes.BonusDex = 10;
                  Attributes.BonusHits = 7;
                  Attributes.BonusStr = 2;
                  Attributes.CastRecovery = 2;
                  Attributes.CastSpeed = 2;
                  Attributes.LowerManaCost = 10;
                  Attributes.LowerRegCost = 15;
                  Attributes.ReflectPhysical = 10;
		}

		public PsychoArms( Serial serial ) : base( serial )
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