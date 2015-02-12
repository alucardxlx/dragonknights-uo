using System; 
using Server.Items; 
using Server.Mobiles; 

namespace Server.Engines.Craft 
{ 
	public class DefMasonry : CraftSystem 
	{ 
		public override SkillName MainSkill 
		{ 
			get{ return SkillName.Carpentry; } 
		} 

		public override int GumpTitleNumber 
		{ 
			get{ return 1044500; } // <CENTER>MASONRY MENU</CENTER> 
		} 

		private static CraftSystem m_CraftSystem; 

		public static CraftSystem CraftSystem 
		{ 
			get 
			{ 
				if ( m_CraftSystem == null ) 
					m_CraftSystem = new DefMasonry(); 

				return m_CraftSystem; 
			} 
		} 

		public override double GetChanceAtMin( CraftItem item ) 
		{ 
			return 0.0; // 0% 
		} 

		private DefMasonry() : base( 1, 1, 1.25 )// base( 1, 2, 1.7 ) 
		{ 
		} 

		public override bool RetainsColorFrom( CraftItem item, Type type )
		{
			return true;
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if( tool == null || tool.Deleted || tool.UsesRemaining < 0 )
				return 1044038; // You have worn out your tool!
			else if ( !BaseTool.CheckTool( tool, from ) )
				return 1048146; // If you have a tool equipped, you must use that tool.
			else if ( !(from is PlayerMobile && ((PlayerMobile)from).Masonry && from.Skills[SkillName.Carpentry].Base >= 100.0) )
				return 1044633; // You havent learned stonecraft.
			else if ( !BaseTool.CheckAccessible( tool, from ) )
				return 1044263; // The tool must be on your person to use.

			return 0;
		} 

		public override void PlayCraftEffect( Mobile from ) 
		{ 
			// no effects
			if ( from.Body.Type == BodyType.Human && !from.Mounted ) 
				from.Animate( 9, 5, 1, true, false, 0 ); 
			new InternalTimer( from ).Start(); 
		} 

		// Delay to synchronize the sound with the hit on the anvil 
		private class InternalTimer : Timer 
		{ 
			private Mobile m_From; 

			public InternalTimer( Mobile from ) : base( TimeSpan.FromSeconds( 0.7 ) ) 
			{ 
				m_From = from; 
			} 

			protected override void OnTick() 
			{ 
				m_From.PlaySound( 0x23D ); 
			} 
		} 

		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item ) 
		{ 
			if ( toolBroken ) 
				from.SendLocalizedMessage( 1044038 ); // You have worn out your tool 

			if ( failed ) 
			{ 
				if ( lostMaterial ) 
					return 1044043; // You failed to create the item, and some of your materials are lost. 
				else 
					return 1044157; // You failed to create the item, but no materials were lost. 
			} 
			else 
			{ 
				if ( quality == 0 ) 
					return 502785; // You were barely able to make this item.  It's quality is below average. 
				else if ( makersMark && quality == 2 ) 
					return 1044156; // You create an exceptional quality item and affix your maker's mark. 
				else if ( quality == 2 ) 
					return 1044155; // You create an exceptional quality item. 
				else             
					return 1044154; // You create the item. 
			} 
		} 

