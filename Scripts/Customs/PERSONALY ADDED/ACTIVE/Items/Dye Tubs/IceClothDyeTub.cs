using System;
using Server;

namespace Server.Items
{
	public class IceDyeTub : DyeTub
	{
		[Constructable]
		public IceDyeTub()
		{
                        Name = "Ice cloth Dye Tub";
			Hue = DyedHue = 0x480;
			Redyable = false;
		}

		public IceDyeTub( Serial serial ) : base( serial )
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