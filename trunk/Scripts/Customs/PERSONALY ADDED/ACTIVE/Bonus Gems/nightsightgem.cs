using System;
using Server;
using Server.Targeting;

namespace Server.Items
{
	public class nightsightgem : Item
	{
		[Constructable]
		public nightsightgem() : this( 1 )
		{
		}

		[Constructable]
		public nightsightgem( int amount ) : base( 0x186F )
		{
			Stackable = false;
			Weight = 0.1;
			Amount = amount;
			Hue = 0;
			Name = "Night Sight Skill Gem"; 
		}

		public nightsightgem( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) ) from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.

			else if (from.Skills.Tailoring.Value < 100)

				from.SendMessage( "You don't have the requird Tailoring skill to use this gem. You need to have 100+ Tailoring and Blacksmithing in order to use this gem." );

			else if (from.Skills.Blacksmith.Value < 100)

				from.SendMessage( "You don't have the requird Blacksmithing skill to use this gem.  You need to have 100+ Tailoring and Blacksmithing in order to use this gem." );
			else
				from.Target = new InternalTarget(this);
		}

		private class InternalTarget : Target
		{
			private nightsightgem m_nsOrb;
			public InternalTarget( nightsightgem nsOrb ) :  base ( 8, false, TargetFlags.None )
			{
				m_nsOrb = nsOrb;
				AllowNonlocal = true;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Item )
				{
					if ( from.CheckTargetSkill( SkillName.ItemID, o, 0, 100 ) )
					{
						if ( o is BaseWeapon )
							((BaseWeapon)o).Attributes.NightSight = 1;		
						else if ( o is BaseArmor )
							((BaseArmor)o).Attributes.NightSight = 1;	
						else if ( o is BaseJewel )
							((BaseJewel)o).Attributes.NightSight = 1;
						else if ( o is BaseClothing )
							((BaseClothing)o).Attributes.NightSight = 1;

						if ( !Core.AOS )
							((Item)o).OnSingleClick( from );
						
						from.SendMessage( "You Successfully enhance the Item." );
						from.PlaySound( Utility.Random( 0x520, 0 ) );
						m_nsOrb.Delete();
					}
					else
						from.SendMessage( "You fail to enhance the item." ); // You cannot augment that...
				}
				else if ( o is Mobile )
					((Mobile)o).OnSingleClick( from );
				else
					from.SendMessage( "You cannot do that." );
			}
		}
	}
}
