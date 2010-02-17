
//////////////////////////
//Created by LostSinner//
////////////////////////
using System;
using Server.Items;

namespace Server.Items
{
	public class VampGorget : BaseArmor
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
        public VampGorget()
            : base(0x1413)
		{
			Weight = 2.0;
			Name = "Lamia Iugulo";
			Hue = 601;
			
			Attributes.ReflectPhysical = 5;
						
			Attributes.RegenMana = 1;
			Attributes.LowerManaCost = 2;

            SkillBonuses.SetValues(0, SkillName.Necromancy, 1.0);
        }

		public VampGorget( Serial serial ) : base( serial )
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
			
				if( this.ItemID == 5063 ) this.ItemID = 5139;
				else if( this.ItemID == 5139 ) this.ItemID = 10105;
				else if( this.ItemID == 10105 ) this.ItemID = 10106;
				else if( this.ItemID == 10106 ) this.ItemID = 7944;
				else if( this.ItemID == 7944 ) this.ItemID = 4229;
				else if( this.ItemID == 4229 ) this.ItemID = 5063;
			}
		}	
	}
}