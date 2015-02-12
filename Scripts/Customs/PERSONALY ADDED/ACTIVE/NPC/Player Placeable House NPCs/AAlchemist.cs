using System;
using System.Collections;
using Server;
using System.Collections.Generic;

using Server.ContextMenus;
using Server.Multis;
using Server.Items;

namespace Server.Mobiles
{
	public class AAlchemist : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		[Constructable]
		public AAlchemist() : base( "The Alchemist" )
		{
		    CantWalk = true;
			
			SetSkill( SkillName.Alchemy, 85.0, 100.0 );
			SetSkill( SkillName.TasteID, 65.0, 88.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBAlchemist() );
		}

		public override VendorShoeType ShoeType
		{
			get{ return Utility.RandomBool() ? VendorShoeType.Shoes : VendorShoeType.Sandals; }
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( new Server.Items.Robe( Utility.RandomPinkHue() ) );
		}

		public AAlchemist( Serial serial ) : base( serial )
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
					AAlchemist.GetContextMenuEntries( from, this, list );//HERE
				}

				base.GetContextMenuEntries( from, list );
			}
		}

        public static void GetContextMenuEntries(Mobile from, BaseVendor vendor, List<ContextMenuEntry> list)
		{
			list.Add( new AAlchemistCancelMenu( from, vendor ) );//HERE
		}

		private class AAlchemistCancelMenu : ContextMenuEntry//HERE
		{
			private BaseVendor m_Vendor;
			private Mobile m_Mobile;

			public AAlchemistCancelMenu(Mobile from, BaseVendor vendor) : base(6129, 3) //Dismissal //HERE
			{
				m_Vendor = vendor as AAlchemist;//HERE
				m_Mobile = from;
			}
			public override void OnClick()
			{
				m_Vendor.Delete();
				m_Mobile.AddToBackpack( new AAlchemistContract() );//HERE
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
