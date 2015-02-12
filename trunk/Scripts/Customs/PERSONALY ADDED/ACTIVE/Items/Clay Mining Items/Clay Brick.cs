using System;
using Server.Items;

namespace Server.Items
{
	public class ClayBrick : Item, ICommodity
	{
		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }
		
		[Constructable]
		public ClayBrick() : this(1)
		{
		}
		
		[Constructable]
        public ClayBrick(int amount)
        	: base(0x1BE6)
        {
        	Stackable = true;
        	Hue = 2967;
        	Amount = amount;
        	Name = "A Clay Brick";
        	Weight = 1.5;
        }
        
        public ClayBrick(Serial serial) : base(serial)
        {
        }
        
        public override void AddNameProperty( ObjectPropertyList list )
        {
        	if ( Amount > 1)
        	{
        		list.Add( Amount+ " Clay Bricks" );
        	}
        	else
        	{
        		list.Add( "A Clay Brick" );
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