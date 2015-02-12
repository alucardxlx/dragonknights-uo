using System;
using Server.Items;

namespace Server.Items
{
	public class ClayMud : Item, ICommodity
	{
		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }
		
		[Constructable]
		public ClayMud() : this(1)
		{
		}
		
		[Constructable]
		public ClayMud(int amount)
			: base(0xdf8)//0xdf8
		{
			Stackable = true;
			Hue = 2967;
			Amount = amount;
			Name = "A Clay Mud";
			Weight = 4.0;
		}
		
		public ClayMud(Serial serial) : base(serial)
		{
		}
		
		public override void AddNameProperty( ObjectPropertyList list )
		{
			if ( Amount > 1)
			{
				list.Add( Amount+ " Clay Muds" );
			}
			else
			{
				list.Add( "A Clay Mud" );
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