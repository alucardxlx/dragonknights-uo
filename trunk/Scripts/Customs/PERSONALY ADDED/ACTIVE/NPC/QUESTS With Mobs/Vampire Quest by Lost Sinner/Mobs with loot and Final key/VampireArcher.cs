/////////////////
///LostSinner///
///////////////
using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles 
{ 
	[CorpseName( "an Vampire Archers corpse" )] 
	public class VampireArcher : BaseCreature 
	{ 
		[Constructable] 
		public VampireArcher() : base( AIType.AI_Archer, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{ 	
			Title = "the Vampire Archer";
			Name = NameList.RandomName( "male" );
			Body = 400;
			Hue = 0; 

			SetStr( 365, 365 );
			SetDex( 191, 215 );
			SetInt( 196, 220 );

			SetHits( 520, 525 );

			SetDamage( 10, 15 );

			SetDamageType( ResistanceType.Physical, 100 );

            SetResistance(ResistanceType.Fire, 10, 20);
            SetResistance(ResistanceType.Cold, 30, 40);
			SetResistance( ResistanceType.Poison, 50, 60 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.Archery, 75.1, 100.0 );
			SetSkill( SkillName.Tactics, 75.1, 100.0 );
			SetSkill( SkillName.MagicResist, 75.0, 97.5 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 20.2, 60.0 );

			Fame = 2500;
			Karma = -2500;

			VirtualArmor = 50;

			Item hair = new Item( Utility.RandomList( 0x203B, 0x2049, 0x2048, 0x204A ) );
			hair.Hue = 1150;
			hair.Layer = Layer.Hair;
			hair.Movable = false;
			AddItem( hair );
			
			Item beard = new Item( Utility.RandomList( 0x203E, 0x203F, 0x2040, 0x2041, 0x204B, 0x204C, 0x204D ) );

				beard.Hue = hair.Hue;
				beard.Layer = Layer.FacialHair;
				beard.Movable = false;
				AddItem( beard );
			
				//switch ( Utility.Random( 50 ))
					if (Utility.Random(50) == 1 ) 
   					AddItem( new VampiriacBow() ); 
					else 
				    AddItem( new CompositeBow());

			
			VampireLeatherChest chest = new VampireLeatherChest();
			chest.Movable = false;
   			AddItem(chest);
   			
   			VampireLeatherGloves gloves = new VampireLeatherGloves();
   			gloves.Movable = false;
  			AddItem(gloves);
  			
  			VampireLeatherGorget gorget = new VampireLeatherGorget();
  			gorget.Movable = false;
 			AddItem(gorget);
 			
 			VampireLeatherLegs legs = new VampireLeatherLegs();
 			legs.Movable = false;
			AddItem(legs);

			VampireRobe robe = new VampireRobe();
			robe.Movable = false;
			AddItem(robe);

			Sandals sandals = new Sandals();
            if (Utility.Random(30) == 1)
                sandals.Hue = 1;
			AddItem( sandals );

			//Horse VampiriacSteed = new VampiriacSteed();
        	 	//horse.Hue = 686;
			//horse.Hits = 200;
			//horse.Karma = 500;
        		//VampiriacSteed.Rider = this;
        		new VampiriacSteed().Rider = this;

		}

		public override void GenerateLoot()
		{			
			AddLoot( LootPack.Rich, 2 );
			AddLoot( LootPack.MedScrolls, 2 );
			AddLoot( LootPack.Gems, 5 ); 
			AddItem( new Arrow(30) );
		}

        public override bool OnBeforeDeath()
        {
            switch (Utility.Random(30))
            {
                case 0: PackItem(new VampireLeatherChest()); break;
                case 1: PackItem(new VampireLeatherGloves()); break;
                case 2: PackItem(new VampireLeatherGorget()); break;
                case 3: PackItem(new VampireLeatherLegs()); break;
                case 4: PackItem(new VampireLeatherArms()); break;

            }
            PackGold(50, 100);
            IMount mount = this.Mount;

            if (mount != null)
                mount.Rider = null;

            if (mount is Mobile)
                ((Mobile)mount).Delete();

            if (Utility.RandomDouble() < 0.3)
            {
                Mobile mob = new VampiriacSteed();
                mob.MoveToWorld(this.Location, this.Map);
                mob.Direction = this.Direction;
            }

            return base.OnBeforeDeath();
        }

		public override bool CanRummageCorpses{ get{ return true; } }
		public override bool AlwaysMurderer{ get{ return true; } }

		public VampireArcher( Serial serial ) : base( serial )
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
