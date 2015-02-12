#region AuthorHeader
//
//	Auction version 2.1, by Xanthos and Arya
//
//  Based on original ideas and code by Arya
//
#endregion AuthorHeader
using System;
using System.Collections;

using Server;
using Server.Gumps;
using Xanthos.Utilities;

namespace Arya.Auction
{
	/// <summary>
	/// The main gump for the auction house
	/// </summary>
	public class MyAuctionGump : Gump
    {
        private ArrayList m_List;
        private AuctionGumpCallback m_Callback;
        public MyAuctionGump(Mobile m, AuctionGumpCallback callback) : base(50, 50)
        {
            m_Callback = callback;
            m.CloseGump(typeof(MyAuctionGump));
            MakeGump();
            m_List = new ArrayList(AuctionSystem.Auctions);
        }

		private void MakeGump()
		{
			Closable = true;
			Disposable = true;
			Dragable = true;
			Resizable = false;

			AddPage(0);
			AddImageTiled(49, 39, 402, 197, 3004);
			AddImageTiled(50, 40, 400, 195, 2624);
			AddAlphaRegion(50, 40, 400, 195);
			AddImage(165, 65, 10452);
			AddImage(-1, 20, 10400);
			AddImage(-1, 185, 10402);
			AddImage(35, 20, 10420);
			AddImage(421, 20, 10410);
			AddImage(410, 20, 10430);
			AddImageTiled(90, 32, 323, 16, 10254);
			AddImage(420, 185, 10412);

			AddLabel(160, 45, 151, AuctionSystem.ST[ 8 ] );

			// Create new auction: B1
			AddLabel(95, 120, 88, AuctionSystem.ST[ 9 ] );
			AddButton(60, 120, 4005, 4006, 1, GumpButtonType.Reply, 0);

			// View all auctions: B2 sort by newest first
			AddLabel(270, 120, 88, AuctionSystem.ST[ 10 ] );
            AddButton(235, 120, 4005, 4006, 2, GumpButtonType.Reply, 0);

            // View all auctions: B6 sort by closing first
            AddLabel(270, 145, 88, AuctionSystem.ST[236]);
            AddButton(235, 145, 4005, 4006, 6, GumpButtonType.Reply, 0);

			// View your auctions: B3
            AddLabel(270, 170, 88, AuctionSystem.ST[11]);
            AddButton(235, 170, 4005, 4006, 3, GumpButtonType.Reply, 0);

			// View your bids: B4
            AddLabel(95, 170, 88, AuctionSystem.ST[12]);
			AddButton(60, 170, 4005, 4006, 4, GumpButtonType.Reply, 0);

			// View pendencies: B5
			AddButton( 60, 195, 4005, 4006, 5, GumpButtonType.Reply, 0 );
            AddLabel(95, 195, 88, AuctionSystem.ST[13]);

			// Exit: B0
			AddLabel(270, 195, 88, AuctionSystem.ST[ 14 ] );
			AddButton(235, 195, 4017, 4018, 0, GumpButtonType.Reply, 0);
		}

		public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
		{
			if ( ! AuctionSystem.Running )
			{
				sender.Mobile.SendMessage( AuctionConfig.MessageHue, AuctionSystem.ST[ 15 ] );
				return;
			}

            int buttonid = info.ButtonID;
            AuctionComparer cmp = null;

            if (buttonid < 0 || buttonid > 6)
            {
                sender.Mobile.SendMessage("Invalid option.  Please try again.");
                return;
            }

			switch ( info.ButtonID )
			{
                case 0: // Exit
                    if (m_Callback != null)
                    {
                        try { m_Callback.DynamicInvoke(new object[] { sender.Mobile }); }
                        catch { }
                    }
                    break;

				case 1: // Create auction
					AuctionSystem.AuctionRequest( sender.Mobile );
					break;

                case 2: // View all auctions

                    cmp = new AuctionComparer(AuctionSorting.Date, false);//Sort by date newest first
                    if (cmp != null)
                    {
                        m_List.Sort(cmp);
                    }

                    sender.Mobile.SendGump(new AuctionListing(sender.Mobile, m_List, true, false));
                   // sender.Mobile.SendGump(new AuctionListing(sender.Mobile, AuctionSystem.Auctions, true, false));
					break;

				case 3: // View your auctions

                    sender.Mobile.SendGump(new AuctionListing(sender.Mobile, AuctionSystem.GetAuctions(sender.Mobile), true, false));
                    break;


				case 4: // View your bids

                    sender.Mobile.SendGump(new AuctionListing(sender.Mobile, AuctionSystem.GetBids(sender.Mobile), true, false));
                    break;

				case 5: // View pendencies

                    sender.Mobile.SendGump(new AuctionListing(sender.Mobile, AuctionSystem.GetPendencies(sender.Mobile), true, false));
                    break;
                case 6: // View all auctions sorted by ending first

                    cmp = new AuctionComparer(AuctionSorting.TimeLeft, true);//Sort by ending first
                    if (cmp != null)
                    {
                        m_List.Sort(cmp);
                    }

                    sender.Mobile.SendGump(new AuctionListing(sender.Mobile, m_List, true, false));
                    break;

			}
		}

	}
}