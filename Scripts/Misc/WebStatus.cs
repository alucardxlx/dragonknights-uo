using System;
using System.IO;
using System.Text;
using Server;
using Server.Network;
using Server.Guilds;

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



				op.WriteLine( "   <head>" );
				op.WriteLine( "      <title>DragonKnights UO Server Status</title>");
				op.WriteLine( "   </head>" );
				op.WriteLine( "   <body bgcolor=\"black\">" );
				op.WriteLine( " <img src= http://dragonknights.kicks-ass.net/themes/Black_Enuff_Yellow/images/logo.jpg <br> " );
				op.WriteLine( "      <h1><font color =\"gold\">UltimaOnline Server Status</font></h1>" );
				op.WriteLine( "<b><font color =\"gold\">Current Server Version # :</font></b><font color=\"green\"> 7.0.12.0</font><br>");
				op.WriteLine( "<b><font color =\"gold\">Status Page Last Updated:</font></b><font color=\"green\"> "+DateTime.Now.ToString() + "</font><br>");
				op.WriteLine( "<b><font color =\"gold\">Total World Items:</font></b><font color=\"green\"> "+World.Items.Count.ToString()+"</font><br>");
				op.WriteLine( "<b><font color =\"gold\">Total World Mobiles:</font></b><font color=\"green\"> " + World.Mobiles.Count.ToString() + "</font><br>");
				op.WriteLine( "<b><font color =\"gold\">Total Online:</font></b><font color=\"green\"> " + NetState.Instances.Count.ToString() + "</font><br>");
				op.WriteLine( "<b><font color =\"gold\">Online clients:</font></b><br>" );
				op.WriteLine( "<font color=\"green\">      <table width=\"100%\">" );
				op.WriteLine( "         <tr></font>" );
				op.WriteLine( "            <td bgcolor=\"white\"><font color=\"black\">Name</font></td><td bgcolor=\"white\"><font color=\"black\">Location</font></td><td bgcolor=\"white\"><font color=\"black\">Kills</font></td><td bgcolor=\"white\"><font color=\"black\">Karma / Fame</font></td>" );
				op.WriteLine( "         </tr>" );

				foreach ( NetState state in NetState.Instances )
				{
					Mobile m = state.Mobile;

					if ( m != null )
					{
						Guild g = m.Guild as Guild;

						op.Write( "         <tr><td>" );


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

							op.Write( ']' );
						}
						else
						{
							op.Write( Encode( m.Name ) );
						}

						op.Write( "</td><td><font color = 008000>" );
						op.Write( m.X );
						op.Write( ", " );
						op.Write( m.Y );
						op.Write( ", " );
						op.Write( m.Z );
						op.Write( " (" );
						op.Write( m.Map );
						op.Write( ")</td><td><font color = FF0000>" );
						op.Write( m.Kills );
						op.Write( "</td><td><font color = 800080>" );
						op.Write( m.Karma );
						op.Write( " / " );
						op.Write( m.Fame );
						op.WriteLine( "</td></tr>" );
					}
				}

				op.WriteLine( "         <tr>" );
				op.WriteLine( "      </table>" );
				op.WriteLine( "   </body>" );
				op.WriteLine( "</html>" );
			}
		}
	}
}
