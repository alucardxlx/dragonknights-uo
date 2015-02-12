using System;
using System.Collections;
using Server;
using System.Collections.Generic; 

using Server.ContextMenus;
using Server.Multis;
using Server.Items;


namespace Server.Mobiles
{
	public class AProvisioner : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		[Constructable]
		public AProvisioner() : base( "The Provisioner" )
		{
		    CantWalk = true;
		
			SetSkill( SkillName.Camping, 45.0, 68.0 );
			SetSkill( SkillName.Tactics, 45.0, 68.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBProvisioner() );
		}

		public AProvisioner( Serial serial ) : base( serial )
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
					AProvisioner.GetContextMenuEntries( from, this, list );//HERE
				}

				base.GetContextMenuEntries( from, list );
			}
		}

        public static void GetContextMenuEntries(Mobile from, BaseVendor vendor, List<ContextMenuEntry> list)
		{
			list.Add( new AProvisionerCancelMenu( from, vendor ) );//HERE
		}

		private class AProvisionerCancelMenu : ContextMenuEntry//HERE
		{
			private BaseVendor m_Vendor;
			private Mobile m_Mobile;

			public AProvisionerCancelMenu(Mobile from, BaseVendor vendor) : base(6129, 3) //Dismissal //HERE
			{
				m_Vendor = vendor as AProvisioner;//HERE
				m_Mobile = from;
			}
			public override void OnClick()
			{
				m_Vendor.Delete();
				m_Mobile.AddToBackpack( new AProvisionerContract() );//HERE
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
