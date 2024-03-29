using System;
using Server;

namespace Server.Items
{
	public class LesserManaPotion : BaseManaPotion
	{
		public override int MinMana { get { return (Core.AOS ? 6 : 3); } }
		public override int MaxMana { get { return (Core.AOS ? 8 : 10); } }
		public override double Delay{ get{ return (Core.AOS ? 10.0 : 10.0); } }

		[Constructable]
		public LesserManaPotion() : base( PotionEffect.ManaLesser )
		{
			Hue = 98;
			Name = "lesser mana potion - 10 sec cool down";
		}

		public LesserManaPotion( Serial serial ) : base( serial )
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