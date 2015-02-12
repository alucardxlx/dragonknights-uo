using System;
using Server;

namespace Server.Items
{
	public class HarborBuoyAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new HarborBuoyDeed(); } }


		[Constructable]
		public HarborBuoyAddon()
		{
			AddComponent( new AddonComponent( 0x46B8 ), 0,  0, 0 );
			AddComponent( new AddonComponent( 0x46B7 ), 1,  0, 0 );
			AddComponent( new AddonComponent( 0x46B6 ), 1,  1, 0 );
			AddComponent( new AddonComponent( 0x46B9 ), 0,  1, 0 );
		}

		public HarborBuoyAddon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class HarborBuoyDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new HarborBuoyAddon(); } }
		

		[Constructable]
		public HarborBuoyDeed()
		{
			Name = "Harbor Buoy Deed";
		}

		public HarborBuoyDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}