// Author: XmlGrab 1.0, by Oak
// Date: 7/1/2006
// Version: 1.0
// Requirements: Runuo 2.0, XmlSpawner2
// History: 
//	Used "Claim System version 1.6, by Xanthos" as a base
//	added XmlAttachment to allow player specific looting options, 
//	removed external config file and other dependencies.
//	simplified code and made it grab/claim corpses only.
//
using System;
using System.Collections;
using System.Reflection;
using Server;
using Server.Items;
using Server.Misc;
using Server.Spells;
using Server.Guilds;
using Server.Mobiles;
using Server.Engines.PartySystem;
using Server.Engines.XmlSpawner2;
using Server.Gumps;

namespace Server.Commands
{
	public class XmlGrab
	{
		//============= Configuration ================================//
		
		
		/* How many tiles around the player that 'grab' will search	*/
		private const int GrabRadius = 5;

		/*  Set to a positive value to make players manually scavenge
			when other players are near.
			Set to zero to always allow grabbing in proximity of
			other players.	*/
		private const  int CompetitiveGrabRadius = 0;

		/* allow player corpses to be looted */
		private const  bool LootPlayers = false;

		/* Mobs with fame below this require no looting rights */
		private const  int FreelyLootableFame = 1000;

		/* minimum reward for corpes */
//		private const  int MinimumReward = 5; //REMED OUT SO THAT REGULAR MOBS WONT GIVE GOLD WHEN USE GRAB COMMAND
		
		/* Reward = (((mob fame + |mob karma| + player fame + |player karma|)/1.5) / FameDivisor)
			Lower divisor yeilds higher reward.  In this formula, it pays to keep your fame
			and karma high.	 */
//		private  const int FameDivisor = 150;//REMED OUT SO THAT REGULAR MOBS WONT GIVE GOLD WHEN USE GRAB COMMAND


		public static void Initialize()
		{
				CommandSystem.Register( "Grab", AccessLevel.Player, new CommandEventHandler( Grab_OnCommand ) );
		}

		public static bool IsArtifact( Item item )
		{
			if ( null == item )
				return false;
		
			Type t = item.GetType();
			PropertyInfo prop = null;

			try { prop = t.GetProperty( "ArtifactRarity" ); }
			catch {}

			if ( null == prop || (int)(prop.GetValue( item, null )) <= 0 )
				return false;			

			return true;
		}

