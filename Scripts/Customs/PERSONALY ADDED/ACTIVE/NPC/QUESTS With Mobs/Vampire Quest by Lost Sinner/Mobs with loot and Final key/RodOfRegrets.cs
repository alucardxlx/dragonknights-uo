  /////////////////////////////
 //////  LostSinner  /////////
/////////////////////////////

using System;
using Server.Network;
using Server.Items;
using Server.Spells;

namespace Server.Items
{
	
	public class RodOfRegrets : QuarterStaff
	{
		

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public RodOfRegrets()
		{
			ItemID = 3721;
			Name = "Rod Of Regrets";
			Hue = 1055;
			Attributes.SpellChanneling = 1;
			Attributes.WeaponSpeed = 25;
			Attributes.WeaponDamage = 20;
			Attributes.DefendChance = 10;			
			
			WeaponAttributes.HitLeechMana = 35;
			Weight = 5.0;
		}
		public override void OnHit( Mobile attacker, Mobile defender, double damageBonus )
		{
			double damage = 0.0;

			PlaySwingAnimation( attacker );
			PlayHurtAnimation( defender );

			attacker.PlaySound( GetHitAttackSound( attacker, defender ) );
			defender.PlaySound( GetHitDefendSound( attacker, defender ) );

            switch (Utility.Random(5))
            {
                case 0:
                    defender.FixedParticles(0x373A, 10, 15, 5012, EffectLayer.Waist);
                    attacker.Say("Why must I endure this!!!"); break;
                    SpellHelper.Damage(TimeSpan.Zero, defender, attacker, 15, 0, 0, 0, 0, 0);
            }

		
			base.OnHit( attacker, defender, damageBonus );
		}
	
		public RodOfRegrets( Serial serial ) : base( serial )
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
