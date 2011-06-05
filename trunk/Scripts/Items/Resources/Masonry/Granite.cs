using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public abstract class BaseGranite : Item
	{
		private CraftResource m_Resource;

		[CommandProperty( AccessLevel.GameMaster )]
		public CraftResource Resource
		{
			get{ return m_Resource; }
			set{ m_Resource = value; InvalidateProperties(); }
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

			writer.Write( (int) m_Resource );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				case 0:
				{
					m_Resource = (CraftResource)reader.ReadInt();
					break;
				}
			}

			if ( version < 1 )
				Stackable = Core.ML;
		}

		public override double DefaultWeight
		{
			get { return Core.ML ? 1.0 : 10.0; } // Pub 57
		}

//Modded from - 		public BaseGranite( CraftResource resource ) : base( 0x1779 ) - to what it is below
		public BaseGranite( CraftResource resource, int amount ) : base( 0x1779 )
		{
			Stackable = true;//I ADDED
			Amount = amount;//I ADDED
			Hue = CraftResources.GetHue( resource );
			Stackable = Core.ML;

			m_Resource = resource;
		}

		public BaseGranite( Serial serial ) : base( serial )
		{
		}

		public override int LabelNumber{ get{ return 1044607; } } // high quality granite

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( !CraftResources.IsStandard( m_Resource ) )
			{
				int num = CraftResources.GetLocalizationNumber( m_Resource );

				if ( num > 0 )
					list.Add( num );
				else
					list.Add( CraftResources.GetName( m_Resource ) );
			}
		}
	}

	public class Granite : BaseGranite
	{
		[Constructable]
		public Granite() : this( 1 )
		{
		}

		[Constructable]
		public Granite( int amount ) : base( CraftResource.Iron, amount )
		{
		}

		public Granite( Serial serial ) : base( serial )
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
		
		
		
//		public override BaseGranite GetGranite()
//		{
//			return new Granite();
//		}
	}


	public class DullCopperGranite : BaseGranite
	{
		[Constructable]
		public DullCopperGranite() : this( 1 )
		{
		}

		[Constructable]
		public DullCopperGranite( int amount ) : base( CraftResource.DullCopper, amount )
		{
		}

		public DullCopperGranite( Serial serial ) : base( serial )
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
//		public override BaseGranite GetGranite()
//		{
//			return new DullCopperGranite();
//		}
	}

	public class ShadowIronGranite : BaseGranite
	{
		[Constructable]
		public ShadowIronGranite() : this( 1 )
		{
		}

		[Constructable]
		public ShadowIronGranite( int amount ) : base( CraftResource.ShadowIron, amount )
		{
		}

		public ShadowIronGranite( Serial serial ) : base( serial )
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
//		public override BaseGranite GetGranite()
//		{
//			return new ShadowIronGranite();
//		}
	}

	public class CopperGranite : BaseGranite
	{
		[Constructable]
		public CopperGranite() : this( 1 )
		{
		}

		[Constructable]
		public CopperGranite( int amount ) : base( CraftResource.Copper, amount )
		{
		}

		public CopperGranite( Serial serial ) : base( serial )
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
//		public override BaseGranite GetGranite()
//		{
//			return new CopperGranite();
//		}
	}

	public class BronzeGranite : BaseGranite
	{
		[Constructable]
		public BronzeGranite() : this( 1 )
		{
		}

		[Constructable]
		public BronzeGranite( int amount ) : base( CraftResource.Bronze, amount )
		{
		}

		public BronzeGranite( Serial serial ) : base( serial )
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
//		public override BaseGranite GetGranite()
//		{
//			return new BronzeGranite();
//		}
	}

	public class GoldGranite : BaseGranite
	{
		[Constructable]
		public GoldGranite() : this( 1 )
		{
		}

		[Constructable]
		public GoldGranite( int amount ) : base( CraftResource.Gold, amount )
		{
		}

		public GoldGranite( Serial serial ) : base( serial )
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
//		public override BaseGranite GetGranite()
//		{
//			return new GoldGranite();
//		}
	}

	public class AgapiteGranite : BaseGranite
	{
		[Constructable]
		public AgapiteGranite() : this( 1 )
		{
		}

		[Constructable]
		public AgapiteGranite( int amount ) : base( CraftResource.Agapite, amount )
		{
		}

		public AgapiteGranite( Serial serial ) : base( serial )
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
//		public override BaseGranite GetGranite()
//		{
//			return new AgapiteGranite();
//		}
	}

	public class VeriteGranite : BaseGranite
	{
		[Constructable]
		public VeriteGranite() : this( 1 )
		{
		}

		[Constructable]
		public VeriteGranite( int amount ) : base( CraftResource.Verite, amount )
		{
		}

		public VeriteGranite( Serial serial ) : base( serial )
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
//		public override BaseGranite GetGranite()
//		{
//			return new VeriteGranite();
//		}
	}

	public class ValoriteGranite : BaseGranite
	{
		[Constructable]
		public ValoriteGranite() : this( 1 )
		{
		}

		[Constructable]
		public ValoriteGranite( int amount ) : base( CraftResource.Valorite, amount )
		{
		}

		public ValoriteGranite( Serial serial ) : base( serial )
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
//		public override BaseGranite GetGranite()
//		{
//			return new Granite();
//		}
	}
}
	
	
