using System; 
using Server; 

namespace Server.Items 
{ 
	public class CarrotPlant : Item
	{ 
		[Constructable]
		public CarrotPlant() : base( Utility.RandomList( 0x0C76 ) ) 
		{
			Weight = 1.0; 
			Name = "carrot plants"; 
			Movable = false;
		} 

		public CarrotPlant( Serial serial ) : base( serial ) 
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
			Carrot crop = new Carrot( pick ); 
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