using System;

namespace Server.Items
{
	
	public class CarpetGoldenSouthPart : Item
	{
		[Constructable]
		public CarpetGoldenSouthPart() : base( 2786 )
		{
			Weight = 1.0;
		}

		public CarpetGoldenSouthPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
