  /////////////////////////////
 //////  LostSinner  /////////
/////////////////////////////

using System;
namespace Server.Items
{
	public class VampEarrings : BaseEarrings
	{
		

		[Constructable]
		public VampEarrings() : base( 0x1F07 )
		{
			Name = "Draculas Aegris";
			Hue = 601;
			
			Attributes.RegenStam = 3;
			Attributes.RegenHits = 2;
			
			Attributes.NightSight = 1;

            SkillBonuses.SetValues(0, SkillName.Necromancy, 1.0);

		}

		public VampEarrings( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
