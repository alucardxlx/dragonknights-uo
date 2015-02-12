  /////////////////////////////
 //////  LostSinner  /////////
/////////////////////////////

using System;
using Server.Network;
using Server.Items;
using Server.Spells;
using Server.Engines.Harvest;

namespace Server.Items
{
	[FlipableAttribute( 0x26BA, 0x26C4 )]
	public class ScytheOfFaith : BasePoleArm
	{
		
		
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.BleedAttack; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ParalyzingBlow; } }

		public override int AosStrengthReq{ get{ return 45; } }
		public override int AosMinDamage{ get{ return 15; } }
		public override int AosMaxDamage{ get{ return 18; } }
		public override int AosSpeed{ get{ return 32; } }

		public override int OldStrengthReq{ get{ return 45; } }
		public override int OldMinDamage{ get{ return 15; } }
		public override int OldMaxDamage{ get{ return 18; } }
		public override int OldSpeed{ get{ return 32; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		public override HarvestSystem HarvestSystem{ get{ return null; } }

		[Constructable]
		public ScytheOfFaith() : base( 0x26BA )
		{
			Name = "The Scythe of Faith";
			Hue = 906;
			Attributes.SpellChanneling = 1;
			Attributes.WeaponSpeed = 25;
			Attributes.WeaponDamage = 35;
			Attributes.AttackChance = 10;
			Attributes.DefendChance = 10;
			
			
			Weight = 5.0;
			
		}
		public override void OnHit( Mobile attacker, Mobile defender, double damageBonus )
		{
			PlaySwingAnimation( attacker );
			PlayHurtAnimation( defender );

			attacker.PlaySound( GetHitAttackSound( attacker, defender ) );
			defender.PlaySound( GetHitDefendSound( attacker, defender ) );

            switch (Utility.Random(5))
            {
            		case 0:
            		{
            			defender.FixedParticles(0x37CC, 1, 40, 97, 3, 9917, 0);
            			attacker.Say("Lord Defend me from this evil!!!");
            			SpellHelper.Damage(TimeSpan.Zero, defender, attacker, 15, 0, 0, 30, 0, 0);
            			break;
            		}
            }
            
		
			base.OnHit( attacker, defender, damageBonus );
		}
	
		public ScytheOfFaith( Serial serial ) : base( serial )
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

			if ( Weight == 15.0 )
				Weight = 5.0;
		}
	}
}