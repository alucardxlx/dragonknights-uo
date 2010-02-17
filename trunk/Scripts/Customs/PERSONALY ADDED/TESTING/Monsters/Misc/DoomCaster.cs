using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a doom caster corpse" )]
	public class DoomCaster : BaseCreature
	{
		[Constructable]
		public DoomCaster() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.1, 0.1 )
		{
			Name = "a doom caster";
			Body = 316;
			BaseSoundID = 377;

			SetStr( 216, 305 );
			SetDex( 96, 115 );
			SetInt( 966, 1045 );

			SetHits( 560, 595 );

			SetDamage( 15, 27 );

			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Cold, 40 );
			SetDamageType( ResistanceType.Energy, 40 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 25, 30 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 50, 60 );
			SetResistance( ResistanceType.Energy, 25, 30 );

			SetSkill( SkillName.EvalInt, 120.1, 130.0 );
			SetSkill( SkillName.Necromancy, 120.1, 130.0 );
			SetSkill( SkillName.SpiritSpeak, 120.1, 130.0 );
			SetSkill( SkillName.Meditation, 100.1, 101.0 );
			SetSkill( SkillName.Poisoning, 100.1, 101.0 );
			SetSkill( SkillName.MagicResist, 175.2, 200.0 );
			SetSkill( SkillName.Tactics, 90.1, 100.0 );
			SetSkill( SkillName.Wrestling, 75.1, 100.0 );

			Fame = 23000;
			Karma = -23000;

			VirtualArmor = 60;

			AddItem( new DarkSource() );

			int count = Utility.RandomMinMax( 2, 3 );

			for ( int i = 0; i < count; ++i )
				PackItem( new TreasureMap( 3, Map.Trammel ) );
		}

		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override int TreasureMapLevel{ get{ return Core.AOS ? 4 : 1; } }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich );
		}

		public DoomCaster( Serial serial ) : base( serial )
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