using System;

namespace Server.Items
{
		[FlipableAttribute( 7617, 7618, 7619, 7620, 7621, 7622 )]
	public class MarbleTableSectionable : Item
	{
		[Constructable]
		public MarbleTableSectionable() : base( 7617 )
		{
			Weight = 5.0;
		}

		public MarbleTableSectionable( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
