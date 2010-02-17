using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class LunarCheeseWedgeSmall : Food
	{
		[Constructable]
		public LunarCheeseWedgeSmall() : this( 1 )
		{
		}

		[Constructable]
		public LunarCheeseWedgeSmall( int amount ) : base( amount, 0x97C )
		{
			Name = "small lunar cheese wedge";
			this.Weight = 0.1;
			this.FillFactor = 3;
			Hue = 1154;
		}


		public LunarCheeseWedgeSmall( Serial serial ) : base( serial )
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