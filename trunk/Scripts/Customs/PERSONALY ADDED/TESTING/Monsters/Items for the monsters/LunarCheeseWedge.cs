using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class LunarCheeseWedge : Food, ICarvable
	{
		public void Carve( Mobile from, Item item )
		{
			if ( !Movable )
				return;

			base.ScissorHelper( from, new LunarCheeseWedgeSmall(), 3 );
			from.SendMessage( "You cut the wheel into 3 wedges." );
		}

		[Constructable]
		public LunarCheeseWedge() : this( 1 )
		{
		}

		[Constructable]
		public LunarCheeseWedge( int amount ) : base( amount, 0x97D )
		{
			Name = "lunar cheese wedge";
			this.Weight = 0.3;
			this.FillFactor = 9;
			Hue = 1154;
		}

		public LunarCheeseWedge( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
