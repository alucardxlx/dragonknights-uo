using System;
using Server.Network;
using Server.Prompts;
using Server.Mobiles;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	public class spetdyeyTarget : Target // Create our targeting class (which we derive from the base target class)
	{
		private spetdyey m_Deed;

		public spetdyeyTarget( spetdyey deed ) : base( 1, false, TargetFlags.None )
		{
			m_Deed = deed;
		}

		protected override void OnTarget( Mobile from, object target ) // Override the protected OnTarget() for our feature
		{
			if ( target is BaseCreature )
			{
				BaseCreature bc = (BaseCreature)target;

				if ( bc.Controlled == true || bc.ControlMaster == from ) 
				{
					from.SendMessage( "You dye the pet" );
					bc.Hue = 1281;
					m_Deed.Delete();
				}
				else if ( bc.Controlled == false )
				{
					from.SendMessage( "That creature is wild!" );
				}
				
			}
			else
			{
				from.SendMessage( "that is not your pet!" );
			}
		}
	}

	public class spetdyey : Item // Create the item class which is derived from the base item class
	{
		[Constructable]
		public spetdyey() : base( 3836 )
		{
			Weight = 1.0;
			Name = "a pet dye bottle";
			Hue = 1281;
		}

		public spetdyey( Serial serial ) : base( serial )
		{
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			Hue = 1281;

			int version = reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		
		

		public override void OnDoubleClick( Mobile from ) // Override double click of the deed to call our target
		{
			if ( !IsChildOf( from.Backpack ) ) // Make sure its in their pack
			{
				 from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
			else
			{
				from.SendMessage( "which pet would you like to dye?" );
				from.Target = new spetdyeyTarget( this ); // Call our target
			 }
		}	
	}
}


