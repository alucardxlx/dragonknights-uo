using System;

namespace Server.Items
{
		[FlipableAttribute( 7611, 7612, 7613, 7614, 7615, 7616 )]
	public class SandstoneTableSectionable : Item
	{
		[Constructable]
		public SandstoneTableSectionable() : base( 7611 )
		{
			Weight = 5.0;
		}

		public SandstoneTableSectionable( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
