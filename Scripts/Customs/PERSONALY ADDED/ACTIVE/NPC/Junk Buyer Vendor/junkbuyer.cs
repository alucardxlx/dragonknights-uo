using System;
using System.Collections;
using Server;

namespace Server.Mobiles
{
	public class junkbuyer : BaseVendor
	{
		private ArrayList m_SBInfos = new ArrayList();
		protected override ArrayList SBInfos { get { return m_SBInfos; } }

		[Constructable]
		public junkbuyer() : base( "the junk buyer" )
		{
			SetSkill( SkillName.Begging, 64.0, 100.0 );
                        
			Hue = 0;
                        Name = "Leroy";
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBjunkbuyer() );

		}

		public override VendorShoeType ShoeType { get { return VendorShoeType.ThighBoots; } }

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( new Server.Items.HoodedShroudOfShadows( Utility.RandomRedHue() ) );

			
		}

		public junkbuyer( Serial serial ) : base( serial )
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
