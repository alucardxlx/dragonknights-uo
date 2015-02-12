using System;
using System.Collections;
using Server;
using System.Collections.Generic; 

using Server.ContextMenus;
using Server.Multis;
using Server.Items;


namespace Server.Mobiles
{
	public class ATinker : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		//public override NpcGuild NpcGuild{ get{ return NpcGuild.TinkersGuild; } }

		[Constructable]
		public ATinker() : base( "The Tinker" )
		{
		    CantWalk = true;
		
			SetSkill( SkillName.Lockpicking, 60.0, 83.0 );
			SetSkill( SkillName.RemoveTrap, 75.0, 98.0 );
			SetSkill( SkillName.Tinkering, 64.0, 100.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBTinker() );
		}

		public ATinker( Serial serial ) : base( serial )
		{
		}
#region
		public override void GetContextMenuEntries( Mobile from,  List<ContextMenuEntry> list )
		{
			BaseHouse house = BaseHouse.FindHouseAt( from );

			if ( house != null && from.Alive )
			{
				if ( house.IsOwner( from ) && house.IsInside( from )  )
				{
					ATinker.GetContextMenuEntries( from, this, list );//HERE
				}

				base.GetContextMenuEntries( from, list );
			}
		}

        public static void GetContextMenuEntries(Mobile from, BaseVendor vendor, List<ContextMenuEntry> list)
		{
			list.Add( new ATinkerCancelMenu( from, vendor ) );//HERE
		}

		private class ATinkerCancelMenu : ContextMenuEntry//HERE
		{
			private BaseVendor m_Vendor;
			private Mobile m_Mobile;

			public ATinkerCancelMenu(Mobile from, BaseVendor vendor) : base(6129, 3) //Dismissal //HERE
			{
				m_Vendor = vendor as ATinker;//HERE
				m_Mobile = from;
			}
			public override void OnClick()
			{
				m_Vendor.Delete();
				m_Mobile.AddToBackpack( new ATinkerContract() );//HERE
			}
		}
#endregion
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
