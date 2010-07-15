//Created By ABC123 of RunUO
using System;
using Server.Network;
using Server;
using Server.Targeting;
using Server.Items;
using Server.Prompts;

namespace Server.Items
{
	public class ContainerReNameTool : Item
	{
		private int m_Charges;
		[CommandProperty( AccessLevel.GameMaster )]
		public int Charges
		{
			get { return m_Charges; }
			set { m_Charges = value; InvalidateProperties(); }
		}

		[Constructable]
		public ContainerReNameTool() : this(10) { }

		[Constructable]
		public ContainerReNameTool(int charges) : base( 4787 )
		{
			Name = "Container Re-name Tool";
			m_Charges = charges;
			Hue = 1165;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add("Uses Remaining: {0}", Charges);
		}

		public override void OnDoubleClick( Mobile from )
		{
			if(!IsChildOf(from.Backpack)) from.SendMessage( 38,"This must be in your backpack to use it." );
			else if ( m_Charges > 0)
			{
				from.SendMessage( 75, "Target the item you wish to re-name." );
				from.Target = new InternalTarget( this );
			}
			else
			{
				from.SendMessage( 38,"It was out of Uses and it disintegrated." );
				this.Delete();
			}
		}
		private class InternalTarget : Target
		{
			private ContainerReNameTool m_ContainerReNameTool;
			private Item m_engtarg;

			public InternalTarget( ContainerReNameTool engrave ) : base( 1, false, TargetFlags.None )
			{
				m_ContainerReNameTool = engrave;
			}
			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is ContainerReNameTool )
				{
					ContainerReNameTool knife = targeted as ContainerReNameTool;
					if (knife != null)
					{
//						int knifeuses = knife.Charges;
//						m_ContainerReNameTool.Charges += knifeuses;
//						knife.Delete();
						from.SendMessage( 38,"You cant use this on it self!");
					}
				}
				else if ( targeted is CommodityDeedBox )
					from.SendMessage( 38, "You cant use this on Commodity Deed Box!");

				
				// put in else if statements to have it not engrave certain things, like baseweapons, or basearmor, etc etc
//				else if ( targeted is Item )
				else if ( targeted is BaseContainer )
				{
					m_engtarg = (Item)targeted;
					if(!m_engtarg.IsChildOf(from.Backpack)) from.SendMessage( 38, "This must be in your backpack to change its name." );

					else
					{
						from.SendMessage( 75,"What would you like to re-name this item to?" );
						m_ContainerReNameTool.Charges -= 1 ;
						m_ContainerReNameTool.InvalidateProperties();
						from.Prompt = new RenameContPrompt( m_engtarg );
					}
				}
				else from.SendMessage( 38, "You cannot re-name that." );
			}
		}

		public ContainerReNameTool(Serial serial) : base(serial){}
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
			writer.Write( (int) m_Charges );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Charges = reader.ReadInt();
		}
	}
}

namespace Server.Prompts
{
	public class RenameContPrompt : Prompt
	{
		private Item m_engtarg;

		public RenameContPrompt( Item rcont )
		{
			m_engtarg = rcont;
		}
		public override void OnResponse( Mobile from, string text )
		{
			m_engtarg.Name = text;
			from.SendMessage( 75,"You have re-named the item." );
			Effects.SendLocationParticles(EffectItem.Create(from.Location, from.Map, EffectItem.DefaultDuration), 0x376A, 1, 29, 0x47D, 2, 9962, 0);
			Effects.SendLocationParticles(EffectItem.Create(new Point3D(from.X, from.Y, from.Z - 7), from.Map, EffectItem.DefaultDuration), 0x37C4, 1, 29, 0x47D, 2, 9502, 0);
			from.PlaySound(0x212);
			from.PlaySound(0x206);
			
		}
	}
}
