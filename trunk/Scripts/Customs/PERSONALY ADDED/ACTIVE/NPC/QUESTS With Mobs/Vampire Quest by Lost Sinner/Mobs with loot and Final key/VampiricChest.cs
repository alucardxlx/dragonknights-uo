
//////////////////////////
//Created by LostSinner//
////////////////////////
using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	
	public class VampChest : BaseArmor
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

		public override int OldDexBonus{ get{ return -8; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Bone; } }


		[Constructable]
        public VampChest()
            : base(0x144F)
		{
			Weight = 10.0;
			Name = "Lamia Arca";
			Hue = 601;			
			
			Attributes.RegenMana = 1;
			Attributes.LowerManaCost = 2;
			Attributes.BonusStr = 5;

            SkillBonuses.SetValues(0, SkillName.Necromancy, 1.0);			
		}

		public VampChest( Serial serial ) : base( serial )
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
		public override void OnDoubleClick( Mobile from )
		{
            if (!from.Alive) return;

            if (!IsChildOf(from.Backpack))
                from.SendMessage("You must have the item in your pack to morph it."); 
            else
			{
			
				if( this.ItemID == 5199 ) this.ItemID = 5100;
				else if( this.ItemID == 5100 ) this.ItemID = 5055;
				else if( this.ItemID == 5055 ) this.ItemID = 5141;
				else if( this.ItemID == 5141 ) this.ItemID = 7172;
				else if( this.ItemID == 7172 ) this.ItemID = 10109;
				else if( this.ItemID == 10109 ) this.ItemID = 10182;
				else if( this.ItemID == 10182 ) this.ItemID = 5068;
				else if( this.ItemID == 5068 ) this.ItemID = 10131;
				else if( this.ItemID == 10131 ) this.ItemID = 5199;
			}
		}
	}
}
