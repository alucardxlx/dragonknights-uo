using System;
using Server;

namespace Server.Items
{
	public class WhiteDyeTub : DyeTub
	{
		[Constructable]
		public WhiteDyeTub()
		{
                        Name = "White Cloth Dye Tub";
			Hue = DyedHue = 0x481;
			Redyable = false;
		}

		public WhiteDyeTub( Serial serial ) : base( serial )
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
