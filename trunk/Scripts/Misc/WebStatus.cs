using System;
using System.IO;
using System.Text;
using Server;
using Server.Network;
using Server.Guilds;
//Added to work with getting list of items and account info
using Server.Items;
using Server.Mobiles;
using Server.Accounting;
//end-Added to work with getting list of items and account info
 
namespace Server.Misc
{
    public class StatusPage : Timer
    {
        public static bool Enabled = true;
 
        public static void Initialize()
        {
            if ( Enabled )
                new StatusPage().Start();
        }
 
        public StatusPage() : base( TimeSpan.FromSeconds( 5.0 ), TimeSpan.FromSeconds( 60.0 ) )
        {
            Priority = TimerPriority.FiveSeconds;
        }
 
        private static string Encode( string input )
        {
            StringBuilder sb = new StringBuilder( input );
 
            sb.Replace( "&", "&amp;" );
            sb.Replace( "<", "&lt;" );
            sb.Replace( ">", "&gt;" );
            sb.Replace( "\"", "&quot;" );
            sb.Replace( "'", "&apos;" );
 
            return sb.ToString();
        }
 
        protected override void OnTick()
        {
            if ( !Directory.Exists( "web" ) )
                Directory.CreateDirectory( "web" );
 
            using ( StreamWriter op = new StreamWriter( "web/status.html" ) )
            {
                op.WriteLine( "<html>" );
                
 #region Header / Title
                op.WriteLine( "<head>" );
                op.WriteLine( "<title>DragonKnights UO Server Status</title>" );
                op.WriteLine( "</head>" );

                op.WriteLine( "<body bgcolor=\"black\">" );
                
                op.WriteLine( "<img src= http://aaaservices.homeip.net/UO/DragonKnights%20-%20logo.jpg <br>" );
                
                op.WriteLine( "<h3><font color =\"gold\">UltimaOnline Server Status</font></h3>" );
                op.WriteLine( "<b><font color =\"gold\">Server Core Version #: </font></b><font color=\"green\">" + Server.Core.Version.Major.ToString() + "." + Server.Core.Version.Minor.ToString() + "</font><br>" );
                op.WriteLine( "<b><font color =\"gold\">Current Server Client Support Version #: </font></b><font color=\"green\">" + ClientVerification.Required + "</font><br>" );
                op.WriteLine( "<b><font color =\"gold\">Status Page Last Updated: </font></b><font color=\"green\">"+DateTime.Now.ToString() + "</font><br>" );
                op.WriteLine( "<b><font color =\"gold\">Total World Items: </font></b><font color=\"green\">"+World.Items.Count.ToString()+"</font><br>" );
                op.WriteLine( "<b><font color =\"gold\">Total World Mobiles: </font></b><font color=\"green\">" + World.Mobiles.Count.ToString() + "</font><br>" );
                op.WriteLine( "<b><font color =\"gold\">Total Online: </font></b><font color=\"green\">" + NetState.Instances.Count.ToString() + "</font><br>" );
                op.WriteLine( "<br>" );
#endregion Header / Title
				
#region online client table
//NOTE: want to change for a world graphic
                op.WriteLine( "<table width=\"100%\">" );
                op.WriteLine( "<tr>" );
                op.WriteLine( "<td bgcolor=\"white\"><font color =\"black\"><b><img src= http://aaaservices.homeip.net/UO/uopics/uologoworld.bmp align=\"absmiddle\"> ONLINE PLAYERS:</b></font></td>" );
                op.WriteLine( "</tr>" );
                op.WriteLine( "</table>" );
                
                op.WriteLine( "<table width=\"100%\">" );
                op.WriteLine( "<tr>" );
                op.WriteLine( "<td bgcolor=\"white\"><font color=\"black\"><img src= http://aaaservices.homeip.net/UO/uopics/PlayerInsignia.bmp align=\"absmiddle\"> Name</font></td><td bgcolor=\"white\"><font color=\"black\"><img src= http://aaaservices.homeip.net/UO/uopics/MapLogoNorth.bmp align=\"absmiddle\">Location</font></td><td bgcolor=\"white\"><font color=\"black\"><img src= http://aaaservices.homeip.net/UO/uopics/ChaosLogo.bmp align=\"absmiddle\"> Kills</font></td><td bgcolor=\"white\"><font color=\"black\">Karma <img src= http://aaaservices.homeip.net/UO/uopics/VirtueLogo.bmp align=\"absmiddle\"> Fame</font></td>" );
                op.WriteLine( "</tr>" );
     
                if ( NetState.Instances.Count > 0 )
                {
                #region online client table\list online players     
                foreach ( NetState state in NetState.Instances )
                {
                    Mobile m = state.Mobile;
         
         
         
 
                    if ( m != null )
                    {
                        Guild g = m.Guild as Guild;
 
                        op.Write("<tr><td><img src= http://aaaservices.homeip.net/UO/uopics/SelectionCircle.bmp align=\"absmiddle\"> ");
 
 
                        switch (m.AccessLevel)
                        {
                        case AccessLevel.Player:
                        op.Write( "<font color = FFFFFF><b>Player</b> - " );
                        break;
                        case AccessLevel.Counselor:
                        op.Write( "<font color = 008000><b>Counselor</b> - " );
                        break;
                        case AccessLevel.GameMaster:
                        op.Write( "<font color = FF0000><b>GameMaster</b> - " );
                        break;
                        case AccessLevel.Seer:
                        op.Write( "<font color = FF0000><b>Seer</b> - " );
                        break;
                        case AccessLevel.Administrator:
                        op.Write( "<font color = FF0000><b>Administrator</b> - " );
                        break;
                        case AccessLevel.Developer:
                        op.Write( "<font color = FF0000><b>Developer</b> - " );
                        break;
                        case AccessLevel.Owner:
                        op.Write( "<font color = 0000FF><b>Owner</b> - " );
                        break;
                        }
 
 
 
 
                        if ( g != null )
                        {
                            op.Write( Encode( m.Name ) );
                            op.Write( " [ " );
 
                            string title = m.GuildTitle;
 
                            if ( title != null )
                                title = title.Trim();
                            else
                                title = "";
 
                            if ( title.Length > 0 )
                            {
                                op.Write( Encode( title ) );
                                op.Write( ", " );
                            }
 
                            op.Write( Encode( g.Abbreviation ) );
 
                            op.Write( " ] " );
                        }
                        else
                        {
                            op.Write( Encode( m.Name ) );
                        }
 
                        op.Write("</td><td><font color = 008000><img src= http://aaaservices.homeip.net/UO/uopics/Mapb.bmp align=\"absmiddle\"> ");
                        op.Write(m.X);
                        op.Write(", ");
                        op.Write(m.Y);
                        op.Write(", ");
                        op.Write(m.Z);
                        op.Write(" (");
                        op.Write(m.Map );
                        op.Write(")</td><td><font color = FF0000><img src= http://aaaservices.homeip.net/UO/uopics/ChaosLogob.bmp align=\"absmiddle\"> " );
                        op.Write(m.Kills );
                        op.Write("</td><td><font color = 800080>" );
                        op.Write(m.Karma );
                        op.Write(" <img src= http://aaaservices.homeip.net/UO/uopics/VirtueLogob.bmp align=\"absmiddle\"> " );
                        op.Write(m.Fame);
                        op.WriteLine("</font></td></tr>");
                    }
 
         
         
                }
                #endregion online client table\list online players
                }
     
                else
                {
                    op.WriteLine( "<td><b><tr><td><img src= http://aaaservices.homeip.net/UO/uopics/SelectionCircle.bmp align=\"absmiddle\"> <font color =\"red\">Nobody but us chickens! =P</font></b></td><td><img src= http://aaaservices.homeip.net/UO/uopics/Mapb.bmp align=\"absmiddle\"> <font color =\"green\">xxx, yyy, zzz (Trammel)</font></td><td><font color = FF0000><img src= http://aaaservices.homeip.net/UO/uopics/ChaosLogob.bmp align=\"absmiddle\"> 100000</font></td><td><font color = 800080>100000 <img src= http://aaaservices.homeip.net/UO/uopics/VirtueLogob.bmp align=\"absmiddle\"> 100000</font></td></tr>" );
                }
				                
                op.WriteLine( "</table>" );
                op.WriteLine( "<br>" );

#endregion online client table
             	
#region player vendors table
//Note want to add a shop sign here				
				op.WriteLine( "<table width=\"100%\">" );
				op.WriteLine( "<tr>" );
                op.WriteLine( "<td bgcolor=\"white\"><font color =\"black\"><b><img src= http://aaaservices.homeip.net/UO/uopics/signuo.bmp align=\"absmiddle\"> PLAYER VENDOR SHOPS:</td></b></font>" );
				op.WriteLine( "</tr>" );
				op.WriteLine( "</table>" );

				op.WriteLine( "<table width=\"100%\">" );
				
 
                foreach ( Mobile mob in World.Mobiles.Values )
                {
                    if ( mob is PlayerVendor )
                    {
                        PlayerVendor pv = mob as PlayerVendor;
                        
                        op.WriteLine( "<tr>" );
                        op.WriteLine( "<td bgcolor=\"white\"><font color=\"black\"><img src= http://aaaservices.homeip.net/UO/uopics/signswordandshield.bmp align=\"absmiddle\"> Owner's Name</font></td><td bgcolor=\"white\"><font color=\"black\"><img src= http://aaaservices.homeip.net/UO/uopics/signwritten.bmp align=\"absmiddle\"> Shop's Name</font></td><td bgcolor=\"white\"><font color=\"black\"><img src= http://aaaservices.homeip.net/UO/uopics/signwritten.bmp align=\"absmiddle\"> Vendor's Name</font></td><td bgcolor=\"white\"><font color=\"black\"><img src= http://aaaservices.homeip.net/UO/uopics/MapLogoNorth.bmp align=\"absmiddle\"> Location</font></td>" );
                        op.WriteLine( "</tr>" );                        
                        
                        
                        op.WriteLine( "<tr>" );                        
                        op.Write( "<td bgcolor=\"red\"><font color=\"black\"><img src= http://aaaservices.homeip.net/UO/uopics/SelectionCircleWithPoint.bmp align=\"absmiddle\"> ");
                        op.Write(pv.Owner.Name);
                        op.Write("</td><td bgcolor=\"red\"><font color = \"black\"><img src= http://aaaservices.homeip.net/UO/uopics/signwritten1.bmp align=\"absmiddle\"> ");
                        op.Write(pv.ShopName);
                        op.Write("</td><td bgcolor=\"red\"><font color = \"black\"><img src= http://aaaservices.homeip.net/UO/uopics/signwritten1.bmp align=\"absmiddle\"> ");
                        op.Write(pv.Name);
                        op.Write("</td><td bgcolor=\"red\"><font color = \"black\"><img src= http://aaaservices.homeip.net/UO/uopics/mapr.bmp align=\"absmiddle\"> ");
                        op.Write(pv.Map);
                        op.Write(" - ");
                        op.Write(pv.Location);
                        op.WriteLine("</td>" );
                        op.WriteLine( "</tr>" );                        
//                        op.WriteLine( pv.Owner.Name + "," + pv.ShopName + "," + pv.Name + "," + pv.Map + " - " + pv.Location + "<br>" );
						
						op.WriteLine( "<tr>" );
						op.WriteLine( "<td bgcolor=\"yellow\"><font color=\"black\"><img src= http://aaaservices.homeip.net/UO/uopics/pouch0.bmp align=\"absmiddle\"> Amount</td><td bgcolor=\"yellow\"><font color=\"black\"><img src= http://aaaservices.homeip.net/UO/uopics/bodbook.bmp align=\"absmiddle\"> Item</td><td bgcolor=\"yellow\"><font color=\"black\"><img src= http://aaaservices.homeip.net/UO/uopics/scales.bmp align=\"absmiddle\"> Price for the Amount<t/d><td bgcolor=\"yellow\"><font color=\"black\"><img src= http://aaaservices.homeip.net/UO/uopics/pen.bmp align=\"absmiddle\"> Description</td>" );
						op.WriteLine( "</tr>" );
                     
                        foreach ( Item item in pv.GetItems() )
                        {
                            GetVendorItemsDisplay( op, pv, item );
                        }
                    }
                }
                op.WriteLine( "</table>" );
                
#endregion player vendors table
         
         
         
         
            op.WriteLine( "</body>" );
            op.WriteLine( "</html>" );
            }
        }

#region GetVendorItemsDisplay Loop        
        private void GetVendorItemsDisplay(StreamWriter op, PlayerVendor pv, Item item)
        {
        	VendorItem vi = pv.GetVendorItem(item);
        	if ( vi == null )
        		return;
        	
        	if ( vi.IsForSale )
        	{
        		if (pv.Owner == null || pv.Owner.Name == "1k5g6se84f895s854f884s6a") //If name same as this it will not show items in list.
        			return;
        		string ownername = (pv.Name != pv.Owner.Name ? pv.Owner.Name : " ");
        		string name = item.Name;
        		if (string.IsNullOrEmpty(name))
        		{
        			name = item.GetType().ToString();
        			if (name.LastIndexOf('.') >= 0)
        				name = name.Substring(name.LastIndexOf('.') + 1);
        		}
        		if (name.Length > 25)
        			name = name.Substring(0, 25);
        		
        		string des = (string.IsNullOrEmpty(vi.Description) ? " " : vi.Description);
        		op.WriteLine( "<tr><td bgcolor=\"green\"><font color = \"black\"><img src= http://aaaservices.homeip.net/UO/uopics/pouch.bmp align=\"absmiddle\"> {0} </td> <td bgcolor=\"green\"><font color = \"black\"><img src= http://aaaservices.homeip.net/UO/uopics/bodscroll.bmp align=\"absmiddle\"> {1,-25} </td> <td bgcolor=\"green\"><font color = \"black\"><img src= http://aaaservices.homeip.net/UO/uopics/gold.bmp align=\"absmiddle\"> {2,7} </td> <td bgcolor=\"green\"><font color = \"black\"><img src= http://aaaservices.homeip.net/UO/uopics/scroll.bmp align=\"absmiddle\"> {3} </td></tr>", item.Amount, name, vi.Price.ToString(), des );//Note want to add bag icon here
        	}
        	else if ( item is Container)
        	{
        		foreach ( Item containerItem in item.Items)
        		{
        			GetVendorItemsDisplay( op, pv, containerItem);//
        		}
        	}
        }
#endregion GetVendorItemsDisplay Loop
    }
}