using System;

namespace Server.Items
{
	
	public class CarpetRedCenterCPart : Item
	{
		[Constructable]
		public CarpetRedCenterCPart() : base( 2760 )
		{
			Weight = 1.0;
		}

		public CarpetRedCenterCPart( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
