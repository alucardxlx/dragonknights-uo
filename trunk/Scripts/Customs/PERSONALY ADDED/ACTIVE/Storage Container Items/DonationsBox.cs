// All thanks goes to Cyberspud for the original script.
// Modified for RunUO2 18 June 2006 (Xeraz)

using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Accounting;
using Server.Gumps;
using Server.Commands;


namespace Server.Items
{
	[FlipableAttribute( 0xE41, 0xE40 )]
	public class DonationBox : LockableContainer
	{


		private bool m_AutoLock;
		private InternalTimer m_RelockTimer;		

		[CommandProperty( AccessLevel.GameMaster )]
		public bool AutoLock
		{
			get { return m_AutoLock; }
			set
			{
				m_AutoLock = value;

				if ( !m_AutoLock )
					StopTimer();
				else if ( !Locked && m_RelockTimer == null )
					m_RelockTimer = new InternalTimer( this );
			}
		}

		public static int MaxDonations = 10; // maximum number of donations

		public override bool Decays{ get{ return false; } } 
		
		public override int DefaultGumpID{ get{ return 0x42; } }
		public override int DefaultDropSound{ get{ return 0x42; } }

		public override Rectangle2D Bounds
		{
			get{ return new Rectangle2D( 16, 51, 168, 73 ); }
		}
		[Constructable]
		public DonationBox() : base( 0xE41 )
		{
			//Hue = 1160;
			Hue = 2122;
			Movable = false;			
			Name = "a donation box";
			LiftOverride = true;
			
		}

		public DonationBox( Serial serial ) : base( serial )
		{
		}


		[CommandProperty( AccessLevel.GameMaster )]
		public override bool Locked
		{
			get { return base.Locked; }
			set
			{
				base.Locked = value;

				if ( m_AutoLock )
				{
					StopTimer();

					if ( !Locked )
						m_RelockTimer = new InternalTimer( this );
				}
			}
		}


		public void StopTimer()
		{
			if ( m_RelockTimer != null )
				m_RelockTimer.Stop();

			m_RelockTimer = null;
		}

		private class InternalTimer : Timer
		{
			private DonationBox m_Container;
			private DateTime m_RelockTime;

			public DonationBox Container { get { return m_Container; } }
			public DateTime RelockTime { get { return m_RelockTime; } }

			public InternalTimer( DonationBox container ) : this( container, TimeSpan.FromMinutes( 5.0 ) )
			{
			}

			public InternalTimer( DonationBox container, TimeSpan delay ) : base( delay )
			{
				m_Container = container;
				m_RelockTime = DateTime.Now + delay;

				Start();
			}

			protected override void OnTick()
			{
				m_Container.Locked = true;
				m_Container.LockLevel = -255;
			}
		}



		public override void OnDoubleClick(Mobile from)
		{
			//make sure they are a "newbie" or a GM
			if ( from.AccessLevel >= AccessLevel.GameMaster )
				this.SendLocalizedMessageTo(from,1042971,"You have the authority to access Donation Boxes.");
			else if( !(CheckTag(from)) )
			{
				//Greedy!
				this.SendLocalizedMessageTo(from,1042971,"You have taken more then enough from the Donation Boxes.  Greed is not a virtue.");
				return;
			}
			else if( CanGet(from) )
				this.SendLocalizedMessageTo(from,1042971,"As a new comer to the realm, you are allowed to access Donation Boxes.");
			else
			{				
				this.SendLocalizedMessageTo(from,1042971,"Only new comers to the realm may access Donation Boxes.");
				return;
			}

			base.OnDoubleClick (from);

			//increase tag
			if( from.AccessLevel == AccessLevel.Player )
				IncreaseTag( from, 1);

			//show gump
			from.CloseGump( typeof(DonationBoxGump) );
			from.SendGump( new DonationBoxGump( from, MaxDonations ) );
		}

		public override void OnTelekinesis(Mobile from)
		{
			from.SendLocalizedMessage( 501857 ); //This spell won't work on that!
		}

		public override bool OnDragDropInto( Mobile from, Item item, Point3D p )
		{
			//make sure it is a valid item
			if( ValidItem( item ) )
			{
				if ( ValidStack( item) )
				{
					return base.OnDragDropInto( from, item, p );
				}
				else
				{
					this.SendLocalizedMessageTo(from,1042971,"Stacked items must be in quantities over 100.");
					return false;
				}
			}
			this.SendLocalizedMessageTo(from,1042971,"That cannot be placed into a Donation Box.");
			return false;
		}

		public override bool OnDragDrop(Mobile from, Item dropped)
		{
			//make sure it is a valid item
			if( ValidItem( dropped ) )
			{
				if ( ValidStack( dropped) )
				{
					this.SendLocalizedMessageTo(from,1042971,"Thank you very much for your donation!");
					if( from.Female )
					{
						from.PlaySound( 780 ); //applause
						from.PlaySound( 783 ); //cheer
					}
					else
					{
						from.PlaySound( 1051 ); //applause
						from.PlaySound( 1054 ); //cheer
					}
					return base.OnDragDrop( from, dropped );
				}
				else
				{
					this.SendLocalizedMessageTo(from,1042971,"Stackable items must be donated in quantities greater then 100.");
					return false;
				}
			}
			
			this.SendLocalizedMessageTo(from,1042971,"That cannot be donated.  Please donate things like ingots, wood, and weapons.");
			return false;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version


			writer.Write( m_AutoLock );

			if ( !Locked && m_AutoLock )
				writer.WriteDeltaTime( m_RelockTimer.RelockTime );


		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();


			m_AutoLock = reader.ReadBool();

			if ( !Locked && m_AutoLock )
				m_RelockTimer = new InternalTimer( this, reader.ReadDeltaTime() - DateTime.Now );



		}

