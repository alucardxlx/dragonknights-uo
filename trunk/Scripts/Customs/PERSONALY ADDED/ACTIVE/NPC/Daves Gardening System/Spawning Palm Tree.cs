

using System; 
using Server.Items;

namespace Server.Items
{ 
   public class SpawningPalmTree : Item 
   { 
      [Constructable] 
      public SpawningPalmTree() : base( 0xC96 ) 
      { 
         Movable = false; 
         Name = "Palm Tree"; 
      } 

      public override void OnDoubleClick( Mobile from ) 
      { 
         SplitCoconut SplitCoconut = new SplitCoconut(); 

         if ( !from.AddToBackpack( SplitCoconut ) ) 
            SplitCoconut.Delete(); 
      } 

      public SpawningPalmTree( Serial serial ) : base( serial ) 
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
