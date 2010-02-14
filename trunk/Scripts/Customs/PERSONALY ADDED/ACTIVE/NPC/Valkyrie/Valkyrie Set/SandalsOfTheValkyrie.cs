/* Created by Hammerhand */

using System;
using Server.Misc;

namespace Server.Items
{

	public class SandalsOfTheValkyrie : Sandals
	{

        public override int ArtifactRarity { get { return 63; } }
        public override bool AllowMaleWearer { get { return false; } }

		[Constructable] 
		public SandalsOfTheValkyrie() : base( 0x170d ) 
		{
            Name = "Sandals Of The Valkyrie";
			Hue = 1153;
            Weight = 1.0;

            Attributes.BonusDex = Utility.RandomMinMax(1, 5);
            Attributes.BonusStam = Utility.RandomMinMax(1, 7);
			
		}

        public SandalsOfTheValkyrie(Serial serial)
            : base(serial) 
		{ 

		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 
			writer.Write( (int) 0 ); 
		} 

		public override void Deserialize(GenericReader reader) 
		{ 
			base.Deserialize( reader ); 
			int version = reader.ReadInt(); 
		} 
	} 
} 
