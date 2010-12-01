using System;
using Server;

namespace Server.Items
{
	public class GreaterManaPotion : BaseManaPotion
	{
		public override int MinMana { get { return (Core.AOS ? 20 : 9); } }
		public override int MaxMana { get { return (Core.AOS ? 25 : 30); } }
		public override double Delay{ get{ return 60.0; } }

		[Constructable]
		public GreaterManaPotion() : base( PotionEffect.ManaGreater )
		{
			Hue = 88;
			Name = "greater mana potion - 1 min cool down";
		}

		public GreaterManaPotion( Serial serial ) : base( serial )
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