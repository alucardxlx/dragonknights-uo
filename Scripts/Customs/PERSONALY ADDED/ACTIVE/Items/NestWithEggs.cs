using System;
using Server;
using Server.Items;
using Server.Network;


namespace Server.Items
{
	public class Nest : Item
	{
		
		private DateTime m_NextEggsTime;

		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime NextEggsTime
		{
			get
            { 
                return m_NextEggsTime; 
            }
			set
            { 
                m_NextEggsTime = value; 
                ItemID = ( DateTime.Now >= m_NextEggsTime ) ? 0x1AD4 : 0x1AD5; 
            }
		}

        public override void OnDoubleClick( Mobile from )
        {
            if( DateTime.Now < m_NextEggsTime )
            {
                from.SendMessage( "There are no eggs in this nest." );
                return;
            }

            from.SendMessage( "You found an egg!" );
            from.AddToBackpack( new Eggs() );

            NextEggsTime = DateTime.Now + TimeSpan.FromHours( 1.0 );
        }

		[Constructable]
		public Nest() : base( 0x1AD4 )
		{
			Weight = 1.0;
			Movable = true;


		}

		public Nest( Serial serial ) : base( serial )
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
