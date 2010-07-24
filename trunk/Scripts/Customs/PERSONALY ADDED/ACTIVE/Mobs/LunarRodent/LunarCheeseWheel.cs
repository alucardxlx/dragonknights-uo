using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class LunarCheeseWheel : Food, ICarvable
	{
		public void Carve( Mobile from, Item item )
		{
			if ( !Movable )
				return;

			if ( this.Amount > 1 )  // workaround because I can't call scissorhelper twice?
			{
				from.SendMessage( "You can only cut up one wheel at a time." );
				return;
			}

			base.ScissorHelper( from, new LunarCheeseWedge(), 3 );
			// base.ScissorHelper( from, new LunarCheeseWedgeSmall(), 1 );  // why doesn't this work?

			from.AddToBackpack( new LunarCheeseWedgeSmall() );

			from.SendMessage( "You cut a wedge out of the wheel." );
		}

		[Constructable]
		public LunarCheeseWheel() : this( 1 )
		{
		}

		[Constructable]
		public LunarCheeseWheel( int amount ) : base( amount, 0x97E )
		{
			Name = "lunar cheese wheel";
			this.Weight = 0.4;
			this.FillFactor = 12;
			Hue = 1154;
		}

		public LunarCheeseWheel( Serial serial ) : base( serial )
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