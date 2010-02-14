//This script is the string version of publicmoongate.cs script found on the RunUO forums.  
//I simply, renamed it, changed it to moveable and changed the pic to that of a hued bodbook.
//Many of the locations were copied from Traveling Books script by Broze The Newb        
//Modified by Ashlar, beloved of Morrigan
// Moded by Icon



using System; 
using System.Collections; 
using Server; 
using Server.Gumps; 
using Server.Network; 
using Server.Mobiles;
using Server.Commands;











namespace Server.Items 
{ 
   public class BookOfTravel : Item 
   { 
      [Constructable] 
      public BookOfTravel() : base( 0x22C5 ) //old id is 0x2259
      { 
         Movable = true; 
         //Light = LightType.Circle300;
         Hue = 1153; 
         Weight = 3;  
         Name = "World Travel Atlas"; 
         LootType = LootType.Blessed;
 
      } 

      public BookOfTravel( Serial serial ) : base( serial ) 
      { 
      } 

      public override void OnDoubleClick( Mobile from ) 
      { 
         if ( !from.Player ) 
            return; 

         if ( from.InRange( GetWorldLocation(), 1 ) ) 
            UseGate( from ); 
         else 
            from.SendLocalizedMessage( 500446 ); // That is too far away. 
      } 

      public override bool OnMoveOver( Mobile m ) 
      { 
         return !m.Player || UseGate( m ); 
      } 

