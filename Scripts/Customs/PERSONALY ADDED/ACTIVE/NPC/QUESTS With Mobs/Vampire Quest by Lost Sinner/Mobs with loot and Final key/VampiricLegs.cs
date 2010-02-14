
//////////////////////////
//Created by LostSinner//
////////////////////////using System;
using Server.Items;

namespace Server.Items
{


    public class VampLegs : BaseArmor
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

		public override int OldDexBonus{ get{ return -6; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Bone; } }

		[Constructable]
        public VampLegs()
            : base(0x1452)
		{
			Weight = 6.0;
			Name = "Lamia Articulus";
			Hue = 601;
			
			Attributes.ReflectPhysical = 5;
						
			Attributes.SpellDamage = 10;
			Attributes.RegenMana = 1;
			Attributes.LowerManaCost = 2;

            SkillBonuses.SetValues(0, SkillName.Necromancy, 1.0);
        }

		public VampLegs( Serial serial ) : base( serial )
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
			
				if( this.ItemID == 5202 ) this.ItemID = 5104;
				else if( this.ItemID == 5104 ) this.ItemID = 5054;
				else if( this.ItemID == 5054 ) this.ItemID = 5137;
				else if( this.ItemID == 5137 ) this.ItemID = 10125;
				else if( this.ItemID == 10125 ) this.ItemID = 10120;
				else if( this.ItemID == 10120 ) this.ItemID = 10118;
				else if( this.ItemID == 10118 ) this.ItemID = 5067;
				else if( this.ItemID == 5067 ) this.ItemID = 10129;
				else if( this.ItemID == 10129 ) this.ItemID = 5202;
			}
		}
	}
}
