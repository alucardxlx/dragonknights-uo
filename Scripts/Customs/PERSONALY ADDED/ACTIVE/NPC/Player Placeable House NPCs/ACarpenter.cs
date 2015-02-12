using System;
using System.Collections;
using Server;
using System.Collections.Generic; 

using Server.ContextMenus;
using Server.Multis;
using Server.Items;


namespace Server.Mobiles
{
	public class ACarpenter : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		//public override NpcGuild NpcGuild{ get{ return NpcGuild.TinkersGuild; } }

		[Constructable]
		public ACarpenter() : base( "The Carpenter" )
		{
		    CantWalk = true;
		
			SetSkill( SkillName.Carpentry, 85.0, 100.0 );
			SetSkill( SkillName.Lumberjacking, 60.0, 83.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBStavesWeapon() );
			m_SBInfos.Add( new SBCarpenter() );
			m_SBInfos.Add( new SBWoodenShields() );
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( new Server.Items.HalfApron() );
		}

		public ACarpenter( Serial serial ) : base( serial )
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
					ACarpenter.GetContextMenuEntries( from, this, list );//HERE
				}

				base.GetContextMenuEntries( from, list );
			}
		}

        public static void GetContextMenuEntries(Mobile from, BaseVendor vendor, List<ContextMenuEntry> list)
		{
			list.Add( new ACarpenterCancelMenu( from, vendor ) );//HERE
		}

		private class ACarpenterCancelMenu : ContextMenuEntry//HERE
		{
			private BaseVendor m_Vendor;
			private Mobile m_Mobile;

			public ACarpenterCancelMenu(Mobile from, BaseVendor vendor) : base(6129, 3) //Dismissal //HERE
			{
				m_Vendor = vendor as ACarpenter;//HERE
				m_Mobile = from;
			}
			public override void OnClick()
			{
				m_Vendor.Delete();
				m_Mobile.AddToBackpack( new ACarpenterContract() );//HERE
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
