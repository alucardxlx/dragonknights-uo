#region Props
// 15JAN2008 written by RavonTUS
//
//   /\\           |\\  ||
//  /__\\  |\\ ||  | \\ ||  /\\  \ //
// /    \\ | \\||  |  \\||  \//  / \\ 
// Play at An Nox, the cure for the UO addiction
// http://annox.no-ip.com  // RavonTUS@Yahoo.com

/* 
 * Moded / Added June 28,2011
 * By: AlphaDragon: help from tass23, lordgreywolf, Talow, and a LOT of others that have helped me get this far! To you all: Thanks!
 * 
*/
#endregion Props

#region Usings


using Server.Accounting;
using Server.ContextMenus;
using Server.Gumps;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Multis;
using Server.Network;
using Server.Targeting;
using Server;
using System.Collections.Generic;
using System.Collections;
using System;


using Server.Engines.Quests;
using Server.Engines.Quests.Haven;


#endregion Usings


#region The Item itself and what it does

namespace Server.Items
{
    [Furniture]
    [Flipable(0x44dd, 0x44e1)]
    public class DragonKnightsGhostClock : Item, ISecurable, IChopable
    {


#region Props needed such as if the item is securable, chopable, or if you want gms to access things in props


private SecureLevel m_Level;



[CommandProperty(AccessLevel.GameMaster)]
public SecureLevel Level{get { return m_Level; }set { m_Level = value; }}


#endregion Props needed such as if the item is securable, chopable, or if you want gms to access things in props

#region Construction


    	[Constructable]
        public DragonKnightsGhostClock() : base(0x44e0)
        {
            Name = "DragonKnight's Clock";
            m_Level = SecureLevel.Anyone;
        }

        
#endregion Construction

#region on player movement


		public override bool HandlesOnMovement { get { return true; } }

        public override void OnMovement(Mobile from, Point3D oldLocation)
        {
                    from.CloseGump(typeof(DragonKnightsGhostClockGump));
        }
        
        
#endregion on player movement


#region on doubleclick


        public override void OnDoubleClick(Mobile from)
        {
          		from.Say("Who you gona call?");
				Effects.SendLocationEffect( Location,Map, 0x3709, 13, 0x3B2, 0 );
				Effects.SendLocationEffect( from,Map, 0x3709, 13, 0x3B2, 0 );
      	from.CloseGump(typeof(DragonKnightsGhostClockGump));
        	BaseHouse house = BaseHouse.FindHouseAt(this);
        	if (house != null)// && house.IsFriend(from))
        	{
        		from.CloseGump(typeof(DragonKnightsGhostClockGump));
        		from.SendGump(new DragonKnightsGhostClockGump(Name));
        		from.PlaySound(0x507);
        	}
        	if (house == null)
        		{
        			from.CloseGump(typeof(DragonKnightsGhostClockGump));
        			from.SendGump(new DragonKnightsGhostClockGump(Name));
        			from.PlaySound(0x507);
        		}
        				
        }


#endregion on doubleclick
        


#region CheckAccess


			public bool CheckAccess(Mobile m)
        	{
        	BaseHouse house = BaseHouse.FindHouseAt(this);

			if (house == null)
				return false;

			if (!house.IsAosRules)
				return true;

			if (house.Public ? house.IsBanned(m) : !house.HasAccess(m))
				return false;

			return house.HasSecureAccess(m, m_Level);
			}
        

#endregion CheckAccess


#region Ischopable What happens when choped


		public void OnChop(Mobile from)
		{
			BaseHouse house = BaseHouse.FindHouseAt(from);
			if (house != null && house.IsOwner(from))
			{
					house.ReleaseSecure(from, (Item)this);
					from.SendLocalizedMessage(500461); // You destroy the item.
					ClockGhost cg = new ClockGhost();
					cg.MoveToWorld( from.Location, from.Map );
					Delete();
					from.PlaySound(from.Female ? 814 : 1088);
					Effects.SendLocationEffect( Location,Map, 0x3709, 13, 0x3B2, 0 );
					Effects.SendLocationEffect( from,Map, 0x3709, 13, 0x3B2, 0 );
					from.Say("AHHHHH GHOST BUSTERRR!!!!");
					from.Hits = 0;
					from.Animate(17,5,1,true,false,0);
			}
			else
				{
				from.SendMessage("That is not accessible.");
				}
		}


#endregion Ishcopable What happens when choped


#region Public Serial
        
        public DragonKnightsGhostClock(Serial serial)
            : base(serial)
        {
        }
        
#endregion Public Serial

        
#endregion The Item itself and what it does
        


#region Item Serializetion
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
            writer.Write((int)m_Level);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            m_Level = (SecureLevel)reader.ReadInt();
        }

#endregion Item Serializetion



#region Item Context Menu
        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        	{
        	from.PlaySound(0x482);
        	from.Say("There is something strange.");
        	Effects.SendLocationEffect( this,Map, 0x3709, 13, 0x3B2, 0 );
        	Effects.SendLocationEffect( from,Map, 0x3709, 13, 0x3B2, 0 );

         	SecureLevel securelevel = this.m_Level;
        	BaseHouse house = BaseHouse.FindHouseAt(this);
        	if (house != null && house.IsOwner(from))
        		{
        		SetSecureLevelEntry.AddTo(from, this, list);
        		}
        	if (!CheckAccess(from))
        		{
        		if (house == null)
        			{
        			base.GetContextMenuEntries(from, list);
        			list.Add(new MenuEntry(from, this));
        			}
        		else
        			{
        			from.SendLocalizedMessage( 1061637 ); // You are not allowed to access this.
        			}
        		}
        	else
        		{
        			base.GetContextMenuEntries(from, list);
        			list.Add(new MenuEntry(from, this));
        		}
        	}


