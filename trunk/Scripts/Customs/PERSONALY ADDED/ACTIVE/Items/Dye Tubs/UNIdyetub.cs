//Scripted by: Espcevan
using Server;
using System;
using Server.Items;
using Server.Multis;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Items
{
	public class UNIDyeTubTarget : Target
	{
		private Item m_Tub;
		private int theHue;

		public UNIDyeTubTarget( UNIDyeTub dyetub ) : base( 12, false, TargetFlags.None )
		{
			m_Tub = dyetub;
			theHue = (int) dyetub.DyedHue;
		}

		protected override void OnTarget( Mobile from, object targeted )
		{
			if ( targeted is Item )
			{
				Item item = (Item) targeted;
				if( (FurnitureAttribute.Check( item ) || (item is PotionKeg)) || item is BaseArmor || item is BaseWeapon || item is IDyable || item is MonsterStatuette || item is EtherealMount || item is Spellbook || item is Runebook || item is RecallRune )
				{
					if( !item.IsChildOf(from.Backpack) )
						from.SendMessage("The item must be in your pack.");	
					else
					{
						item.Hue = theHue;
						from.PlaySound( 0x23F );
					}
				}
				else
					from.SendMessage("That item cannot be dyed.");
			}
			else
				from.SendMessage("You cannot dye that.");
		}
	}

	public class UNIDyeTub : DyeTub
	{
		private int m_DyedHue;
		private bool m_Redyable = true;
		private bool m_AllowPack;

		[CommandProperty( AccessLevel.Player )]
		public bool AllowPack
		{
			get	{return m_AllowPack;}
			set	{m_AllowPack = value;}
		}

		[Constructable] 
		public UNIDyeTub()
		{
			Weight = 0.0;
			m_Redyable = true;
			Name = "Potion Keg, Basic Metal Armor and Weapon, Monster Statuette, Ethereal Mount, Spellbook, Runebook, Recall Rune, Dye Tub.";
		}

		[Constructable] 
		public UNIDyeTub( int newHue )
		{
			Weight = 0.0;
			m_Redyable = true;
			Hue=newHue;
			DyedHue=newHue;
		}

		public UNIDyeTub( Serial serial ) : base( serial ){}
		
		public override void OnDoubleClick( Mobile from )
		{
			if ( this.IsChildOf(from.Backpack) )
			DoPack( from );
			else
			DoOut( from );
		}

		public void DoPack( Mobile from )
		{
			if(AllowPack)
				DoOut( from );
			else
				from.SendMessage("The dyetub cannot be in your pack.");
		}

		public void DoOut ( Mobile from )
		{
			if ( from.InRange( this.GetWorldLocation(), 1 ) )
			{
				from.SendMessage( "Select the item to dye" );
				from.Target = new UNIDyeTubTarget( this );
			}
			else
				from.SendLocalizedMessage( 500446 ); // That is too far away.
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version 
			writer.Write( (int) m_DyedHue);
			writer.Write( (bool) m_Redyable );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_DyedHue = reader.ReadInt();
			m_Redyable = reader.ReadBool();
		}
	}
}
