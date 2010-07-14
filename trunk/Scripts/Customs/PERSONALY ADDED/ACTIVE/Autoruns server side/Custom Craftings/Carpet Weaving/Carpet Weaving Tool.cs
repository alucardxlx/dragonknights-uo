using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	[FlipableAttribute( 0x1420, 0x1421 )]
	public class CarpetWeaverTool : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefCarpetWeaving.CraftSystem; } }

		[Constructable]
		public CarpetWeaverTool() : base( 0x1420 )
		{
			Name = "Carpet Weaver Tool";
			Weight = 2.0;
		}

		[Constructable]
		public CarpetWeaverTool( int uses ) : base( uses, 0x1420 )
		{
			Weight = 2.0;
		}

		public CarpetWeaverTool( Serial serial ) : base( serial )
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
