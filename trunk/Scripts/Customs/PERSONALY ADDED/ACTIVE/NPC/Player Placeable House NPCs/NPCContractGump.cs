using System; 
using Server;
using Server.Gumps;
using Server.Items; 
using Server.Commands;
using Server.Menus;
using Server.Menus.Questions; 
using Server.Network;
using Server.Targeting;
using Server.Mobiles;
using Server.Prompts;
using Server.Accounting;
using Server.Regions; 

namespace Server.Gumps 
{ 
   public class NPCContractGump : Gump 
   { 
      public static void Initialize() 
      { 
         CommandSystem.Register( "NPCContractGump", AccessLevel.GameMaster, new CommandEventHandler( NPCContractGump_OnCommand ) ); 
      } 

      private static void NPCContractGump_OnCommand( CommandEventArgs e ) 
      { 
        e.Mobile.SendGump( new NPCContractGump( e.Mobile ) ); 
        } 

        public NPCContractGump( Mobile owner ) : base( 50,50 ) 
        { 
        
         AddPage( 0 ); 
         AddBackground( 7, 12, 299, 451, 83 );
         AddBackground( 15, 21, 285, 432, 5054 );
//		 AddBackground( 16, 22, 283, 71, 3600 );
//		 AddBackground( 16, 94, 283, 71, 3600 );
//		 AddBackground( 16, 166, 283, 71, 3600 );
//		 AddBackground( 16, 238, 283, 71, 3600 );
//		 AddBackground( 16, 309, 283, 71, 3600 );
//		 AddBackground( 16, 381, 283, 71, 3600 );
		 AddLabel( 118, 21, 253, "NPC TOKEN" );
		 AddLabel( 26, 39, 0, "This will give you a contract wich will allow" );
		 AddLabel( 27, 60, 0, "you to place a npc in your house. When you" );
		 AddLabel( 33, 81, 0, "use the contract, the npc will be placed" );
		 AddLabel( 43, 101, 0, "in the spot where you are standing." );
		 AddLabel( 28, 123, 0, "You can dismiss the NPC and it will turn" );
		 AddLabel( 85, 142, 0, "back into the contract." );
		 AddLabel( 18, 164, 0, "Select the NPC contract you wish to receive." );
		 
//         AddButton( 34, 115, 4005, 4007, 1, GumpButtonType.Reply, 0 ); AddLabel( 66, 120, 37, "ALCHEMIST" ); 		 
//		 AddButton( 163, 115, 4005, 4007, 2, GumpButtonType.Reply, 0 ); AddLabel( 196, 114, 1257, "ANIMAL" ); AddLabel( 195, 130, 1257, "TRAINER" ); 		 
//		 AddButton( 34, 187, 4005, 4007, 3, GumpButtonType.Reply, 0 ); AddLabel( 66, 192, 1161, "BANKER" ); 		 
//		 AddButton( 163, 187, 4005, 4007, 4, GumpButtonType.Reply, 0 ); AddLabel( 196, 192, 1368, "BLACKSMITH" );  
//         AddButton( 34, 259, 4005, 4007, 5, GumpButtonType.Reply, 0 ); AddLabel( 66, 265, 1265, "CARPENTER" ); 
//         AddButton( 163, 259, 4005, 4007, 6, GumpButtonType.Reply, 0 ); AddLabel( 196, 265, 97, "JEWELER" );  
//         AddButton( 34, 330, 4005, 4007, 7, GumpButtonType.Reply, 0 ); AddLabel( 66, 335, 111, "MAGE" );  
//         AddButton( 163, 330, 4005, 4007, 8, GumpButtonType.Reply, 0 ); AddLabel( 196, 335, 171, "PROVISIONER" );  
//         AddButton( 34, 402, 4005, 4007, 9, GumpButtonType.Reply, 0 ); AddLabel( 66, 408, 95, "TAILOR" );  
//         AddButton( 163, 402, 4005, 4007, 10, GumpButtonType.Reply, 0 ); AddLabel( 196, 408, 416, "TINKER" );
         
         AddButton( 20, 195, 4005, 4007, 1, GumpButtonType.Reply, 0 ); AddLabel( 54, 196, 0, "Alchemist" );
		 AddButton( 168, 195, 4005, 4007, 2, GumpButtonType.Reply, 0 ); AddLabel( 202, 196, 0, "Animal Trainer" );
		 AddButton( 20, 235, 4005, 4007, 3, GumpButtonType.Reply, 0 ); AddLabel( 54, 236, 0, "Banker" );
		 AddButton( 168, 235, 4005, 4007, 4, GumpButtonType.Reply, 0 ); AddLabel( 202, 236, 0, "Blacksmith" );  
         AddButton( 20, 275, 4005, 4007, 5, GumpButtonType.Reply, 0 ); AddLabel( 54, 276, 0, "Carpenter" ); 
         AddButton( 168, 275, 4005, 4007, 6, GumpButtonType.Reply, 0 ); AddLabel( 202, 276, 0, "Jewler" );  
         AddButton( 20, 315, 4005, 4007, 7, GumpButtonType.Reply, 0 ); AddLabel( 54, 316, 0, "Mage" );  
         AddButton( 168, 315, 4005, 4007, 8, GumpButtonType.Reply, 0 ); AddLabel( 202, 316, 0, "Provisioner" );  
         AddButton( 20, 355, 4005, 4007, 9, GumpButtonType.Reply, 0 ); AddLabel( 54, 356, 0, "Tailor" );  
         AddButton( 168, 355, 4005, 4007, 10, GumpButtonType.Reply, 0 ); AddLabel( 202, 356, 0, "Tinker" );
         AddButton(120,412,12006,12008,0,GumpButtonType.Reply, 0 );//Close
         AddLabel( 32, 428, 0, "If you do not wish to decide at this time." );
         
        
        }

