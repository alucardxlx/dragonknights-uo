using System;
using Server;
using Server.Multis;
using Server.Gumps;
using Server.Items;
using Server.Network;

namespace Server.Items
{
        public enum IncredibleHulkStatuetteType
	{
		IncredibleHulk
	}
        public class IncredibleHulkStatuetteInfo
	{
		private int[] m_Sounds;

		public int[] Sounds{ get{ return m_Sounds; } }

		public IncredibleHulkStatuetteInfo( int baseSoundID )
		{
			m_Sounds = new int[]{ baseSoundID, baseSoundID + 1, baseSoundID + 2, baseSoundID + 3, baseSoundID + 4 };
		}

		private static IncredibleHulkStatuetteInfo[] m_Table = new IncredibleHulkStatuetteInfo[]
			{
				/* IncredibleHulkStatuette */				new IncredibleHulkStatuetteInfo( 0x16a )//was 427
			};
        public static IncredibleHulkStatuetteInfo GetInfo( IncredibleHulkStatuetteType type )
		{
			int v = (int)type;

			if ( v < 0 || v >= m_Table.Length )
				v = 0;

			return m_Table[v];
		}
	}	
	public class IncredibleHulkStatuette : Item
	{
                private IncredibleHulkStatuetteType m_Type;
		private bool m_TurnedOn;

		[CommandProperty( AccessLevel.GameMaster )]
		public bool TurnedOn
		{
			get{ return m_TurnedOn; }
			set{ m_TurnedOn = value; InvalidateProperties(); }
		}

                [CommandProperty( AccessLevel.GameMaster )]
		public IncredibleHulkStatuetteType Type
		{
			get{ return m_Type; }
			set
			{
				m_Type = value;
				
				InvalidateProperties();
			}
		}

		[Constructable]
		public IncredibleHulkStatuette() : base( 0x20DF )
		{
                Hue = 2212;
	        Name = "Incredible Hulk Statuette";
                Weight = 1.0;
	        //LootType = LootType.Blessed;
                
		}

		public override bool HandlesOnMovement{ get{ return m_TurnedOn && IsLockedDown; } }

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( m_TurnedOn && IsLockedDown && Utility.InRange( m.Location, this.Location, 1 ) && !Utility.InRange( oldLocation, this.Location, 1 ) )
			{

				int[] sounds = IncredibleHulkStatuetteInfo.GetInfo( m_Type ).Sounds;

                                Effects.PlaySound( this.Location, this.Map, sounds[Utility.Random( sounds.Length )] );
                                this.PublicOverheadMessage( MessageType.Regular, 0x1150, true, "Hulk Smash!"); 
			}

			base.OnMovement( m, oldLocation );
		}

		public IncredibleHulkStatuette( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_TurnedOn )
				list.Add( 502695 ); // turned on
			else
				list.Add( 502696 ); // turned off
		}

		public bool IsOwner( Mobile mob )
		{
			BaseHouse house = BaseHouse.FindHouseAt( this );

			return ( house != null && house.IsOwner( mob ) );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsOwner( from ) )
			{
				OnOffGump onOffGump = new OnOffGump( this );
				from.SendGump( onOffGump );
			}
			else
			{
				from.SendLocalizedMessage( 502691 ); // You must be the owner to use this.
			}
		}

		private class OnOffGump : Gump
		{
			private IncredibleHulkStatuette m_Statuette;

			public OnOffGump( IncredibleHulkStatuette statuette ) : base( 150, 200 )
			{
				m_Statuette = statuette;

				AddBackground( 0, 0, 300, 150, 0xA28 );

				AddHtmlLocalized( 45, 20, 300, 35, statuette.TurnedOn ? 1011035 : 1011034, false, false ); // [De]Activate this item

				AddButton( 40, 53, 0xFA5, 0xFA7, 1, GumpButtonType.Reply, 0 );
				AddHtmlLocalized( 80, 55, 65, 35, 1011036, false, false ); // OKAY

				AddButton( 150, 53, 0xFA5, 0xFA7, 0, GumpButtonType.Reply, 0 );
				AddHtmlLocalized( 190, 55, 100, 35, 1011012, false, false ); // CANCEL
			}

			public override void OnResponse( NetState sender, RelayInfo info )
			{
				Mobile from = sender.Mobile;

				if ( info.ButtonID == 1 )
				{
					bool newValue = !m_Statuette.TurnedOn;
					m_Statuette.TurnedOn = newValue;

					if ( newValue && !m_Statuette.IsLockedDown )
						from.SendLocalizedMessage( 502693 ); // Remember, this only works when locked down.
				}
				else
				{
					from.SendLocalizedMessage( 502694 ); // Cancelled action.
				}
			}
		}
             
                

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
                        writer.WriteEncodedInt( (int) m_Type );
			writer.Write( (int) 0 ); // version
			writer.Write( (bool) m_TurnedOn );
                        
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
                        
			switch ( version )
			{
				case 0:
				{
                                        m_Type = (IncredibleHulkStatuetteType)reader.ReadEncodedInt();
					m_TurnedOn = reader.ReadBool();
					break;
				}
			}
		}
	}
 }
