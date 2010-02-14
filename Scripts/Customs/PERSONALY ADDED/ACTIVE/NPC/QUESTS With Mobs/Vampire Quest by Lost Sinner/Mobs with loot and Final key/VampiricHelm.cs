
//////////////////////////
//Created by LostSinner//
////////////////////////
using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{

	public class VampHelm : BaseArmor
	{

		//public override int BasePhysicalResistance{ get{ return 8; } }
		public override int BaseFireResistance{ get{ return 8; } }
		public override int BaseColdResistance{ get{ return 8; } }
		public override int BasePoisonResistance{ get{ return 8; } }
		public override int BaseEnergyResistance{ get{ return 8; } }

		

		public override int InitMinHits{ get{ return 225; } }
		public override int InitMaxHits{ get{ return 225; } }

		public override int AosStrReq{ get{ return 90; } }
		public override int OldStrReq{ get{ return 90; } }

		public override int OldDexBonus{ get{ return -1; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Bone; } }

		[Constructable]
        public VampHelm()
            : base(0x1451)
		{
			Weight = 5.0;
			Name = "Lamia Cinxi";
			Hue = 601;
			
			Attributes.ReflectPhysical = 5;
			
			Attributes.RegenMana= 1;
			Attributes.LowerManaCost = 2;

            SkillBonuses.SetValues(0, SkillName.Necromancy, 1.0);
        }

		public VampHelm( Serial serial ) : base( serial )
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

			if ( Weight == 1.0 )
				Weight = 5.0;
		}
		public override void OnDoubleClick( Mobile from )
		{
			
            if (!from.Alive) return;

            if (!IsChildOf(from.Backpack))
                from.SendMessage("You must have the item in your pack to morph it."); 
            else
			{
			
				if( this.ItemID == 5201 ) this.ItemID = 5128;
				else if( this.ItemID == 5128 ) this.ItemID = 5130;
				else if( this.ItemID == 5130 ) this.ItemID = 5132;
				else if( this.ItemID == 5132 ) this.ItemID = 5134;
				else if( this.ItemID == 5134 ) this.ItemID = 5138;
				else if( this.ItemID == 5138 ) this.ItemID = 5051;
				else if( this.ItemID == 5051 ) this.ItemID = 10101;
				else if( this.ItemID == 10101 ) this.ItemID = 10100;
				else if( this.ItemID == 10100 ) this.ItemID = 10113;
				else if( this.ItemID == 10113 ) this.ItemID = 10116;
				else if( this.ItemID == 10116 ) this.ItemID = 10102;
				else if( this.ItemID == 10102 ) this.ItemID = 10117;
				else if( this.ItemID == 10117 ) this.ItemID = 10104;
				else if( this.ItemID == 10104 ) this.ItemID = 10126;
				else if( this.ItemID == 10126 ) this.ItemID = 5908;
				else if( this.ItemID == 5908 ) this.ItemID = 5910;
				else if( this.ItemID == 5910 ) this.ItemID = 5911;
				else if( this.ItemID == 5911 ) this.ItemID = 5912;
				else if( this.ItemID == 5912 ) this.ItemID = 5916;
				else if( this.ItemID == 5916 ) this.ItemID = 5907;
				else if( this.ItemID == 5907 ) this.ItemID = 5914;
				else if( this.ItemID == 5914 ) this.ItemID = 5915;
				else if( this.ItemID == 5915 ) this.ItemID = 5440;
				else if( this.ItemID == 5440 ) this.ItemID = 5201;
			}
		}
	}
}