		public override void InitCraftList() 
		{
//I ADDED
			if( Core.ML )
			{//Items you add here dont forget to add the ITEMID to the clay tool!
				int index = AddCraft( typeof( ClayBlock ), "Clay", "Clay Block", 0.0, 140.0, typeof( ClayMud ), "Clay Mud", 50, "You do not have enough clay mud to make that item." );
				index = AddCraft( typeof( ClayBrick ), "Clay", "Clay Brick", 50.0, 140.0, typeof( ClayBlock ), "Clay Block", 100, "You do not have enough clay blocks to make that item." );				
				index = AddCraft( typeof( ClayModernPlanter0x44f1 ), "Clay", "Clay Modern Planter", 60.0, 140.0, typeof( ClayBrick ), "Clay Brick", 100, "You do not have enough clay bricks to make that item." );				
				index = AddCraft( typeof( ClayGrecianPlanter0x44f0 ), "Clay", "Clay Grecian Planter", 65.0, 140.0, typeof( ClayBrick ), "Clay Brick", 100, "You do not have enough clay bricks to make that item." );
				index = AddCraft( typeof( ClayGargishTraditionalVase0x42b2 ), "Clay", "Clay Gargish Traditional Vase", 70.0, 140.0, typeof( ClayBrick ), "Clay Brick", 100, "You do not have enough clay bricks to make that item." );
				index = AddCraft( typeof( ClayGargishBentasVase0x42b3 ), "Clay", "Clay Gargish Bentas Vase", 75.0, 140.0, typeof( ClayBrick ), "Clay Brick", 100, "You do not have enough clay bricks to make that item." );				
				index = AddCraft( typeof( ClayBlackCatStatue0x4688 ), "Clay", "Clay Black Cat Statue", 80.0, 140.0, typeof( ClayBrick ), "Clay Brick", 100, "You do not have enough clay bricks to make that item." );				
				index = AddCraft( typeof( ClayGargishVigilanceTotemStatue0x42bc ), "Clay", "Clay Gargish Vigilance Totem Statue", 85.0, 140.0, typeof( ClayBrick ), "Clay Brick", 100, "You do not have enough clay bricks to make that item." );				
				index = AddCraft( typeof( ClayGargishKnowledgeTotemStatue0x42c0 ), "Clay", "Clay Gargish Knowledge Totem Statue", 90.0, 140.0, typeof( ClayBrick ), "Clay Brick", 100, "You do not have enough clay bricks to make that item." );				
				index = AddCraft( typeof( ClayGargishProtectiveTotemStatue0x42bb ), "Clay", "Clay Gargish Protective Totem Statue", 95.0, 140.0, typeof( ClayBrick ), "Clay Brick", 100, "You do not have enough clay bricks to make that item." );				
				index = AddCraft( typeof( ClayGargishWarriorStatue0x42c2 ), "Clay", "Clay Gargish Warrior Statue", 95.0, 140.0, typeof( ClayBrick ), "Clay Brick", 100, "You do not have enough clay bricks to make that item." );
				index = AddCraft( typeof( ClayGargoyleStatue0x42c5 ), "Clay", "Clay Gargoyle Statue", 100.0, 140.0, typeof( ClayBrick ), "Clay Brick", 100, "You do not have enough clay bricks to make that item." );
				index = AddCraft( typeof( ClayGargishMemorialStatue0x42c3 ), "Clay", "Clay Gargish Memorial Statue", 114.0, 140.0, typeof( ClayBrick ), "Clay Brick", 100, "You do not have enough clay bricks to make that item." );
				index = AddCraft( typeof( ClayMedusaStatue0x40bc ), "Clay", "Clay Medusa Statue", 119.0, 140.0, typeof( ClayBrick ), "Clay Brick", 100, "You do not have enough clay bricks to make that item." );
				index = AddCraft( typeof( ClayShadowStatue0x364b ), "Clay", "Clay Shadow Statue", 124.0, 140.0, typeof( ClayBrick ), "Clay Brick", 100, "You do not have enough clay bricks to make that item." );
				index = AddCraft( typeof( AlchemicalSymbolClay0x181d ), "Clay", "Clay Alchemical SymBol 01", 93.75, 140.0, typeof( ClayBrick ), "Clay Brick", 100, "You do not have enough clay bricks to make that item." );
				index = AddCraft( typeof( AlchemicalSymbolClay0x181E ), "Clay", "Clay Alchemical SymBol 02", 93.75, 140.0, typeof( ClayBrick ), "Clay Brick", 100, "You do not have enough clay bricks to make that item." );
				index = AddCraft( typeof( AlchemicalSymbolClay0x181F ), "Clay", "Clay Alchemical SymBol 03", 93.75, 140.0, typeof( ClayBrick ), "Clay Brick", 100, "You do not have enough clay bricks to make that item." );
				index = AddCraft( typeof( AlchemicalSymbolClay0x1820 ), "Clay", "Clay Alchemical SymBol 04", 93.75, 140.0, typeof( ClayBrick ), "Clay Brick", 100, "You do not have enough clay bricks to make that item." );
				index = AddCraft( typeof( AlchemicalSymbolClay0x1821 ), "Clay", "Clay Alchemical SymBol 05", 93.75, 140.0, typeof( ClayBrick ), "Clay Brick", 100, "You do not have enough clay bricks to make that item." );
				index = AddCraft( typeof( AlchemicalSymbolClay0x1822 ), "Clay", "Clay Alchemical SymBol 06", 93.75, 140.0, typeof( ClayBrick ), "Clay Brick", 100, "You do not have enough clay bricks to make that item." );
				index = AddCraft( typeof( AlchemicalSymbolClay0x1823 ), "Clay", "Clay Alchemical SymBol 07", 93.75, 140.0, typeof( ClayBrick ), "Clay Brick", 100, "You do not have enough clay bricks to make that item." );
				index = AddCraft( typeof( AlchemicalSymbolClay0x1824 ), "Clay", "Clay Alchemical SymBol 08", 93.75, 140.0, typeof( ClayBrick ), "Clay Brick", 100, "You do not have enough clay bricks to make that item." );
				index = AddCraft( typeof( AlchemicalSymbolClay0x1825 ), "Clay", "Clay Alchemical SymBol 09", 93.75, 140.0, typeof( ClayBrick ), "Clay Brick", 100, "You do not have enough clay bricks to make that item." );
				index = AddCraft( typeof( AlchemicalSymbolClay0x1826 ), "Clay", "Clay Alchemical SymBol 10", 93.75, 140.0, typeof( ClayBrick ), "Clay Brick", 100, "You do not have enough clay bricks to make that item." );
				index = AddCraft( typeof( AlchemicalSymbolClay0x1827 ), "Clay", "Clay Alchemical SymBol 11", 93.75, 140.0, typeof( ClayBrick ), "Clay Brick", 100, "You do not have enough clay bricks to make that item." );
				index = AddCraft( typeof( AlchemicalSymbolClay0x1828 ), "Clay", "Clay Alchemical SymBol 12", 93.75, 140.0, typeof( ClayBrick ), "Clay Brick", 100, "You do not have enough clay bricks to make that item." );
//				index = AddCraft( typeof( PedestalWithCrystal0x2fd4 ), "Crystals", "Pedestal With Crystal", 100.0, 140.0, typeof( Granite ), 1044514, 100, 1044513 );
//				AddRes(index, typeof(GammaCrystalFragment), "Gamma Crystal Fragments", 100, "You do not have enough gamma crystal fragments to make that item.");
//				
//				index = AddCraft( typeof( CrystalBrazier0x35ef ), "Crystals", "Crystal Brazer", 100.0, 140.0, typeof( GammaCrystal ), "Gamma Crystal", 100, "You do not have enough gamma crystal to make that item." );
//				AddRes(index, typeof(GammaCrystalFragment), "Gamma Crystal Fragments", 100, "You do not have enough gamma crystal fragments to make that item.");
//				AddRes(index, typeof(Sapphire), "Sapphire", 100, "You do not have enough sapphire to make that item.");
//
//				index = AddCraft( typeof( CrystalStatue0x35fc ), "Crystals", "Crystal Statue", 114.0, 140.0, typeof( GammaCrystal ), "Gamma Crystal", 100, "You do not have enough gamma crystal to make that item." );
//				AddRes(index, typeof(GammaCrystalFragment), "Gamma Crystal Fragments", 100, "You do not have enough gamma crystal fragments to make that item.");
//				AddRes(index, typeof(Sapphire), "Sapphire", 100, "You do not have enough sapphire to make that item.");
//				
//				index = AddCraft( typeof( SmallSoulForge0x44c7 ), "Crystals", "Small Soul Forge", 119.0, 140.0, typeof( GammaCrystal ), "Gamma Crystal", 100, "You do not have enough gamma crystal to make that item." );
//				AddRes(index, typeof(GammaCrystalFragment), "Gamma Crystal Fragments", 100, "You do not have enough gamma crystal fragments to make that item.");
//				AddRes(index, typeof(Sapphire), "Sapphire", 100, "You do not have enough sapphire to make that item.");
//				AddRes(index, typeof(IronIngot), 1044036, 100, 1044037);
//				
//				index = AddCraft( typeof( CrystalThrone0x35ed ), "Crystals", "Crystal Throne", 124.0, 140.0, typeof( GammaCrystal ), "Gamma Crystal", 100, "You do not have enough gamma crystal to make that item." );
			}
//I ADDED FIN
			// Decorations
			AddCraft( typeof( Vase ), 1044501, 1022888, 52.5, 102.5, typeof( Granite ), 1044514, 1, 1044513 );
			AddCraft( typeof( LargeVase ), 1044501, 1022887, 52.5, 102.5, typeof( Granite ), 1044514, 3, 1044513 );
			
//I ADDED
			AddCraft( typeof( DecorationSmallVase ), 1044501, "Decoration Small Vase", 86.7, 140.0, typeof( Granite ), 1044514, 1, 1044513 );
			AddCraft( typeof( DecorationLargeVase ), 1044501, "Decoration Large Vase", 86.7, 140.0, typeof( Granite ), 1044514, 3, 1044513 );
			AddCraft( typeof( StoneGargoyleVase0x4042 ), 1044501, "Decoration Gargoyle Vase (Turnable)", 86.7, 140.0, typeof( Granite ), 1044514, 25, 1044513 );
			AddCraft( typeof( StoneAncientStonePlanter0x44ef ), 1044501, "Ancient Stone Planter", 86.7, 140.0, typeof( Granite ), 1044514, 3, 1044513 );
			AddCraft( typeof( StoneStatue0x48a8 ), 1044501, "Dragon Head Statue", 86.7, 140.0, typeof( Granite ), 1044514, 25, 1044513 );
			AddCraft( typeof( StonePedestal0x1223 ), 1044501, "Stone Pedestal", 86.7, 140.0, typeof( Granite ), 1044514, 25, 1044513 );
//I ADDED FIN			
			if( Core.SE )
			{
				int index = AddCraft( typeof( SmallUrn ), 1044501, 1029244, 82.0, 132.0, typeof( Granite ), 1044514, 3, 1044513 );
				SetNeededExpansion( index, Expansion.SE );
//I ADDED
				index = AddCraft( typeof( Urn2Artifact ), 1044501, "medium urn", 86.7, 140.0, typeof( Granite ), 1044514, 3, 1044513 );
				SetNeededExpansion( index, Expansion.SE );
				index = AddCraft( typeof( AncientUrn ), 1044501, "large urn", 86.7, 140.0, typeof( Granite ), 1044514, 3, 1044513 );
				SetNeededExpansion( index, Expansion.SE );
//I ADDED FIN
				index = AddCraft( typeof( SmallTowerSculpture ), 1044501, 1029242, 82.0, 132.0, typeof( Granite ), 1044514, 3, 1044513 );
				SetNeededExpansion( index, Expansion.SE );
			}

			// Furniture
//I ADDED
			AddCraft( typeof( MarbleTableSectionable ), 1044502, "Marble Table (Turnable)", 86.7, 140.0, typeof( Granite ), 1044514, 4, 1044513 );
			AddCraft( typeof( SandstoneTableSectionable ), 1044502, "Sandstone Table (Turnable)", 86.7, 140.0, typeof( Granite ), 1044514, 4, 1044513 );
			AddCraft( typeof( SmallSoulForge0x44c7 ), 1044502, "Small Soul Forge", 86.7, 140.0, typeof( Granite ), 1044514, 30, 1044513 );
//I ADDED FIN
			AddCraft( typeof( StoneChair ), 1044502, 1024635, 55.0, 105.0, typeof( Granite ), 1044514, 4, 1044513 );
			AddCraft( typeof( MediumStoneTableEastDeed ), 1044502, 1044508, 65.0, 115.0, typeof( Granite ), 1044514, 6, 1044513 );
			AddCraft( typeof( MediumStoneTableSouthDeed ), 1044502, 1044509, 65.0, 115.0, typeof( Granite ), 1044514, 6, 1044513 );
			AddCraft( typeof( LargeStoneTableEastDeed ), 1044502, 1044511, 75.0, 125.0, typeof( Granite ), 1044514, 9, 1044513 );
			AddCraft( typeof( LargeStoneTableSouthDeed ), 1044502, 1044512, 75.0, 125.0, typeof( Granite ), 1044514, 9, 1044513 );

			// Statues
			AddCraft( typeof( StatueSouth ), 1044503, 1044505, 60.0, 120.0, typeof( Granite ), 1044514, 3, 1044513 );
			AddCraft( typeof( StatueNorth ), 1044503, 1044506, 60.0, 120.0, typeof( Granite ), 1044514, 3, 1044513 );
			AddCraft( typeof( StatueEast ), 1044503, 1044507, 60.0, 120.0, typeof( Granite ), 1044514, 3, 1044513 );
			AddCraft( typeof( StatuePegasus ), 1044503, 1044510, 70.0, 130.0, typeof( Granite ), 1044514, 4, 1044513 );

//I ADDED
			AddCraft( typeof( StoneBustStatue0x12ca ), 1044503, "A Bust Statue", 70.0, 140.0, typeof( Granite ), 1044514, 10, 1044513 );
			AddCraft( typeof( StonePosedStatue0x3F19 ), 1044503, "A Posed Statue", 70.0, 140.0, typeof( Granite ), 1044514, 10, 1044513 );
			AddCraft( typeof( StoneSeahorseStatue0x4578 ), 1044503, "A Seahorse Statue", 70.0, 140.0, typeof( Granite ), 1044514, 60, 1044513 );
			AddCraft( typeof( StoneAlantianStatue0x12d8 ), 1044503, "A Alantian Statue", 80.0, 140.0, typeof( Granite ), 1044514, 75, 1044513 );
			AddCraft( typeof( StoneMermaidStatue0x457a ), 1044503, "A Mermaid Statue", 90.0, 140.0, typeof( Granite ), 1044514, 60, 1044513 );
			AddCraft( typeof( StoneGriffinStatue0x457c ), 1044503, "A Griffin Statue", 100.0, 140.0, typeof( Granite ), 1044514, 65, 1044513 );
//I ADDED FIN
			#region Mondain's Legacy
			// Misc Addons
			if ( Core.ML )
			{
//I ADDED
				int index = AddCraft( typeof( SmallEmptyPot ), 1044290, "Small Empty Pot", 86.7, 140.0, typeof( Granite ), 1044514, 10, 1044513 );
				index = AddCraft( typeof( LargeEmptyPot ), 1044290, "Large Empty Pot", 86.7, 140.0, typeof( Granite ), 1044514, 10, 1044513 );
				index = AddCraft( typeof( EmptyPewterBowl ), 1044290, "Empty Pewter Bowl", 86.7, 140.0, typeof( Granite ), 1044514, 10, 1044513 );
				index = AddCraft( typeof( EmptyPewterTub ), 1044290, "Empty Pewter Tub", 86.7, 140.0, typeof( Granite ), 1044514, 10, 1044513 );
				index = AddCraft( typeof( Engines.Plants.PlantBowl ), 1044290, "A Plant Bowl", 86.7, 140.0, typeof( Granite ), 1044514, 10, 1044513 );
//I ADDED FIN
				index = AddCraft( typeof( StoneAnvilSouthDeed ), 1044290, 1072876, 78.0, 128.0, typeof( Granite ), 1044514, 20, 1044513 );
				AddRecipe( index, (int) CarpRecipes.StoneAnvilSouth );
				SetNeededExpansion( index, Expansion.ML );
				
				index = AddCraft( typeof( StoneAnvilEastDeed ), 1044290, 1073392, 78.0, 128.0, typeof( Granite ), 1044514, 20, 1044513 );
				AddRecipe( index, (int) CarpRecipes.StoneAnvilEast );
				SetNeededExpansion( index, Expansion.ML );
			}
			#endregion

			SetSubRes( typeof( Granite ), 1044525 );

			AddSubRes( typeof( Granite ),			1044525, 00.0, 1044514, 1044526 );
			AddSubRes( typeof( DullCopperGranite ),	1044023, 65.0, 1044514, 1044527 );
			AddSubRes( typeof( ShadowIronGranite ),	1044024, 70.0, 1044514, 1044527 );
			AddSubRes( typeof( CopperGranite ),		1044025, 75.0, 1044514, 1044527 );
			AddSubRes( typeof( BronzeGranite ),		1044026, 80.0, 1044514, 1044527 );
			AddSubRes( typeof( GoldGranite ),		1044027, 85.0, 1044514, 1044527 );
			AddSubRes( typeof( AgapiteGranite ),	1044028, 90.0, 1044514, 1044527 );
			AddSubRes( typeof( VeriteGranite ),		1044029, 95.0, 1044514, 1044527 );
			AddSubRes( typeof( ValoriteGranite ),	1044030, 99.0, 1044514, 1044527 );
		}
	}
}
