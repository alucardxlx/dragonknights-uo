  /////////////////////////////
 //////  LostSinner  /////////
/////////////////////////////

using System;
using Server.Network;
using Server.Items;
using Server.Spells;

namespace Server.Items
{

	public class VampiresFang : Scimitar
	{
		

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public VampiresFang()
		{
			ItemID = 5046;
			Name = "Fang of the Vampire";
			Hue = 601;
			Attributes.SpellChanneling = 1;
			Attributes.WeaponSpeed = 15;
			Attributes.WeaponDamage = 20;
			Attributes.DefendChance = 10;
			
			
			WeaponAttributes.HitLeechStam = 20;
			Weight = 5.0;
			
		}
		public override void OnHit( Mobile attacker, Mobile defender, double damageBonus )
		{
			PlaySwingAnimation( attacker );
			PlayHurtAnimation( defender );

			attacker.PlaySound( GetHitAttackSound( attacker, defender ) );
			defender.PlaySound( GetHitDefendSound( attacker, defender ) );

			switch ( Utility.Random( 5 ) )
                        { 
                          case 0:
				  defender.FixedParticles( 0x374A, 10, 15, 5013, 0x496, 0, EffectLayer.Waist ); 
				  attacker.Say( "Sacrafice your life to me!!!" ); break;
                  SpellHelper.Damage(TimeSpan.Zero, defender, attacker, 15, 0, 0, 0, 0, 30);
          }
		
			base.OnHit( attacker, defender, damageBonus );
		}
	
		public VampiresFang( Serial serial ) : base( serial )
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
