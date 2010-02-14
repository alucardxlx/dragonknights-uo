
using System;
using Server;
using Server.Items;
using Server.Mobiles;
using System.Collections;
using Server.Network;


namespace Server.Items
{

	public class DraculasDeadOnlyTeleportalOut : Item
	{
		private Point3D m_DestLoc;
		private Map     m_DestMap;
		private bool    m_AllowCreatures;
		private bool    m_TelePets;

		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D TheLocation
		{
			get { return m_DestLoc; }
			set { m_DestLoc = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Map TheMap
		{
			get { return m_DestMap; }
			set { m_DestMap = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool AllowCreatures
		{
			get { return m_AllowCreatures; }
			set { m_AllowCreatures = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool TelePets
		{
			get { return m_TelePets; }
			set { m_TelePets = value; InvalidateProperties(); }
		}

		[Constructable]
		public DraculasDeadOnlyTeleportalOut() : base( 3948 )
		{
			Visible = true;
			Hue = 1;
			Movable = false;
			Weight = 0.0;
			Name = "A lost souls exit..";
		}

		public DraculasDeadOnlyTeleportalOut( Serial serial ) : base( serial )
		{
		}

		public override bool OnMoveOver( Mobile m )
		{
			if( !m_AllowCreatures && !m.Player )
				return true;
			
			if( m.Alive)
					{
					m.SendMessage( "You must be dead to use this portal." );
					m.Say("Duh... I am not dead... Hello? Use a Rune book..");
					m.PlaySound( 0x440 );
					return true;
					}
			if( m_TelePets)
				{
				Server.Mobiles.BaseCreature.TeleportPets( m, m_DestLoc, m_DestMap );
				}
				
				m.Say("Have mercy on my soul.");
				World.Broadcast( 32, true, "A Darkening chime is heard throughout the land as Dracula has consumed another victim. May {0} rest in peace.", m.Name );
				m.PlaySound(0x1F7);

				m.MoveToWorld( m_DestLoc, m_DestMap );

				return false;
				
			

		}

		
	

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version

			writer.Write( m_DestLoc );
			writer.Write( m_DestMap );
			writer.Write( m_AllowCreatures );
			writer.Write( m_TelePets );
		}


		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			m_DestLoc = reader.ReadPoint3D();
			m_DestMap = reader.ReadMap();
			m_AllowCreatures = reader.ReadBool();
			m_TelePets = reader.ReadBool();
		}
	}
}
