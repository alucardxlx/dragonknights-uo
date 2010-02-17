
//////////////////////////
//Created by LostSinner//
////////////////////////
using System;
using Server;

namespace Server.Items
{
	public class VampBracelet : BaseBracelet
	{
		
		[Constructable]
        public VampBracelet()
            : base(0x1086)
		{
			Hue = 601;
			Name = "Draculas Bracchium ";
			Attributes.WeaponDamage = 10;
						
			Attributes.RegenStam = 1;
			Attributes.RegenMana= 1;
			Attributes.RegenHits = 1;

            SkillBonuses.SetValues(0, SkillName.Necromancy, 1.0);

		}

		public VampBracelet( Serial serial ) : base( serial )
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