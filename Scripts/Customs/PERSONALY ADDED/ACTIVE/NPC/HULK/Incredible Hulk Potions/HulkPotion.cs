using System;
using Server;

namespace Server.Items
{
	public class HulkPotion : BaseHulkPotion
	{
		public override int HueModOffset { get{ return 72; } }//72 good for body hue, 71 green, 38 red for words
		public override int StrOffset{ get{ return 100; } }
		public override TimeSpan Duration{ get{ return TimeSpan.FromMinutes( 60.0 ); } }

		[Constructable]
		public HulkPotion() : base( PotionEffect.Hulk )
		{
			Name = "Gamma Radiation Potion";
			Hue = HueModOffset;
			Stackable = true;
		}

		public HulkPotion( Serial serial ) : base( serial )
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
