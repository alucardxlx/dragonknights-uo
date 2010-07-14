using System;

namespace Server.Items
{
	
	public class CarpetRedSouthPart : Item
	{
		[Constructable]
		public CarpetRedSouthPart() : base( 2768 )
		{
			Weight = 1.0;
		}

		public CarpetRedSouthPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
