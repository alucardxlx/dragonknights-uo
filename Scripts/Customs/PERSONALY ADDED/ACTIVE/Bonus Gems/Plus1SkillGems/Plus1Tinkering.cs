using System;
using Server;
using Server.Targeting;

namespace Server.Items
{
	public class plus1tinkeringgem : Item
	{
		[Constructable]
		public plus1tinkeringgem() : this( 1 )
		{
		}

		[Constructable]
		public plus1tinkeringgem( int amount ) : base( 0x186E )
		{
			Stackable = false;
			Weight = 0.1;
			Amount = amount;
			Hue = 0;
			Name = "+1 Tinkering Skill Gem"; 
		}

		public plus1tinkeringgem( Serial serial ) : base( serial )
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

			else if (from.Skills.Tailoring.Value < 60)

				from.SendMessage( "You don't have the requird Tailoring skill to use this gem. You need to have 60+ Tailoring and Blacksmithing in order to use this gem." );

			else if (from.Skills.Blacksmith.Value < 60)

				from.SendMessage( "You don't have the requird Blacksmithing skill to use this gem.  You need to have 60+ Tailoring and Blacksmithing in order to use this gem." );
			else
				from.Target = new InternalTarget(this);


		}

		private class InternalTarget : Target
		{
			private plus1tinkeringgem m_cOrb;
			public InternalTarget( plus1tinkeringgem cOrb ) :  base ( 8, false, TargetFlags.None )
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
                            if (targetyouselect is BaseWeapon && ((BaseWeapon)targetyouselect).SkillBonuses.Skill_1_Value < 1 )  //or <= if will let to 2 also
                                ((BaseWeapon)targetyouselect).SkillBonuses.SetValues(0, SkillName.Tinkering, 1);
                            else if (targetyouselect is BaseArmor && ((BaseArmor)targetyouselect).SkillBonuses.Skill_1_Value < 1 )  //or <= if will let to 2 also
                                ((BaseArmor)targetyouselect).SkillBonuses.SetValues(0, SkillName.Tinkering, 1);
                            else if (targetyouselect is BaseJewel && ((BaseJewel)targetyouselect).SkillBonuses.Skill_1_Value < 1 )  //or <= if will let to 2 also
                                ((BaseJewel)targetyouselect).SkillBonuses.SetValues(0, SkillName.Tinkering, 1);
                            else if (targetyouselect is BaseClothing && ((BaseClothing)targetyouselect).SkillBonuses.Skill_1_Value < 1 )  //or <= if will let to 2 also
                                ((BaseClothing)targetyouselect).SkillBonuses.SetValues(0, SkillName.Tinkering, 1);
                            else if (targetyouselect is BaseWeapon && ((BaseWeapon)targetyouselect).SkillBonuses.Skill_2_Value < 1 )  //or <= if will let to 2 also
                                ((BaseWeapon)targetyouselect).SkillBonuses.SetValues(1, SkillName.Tinkering, 1);
                            else if (targetyouselect is BaseArmor && ((BaseArmor)targetyouselect).SkillBonuses.Skill_2_Value < 1 )  //or <= if will let to 2 also
                                ((BaseArmor)targetyouselect).SkillBonuses.SetValues(1, SkillName.Tinkering, 1);
                            else if (targetyouselect is BaseJewel && ((BaseJewel)targetyouselect).SkillBonuses.Skill_2_Value < 1 )  //or <= if will let to 2 also
                                ((BaseJewel)targetyouselect).SkillBonuses.SetValues(1, SkillName.Tinkering, 1);
                            else if (targetyouselect is BaseClothing && ((BaseClothing)targetyouselect).SkillBonuses.Skill_2_Value < 1 )  //or <= if will let to 2 also
                                ((BaseClothing)targetyouselect).SkillBonuses.SetValues(1, SkillName.Tinkering, 1);
                            else
                            {
                                from.SendMessage("That can not be enhanced.");
                                return; //continue might work also
                            }

                            from.SendMessage("You Successfully enhance the Item.");
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
                    from.SendMessage("You fail to enhance the item. Try again."); // You cannot augment that...
                }
                else if (targetyouselect is Mobile)
                    ((Mobile)targetyouselect).OnSingleClick(from);
                else
                    from.SendMessage("You can only enhance Weapons, Armors, Jewlery, or Clothing.");
            }
        }	
	}
}
