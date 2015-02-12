using System;
using Server.Items;

namespace Server.Items
{
	public class GlassDust : Item, ICommodity
	{
		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }
		
		[Constructable]
		public GlassDust() : this(1)
		{
		}

        [Constructable]
        public GlassDust(int amount)
        	: base(0x26b8)
        {
        	Stackable = true;
            Amount = amount;
            Hue = 2067;
           	Name = "A Glass Dust";
        Weight = 0.5;
        }
		public GlassDust(Serial serial) : base(serial)
		{
		}
		
		public override void AddNameProperty( ObjectPropertyList list )
		{
			if ( Amount > 1)
			{
				list.Add( Amount+ " Glass Dust" );
			}
			else
			{				
				list.Add( "A Glass Dust" );
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