using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Commands;

using System.Collections;


namespace Arya.Auction
{
    public class AuctionWontAcceptNoticeGump : Gump
    {
        public AuctionWontAcceptNoticeGump(Mobile from) : base( 100, 100 )
        {
        	this.Closable=true;
        	this.Disposable=true;
        	this.Dragable=true;
        	this.Resizable=false;
        	
			AddPage(0);
			AddBackground(0, 0, 320, 164, 5054);
//			AddAlphaRegion(10, 10, 300, 20);
			AddImageTiled(10, 10, 300, 20, 2624);
			AddLabel(139, 10, 54, @"NOTICE");

//			AddAlphaRegion(10, 40, 300, 80);
//			AddImageTiled(10, 40, 300, 80, 2624);
			AddHtml( 10, 40, 300, 80, @"My manager changed his mind and will not add this item to the auctions. For some reason he doesnt like the item. You can always try to auction something else.", (bool)true, (bool)false);
			AddButton(134, 129, 2128, 2129, 0, GumpButtonType.Reply, 0);            
        }
        
        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

            if (info.ButtonID == 0)
            	from.SendGump( new AuctionGump( from ) );
            
        }
    }
}