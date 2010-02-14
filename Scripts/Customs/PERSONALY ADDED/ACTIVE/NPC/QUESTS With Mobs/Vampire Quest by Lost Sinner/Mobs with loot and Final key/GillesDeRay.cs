/////////////////
///LostSinner///
///////////////
using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles 
{ 
	[CorpseName( "a demonic corpse" )] 
	public class GillesDeRay : BaseCreature 
	{ 
		[Constructable] 
		public GillesDeRay() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{ 
			Title = "Lord of Darkness";
			Name = ( "Gilles De Ray" );
			Body = 400;
			Hue = 1;  

			SetStr( 800, 950 );
			SetDex( 91, 115 );
			SetInt( 300, 320 );

			SetHits( 2820, 3225 );

			SetDamage( 20, 22 );

			SetDamageType( ResistanceType.Cold, 20 );
            SetDamageType(ResistanceType.Energy, 20);
            SetDamageType(ResistanceType.Poison, 20);
            SetDamageType(ResistanceType.Physical, 40);

            SetResistance(ResistanceType.Fire, 10, 20);
            SetResistance(ResistanceType.Cold, 40, 50);
            SetResistance(ResistanceType.Poison, 60, 70);
            SetResistance(ResistanceType.Energy, 40, 50);

			SetSkill( SkillName.EvalInt, 100.0, 135.0 );
            		SetSkill(SkillName.Tactics, 125.1, 130.0);
			SetSkill( SkillName.MagicResist, 75.0, 97.5 );
			SetSkill( SkillName.Wrestling, 100.2, 100.0 );
			SetSkill( SkillName.Meditation, 120.0);
			SetSkill( SkillName.Focus, 120.0);
			SetSkill( SkillName.Magery, 120.0, 140 );

            Fame = 15000;
            Karma = -15000;

			VirtualArmor = 70;
			
			VampireRobe robe = new VampireRobe();
			robe.Movable = false;
			robe.Hue = 1;
			AddItem(robe);
		}

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 2);
            AddLoot(LootPack.HighScrolls, 5);
            AddLoot(LootPack.Gems, 10);
        }

        public override bool OnBeforeDeath()
        {
            switch (Utility.Random(20))
            {
                case 0:
                case 1: PackItem(new VampireLeatherChest()); break;
                case 2:
                case 3: PackItem(new VampireLeatherGloves()); break;
                case 4:
                case 5: PackItem(new VampireLeatherGorget()); break;
                case 6:
                case 7: PackItem(new VampireLeatherLegs()); break;
                case 8:
                case 9: PackItem(new VampireLeatherArms()); break;
                case 10: PackItem(new VampireRobe()); break;
            }

            PackGold(1000, 1500);

            return base.OnBeforeDeath();
        }

		public override bool AlwaysMurderer{ get{ return true; } }

		public GillesDeRay( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
