//////////////////////
//Created by KyleMan//
//////////////////////
using System;
using Server.Network;

namespace Server
{
       public class AnnounceLogin
	{
		public static void Initialize()
		{
	               EventSink.Login += new LoginEventHandler( World_Login );
		}
		private static void World_Login( LoginEventArgs args )
		{
		       Mobile m = args.Mobile; 

		if (args.Mobile.AccessLevel < AccessLevel.GameMaster) 
		{ 
                       World.Broadcast( 0x35, true, "{0} has logged into the DragonKnight's world.", args.Mobile.Name ); // to edit the message just make sure you only change what is in the " " and not the {0} because that is the players name. 
		}
		}
	}
}