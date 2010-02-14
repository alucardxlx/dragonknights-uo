/*Created by Hammerhand*/

using System;
using Server;

namespace Server.Items
{
    [FlipableAttribute(0xF61, 0xF60)]
    public class BladeOfTheValkyrie : BaseSword
	{
        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.ArmorIgnore; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.ConcussionBlow; } }

        public override int ArtifactRarity { get { return 60; } }

        public override int AosStrengthReq { get { return 35; } }
		public override int AosMinDamage{ get{ return 25; } }		
		public override int AosMaxDamage{ get{ return 50; } }

        #region Mondain's Legacy
        public override float MlSpeed { get { return 3.50f; } }
        #endregion

		public override int InitMinHits{ get{ return 100; } }
		public override int InitMaxHits{ get{ return 150; } }

        public override bool CanEquip(Mobile from)
        {
            if (from.Female == false)
            {
                from.SendMessage("Only females may use this.");
                return false;
            }
            return base.CanEquip(from);
        }

		[Constructable]
        public BladeOfTheValkyrie()
            : base(0xF61)
		{
            Name = "Blade Of The Valkyrie";
            Weight = 6.0;                       
			Hue = 1153;

            Attributes.AttackChance = Utility.RandomMinMax(8, 25);
            Attributes.DefendChance = Utility.RandomMinMax(9, 15);
            Attributes.WeaponDamage = Utility.RandomMinMax(20, 30);
            Attributes.BonusStr = Utility.RandomMinMax(7, 15);
            Attributes.WeaponSpeed = Utility.RandomMinMax(17, 30);
			Attributes.SpellChanneling = 1;
            Attributes.Luck = Utility.RandomMinMax(100, 250);
            Attributes.RegenHits = Utility.RandomMinMax(4, 15);
            Attributes.RegenStam = Utility.RandomMinMax(5, 12);
            WeaponAttributes.HitLightning = Utility.RandomMinMax(10, 30);
			WeaponAttributes.UseBestSkill = 1;
            WeaponAttributes.HitLeechHits = Utility.RandomMinMax(9, 25);

		}

        public BladeOfTheValkyrie(Serial serial)
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

			if ( Hue == 0x44F )
				Hue = 0x76D;
		}
	}
}
