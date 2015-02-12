using System;
using Server.Items;

namespace Server.Items
{
	public class GammaCrystalDust : Item, ICommodity
	{
		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }
		
		[Constructable]
		public GammaCrystalDust() : this(1)
		{
		}

        [Constructable]
        public GammaCrystalDust(int amount)
        	: base(0x26b8)
        {
        	Stackable = true;
            Amount = amount;
            Hue = 75;
           	Name = "A Gamma Dust";
        Weight = 0.5;
        }
		public GammaCrystalDust(Serial serial) : base(serial)
		{
		}
		
		public override void AddNameProperty( ObjectPropertyList list )
		{
			if ( Amount > 1)
			{
				list.Add( Amount+ " Gamma Dust" );
			}
			else
			{				
				list.Add( "A Gamma Dust" );
			}
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}