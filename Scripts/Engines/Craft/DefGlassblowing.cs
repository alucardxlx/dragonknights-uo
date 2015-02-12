using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Craft
{
    public class DefGlassblowing : CraftSystem
    {
        public override SkillName MainSkill
        {
            get { return SkillName.Alchemy; }
        }

        public override int GumpTitleNumber
        {
            get { return 1044622; } // <CENTER>Glassblowing MENU</CENTER>
        }

        private static CraftSystem m_CraftSystem;

        public static CraftSystem CraftSystem
        {
            get
            {
                if (m_CraftSystem == null)
                    m_CraftSystem = new DefGlassblowing();

                return m_CraftSystem;
            }
        }

        public override double GetChanceAtMin(CraftItem item)
        {
            return 0.0; // 0%
        }

        private DefGlassblowing()
            : base(1, 1, 1.25)// base( 1, 2, 1.7 )
        {
        }

        public override int CanCraft(Mobile from, BaseTool tool, Type itemType)
        {
            if (tool == null || tool.Deleted || tool.UsesRemaining < 0)
                return 1044038; // You have worn out your tool!
            else if (!BaseTool.CheckTool(tool, from))
                return 1048146; // If you have a tool equipped, you must use that tool.
            else if (!(from is PlayerMobile && ((PlayerMobile)from).Glassblowing && from.Skills[SkillName.Alchemy].Base >= 100.0))
                return 1044634; // You havent learned glassblowing.
            else if (!BaseTool.CheckAccessible(tool, from))
                return 1044263; // The tool must be on your person to use.

            bool anvil, forge;

            DefBlacksmithy.CheckAnvilAndForge(from, 2, out anvil, out forge);

            if (forge)
                return 0;

            return 1044628; // You must be near a forge to blow glass.
        }

        public override void PlayCraftEffect(Mobile from)
        {
            from.PlaySound(0x2B); // bellows

			if ( from.Body.Type == BodyType.Human && !from.Mounted )
				from.Animate( 9, 5, 1, true, false, 0 );

			new InternalTimer( from ).Start();
		}

        // Delay to synchronize the sound with the hit on the anvil
        private class InternalTimer : Timer
        {
            private Mobile m_From;

            public InternalTimer(Mobile from)
                : base(TimeSpan.FromSeconds(0.7))
            {
                m_From = from;
            }

            protected override void OnTick()
            {
                m_From.PlaySound(0x2A);
            }
        }

        public override int PlayEndingEffect(Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item)
        {
            if (toolBroken)
                from.SendLocalizedMessage(1044038); // You have worn out your tool

            if (failed)
            {
                if (lostMaterial)
                    return 1044043; // You failed to create the item, and some of your materials are lost.
                else
                    return 1044157; // You failed to create the item, but no materials were lost.
            }
            else
            {
                from.PlaySound(0x41); // glass breaking

                if (quality == 0)
                    return 502785; // You were barely able to make this item.  It's quality is below average.
                else if (makersMark && quality == 2)
                    return 1044156; // You create an exceptional quality item and affix your maker's mark.
                else if (quality == 2)
                    return 1044155; // You create an exceptional quality item.
                else
                    return 1044154; // You create the item.
            }
        }

        public override void InitCraftList()
        {
//I ADDED and Modded
			int index = AddCraft( typeof( PedestalWithCrystal0x2fd4 ), "Crystals", "Pedestal With Crystal", 100.0, 140.0, typeof( StonePedestal0x1223 ), "Stone Pedestal", 1, "You do not have a stone pedestal." );
        	AddRes(index, typeof(GammaCrystalFragment), "Gamma Crystal Fragments", 100, "You do not have enough gamma crystal fragments to make that item.");
        	
        	index = AddCraft( typeof( CrystalBrazier0x35ef ), "Crystals", "Crystal Brazer", 100.0, 140.0, typeof( GammaCrystal ), "Gamma Crystal", 100, "You do not have enough gamma crystal to make that item." );
        	AddRes(index, typeof(GammaCrystalFragment), "Gamma Crystal Fragments", 100, "You do not have enough gamma crystal fragments to make that item.");
        	AddRes(index, typeof(Sapphire), "Sapphire", 100, "You do not have enough sapphire to make that item.");
        	
        	index = AddCraft( typeof( CrystalStatue0x35fc ), "Crystals", "Crystal Statue", 114.0, 140.0, typeof( GammaCrystal ), "Gamma Crystal", 100, "You do not have enough gamma crystal to make that item." );
        	AddRes(index, typeof(GammaCrystalFragment), "Gamma Crystal Fragments", 100, "You do not have enough gamma crystal fragments to make that item.");
        	AddRes(index, typeof(Sapphire), "Sapphire", 100, "You do not have enough sapphire to make that item.");
        	
        	index = AddCraft( typeof( SmallSoulForge0x44c7 ), "Crystals", "Small Soul Forge", 119.0, 140.0, typeof( GammaCrystal ), "Gamma Crystal", 100, "You do not have enough gamma crystal to make that item." );
        	AddRes(index, typeof(GammaCrystalFragment), "Gamma Crystal Fragments", 100, "You do not have enough gamma crystal fragments to make that item.");
        	AddRes(index, typeof(Sapphire), "Sapphire", 100, "You do not have enough sapphire to make that item.");
        	AddRes(index, typeof(IronIngot), 1044036, 100, 1044037);
        	
        	index = AddCraft( typeof( CrystalThrone0x35ed ), "Crystals", "Crystal Throne", 124.0, 140.0, typeof( GammaCrystal ), "Gamma Crystal", 100, "You do not have enough gamma crystal to make that item." );
        	AddRes(index, typeof(GammaCrystalFragment), "Gamma Crystal Fragments", 400, "You do not have enough gamma crystal fragments to make that item.");
        	AddRes(index, typeof(Sapphire), "Sapphire", 400, "You do not have enough sapphire to make that item.");
///////////////////////        	
			index = AddCraft( typeof( AlchemicalSymbolGlowSA0x483B ), "Gamma Glowing Runes ", "Gamma Alchemical SymBol 01", 93.75, 140.0, typeof( GammaCrystalFragment ), "Gamma Crystal Fragments", 3, "You do not have enough gamma crystal fragments to make that item." );
			AddRes(index, typeof(GlassBrick), "Glass Bricks", 16, "You do not have enough glass bricks to make that.");
			AddRes(index, typeof(GammaCrystalDust), "Gamma Dust", 16, "You do not have enough gamma dust to make that.");

			index = AddCraft( typeof( AlchemicalSymbolGlowSA0x483E ), "Gamma Glowing Runes ", "Gamma Alchemical SymBol 02", 93.75, 140.0, typeof( GammaCrystalFragment ), "Gamma Crystal Fragments", 3, "You do not have enough gamma crystal fragments to make that item." );
			AddRes(index, typeof(GlassBrick), "Glass Bricks", 16, "You do not have enough glass bricks to make that.");
			AddRes(index, typeof(GammaCrystalDust), "Gamma Dust", 16, "You do not have enough gamma dust to make that.");

			index = AddCraft( typeof( AlchemicalSymbolGlowSA0x4841 ), "Gamma Glowing Runes ", "Gamma Alchemical SymBol 03", 93.75, 140.0, typeof( GammaCrystalFragment ), "Gamma Crystal Fragments", 3, "You do not have enough gamma crystal fragments to make that item." );
			AddRes(index, typeof(GlassBrick), "Glass Bricks", 16, "You do not have enough glass bricks to make that.");
			AddRes(index, typeof(GammaCrystalDust), "Gamma Dust", 16, "You do not have enough gamma dust to make that.");

			index = AddCraft( typeof( AlchemicalSymbolGlowSA0x4844 ), "Gamma Glowing Runes ", "Gamma Alchemical SymBol 04", 93.75, 140.0, typeof( GammaCrystalFragment ), "Gamma Crystal Fragments", 3, "You do not have enough gamma crystal fragments to make that item." );
			AddRes(index, typeof(GlassBrick), "Glass Bricks", 16, "You do not have enough glass bricks to make that.");
			AddRes(index, typeof(GammaCrystalDust), "Gamma Dust", 16, "You do not have enough gamma dust to make that.");

			index = AddCraft( typeof( AlchemicalSymbolGlowSA0x4847 ), "Gamma Glowing Runes ", "Gamma Alchemical SymBol 05", 93.75, 140.0, typeof( GammaCrystalFragment ), "Gamma Crystal Fragments", 3, "You do not have enough gamma crystal fragments to make that item." );
			AddRes(index, typeof(GlassBrick), "Glass Bricks", 16, "You do not have enough glass bricks to make that.");
			AddRes(index, typeof(GammaCrystalDust), "Gamma Dust", 16, "You do not have enough gamma dust to make that.");

			index = AddCraft( typeof( AlchemicalSymbolGlowSA0x484A ), "Gamma Glowing Runes ", "Gamma Alchemical SymBol 06", 93.75, 140.0, typeof( GammaCrystalFragment ), "Gamma Crystal Fragments", 3, "You do not have enough gamma crystal fragments to make that item." );
			AddRes(index, typeof(GlassBrick), "Glass Bricks", 16, "You do not have enough glass bricks to make that.");
			AddRes(index, typeof(GammaCrystalDust), "Gamma Dust", 16, "You do not have enough gamma dust to make that.");

			index = AddCraft( typeof( AlchemicalSymbolGlowSA0x484D ), "Gamma Glowing Runes ", "Gamma Alchemical SymBol 07", 93.75, 140.0, typeof( GammaCrystalFragment ), "Gamma Crystal Fragments", 3, "You do not have enough gamma crystal fragments to make that item." );
			AddRes(index, typeof(GlassBrick), "Glass Bricks", 16, "You do not have enough glass bricks to make that.");
			AddRes(index, typeof(GammaCrystalDust), "Gamma Dust", 16, "You do not have enough gamma dust to make that.");

			index = AddCraft( typeof( AlchemicalSymbolGlowSA0x4850 ), "Gamma Glowing Runes ", "Gamma Alchemical SymBol 08", 93.75, 140.0, typeof( GammaCrystalFragment ), "Gamma Crystal Fragments", 3, "You do not have enough gamma crystal fragments to make that item." );
			AddRes(index, typeof(GlassBrick), "Glass Bricks", 16, "You do not have enough glass bricks to make that.");
			AddRes(index, typeof(GammaCrystalDust), "Gamma Dust", 16, "You do not have enough gamma dust to make that.");

			index = AddCraft( typeof( AlchemicalSymbolGlowSA0x4853 ), "Gamma Glowing Runes ", "Gamma Alchemical SymBol 09", 93.75, 140.0, typeof( GammaCrystalFragment ), "Gamma Crystal Fragments", 3, "You do not have enough gamma crystal fragments to make that item." );
			AddRes(index, typeof(GlassBrick), "Glass Bricks", 16, "You do not have enough glass bricks to make that.");
			AddRes(index, typeof(GammaCrystalDust), "Gamma Dust", 16, "You do not have enough gamma dust to make that.");

			index = AddCraft( typeof( AlchemicalSymbolGlowSA0x4856 ), "Gamma Glowing Runes ", "Gamma Alchemical SymBol 10", 93.75, 140.0, typeof( GammaCrystalFragment ), "Gamma Crystal Fragments", 3, "You do not have enough gamma crystal fragments to make that item." );
			AddRes(index, typeof(GlassBrick), "Glass Bricks", 16, "You do not have enough glass bricks to make that.");
			AddRes(index, typeof(GammaCrystalDust), "Gamma Dust", 16, "You do not have enough gamma dust to make that.");

			index = AddCraft( typeof( AlchemicalSymbolGlowSA0x4859 ), "Gamma Glowing Runes ", "Gamma Alchemical SymBol 11", 93.75, 140.0, typeof( GammaCrystalFragment ), "Gamma Crystal Fragments", 3, "You do not have enough gamma crystal fragments to make that item." );
			AddRes(index, typeof(GlassBrick), "Glass Bricks", 16, "You do not have enough glass bricks to make that.");
			AddRes(index, typeof(GammaCrystalDust), "Gamma Dust", 16, "You do not have enough gamma dust to make that.");

			index = AddCraft( typeof( AlchemicalSymbolGlowSA0x485C ), "Gamma Glowing Runes ", "Gamma Alchemical SymBol 12", 93.75, 140.0, typeof( GammaCrystalFragment ), "Gamma Crystal Fragments", 3, "You do not have enough gamma crystal fragments to make that item." );
			AddRes(index, typeof(GlassBrick), "Glass Bricks", 16, "You do not have enough glass bricks to make that.");
			AddRes(index, typeof(GammaCrystalDust), "Gamma Dust", 16, "You do not have enough gamma dust to make that.");

			index = AddCraft( typeof( AlchemicalSymbolGlowSA0x485F ), "Gamma Glowing Runes ", "Gamma Alchemical SymBol 13", 93.75, 140.0, typeof( GammaCrystalFragment ), "Gamma Crystal Fragments", 3, "You do not have enough gamma crystal fragments to make that item." );
			AddRes(index, typeof(GlassBrick), "Glass Bricks", 16, "You do not have enough glass bricks to make that.");
			AddRes(index, typeof(GammaCrystalDust), "Gamma Dust", 16, "You do not have enough gamma dust to make that.");

			index = AddCraft( typeof( AlchemicalSymbolGlowSA0x4862 ), "Gamma Glowing Runes ", "Gamma Alchemical SymBol 14", 93.75, 140.0, typeof( GammaCrystalFragment ), "Gamma Crystal Fragments", 3, "You do not have enough gamma crystal fragments to make that item." );
			AddRes(index, typeof(GlassBrick), "Glass Bricks", 16, "You do not have enough glass bricks to make that.");
			AddRes(index, typeof(GammaCrystalDust), "Gamma Dust", 16, "You do not have enough gamma dust to make that.");

			index = AddCraft( typeof( AlchemicalSymbolGlowSA0x4865 ), "Gamma Glowing Runes ", "Gamma Alchemical SymBol 15", 93.75, 140.0, typeof( GammaCrystalFragment ), "Gamma Crystal Fragments", 3, "You do not have enough gamma crystal fragments to make that item." );
			AddRes(index, typeof(GlassBrick), "Glass Bricks", 16, "You do not have enough glass bricks to make that.");
			AddRes(index, typeof(GammaCrystalDust), "Gamma Dust", 16, "You do not have enough gamma dust to make that.");

			index = AddCraft( typeof( AlchemicalSymbolGlowSA0x4868 ), "Gamma Glowing Runes ", "Gamma Alchemical SymBol 16", 93.75, 140.0, typeof( GammaCrystalFragment ), "Gamma Crystal Fragments", 3, "You do not have enough gamma crystal fragments to make that item." );
			AddRes(index, typeof(GlassBrick), "Glass Bricks", 16, "You do not have enough glass bricks to make that.");
			AddRes(index, typeof(GammaCrystalDust), "Gamma Dust", 16, "You do not have enough gamma dust to make that.");

			index = AddCraft( typeof( AlchemicalSymbolGlowSA0x486B ), "Gamma Glowing Runes ", "Gamma Alchemical SymBol 17", 93.75, 140.0, typeof( GammaCrystalFragment ), "Gamma Crystal Fragments", 3, "You do not have enough gamma crystal fragments to make that item." );
			AddRes(index, typeof(GlassBrick), "Glass Bricks", 16, "You do not have enough glass bricks to make that.");
			AddRes(index, typeof(GammaCrystalDust), "Gamma Dust", 16, "You do not have enough gamma dust to make that.");

			index = AddCraft( typeof( AlchemicalSymbolGlowSA0x486E ), "Gamma Glowing Runes ", "Gamma Alchemical SymBol 18", 93.75, 140.0, typeof( GammaCrystalFragment ), "Gamma Crystal Fragments", 3, "You do not have enough gamma crystal fragments to make that item." );
			AddRes(index, typeof(GlassBrick), "Glass Bricks", 16, "You do not have enough glass bricks to make that.");
			AddRes(index, typeof(GammaCrystalDust), "Gamma Dust", 16, "You do not have enough gamma dust to make that.");

			index = AddCraft( typeof( AlchemicalSymbolGlowSA0x4871 ), "Gamma Glowing Runes ", "Gamma Alchemical SymBol 19", 93.75, 140.0, typeof( GammaCrystalFragment ), "Gamma Crystal Fragments", 3, "You do not have enough gamma crystal fragments to make that item." );
			AddRes(index, typeof(GlassBrick), "Glass Bricks", 16, "You do not have enough glass bricks to make that.");
			AddRes(index, typeof(GammaCrystalDust), "Gamma Dust", 16, "You do not have enough gamma dust to make that.");

			index = AddCraft( typeof( AlchemicalSymbolGlowSA0x4874 ), "Gamma Glowing Runes ", "Gamma Alchemical SymBol 20", 93.75, 140.0, typeof( GammaCrystalFragment ), "Gamma Crystal Fragments", 3, "You do not have enough gamma crystal fragments to make that item." );
			AddRes(index, typeof(GlassBrick), "Glass Bricks", 16, "You do not have enough glass bricks to make that.");
			AddRes(index, typeof(GammaCrystalDust), "Gamma Dust", 16, "You do not have enough gamma dust to make that.");

			index = AddCraft( typeof( AlchemicalSymbolGlowSA0x4877 ), "Gamma Glowing Runes ", "Gamma Alchemical SymBol 21", 93.75, 140.0, typeof( GammaCrystalFragment ), "Gamma Crystal Fragments", 3, "You do not have enough gamma crystal fragments to make that item." );
			AddRes(index, typeof(GlassBrick), "Glass Bricks", 16, "You do not have enough glass bricks to make that.");
			AddRes(index, typeof(GammaCrystalDust), "Gamma Dust", 16, "You do not have enough gamma dust to make that.");

			index = AddCraft( typeof( AlchemicalSymbolGlowSA0x487A ), "Gamma Glowing Runes ", "Gamma Alchemical SymBol 22", 93.75, 140.0, typeof( GammaCrystalFragment ), "Gamma Crystal Fragments", 3, "You do not have enough gamma crystal fragments to make that item." );
			AddRes(index, typeof(GlassBrick), "Glass Bricks", 16, "You do not have enough glass bricks to make that.");
			AddRes(index, typeof(GammaCrystalDust), "Gamma Dust", 16, "You do not have enough gamma dust to make that.");

			index = AddCraft( typeof( AlchemicalSymbolGlowSA0x487D ), "Gamma Glowing Runes ", "Gamma Alchemical SymBol 23", 93.75, 140.0, typeof( GammaCrystalFragment ), "Gamma Crystal Fragments", 3, "You do not have enough gamma crystal fragments to make that item." );
			AddRes(index, typeof(GlassBrick), "Glass Bricks", 16, "You do not have enough glass bricks to make that.");
			AddRes(index, typeof(GammaCrystalDust), "Gamma Dust", 16, "You do not have enough gamma dust to make that.");

			index = AddCraft( typeof( AlchemicalSymbolGlowSA0x4880 ), "Gamma Glowing Runes ", "Gamma Alchemical SymBol 24", 93.75, 140.0, typeof( GammaCrystalFragment ), "Gamma Crystal Fragments", 3, "You do not have enough gamma crystal fragments to make that item." );
			AddRes(index, typeof(GlassBrick), "Glass Bricks", 16, "You do not have enough glass bricks to make that.");
			AddRes(index, typeof(GammaCrystalDust), "Gamma Dust", 16, "You do not have enough gamma dust to make that.");

			index = AddCraft( typeof( AlchemicalSymbolGlowSA0x4883 ), "Gamma Glowing Runes ", "Gamma Alchemical SymBol 25", 93.75, 140.0, typeof( GammaCrystalFragment ), "Gamma Crystal Fragments", 3, "You do not have enough gamma crystal fragments to make that item." );
			AddRes(index, typeof(GlassBrick), "Glass Bricks", 16, "You do not have enough glass bricks to make that.");
			AddRes(index, typeof(GammaCrystalDust), "Gamma Dust", 16, "You do not have enough gamma dust to make that.");


//I ADDED and Modded FIN
            index = AddCraft(typeof(Bottle), 1044050, 1023854, 52.5, 102.5, typeof(Sand), 1044625, 1, 1044627);
            SetUseAllRes(index, true);

            AddCraft(typeof(SmallFlask), 1044050, 1044610, 52.5, 102.5, typeof(Sand), 1044625, 2, 1044627);
            AddCraft(typeof(MediumFlask), 1044050, 1044611, 52.5, 102.5, typeof(Sand), 1044625, 3, 1044627);
            AddCraft(typeof(CurvedFlask), 1044050, 1044612, 55.0, 105.0, typeof(Sand), 1044625, 2, 1044627);
            AddCraft(typeof(LongFlask), 1044050, 1044613, 57.5, 107.5, typeof(Sand), 1044625, 4, 1044627);
            AddCraft(typeof(LargeFlask), 1044050, 1044623, 60.0, 110.0, typeof(Sand), 1044625, 5, 1044627);
            AddCraft(typeof(AniSmallBlueFlask), 1044050, 1044614, 60.0, 110.0, typeof(Sand), 1044625, 5, 1044627);
            AddCraft(typeof(AniLargeVioletFlask), 1044050, 1044615, 60.0, 110.0, typeof(Sand), 1044625, 5, 1044627);
            AddCraft(typeof(AniRedRibbedFlask), 1044050, 1044624, 60.0, 110.0, typeof(Sand), 1044625, 7, 1044627);
            AddCraft(typeof(EmptyVialsWRack), 1044050, 1044616, 65.0, 115.0, typeof(Sand), 1044625, 8, 1044627);
            AddCraft(typeof(FullVialsWRack), 1044050, 1044617, 65.0, 115.0, typeof(Sand), 1044625, 9, 1044627);
            AddCraft(typeof(SpinningHourglass), 1044050, 1044618, 75.0, 125.0, typeof(Sand), 1044625, 10, 1044627);
            
            if (Core.ML)
            {
                index = AddCraft(typeof(HollowPrism), 1044050, 1072895, 100.0, 150.0, typeof(Sand), 1044625, 8, 1044627);
                SetNeededExpansion(index, Expansion.ML);
//I ADDED
				index = AddCraft(typeof(HealingFairyJar),1044050, "Healing Fairy Jar: Empty", 100.0, 150.0, typeof(Sand),1044625, 40, 1044627);
				AddRes(index, typeof(Diamond), 1062608, 10, 1044240);
				SetNeededExpansion(index, Expansion.ML);
				
				index = AddCraft(typeof(GlassDust),"Resources", "Pure Glass Dust", 124.0, 140.0, typeof(Sand),1044625, 50, 1044627);
				index = AddCraft(typeof(GlassBrick),"Resources", "Pure Glass Bricks", 124.0, 140.0, typeof(GlassDust),"Glass Dust", 100, "You do not have enough glass dust to make that.");
				index = AddCraft(typeof(GammaCrystalDust),"Resources", "Pure Gamma Crystal Dust", 124.0, 140.0, typeof(GammaCrystalFragment),"Gamma Crystal Fragment", 50, "You do not have enough gamma crystal fragments to make that.");


				index = AddCraft( typeof( AlchemicalSymbolGlowML0x0E5C ), "Sapphire Glowing Runes", "Sapphire Alchemical SymBol 1", 93.75, 140.0, typeof( Sapphire ), "Sapphire", 100, "You do not have enough Sapphires to make that item." );
				AddRes(index, typeof(GlassDust), "Glass Dust", 10, "You do not have enough glass dust to make that.");

				index = AddCraft( typeof( AlchemicalSymbolGlowML0x0E5F ), "Sapphire Glowing Runes", "Sapphire Alchemical SymBol 2", 93.75, 140.0, typeof( Sapphire ), "Sapphire", 100, "You do not have enough Sapphires to make that item." );
				AddRes(index, typeof(GlassDust), "Glass Dust", 10, "You do not have enough glass dust to make that.");

				index = AddCraft( typeof( AlchemicalSymbolGlowML0x0E62 ), "Sapphire Glowing Runes", "Sapphire Alchemical SymBol 3", 93.75, 140.0, typeof( Sapphire ), "Sapphire", 100, "You do not have enough Sapphires to make that item." );
				AddRes(index, typeof(GlassDust), "Glass Dust", 10, "You do not have enough glass dust to make that.");

				index = AddCraft( typeof( AlchemicalSymbolGlowML0x0E65 ), "Sapphire Glowing Runes", "Sapphire Alchemical SymBol 4", 93.75, 140.0, typeof( Sapphire ), "Sapphire", 100, "You do not have enough Sapphires to make that item." );
				AddRes(index, typeof(GlassDust), "Glass Dust", 10, "You do not have enough glass dust to make that.");

				index = AddCraft( typeof( AlchemicalSymbolGlowML0x0E68 ), "Sapphire Glowing Runes", "Sapphire Alchemical SymBol 5", 93.75, 140.0, typeof( Sapphire ), "Sapphire", 100, "You do not have enough Sapphires to make that item." );
				AddRes(index, typeof(GlassDust), "Glass Dust", 10, "You do not have enough glass dust to make that.");
				
//I ADDED FIN
            }
        }
    }
}
