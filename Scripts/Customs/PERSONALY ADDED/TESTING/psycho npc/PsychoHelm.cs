//////////////////////////
//Created By FMKaraokeRadio//
////////////////////////
using System;
using Server;

namespace Server.Items
{
	public class PsychoHelm : PlateHelm
	{
		public override int ArtifactRarity{ get{ return 85; } }

		public override int BasePhysicalResistance{ get{ return 12; } }
		public override int BaseFireResistance{ get{ return 12; } }
		public override int BaseColdResistance{ get{ return 18; } }
		public override int BasePoisonResistance{ get{ return 12; } }
		public override int BaseEnergyResistance{ get{ return 16; } }

		public override int InitMinHits{ get{ return 100; } }
		public override int InitMaxHits{ get{ return 300; } }

		[Constructable]
		public PsychoHelm()
		{
			Name = "Helm Of The Psycho";
			Hue = 1172;
			ArmorAttributes.SelfRepair = 10;
                  Attributes.CastRecovery = 1;
                  Attributes.CastSpeed = 1;
                  Attributes.LowerManaCost = 15;
                  Attributes.LowerRegCost = 20;
                  Attributes.ReflectPhysical = 25;
                  Attributes.SpellDamage = 15;
		  Attributes.WeaponDamage = 10;
		  Attributes.WeaponSpeed = 5;
		}

		public PsychoHelm( Serial serial ) : base( serial )
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