        private class MenuEntry : ContextMenuEntry
        	{
        	private DragonKnightsGhostClock m_Item;
			private Mobile m_Mobile;

			public MenuEntry(Mobile from, Item item)
				: base(159) // uses 3000159
			{
				m_Item = item as DragonKnightsGhostClock;
				m_Mobile = from;
			}

			public override void OnClick()
			{
				// send gump
				m_Mobile.CloseGump(typeof(DragonKnightsGhostClockGump));
				m_Mobile.SendGump(new DragonKnightsGhostClockGump(m_Item.Name));
				m_Mobile.PlaySound(0x507);
				m_Mobile.Say ("Who you gona call?");
			}
		}


#endregion Item Context Menu Stuff



#region end

    }
}
#endregion end




#region Gump

    
namespace Server.Gumps
{
    public class DragonKnightsGhostClockGump : Gump
    {
    	DateTime now = DateTime.Now;
    	
    	public DragonKnightsGhostClockGump(string Name) : base(500, 350)
    		{
    		this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;
            AddPage(0);
            AddBackground(104, 11, 275, 142, 2620);
            AddImage(-38, -38, 666);
            AddLabel(188, 17, 1153, @"Welcome to the");
            AddLabel(196, 41, 1153, @"DragonKnights");
            AddLabel(175, 63, 1153, @"Current Server time:");
            AddLabel(165, 96, 265, @"" + now);
            }
    	}
    }
    
#endregion Gump



#region Deed

	public class DragonKnightsGhostClockDeed : Item
	{
		private bool m_East;
		public bool East { get { return m_East; } set { m_East = value; } }

		[Constructable]
		public DragonKnightsGhostClockDeed() : base( 0x14F0 )
		{
			Name = "DragonKnights Clock Deed";
			Weight = 1.0;
		}

		public DragonKnightsGhostClockDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( Weight == 0.0 )
				Weight = 1.0;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
				{
				BaseHouse house = BaseHouse.FindHouseAt( from );
				if ( house != null && house.IsOwner( from ) )
					{
					from.PlaySound(0x482);
					from.Say("Theres something strange.");
					Effects.SendLocationEffect( Location,Map, 0x3709, 13, 0x3B2, 0 );
					Effects.SendLocationEffect( from,Map, 0x3709, 13, 0x3B2, 0 );

					from.SendLocalizedMessage( 1062838 ); // Where would you like to place this decoration?
					from.BeginTarget( -1, true, TargetFlags.None, new TargetStateCallback( Placement_OnTarget ), null );
					}
				else
					{
					from.SendLocalizedMessage( 502092 ); // You must be in your house to do this.
					}
				}
			else
				{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
				}
		}
		public void Placement_OnTarget( Mobile from, object targeted, object state )
		{
			IPoint3D p = targeted as IPoint3D;
			Map map = from.Map;

			if ( p == null )
				return;

			Point3D loc = new Point3D( p );

			BaseHouse house = BaseHouse.FindHouseAt( loc, from.Map, 16 );

			if ( house != null && house.IsOwner( from ) )
			{
				DragonKnightsGhostClock dkgc = new DragonKnightsGhostClock( );
				dkgc.MoveToWorld( new Point3D( p ), map );
				house.AddSecure(from, (Item)dkgc);
				Delete();
				from.PlaySound(0x482);
				from.Say("In the neighbor hood.");
				Effects.SendLocationEffect( Location,Map, 0x3709, 13, 0x3B2, 0 );
				Effects.SendLocationEffect( from,Map, 0x3709, 13, 0x3B2, 0 );


			}
			else
			{
				from.SendLocalizedMessage( 1042036 ); // That location is not in your house.
			}
		}

	}
	
	
#endregion Deed
	
	
#region Creature
	

namespace Server.Mobiles
{
	[CorpseName( "a dragonknight's corpse" )]
	public class ClockGhost : BaseCreature
	{
		[Constructable]
		public ClockGhost() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.4, 0.8 )
		{
			Name = "a dragonknight's ghost";
			Body = 0x3CA;
			Hue = 20000;
			BaseSoundID = 0x482;

			SetStr( 227, 265 );
			SetDex( 66, 85 );
			SetInt( 46, 70 );

			SetHits( 140, 156 );

			SetDamage( 14, 20 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 5, 10 );
			SetResistance( ResistanceType.Energy, 5, 10 );

			SetSkill( SkillName.MagicResist, 65.1, 80.0 );
			SetSkill( SkillName.Swords, 20.1, 30.0 );
			SetSkill( SkillName.Tactics, 80.1, 100.0 );
			SetSkill( SkillName.Wrestling, 80.1, 100.0 );

			Fame = 500;
			Karma = -500;

			VirtualArmor = 6;
			PackItem(new DragonKnightsGhostClockDeed());
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Poor );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Gems );
		}

		public override bool AlwaysAttackable{ get{ return true; } }
		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public override void DisplayPaperdollTo(Mobile to)
		{
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );

			for ( int i = 0; i < list.Count; ++i )
			{
				if ( list[i] is ContextMenus.PaperdollEntry )
					list.RemoveAt( i-- );
			}
		}

		public ClockGhost( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
	#endregion Creature
