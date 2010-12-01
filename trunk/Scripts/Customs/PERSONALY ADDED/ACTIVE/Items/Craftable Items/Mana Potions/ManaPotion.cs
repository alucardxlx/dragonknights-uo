using System;
using Server;

namespace Server.Items
{
	public class ManaPotion : BaseManaPotion
	{
		public override int MinMana { get { return (Core.AOS ? 13 : 6); } }
		public override int MaxMana { get { return (Core.AOS ? 16 : 20); } }
		public override double Delay{ get{ return (Core.AOS ? 30.0 : 30.0); } }

		[Constructable]
		public ManaPotion() : base( PotionEffect.Mana )
		{
			Hue = 93;
			Name = "mana potion - 30 sec cool down";
		}

		public ManaPotion( Serial serial ) : base( serial )
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