using System;
using Server.Items;

namespace Server.Items
{
	public class Necrostonevendor : Item
	{
		public override string DefaultName
		{
			get { return "Necro reagent vendor stone"; }
		}

		[Constructable]
		public Necrostonevendor() : base( 0xED4 )
		{
			Movable = false;
			Hue = 0x2D1;
		}

		public override void OnDoubleClick( Mobile from )
		{
			Container pack = from.Backpack;
			
			if ( pack != null && pack.ConsumeTotal( typeof( Gold ), 3200) )
			{
			BagOfNecroReagents necBag = new BagOfNecroReagents( 40 );

			if ( !from.AddToBackpack( necBag ) )
				necBag.Delete();
		}
			else
			{
				from.SendMessage( 0XAD, "You need at least 3200gp in your backpack to use this." );
			}
		}



		public Necrostonevendor( Serial serial ) : base( serial )
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