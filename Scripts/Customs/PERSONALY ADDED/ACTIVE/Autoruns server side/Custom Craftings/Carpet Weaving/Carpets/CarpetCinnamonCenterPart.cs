using System;

namespace Server.Items
{
	
	public class CarpetCinnamonCenterPart : Item
	{
		[Constructable]
		public CarpetCinnamonCenterPart() : base( 2795 )
		{
			Weight = 1.0;
		}

		public CarpetCinnamonCenterPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
