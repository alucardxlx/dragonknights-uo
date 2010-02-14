/*Created by Hammerhand*/

using System;
using Server;

namespace Server.Items
{
	public class ShieldOfTheValkyrie : BaseShield
	{
        public override int ArtifactRarity { get { return 62; } }
		public override int BasePhysicalResistance{ get{ return 10; } }
		public override int BaseFireResistance{ get{ return 10; } }
		public override int BaseColdResistance{ get{ return 10; } }
		public override int BasePoisonResistance{ get{ return 10; } }
		public override int BaseEnergyResistance{ get{ return 10; } }

		public override int InitMinHits{ get{ return 150; } }
		public override int InitMaxHits{ get{ return 400; } }

		public override int AosStrReq{ get{ return 0; } }

		public override int ArmorBase{ get{ return 400; } }

        public override bool AllowMaleWearer { get { return false; } }

		[Constructable]
        public ShieldOfTheValkyrie()
            : base(0x2B01)
		{
            Name = "Shield Of The Valkyrie";
		    Hue = 1153;
            Weight = 5.0;

            Attributes.CastRecovery = Utility.RandomMinMax(3, 7);
            Attributes.CastSpeed = Utility.RandomMinMax(2, 10);
            Attributes.SpellDamage = Utility.RandomMinMax(10, 35);
            Attributes.Luck = Utility.RandomMinMax(100, 250);
            Attributes.BonusStr = Utility.RandomMinMax(2, 10);
            Attributes.WeaponSpeed = Utility.RandomMinMax(5, 35);
            Attributes.SpellChanneling = 1;
            Attributes.ReflectPhysical = Utility.RandomMinMax(10, 25);

            PhysicalBonus = Utility.RandomMinMax(1, 10);
            ColdBonus = Utility.RandomMinMax(1, 10);
            EnergyBonus = Utility.RandomMinMax(1, 10);
		}

        public ShieldOfTheValkyrie(Serial serial)
            : base(serial)
		{
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );//version
		}
	}
}
