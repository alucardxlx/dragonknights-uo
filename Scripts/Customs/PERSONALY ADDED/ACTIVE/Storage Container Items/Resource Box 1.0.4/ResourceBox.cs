/***************************************************************************/
/*			ResourceBox.cs | ResourceBoxGump.cs | StorageTypes.cs          */
/*							Created by A_Li_N                              */
/*				Credits :                                                  */
/*						Original Gump Layout - Lysdexic                    */
/*						Hashtable help - UOT and daat99                    */
/***************************************************************************/

using System;
using System.Collections;
using Server;
using Server.Mobiles;
using Server.Gumps;
using Server.Network;

namespace Server.Items
{
	[FlipableAttribute( 0xE41, 0xE40 )]
	public class ResourceBox : BaseContainer
	{
		private static Type[][] m_AllTypes = new Type[][]
		{
			StorageTypes.Boards,
			StorageTypes.Gems,
			StorageTypes.Granites,
			StorageTypes.Ingots,
			StorageTypes.Leathers,
			StorageTypes.Logs,
			StorageTypes.Misc,
			StorageTypes.Reagents,
			StorageTypes.Scales,
		};
		public Type[][] AllTypes{ get{ return m_AllTypes; } }

		private Hashtable m_Resources;
		public Hashtable Resources{ get{ return m_Resources; } }
		
		private DarkYarn m_Yarn;

		[Constructable]
		public ResourceBox() : base( 0xE41 )
		{
			Movable = true;
			Weight = 100.0;
			Hue = 0x488;
			Name = "Resource Box";

			m_Resources = new Hashtable();
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( Movable )
			{
				from.SendMessage( "You haven't locked it down!" );
				return false;
			}
			else if ( dropped is PotionKeg && ((PotionKeg)dropped).Held > 0 )
			{
				from.SendMessage("You can add only empty potion kegs.");
				return false;
			}
			else if ( dropped is LightYarn || dropped is LightYarnUnraveled )
			{
				m_Yarn = new DarkYarn();
				m_Yarn.Amount = dropped.Amount;

				return TryAdd( m_Yarn );
			}

			return TryAdd( dropped );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( Movable )
			{
				from.SendMessage( "You haven't locked it down!" );
				return;
			}

			if ( !from.InRange( GetWorldLocation(), 2 ) )
				from.LocalOverheadMessage( Network.MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			else
				from.SendGump( new ResourceBoxGump( from, this, ResourceBoxGump.Pages.Start ) );
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			AddNameProperty( list );
		}

		public bool TryAdd( Item item )
		{
			foreach( Type[] cat in m_AllTypes )
			{
				foreach( Type type in cat )
				{
					if( item.GetType() == type )
					{
						if( m_Resources.ContainsKey( type ) && (int)m_Resources[type] + item.Amount >= 100000 )
						{
							this.PublicOverheadMessage( MessageType.Whisper, 0, false, "I cannot hold more of that resource!" );
							return false;
						}

						AddResource( type, item.Amount );
						this.PublicOverheadMessage( MessageType.Whisper, 0, false, "Resource Added." );
						item.Delete();
						return true;
					}
				}
			}
			this.PublicOverheadMessage( MessageType.Whisper, 0, false, "I don't hold that resource!" );
			return false;
		}

		public void AddResource( Type type, int amount )
		{
			if( m_Resources == null )
			{
				m_Resources = new Hashtable();
				m_Resources.Add( type, amount );
				return;
			}

			if( m_Resources.ContainsKey( type ) )
			{
				m_Resources[type] = (int)m_Resources[type] + amount;
				return;
			}

			else
				m_Resources.Add( type, amount );
		}

		public void ExtractResource( Mobile from, Type type )
		{
			int m_Amount = 1;

			if( !m_Resources.ContainsKey(type) )
			{
				this.PublicOverheadMessage( MessageType.Whisper, 0, false, "I do not have that resource!" );
				return;
			}

			if( (int)m_Resources[type] >= 100 )
				m_Amount = 100;
			else if( (int)m_Resources[type] >= 10 && (int)m_Resources[type] < 100 )
				m_Amount = 10;

			for( int i = 0; i < StorageTypes.Granites.Length; i++ )
			{
				if( type == StorageTypes.Granites[i] )
					m_Amount = 1;
			}

			if( ((int)m_Resources[type] - m_Amount) >= 0 )
			{
				m_Resources[type] = (int)m_Resources[type] - m_Amount;
				Item toGive;
				toGive = Activator.CreateInstance( type ) as Item;
				toGive.Amount = m_Amount;
				from.AddToBackpack( toGive );
			}
		}


		public ResourceBox( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 ); // version

			writer.Write( m_Resources.Count );
			foreach( DictionaryEntry de in m_Resources )
			{
				writer.Write( ((Type)de.Key).Name );
				writer.Write( (int)de.Value );
			}
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			m_Resources = new Hashtable();


			switch( version )
			{
				case 0:
				{
					int count = reader.ReadInt();
					for (int i=0; i < count; i++)
					{
						Type type = ScriptCompiler.FindTypeByName(reader.ReadString());
						if( type == null )
						{
							int bad = reader.ReadInt();
							continue;
						}
						m_Resources.Add( type, reader.ReadInt() );
					}
					break;
				}
			}
		}
	}
}
