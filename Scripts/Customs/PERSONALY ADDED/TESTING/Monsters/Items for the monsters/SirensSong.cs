using System;

namespace Server.Items
{
	public class SirensSong : BaseInstrument
	{
		[Constructable]
		public SirensSong() : base( 0xEB2, 0x45, 0x46 )
		{
			Weight = 10.0;
			Name = "Siren's Song Harp";
			Hue = 1153;
		}

		public SirensSong( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( Weight == 3.0 )
				Weight = 10.0;
		}
	}
}