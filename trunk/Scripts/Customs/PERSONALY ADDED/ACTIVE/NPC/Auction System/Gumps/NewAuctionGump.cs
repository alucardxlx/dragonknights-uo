#region AuthorHeader
//
//	Auction version 2.1, by Xanthos and Arya
//
//  Based on original ideas and code by Arya
//
#endregion AuthorHeader
using System;
using System.Collections;
using System.Globalization;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Misc;
using Xanthos.Utilities;

namespace Arya.Auction
{
	/// <summary>
	/// Configuration for a new auction
	/// </summary>
	public class NewAuctionGump : Gump
	{
		private Mobile m_User;
		private AuctionItem m_Auction;

		public NewAuctionGump( Mobile user, AuctionItem auction ) : base( 100, 100 )
		{
			user.CloseGump( typeof( NewAuctionGump ) );
			m_User = user;
			m_Auction = auction;
			MakeGump();
		}

        public static void AddItemCentered(int x, int y, int w, int h, int itemID, Gump gump)
        {
            Rectangle2D r = ItemBounds.Table[itemID];
            gump.AddItem(x + ((w - r.Width) / 2) - r.X, y + ((h - r.Height) / 2) - r.Y, itemID);
        }

		private void MakeGump()
		{			
			Closable = false;
			Disposable = true;
			Dragable = true;
			Resizable = false;

			AddPage(0);
			AddBackground( 0, 0, 502, 370, 9270 );
			AddImageTiled(4, 4, 492, 362, 2524);
			AddImageTiled(5, 5, 490, 360, 2624);
			AddAlphaRegion(5, 5, 490, 360);

			// Auction item goes here
			AddItemCentered( 5, 5, 155, 155, m_Auction.Item.ItemID, this );

			AddImageTiled(159, 5, 20, 184, 2524);
			AddImageTiled(5, 169, 255, 20, 2524);
			AddImageTiled(160, 5, 335, 355, 2624);
			AddImageTiled(5, 170, 155, 195, 2624);
			AddAlphaRegion(160, 5, 335, 360);
			AddAlphaRegion(5, 170, 155, 195);

			AddLabel(250, 10, 33, AuctionSystem.ST[ 100 ] );

			// Starting bid: text 0
			AddLabel(170, 35, 88, AuctionSystem.ST[ 68 ] );
			AddImageTiled(254, 34, 72, 22, 2524);
			AddImageTiled(255, 35, 70, 20, 2624);
			AddAlphaRegion(255, 35, 70, 20);
			AddTextEntry(255, 35, 70, 20, 273, 0, m_Auction.MinBid.ToString( "#,0" ) );

			// Reserve: text 1
			AddLabel(345, 35, 88, AuctionSystem.ST[ 69 ] );
			AddImageTiled(414, 34, 72, 22, 2524);
			AddImageTiled(415, 35, 70, 20, 2624);
            AddAlphaRegion(415, 35, 70, 20);
            AddTextEntry(415, 35, 70, 20, 273, 1, "");
            //AddTextEntry(415, 35, 70, 20, 273, 1, m_Auction.Reserve.ToString( "#,0" ) );

			// Days duration: text 2
			AddLabel(170, 60, 88, AuctionSystem.ST[ 101 ] );
			AddImageTiled(254, 59, 32, 22, 2524);
			AddImageTiled(255, 60, 30, 20, 2624);
			AddAlphaRegion(255, 60, 30, 20);
			AddTextEntry(255, 60, 30, 20, 273, 2, m_Auction.Duration.TotalDays.ToString() );
			AddLabel(290, 60, 88, AuctionSystem.ST[ 102 ] );

			// Item name: text 3
			AddLabel(170, 85, 88, AuctionSystem.ST[ 50 ] );
			AddImageTiled(254, 84, 232, 22, 2524);
			AddImageTiled(255, 85, 230, 20, 2624);
			AddAlphaRegion(255, 85, 230, 20);
			AddTextEntry(255, 85, 230, 20, 273, 3, m_Auction.ItemName );

			// Buy now: Check 0, Text 6
            //AddCheck(165, 110, 2152, 2153, true, 0);
            AddCheck(165, 110, 2474, 2153, true, 0);
			AddLabel( 200, 115, 88, AuctionSystem.ST[ 208 ] );
			AddImageTiled( 329, 114, 157, 22, 2524 );
			AddImageTiled( 330, 115, 155, 20, 2624 );
			AddAlphaRegion( 330, 115, 155, 20 );
			AddTextEntry( 330, 115, 155, 20, 273, 6, "" );

			// Description: text 4
			AddLabel(170, 140, 88, AuctionSystem.ST[ 103 ] );
			AddImageTiled(169, 159, 317, 92, 2524);
			AddImageTiled(170, 160, 315, 90, 2624);
			AddAlphaRegion(170, 160, 315, 90);
			AddTextEntry(170, 160, 315, 90, 273, 4, m_Auction.Description);

			// Web link: text 5
			AddLabel(170, 255, 88, AuctionSystem.ST[ 104 ] );
			AddImageTiled(224, 274, 262, 22, 2524);
			AddLabel(170, 275, 88, @"http://");
			AddImageTiled(225, 275, 260, 20, 2624);
			AddAlphaRegion(225, 275, 260, 20);
			AddTextEntry(225, 275, 260, 20, 273, 5, m_Auction.WebLink );
			
			// Help area
			AddImageTiled(9, 174, 152, 187, 2524);
			AddImageTiled(10, 175, 150, 185, 2624);
			AddAlphaRegion(10, 175, 150, 185);
			AddHtml( 10, 175, 150, 185, AuctionSystem.ST[ 105 ] , (bool)false, (bool)true);

			// OK Button: button 1
			AddButton(170, 305, 4023, 4024, 1, GumpButtonType.Reply, 0);
			AddLabel(210, 300, 33, AuctionSystem.ST[ 106 ] );
            AddLabel(210, 315, 33, AuctionSystem.ST[107]);

			// Cancel button: button 0
			AddButton(170, 335, 4020, 4020, 0, GumpButtonType.Reply, 0);
			AddLabel(210, 335, 88, AuctionSystem.ST[ 108 ] );
		}

