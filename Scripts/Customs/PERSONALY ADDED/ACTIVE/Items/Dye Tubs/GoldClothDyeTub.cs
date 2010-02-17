using System;
using Server;

namespace Server.Items
{
	public class GoldDyeTub : DyeTub
	{
		[Constructable]
		public GoldDyeTub()
		{
                        Name = "Gold Cloth Dye Tub";
			Hue = DyedHue = 0x499;
			Redyable = false;
		}

		public GoldDyeTub( Serial serial ) : base( serial )
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
		}
	}
}