      public bool UseGate( Mobile m ) 
      { 
         if ( m.Criminal ) 
         { 
            m.SendLocalizedMessage( 1005561, "", 0x22 ); // Thou'rt a criminal and cannot escape so easily. 
            return false; 
         } 
         else if ( Server.Spells.SpellHelper.CheckCombat( m ) ) 
         { 
            m.SendLocalizedMessage( 1005564, "", 0x22 ); // Wouldst thou flee during the heat of battle?? 
            return false; 
         } 

                  else if ( Server.Misc.WeightOverloading.IsOverloaded(  m ) )
			{
				m.SendLocalizedMessage( 502359, "", 0x22 ); // Thou art too encumbered to move.
				return false;
			}



                  else if ( m.Region is Server.Regions.Jail )
         		{
                  	m.SendLocalizedMessage( 1041530, "", 0x35 ); // You'll need a better jailbreak plan then that!
                        return false;
         		}


                       else if ( Server.Factions.Sigil.ExistsOn( m ) )
				{
					m.SendLocalizedMessage( 1061632 ); // You can't do that while carrying the sigil.
                                        return false;
				}













         else if ( m.Spell != null ) 
         { 
            m.SendLocalizedMessage( 1049616 ); // You are too busy to do that at the moment. 
            return false; 
         } 
         else 
         { 
            m.CloseGump( typeof( BookOfTravelGump ) ); 
            m.SendGump( new BookOfTravelGump( m ) ); 

            //Effects.PlaySound( m.Location, m.Map, 0x20E ); 
            return true; 
         } 
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

   public class PMEntry 
   { 
      private Point3D m_Location; 
      private string m_Text; 

      public Point3D Location 
      { 
         get 
         { 
            return m_Location; 
         } 
      } 

      public string Text 
      { 
         get 
         { 
            return m_Text; 
         } 
      } 

      public PMEntry( Point3D loc, string text ) 
      { 
         m_Location = loc; 
         m_Text = text; 
      } 
   } 

   public class PMList 
   { 
      private string m_Text, m_SelText; 
      private Map m_Map; 
      private PMEntry[] m_Entries; 

      public string Text 
      { 
         get 
         { 
            return m_Text; 
         } 
      } 

      public string SelText 
      { 
         get 
         { 
            return m_SelText; 
         } 
      } 

      public Map Map 
      { 
         get 
         { 
            return m_Map; 
         } 
      } 

      public PMEntry[] Entries 
      { 
         get 
         { 
            return m_Entries; 
         } 
      } 

      public PMList( string text, string selText, Map map, PMEntry[] entries ) 
      { 
         m_Text = text; 
         m_SelText = selText; 
         m_Map = map; 
         m_Entries = entries; 
      } 

      public static readonly PMList Felucca = 
         new PMList( "Felucca", "Felucca", Map.Felucca, new PMEntry[] 
            { 
               new PMEntry( new Point3D( 4470,1146,0 ), "Fel Moonglow" ), // Moonglow 
               new PMEntry( new Point3D( 1430,1703,9 ), "Fel Britain" ), // Britain 
               new PMEntry( new Point3D( 1417,3821,0 ), "Fel Jhelom" ), // Jhelom 
               new PMEntry( new Point3D(  542,985,0 ), "Fel Yew" ), // Yew 
               new PMEntry( new Point3D( 2526,583,0 ), "Fel Minoc" ), // Minoc 
               new PMEntry( new Point3D( 1823,2821,0 ), "Fel Trinsic" ), // Trinsic 
               new PMEntry( new Point3D(  598,2134,0 ), "Fel Skara Brae" ), // Skara Brae 
               new PMEntry( new Point3D( 3728,2164,20 ), "Fel Magincia" ), // Magincia 
               new PMEntry( new Point3D( 3626,2612,0 ), "Fel Ocllo" ),  // Haven 
               new PMEntry( new Point3D( 2899,676,0 ), "Fel Vesper" ),  // Vesper 
               new PMEntry( new Point3D( 3770,1308,0 ), "Fel NuJhelom" ),  // NuJhelom 
               new PMEntry( new Point3D( 2237,1214,0 ), "Fel Cove" ),  // Cove 
               new PMEntry( new Point3D( 2895,3479,15 ), "Fel Serpents Hold" ),  // Serpents Hold 
               new PMEntry( new Point3D( 2705,2162,0 ), "Fel Bucs Den" ),  // Bucs Den 
               new PMEntry( new Point3D( 5272,3995,37 ), "Fel Delucia" ),  // Delucia
               new PMEntry( new Point3D( 5729,3209,-1 ), "Fel Papua" ),  // Papua
               new PMEntry( new Point3D( 1361,895,0 ), "Fel Wind" )  // Wind
            } ); 

      public static readonly PMList FeluccaDungeons = 
         new PMList( "Felucca Dungeons", "Felucca Dungeons", Map.Felucca, new PMEntry[] 
		{
               new PMEntry( new Point3D( 1301,1080,0 ), "Fel Despise" ),  // Despise
               new PMEntry( new Point3D( 511,1565,0 ), "Fel Shame" ),  // Shame
               new PMEntry( new Point3D( 4111,434,5 ), "Fel Deceit" ),  // Deceit
               new PMEntry( new Point3D( 2498,921,0 ), "Fel Covetous" ),  // Covetous
               new PMEntry( new Point3D( 2043,238,10 ), "Fel Wrong" ),  // Wrong
               new PMEntry( new Point3D( 1176,2637,0 ), "Fel Destard" ),  // Destard
               new PMEntry( new Point3D( 4721,3824,0 ), "Fel Hythloth" ),  // Hythloth
               new PMEntry( new Point3D( 4596,3630,30 ), "Fel Hythloth Fire Pit" ),  // Hythloth
               new PMEntry( new Point3D( 2923,3409,8 ), "Fel Fire" ),  // Fire
               new PMEntry( new Point3D( 1999,81,4 ), "Fel Ice" ),  // Ice
               new PMEntry( new Point3D( 5451,3143,-60 ), "Fel Terathan Keep" ),  // Terathan Keep
               new PMEntry( new Point3D( 4596,3630,30 ), "Fel Daemon Temple" ),  // Daemon Temple
               new PMEntry( new Point3D( 5766,2634,43 ), "Fel Ophidian Temple" ),  // Ophidian Temple
               new PMEntry( new Point3D( 2607,763,0 ), "Fel Solen Hives Area B" )  // Solen
		} );

      public static readonly PMList Trammel = 
         new PMList( "Trammel", "Trammel", Map.Trammel, new PMEntry[] 
            { 
               new PMEntry( new Point3D( 4470,1146,0 ), "Tram Moonglow" ), // Moonglow 
               new PMEntry( new Point3D( 1430,1703,9 ), "Tram Britain" ), // Britain 
               new PMEntry( new Point3D( 1417,3821,0 ), "Tram Jhelom" ), // Jhelom 
               new PMEntry( new Point3D(  542,985,0 ), "Tram Yew" ), // Yew 
               new PMEntry( new Point3D( 2526,583,0 ), "Tram Minoc" ), // Minoc 
               new PMEntry( new Point3D( 1823,2821,0 ), "Tram Trinsic" ), // Trinsic 
               new PMEntry( new Point3D(  598,2134,0 ), "Tram Skara Brae" ), // Skara Brae 
               new PMEntry( new Point3D( 3728,2164,20 ), "Tram Magincia" ), // Magincia 
               new PMEntry( new Point3D( 3497,2571,14 ), "Tram New Haven" ),  // Haven 
               new PMEntry( new Point3D( 2899,676,0 ), "Tram Vesper" ),  // Vesper 
               new PMEntry( new Point3D( 3770,1308,0 ), "Tram NuJhelom" ),  // NuJhelom 
               new PMEntry( new Point3D( 2237,1214,0 ), "Tram Cove" ),  // Cove 
               new PMEntry( new Point3D( 2895,3479,15 ), "Tram Serpents Hold" ),  // Serpents Hold 
               new PMEntry( new Point3D( 2705,2162,0 ), "Tram Bucs Den" ),  // Bucs Den 
               new PMEntry( new Point3D( 5272,3995,37 ), "Tram Delucia" ),  // Delucia
               new PMEntry( new Point3D( 5729,3209,-1 ), "Tram Papua" ),  // Papua
               new PMEntry( new Point3D( 1361,895,0 ), "Tram Wind" )  // Wind
            } ); 

      public static readonly PMList TrammelDungeons = 
         new PMList( "Trammel Dungeons", "Trammel Dungeons", Map.Trammel, new PMEntry[] 
		{
               new PMEntry( new Point3D( 1301,1080,0 ), "Tram Despise" ),  // Despise
               new PMEntry( new Point3D( 511,1565,0 ), "Tram Shame" ),  // Shame
               new PMEntry( new Point3D( 4111,434,5 ), "Tram Deceit" ),  // Deceit
               new PMEntry( new Point3D( 2498,921,0 ), "Tram Covetous" ),  // Covetous
               new PMEntry( new Point3D( 2043,238,10 ), "Tram Wrong" ),  // Wrong
               new PMEntry( new Point3D( 1176,2637,0 ), "Tram Destard" ),  // Destard
               new PMEntry( new Point3D( 4721,3824,0 ), "Tram Hythloth" ),  // Hythloth
               new PMEntry( new Point3D( 4596,3630,30 ), "Tram Hythloth Fire Pit" ),  // Hythloth
               new PMEntry( new Point3D( 2923,3409,8 ), "Tram Fire" ),  // Fire
               new PMEntry( new Point3D( 1999,81,4 ), "Tram Ice" ),  // Ice
               new PMEntry( new Point3D( 5451,3143,-60 ), "Tram Terathan Keep" ),  // Terathan Keep
               new PMEntry( new Point3D( 4596,3630,30 ), "Tram Daemon Temple" ),  // Daemon Temple
               new PMEntry( new Point3D( 5766,2634,43 ), "Tram Ophidian Temple" ),  // Ophidian Temple
               new PMEntry( new Point3D( 2607,763,0 ), "Fel Solen Hives Area B" )  // Solen
		} );

      public static readonly PMList Ilshenar = 
         new PMList( "Ilshenar", "Ilshenar", Map.Ilshenar, new PMEntry[] 
            { 
               new PMEntry( new Point3D( 852,602,-40 ), "Gargoyle City" ),  // Gargoyle 
               new PMEntry( new Point3D( 1151,659,-80 ), "Savage Camp" ),  // Savage 
               new PMEntry( new Point3D( 576,1150,-100 ), "Ankh Dungeon" ),  // Ankh 
               new PMEntry( new Point3D( 1747,1171,-2 ), "Blood Dungeon" ),  // Blood 
               new PMEntry( new Point3D( 548,462,-53 ), "Sorceror's Dungeon" ),  // Sorceror's 
               new PMEntry( new Point3D( 1362,1033,-8 ), "Spectre Dungeon" ),  // Spectre 
               new PMEntry( new Point3D( 651,1302,-58 ), "Wisp Dungeon" ),  // Wisp 
               new PMEntry( new Point3D( 1203,1124,-25 ), "Lakeshire" ),  // Lakeshire
               new PMEntry( new Point3D( 819,1130,-29 ), "Mistas" ),  // Mistas
               new PMEntry( new Point3D( 1706,205,104 ), "Montor" )  // Montor
            } ); 

      public static readonly PMList Malas = 
         new PMList( "Malas", "Malas", Map.Malas, new PMEntry[] 
            { 
               new PMEntry( new Point3D( 990,520,-50 ), "Luna" ), // Luna 
               new PMEntry( new Point3D( 2047,1353,-85 ), "Umbra" ),  // Umbra 
               new PMEntry( new Point3D( 2368,1267,-85 ), "Doom" )  // Doom 

            } ); 

      public static readonly PMList Tokuno = 
         new PMList( "Tokuno", "Tokuno", Map.Maps[4], new PMEntry[] 
            { 
               new PMEntry( new Point3D( 322,430,32 ), "Homare Bushido Dojo" ), // Homare Bushido Dojo
               new PMEntry( new Point3D( 255,789,63 ), "Homare Yomotsu Mine" ),  // Homare Yomotsu Mine
               new PMEntry( new Point3D( 203,985,18 ), "Homare Crane Marsh" ),  // Homare Crane Marsh
               new PMEntry( new Point3D( 741,1261,30 ), "Makoto Zento" ), // Makoto Zento
               new PMEntry( new Point3D( 727,1048,33 ), "Makoto Desert" ),  // Makoto Desert
               new PMEntry( new Point3D( 970,222,23 ), "Isamu Fan Dancer's Dojo" ), // Isamu Fan Dancer's Dojo 
               new PMEntry( new Point3D( 1234,772,3 ), "Isamu Mt. Sho Castle" ),  // Isamu Mt. Sho Castle 
               new PMEntry( new Point3D( 1044,523,15 ), "Isamu Valor Shrine" )  // Isamu Valor Shrine 

            } ); 
       
      	/*Here you can edit what facets to show on the TravelBooks...*/ 
		public static readonly PMList[] UORLists		= new PMList[] { Felucca, Trammel, TrammelDungeons, FeluccaDungeons };
		public static readonly PMList[] UORlistsYoung	= new PMList[] { Trammel, TrammelDungeons };
		public static readonly PMList[] LBRLists		= new PMList[] { Felucca, Trammel, Ilshenar, TrammelDungeons, FeluccaDungeons };
		public static readonly PMList[] LBRListsYoung	= new PMList[] { Trammel, TrammelDungeons, Ilshenar };
		public static readonly PMList[] AOSLists		= new PMList[] { Trammel, Felucca, Ilshenar, Malas, Tokuno, TrammelDungeons, FeluccaDungeons };
		public static readonly PMList[] AOSListsYoung	= new PMList[] { Trammel, TrammelDungeons, Ilshenar, Malas };
		public static readonly PMList[] SELists		= new PMList[] { Trammel, Felucca, Ilshenar, Malas, Tokuno, TrammelDungeons, FeluccaDungeons };
		public static readonly PMList[] SEListsYoung	= new PMList[] { Trammel, TrammelDungeons, Ilshenar, Malas, Tokuno };
		public static readonly PMList[] RedLists		= new PMList[] { Felucca, FeluccaDungeons  };
		public static readonly PMList[] SigilLists	= new PMList[] { Felucca, FeluccaDungeons  };
    } 

   public class BookOfTravelGump : Gump 
   { 
      public static void Initialize() 
      { 
         CommandSystem.Register( "Pod2", AccessLevel.GameMaster, new CommandEventHandler( BookOfTravelGump_OnCommand ) ); 
      } 

      private static void BookOfTravelGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new BookOfTravelGump( e.Mobile ) ); 
      }

