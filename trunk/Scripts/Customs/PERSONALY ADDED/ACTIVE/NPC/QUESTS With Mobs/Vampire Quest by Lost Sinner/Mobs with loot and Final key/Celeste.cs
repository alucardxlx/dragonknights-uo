/////////////////
///LostSinner///
///////////////
using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles 
{ 
	[CorpseName( "an Vampiriac corpse" )] 
	public class Celeste : BaseCreature 
	{ 
		[Constructable] 
		public Celeste() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{ 
			Title = "the Queen of the Underworld";
			Name = "Celeste";
			Body = 745;
			Hue = 0;  

			SetStr( 900, 940 );
			SetDex( 191, 115 );
			SetInt( 670, 690 );

			SetHits( 1520, 1700 );

			SetDamage( 14, 16 );

			SetDamageType( ResistanceType.Energy, 100 );

            SetResistance(ResistanceType.Fire, 30, 40);
            SetResistance(ResistanceType.Cold, 50, 60);
            SetResistance(ResistanceType.Poison, 60, 70);
            SetResistance(ResistanceType.Energy, 50, 60);
	
			VirtualArmor = 65;

            SetSkill(SkillName.Swords, 80.0, 90.0);
            SetSkill(SkillName.Anatomy, 80.0, 90.0);
            SetSkill(SkillName.Tactics, 80.0, 90.0);
			SetSkill( SkillName.MagicResist, 75.0, 97.5 );
			SetSkill( SkillName.Wrestling, 90.2, 110.0 );
			SetSkill( SkillName.Focus, 120.0);

			Fame = 15000;
			Karma = -15000;
            							
			VampireLeatherChest chest = new VampireLeatherChest();
			chest.Movable = false;
			chest.Hue = 906;
   			AddItem(chest);
   			
   			VampireLeatherGloves gloves = new VampireLeatherGloves();
   			gloves.Movable = false;
			gloves.Hue = 906;
  			AddItem(gloves);
  			
  			VampireLeatherGorget gorget = new VampireLeatherGorget();
  			gorget.Movable = false;
			gorget.Hue = 906;
 			AddItem(gorget);
 			
 			VampireLeatherLegs legs = new VampireLeatherLegs();
 			legs.Movable = false;
			legs.Hue = 906;
			AddItem(legs);

			VampireLeatherArms arms = new VampireLeatherArms();
			arms.Movable = false;
			arms.Hue = 906;
			AddItem(arms);

			VampireRobe VampireRobe = new VampireRobe();
			VampireRobe.Hue = 906;
			VampireRobe.Movable = false;
			AddItem(VampireRobe);
			
			Sandals sandals = new Sandals();
			sandals.Hue = 906;
			AddItem( sandals );

			HalfApron halfapron = new HalfApron();
			halfapron.Movable = false;
			halfapron.Hue = 906;
			halfapron.Layer = Layer.Waist;
			AddItem(halfapron);

			Item weapon = new ScytheOfFaith();
				weapon.Movable = false;
				weapon.Hue = 906;
			AddItem( weapon );

		}

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 2);
            AddLoot(LootPack.HighScrolls, 5);
            AddLoot(LootPack.Gems, 10);
        }

        public override bool OnBeforeDeath()
        {
            switch (Utility.Random(30))
            {
                case 0: PackItem(new ScytheOfFaith()); break;
            }

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

		public Celeste( Serial serial ) : base( serial )
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
