using System; 
using Server; 

namespace Server.Items 
{ 
	public class CottonPlant : Item
	{ 
		[Constructable]
		public CottonPlant() : base( Utility.RandomList( 0x0C4F, 0x0C50 ) ) 
		{
			Weight = 1.0; 
			Name = "Cotton Plant";
			Movable = false;
		} 

		public CottonPlant( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 
			writer.Write( (int) 0 ); 
		} 
       
		public override void Deserialize(GenericReader reader) 
		{ 
			base.Deserialize( reader ); 
			int version = reader.ReadInt(); 
		}
		public override void OnDoubleClick( Mobile from ) 
		{ 
		    if ( from.InRange( this.GetWorldLocation(), 2 ) ) 
		    { 
		    int pick = Utility.Random( 1,3 );
			Cotton crop = new Cotton( pick ); 
			from.AddToBackpack( crop );
			this.Delete();
		    }
		    else
		    { 
			from.SendMessage( "You are too far away to harvest anything." ); 
		    } 
		}

	} 
}
