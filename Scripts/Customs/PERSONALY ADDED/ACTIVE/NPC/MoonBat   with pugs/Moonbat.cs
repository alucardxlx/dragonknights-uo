using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a Moonbat corpse" )]
	public class Moonbat : BaseCreature
	{
		private ArrayList m_pugs;
		int pugCount = Utility.RandomMinMax( 2, 5 );
		public override bool CanRegenHits{ get{ return true; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public bool RespawnPugs
		{
			get{ return false; }
			set{ if( value ) Spawnpugs(); }
		}

		[Constructable]
		public Moonbat() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Moonbat";
			Hue = 1284;
			Body = 39;
			BaseSoundID = 422;

			SetStr( 18, 30 );
			SetDex( 52, 76 );
			SetInt( 12, 28 );

			SetHits( 55, 75 );
			SetMana( 0 );

			SetDamage( 5, 9 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 9, 18 );

			SetSkill( SkillName.MagicResist, 15.1, 23.0 );
			SetSkill( SkillName.Tactics, 18.1, 26.0 );
			SetSkill( SkillName.Wrestling, 33.1, 37.0 );

			switch ( Utility.Random( 5 ))
			{
				case 0: PackItem( new MoonRapier() ); break;
				case 1: PackItem( new LeatherSkirt() ); break;
				case 2: PackItem( new LeatherNinjaMitts() ); break;
				case 3: PackItem( new LeatherNinjaHood() ); break;
				case 4: PackItem( new LeatherNinjaPants() ); break;
			}

			Fame = 350;
			Karma = -350;

			VirtualArmor = 10;

			Tamable = false;

			m_pugs = new ArrayList();
			Timer m_timer = new PugTimer( this );
			m_timer.Start();
		}

		public override bool OnBeforeDeath()
		{	
			foreach( Mobile m in m_pugs )
			{	
				if( m is Pug && m.Alive && ((Pug)m).ControlMaster != null )
					m.Kill();
			}
			
			return base.OnBeforeDeath();
		}

		public void Spawnpugs()
		{

			Defrag();
			int family = m_pugs.Count;

			if( family >= pugCount )
				return;

			//Say( "family {0}, should be {1}", family, pugCount );

			Map map = this.Map;

			if ( map == null )
				return;

			int hr = (int)((this.RangeHome + 1) / 2);

			for ( int i = family; i < pugCount; ++i )
			{
				Pug pug = new Pug();

				bool validLocation = false;
				Point3D loc = this.Location;

				for ( int j = 0; !validLocation && j < 10; ++j )
				{
					int x = X + Utility.Random( 5 ) - 1;
					int y = Y + Utility.Random( 5 ) - 1;
					int z = map.GetAverageZ( x, y );

					if ( validLocation = map.CanFit( x, y, this.Z, 16, false, false ) )
						loc = new Point3D( x, y, Z );
					else if ( validLocation = map.CanFit( x, y, z, 16, false, false ) )
						loc = new Point3D( x, y, z );
				}

				pug.kin = this;
				pug.Team = this.Team;
				pug.Home = this.Location;
				pug.RangeHome = ( hr > 4 ? 4 : hr );
				
				pug.MoveToWorld( loc, map );
				m_pugs.Add( pug );
			}
		}

		protected override void OnLocationChange( Point3D oldLocation )
		{

			try
			{
				foreach( Mobile m in m_pugs )
				{	
					if( m is Pug && m.Alive && ((Pug)m).ControlMaster == null )
					{
						((Pug)m).Home = this.Location;
					}
				}
			}
			catch{}

			base.OnLocationChange( oldLocation );
		}
		
		public void Defrag()
		{
			for ( int i = 0; i < m_pugs.Count; ++i )
			{
				try
				{
					object o = m_pugs[i];

					Pug pug = o as Pug;

					if ( pug == null || !pug.Alive )
					{
						m_pugs.RemoveAt( i );
						--i;
					}

					else if ( pug.Controlled || pug.IsStabled )
					{
						pug.kin = null;
						m_pugs.RemoveAt( i );
						--i;
					}
				}
				catch{}
			}
		}

		public override void OnDelete()
		{
			Defrag();

			foreach( Mobile m in m_pugs )
			{	
				if( m.Alive && ((Pug)m).ControlMaster == null )
					m.Delete();
			}

			base.OnDelete();
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		public override int Meat{ get{ return 1; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }

		public Moonbat( Serial serial ) : base( serial )
		{
			m_pugs = new ArrayList();
			Timer m_timer = new PugTimer( this );
			m_timer.Start();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize(writer);
			writer.Write((int) 0);
			writer.WriteMobileList( m_pugs, true );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			m_pugs = reader.ReadMobileList();
		}
	}
	[CorpseName( "a pug corpse" )]
	public class Pug : BaseCreature
	{
		public override int Meat{ get{ return 1; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Canine; } }

		private Moonbat m_kin;

		[CommandProperty( AccessLevel.GameMaster )]
		public Moonbat kin
		{
			get{ return m_kin; }
			set{ m_kin = value; }
		}

		[Constructable]
		public Pug() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Pug";
			Body = 17;
			Hue = 0x502;

			SetStr( 37, 47 );
			SetDex( 38, 53 );
			SetInt( 39, 47 );

			SetHits( 17, 42 );
			SetMana( 0 );

			SetDamage( 4, 7 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 10, 15 );

			SetSkill( SkillName.MagicResist, 22.1, 47.0 );
			SetSkill( SkillName.Tactics, 19.2, 31.0 );
			SetSkill( SkillName.Wrestling, 19.2, 46.0 );

			Fame = 100;
			Karma = 100;

			VirtualArmor = 10;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 53.1;
		}

		public override void OnCombatantChange()
		{
			if( Combatant != null && Combatant.Alive && m_kin != null && m_kin.Combatant == null )
				m_kin.Combatant = Combatant;
		}

		public Pug(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
			writer.Write( m_kin );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			m_kin = (Moonbat)reader.ReadMobile();
		}
	}

	public class PugTimer : Timer
	{
		private Moonbat m_from;

		public PugTimer( Moonbat from  ) : base( TimeSpan.FromMinutes( 1 ), TimeSpan.FromMinutes( 20 ) )
		{
			Priority = TimerPriority.OneMinute; 
			m_from = from;
		}

		protected override void OnTick()
		{
			if ( m_from != null && m_from.Alive )
				m_from.Spawnpugs();
			else
				Stop();
		}
	}
}
