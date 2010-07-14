using System;

namespace Server.Items
{
	
	public class CarpetPinkCenterPart : Item
	{
		[Constructable]
		public CarpetPinkCenterPart() : base( 2796 )
		{
			Weight = 1.0;
		}

		public CarpetPinkCenterPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
