using System; 
using Server;
using Server.Items;

namespace Server.Mobiles 
{ 
	[CorpseName( "a shadow lord corpse" )] 
	public class ShadowLord : BaseCreature
	{ 
		[Constructable] 
		public ShadowLord() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{ 
			Name = "a shadow lord";
			Body = 146;
			Hue = 2406;

			SetStr( 81, 105 );
			SetDex( 191, 215 );
			SetInt( 126, 150 );

			SetHits( 49, 63 );

			SetDamage( 5, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 40 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.EvalInt, 80.2, 100.0 );
			SetSkill( SkillName.Magery, 95.1, 100.0 );
			SetSkill( SkillName.Meditation, 27.5, 50.0 );
			SetSkill( SkillName.MagicResist, 77.5, 100.0 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 20.3, 80.0 );

			PackNecroReg( 10, 14 );

			Fame = 10500;
			Karma = -10500;
			
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Meager );
			AddLoot( LootPack.MedScrolls, 2 );
		}
		public override int TreasureMapLevel{ get{ return Core.AOS ? 2 : 0; } }
		public override bool AlwaysMurderer{ get{ return true; } }
		public override bool BardImmune{ get{ return !Core.SE; } }
		public override bool Unprovokable{ get{ return Core.SE; } }
		public override bool Uncalmable{ get{ return Core.SE; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }

		public void SpawnShadowFiend( Mobile target )
		{
			Map map = this.Map;

			if ( map == null )
				return;

			int newShadowFiend = Utility.RandomMinMax( 4, 9 );

			for ( int i = 0; i < newShadowFiend; ++i )
			{
				ShadowFiend ShadowFiend = new ShadowFiend();

				ShadowFiend.Team = this.Team;
				ShadowFiend.FightMode = FightMode.Closest;

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

				ShadowFiend.MoveToWorld( loc, map );
				ShadowFiend.Combatant = target;
			}
		}

		public void DoSpecialAbility( Mobile target )
		{

			if ( 0.7 >= Utility.RandomDouble() )
				SpawnShadowFiend( target );
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			DoSpecialAbility( defender );

			defender.Damage( Utility.Random( 20, 10 ), this );
			defender.Stam -= Utility.Random( 20, 10 );
			defender.Mana -= Utility.Random( 20, 10 );
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			DoSpecialAbility( attacker );

		}

		public ShadowLord( Serial serial ) : base( serial ) 
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