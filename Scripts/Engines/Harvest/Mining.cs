using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Engines.Harvest
{
	public class Mining : HarvestSystem
	{
		private static Mining m_System;

		public static Mining System
		{
			get
			{
				if ( m_System == null )
					m_System = new Mining();

				return m_System;
			}
		}

		private HarvestDefinition m_OreAndStone, m_Sand, m_Crystal, m_Clay; //Added Crystal and Clay

		public HarvestDefinition OreAndStone
		{
			get{ return m_OreAndStone; }
		}

		public HarvestDefinition Sand
		{
			get{ return m_Sand; }
		}
//ADDED crystal clay
		public HarvestDefinition Crystal
		{
			get{ return m_Crystal; }
		}
		public HarvestDefinition Clay
		{
			get{ return m_Clay; }
		}
//ADDED END crystal clay

		private Mining()
		{
			HarvestResource[] res;
			HarvestVein[] veins;

			#region Mining for ore and stone
			HarvestDefinition oreAndStone = m_OreAndStone = new HarvestDefinition();

			// Resource banks are every 8x8 tiles CHANGED TO 4X4 AREA
			oreAndStone.BankWidth = 4;
			oreAndStone.BankHeight = 4;

			// Every bank holds from 10 to 34 ore
			oreAndStone.MinTotal = 10;
			oreAndStone.MaxTotal = 34;

			// A resource bank will respawn its content every 10 to 20 minutes
			oreAndStone.MinRespawn = TimeSpan.FromMinutes( 10.0 );
			oreAndStone.MaxRespawn = TimeSpan.FromMinutes( 20.0 );

			// Skill checking is done on the Mining skill
			oreAndStone.Skill = SkillName.Mining;

			// Set the list of harvestable tiles
			oreAndStone.Tiles = m_MountainAndCaveTiles;

			// Players must be within 2 tiles to harvest
			oreAndStone.MaxRange = 2;

			// One ore per harvest action
			oreAndStone.ConsumedPerHarvest = 1;
			oreAndStone.ConsumedPerFeluccaHarvest = 2;

			// The digging effect
			oreAndStone.EffectActions = new int[]{ 11 };
			oreAndStone.EffectSounds = new int[]{ 0x125, 0x126 };
			oreAndStone.EffectCounts = new int[]{ 1 };
			oreAndStone.EffectDelay = TimeSpan.FromSeconds( 1.6 );
			oreAndStone.EffectSoundDelay = TimeSpan.FromSeconds( 0.9 );

			oreAndStone.NoResourcesMessage = 503040; // There is no metal here to mine.
			oreAndStone.DoubleHarvestMessage = 503042; // Someone has gotten to the metal before you.
			oreAndStone.TimedOutOfRangeMessage = 503041; // You have moved too far away to continue mining.
			oreAndStone.OutOfRangeMessage = 500446; // That is too far away.
			oreAndStone.FailMessage = 503043; // You loosen some rocks but fail to find any useable ore.
			oreAndStone.PackFullMessage = 1010481; // Your backpack is full, so the ore you mined is lost.
			oreAndStone.ToolBrokeMessage = 1044038; // You have worn out your tool!

			res = new HarvestResource[]
				{
					new HarvestResource( 00.0, 00.0, 100.0, 1007072, typeof( IronOre ),			typeof( Granite ) ),
					new HarvestResource( 65.0, 25.0, 105.0, 1007073, typeof( DullCopperOre ),	typeof( DullCopperGranite ),	typeof( DullCopperElemental ) ),
					new HarvestResource( 70.0, 30.0, 110.0, 1007074, typeof( ShadowIronOre ),	typeof( ShadowIronGranite ),	typeof( ShadowIronElemental ) ),
					new HarvestResource( 75.0, 35.0, 115.0, 1007075, typeof( CopperOre ),		typeof( CopperGranite ),		typeof( CopperElemental ) ),
					new HarvestResource( 80.0, 40.0, 120.0, 1007076, typeof( BronzeOre ),		typeof( BronzeGranite ),		typeof( BronzeElemental ) ),
					new HarvestResource( 85.0, 45.0, 125.0, 1007077, typeof( GoldOre ),			typeof( GoldGranite ),			typeof( GoldenElemental ) ),
					new HarvestResource( 90.0, 50.0, 130.0, 1007078, typeof( AgapiteOre ),		typeof( AgapiteGranite ),		typeof( AgapiteElemental ) ),
					new HarvestResource( 95.0, 55.0, 135.0, 1007079, typeof( VeriteOre ),		typeof( VeriteGranite ),		typeof( VeriteElemental ) ),
					new HarvestResource( 99.0, 59.0, 139.0, 1007080, typeof( ValoriteOre ),		typeof( ValoriteGranite ),		typeof( ValoriteElemental ) )

				};

			veins = new HarvestVein[]
				{
					new HarvestVein( 49.6, 0.0, res[0], null   ), // Iron
					new HarvestVein( 11.2, 0.5, res[1], res[0] ), // Dull Copper
					new HarvestVein( 09.8, 0.5, res[2], res[0] ), // Shadow Iron
					new HarvestVein( 08.4, 0.5, res[3], res[0] ), // Copper
					new HarvestVein( 07.0, 0.5, res[4], res[0] ), // Bronze
					new HarvestVein( 05.6, 0.5, res[5], res[0] ), // Gold
					new HarvestVein( 04.2, 0.5, res[6], res[0] ), // Agapite
					new HarvestVein( 02.8, 0.5, res[7], res[0] ), // Verite
					new HarvestVein( 01.4, 0.5, res[8], res[0] )  // Valorite
				};

			oreAndStone.Resources = res;
			oreAndStone.Veins = veins;

			if ( Core.ML )
			{
				oreAndStone.BonusResources = new BonusHarvestResource[]
				{
					new BonusHarvestResource( 0, 98.15, null, null ),	//Nothing
					new BonusHarvestResource( 100, .10, 1072562, typeof( BlueDiamond ) ),
					new BonusHarvestResource( 100, .10, 1072567, typeof( DarkSapphire ) ),
					new BonusHarvestResource( 100, .10, 1072570, typeof( EcruCitrine ) ),
					new BonusHarvestResource( 100, .10, 1072564, typeof( FireRuby ) ),
					new BonusHarvestResource( 100, .10, 1072566, typeof( PerfectEmerald ) ),
					new BonusHarvestResource( 100, .10, 1072568, typeof( Turquoise ) ),


//
					new BonusHarvestResource( 100, .01, "You dig up a Tourmaline and put it in your pack", typeof( Tourmaline ) ),
					new BonusHarvestResource( 100, .01, "You dig up a Amethyst and put it in your pack", typeof( Amethyst ) ),
					new BonusHarvestResource( 100, .01, "You dig up a Citrine and put it in your pack", typeof( Citrine ) ),
					new BonusHarvestResource( 100, .01, "You dig up a Ruby and put it in your pack", typeof( Ruby ) ),
					new BonusHarvestResource( 100, .01, "You dig up a Sapphire and put it in your pack", typeof( Sapphire ) ),
					new BonusHarvestResource( 100, .01, "You dig up a Emerald and put it in your pack", typeof( Emerald ) ),
					new BonusHarvestResource( 100, .01, "You dig up a Diamond and put it in your pack", typeof( Diamond ) ),
					new BonusHarvestResource( 100, .01, "You dig up a Star Sapphire and put it in your pack", typeof( StarSapphire ) ),
					new BonusHarvestResource( 100, .01, "You dig up a Amber and put it in your pack", typeof( Amber ) ),
					new BonusHarvestResource( 100, .01, "You dig up a Brilliant Amber and put it in your pack", typeof( BrilliantAmber ) ),

//57
					new BonusHarvestResource( 60, .01, "You dig up a DragonKnight Token and put it in your pack", typeof( DragonKnightToken) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1alchemygem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1anatomygem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1animalloregem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1animaltaminggem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1archerygem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1armsloregem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1begginggem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1blacksmithgem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1bushidogem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1campinggem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1carpentrygem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1cartographygem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1chivalrygem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1cookinggem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1detecthiddengem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1discordancegem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1evalintgem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1fencinggem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1fishinggem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1fletchinggem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1focusgem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1forensicsgem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1healinggem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1herdinggem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1hidinggem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1inscribegem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1itemidgem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1lockpickinggem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1lumberjackinggem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1macinggem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1magerygem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1magicresistgem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1meditationgem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1mininggem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1musicianshipgem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1necromancygem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1ninjitsugem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1parrygem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1peacemakinggem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1poisoninggem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1provocationgem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1removetrapgem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1snoopinggem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1spellweavinggem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1spiritspeakgem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1stealinggem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1stealthgem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1swordsgem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1tacticsgem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1tailoringgem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1tasteidgem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1tinkeringgem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1trackinggem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1veterinarygem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( plus1wrestlinggem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( nightsightgem ) ),
					new BonusHarvestResource( 60, .01, "You dig up a skill gem and put it in your pack", typeof( SpellChannelingGem ) ),
//56
					new BonusHarvestResource( 70, .01, "You dig up a \"Fairy Jar\" and put it in your pack", typeof( HealingFairyJar ) ),


					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2alchemygem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2anatomygem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2animalloregem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2animaltaminggem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2archerygem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2armsloregem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2begginggem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2blacksmithgem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2bushidogem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2campinggem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2carpentrygem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2cartographygem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2chivalrygem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2cookinggem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2detecthiddengem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2discordancegem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2evalintgem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2fencinggem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2fishinggem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2fletchinggem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2focusgem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2forensicsgem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2healinggem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2herdinggem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2hidinggem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2inscribegem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2itemidgem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2lockpickinggem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2lumberjackinggem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2macinggem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2magerygem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2magicresistgem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2meditationgem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2mininggem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2musicianshipgem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2necromancygem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2ninjitsugem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2parrygem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2peacemakinggem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2poisoninggem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2provocationgem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2removetrapgem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2snoopinggem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2spellweavinggem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2spiritspeakgem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2stealinggem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2stealthgem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2swordsgem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2tacticsgem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2tailoringgem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2tasteidgem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2tinkeringgem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2trackinggem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2veterinarygem ) ),
					new BonusHarvestResource( 90, .01, "You dig up a skill gem and put it in your pack", typeof( plus2wrestlinggem ) ),

					new BonusHarvestResource( 100, .01, "You dig up a skill erace gem and put it in your pack", typeof( plus0skilleracegem ) )



//





				};
			}

			oreAndStone.RaceBonus = Core.ML;
			oreAndStone.RandomizeVeins = Core.ML;

			Definitions.Add( oreAndStone );
			#endregion

			#region Mining for sand
			HarvestDefinition sand = m_Sand = new HarvestDefinition();

			// Resource banks are every 8x8 tiles MADE 4 X 4
			sand.BankWidth = 4;
			sand.BankHeight = 4;

			// Every bank holds from 6 to 12 sand
			sand.MinTotal = 6;
			sand.MaxTotal = 12;

			// A resource bank will respawn its content every 10 to 20 minutes
			sand.MinRespawn = TimeSpan.FromMinutes( 10.0 );
			sand.MaxRespawn = TimeSpan.FromMinutes( 20.0 );

			// Skill checking is done on the Mining skill
			sand.Skill = SkillName.Mining;

			// Set the list of harvestable tiles
			sand.Tiles = m_SandTiles;

			// Players must be within 2 tiles to harvest
			sand.MaxRange = 2;

			// One sand per harvest action
			sand.ConsumedPerHarvest = 1;
			sand.ConsumedPerFeluccaHarvest = 1;

			// The digging effect
			sand.EffectActions = new int[]{ 11 };
			sand.EffectSounds = new int[]{ 0x125, 0x126 };
			sand.EffectCounts = new int[]{ 6 };
			sand.EffectDelay = TimeSpan.FromSeconds( 1.6 );
			sand.EffectSoundDelay = TimeSpan.FromSeconds( 0.9 );

			sand.NoResourcesMessage = 1044629; // There is no sand here to mine.
			sand.DoubleHarvestMessage = 1044629; // There is no sand here to mine.
			sand.TimedOutOfRangeMessage = 503041; // You have moved too far away to continue mining.
			sand.OutOfRangeMessage = 500446; // That is too far away.
			sand.FailMessage = 1044630; // You dig for a while but fail to find any of sufficient quality for glassblowing.
			sand.PackFullMessage = 1044632; // Your backpack can't hold the sand, and it is lost!
			sand.ToolBrokeMessage = 1044038; // You have worn out your tool!

			res = new HarvestResource[]
				{
					new HarvestResource( 100.0, 70.0, 400.0, 1044631, typeof( Sand ) )
				};

			veins = new HarvestVein[]
				{
					new HarvestVein( 100.0, 0.0, res[0], null )
				};

			sand.Resources = res;
			sand.Veins = veins;

			Definitions.Add( sand );
			#endregion
			
            #region Mining for crystal
			HarvestDefinition crystal = m_Crystal = new HarvestDefinition();

			// Resource banks are every 8x8 tiles
			crystal.BankWidth = 4;
			crystal.BankHeight = 4;

			// Every bank holds from 10 to 34 Clay
			crystal.MinTotal = 0;
			crystal.MaxTotal = 5;

			// A resource bank will respawn its content every 10 to 20 minutes
			crystal.MinRespawn = TimeSpan.FromMinutes( 10.0 );
			crystal.MaxRespawn = TimeSpan.FromMinutes( 20.0 );

			// Skill checking is done on the Mining skill
			crystal.Skill = SkillName.Mining;

			// Set the list of harvestable tiles
			crystal.Tiles = m_CrystalTiles;

			// Players must be within 2 tiles to harvest
			crystal.MaxRange = 3;

			// One sand per harvest action
			crystal.ConsumedPerHarvest = 1;
			crystal.ConsumedPerFeluccaHarvest = 2;

			// The digging effect
			crystal.EffectActions = new int[]{ 11 };
			crystal.EffectSounds = new int[]{ 0x03f, 0x040, 0x041  };
			crystal.EffectCounts = new int[]{ 6 };
			crystal.EffectDelay = TimeSpan.FromSeconds( 1.6 );
			crystal.EffectSoundDelay = TimeSpan.FromSeconds( 0.9 );

			crystal.NoResourcesMessage = "There is no crystal here to mine."; // There is no clay here to mine.
			crystal.DoubleHarvestMessage = "Someone has gotten to the crystal before you."; // There is no Clay here to mine.
			crystal.TimedOutOfRangeMessage = 503041; // You have moved too far away to continue mining.
			crystal.OutOfRangeMessage = 500446; // That is too far away.
			crystal.FailMessage = "You dig for a while but fail to find any usable crystal."; // You dig for a while but fail to find any of sufficient quality for pottery
			crystal.PackFullMessage = "Your backpack is full, so the crystal you mined is lost." ; // Your backpack can't hold the clay, and it is lost!
			crystal.ToolBrokeMessage = 1044038; // You have worn out your tool!

			res = new HarvestResource[]
				{
				new HarvestResource( 00.0, 00.0, 140.0, "You shattered a crystal and gathered a decent piece.", typeof( GammaCrystalFragment ) )
				};

			veins = new HarvestVein[]
				{
					new HarvestVein( 100.0, 0.0, res[0], null )
				};

			crystal.Resources = res;
			crystal.Veins = veins;
			crystal.BonusResources = new BonusHarvestResource[]
			{
				new BonusHarvestResource( 0, 09.00, null, null ),//Nothing
				new BonusHarvestResource( 0, 10.00, "", typeof( GammaCrystalFragment ) ),
				new BonusHarvestResource( 0, 10.00, "", typeof( GammaCrystalFragment ) ),
				new BonusHarvestResource( 0, 10.00, "", typeof( GammaCrystalFragment ) ),
				new BonusHarvestResource( 0, 10.00, "", typeof( GammaCrystalFragment ) ),
				new BonusHarvestResource( 0, 10.00, "", typeof( GammaCrystalFragment ) ),
				new BonusHarvestResource( 0, 10.00, "", typeof( GammaCrystal ) ),
				new BonusHarvestResource( 0, 10.00, "", typeof( GammaCrystal ) ),
				new BonusHarvestResource( 0, 10.00, "", typeof( GammaCrystal ) ),
				new BonusHarvestResource( 0, 10.00, "", typeof( GammaCrystal ) ),
				new BonusHarvestResource( 0, 01.00, "You dig up a DragonKnight Token and put it in your pack.", typeof( DragonKnightToken) ),


			};
			



			Definitions.Add( crystal );
			#endregion Mining for crystal
			
            #region Mining for clay
			HarvestDefinition clay = m_Clay = new HarvestDefinition();

			// Resource banks are every 8x8 tiles
			clay.BankWidth = 4;
			clay.BankHeight = 4;

			// Every bank holds from 10 to 34 Clay
			clay.MinTotal = 0;
			clay.MaxTotal = 5;

			// A resource bank will respawn its content every 10 to 20 minutes
			clay.MinRespawn = TimeSpan.FromMinutes( 10.0 );
			clay.MaxRespawn = TimeSpan.FromMinutes( 20.0 );

			// Skill checking is done on the Mining skill
			clay.Skill = SkillName.Mining;

			// Set the list of harvestable tiles
			clay.Tiles = m_ClayTiles;

			// Players must be within 2 tiles to harvest
			clay.MaxRange = 3;

			// One sand per harvest action
			clay.ConsumedPerHarvest = 1;
			clay.ConsumedPerFeluccaHarvest = 2;

			// The digging effect
			clay.EffectActions = new int[]{ 11 };
			clay.EffectSounds = new int[]{ 0x1CA, 0x33B, 0x3E6 };
			clay.EffectCounts = new int[]{ 6 };
			clay.EffectDelay = TimeSpan.FromSeconds( 1.6 );
			clay.EffectSoundDelay = TimeSpan.FromSeconds( 0.9 );

			clay.NoResourcesMessage = "There is no clay here to mine."; // There is no clay here to mine.
			clay.DoubleHarvestMessage = "Someone has gotten to the clay before you."; // There is no Clay here to mine.
			clay.TimedOutOfRangeMessage = 503041; // You have moved too far away to continue mining.
			clay.OutOfRangeMessage = 500446; // That is too far away.
			clay.FailMessage = "You dig for a while but fail to find any usable clay."; // You dig for a while but fail to find any of sufficient quality for pottery
			clay.PackFullMessage = "Your backpack is full, so the clay you mined is lost." ; // Your backpack can't hold the clay, and it is lost!
			clay.ToolBrokeMessage = 1044038; // You have worn out your tool!

			res = new HarvestResource[]
				{
				new HarvestResource( 00.0, 00.0, 140.0, "You carefully dig up clay of sufficent quality to use.", typeof( ClayMud ) )
				};

			veins = new HarvestVein[]
				{
					new HarvestVein( 100.0, 0.0, res[0], null )
				};

			clay.Resources = res;
			clay.Veins = veins;
			clay.BonusResources = new BonusHarvestResource[]
			{
				new BonusHarvestResource( 0, 09.00, null, null ),//Nothing
				new BonusHarvestResource( 0, 10.00, "", typeof( ClayMud ) ),
				new BonusHarvestResource( 0, 10.00, "", typeof( ClayMud ) ),
				new BonusHarvestResource( 0, 10.00, "", typeof( ClayMud ) ),
				new BonusHarvestResource( 0, 10.00, "", typeof( ClayMud ) ),
				new BonusHarvestResource( 0, 10.00, "", typeof( ClayMud ) ),
				new BonusHarvestResource( 0, 10.00, "", typeof( ClayMud ) ),
				new BonusHarvestResource( 0, 10.00, "", typeof( ClayMud ) ),
				new BonusHarvestResource( 0, 10.00, "", typeof( ClayMud ) ),
				new BonusHarvestResource( 0, 10.00, "", typeof( ClayMud ) ),
				new BonusHarvestResource( 0, 01.00, "You dig up a DragonKnight Token and put it in your pack.", typeof( DragonKnightToken) ),


			};
			



			Definitions.Add( clay );
			#endregion Mining for clay
		
			
		}

		public override Type GetResourceType( Mobile from, Item tool, HarvestDefinition def, Map map, Point3D loc, HarvestResource resource )
		{
			if ( def == m_OreAndStone )
			{
				PlayerMobile pm = from as PlayerMobile;
				if ( pm != null && pm.StoneMining && pm.ToggleMiningStone && from.Skills[SkillName.Mining].Base >= 100.0 && 0.1 > Utility.RandomDouble() )
					return resource.Types[1];

				return resource.Types[0];
			}

			return base.GetResourceType( from, tool, def, map, loc, resource );
		}

		public override bool CheckHarvest( Mobile from, Item tool )
		{
			if ( !base.CheckHarvest( from, tool ) )
				return false;

			if ( from.Mounted )
			{
				from.SendLocalizedMessage( 501864 ); // You can't mine while riding.
				return false;
			}
			else if ( from.IsBodyMod && !from.Body.IsHuman )
			{
				from.SendLocalizedMessage( 501865 ); // You can't mine while polymorphed.
				return false;
			}

			return true;
		}

		public override void SendSuccessTo( Mobile from, Item item, HarvestResource resource )
		{
			if ( item is BaseGranite )
				from.SendLocalizedMessage( 1044606 ); // You carefully extract some workable stone from the ore vein!
			else
				base.SendSuccessTo( from, item, resource );
		}

		public override bool CheckHarvest( Mobile from, Item tool, HarvestDefinition def, object toHarvest )
		{
			if ( !base.CheckHarvest( from, tool, def, toHarvest ) )
				return false;

			if ( def == m_Sand && !(from is PlayerMobile && from.Skills[SkillName.Mining].Base >= 100.0 && ((PlayerMobile)from).SandMining) )
			{
				OnBadHarvestTarget( from, tool, toHarvest );
				return false;
			}
			else if ( from.Mounted )
			{
				from.SendLocalizedMessage( 501864 ); // You can't mine while riding.
				return false;
			}
			else if ( from.IsBodyMod && !from.Body.IsHuman )
			{
				from.SendLocalizedMessage( 501865 ); // You can't mine while polymorphed.
				return false;
			}

			return true;
		}

		public override HarvestVein MutateVein( Mobile from, Item tool, HarvestDefinition def, HarvestBank bank, object toHarvest, HarvestVein vein )
		{
			if ( tool is GargoylesPickaxe && def == m_OreAndStone )
			{
				int veinIndex = Array.IndexOf( def.Veins, vein );

				if ( veinIndex >= 0 && veinIndex < (def.Veins.Length - 1) )
					return def.Veins[veinIndex + 1];
			}

			return base.MutateVein( from, tool, def, bank, toHarvest, vein );
		}

		private static int[] m_Offsets = new int[]
			{
				-1, -1,
				-1,  0,
				-1,  1,
				 0, -1,
				 0,  1,
				 1, -1,
				 1,  0,
				 1,  1
			};

		public override void OnHarvestFinished( Mobile from, Item tool, HarvestDefinition def, HarvestVein vein, HarvestBank bank, HarvestResource resource, object harvested )
		{
			if ( tool is GargoylesPickaxe && def == m_OreAndStone && 0.1 > Utility.RandomDouble() )
			{
				HarvestResource res = vein.PrimaryResource;

				if ( res == resource && res.Types.Length >= 3 )
				{
					try
					{
						Map map = from.Map;

						if ( map == null )
							return;

						BaseCreature spawned = Activator.CreateInstance( res.Types[2], new object[]{ 25 } ) as BaseCreature;

						if ( spawned != null )
						{
							int offset = Utility.Random( 8 ) * 2;

							for ( int i = 0; i < m_Offsets.Length; i += 2 )
							{
								int x = from.X + m_Offsets[(offset + i) % m_Offsets.Length];
								int y = from.Y + m_Offsets[(offset + i + 1) % m_Offsets.Length];

								if ( map.CanSpawnMobile( x, y, from.Z ) )
								{
									spawned.OnBeforeSpawn( new Point3D( x, y, from.Z ), map );
									spawned.MoveToWorld( new Point3D( x, y, from.Z ), map );
									spawned.Combatant = from;
									return;
								}
								else
								{
									int z = map.GetAverageZ( x, y );

									if ( map.CanSpawnMobile( x, y, z ) )
									{
										spawned.OnBeforeSpawn( new Point3D( x, y, z ), map );
										spawned.MoveToWorld( new Point3D( x, y, z ), map );
										spawned.Combatant = from;
										return;
									}
								}
							}

							spawned.OnBeforeSpawn( from.Location, from.Map );
							spawned.MoveToWorld( from.Location, from.Map );
							spawned.Combatant = from;
						}
					}
					catch
					{
					}
				}
			}
		}

		public override bool BeginHarvesting( Mobile from, Item tool )
		{
			if ( !base.BeginHarvesting( from, tool ) )
				return false;

			from.SendLocalizedMessage( 503033 ); // Where do you wish to dig?
			return true;
		}

		public override void OnHarvestStarted( Mobile from, Item tool, HarvestDefinition def, object toHarvest )
		{
			base.OnHarvestStarted( from, tool, def, toHarvest );

			if ( Core.ML )
				from.RevealingAction();
		}

		public override void OnBadHarvestTarget( Mobile from, Item tool, object toHarvest )
		{
			if ( toHarvest is LandTarget )
				from.SendLocalizedMessage( 501862 ); // You can't mine there.
			else
				from.SendLocalizedMessage( 501863 ); // You can't mine that.
		}

		#region Tile lists
		private static int[] m_MountainAndCaveTiles = new int[]
			{
				220, 221, 222, 223, 224, 225, 226, 227, 228, 229,
				230, 231, 236, 237, 238, 239, 240, 241, 242, 243,
				244, 245, 246, 247, 252, 253, 254, 255, 256, 257,
				258, 259, 260, 261, 262, 263, 268, 269, 270, 271,
				272, 273, 274, 275, 276, 277, 278, 279, 286, 287,
				288, 289, 290, 291, 292, 293, 294, 296, 296, 297,
				321, 322, 323, 324, 467, 468, 469, 470, 471, 472,
				473, 474, 476, 477, 478, 479, 480, 481, 482, 483,
				484, 485, 486, 487, 492, 493, 494, 495, 543, 544,
				545, 546, 547, 548, 549, 550, 551, 552, 553, 554,
				555, 556, 557, 558, 559, 560, 561, 562, 563, 564,
				565, 566, 567, 568, 569, 570, 571, 572, 573, 574,
				575, 576, 577, 578, 579, 581, 582, 583, 584, 585,
				586, 587, 588, 589, 590, 591, 592, 593, 594, 595,
				596, 597, 598, 599, 600, 601, 610, 611, 612, 613,

				1010, 1741, 1742, 1743, 1744, 1745, 1746, 1747, 1748, 1749,
				1750, 1751, 1752, 1753, 1754, 1755, 1756, 1757, 1771, 1772,
				1773, 1774, 1775, 1776, 1777, 1778, 1779, 1780, 1781, 1782,
				1783, 1784, 1785, 1786, 1787, 1788, 1789, 1790, 1801, 1802,
				1803, 1804, 1805, 1806, 1807, 1808, 1809, 1811, 1812, 1813,
				1814, 1815, 1816, 1817, 1818, 1819, 1820, 1821, 1822, 1823,
				1824, 1831, 1832, 1833, 1834, 1835, 1836, 1837, 1838, 1839,
				1840, 1841, 1842, 1843, 1844, 1845, 1846, 1847, 1848, 1849,
				1850, 1851, 1852, 1853, 1854, 1861, 1862, 1863, 1864, 1865,
				1866, 1867, 1868, 1869, 1870, 1871, 1872, 1873, 1874, 1875,
				1876, 1877, 1878, 1879, 1880, 1881, 1882, 1883, 1884, 1981,
				1982, 1983, 1984, 1985, 1986, 1987, 1988, 1989, 1990, 1991,
				1992, 1993, 1994, 1995, 1996, 1997, 1998, 1999, 2000, 2001,
				2002, 2003, 2004, 2028, 2029, 2030, 2031, 2032, 2033, 2100,
				2101, 2102, 2103, 2104, 2105,

				0x453B, 0x453C, 0x453D, 0x453E, 0x453F, 0x4540, 0x4541,
				0x4542, 0x4543, 0x4544,	0x4545, 0x4546, 0x4547, 0x4548,
				0x4549, 0x454A, 0x454B, 0x454C, 0x454D, 0x454E,	0x454F
			};

		private static int[] m_SandTiles = new int[]
			{
				22, 23, 24, 25, 26, 27, 28, 29, 30, 31,
				32, 33, 34, 35, 36, 37, 38, 39, 40, 41,
				42, 43, 44, 45, 46, 47, 48, 49, 50, 51,
				52, 53, 54, 55, 56, 57, 58, 59, 60, 61,
				62, 68, 69, 70, 71, 72, 73, 74, 75,

				286, 287, 288, 289, 290, 291, 292, 293, 294, 295,
				296, 297, 298, 299, 300, 301, 402, 424, 425, 426,
				427, 441, 442, 443, 444, 445, 446, 447, 448, 449,
				450, 451, 452, 453, 454, 455, 456, 457, 458, 459,
				460, 461, 462, 463, 464, 465, 642, 643, 644, 645,
				650, 651, 652, 653, 654, 655, 656, 657, 821, 822,
				823, 824, 825, 826, 827, 828, 833, 834, 835, 836,
				845, 846, 847, 848, 849, 850, 851, 852, 857, 858,
				859, 860, 951, 952, 953, 954, 955, 956, 957, 958,
				967, 968, 969, 970,

				1447, 1448, 1449, 1450, 1451, 1452, 1453, 1454, 1455,
				1456, 1457, 1458, 1611, 1612, 1613, 1614, 1615, 1616,
				1617, 1618, 1623, 1624, 1625, 1626, 1635, 1636, 1637,
				1638, 1639, 1640, 1641, 1642, 1647, 1648, 1649, 1650
			};
		
		
		#region Crystal Tile lists
		private static int[] m_CrystalTiles = new int[]
		{
			0x6206, 0x6207, 0x6208, 0x6209, 0x620A, 0x620B, 0x620C, 0x620D,
			0x620E, 0x6210, 0x6211, 0x6212, 0x6213, 0x6214, 0x6215, 0x6216,
			0x6217, 0x6218, 0x621A, 0x621B, 0x621C, 0x621D, 0x621E, 0x621F,
			0x6220, 0x6221, 0x6222, 0x6224, 0x6225, 0x6226, 0x6227, 0x6228,
			0x6229, 0x622A, 0x622B, 0x622C,

			0x623A, 0x623B, 0x623C, 0x623D, 0x623E, 0x623F,
			0x6240, 0x6241, 0x6242, 0x6243, 0x6244, 0x6245, 0x6246, 0x6247, 0x6248, 0x6249,

			0x2206, 0x2207,0x2208,0x2209,0x220A,0x220B,0x220C,0x220D,
			0x220E, 0x2210, 0x2211, 0x2212, 0x2213, 0x2214, 0x2215, 0x2216,
			0x2217, 0x2218, 0x221A, 0x221B, 0x221C, 0x221D, 0x221E, 0x221F,
			0x2220, 0x2221, 0x2222, 0x2224, 0x2225, 0x2226, 0x2227, 0x2228,
			0x2229, 0x222A, 0x222B, 0x222C,
			0x223A, 0x223B, 0x223C, 0x223D, 0x223E, 0x223F,
			0x2240, 0x2241, 0x2242, 0x2243, 0x2244, 0x2245, 0x2246, 0x2247, 0x2248, 0x2249
		};
		#endregion Crystal Tile lists

		
		#region Clay Tile lists
		private static int[] m_ClayTiles = new int[]
		{
			15717, 15808, 15809, 15810, 15811, 15812,
			15813, 15814, 15815, 15816, 15817, 15818,
			15819, 15820, 15821, 15822, 15823, 15824,
			15825, 15826, 15827, 15828, 15829, 15830,
			15831, 15832, 15833, 15835, 15836, 15838,
			15839, 15840, 15831, 15842, 15843, 15844,
			15845, 15846, 15847, 15848, 15849, 15850,
			15851, 15852, 15853, 15854, 15855, 15856,
			15857, 16112
		};
        #endregion Clay Tile lists

		
		#endregion
	}
}
