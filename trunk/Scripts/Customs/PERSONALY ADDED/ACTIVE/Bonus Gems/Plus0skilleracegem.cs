using System;
using Server;
using Server.Targeting;

namespace Server.Items
{
	public class plus0skilleracegem : Item
	{
		[Constructable]
		public plus0skilleracegem() : this( 1 )
		{
		}

		[Constructable]
		public plus0skilleracegem( int amount ) : base( 0x1ED2 )
		{
			Stackable = false;
			Movable = true;
			Weight = 0.1;
			Amount = amount;
			Hue = 0;
			Name = "Skill Erace Gem - Will erace all skill bonuses on Armor, Weapons, Jewlery, and Clothing. "; 
		}

		public plus0skilleracegem( Serial serial ) : base( serial )
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

			else if (from.Skills.Tailoring.Value < 115)

				from.SendMessage( "You don't have the requird Tailoring skill to use this gem. You need to have 115+ Tailoring and Blacksmithing in order to use this gem." );

			else if (from.Skills.Blacksmith.Value < 115)

				from.SendMessage( "You don't have the requird Blacksmithing skill to use this gem.  You need to have 115+ Tailoring and Blacksmithing in order to use this gem." );
			else
				from.Target = new InternalTarget(this);


		}

		private class InternalTarget : Target
		{
			private plus0skilleracegem m_cOrb;
			public InternalTarget( plus0skilleracegem cOrb ) :  base ( 8, false, TargetFlags.None )
			{
				m_cOrb = cOrb;
				AllowNonlocal = true;
			}

protected override void OnTarget(Mobile from, object targetyouselect)
            {
                if (targetyouselect is Item)
                {
                    if (from.CheckTargetSkill(SkillName.ItemID, targetyouselect, 0, 100))
                    {
                        if ( ((Item)targetyouselect).IsChildOf( from.Backpack ) )
                        {
                        	if (targetyouselect is BaseWeapon)
                        	    {
                        		((BaseWeapon)targetyouselect).SkillBonuses.SetValues(0, SkillName.AnimalLore, 0);
								((BaseWeapon)targetyouselect).SkillBonuses.SetValues(1, SkillName.AnimalLore, 0);
                        	    }
                        	else if (targetyouselect is BaseArmor)
                        		{
                        		((BaseArmor)targetyouselect).SkillBonuses.SetValues(0, SkillName.AnimalLore, 0);
                        		((BaseArmor)targetyouselect).SkillBonuses.SetValues(1, SkillName.AnimalLore, 0);
                        		}
                        	else if (targetyouselect is BaseJewel)
                        		{
                        		((BaseJewel)targetyouselect).SkillBonuses.SetValues(0, SkillName.AnimalLore, 0);
                        	    ((BaseJewel)targetyouselect).SkillBonuses.SetValues(1, SkillName.AnimalLore, 0);
								}
                        	else if (targetyouselect is BaseClothing)
                        		{
                        		((BaseClothing)targetyouselect).SkillBonuses.SetValues(0, SkillName.AnimalLore, 0);
                        		((BaseClothing)targetyouselect).SkillBonuses.SetValues(1, SkillName.AnimalLore, 0);
                        		}
                        	else
                        		{
								from.SendMessage("That can not be unenhanced.");
								return; //continue might work also
								}

                            from.SendMessage("You Successfully unenhance the Item.");
                            from.PlaySound(Utility.Random(0x520, 0));
                            m_cOrb.Delete();
                        }
                        else
                        {
                            from.SendMessage("The target item must be in your backpack to use this.");
                            return; //continue might work also
                        }
                    }
                    else
                    from.SendMessage("You fail to unenhance the item. Try again."); // You cannot augment that...
                }
                else if (targetyouselect is Mobile)
                    ((Mobile)targetyouselect).OnSingleClick(from);
                else
                    from.SendMessage("You can only unenhance Weapons, Armors, Jewlery, or Clothing.");
            }
        }	
	}
}
