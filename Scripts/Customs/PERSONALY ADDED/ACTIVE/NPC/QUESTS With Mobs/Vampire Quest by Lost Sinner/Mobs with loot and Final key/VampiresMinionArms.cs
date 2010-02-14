

//////////////////////////
//Created by LostSinner//
////////////////////////
using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x144e, 0x1453 )]
	public class VampiresMinionArms : BaseArmor
	{
		
		//public override int BasePhysicalResistance{ get{ return 3; } }
		public override int BaseFireResistance{ get{ return 2; } }
		public override int BaseColdResistance{ get{ return 4; } }
		public override int BasePoisonResistance{ get{ return 9; } }
		public override int BaseEnergyResistance{ get{ return 4; } }

        public override int InitMinHits { get { return 25; } }
        public override int InitMaxHits { get { return 30; } }

        public override int AosStrReq { get { return 55; } }
        public override int OldStrReq { get { return 40; } }

        public override int OldDexBonus { get { return -2; } }
        public override int AosDexBonus { get { return 0; } }

		public override int ArmorBase{ get{ return 35; } }
		public override int RevertArmorBase{ get{ return 4; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Bone; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }
        public override ArmorMeditationAllowance DefMedAllowance { get { return ArmorMeditationAllowance.Half; } }

		[Constructable]
		public VampiresMinionArms() : base( 0x144E )
		{
			Name = "Minion's Arms";
			Weight = 2.0;
			Hue = 1419;

            SkillBonuses.SetValues(0, SkillName.Necromancy, 1.0);
		}

		public VampiresMinionArms( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );

			if ( Weight == 1.0 )
				Weight = 2.0;
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
