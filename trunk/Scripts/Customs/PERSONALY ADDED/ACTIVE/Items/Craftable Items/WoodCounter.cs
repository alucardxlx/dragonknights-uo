using System;

namespace Server.Items
{
		[FlipableAttribute( 2879, 2880 )]
	public class WoodCounter : Item
	{
		[Constructable]
		public WoodCounter() : base( 2879 )
		{
			Name = "Counter";
			Weight = 1.0;
		}

		public WoodCounter( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}
