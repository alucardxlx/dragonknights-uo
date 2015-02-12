using System;
using Server.Items;

namespace Server.Items
{
	public class GammaCrystalFragment : Item, ICommodity
	{
		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }
		
		[Constructable]
		public GammaCrystalFragment() : this(1)
		{
		}

        [Constructable]
        public GammaCrystalFragment(int amount)
        	: base(0x1059)
        {
        	Stackable = true;
            Amount = amount;
            Hue = 75;
           	Name = "A Gamma Crystal Fragment";
           	Weight = 1.5;
        }
		public GammaCrystalFragment(Serial serial) : base(serial)
		{
		}
		
		public override void AddNameProperty( ObjectPropertyList list )
		{
			if ( Amount > 1)
			{
				list.Add( Amount+ " Gamma Crystal Fragments" );
			}
			else
			{				
				list.Add( "A Gamma Crystal Fragment" );
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