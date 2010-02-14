/* Created by Hammerhand*/

using System;
using Server.Misc;

namespace Server.Items
{

	public class CloakOfTheValkyrie : Cloak 
	{
        public override int BaseFireResistance { get { return 20; } }
        public override int ArtifactRarity { get { return 67; } }
        public override bool AllowMaleWearer { get { return false; } }

		
		[Constructable] 
		public CloakOfTheValkyrie()  
		{
            Name = "Cloak Of The Valkyrie";
            Hue = 1153;
            Weight = 2.0;

            Attributes.RegenStam = Utility.RandomMinMax(4, 9);
            Attributes.RegenHits = Utility.RandomMinMax(3, 10);
            Attributes.RegenMana = Utility.RandomMinMax(5, 8);

		}

		public override void OnSingleClick( Mobile from ) 
		{ 
			this.LabelTo( from, Name ); 
		}

        public CloakOfTheValkyrie(Serial serial)
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
