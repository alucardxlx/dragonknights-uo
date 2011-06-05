using System;
using Server.Network;
using Server.Prompts;
using Server.Mobiles;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	public class PetColorResetToDefaultPotionTarget : Target // Create our targeting class (which we derive from the base target class)
	{
		private PetColorResetToDefaultPotion m_Deed;

		public PetColorResetToDefaultPotionTarget( PetColorResetToDefaultPotion deed ) : base( 1, false, TargetFlags.None )
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
					from.SendMessage( 65,"You wash off the dye thats on the pet. The pet now has its original color." );
					bc.Hue = 0;
					m_Deed.Delete();
				}
				else if ( bc.Controlled == false )
				{
					from.SendMessage( 38,"That creature is wild!" );
				}
				
			}
			else
			{
				from.SendMessage( 38,"That is not your pet!" );
			}
		}
	}

	public class PetColorResetToDefaultPotion : Item // Create the item class which is derived from the base item class
	{
		[Constructable]
		public PetColorResetToDefaultPotion() : base( 3836 )
		{
			Weight = 1.0;
			Name = "A pet dye reset bottle. - This will reset your pet's color, back to the pet's default color.";
			Hue = 1150;
		}

		public PetColorResetToDefaultPotion( Serial serial ) : base( serial )
		{
		}
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			Hue = 1150;

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
				from.SendMessage( 52,"Which pet would you like to reset to the original color?" );
				from.Target = new PetColorResetToDefaultPotionTarget( this ); // Call our target
			 }
		}	
	}
}