		[Usage( "Grab" )]
		[Description( "Grab lootable items off of the ground and claim nearby corpses" )]
		public static void Grab_OnCommand( CommandEventArgs e )
		{

			//   Get LootData attachment 
			LootData lootoptions = new LootData();
			// does player already have a lootdata attachment?
			if (XmlAttach.FindAttachment(e.Mobile, typeof(LootData))==null)
			{
				XmlAttach.AttachTo(e.Mobile, lootoptions);
				// give them one free lootbag
				e.Mobile.AddToBackpack ( new LootBag());
			}
			else
			{
				// they have the attachment, just load their options
				lootoptions=(LootData)XmlAttach.FindAttachment(e.Mobile,typeof(LootData));
			}

		//   Check args to see if they want to change loot options
		// if we have args after  "grab"
			if ( e.Length != 0 )
			{
				// we need to set the loot bag
				if (e.GetString(0) != "options")
				{
					e.Mobile.SendMessage ("Typing the command [grab  by itself loots corpses of your victims. [grab options will allow you to decide what you want to loot.");
				}
				// show loot options gump
				else if (e.GetString(0) == "options")
				{
				   e.Mobile.SendGump( new LootGump(e.Mobile));
				}
			}

			//   Check loot legalities
			Mobile from = e.Mobile;

			if ( from.Alive == false )
			{
				from.PlaySound( 1069 ); //hey
				from.SendMessage( "You cannot do that while you are dead!" );
				return;
			}
			//else if ( 0 != CompetitiveGrabRadius && BlockingMobilesInRange( from, GrabRadius ))
			//{
			//	from.PlaySound( 1069 ); //hey
			//	from.SendMessage( "You are too close to another player to do that!" );
			//	return;
			//}

			ArrayList grounditems = new ArrayList();
			ArrayList lootitems = new ArrayList();
			ArrayList corpses = new ArrayList();
			Container lootBag = GetLootBag( from );

			// Gather lootable corpses and items into lists
			foreach ( Item item in from.GetItemsInRange( GrabRadius ))
			{
				if ( !from.InLOS( item ) || !item.IsAccessibleTo( from ) || !(item.Movable || item is Corpse) )
					continue;

				// add to corpse list if corpse
				if ( item is Corpse && CorpseIsLootable( from, item as Corpse, false ) ) // && item.Killer == from
				{
					Corpse deadbody = item as Corpse;
					if ( deadbody.Killer == null )
						corpses.Add( item );
					else if ( deadbody.Killer == from )
					{
						corpses.Add( item );
					}
					else if ( deadbody.Killer is BaseCreature && !( deadbody.Killer is PlayerMobile ) )
					{
						BaseCreature pet = deadbody.Killer as BaseCreature;
						if ( pet.ControlMaster == from || pet.ControlMaster == null )
						{
							corpses.Add( item );
						}
					}
					
				}
	
				// otherwise add to ground items list if loot options indicate
				else if ( !( item is PlayerMobile ) )
				{
					if(lootoptions.GetGroundItems)
						if (!(item is Corpse))
							grounditems.Add( item );
				}
			}

			// see if we really want any of the junk lying on the ground
			GetItems(lootoptions, from, grounditems);

			grounditems.Clear();

			// now inspect and loot appropriate items in corpses
			foreach ( Item corpse in corpses )
            {
                Corpse bod = corpse as Corpse;
                PlayerMobile pm = from as PlayerMobile;
                //pm.PlayerLevel += 1;

                /*                                          // Uncomment for eventual modifications (not to allow grabbing certain bodies)
                Mobile own = bod.Owner as Mobile;
                if( (own is Mobile1) || (own is Mobile2) )  // Change mobile names according to what you want to get
                */
                {

                    // if we are looting hides/scales/meat then carve the corpse
                    if (lootoptions.GetHides && !(bod.Owner is PlayerMobile))
                        bod.Carve(from, null);

                    //rummage through the corpse for good stuff
                    foreach (Item item in bod.Items)
                        lootitems.Add(item);

                    //  now see if we really want any of this junk
                    GetItems(lootoptions, from, lootitems);

                    // alrighty then, we have all the items we want, now award gold for this corpse, delete it and increment the body count
//                    AwardGold(from, bod, lootBag); //REMED OUT SO THAT REGULAR MOBS WONT GIVE GOLD WHEN USE GRAB COMMAND
                    bod.Delete();

                    // empty lootitems arraylist
                    lootitems.Clear();
                }

            }
		}
		private static void GetItems(LootData lootoptions, Mobile from, ArrayList loothopefuls)
		{
			ArrayList itemstoloot = new ArrayList();

			Container lootBag = GetLootBag( from );

			foreach(Item item in loothopefuls)
			{

                if (item is Server.Items.Gold)
                    itemstoloot.Add(item);
                /*else if( !(item is xxx || item is yyy) ) // Uncomment for eventual customizations (not to allow grabbing certain items)
                     itemstoloot.Add(item);
                */
                else if (lootoptions.GetAll)
                    itemstoloot.Add(item);
                else
                {
                    if ((IsArtifact(item)) && (lootoptions.GetArtifacts))
                        itemstoloot.Add(item);
                    else if ((item is Server.Items.BaseWeapon) && (lootoptions.GetWeapons))
                        itemstoloot.Add(item);
                    else if ((item is Server.Items.BaseArmor) && (lootoptions.GetArmor))
                        itemstoloot.Add(item);
                    else if ((item is Server.Items.BaseClothing || item is Server.Items.BaseShoes) && (lootoptions.GetClothing))
                        itemstoloot.Add(item);
                    else if ((item is Server.Items.BaseJewel) && (lootoptions.GetJewelry))
                        itemstoloot.Add(item);
                    else if ((item is Server.Items.Diamond || item is Server.Items.Amber || item is Server.Items.Amethyst || item is Server.Items.Citrine || item is Server.Items.Emerald || item is Server.Items.Ruby || item is Server.Items.Sapphire || item is Server.Items.StarSapphire || item is Server.Items.Tourmaline) && (lootoptions.GetGems))
                        itemstoloot.Add(item);
                    else if ((item is Server.Items.Arrow || item is Server.Items.Bolt) && (lootoptions.GetArrows))
                        itemstoloot.Add(item);
                    else if ((item is Server.Items.CookableFood || item is Server.Items.Food) && (lootoptions.GetFood))
                        itemstoloot.Add(item);
                    else if ((item is Server.Items.BaseHides) && (lootoptions.GetHides))
                        itemstoloot.Add(item);
                    else if ((item is Server.Items.BaseScales) && (lootoptions.GetScales))
                        itemstoloot.Add(item);
                    else if ((item is Server.Items.Board || item is Server.Items.Log || item is Server.Items.Shaft) && (lootoptions.GetWood))
                        itemstoloot.Add(item);
                    else if ((item is Server.Items.BaseIngot || item is Server.Items.BaseOre) && (lootoptions.GetOres))
                        itemstoloot.Add(item);
                    else if ((item is Server.Items.BaseReagent) && (lootoptions.GetReagents))
                        itemstoloot.Add(item);
                    else if ((item is Server.Items.BasePotion) && (lootoptions.GetPotions))
                        itemstoloot.Add(item);
                    else if (lootoptions.GetOther) 
                        itemstoloot.Add(item);
                }

				
				// if we have any items
				if(itemstoloot != null)
				{
					// Drop all of the items into player's bag/pack
					foreach ( Item itemx in itemstoloot )
					{
						if ( !lootBag.TryDropItem( from, itemx, false ) )
							lootBag.DropItem( itemx );
						from.PlaySound( 0x2E6 ); // drop gold sound
					}
					itemstoloot.Clear();
				}
			}
		}

