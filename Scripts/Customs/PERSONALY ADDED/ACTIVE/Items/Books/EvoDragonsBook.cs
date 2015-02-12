using System;
using Server;

namespace Server.Items
{
	public class EvoDragonsBook : BlueBook
	{
		public static readonly BookContent Content = new BookContent// 8 lines per page
		(
			"EVO DRAGONS", "AlphaDragon",

			new BookPageInfo
			(
				"To control the Evo",
				"Dragons you need a Min",
				"Tame and Lore skill",
				"level for each stage.",
				"'EP' is the experiance",
				"points the pet needs in",
				"order to evolve to the",
				"next stage. There is a"
			),
			new BookPageInfo
			(
				"total of seven stages.",
				"",
				"Level 1 = 99.9",
				"EP = 25000",
				"",
				"Level 2 = 103.9",
				"EP = 75000",
				""
			),
			new BookPageInfo
			(
				"Level 3 = 106.9",
				"EP = 175000",
				"",
				"Level 4 = 109.9",
				"EP = 3750000",
				"",
				"Level 5 = 113.9",
				"EP = 7750000"
			),
			new BookPageInfo
			(
				"Level 6 = 116.9",
				"EP = 15000000",
				"",
				"Level 7 = 119.9",
				"EP = 30000000",
				"",
				"",
				""
			),
			new BookPageInfo
			(
				"Feeding the pet is a",
				"requirement also. If",
				"they are hungry they",
				"will stop responding",
				"and also can lose",
				"loyalty."
			)
		);
	

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public EvoDragonsBook() : base( false )
		{
			ItemID = 7185;
			Writable = false;
		}

		public EvoDragonsBook( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int)0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
}
