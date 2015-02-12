using System;
using Server;

namespace Server.Items
{
	public class JasonMask : BaseHat
	{
		[Constructable]
		public JasonMask() : this( 0 )
		{
		}

		[Constructable]
		public JasonMask( int hue ) : base( 0x154B, hue )
		{
			Name = "Jason's Mask";
			Weight = 2.0;
			Hue = 1150;
			LootType = LootType.Cursed;
			
			Attributes.AttackChance = 15;
			Attributes.BonusDex = 10;
			Attributes.BonusHits = 20;
			Attributes.BonusInt = 10;
			Attributes.CastRecovery = 4;
			Attributes.CastSpeed = 3;
			Attributes.DefendChance = 15;
			Attributes.Luck = 200;
			Attributes.NightSight = 1;
			Attributes.ReflectPhysical = 15;
			Attributes.SpellChanneling = 1;
			Attributes.WeaponDamage = 60;
			Attributes.WeaponSpeed = 50;
		}
		
		public override bool Dye( Mobile from, DyeTub sender )
		{
			from.SendLocalizedMessage( sender.FailMessage );
			return false;
		}
		
		public JasonMask( Serial serial ) : base( serial )
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