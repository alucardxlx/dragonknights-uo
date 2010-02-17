  /////////////////////////////
 //////  LostSinner  /////////
/////////////////////////////

using System;
using Server.Network;
using Server.Items;
using Server.Spells;

namespace Server.Items
{

	public class FistsOfPainandFury : Club
	{

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public FistsOfPainandFury()
		{
			Name = "Fists of Pain and Fury";
			Hue = 295;
			Attributes.SpellChanneling = 1;
			Attributes.WeaponSpeed = 15;
			Attributes.WeaponDamage = 20;
			WeaponAttributes.HitEnergyArea = 25;
			Weight = 5.0;
		}
		public override void OnHit( Mobile attacker, Mobile defender, double damageBonus )
		{
            switch (Utility.Random(5))
            {
                case 0:
                    defender.FixedParticles(0x37CC, 1, 40, 97, 3, 9917, 0);
                    attacker.Say("Suffer my fury!!!"); break;
                    SpellHelper.Damage(TimeSpan.Zero, defender, attacker, 15, 0, 30, 0, 0, 0);
            }
		
			base.OnHit( attacker, defender, damageBonus );
		}
	
		public FistsOfPainandFury( Serial serial ) : base( serial )
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