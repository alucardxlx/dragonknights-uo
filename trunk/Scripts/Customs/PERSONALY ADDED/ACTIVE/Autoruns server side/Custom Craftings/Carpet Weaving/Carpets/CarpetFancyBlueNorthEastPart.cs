using System;

namespace Server.Items
{
	
	public class CarpetFancyBlueNorthEastPart : Item
	{
		[Constructable]
		public CarpetFancyBlueNorthEastPart() : base( 2773 )
		{
			Weight = 1.0;
		}

		public CarpetFancyBlueNorthEastPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
