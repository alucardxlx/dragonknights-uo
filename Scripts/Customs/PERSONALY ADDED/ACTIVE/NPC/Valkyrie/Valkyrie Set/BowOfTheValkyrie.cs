/*Created by Hammerhand*/

using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
    [FlipableAttribute(0x26C2, 0x26CC)]
	public class BowOfTheValkyrie : BaseRanged
	{      
        public override int EffectID { get { return 0xF42; } }
		public override Type AmmoType{ get{ return typeof( Arrow ); } }
		public override Item Ammo{ get{ return new Arrow(); } }

        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.ArmorIgnore; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.MovingShot; } }

        public override int ArtifactRarity { get { return 70; } }

		public override int AosStrengthReq{ get{ return 45; } }
		public override int AosMinDamage{ get{ return 15; } }
		public override int AosMaxDamage{ get{ return 25; } }
		public override int AosSpeed{ get{ return 27; } }
		
		#region Mondain's Legacy
		public override float MlSpeed{ get{ return 4.00f; } }
		#endregion


		public override int DefMaxRange{ get{ return 10; } }
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 190; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.ShootBow; } }

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
        public BowOfTheValkyrie(): base(0x26C2)
		{
            Name = "Bow Of The Valkyrie";
			Weight = 2.0;
            Hue = 1153;

            Attributes.AttackChance = Utility.RandomMinMax(9, 20);
            Attributes.DefendChance = Utility.RandomMinMax(12, 25);
            Attributes.WeaponDamage = Utility.RandomMinMax(10, 17);
            Attributes.BonusStr = Utility.RandomMinMax(3, 8);
            Attributes.WeaponSpeed = Utility.RandomMinMax(7, 25);
            Attributes.SpellChanneling = 1;
            Attributes.Luck = Utility.RandomMinMax(100, 250);
            Attributes.RegenHits = Utility.RandomMinMax(7, 12);
            Attributes.RegenStam = Utility.RandomMinMax(7, 12);
            WeaponAttributes.ResistPhysicalBonus = Utility.RandomMinMax(10, 18);
            WeaponAttributes.HitLightning = Utility.RandomMinMax(10, 25);
            WeaponAttributes.UseBestSkill = 1;
            WeaponAttributes.HitLeechStam = Utility.RandomMinMax(8, 20);

		}

        public BowOfTheValkyrie(Serial serial)
            : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}