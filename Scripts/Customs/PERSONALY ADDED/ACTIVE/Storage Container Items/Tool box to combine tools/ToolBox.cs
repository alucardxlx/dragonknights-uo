using System;
using Server;
using Server.Targeting;
using Server.Mobiles;
using Server.Items;
using Server.Prompts;
using Server.Gumps;
using Server.Network;

namespace Server.Items
{
	[FlipableAttribute( 0x9ab, 0xe7c )]
	public class ToolBox : Item
    {
		public int i_S, i_C, i_M, i_L, i_F, i_Co, i_Ti, i_St, i_G, i_A, i_T, i_Ca, i_Sc, i_Pros=0, i_Garg=0;

        private int m_Combines;

        [CommandProperty(AccessLevel.GameMaster)]
        public int Combines
        {
            get { return m_Combines; }
            set { if (value>=0) m_Combines = value; }
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            list.Add(1060584, Combines.ToString()); // uses remaining: ~1_val~
        }

        [Constructable]
        public ToolBox()
            : this(100)
        {
        }

		[Constructable]
		public ToolBox(int combines) : base( 0x9AB ) 
        {
            Name = "Tool House";
            Weight = 5.0;
            Hue = 98;
            m_Combines = combines;
        }
		
		public override void OnDoubleClick( Mobile m )
        {
            if (!m.Alive)
                return;
			if ( !IsChildOf( m.Backpack ) )
                m.SendLocalizedMessage( 1042001 );
			else
                this.NG( m, this );
        }
		
		public ToolBox( Serial serial ) : base( serial )
        {
        }
		
		public void OW( Mobile from )
        {
            if (m_Combines <= 0)
            {
                from.SendMessage("The tool box has used up all its combines. You can not combine anything into it anymore.");
                return;
            }
            from.Target = new InternalTarget( this );
        }

		public void NG( Mobile m, ToolBox box )
        {
            m.SendGump( new ToolBoxGump( m, box ) );
        }
		
		public void AddTool( Mobile m , Item tar, int uses, ToolBox box )
        {
			if ( ( tar is BaseTool || tar is BaseHarvestTool || tar is BaseAxe || tar is ProspectorsTool) && uses > 0 )
            {
				if ( tar is SmithHammer || tar is Tongs )
				{
                    this.i_S += uses;
                    m_Combines--;
                    tar.Consume();
                    box.OW( m );
                }
//Modded from this to 				else if ( tar is TinkerTools || tar is SturdyTinkerTools )
				else if ( tar is TinkerTools )
                {
                    this.i_Ti += uses;
                    m_Combines--;
                    tar.Consume();
                    box.OW( m );
                }
				else if( tar is Skillet || tar is RollingPin || tar is FlourSifter )
				{
                    this.i_Co += uses;
                    m_Combines--;
                    tar.Consume();
                    box.OW( m );
                }
//Modded from this to				else if ( tar is Hammer || tar is DovetailSaw || tar is Saw || tar is DrawKnife || tar is Froe || tar is Inshave || tar is JointingPlane || tar is MouldingPlane || tar is Nails || tar is Scorp || tar is SmoothingPlane || tar is SturdySaw)
				else if ( tar is Hammer || tar is DovetailSaw || tar is Saw || tar is DrawKnife || tar is Froe || tar is Inshave || tar is JointingPlane || tar is MouldingPlane || tar is Nails || tar is Scorp || tar is SmoothingPlane )
				{ 
                    this.i_C += uses;
                    m_Combines--;
                    tar.Consume(); 
                    box.OW( m ); 
                }
				else if ( tar is Shovel || tar is Pickaxe || tar is SturdyPickaxe || tar is SturdyShovel)
                {
                    this.i_M += uses;
                    m_Combines--;
                    tar.Consume();
                    box.OW( m );
                }
//Modded from this to                else if (tar is Hatchet || tar is SturdyHatchet)
                else if (tar is Hatchet)
                {
                    this.i_L += uses;
                    m_Combines--;
                    tar.Consume();
                    box.OW(m);
                }
                else if (tar is Blowpipe)
                { 
                    this.i_G += uses;
                    m_Combines--;
                    tar.Consume(); 
                    box.OW( m ); 
                }
				else if ( tar is MalletAndChisel ) 
                { 
                    this.i_St += uses;
                    m_Combines--;
                    tar.Consume(); 
                    box.OW( m ); 
                }
				else if ( tar is SewingKit ) 
                { 
                    this.i_T += uses;
                    m_Combines--;
                    tar.Consume(); 
                    box.OW( m ); 
                }
//Modded from this to				else if ( tar is FletcherTools || tar is SturdyFletcherTools) 
				else if ( tar is FletcherTools ) 
                {
                    this.i_F += uses;
                    m_Combines--;
                    tar.Consume(); 
                    box.OW( m ); 
                }
				else if ( tar is MortarPestle ) 
                { 
                    this.i_A += uses;
                    m_Combines--;
                    tar.Consume(); 
                    box.OW( m ); 
                }
				else if ( tar is MapmakersPen ) 
                { 
                    this.i_Ca += uses;
                    m_Combines--;
                    tar.Consume(); 
                    box.OW( m ); 
                }
				else if ( tar is ScribesPen ) 
                {
                    this.i_Sc += uses;
                    m_Combines--;
                    tar.Consume(); 
                    box.OW( m );
                }
                else if (tar is ProspectorsTool)
                {
                    this.i_Pros += uses;
                    m_Combines--;
                    tar.Consume();
                    box.OW(m);
                }
                else if (tar is GargoylesPickaxe)
                {
                    this.i_Garg += uses;
                    m_Combines--;
                    tar.Consume();
                    box.OW(m);
                }
                else
                {
                    m.SendMessage( "I do not recognize this type." );
                    box.NG( m, box );
                }
            }
			else
            {
                m.SendMessage( "This is not the appropriate type of item or it has no uses left." );
                box.NG( m, box );
            }
        }
		
