using System; 
using Server; 

namespace Server.Items 
{ 
	public class WheatPlant : Item
	{ 
		[Constructable]
		public WheatPlant() : base( Utility.RandomList( 0x0C5A, 0x0C5B ) ) 
		{
			Weight = 1.0; 
			Name = "Wheat Plant";
			Movable = false;
		} 

		public WheatPlant( Serial serial ) : base( serial ) 
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
			Wheat crop = new Wheat( pick ); 
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