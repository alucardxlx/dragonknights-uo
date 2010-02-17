//////////////////////////
//Created By FMKaraokeRadio//
////////////////////////
using System;
using Server;

namespace Server.Items
{
	public class PsychoGorget : LeatherGorget
	{
		public override int ArtifactRarity{ get{ return 100; } }

		public override int BasePhysicalResistance{ get{ return 15; } }
		public override int BaseFireResistance{ get{ return 15; } }
		public override int BaseColdResistance{ get{ return 15; } }
		public override int BasePoisonResistance{ get{ return 15; } }
		public override int BaseEnergyResistance{ get{ return 15; } }

		public override int InitMinHits{ get{ return 100; } }
		public override int InitMaxHits{ get{ return 300; } }

		[Constructable]
		public PsychoGorget()
		{
			Name = "Gorget Of The Psycho";
			Hue = 1172;
			ArmorAttributes.SelfRepair = 10;
                  Attributes.BonusStam = 10;
                  Attributes.BonusStr = 10;
                  Attributes.WeaponSpeed = 15;
                  Attributes.Luck = 95;
                  Attributes.ReflectPhysical = 10;
                  Attributes.WeaponDamage = 15;
		}

		public PsychoGorget( Serial serial ) : base( serial )
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