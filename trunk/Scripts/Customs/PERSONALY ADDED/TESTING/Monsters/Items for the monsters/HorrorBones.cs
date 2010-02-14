using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	[FlipableAttribute( 0x1B09, 0x1B10 )]
	public class HorrorBones : Item
	{
		[Constructable]
		public HorrorBones( ) : base( 0x1B09 + Utility.Random( 8 ) )
		{
			Name = "horror bones";
			Stackable = false;
			Weight = 10.0;
			Hue = 2406;
		}

		public HorrorBones( Serial serial ) : base( serial )
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
