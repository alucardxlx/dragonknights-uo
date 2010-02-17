using System; 
using Server; 
using Server.Gumps; 
using Server.Network; 
using Server.Misc; 
using Server.Mobiles; 
using Server.Targeting;
using Server.Engines.BulkOrders; 

namespace Server.Items 
{ 
   public class BulkOrderBookDyeTub : DyeTub 
   { 
     
	public override CustomHuePicker CustomHuePicker{ get{ return CustomHuePicker.LeatherDyeTub; } }

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

      [Constructable] 
      public BulkOrderBookDyeTub() 
      { 
         Weight = 10.0; 
         Redyable = true; 
         Name = "Bulk Order Book Dye Tub";
	 LootType = LootType.Blessed;

      } 

      public BulkOrderBookDyeTub( Serial serial ) : base( serial ) 
      { 
      } 

      public override void OnDoubleClick( Mobile from ) 
      { 
         if ( from.InRange( this.GetWorldLocation(), 1 ) ) 
         { 
            from.SendMessage( "Select the Bulk Order Book to dye." ); 
            from.Target = new InternalTarget( this ); 
         } 
         else 
         { 
            from.SendLocalizedMessage( 500446 ); // That is too far away. 
         } 
      } 

      private class InternalTarget : Target 
      { 
         private BulkOrderBookDyeTub m_LTub; 

         public InternalTarget( BulkOrderBookDyeTub tub ) : base( 1, false, TargetFlags.None ) 
         { 
            m_LTub = tub; 
         } 

         protected override void OnTarget( Mobile from, object targeted ) 
         { 
            if ( targeted is BulkOrderBook ) 
            { 
               BulkOrderBook BulkOrderBook = targeted as BulkOrderBook; 
               if ( !from.InRange( m_LTub.GetWorldLocation(), 1 ) || !from.InRange( ((Item)targeted).GetWorldLocation(), 1 ) ) 
               { 
                  from.SendLocalizedMessage( 500446 ); // That is too far away. 
               } 
               else if (( ((Item)targeted).Parent != null ) && ( ((Item)targeted).Parent is Mobile ) ) 
               { 
                  from.SendMessage( "You cannot dye that in it's current location." ); 
               } 
                  BulkOrderBook.Hue = m_LTub.Hue; 
                  from.PlaySound( 0x23E ); 
               } 
            } 
         } 
      } 
   } 