		public override void Serialize( GenericWriter writer ) {
			base.Serialize( writer ); writer.Write( (int) 2 );

            writer.Write((int)i_Pros);
            writer.Write((int)i_Garg);
            writer.Write((int)m_Combines);
            writer.Write((int)i_S); writer.Write((int)i_C); writer.Write((int)i_M); writer.Write((int)i_L);
			writer.Write( (int)i_F ); writer.Write( (int)i_Co ); writer.Write( (int)i_Ti );
			writer.Write( (int)i_St ); writer.Write( (int)i_G ); writer.Write( (int)i_A );
			writer.Write( (int)i_T ); writer.Write( (int)i_Ca ); writer.Write( (int)i_Sc ); }
		
		public override void Deserialize( GenericReader reader ) {
			base.Deserialize( reader ); int version = reader.ReadInt();

            switch (version)
            {
                case 2:
                    i_Pros = reader.ReadInt();
                    i_Garg = reader.ReadInt();
                    goto case 1;
                case 1:
                    m_Combines = reader.ReadInt();
                    goto case 0;
                case 0:
                    i_S = reader.ReadInt(); i_C = reader.ReadInt(); i_M = reader.ReadInt(); i_L = reader.ReadInt();
                    i_F = reader.ReadInt(); i_Co = reader.ReadInt(); i_Ti = reader.ReadInt();
                    i_St = reader.ReadInt(); i_G = reader.ReadInt(); i_A = reader.ReadInt();
                    i_T = reader.ReadInt(); i_Ca = reader.ReadInt(); i_Sc = reader.ReadInt();
                    break;
            }
        }
		
		private class InternalTarget : Target 
        { 
            private ToolBox box;			
			public InternalTarget( ToolBox boxa ) : base( 1, false, TargetFlags.None ) 
            { box = boxa; }
			
			protected override void OnTarget( Mobile m, object o ) 
            { 
				if ( o is Item )
                { 
                    Item IT = (Item)o;
					if ( IT.Parent != m.Backpack )
					{
                        m.SendLocalizedMessage( 1042001 );
                        box.NG( m, box );
                    }
					else if ( IT is BaseTool )
					{
                        BaseTool j = (BaseTool)IT;
                        int uses = j.UsesRemaining;
                        box.AddTool( m, j, uses, box );
                    }
					else if ( IT is BaseHarvestTool )
					{
                        BaseHarvestTool j = (BaseHarvestTool)IT;
                        int uses = j.UsesRemaining;
                        box.AddTool( m, j, uses, box );
                    }
//Modded this from to                    else if (IT is Hatchet || IT is SturdyHatchet || IT is Pickaxe || IT is SturdyPickaxe)
                    else if (IT is Hatchet || IT is Pickaxe || IT is SturdyPickaxe)
                    {
                        BaseAxe j = (BaseAxe)IT;
                        int uses = j.UsesRemaining;
                        box.AddTool(m, j, uses, box);
                    }
                    else if (IT is ProspectorsTool)
                    {
                        ProspectorsTool j = (ProspectorsTool)IT;
                        int uses = j.UsesRemaining;
                        box.AddTool(m, j, uses, box);
                    }
                    else if (IT is GargoylesPickaxe)
                    {
                        GargoylesPickaxe j = (GargoylesPickaxe)IT;
                        int uses = j.UsesRemaining;
                        box.AddTool(m, j, uses, box);
                    }
                    else if (IT is BaseAxe)
                    {
                        m.SendMessage("I do not take axe other than pickaxe and gargoyle pickaxe.");
                        box.NG(m, box);
                    } 
                    else
                    {
                        m.SendMessage("I'm sorry but I do not recognize this type");
                        box.NG(m, box);
                    } 
                }
				else 
                { 
                    m.SendMessage( "I'm sorry but I do not recognize this type" ); 
                    box.NG( m, box ); 
                } 
            } 
        } 
    } 
}

