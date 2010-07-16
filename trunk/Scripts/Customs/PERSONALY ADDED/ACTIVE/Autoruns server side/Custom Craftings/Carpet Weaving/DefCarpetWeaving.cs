using System;
using Server.Items;

namespace Server.Engines.Craft
{

	public class DefCarpetWeaving : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Tailoring; }
		}
        public override string GumpTitleString
        {
            get { return "<BASEFONT COLOR=#FFFFFF><CENTER>CARPET WEAVING MENU</CENTER></BASEFONT>"; }
        }


		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefCarpetWeaving();

				return m_CraftSystem;
			}
		}

//		public override CraftECA ECA{ get{ return CraftECA.ChanceMinusSixtyToFourtyFive; } }

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.0; // was 0.5 50%
		}

		private DefCarpetWeaving() : base( 1, 1, 1.25 )// base( 1, 1, 4.5 )
		{
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if( tool == null || tool.Deleted || tool.UsesRemaining < 0 )
				return 1044038; // You have worn out your tool!
			else if ( !BaseTool.CheckAccessible( tool, from ) )
				return 1044263; // The tool must be on your person to use.

			return 0;
		}

		public override void PlayCraftEffect( Mobile from )
		{
			from.PlaySound( 0x248 );
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
			int index = -1;

			#region BLUE CARPETS
			index = AddCraft( typeof( CarpetPlainBlueNorthPart ), "Blue Carpets", "Carpet Plain Blue Top North Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetPlainBlueNorthEastPart ), "Blue Carpets", "Carpet Plain Blue North East Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );
			
			index = AddCraft( typeof( CarpetPlainBlueEastPart ), "Blue Carpets", "Carpet Plain Blue East Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetPlainBlueSouthEastPart ), "Blue Carpets", "Carpet Plain Blue South East Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetPlainBlueSouthPart ), "Blue Carpets", "Carpet Plain Blue South Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetPlainBlueSouthWestPart ), "Blue Carpets", "Carpet Plain Blue South West Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetPlainBlueWestPart ), "Blue Carpets", "Carpet Plain Blue West Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetPlainBlueNorthWestPart ), "Blue Carpets", "Carpet Plain Blue North West Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetPlainBlueCenterAAPart ), "Blue Carpets", "Carpet Plain Blue Center AA Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetPlainBlueCenterABPart ), "Blue Carpets", "Carpet Plain Blue Center AB Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetPlainBlueCenterBAPart ), "Blue Carpets", "Carpet Plain Blue Center BA Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetPlainBlueCenterBBPart ), "Blue Carpets", "Carpet Plain Blue Center BB Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );
			
			index = AddCraft( typeof( CarpetPlainBlueCenterCPart ), "Blue Carpets", "Carpet Plain Blue Center C Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetPlainBlueCenterDPart ), "Blue Carpets", "Carpet Plain Blue Center D Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );
// FANCY
			index = AddCraft( typeof( CarpetFancyBlueNorthPart ), "Blue Carpets", "Carpet Fancy Blue Top North Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetFancyBlueNorthEastPart ), "Blue Carpets", "Carpet Fancy Blue North East Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );
			
			index = AddCraft( typeof( CarpetFancyBlueEastPart ), "Blue Carpets", "Carpet Fancy Blue East Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetFancyBlueSouthEastPart ), "Blue Carpets", "Carpet Fancy Blue South East Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetFancyBlueSouthPart ), "Blue Carpets", "Carpet Fancy Blue South Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetFancyBlueSouthWestPart ), "Blue Carpets", "Carpet Fancy Blue South West Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetFancyBlueWestPart ), "Blue Carpets", "Carpet Fancy Blue West Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetFancyBlueNorthWestPart ), "Blue Carpets", "Carpet Fancy Blue North West Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetFancyBlueCenterPart ), "Blue Carpets", "Carpet Fancy Blue Center Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );
			
			
			
			
			
			
