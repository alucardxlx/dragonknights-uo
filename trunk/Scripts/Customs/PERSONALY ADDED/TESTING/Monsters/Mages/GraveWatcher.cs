using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "the grave watcher's corpse" )]
	public class GraveWatcher : BaseCreature
	{
		[Constructable]
		public GraveWatcher() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "the grave watcher";
			Body = 267;
			BaseSoundID = 1440;
			Hue = 607;

			SetStr( 796, 825 );
			SetDex( 86, 105 );
			SetInt( 436, 475 );

			SetHits( 1000 );

			SetDamage( 26, 32 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Cold, 50 );

			SetResistance( ResistanceType.Physical, 100 );
			SetResistance( ResistanceType.Cold, 100 );
			SetResistance( ResistanceType.Fire, 15 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 100 );

			SetSkill( SkillName.MagicResist, 100 );
			SetSkill( SkillName.Tactics, 95.1, 100.0 );
			SetSkill( SkillName.Wrestling, 95.1, 100.0 );

			Fame = 7000;
			Karma = -7000;

			VirtualArmor = 50;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich );
			AddLoot( LootPack.FilthyRich );
			AddLoot( LootPack.FilthyRich );
			AddLoot( LootPack.Gems );
		}

		public override bool BardImmune{ get{ return !Core.AOS; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override int GetAttackSound()

		{
			return 1441;
		}

		public override int GetAngerSound()
		{
			return 1442;
		}

		public override int GetDeathSound()
		{
			return 1438;
		}

		public override int GetHurtSound()
		{
			return 1439;
		}

		public override int GetIdleSound()
		{
			return 1440;
		}

		public override int Meat{ get{ return 2; } }
		public override int TreasureMapLevel{ get{ return 3; } }

		public GraveWatcher( Serial serial ) : base( serial )
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

		public void SpawnBoneKnight( Mobile m )
		{
			Map map = this.Map;

			if ( map == null )
				return;

			BoneKnight spawned = new BoneKnight();

			spawned.Team = this.Team;

			bool validLocation = false;
			Point3D loc = this.Location;

			for ( int j = 0; !validLocation && j < 10; ++j )
			{
				int x = X + Utility.Random( 3 ) - 1;
				int y = Y + Utility.Random( 3 ) - 1;
				int z = map.GetAverageZ( x, y );

				if ( validLocation = map.CanFit( x, y, this.Z, 16, false, false ) )
					loc = new Point3D( x, y, Z );
				else if ( validLocation = map.CanFit( x, y, z, 16, false, false ) )
					loc = new Point3D( x, y, z );
			}

			spawned.MoveToWorld( loc, map );
			spawned.Combatant = m;
		}

		public void EatBoneKnight()
		{
			ArrayList toEat = new ArrayList();
  
			foreach ( Mobile m in this.GetMobilesInRange( 2 ) )
			{
				if ( m is BoneKnight )
					toEat.Add( m );
			}

			if ( toEat.Count > 0 )
			{
				PlaySound( Utility.Random( 0x3B, 2 ) ); // Eat sound

				foreach ( Mobile m in toEat )
				{
					Hits += (m.Hits / 2);
					m.Delete();
				}
			}
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( this.Hits > (this.HitsMax / 4) )
			{
				if ( 0.25 >= Utility.RandomDouble() )
					SpawnBoneKnight( attacker );
			}
			else if ( 0.25 >= Utility.RandomDouble() )
			{
				EatBoneKnight();
			}
		}
	}
}