      private Mobile m_Mobile; 
      private PMList[] m_Lists; 

      public BookOfTravelGump( Mobile mobile ) : base( 100, 100 ) 
      { 
         m_Mobile = mobile; 

         PMList[] checkLists; 

         if ( mobile.Player ) 
         { 
            if ( mobile.Kills >= 5 ) 
            { 
               checkLists = PMList.RedLists; 
            } 
            else 
            { 
               int flags = mobile.NetState == null ? 0 : mobile.NetState.Flags; 

               if ( Core.AOS && (flags & 0x8) != 0 ) 
                  checkLists = PMList.AOSLists; 
               else if ( (flags & 0x4) != 0 ) 
                  checkLists = PMList.LBRLists; 
               else 
                  checkLists = PMList.UORLists; 
            } 
         } 
         else 
         { 
            checkLists = PMList.AOSLists; 
         } 

         m_Lists = new PMList[checkLists.Length]; 

         for ( int i = 0; i < m_Lists.Length; ++i ) 
            m_Lists[i] = checkLists[i]; 

         for ( int i = 0; i < m_Lists.Length; ++i ) 
         { 
            if ( m_Lists[i].Map == mobile.Map ) 
            { 
               PMList temp = m_Lists[i]; 

               m_Lists[i] = m_Lists[0]; 
               m_Lists[0] = temp; 

               break; 
            } 
         } 

         AddPage( 0 ); 

         AddBackground( 0, 0, 380, 480, 9200 ); //5054 is invis. 2600 is not bad

         AddButton( 10, 270, 4005, 4007, 1, GumpButtonType.Reply, 0 ); 
         AddHtmlLocalized( 45, 270, 140, 25, 1011036, false, false ); // OKAY 

         AddButton( 10, 295, 4005, 4007, 0, GumpButtonType.Reply, 0 ); 
         AddHtmlLocalized( 45, 295, 140, 25, 1011012, false, false ); // CANCEL 

         AddHtmlLocalized( 5, 5, 200, 20, 1012011, false, false ); // Pick your destination: 

         for ( int i = 0; i < checkLists.Length; ++i ) 
         { 
            AddButton( 10, 35 + (i * 25), 2117, 2118, 0, GumpButtonType.Page, Array.IndexOf( m_Lists, checkLists[i] ) + 1 ); 
            AddHtml( 30, 35 + (i * 25), 150, 20, checkLists[i].Text, false, false ); 
         } 

         for ( int i = 0; i < m_Lists.Length; ++i ) 
            RenderPage( i, Array.IndexOf( checkLists, m_Lists[i] ) ); 
      } 

