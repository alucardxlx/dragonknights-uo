using System;
using System.Collections;
using Server.Network;

namespace Server
{
       public class AnnounceDeath
	{
		public static void Initialize()
		{
	               EventSink.PlayerDeath += new PlayerDeathEventHandler( OnDeath);
		}
		public static void OnDeath( PlayerDeathEventArgs args )
		{
		       Mobile m = args.Mobile; 

		if (args.Mobile.AccessLevel < AccessLevel.GameMaster) 
		{ 
			           args.Mobile.PlaySound( 256 );
                       World.Broadcast( 0x35, true, "{0} Has been sent to the netherworld!!!", args.Mobile.Name );
		}
		}
	}
}
