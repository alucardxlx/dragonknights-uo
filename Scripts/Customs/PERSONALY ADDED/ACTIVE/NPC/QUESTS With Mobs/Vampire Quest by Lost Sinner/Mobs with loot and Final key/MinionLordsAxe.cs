//////////////////////////
//Created by LostSinner//
////////////////////////
using System;
using Server;

namespace Server.Items
{
	public class MinionLordsAxe : ExecutionersAxe
	{
	 	[Constructable]
	 	public MinionLordsAxe()
	 	{
	 	 	Name = "a Minion Lords axe";
	 	 	Hue = 1161;
	 	 	
	 	 	Attributes.BonusStr = 5;
	 	 		 	 	
	 	 	Attributes.RegenHits = 2;
	 	 	
	 	 	Attributes.AttackChance = 10;
	 	 	Attributes.WeaponDamage = 25;
            Attributes.WeaponSpeed = 10;	 	
	 	 	
	 	 	WeaponAttributes.HitFireball = 25;
	 	}

	 	public MinionLordsAxe(Serial serial) : base( serial )
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
