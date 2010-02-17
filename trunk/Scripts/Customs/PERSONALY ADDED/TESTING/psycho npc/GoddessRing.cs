//////////////////////////////
//Created By FMKaraokeRadio//
////////////////////////////

using System;
using Server;

namespace Server.Items
{
	public class PsychoRing : GoldRing
	{
		public override int ArtifactRarity{ get{ return 40; } }

		[Constructable]
		public PsychoRing()
		{
			Name = "Disturbed";
			Hue = 1172;
			Attributes.CastRecovery = 1;
			Attributes.CastSpeed = 1;
                        Attributes.RegenHits = 10;
			Attributes.LowerRegCost = 20;
                        Attributes.WeaponSpeed = 5;
                        Attributes.WeaponDamage = 10;
			Attributes.NightSight = 100;
			Attributes.SpellDamage = 18;
			Attributes.AttackChance = 20;
			Attributes.Luck = 250;
		}

		public PsychoRing( Serial serial ) : base( serial )
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
		}
	}
}