//////////////////////////////
//Created By FMKaraokeRadio//
////////////////////////////

using System;
using Server;

namespace Server.Items
{
	public class PsychoBrace : GoldBracelet
	{
		public override int ArtifactRarity{ get{ return 99; } }

		[Constructable]
		public PsychoBrace()
		{
			Name = "Braclet Of The Psycho";
			Hue = 1172;
			Attributes.CastRecovery = 1;
			Attributes.CastSpeed = 1;
			Attributes.BonusDex = 10;
                        Attributes.RegenHits = 10;
			Attributes.LowerManaCost = 20;
			Attributes.LowerRegCost = 20;
                        Attributes.WeaponSpeed = 15;
                        Attributes.WeaponDamage = 13;
		}

		public PsychoBrace( Serial serial ) : base( serial )
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
		}
	}
}