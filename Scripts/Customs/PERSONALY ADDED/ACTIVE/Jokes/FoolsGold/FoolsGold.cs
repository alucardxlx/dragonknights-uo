/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////Blood Staff///////////////////////////////////////////////////////////////////
////////////////////////////////////////////By Hlal @ GD13 CO-OP/////////////////////////////////////////////////////////////
////////////////////////////////////////////////05*21*05/////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using Server.Mobiles;

namespace Server.Items
{
	public class FoolsGold : Item
	{
		[Constructable]
		public FoolsGold() : this( 1 )
		{
		}

		[Constructable]
		public FoolsGold( int amountFrom, int amountTo ) : this( Utility.Random( amountFrom, amountTo - amountFrom ) )
		{
		}

		[Constructable]
		public FoolsGold( int amount ) : base( 0xEED )
		{
            Name = "Gold Coins";
			Movable = false;
			Stackable = true;
			Amount = amount;
            Visible = false;
		}

		public FoolsGold( Serial serial ) : base( serial )
		{
		}

		protected override void OnAmountChange( int oldValue )
		{
            int newValue = this.Amount;

            UpdateTotal(this, TotalType.Gold, newValue - oldValue);
		}

        public override int GetTotal(TotalType type)
        {
            int baseTotal = base.GetTotal(type);

            if (type == TotalType.Gold)
                baseTotal += this.Amount;

            return baseTotal;
		}

		public override bool VerifyMove( Mobile from )
		{
			PlayerMobile From = from as PlayerMobile;
			
			if ( From.Alive == true )
			{ 
				From.BoltEffect( 0 );
				From.SendMessage("You feel a jolt of electricity!");
				From.PlaySound( From.Female ? 799 : 1071 );
				From.Say( "*huh?*" );
				From.Damage( Utility.Random( 20, 55 ) );
				return false;
			} 
			From.SendMessage("You are dead and can not take that!");
			return false;
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
