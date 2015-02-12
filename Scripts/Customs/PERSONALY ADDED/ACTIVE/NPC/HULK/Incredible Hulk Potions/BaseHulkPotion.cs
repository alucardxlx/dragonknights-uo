using System;
using Server;
using Server.Mobiles;
using System.Collections;


namespace Server.Items
{
    public abstract class BaseHulkPotion : BasePotion
    {
        public abstract int HueModOffset{ get; }
        public abstract int StrOffset{ get; }
        public abstract TimeSpan Duration{ get; }
        
        public BaseHulkPotion( PotionEffect effect ) : base( 0xF0A, effect )
        {
        }

        public BaseHulkPotion( Serial serial ) : base( serial )
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

        public bool DoStrength( Mobile from )
        {
            // TODO: Verify scaled; is it offset, duration, or both?
            if ( Spells.SpellHelper.AddStatOffset( from, StatType.Str, Scale( from, StrOffset ), Duration ) )
            {
                from.FixedEffect( 0x375A, 10, 15 );
                from.PlaySound( 0x1E7 );
                return true;
            }
           	from.SendMessage( 38,"You are already under a similar effect." );
//            from.SendLocalizedMessage( 502173 ); // You are already under a similar effect.
            return false;
        }

        public override void Drink( Mobile from )
        {
            if ( DoStrength( from ) )
            {
				TimeSpan duration = Duration;
            	from.HueMod = HueModOffset;
            	from.SendMessage( 71,"You feel the effects from the \"Gamma Radiation Potion\" !" );
            	
            	RemoveTimer( from );
            	Timer t = new InternalTimer( from, duration );
				m_Table[from] = t;
				t.Start();

                BasePotion.PlayDrinkEffect( from );
                this.Consume();
            }
        }
        
		private static Hashtable m_Table = new Hashtable();
		
		public static bool HasTimer( Mobile m )
		{
			return m_Table[m] != null;
		}

		public static void RemoveTimer( Mobile m )
		{
			Timer t = (Timer)m_Table[m];

			if ( t != null )
			{
				t.Stop();
				m_Table.Remove( m );
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_Mobile;

			public InternalTimer( Mobile m, TimeSpan duration ) : base( duration )
			{
				Priority = TimerPriority.OneSecond;
				m_Mobile = m;
			}

			protected override void OnTick()
			{
				m_Mobile.RevealingAction();
				RemoveTimer( m_Mobile );
				m_Mobile.SendMessage( 38,"You lose the effect of the \"Gamma Radiation Potion\"." );
				m_Mobile.HueMod = -1;
			}
		}
		
    }
}
