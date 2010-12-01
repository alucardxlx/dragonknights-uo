using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public abstract class BaseManaPotion : BasePotion
	{
		public abstract int MinMana { get; }
		public abstract int MaxMana { get; }
		public abstract double Delay { get; }

		public BaseManaPotion( PotionEffect effect ) : base( 0xF0C, effect )
		{
		}

		public BaseManaPotion( Serial serial ) : base( serial )
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

		public void DoMana( Mobile from )
		{
			int min = Scale( from, MinMana );
			int max = Scale( from, MaxMana );

			from.Mana += ( Utility.RandomMinMax( min, max ) );
		}

		public override void Drink( Mobile from )
		{
			if ( from.Mana < from.ManaMax )
			{
				if ( from.Poisoned || MortalStrike.IsWounded( from ) )
				{
					from.SendMessage( "You cannot use this in your current state." );
				}
				else
				{
					if ( from.BeginAction( typeof( BaseManaPotion ) ) )
					{
						DoMana( from );

						BasePotion.PlayDrinkEffect( from );

						this.Consume();

						Timer.DelayCall( TimeSpan.FromSeconds( Delay ), new TimerStateCallback( ReleaseManaLock ), from );
					}
					else
					{
						from.SendMessage( "You must wait to use another mana potion." );
					}
				}
			}
			else
			{
				from.SendMessage( "You are already at full mana." ); 
			}
		}

		private static void ReleaseManaLock( object state )
		{
			((Mobile)state).EndAction( typeof( BaseManaPotion ) );
		}
	}
}