		public void IncreaseTag( Mobile player, int amount )
		{
			Account acct = player.Account as Account;

			if ( acct == null )
				return;
			
			
			string tag = acct.GetTag( "numDonations" );

			int cur;

			if ( tag == null || tag == "" )
				cur = 0;
			else
				cur = Utility.ToInt32( tag );

			acct.SetTag( "numDonations", (cur + 1).ToString() );

		}

		public bool CheckTag( Mobile player )
		{

			Account acct = player.Account as Account;

			if ( acct == null )
				return false;
			
			//acct.SetTag( "numRewardsChosen", (cur + 1).ToString() );
			string tag = acct.GetTag( "numDonations" );

			int cur;

			if ( tag == null || tag == "" )
				cur = 0;
			else
				cur = Utility.ToInt32( tag );

			if( cur < MaxDonations )
				return true;
			return false;
		}

		public bool CanGet( Mobile player )
		{
			TimeSpan JoinGameAge = TimeSpan.FromDays( 2.0 );

			PlayerMobile pm = (PlayerMobile)player;

			//player.SendMessage("GameTime: {0}",pm.GameTime);
			//player.SendMessage("TotalSkills: {0}",pm.SkillsTotal);

			//online time
			if ( pm.GameTime > JoinGameAge )
				return false;
			//skills
			if ( pm.SkillsTotal > 3500 )
				return false;

			return true;
		}	

		public bool ValidItem( Item item )
		{
			if( item is BaseWeapon   ||
				item is BaseArmor    ||
				item is Gold         ||
				item is BaseIngot    ||
				item is BaseBoard    ||
				item is BasePiece    ||
				item is Log          ||
				item is Board        ||
				item is Feather      ||
				item is Lockpick     ||
				item is Cloth        ||
				item is BaseLeather  ||
				item is BaseTool     ||
				item is Food         ||
				item is BaseBeverage ||
				item is Bolt         ||
				item is Arrow        ||
				item is Bone         ||
				item is BaseReagent  ||
				item is Bottle       ||
				item is Bag          ||
				item is BankCheck    ||
				item is BaseJewel    ||
				item is Spellbook    ||
				item is SpellScroll  ||
				item is BaseOre      ||
				item is BaseScales   ||
				item is BaseClothing ||
				item is BaseAddonDeed ||
				item is BaseBook     ||
				item is BaseRanged   ||
				item is BasePotion   ||
				item is Bandage      ||
				item is BaseWand     ||
				item is MonsterStatuette  ||
				item is PowerScroll  ||
				item is StatCapScroll  ||
				item is BagOfSending  ||
				item is BallOfSummoning  ||
				item is BraceletOfBinding  ||
				item is PowderOfTranslocation  ||
				item is ZoogiFungus  ||
				item is BlueSnowflake ||
				item is BaseContainer ||
				item is RedPoinsettia  ||
				item is Snowman  ||
				item is GiftBox  ||
				item is WreathAddon  ||
				item is ColoredAnvil  ||
				item is BaseGlovesOfMining ||
				item is PowderOfTemperament  ||
				item is BaseShield   ||
				item is BaseWeapon   )
				return true;

			return false;
		}

		public bool ValidStack( Item item )
		{
/*			
			if( item.Stackable )
			{
				if( item.Amount >= 100 )
					return true;
				else
					return false;

			}
*/
			return true;
		}

	}

	public class DonationBoxGump : Gump 
	{
		private Mobile m_from;
		private int m_max;

		public DonationBoxGump( Mobile from, int max ) : base(50, 50) 
		{
			m_from = from;
			m_max = max;
			this.InitializeGump();
		}
		public override void OnResponse(NetState sender, RelayInfo info) 
		{
		}
		public virtual void InitializeGump() 
		{
			this.Closable = true;
			this.Disposable = true;
			this.Dragable = true;
			// Initializing Page
			this.AddPage(0);
			// Initializing Background
			this.AddBackground(10, 10, 270, 216, 5054);
			// Initializing Label
			this.AddLabel(90, 19, 255, "The Donation Box");
			// Initializing Background
			this.AddBackground(16, 39, 255, 183, 83);
			// Initializing Image
			this.AddImage(250, 20, 216);
			// Initializing Image
			this.AddImage(25, 20, 216);
			// Initializing AlphaRegion
			this.AddAlphaRegion(25, 50, 240, 162);
	
			// Initializing Html
			string temp = "";
			
			if( m_from.AccessLevel == AccessLevel.Player )
				temp = "You have opened the donation box " + GetNumberofDonations( m_from )  + " of " + m_max.ToString() + " times.<br><br>";
			else
				temp = "Due to your enlightened state, you can open the box without limit.<br><br>";

			temp += "The idea behind the Donation Box is simple:  veteran players can place items in the box to help out new arrivals to the realm.<br><br>";
			temp += "However, the items in this box are meant to help new comers along, not sustain their existance.  As such, there is a limit to the number of times a new comer may use a Donation Box.<Br><Br>";
			temp += "Remember, greed is not a virtue.<br><br>";
			temp += "Finally, if the items in this box are a help to you, show your gratitude by donating an item or two when ever you can spare it.";

			this.AddHtml(28, 53, 230, 155, temp, false, true);
		}

		public int GetNumberofDonations( Mobile player )
		{
			Account acct = player.Account as Account;

			if ( acct == null )
				return 0;
			
			//acct.SetTag( "numRewardsChosen", (cur + 1).ToString() );
			string tag = acct.GetTag( "numDonations" );

			int cur;

			if ( tag == null || tag == "" )
				cur = 0;
			else
				cur = Utility.ToInt32( tag );

			return cur;
		}
	}
}