		//    determine if the corpse is ok to loot
		private static bool CorpseIsLootable( Mobile from, Corpse corpse, bool notify )
		{
			if ( null == corpse )
				return false;

			bool result = false;
			string notification = "";

			if ( corpse.Owner == from )
				notification = "You may not claim your own corpses.";
			else if ( corpse.Owner is PlayerMobile && !LootPlayers )
				notification = "You may not loot player corpses.";
			else
			{
				BaseCreature creature = corpse.Owner as BaseCreature;

				if ( null != creature && creature.IsBonded )
					notification = "You may not loot the corpses of bonded pets.";
				else if ( null != creature && creature.Fame <= FreelyLootableFame )
					result = true;
				else
					result = corpse.CheckLoot( from, null ) && !( corpse.IsCriminalAction( from ) );
			}

			if ( false == result && notify )
			{
				from.PlaySound( 1074 );		// no
				from.SendMessage( notification );
			}

			return result;
		}

// ================================  make sure we have a loot bag
		public static Container GetLootBag( Mobile from )
		{
			Container lootBag = from.Backpack.FindItemByType( typeof(LootBag) ) as Container;
			return ( null == lootBag ) ? from.Backpack : lootBag;
		}

// ================================  reward the player for not leaving those stinking corpses lying about //REMED OUT SO THAT REGULAR MOBS WONT GIVE GOLD WHEN USE GRAB COMMAND
//		public static void AwardGold( Mobile from, Corpse corpse, Container lootbag )
//		{
//			BaseCreature mob = corpse.Owner as BaseCreature;
//			int mobBasis = ( mob == null ? MinimumReward : mob.Fame + Math.Abs( mob.Karma ) );
//			int playerBasis = ( from.Fame + Math.Abs( from.Karma ) );
//			int amount = Math.Max( (int)((mobBasis + playerBasis) / 1.5) / FameDivisor, MinimumReward );
//			if ( !lootbag.TryDropItem( from, new Gold(amount), false ) )	// Attempt to stack it
//				lootbag.DropItem( new Gold(amount) );
//			from.PlaySound( 0x2E6 ); // drop gold sound
//
//		}

// ================================   Is someone too close for comfort?
		public static bool BlockingMobilesInRange( Mobile from, int range )
		{
			foreach ( Mobile mobile in from.GetMobilesInRange( range ) )
			{
				if ( mobile is PlayerMobile && IsBlockingMobile( from, mobile ) )
					return true;
			}
			return false;
		}

		public static bool IsBlockingMobile( Mobile looter, Mobile other )
		{
			// Self and hidden staff dont count
			if ( looter == other || ( other.Hidden && other.AccessLevel > AccessLevel.Player ) )
				return false;

			Guild looterGuild = SpellHelper.GetGuildFor( looter );
			Guild otherGuild = SpellHelper.GetGuildFor( other );

			if ( null != looterGuild && null != otherGuild && ( looterGuild == otherGuild || looterGuild.IsAlly( otherGuild ) ) )
				return false;

			Party party = Party.Get( looter );

			return !( null != party && party.Contains( other ) );
		}
	}
	
// ================================  Loot Options gump



    public class LootGump : Gump
    {


