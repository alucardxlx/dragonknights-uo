//////////////////////////
//Created By FMKaraokeRadio//
////////////////////////
using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x13FF, 0x13FF )]
	public class PsychoBlade : BaseWeapon

	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.CrushingBlow; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ArmorIgnore; } }

		public override int ArtifactRarity{ get{ return 35; } }

		public override int AosStrengthReq{ get{ return 30; } }
		public override int AosMinDamage{ get{ return 20; } }
		public override int AosMaxDamage{ get{ return 22; } }
		public override int AosSpeed{ get{ return 40; } }

		public override int OldStrengthReq{ get{ return 25; } }
		public override int OldMinDamage{ get{ return 5; } }
		public override int OldMaxDamage{ get{ return 29; } }
		public override int OldSpeed{ get{ return 45; } }

		public override int DefHitSound{ get{ return 0x237; } }
		public override int DefMissSound{ get{ return 0x23A; } }

		public override int InitMinHits{ get{ return 100; } }
		public override int InitMaxHits{ get{ return 100; } }

		[Constructable]
		public PsychoBlade() : base( 0x26CF )
		{
			Weight = 6.0;
			Name = "Psychotic";
			Attributes.AttackChance = 35;
                  Attributes.BonusDex = 15;
                  Attributes.BonusInt = 15;
                  Attributes.BonusStr = 15;
                  Attributes.ReflectPhysical = 35;
                  Attributes.RegenHits = 5;
                  Attributes.RegenStam = 7;
			Attributes.WeaponDamage = 45;
			Attributes.WeaponSpeed = 50;
			WeaponAttributes.DurabilityBonus = 300;
			WeaponAttributes.HitFireball = 45;
			WeaponAttributes.HitLeechHits = 50;
                  WeaponAttributes.HitLeechMana = 50;
                  WeaponAttributes.HitLeechStam = 50;
			WeaponAttributes.SelfRepair = 10;
			Hue = 1172;
		}

		public PsychoBlade( Serial serial ) : base( serial )
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