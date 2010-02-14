/*Created by Hammerhand*/

using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
	public class ValkyrieBow : BaseCreature
	{
        private bool m_Stunning;

        public override bool IsScaredOfScaryThings { get { return false; } }
        public override bool IsScaryToPets { get { return true; } }

        public override bool BardImmune { get { return !Core.SE; } }
        public override bool Unprovokable { get { return Core.SE; } }
        public override bool Uncalmable { get { return Core.SE; } }
        public override Poison PoisonImmune { get { return Poison.Lethal; } }

		public override bool ClickTitle{ get{ return false; } }

		[Constructable]
		public ValkyrieBow() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{

            Title = "the Valkyrie";
			Hue = Utility.RandomSkinHue();

            Body = 0x191;
			Name = NameList.RandomName( "female" );
            Female = true;

			SetStr( 226, 370 );
			SetDex( 221, 245 );
			SetInt( 371, 485 );

            SetHits(2500, 4000);
            SetMana(500, 650);
			SetDamage( 20, 35 );

			SetSkill( SkillName.MagicResist, 85.0, 97.5 );
			SetSkill( SkillName.Archery, 109.0, 119.5 );
			SetSkill( SkillName.Tactics, 105.0, 117.5 );
			SetSkill( SkillName.Wrestling, 95.0, 107.5 );
            SetSkill(SkillName.Healing, 100.0);

			Fame = 15000;
			Karma = -15000;

            ChestplateOfTheValkyrie chest = new ChestplateOfTheValkyrie();
            chest.Hue = 1153;
            chest.Movable = false;
            AddItem(chest);

            GlovesOfTheValkyrie gloves = new GlovesOfTheValkyrie();
            gloves.Hue = 1153;
            gloves.Movable = false;
            AddItem(gloves);

            CollarOfTheValkyrie gorget = new CollarOfTheValkyrie();
            gorget.Hue = 1153;
            gorget.Movable = false;
            AddItem(gorget);

            CloakOfTheValkyrie cloak = new CloakOfTheValkyrie();
            cloak.Hue = 1153;
            cloak.Movable = false;
            AddItem(cloak);

            BowOfTheValkyrie weapon = new BowOfTheValkyrie();
            weapon.Hue = 1153;
            weapon.Movable = false;
            AddItem(weapon);

            HelmOfTheValkyrie clothes = new HelmOfTheValkyrie();
            clothes.Hue = 1153;
            clothes.Movable = false;
            AddItem(clothes);

            SandalsOfTheValkyrie shoes = new SandalsOfTheValkyrie();
            shoes.Hue = 1153;
            shoes.Movable = false;
            AddItem(shoes);

            SkirtOfTheValkyrie skirt = new SkirtOfTheValkyrie();
            skirt.Hue = 1153;
            skirt.Movable = false;
            AddItem(skirt);			

			Utility.AssignRandomHair( this );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 3 );
		}

        public override bool AlwaysMurderer { get { return true; } }

        public override bool OnBeforeDeath()
        {
            switch (Utility.Random(20))
            {
                case 0: PackItem(new ChestplateOfTheValkyrie()); break;
                case 1: PackItem(new CollarOfTheValkyrie()); break;
                case 2: PackItem(new GlovesOfTheValkyrie()); break;
                case 3: PackItem(new CloakOfTheValkyrie()); break;
                case 4: PackItem(new BowOfTheValkyrie()); break;
                case 5: PackItem(new HelmOfTheValkyrie()); break;
                case 6: PackItem(new SandalsOfTheValkyrie()); break;
                case 7: PackItem(new SkirtOfTheValkyrie()); break;
            }

            return base.OnBeforeDeath();
        }

        public ValkyrieBow(Serial serial)
            : base(serial)
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
