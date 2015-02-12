using System;
using Server.Items;

namespace Server.Items
{
	public class GlassBrick : Item, ICommodity
	{
		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }
		
		[Constructable]
		public GlassBrick() : this(1)
		{
		}

        [Constructable]
        public GlassBrick(int amount)
        	: base(0x1BF8)
        {
        	Stackable = true;
            Amount = amount;
            Hue = 2067;
           	Name = "A Glass Brick";
        Weight = 1.5;
        }
		public GlassBrick(Serial serial) : base(serial)
		{
		}
		
		public override void AddNameProperty( ObjectPropertyList list )
		{
			if ( Amount > 1)
			{
				list.Add( Amount+ " Glass Bricks" );
			}
			else
			{				
				list.Add( "A Glass Brick" );
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
