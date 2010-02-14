/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////Blood Staff///////////////////////////////////////////////////////////////////
////////////////////////////////////////////By Hlal @ GD13 CO-OP/////////////////////////////////////////////////////////////
////////////////////////////////////////////////05*21*05/////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using Server.Mobiles;

namespace Server.Items
{
	public class FoolsBankCheck : Item
	{
		[Constructable]
		public FoolsBankCheck() : this( 1 )
        {
		}
                    private int m_Worth;

        [CommandProperty(AccessLevel.GameMaster)]
        public int Worth
        {
            get { return m_Worth; }
            set { m_Worth = value; InvalidateProperties(); }
		}
        public FoolsBankCheck(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

            writer.Write((int)m_Worth);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            LootType = LootType.Blessed;

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        m_Worth = reader.ReadInt();
                        break;
                    }
            }
        }

		[Constructable]
        public FoolsBankCheck(int worth): base(0x14F0)
		{
            Name = "A Bank Check";
            Hue = 0x34;
			Movable = false;
            LootType = LootType.Blessed;
            m_Worth = worth;
            Visible = false;
		}

        public override bool DisplayLootType { get { return Core.AOS; } }
        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            list.Add(1060738, m_Worth.ToString()); // value: ~1_val~

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
                From.PlaySound(From.Female ? 799 : 1071);
				From.Say( "*huh?*" );
				From.Damage( Utility.Random( 20, 55 ) );
				return false;
			} 
			From.SendMessage("You are dead and can not take that!");
			return false;
		}


		}
	}