        public LootGump(Mobile m) : base( 200, 100 )
        {
            LootData lootoptions = (LootData)XmlAttach.FindAttachment(m, typeof(LootData));


            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddBackground(0, 0, 450, 511, 9380);

			AddLabel(166, 4, 0, @"Looting Options");
			AddLabel(108, 37, 233, @"Select what you want to be looted");

            AddLabel(40, 90, 0, @"All Items");
			AddCheck(160, 90, 1151, 1154, lootoptions.GetAll, 1);

			AddLabel(39, 130, 0, @"Artifacts");
            AddCheck(160, 130, 1151, 1154, lootoptions.GetArtifacts, 2);

			AddLabel(40, 170, 0, @"Weapons");
            AddCheck(160, 170, 1151, 1154, lootoptions.GetWeapons, 3);

			AddLabel(40, 210, 0, @"Armor");
            AddCheck(160, 210, 1151, 1154, lootoptions.GetArmor, 4);

			AddLabel(40, 250, 0, @"Clothing");
            AddCheck(160, 250, 1151, 1154, lootoptions.GetClothing, 5);

			AddLabel(40, 290, 0, @"Jewelry");
            AddCheck(160, 290, 1151, 1154, lootoptions.GetJewelry, 6);
			
			AddLabel(40, 330, 0, @"Gems");
            AddCheck(160, 330, 1151, 1154, lootoptions.GetGems, 7);

			AddLabel(40, 370, 0, @"Bolts/Arrows");
            AddCheck(160, 370, 1151, 1154, lootoptions.GetArrows, 8);

			AddLabel(270, 90, 0, @"Food");
            AddCheck(380, 90, 1151, 1154, lootoptions.GetFood, 9);

			AddLabel(270, 130, 0, @"Hides");
            AddCheck(380, 129, 1151, 1154, lootoptions.GetHides, 10);

			AddLabel(270, 170, 0, @"Scales");
            AddCheck(380, 170, 1151, 1154, lootoptions.GetScales, 11);

			AddLabel(270, 210, 0, @"Wood");
            AddCheck(380, 210, 1151, 1154, lootoptions.GetWood, 12);

			AddLabel(270, 250, 0, @"Ores");
            AddCheck(380, 250, 1151, 1154, lootoptions.GetOres, 13);
			
            AddLabel(270, 290, 0, @"Reagents");
            AddCheck(380, 290, 1151, 1154, lootoptions.GetReagents, 14);

			AddLabel(270, 330, 0, @"Potions");
            AddCheck(380, 330, 1151, 1154, lootoptions.GetPotions, 15);

            AddLabel(270, 370, 0, @"Other Things");
            AddCheck(380, 370, 1151, 1154, lootoptions.GetOther, 16);

            AddLabel(140, 410, 0, @"Get Ground Items");
            AddCheck(280, 410, 1151, 1154, lootoptions.GetGroundItems, 17);
			
			AddButton(349, 450, 247, 248, 1, GumpButtonType.Reply, 0);
			AddButton(30, 450, 241, 242, 0, GumpButtonType.Reply, 0);
        }



        public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
        {
            PlayerMobile pm = sender.Mobile as PlayerMobile;
		    LootData lootoptions = (LootData)XmlAttach.FindAttachment(pm, typeof(LootData));

            switch(info.ButtonID)
            {
                case 0:     // closed or cancel
				{
					break;
				}
				case 1:     // OKAY button pressed
				{
                    ArrayList Selections = new ArrayList( info.Switches );

					if( Selections.Contains( 1 ) == true )  lootoptions.GetAll=true;
					    else    lootoptions.GetAll=false;

                    if( Selections.Contains( 2 ) == true )  lootoptions.GetArtifacts=true;
					    else    lootoptions.GetArtifacts=false;

                    if( Selections.Contains( 3 ) == true )  lootoptions.GetWeapons=true;
					    else    lootoptions.GetWeapons=false;

                    if( Selections.Contains( 4 ) == true )  lootoptions.GetArmor=true;
					    else    lootoptions.GetArmor=false;

                    if( Selections.Contains( 5 ) == true )  lootoptions.GetClothing=true;
					    else    lootoptions.GetClothing=false;

                    if( Selections.Contains( 6 ) == true )  lootoptions.GetJewelry=true;
					    else    lootoptions.GetJewelry=false;

                    if( Selections.Contains( 7 ) == true )  lootoptions.GetGems=true;
					    else    lootoptions.GetGems=false;

                    if( Selections.Contains( 8 ) == true )  lootoptions.GetArrows=true;
					    else    lootoptions.GetArrows=false;

                    if( Selections.Contains( 9 ) == true )  lootoptions.GetFood=true;
					    else    lootoptions.GetFood=false;

                    if( Selections.Contains( 10 ) == true )  lootoptions.GetHides=true;
					    else    lootoptions.GetHides=false;

                    if( Selections.Contains( 11 ) == true )  lootoptions.GetScales=true;
					    else    lootoptions.GetScales=false;

                    if( Selections.Contains( 12 ) == true )  lootoptions.GetWood=true;
					    else    lootoptions.GetWood=false;

                    if( Selections.Contains( 13 ) == true )  lootoptions.GetOres=true;
					    else    lootoptions.GetOres=false;

                    if( Selections.Contains( 14 ) == true )  lootoptions.GetReagents=true;
					    else    lootoptions.GetReagents=false;

                    if( Selections.Contains( 15 ) == true )  lootoptions.GetPotions=true;
					    else    lootoptions.GetPotions=false;

                    if( Selections.Contains( 16 ) == true )  lootoptions.GetOther=true;
					    else    lootoptions.GetOther=false;

                    if( Selections.Contains( 17 ) == true )  lootoptions.GetGroundItems=true;
					    else    lootoptions.GetGroundItems=false;

					break;
				}
            }
        }
    }
}