      private void RenderPage( int index, int offset ) 
      { 
         PMList list = m_Lists[index]; 

         AddPage( index + 1 ); 

         AddButton( 10, 35 + (offset * 25), 2117, 2118, 0, GumpButtonType.Page, index + 1 ); 
         AddHtml( 30, 35 + (offset * 25), 150, 20, list.SelText, false, false ); 

         PMEntry[] entries = list.Entries; 

         for ( int i = 0; i < entries.Length; ++i ) 
         { 
            AddRadio( 200, 35 + (i * 25), 210, 211, false, (index * 100) + i ); 
            AddHtml( 225, 35 + (i * 25), 150, 20, entries[i].Text, false, false ); 
         } 
      } 

      public override void OnResponse( NetState state, RelayInfo info ) 
      { 
         if ( info.ButtonID == 0 ) // Cancel 
            return; 
         else if ( m_Mobile.Deleted || m_Mobile.Map == null ) 
            return; 

         int[] switches = info.Switches; 

         if ( switches.Length == 0 ) 
            return; 

         int switchID = switches[0]; 
         int listIndex = switchID / 100; 
         int listEntry = switchID % 100; 

         if ( listIndex < 0 || listIndex >= m_Lists.Length ) 
            return; 

         PMList list = m_Lists[listIndex]; 

         if ( listEntry < 0 || listEntry >= list.Entries.Length ) 
            return; 

         PMEntry entry = list.Entries[listEntry]; 

         if ( m_Mobile.Player && m_Mobile.Kills >= 5 && list.Map != Map.Trammel ) 
         { 
            m_Mobile.SendLocalizedMessage( 1019004 ); // You are not allowed to travel there. 
         } 
         else if ( m_Mobile.Criminal ) 
         { 
            m_Mobile.SendLocalizedMessage( 1005561, "", 0x22 ); // Thou'rt a criminal and cannot escape so easily. 
         } 
         else if ( Server.Spells.SpellHelper.CheckCombat( m_Mobile ) ) 
         { 
            m_Mobile.SendLocalizedMessage( 1005564, "", 0x22 ); // Wouldst thou flee during the heat of battle?? 
         } 
         else if ( m_Mobile.Spell != null ) 
         { 
            m_Mobile.SendLocalizedMessage( 1049616 ); // You are too busy to do that at the moment. 
         } 
         else if ( m_Mobile.Map == list.Map && m_Mobile.InRange( entry.Location, 1 ) ) 
         { 
            m_Mobile.SendLocalizedMessage( 1019003 ); // You are already there. 
         } 
         else 
         { 
            BaseCreature.TeleportPets( m_Mobile, entry.Location, list.Map ); 

            m_Mobile.Combatant = null; 
            m_Mobile.Warmode = false; 
            m_Mobile.Map = list.Map; 
            m_Mobile.Location = entry.Location; 
         } 
      } 
   } 
} }
