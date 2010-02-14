using System;
using Server.Items;


//////////////////////////
//Created by LostSinner//
////////////////////////
namespace Server.Items
{

	public class VampGloves : BaseArmor
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

		public override int OldDexBonus{ get{ return -2; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Bone; } }

		[Constructable]
        public VampGloves()
            : base(0x1450)
		{
			Weight = 2.0;
			Name = "Lamia Attrecto";
			Hue = 601;
			
			Attributes.ReflectPhysical = 5;
			
			Attributes.RegenMana= 1;
			Attributes.SpellDamage = 1;
			
			Attributes.LowerManaCost = 3;

            SkillBonuses.SetValues(0, SkillName.Necromancy, 1.0);
        }

		public VampGloves( Serial serial ) : base( serial )
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
				Weight = 2.0;
		}
		public override void OnDoubleClick( Mobile from )
		{
            if (!from.Alive) return;

            if (!IsChildOf(from.Backpack))
                from.SendMessage("You must have the item in your pack to morph it."); 
            else
			{
			
				if( this.ItemID == 5200 ) this.ItemID = 5099;
				else if( this.ItemID == 5099 ) this.ItemID = 5140;
				else if( this.ItemID == 5140 ) this.ItemID = 5062;
				else if( this.ItemID == 5062 ) this.ItemID = 10130;
				else if( this.ItemID == 10130 ) this.ItemID = 5200;
			}
		}
	}
}
