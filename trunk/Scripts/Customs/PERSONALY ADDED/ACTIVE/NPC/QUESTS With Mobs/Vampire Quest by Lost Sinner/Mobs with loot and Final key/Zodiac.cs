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
	public class Zodiac : BaseCreature 
	{ 
		[Constructable] 
		public Zodiac() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{ 
			Title = "the Blood Hungry Vampire";
			Name = "Zodiac";
			Body = 400;
			Hue = 0;  

			SetStr( 900, 940 );
			SetDex( 191, 115 );
			SetInt( 470, 490 );

			SetHits( 1120, 1300 );

			SetDamage( 14, 16 );

			SetDamageType( ResistanceType.Physical, 50 );
            SetDamageType(ResistanceType.Energy, 50);

            SetResistance(ResistanceType.Fire, 30, 40);
            SetResistance(ResistanceType.Cold, 40, 50);
            SetResistance(ResistanceType.Poison, 60, 70);
            SetResistance(ResistanceType.Energy, 40, 50);


			SetSkill( SkillName.EvalInt, 100.0, 120.0 );
            SetSkill(SkillName.Tactics, 95.1, 110.0);
			SetSkill( SkillName.MagicResist, 75.0, 97.5 );
			SetSkill( SkillName.Wrestling, 90.2, 110.0 );
			SetSkill( SkillName.Meditation, 120.0);
			SetSkill( SkillName.Focus, 120.0);
			SetSkill( SkillName.Magery, 120.0 );

			Fame = 15000;
			Karma = -15000;

			VirtualArmor = 65;

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
			chest.Hue = 39;
			chest.Movable = false;
   			AddItem(chest);
   			
   			VampireLeatherGloves gloves = new VampireLeatherGloves();
			gloves.Hue = 39;
   			gloves.Movable = false;
  			AddItem(gloves);
  			
  			VampireLeatherGorget gorget = new VampireLeatherGorget();
			gorget.Hue = 39;
  			gorget.Movable = false;
 			AddItem(gorget);
 			
 			VampireLeatherLegs legs = new VampireLeatherLegs();
			legs.Hue = 39;
 			legs.Movable = false;
			AddItem(legs);

			VampireLeatherArms arms = new VampireLeatherArms();
			arms.Hue = 39;
			arms.Movable = false;
			AddItem(arms);

			VampireRobe VampireRobe = new VampireRobe();
			VampireRobe.Hue = 39;
			VampireRobe.Movable = false;
			AddItem(VampireRobe);
			
			Sandals sandals = new Sandals();
			sandals.Hue = 39;
			AddItem( sandals );

			HalfApron halfapron = new HalfApron();
			halfapron.Movable = false;
			halfapron.Hue = 39;
			halfapron.Layer = Layer.Waist;
			AddItem(halfapron);

			Item weapon = new VampiresFang();
				weapon.Movable = false;
				weapon.Hue = 39;
			AddItem( weapon );

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
            switch (Utility.Random(50))
            {
                case 0: PackItem(new VampiresFang()); break;
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

		public Zodiac( Serial serial ) : base( serial )
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