namespace Server.Gumps
{
	public class ToolBoxGump : Gump
    {
		PlayerMobile m_user;
        ToolBox i_box;
		
		public ToolBoxGump( Mobile m, ToolBox box ) : base( 55, 65 )
        {
			m_user = (PlayerMobile)m;
            i_box = box;

            m.CloseGump(typeof(ToolBoxGump));
			
			Closable=true; Disposable=true; Dragable=true; Resizable=false;
			AddPage(0);
            AddBackground( 0, 0, 340, 300, 9200 );
            AddLabel( 140, 10, 152, "Tool House (combines left: " + box.Combines.ToString() + ")" );
			
			AddLabel( 30, 45, 0, "Smithy:" );
			AddLabel( 110, 45, 0x480, box.i_S.ToString() );
			if ( box.i_S > 0 )
			AddButton( 10, 50, 2361, 2361, 3, GumpButtonType.Reply, 0 );
			
			AddLabel( 30, 70, 0, "Carpentry:" );
			AddLabel( 110, 70, 0x480, box.i_C.ToString() );
			if ( box.i_C > 0 )
			AddButton( 10, 75, 2361, 2361, 4, GumpButtonType.Reply, 0 );
			
			AddLabel( 30, 95, 0, "Tinkering:" );
			AddLabel( 110, 95, 0x480, box.i_Ti.ToString() );
			if ( box.i_Ti > 0 )
			AddButton( 10, 100, 2361, 2361, 5, GumpButtonType.Reply, 0 );
			
			AddLabel( 30, 120, 0, "Tailoring:" );
			AddLabel( 110, 120, 0x480, box.i_T.ToString() );
			if ( box.i_T > 0 )
			AddButton( 10, 125, 2361, 2361, 6, GumpButtonType.Reply, 0 );
			
			AddLabel( 30, 145, 0, "Lumberjacking:" );
			AddLabel( 110, 145, 0x480, box.i_L.ToString() );
			if ( box.i_L > 0 )
			AddButton( 10, 150, 2361, 2361, 7, GumpButtonType.Reply, 0 );
			
			AddLabel( 30, 170, 0, "Inscription:" );
			AddLabel( 110, 170, 0x480, box.i_Sc.ToString() );
			if ( box.i_Sc > 0 )
			AddButton( 10, 175, 2361, 2361, 8, GumpButtonType.Reply, 0 );
			
			AddLabel( 190, 45, 0, "Mining:" );
			AddLabel( 280, 45, 0x480, box.i_M.ToString() );
			if ( box.i_M > 0 )
			AddButton( 170, 50, 2361, 2361, 9, GumpButtonType.Reply, 0 );
			
			AddLabel( 190, 70, 0, "Cooking:" );
			AddLabel( 280, 70, 0x480, box.i_Co.ToString() );
			if ( box.i_Co > 0 )
			AddButton( 170, 75, 2361, 2361, 10, GumpButtonType.Reply, 0 );
			
			AddLabel( 190, 95, 0, "Fletching:" );
			AddLabel( 280, 95, 0x480, box.i_F.ToString() );
			if ( box.i_F > 0 )
			AddButton( 170, 100, 2361, 2361, 11, GumpButtonType.Reply, 0 );
			
			AddLabel( 190, 120, 0, "Alchemy:" );
			AddLabel( 280, 120, 0x480, box.i_A.ToString() );
			if ( box.i_A > 0 )
			AddButton( 170, 125, 2361, 2361, 12, GumpButtonType.Reply, 0 );
			
			AddLabel( 190, 145, 0, "Stoneworking:" );
			AddLabel( 280, 145, 0x480, box.i_St.ToString() );
			if ( box.i_St > 0 )
			AddButton( 170, 150, 2361, 2361, 13, GumpButtonType.Reply, 0 );
			
			AddLabel( 190, 170, 0, "Cartography:" );
			AddLabel( 280, 170, 0x480, box.i_Ca.ToString() );
			if ( box.i_Ca > 0 )
			AddButton( 170, 175, 2361, 2361, 14, GumpButtonType.Reply, 0 );

            AddLabel(30, 195, 0, "Garg Pickaxe:");
            AddLabel(110, 195, 0x480, box.i_Garg.ToString());
            if (box.i_Garg > 0)
                AddButton(10, 200, 2361, 2361, 15, GumpButtonType.Reply, 0);

            AddLabel(30, 220, 0, "Glass blowing:");
            AddLabel(110, 220, 0x480, box.i_G.ToString());
            if (box.i_G > 0)
                AddButton(10, 225, 2361, 2361, 17, GumpButtonType.Reply, 0);
        
            AddLabel(190, 195, 0, "Prospect Tool:");
            AddLabel(280, 195, 0x480, box.i_Pros.ToString());
            if (box.i_Pros > 0)
                AddButton(170, 200, 2361, 2361, 16, GumpButtonType.Reply, 0);
			
			AddLabel( 200, 270, 0, "Combine Item" );
			
			AddButton( 10, 270, 2073, 2072, 0, GumpButtonType.Reply, 0 );
			AddButton( 280, 270, 2143, 2142, 2, GumpButtonType.Reply, 0 );
		}
		
