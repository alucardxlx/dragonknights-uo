//////////////////////////
//Created by LostSinner//
////////////////////////
using System;
using Server;

namespace Server.Items
{
	public class VampiresMinionHelmet : BaseArmor
	{
		
		//public override int BasePhysicalResistance{ get{ return 3; } }
		public override int BaseFireResistance{ get{ return 2; } }
		public override int BaseColdResistance{ get{ return 4; } }
		public override int BasePoisonResistance{ get{ return 9; } }
		public override int BaseEnergyResistance{ get{ return 4; } }

        public override int InitMinHits { get { return 25; } }
        public override int InitMaxHits { get { return 30; } }

        public override int AosStrReq { get { return 20; } }
        public override int OldStrReq { get { return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Bone; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }
        public override ArmorMeditationAllowance DefMedAllowance { get { return ArmorMeditationAllowance.Half; } }

		[Constructable]
		public VampiresMinionHelmet() : base( 0x1451 )
		{
			Name = "Minion's Helmet";
			Weight = 1;
			Hue = 1175;
            SkillBonuses.SetValues(0, SkillName.Necromancy, 1.0);
		}

		public VampiresMinionHelmet( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
