#region AuthorHeader
//
//	Auction version 2.1, by Xanthos and Arya
//
//  Based on original ideas and code by Arya
//
#endregion AuthorHeader
using System;
using Server;
using Server.Targeting;
using Server.Gumps;

namespace Arya.Auction
{
	/// <summary>
	/// General purpose target used by the auction system
	/// </summary>
	public class AuctionTarget : Target
	{
		private AuctionTargetCallback m_Callback;
		
		public AuctionTarget( AuctionTargetCallback callback, int range, bool allowground ) : base( range, allowground, TargetFlags.None )
		{
			m_Callback = callback;
		}
		
		protected override void OnTarget(Mobile from, object targeted)
		{
			try
			{
				m_Callback.DynamicInvoke( new object[] { from, targeted } );
			}
			catch
			{
//				Console.WriteLine( "The auction system cannot access the cliloc.enu file. Please review the system instructions for proper installation" );
				if (targeted != null)
				{
//					from.SendMessage ("target ! null");
					if (targeted is Item)
					{
						((Item)targeted).Visible = true;
						from.SendGump(new AuctionWontAcceptNoticeGump(from));
					}
					else
					{
						from.SendGump( new NoticeGump( 1060637, 30720, "Please let a GM know - Aution Target Else.", 0xFFC000, 320, 240, null, null ) );
					}
				}
				else
				{
					from.SendMessage ("targeted was null");
				}
			}
		}
		
		protected override void OnTargetCancel(Mobile from, TargetCancelType cancelType)
		{
			if ( AuctionSystem.Running )
			{
				from.SendGump( new AuctionGump( from ) );
			}
		}
	}
}