﻿using System;
using Server.Items;

namespace Server.Items
{
	public class GammaCrystal : Item, ICommodity
	{
		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }
		
		[Constructable]
		public GammaCrystal() : this(1)
		{
		}

        [Constructable]
        public GammaCrystal(int amount)
        	: base(0x1779)
        {
        	Stackable = true;
            Amount = amount;
            Hue = 75;
           	Name = "A Gamma Crystal";
        Weight = 32.0;
        }
		public GammaCrystal(Serial serial) : base(serial)
		{
		}
		
		public override void AddNameProperty( ObjectPropertyList list )
		{
			if ( Amount > 1)
			{
				list.Add( Amount+ " Gamma Crystals" );
			}
			else
			{				
				list.Add( "A Gamma Crystal" );
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