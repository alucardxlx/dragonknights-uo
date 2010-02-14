using System;
using System.Collections;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a bermuda titan's corpse" )]
	public class BermudaTitan : BaseCreature
	{
		[Constructable]
		public BermudaTitan() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.6, 1.2 )
		{
			Name = "a bermuda titan";
			Body = 312;
			BaseSoundID = 0x451;

			Hue = 141;

			SetStr( 756, 780 );
			SetDex( 226, 245 );
			SetInt( 26, 40 );

			SetHits( 481, 540 );
			SetMana( 0 );

			SetDamage( 10, 23 );

			SetDamageType( ResistanceType.Physical, 60 );
			SetDamageType( ResistanceType.Poison, 100 );

			SetResistance( ResistanceType.Physical, 60 );
			SetResistance( ResistanceType.Fire, 50 );
			SetResistance( ResistanceType.Poison, 100 );

			SetSkill( SkillName.MagicResist, 90.1, 95.0 );
			SetSkill( SkillName.Tactics, 70.1, 85.0 );
			SetSkill( SkillName.Wrestling, 65.1, 80.0 );

			Fame = 8000;
			Karma = -8000;

			VirtualArmor = 28;

			CanSwim = true;
			CantWalk = true;

			if( Utility.RandomDouble() < .25 )
				PackItem( new MessageInABottle() );

		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average, 2 );
		}

		public override bool BardImmune{ get{ return !Core.AOS; } }
		public override Poison PoisonImmune{ get{ return Poison.Greater; } }

		public BermudaTitan( Serial serial ) : base( serial )
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

		public void SpawnBermudaTitanTentacles( Mobile m )
		{
			Map map = this.Map;

			if ( map == null )
				return;

			BermudaTitanTentacles spawned = new BermudaTitanTentacles();

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

		public void EatBermudaTitanTentacles()
		{
			ArrayList toEat = new ArrayList();
  
			foreach ( Mobile m in this.GetMobilesInRange( 2 ) )
			{
				if ( m is BermudaTitanTentacles )
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
					SpawnBermudaTitanTentacles( attacker );
			}
			else if ( 0.25 >= Utility.RandomDouble() )
			{
				EatBermudaTitanTentacles();
			}
		}
	}
}
