//================================================//
// Created by dracana				  //
// Desc: Vinter NPC to buy/sell crafted bottles   //
//       of wine.                                 //
//================================================//
using System;
using System.Collections.Generic;
using Server;

namespace Server.Mobiles
{
	public class WineVintner : BaseVendor
	{
        private List<SBInfo> m_SBInfos = new List<SBInfo>();
        protected override List<SBInfo> SBInfos { get { return m_SBInfos; } }

		[Constructable]
		public WineVintner() : base( "the wine Vintner" )
		{
			SetSkill( SkillName.TasteID, 80.0, 100.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBWineVintner() );
		}

		public override VendorShoeType ShoeType
		{
			get{ return VendorShoeType.Shoes; }
		}

		public override int GetShoeHue()
		{
			return 0;
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

		}

		public WineVintner( Serial serial ) : base( serial )
		{
		}

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
