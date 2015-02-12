using System;
using Server.Items;

namespace Server.Items
{
	public class ClayBlock : Item, ICommodity
	{
		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }
		
		[Constructable]
		public ClayBlock() : this(1)
		{
		}
		
		[Constructable]
		public ClayBlock(int amount)
			: base(0x1767)
		{
			Stackable = true;
			Hue = 2967;
			Amount = amount;
			Name = "A Clay Block";
			Weight = 8.0;
		}
		
		public ClayBlock(Serial serial) : base(serial)
		{
		}
		
		public override void AddNameProperty( ObjectPropertyList list )
		{
			if ( Amount > 1)
			{
				list.Add( Amount+ " Clay Blocks" );
			}
			else
			{
				list.Add( "A Clay Block" );
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