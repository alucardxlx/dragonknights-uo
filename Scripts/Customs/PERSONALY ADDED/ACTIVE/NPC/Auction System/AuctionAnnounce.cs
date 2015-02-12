
using System;
using Server.Network;
using System.Collections;
using System.Reflection;
using System.IO;

using Server;
using Server.Items;
using Server.Accounting;
using Server.Mobiles;

namespace Arya.Auction
{
    public class AuctionAnnouncTimer : Timer
    {
        public static void Initialize()
        {
            new AuctionAnnouncTimer().Start();
        }
        public AuctionAnnouncTimer()
            : base(TimeSpan.FromMinutes(60), TimeSpan.FromMinutes(60))
        {
            Priority = TimerPriority.OneMinute;
        }

        protected override void OnTick()
        {
            AuctionAnnounce();
        }
        public static void AuctionAnnounce()
        {
            AuctionSystem.ProfileAuctions();

        }
    }
}