      public override void OnResponse( NetState state, RelayInfo info ) //Function for GumpButtonType.Reply Buttons 
      { 
         Mobile from = state.Mobile; 

         switch ( info.ButtonID ) 
         { 
          case 0: //Case uses the ActionIDs defenied above. Case 0 defenies the actions for the button with the action id 0
            { 
               //Cancel 
               from.SendMessage( 33, "You decide not to make your selection at this time." ); 
               break; 
            } 
            case 1: //Case uses the ActionIDs defenied above. Case 0 defenies the actions for the button with the action id 0 
            {
			    Item[] NPCToken = from.Backpack.FindItemsByType( typeof ( NPCToken ) );
				if ( from.Backpack.ConsumeTotal( typeof( NPCToken ), 1 ) )
				{  
                    AAlchemistContract AAlchemistContract = new AAlchemistContract(); 
                    from.AddToBackpack ( AAlchemistContract );
			        from.SendMessage( "An Alchemist Contract has been placed in your pack." );
			} 
            break;
			}
			case 2: 
            {
			    Item[] NPCToken = from.Backpack.FindItemsByType( typeof ( NPCToken ) );
				if ( from.Backpack.ConsumeTotal( typeof( NPCToken ), 1 ) )
				{  
                    AAnimalTrainerContract AAnimalTrainerContract = new AAnimalTrainerContract(); 
                    from.AddToBackpack ( AAnimalTrainerContract );
			        from.SendMessage( "An Animal Trainer Contract has been placed in your pack." );
			} 
            break;
			}
			case 3: 
            {
			    Item[] NPCToken = from.Backpack.FindItemsByType( typeof ( NPCToken ) );
				if ( from.Backpack.ConsumeTotal( typeof( NPCToken ), 1 ) )
				{  
                    ABankerContract ABankerContract = new ABankerContract(); 
                    from.AddToBackpack ( ABankerContract );
			        from.SendMessage( "A Banker Contract has been placed in your pack." );
			} 
            break;
			}
			case 4:
            {
			     Item[] NPCToken = from.Backpack.FindItemsByType( typeof ( NPCToken ) );
				if ( from.Backpack.ConsumeTotal( typeof( NPCToken ), 1 ) )
				{  
                    ABlacksmithContract ABlacksmithContract = new ABlacksmithContract(); 
                    from.AddToBackpack ( ABlacksmithContract );
			        from.SendMessage( "A Blacksmith Contract has been placed in your pack." );
			} 
            break; 
            } 
            case 5: //Same as above 
            {
			    Item[] NPCToken = from.Backpack.FindItemsByType( typeof ( NPCToken ) );
				if ( from.Backpack.ConsumeTotal( typeof( NPCToken ), 1 ) )
				{  
                    ACarpenterContract ACarpenterContract = new ACarpenterContract(); 
                    from.AddToBackpack ( ACarpenterContract );
			        from.SendMessage( "A Carpenter Contract has been placed in your pack." );
			}
			break;  
            } 
            case 6: 
            {
			     Item[] NPCToken = from.Backpack.FindItemsByType( typeof ( NPCToken ) );
				if ( from.Backpack.ConsumeTotal( typeof( NPCToken ), 1 ) )
				{  
                    AJewelerContract AJewelerContract = new AJewelerContract(); 
                    from.AddToBackpack ( AJewelerContract );
			        from.SendMessage( "A Jeweler Contract has been placed in your pack." );
			}
			break; 
            } 
            case 7: 
            {
			    Item[] NPCToken = from.Backpack.FindItemsByType( typeof ( NPCToken ) );
				if ( from.Backpack.ConsumeTotal( typeof( NPCToken ), 1 ) )
				{ 
                    AMageContract AMageContract = new AMageContract(); 
                    from.AddToBackpack ( AMageContract );
			        from.SendMessage( "A Mage Contract has been placed in your pack." );
			}
			break; 
            } 
            case 8: 
            {
			    Item[] NPCToken = from.Backpack.FindItemsByType( typeof ( NPCToken ) );
				if ( from.Backpack.ConsumeTotal( typeof( NPCToken ), 1 ) )
				{ 
                    AProvisionerContract AProvisionerContract = new AProvisionerContract(); 
                    from.AddToBackpack ( AProvisionerContract );
			        from.SendMessage( "A Provisioner Contract has been placed in your pack." );
			}
			break; 
            } 
            case 9: 
            {
			    Item[] NPCToken = from.Backpack.FindItemsByType( typeof ( NPCToken ) );
				if ( from.Backpack.ConsumeTotal( typeof( NPCToken ), 1 ) )
				{  
                    ATailorContract ATailorContract = new ATailorContract(); 
                    from.AddToBackpack ( ATailorContract );
			        from.SendMessage( "A Tailor Contract has been placed in your pack." );
			}
			break; 
            } 
            case 10: 
            {
			    Item[] NPCToken = from.Backpack.FindItemsByType( typeof ( NPCToken ) );
				if ( from.Backpack.ConsumeTotal( typeof( NPCToken ), 1 ) )
				{  
                    ATinkerContract ATinkerContract = new ATinkerContract(); 
                    from.AddToBackpack ( ATinkerContract );
			        from.SendMessage( "A Tinker Contract has been placed in your pack." );
			}
			break;     
            } 
         } 
      } 
   } 
}