		public override void OnResponse( NetState state, RelayInfo info )
		{
            PlayerMobile m = m_user; ToolBox box = i_box; BaseTool tool; BaseHarvestTool toola; Hatchet toolh; Container pack = m.Backpack;

			switch ( info.ButtonID ) {
				default: { break; }
				case 2: { box.OW( m ); break; }
				case 3: { tool = new Tongs(); tool.UsesRemaining = box.i_S; pack.DropItem( tool ); box.i_S = 0; break; }
				case 4: { tool = new MouldingPlane(); tool.UsesRemaining = box.i_C; pack.DropItem( tool ); box.i_C = 0; break; }
				case 5: { tool = new TinkerTools(); tool.UsesRemaining = box.i_Ti; pack.DropItem( tool ); box.i_Ti = 0; break; }
				case 6: { tool = new SewingKit(); tool.UsesRemaining = box.i_T; pack.DropItem( tool ); box.i_T = 0; break; }
				case 7: { toolh = new Hatchet(); toolh.UsesRemaining = box.i_L; pack.DropItem( toolh ); box.i_L = 0; break; }
				case 8: { tool = new ScribesPen(); tool.UsesRemaining = box.i_Sc; pack.DropItem( tool ); box.i_Sc = 0; break; }
				case 9: { toola = new Shovel(); toola.UsesRemaining = box.i_M; pack.DropItem( toola ); box.i_M = 0; break; }
				case 10: { tool = new Skillet(); tool.UsesRemaining = box.i_Co; pack.DropItem( tool ); box.i_Co = 0; break; }
				case 11: { tool = new FletcherTools(); tool.UsesRemaining = box.i_F; pack.DropItem( tool ); box.i_F = 0; break; }
				case 12: { tool = new MortarPestle(); tool.UsesRemaining = box.i_A; pack.DropItem( tool ); box.i_A = 0; break; }
				case 13: { tool = new MalletAndChisel(); tool.UsesRemaining = box.i_St; pack.DropItem( tool ); box.i_St = 0; break; }
				case 14: { tool = new MapmakersPen(); tool.UsesRemaining = box.i_Ca; pack.DropItem( tool ); box.i_Ca = 0; break; }
                case 15: { GargoylesPickaxe item = new GargoylesPickaxe(); item.UsesRemaining = box.i_Garg; pack.DropItem(item); box.i_Garg = 0; break; }
                case 16: { ProspectorsTool item = new ProspectorsTool(); item.UsesRemaining = box.i_Pros; pack.DropItem(item); box.i_Pros = 0; break; }
                case 17: { Blowpipe item = new Blowpipe(); item.UsesRemaining = box.i_G; pack.DropItem(item); box.i_G = 0; break; }
            } 
            if ( info.ButtonID > 2 ) { box.NG( m, box ); 
            } 
        } 
    } 
}