		public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
		{
			if ( ! AuctionSystem.Running )
			{
				sender.Mobile.SendMessage( AuctionConfig.MessageHue, AuctionSystem.ST[ 15 ] );

				m_Auction.Cancel();

				return;
			}

            int buttonid = info.ButtonID;

            //right click
            if (buttonid == 0)
            {
                m_Auction.Cancel();
                m_User.SendGump(new AuctionGump(m_User));
                return;
            }

            if (buttonid > 1)
            {
                m_User.SendMessage("Invalid option.  Please try again.");
                return;
            }
			bool allowBuyNow = info.Switches.Length > 0; // Just one switch
            String errormessage = "";
			switch ( info.ButtonID )
			{
				case 0: // Cancel the auction

					m_Auction.Cancel();
                    m_User.SendGump(new AuctionGump(m_User));
					break;

				case 1: // Commit the auction

					// Collect information
					int minbid = 100; // text 0
                    int reserve = minbid; // text 1
					int days = 7; // text 2
                    string name = m_Auction.ItemName;// text 3
                    string description = " "; // text 4
                    string weblink = " "; // text 5
                    int buynow = 0; // text 6

					// The 3D client sucks

					string[] tr = new string[ 7 ];

					foreach( TextRelay t in info.TextEntries )
					{
                        try
                        {
                            tr[t.EntryID] = t.Text;
                        }
                        catch (Exception e)
                        {
                            errormessage = e.Message;
                        }
                    }
					

					try { minbid = (int) uint.Parse( tr[ 0 ], NumberStyles.AllowThousands ); } 
					catch {}

                    try { reserve = (int)uint.Parse(tr[1], NumberStyles.AllowThousands); }
                    catch { }

					try { days = (int) uint.Parse( tr[ 2 ] ); }
					catch {}

					try { buynow = (int) uint.Parse( tr[ 6 ], NumberStyles.AllowThousands ); }
					catch {}

                    if (buynow < 1)
                    {
                        allowBuyNow = false;
                    }

					if ( tr[ 3 ] != null )
					{
                        if (NameVerification.Validate(tr[3], 2, 40, true, true, true, 2, NameVerification.Empty))
                        {
                            string[] disallowed = ProfanityProtection.Disallowed;

                            for (int i = 0; i < disallowed.Length; i++)
                            {
                                if (tr[3].IndexOf(disallowed[i]) != -1)
                                {
                                    m_User.SendLocalizedMessage(1072622); // That name isn't very polite.
                                    goto case 0; // Pretty much cancel the auction
                                }
                            }
                        }
                        else
                        {
                            name = tr[3];
                        }
                        name = tr[3];
					}

					if ( tr[ 4 ] != null )
                    {
                        if (NameVerification.Validate(tr[4], 0, 100, true, true, true, 2, NameVerification.Empty))
                        {
                            string[] disallowed = ProfanityProtection.Disallowed;

                            for (int i = 0; i < disallowed.Length; i++)
                            {
                                if (tr[4].IndexOf(disallowed[i]) != -1)
                                {
                                    m_User.SendLocalizedMessage(1072622); // That name isn't very polite.
                                    goto case 0; // Pretty much cancel the auction
                                }
                            }
                        }
                        else
                        {
                            description = tr[4];
                            //name = tr[3];
                        }
						description = tr[ 4 ];
					}

					if ( tr[ 5 ] != null )
					{
						weblink = tr[ 5 ];
					}

					bool ok = true;

					if ( minbid < 1 )
					{
						m_User.SendMessage( AuctionConfig.MessageHue, AuctionSystem.ST[ 109 ] );
						ok = false;
                    }
                    if (reserve < 1)
                    {
                        reserve = minbid;
                    }

					if ( reserve < minbid )
					{
                        reserve = minbid;
						m_User.SendMessage( 214, AuctionSystem.ST[ 110 ] );
						ok = false;
                    }

					if ( days < AuctionSystem.MinAuctionDays && m_User.AccessLevel < AccessLevel.GameMaster || days < 1 )
					{
						m_User.SendMessage( AuctionConfig.MessageHue, AuctionSystem.ST[ 111 ] , AuctionSystem.MinAuctionDays );
						ok = false;
                    }

					if ( days > AuctionSystem.MaxAuctionDays && m_User.AccessLevel < AccessLevel.GameMaster )
					{
						m_User.SendMessage( AuctionConfig.MessageHue, AuctionSystem.ST[ 112 ] , AuctionSystem.MaxAuctionDays );
						ok = false;
					}

					if ( name.Length == 0 )
					{
						m_User.SendMessage( AuctionConfig.MessageHue, AuctionSystem.ST[ 113 ] );
						ok = false;
					}

					if ( minbid * AuctionConfig.MaxReserveMultiplier < reserve && m_User.AccessLevel < AccessLevel.GameMaster )
					{
						m_User.SendMessage( AuctionConfig.MessageHue, AuctionSystem.ST[ 114 ] );
						ok = false;
					}

					if ( allowBuyNow && buynow < reserve )
					{
						m_User.SendMessage( AuctionConfig.MessageHue, AuctionSystem.ST[ 209 ] );
						ok = false;
					}

					if ( ok && AuctionConfig.CostOfAuction > 0.0 )
					{
						int toPay = 0;

						if ( AuctionConfig.CostOfAuction <= 1.0 )
							toPay = (int) ( Math.Max( minbid, reserve ) * AuctionConfig.CostOfAuction );
						else
							toPay = (int) AuctionConfig.CostOfAuction;

						if ( toPay > 0 )
						{
                            if (Server.Mobiles.Banker.Withdraw(m_User, toPay))
                            {
                                m_User.SendMessage(AuctionConfig.MessageHue, AuctionSystem.ST[228], toPay);
                            }
                            else
                            {
                                m_User.SendMessage(AuctionConfig.MessageHue, AuctionSystem.ST[229], toPay);
                                goto case 0; // Pretty much cancel the auction
                            }
						}
					}

					m_Auction.MinBid = minbid;
					m_Auction.Reserve = reserve;
					m_Auction.ItemName = name;
					m_Auction.Duration = TimeSpan.FromDays( days );					
					m_Auction.Description = description;
					m_Auction.WebLink = weblink;
					m_Auction.BuyNow = allowBuyNow ? buynow : 0;

					if ( ok && AuctionSystem.Running )
                    {
                        World.Broadcast(0x35, true, "A {0} just became available on [Myauction for {1}", m_Auction.ItemName, minbid);
                        m_Auction.Confirm();
						m_User.SendGump( new AuctionViewGump( m_User, m_Auction, new AuctionGumpCallback( AuctionCallback ) ) );
					}
					else if ( AuctionSystem.Running )
					{
						m_User.SendGump( new NewAuctionGump( m_User, m_Auction ) );
					}
					else
					{
						m_User.SendMessage( AuctionConfig.MessageHue, AuctionSystem.ST[ 115 ] );
					}

					break;
			}
		}

		private static void AuctionCallback( Mobile user )
		{
			user.SendGump( new AuctionGump( user ) );
		}
	}
}
