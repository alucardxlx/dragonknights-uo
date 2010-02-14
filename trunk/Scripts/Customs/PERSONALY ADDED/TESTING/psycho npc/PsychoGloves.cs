//////////////////////////
//Created By FMKaraokeRadio//
////////////////////////
using System;
using Server;

namespace Server.Items
{
	public class PsychoGloves : LeatherGloves
	{
		public override int ArtifactRarity{ get{ return 32; } }

		public override int BasePhysicalResistance{ get{ return 8; } }
		public override int BaseFireResistance{ get{ return 9; } }
		public override int BaseColdResistance{ get{ return 12; } }
		public override int BasePoisonResistance{ get{ return 15; } }
		public override int BaseEnergyResistance{ get{ return 15; } }

		public override int InitMinHits{ get{ return 100; } }
		public override int InitMaxHits{ get{ return 300; } }

		[Constructable]
		public PsychoGloves()
		{
			Name = "Gloves Of The Psycho";
			Hue = 1172;
			ArmorAttributes.SelfRepair = 10;
                  Attributes.BonusDex = 10;
                  Attributes.CastRecovery = 1;
                  Attributes.CastSpeed = 1;
                  Attributes.Luck = 100;
                  Attributes.WeaponSpeed = 20;
                  Attributes.ReflectPhysical = 25;
                  Attributes.AttackChance = 15;
		}

		public PsychoGloves( Serial serial ) : base( serial )
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
