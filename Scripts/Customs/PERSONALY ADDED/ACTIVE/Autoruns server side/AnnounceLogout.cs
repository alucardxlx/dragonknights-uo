//////////////////////
//Created by KyleMan//
//////////////////////
using System;
using Server.Network;

namespace Server
{
	public class AnnounceLogout
	{
		public static void Initialize()
		{
			EventSink.Logout += new LogoutEventHandler( World_Logout );
		}
		private static void World_Logout( LogoutEventArgs args )
		{
			Mobile m = args.Mobile;

		if (args.Mobile.AccessLevel < AccessLevel.GameMaster)
		{
			World.Broadcast( 0x35, true, "{0} has logged out of the world.", args.Mobile.Name );
		}
		}
	}
}