//			index = AddCraft( typeof( CarpetPlainBlueEastPart ), "Plain Carpets", "Carpet Plain Blue East Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough wool to make that" );
//			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that" );
//			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that" );
			

			#endregion
			
			
			
			#region CINNAMON CARPETS
			index = AddCraft( typeof( CarpetCinnamonNorthPart ), "Cinnamon Carpets", "Carpet Cinnamon Top North Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetCinnamonNorthEastPart ), "Cinnamon Carpets", "Carpet Cinnamon North East Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );
			
			index = AddCraft( typeof( CarpetCinnamonEastPart ), "Cinnamon Carpets", "Carpet Cinnamon East Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetCinnamonSouthEastPart ), "Cinnamon Carpets", "Carpet Cinnamon South East Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetCinnamonSouthPart ), "Cinnamon Carpets", "Carpet Cinnamon South Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetCinnamonSouthWestPart ), "Cinnamon Carpets", "Carpet Cinnamon South West Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetCinnamonWestPart ), "Cinnamon Carpets", "Carpet Cinnamon West Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetCinnamonNorthWestPart ), "Cinnamon Carpets", "Carpet Cinnamon North West Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetCinnamonCenterPart ), "Cinnamon Carpets", "Carpet Cinnamon Center Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );
			
			#endregion
			
			
			
			#region GOLDEN CARPETS
			index = AddCraft( typeof( CarpetGoldenNorthPart ), "Golden Carpets", "Carpet Golden Top North Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetGoldenNorthEastPart ), "Golden Carpets", "Carpet Golden North East Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );
			
			index = AddCraft( typeof( CarpetGoldenEastPart ), "Golden Carpets", "Carpet Golden East Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetGoldenSouthEastPart ), "Golden Carpets", "Carpet Golden South East Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetGoldenSouthPart ), "Golden Carpets", "Carpet Golden South Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetGoldenSouthWestPart ), "Golden Carpets", "Carpet Golden South West Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetGoldenWestPart ), "Golden Carpets", "Carpet Golden West Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetGoldenNorthWestPart ), "Golden Carpets", "Carpet Golden North West Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetGoldenCenterPart ), "Golden Carpets", "Carpet Golden Center Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );
			
			#endregion
			
			
			
			#region PINK CARPETS
			index = AddCraft( typeof( CarpetPinkNorthPart ), "Pink Carpets", "Carpet Pink Top North Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetPinkNorthEastPart ), "Pink Carpets", "Carpet Pink North East Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );
			
			index = AddCraft( typeof( CarpetPinkEastPart ), "Pink Carpets", "Carpet Pink East Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetPinkSouthEastPart ), "Pink Carpets", "Carpet Pink South East Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetPinkSouthPart ), "Pink Carpets", "Carpet Pink South Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetPinkSouthWestPart ), "Pink Carpets", "Carpet Pink South West Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetPinkWestPart ), "Pink Carpets", "Carpet Pink West Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetPinkNorthWestPart ), "Pink Carpets", "Carpet Pink North West Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetPinkCenterPart ), "Pink Carpets", "Carpet Pink Center Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );
			
			#endregion
			
			
			
			#region RED CARPETS
			index = AddCraft( typeof( CarpetRedNorthPart ), "Red Carpets", "Carpet Red Top North Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetRedNorthEastPart ), "Red Carpets", "Carpet Red North East Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );
			
			index = AddCraft( typeof( CarpetRedEastPart ), "Red Carpets", "Carpet Red East Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetRedSouthEastPart ), "Red Carpets", "Carpet Red South East Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetRedSouthPart ), "Red Carpets", "Carpet Red South Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetRedSouthWestPart ), "Red Carpets", "Carpet Red South West Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetRedWestPart ), "Red Carpets", "Carpet Red West Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetRedNorthWestPart ), "Red Carpets", "Carpet Red North West Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetRedCenterAPart ), "Red Carpets", "Carpet Red Center A Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetRedCenterBPart ), "Red Carpets", "Carpet Red Center B Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );

			index = AddCraft( typeof( CarpetRedCenterCPart ), "Red Carpets", "Carpet Red Center C Part", 86.7, 140.0, typeof( Wool ), "Wool", 24, "You don't have enough WOOL to make that." );
			AddRes( index, typeof( Dyes ), "Dyes", 6, "You don't have enough DYES to make that." );
			AddRes( index, typeof( Flax ), "Flax", 1, "You don't have enough FLAX to make that." );
			
			
			#endregion
			
			
			
			MarkOption = false;
			
		}
	}
}
