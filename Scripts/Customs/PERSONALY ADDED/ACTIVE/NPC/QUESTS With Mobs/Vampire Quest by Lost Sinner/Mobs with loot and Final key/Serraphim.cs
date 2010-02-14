/////////////////
///LostSinner///
///////////////
using System;
using System.Collections;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles 
{ 
	[CorpseName( "a vampiriac corpse" )] 
	public class Serraphim : BaseCreature 
	{ 
		[Constructable] 
		public Serraphim() : base( AIType.AI_Archer, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{ 
			Title = "the Fallen One";
			Name = "Serraphim";
			Body = 400;
			Hue = 0;  

			SetStr( 900, 940 );
			SetDex( 191, 115 );
			SetInt( 470, 490 );

			SetHits( 1520, 1700 );

			SetDamage( 14, 16 );

			SetDamageType( ResistanceType.Poison, 50 );
            SetDamageType(ResistanceType.Cold, 50);

            SetResistance(ResistanceType.Fire, 30, 40);
            SetResistance(ResistanceType.Cold, 50, 60);
            SetResistance(ResistanceType.Poison, 60, 70);
            SetResistance(ResistanceType.Energy, 50, 60);

			
            SetSkill(SkillName.Archery, 80.0, 90.0);
            SetSkill(SkillName.Anatomy, 80.0, 90.0); 
            SetSkill(SkillName.Tactics, 80.0, 90.0);
			SetSkill( SkillName.MagicResist, 75.0, 97.5 );
			SetSkill( SkillName.Wrestling, 90.2, 110.0 );
			SetSkill( SkillName.Focus, 120.0);

			Fame = 15000;
			Karma = -15000;

			VirtualArmor = 70;

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
				
				
			VampireLeatherChest chest = new VampireLeatherChest();
			chest.Hue = 1150;
			chest.Movable = false;
   			AddItem(chest);
   			
   			VampireLeatherGloves gloves = new VampireLeatherGloves();
			gloves.Hue = 1150;
   			gloves.Movable = false;
  			AddItem(gloves);
  			
  			VampireLeatherGorget gorget = new VampireLeatherGorget();
			gorget.Hue = 1150;
  			gorget.Movable = false;
 			AddItem(gorget);
 			
 			VampireLeatherLegs legs = new VampireLeatherLegs();
			legs.Hue = 1150;
 			legs.Movable = false;
			AddItem(legs);

			VampireLeatherArms arms = new VampireLeatherArms();
			arms.Hue = 1150;
			arms.Movable = false;
			AddItem(arms);

			VampireRobe VampireRobe = new VampireRobe();
			VampireRobe.Hue = 1150;
			VampireRobe.Movable = false;
			AddItem(VampireRobe);

			HalfApron halfapron = new HalfApron();
			halfapron.Movable = false;
			halfapron.Hue = 0;
			halfapron.Layer = Layer.Waist;
			AddItem(halfapron);
			
			Sandals sandals = new Sandals();
			sandals.Hue = 1150;
			AddItem( sandals );

            Item weapon = new VampiriacBow();
				weapon.Movable = false;
				weapon.Hue = 1150;
			AddItem( weapon );
            PackItem(new Arrow(30));
		}

        public void DrainLife()
        {
            ArrayList list = new ArrayList();

            foreach (Mobile m in this.GetMobilesInRange(3))
            {
                if (m == this || !CanBeHarmful(m))
                    continue;

                if (m is BaseCreature && (((BaseCreature)m).Controlled || ((BaseCreature)m).Summoned || ((BaseCreature)m).Team != this.Team))
                    list.Add(m);
                if (m is BaseCreature)
                    list.Add(m);
                else if (m.Player)
                    list.Add(m);

            }

            foreach (Mobile m in list)
            {
                DoHarmful(m);

                m.FixedParticles(0x374A, 10, 15, 5013, 0x496, 0, EffectLayer.Waist);
                m.PlaySound(0x231);

                m.SendMessage("You feel the life drain out of you!");

                int toDrain = Utility.RandomMinMax(10, 15);

                Hits += toDrain;
                m.Damage(toDrain, this);
            }
        }

        public override void OnGaveMeleeAttack(Mobile defender)
        {
            base.OnGaveMeleeAttack(defender);

            if (0.33 >= Utility.RandomDouble())
                DrainLife();
        }

        public override void OnGotMeleeAttack(Mobile attacker)
        {
            base.OnGotMeleeAttack(attacker);

            if (0.33 >= Utility.RandomDouble())
                DrainLife();
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
                case 0: PackItem(new RodOfRegrets()); break;
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

		public Serraphim( Serial serial ) : base( serial )
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
