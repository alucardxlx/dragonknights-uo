  /////////////////////////////
 //////  LostSinner  /////////
/////////////////////////////

using System;
using Server;

namespace Server.Items
{
	public class VampireRobe : BaseArmor, IDyable
	{ 
        public override int ArtifactRarity{ get{return 5; } }

		
		public override int BaseFireResistance{ get{ return 1; } }
		public override int BaseColdResistance{ get{ return 1; } }
		public override int BasePoisonResistance{ get{ return 1; } }
		public override int BaseEnergyResistance{ get{ return 1; } }

public override void OnAdded( object parent )
		{
			base.OnAdded( parent );

			if ( parent is Mobile )
				((Mobile)parent).VirtualArmorMod += 3;
		}

		public override void OnRemoved(object parent)
		{
			base.OnRemoved( parent );

			if ( parent is Mobile )
				((Mobile)parent).VirtualArmorMod -= 3;
		}
                 
                   public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            // rocko mod
            list.Add("Inate armor rating +3");
        }

		public override int OldDexBonus{ get{ return -8; } }

		

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Cloth; } }

		[Constructable]
		public VampireRobe() : base( 0x2684 )
		{
       		Name = "Vampire's Robe";			
			Attributes.WeaponDamage = 5;
            Layer = Layer.OuterTorso;
            ItemID = 0x2684;
			Weight = 3.0;
		}

		public VampireRobe( Serial serial ) : base( serial )
		{
		}

      public override void OnDoubleClick( Mobile m )
      {
         if( Parent != m )
         {
            m.SendMessage( "You must be wearing the robe to use it!" );
         }
         else
         {
            if ( ItemID == 0x2683 || ItemID == 0x2684 )
            {
               m.SendMessage( "You lower the hood." );
               m.PlaySound( 0x57 );
               ItemID = 0x1F03;
               m.NameMod = null;
               m.RemoveItem(this);
               m.EquipItem(this);
               /*if( m.Kills >= 5)
               {
               m.Criminal = false;
                }
                if( m.GuildTitle != null)
               {
                  m.DisplayGuildTitle = false;
                }*/
            }
            else if ( ItemID == 0x1F03 || ItemID == 0x1F04 )
            {
               m.SendMessage( "You pull the hood over your head." );
               m.PlaySound( 0x57 );
               ItemID = 0x2683;
               m.RemoveItem(this);
               m.EquipItem(this);

		}

		}
}
		public virtual bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			Hue = sender.DyedHue;

			return true;
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