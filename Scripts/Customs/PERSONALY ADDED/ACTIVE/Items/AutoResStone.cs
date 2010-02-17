using System;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using System.Collections;
using Server.ContextMenus;

namespace Server.Items
{
   public class AutoResStone : Item
   {

	  private Mobile m_Owner;

      [Constructable]
      public AutoResStone() : base( 7964 )
      {
         Movable = false;
         Name = "Auto Res Stone";
	   Hue = Utility.RandomList( 2970 );
         LootType = LootType.Blessed;
      }

		public override DeathMoveResult OnInventoryDeath(Mobile parent)
		{
			if ( parent == m_Owner )
			{
				new AutoResTimer( parent ).Start();
			}
			return base.OnInventoryDeath (parent);
		}

		private class AutoResTimer : Timer
		{
			private Mobile m_Mobile;
			public AutoResTimer( Mobile mob ) : base( TimeSpan.FromSeconds( 5.0 ) )
			{
				m_Mobile = mob;
			}

			protected override void OnTick()
			{
				m_Mobile.Resurrect();
				m_Mobile.SendMessage( "¤Res¤ you should be more careful in the future." );
				
				new BlessedTimer( m_Mobile ).Start();
				m_Mobile.SendMessage( "¤Res¤ You will be blessed for 60 seconds." );

				m_Mobile.Blessed = true;
				Stop();
			}
            }

		private class BlessedTimer : Timer
		{
			private Mobile m_Mobile;
			public int cnt;

			public BlessedTimer( Mobile mob ) : base( TimeSpan.FromSeconds( 15.0 ), TimeSpan.FromSeconds( 15.0 ) )
			{
				m_Mobile = mob;
				cnt = 60;
			}

			protected override void OnTick()
			{
				if( cnt > 0 )
				{
					cnt -= 15;
					m_Mobile.SendMessage( "You will be blessed for {0} more seconds.", cnt );
				}
				if( cnt == 0 )
				{
					m_Mobile.SendMessage( "You are no longer blessed." );
					m_Mobile.Blessed = false;
					this.Stop();
                		}
				if( cnt < 0 )
				{
					cnt = 0;
					m_Mobile.SendMessage( "You are no longer blessed." );
					m_Mobile.Blessed = false;
					this.Stop();
				}
			}
            }

		public override void OnDoubleClick(Mobile from)
		{
			// set owner if not already set -- this is only done the first time.
			if ( m_Owner == null )
			{
				m_Owner = from;
				this.Name = m_Owner.Name.ToString() + "'s Auto Res Stone";
				from.SendMessage( "This orb has been assigned to you." );
			}
			else
			{
				if ( m_Owner != from )
				{
					from.SendMessage( "This is not yours to use." );
					return;
				}
			}
		}

      public AutoResStone( Serial serial ) : base( serial )
      {
      }

      public override void Serialize( GenericWriter writer )
      {
         base.Serialize( writer );

         writer.Write( (int) 0 ); // version
      }

      public override void Deserialize( GenericReader reader )
      {
         base.Deserialize( reader );

         int version = reader.ReadInt();
      }
   }
}
