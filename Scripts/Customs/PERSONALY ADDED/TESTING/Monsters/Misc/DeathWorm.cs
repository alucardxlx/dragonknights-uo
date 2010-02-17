using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a death worm corpse" )]
	public class DeathWorm : BaseCreature
	{
		[Constructable]
		public DeathWorm() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Allghoi Khorkhoi";
			Title = "the death worm";
			Body = 52;
			Hue = 30;
			BaseSoundID = 456;

			SetStr( 22, 34 );
			SetDex( 16, 25 );
			SetInt( 966, 1045 );

			SetHits( 50, 75 );

			SetDamage( 1, 4 );

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

			Fame = 300;
			Karma = -300;

			VirtualArmor = 16;
			
		}

//		public override void OnCarve( Mobile from, Corpse corpse )
//		{
//			base.OnCarve( from, corpse );
//
//			corpse.DropItem( new DeathWormHeart( ) );
//		}

		public override Poison HitPoison{ get{ return Poison.Lethal; } }
		public override bool DeathAdderCharmable{ get{ return false; } }
		public override int Meat{ get{ return 1; } }
		public override bool Unprovokable{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public DeathWorm(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}