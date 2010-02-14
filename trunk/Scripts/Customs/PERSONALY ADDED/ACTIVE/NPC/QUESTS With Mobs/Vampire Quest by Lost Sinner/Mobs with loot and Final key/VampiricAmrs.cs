
//////////////////////////
//Created by LostSinner//
////////////////////////
using System;
using Server.Items;

namespace Server.Items
{

	
	public class VampArms : BaseArmor
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
        public VampArms()
            : base(0x144E)
		{
			Weight = 5.0;
			Name = "Lamia Armipotens";
			Hue = 601;
			
			Attributes.ReflectPhysical = 5;
						
			Attributes.SpellDamage = 2;
			Attributes.RegenMana = 1;
			Attributes.LowerManaCost = 2;

            SkillBonuses.SetValues(0, SkillName.Necromancy, 1.0);
		}

		public VampArms( Serial serial ) : base( serial )
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
				Weight = 15.0;
		}
		public override void OnDoubleClick( Mobile from )
		{
            if (!from.Alive) return;

            if (!IsChildOf(from.Backpack))
                from.SendMessage("You must have the item in your pack to morph it."); 
            else
			{
			
				if( this.ItemID == 5198 ) this.ItemID = 5102;
				else if( this.ItemID == 5102 ) this.ItemID = 5136;
				else if( this.ItemID == 5136 ) this.ItemID = 10112;
				else if( this.ItemID == 10112 ) this.ItemID = 10110;
				else if( this.ItemID == 10110 ) this.ItemID = 5069;
				else if( this.ItemID == 5069 ) this.ItemID = 5198;
			}
		}
	}
}
