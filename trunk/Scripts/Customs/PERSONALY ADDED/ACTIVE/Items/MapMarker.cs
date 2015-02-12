/* Created by: AlphaDragon 10/17/2013 */
using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Gumps;
using Server.Network;

namespace Server.Items
{
	public class MapMarker : Item
	{
		private String m_MapMarkerNameAddon;
		private String m_MapMarkerNote;
		
		[CommandProperty(AccessLevel.GameMaster)]
		public String MapMarkerNote{get { return m_MapMarkerNote; }set { m_MapMarkerNote = value; }}
		
		[CommandProperty(AccessLevel.GameMaster)]
		public String MapMarkerNameAddon{get { return m_MapMarkerNameAddon; }set { m_MapMarkerNameAddon = value; }}

		[Constructable]
		public MapMarker() : base( 0x23a3 )
		{
			Name = "MapMarker";
			Visible = false;
			Movable = false;			
		}
		
		public override bool HandlesOnMovement { get { return true; } }

        public override void OnMovement(Mobile from, Point3D oldLocation)
        {
        	if ( from.AccessLevel >= AccessLevel.Counselor)
        	{
        		from.CloseGump(typeof(MapMarkerGump));
        	}
        }
        
        public override void OnDoubleClick(Mobile from)
        {
        	if ( from.AccessLevel >= AccessLevel.Counselor)
        		
        	{        	from.CloseGump(typeof(MapMarkerGump));
        		from.SendGump(new MapMarkerGump(from, this));
        	}
        }

        public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 3000265, "{0}", m_MapMarkerNote );
        }
        
		public MapMarker( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.Write( (string) m_MapMarkerNameAddon );
			writer.Write( (string) m_MapMarkerNote );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_MapMarkerNameAddon = reader.ReadString();
			m_MapMarkerNote = reader.ReadString();
		}
	}
}

namespace Server.Gumps
{
    public class MapMarkerGump : Gump
    {
    	private Mobile m_From;
    	private MapMarker m_MapMarker;
    	
    	public MapMarkerGump(Mobile from, MapMarker mapmarker) : base(500, 350)
    	{
    		m_From = from;
    		m_MapMarker = mapmarker;
    		
    		this.Closable = true;
    		this.Disposable = true;
    		this.Dragable = true;
    		this.Resizable = false;
    		AddPage(0);
    		AddBackground(104, 11, 275, 291, 2620);
    		AddImage(-38, -38, 666);//garg statue
    		AddItem(112, 20, 9123);//drag logo
    		AddLabel(188, 17, 1160, @"MapMarker Viewer");
    		AddButton(359, 19, 3, 4, 0, GumpButtonType.Reply, 0);//X  close button
    		
    		AddLabel(202, 39, 1160, @"Name Notation:");
    		AddBackground(158, 61, 200, 20, 9300);
    		AddTextEntry(158, 61, 200, 20, 0, 0, @mapmarker.MapMarkerNameAddon, 22);
    		
    		AddLabel(212, 88, 1160, @"Description:");
    		AddBackground(110, 110, 264, 150, 9300);
    		AddTextEntry(120, 110, 243, 140, 0, 1, @mapmarker.MapMarkerNote);
    		
    		AddButton(135, 265, 9904, 9903, 1, GumpButtonType.Reply, 0);//Save
    		AddLabel(170, 266, 1152, @"Save Changes");
    	}

    	public override void OnResponse(NetState sender, RelayInfo info)
    	{
    		Mobile from = sender.Mobile;

    		TextRelay entry0 = info.GetTextEntry(0);
    		string text0 = (entry0 == null ? "None" : entry0.Text.Trim());
    		
    		TextRelay entry1 = info.GetTextEntry(1);
    		string text1 = (entry1 == null ? "None" : entry1.Text.Trim());

    		if (info.ButtonID ==  0)//close
    		{
    			from.CloseGump(typeof(MapMarkerGump));
    		}

    		if (info.ButtonID ==  1)//edit
    		{
    			m_MapMarker.MapMarkerNameAddon = text0;
            	m_MapMarker.MapMarkerNote = text1;
            	m_MapMarker.Name = ("MapMarker - " + m_MapMarker.MapMarkerNameAddon);
            	
            	from.CloseGump(typeof(MapMarkerGump));
            	from.SendGump(new MapMarkerGump(from, m_MapMarker));
            }
        }
    }
}