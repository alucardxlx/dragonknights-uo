using System;
using System.Collections;
using Server;
using Server.Engines.BulkOrders;
using System.Collections.Generic;

using Server.ContextMenus;
using Server.Multis;
using Server.Items;


namespace Server.Mobiles
{
	public class ABlacksmith : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		//public override NpcGuild NpcGuild{ get{ return NpcGuild.BlacksmithsGuild; } }

		[Constructable]
		public ABlacksmith() : base( "The Blacksmith" )
		{
		    CantWalk = true;
		
			SetSkill( SkillName.ArmsLore, 36.0, 68.0 );
			SetSkill( SkillName.Blacksmith, 65.0, 88.0 );
			SetSkill( SkillName.Fencing, 60.0, 83.0 );
			SetSkill( SkillName.Macing, 61.0, 93.0 );
			SetSkill( SkillName.Swords, 60.0, 83.0 );
			SetSkill( SkillName.Tactics, 60.0, 83.0 );
			SetSkill( SkillName.Parry, 61.0, 93.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBAxeWeapon() );
			m_SBInfos.Add( new SBKnifeWeapon() );
			m_SBInfos.Add( new SBMaceWeapon() );
			m_SBInfos.Add( new SBSmithTools() );
			m_SBInfos.Add( new SBPoleArmWeapon() );
			m_SBInfos.Add( new SBSpearForkWeapon() );
			m_SBInfos.Add( new SBSwordWeapon() );

			m_SBInfos.Add( new SBMetalShields() );

			m_SBInfos.Add( new SBHelmetArmor() );
			m_SBInfos.Add( new SBPlateArmor() );
			m_SBInfos.Add( new SBChainmailArmor() );
			m_SBInfos.Add( new SBRingmailArmor() );
			m_SBInfos.Add( new SBStuddedArmor() );
			m_SBInfos.Add( new SBLeatherArmor() );
		}

		public override VendorShoeType ShoeType
		{
			get{ return VendorShoeType.None; }
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			Item item = ( Utility.RandomBool() ? null : new Server.Items.RingmailChest() );

			if ( item != null && !EquipItem( item ) )
			{
				item.Delete();
				item = null;
			}

			if ( item == null )
				AddItem( new Server.Items.FullApron() );

			AddItem( new Server.Items.Bascinet() );
			AddItem( new Server.Items.SmithHammer() );
		}

		#region Bulk Orders
		public override Item CreateBulkOrder( Mobile from, bool fromContextMenu )
		{
			PlayerMobile pm = from as PlayerMobile;

			if ( pm != null && pm.NextSmithBulkOrder == TimeSpan.Zero && (fromContextMenu || 0.2 > Utility.RandomDouble()) )
			{
				double theirSkill = pm.Skills[SkillName.Blacksmith].Base;

				if ( theirSkill >= 70.1 )
					pm.NextSmithBulkOrder = TimeSpan.FromHours( 4.0 );
				else if ( theirSkill >= 50.1 )
					pm.NextSmithBulkOrder = TimeSpan.FromHours( 2.0 );
				else
					pm.NextSmithBulkOrder = TimeSpan.FromHours( 1.0 );

				if ( theirSkill >= 70.1 && ((theirSkill - 40.0) / 300.0) > Utility.RandomDouble() )
					return new LargeSmithBOD();

				return SmallSmithBOD.CreateRandomFor( from );
			}

			return null;
		}

		public override bool IsValidBulkOrder( Item item )
		{
			return ( item is SmallSmithBOD || item is LargeSmithBOD );
		}

		public override bool SupportsBulkOrders( Mobile from )
		{
			return ( from is PlayerMobile && from.Skills[SkillName.Blacksmith].Base > 0 );
		}

		public override TimeSpan GetNextBulkOrder( Mobile from )
		{
			if ( from is PlayerMobile )
				return ((PlayerMobile)from).NextSmithBulkOrder;

			return TimeSpan.Zero;
		}
		#endregion

		public ABlacksmith( Serial serial ) : base( serial )
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
					ABlacksmith.GetContextMenuEntries( from, this, list );//HERE
				}

				base.GetContextMenuEntries( from, list );
			}
		}

        public static void GetContextMenuEntries(Mobile from, BaseVendor vendor, List<ContextMenuEntry> list)
		{
			list.Add( new ABlacksmithCancelMenu( from, vendor ) );//HERE
		}

		private class ABlacksmithCancelMenu : ContextMenuEntry//HERE
		{
			private BaseVendor m_Vendor;
			private Mobile m_Mobile;

			public ABlacksmithCancelMenu(Mobile from, BaseVendor vendor) : base(6129, 3) //Dismissal //HERE
			{
				m_Vendor = vendor as ABlacksmith;//HERE
				m_Mobile = from;
			}
			public override void OnClick()
			{
				m_Vendor.Delete();
				m_Mobile.AddToBackpack( new ABlacksmithContract() );//HERE
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
