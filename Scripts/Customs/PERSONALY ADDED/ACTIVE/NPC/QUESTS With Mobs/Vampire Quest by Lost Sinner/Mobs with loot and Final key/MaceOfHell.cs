  /////////////////////////////
 //////  LostSinner  /////////
/////////////////////////////

using System;
using Server.Network;
using Server.Items;
using Server.Spells;

namespace Server.Items
{
	public class MaceOfHell : WarMace
	{
		

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public MaceOfHell()
		{
			ItemID = 5127;
			Name = "Hells Might";
			Hue = 49;
			
			Attributes.WeaponSpeed = 15;
			Attributes.WeaponDamage = 35;
			Attributes.AttackChance = 10;
			Attributes.DefendChance = 10;
			
			
			WeaponAttributes.HitFireball = 25;
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
                    defender.FixedParticles(0x3709, 10, 30, 5052, EffectLayer.LeftFoot);
                    attacker.Say("Feel the hate Hell holds for you!!!"); break;
                    SpellHelper.Damage(TimeSpan.Zero, defender, attacker, 15, 0, 20, 20, 20, 20);
            }
		
			base.OnHit( attacker, defender, damageBonus );
		}
	
		public MaceOfHell( Serial serial ) : base( serial )
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