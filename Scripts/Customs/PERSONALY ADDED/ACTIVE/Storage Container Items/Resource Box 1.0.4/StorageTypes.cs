/***************************************************************************/
/*			ResourceBox.cs | ResourceBoxGump.cs | StorageTypes.cs          */
/*							Created by A_Li_N                              */
/*				Credits :                                                  */
/*						Original Gump Layout - Lysdexic                    */
/*						Hashtable help - UOT and daat99                    */
/***************************************************************************/
/*	Addition of different Resources :                                      */
/*		To add/remove resource types from the box, simply put the Type of  */
/*		the resource in the catagory you wish it to be in.  Each catagory  */
/*		can hold up to 32 entries without messing a LOT with the gump.     */
/*	Removing of Resources :                                                */
/*		Commenting out or deleting the type you wish to remove will remove */
/*		the type AND the amount each Resource Box contains.                */
/***************************************************************************/

using System;

namespace Server.Items
{
	public class StorageTypes
	{
		
		
		
		private static Type[] m_Logs = new Type[]
		{
			typeof( AshLog ),
			typeof( BloodwoodLog ),
			typeof( FrostwoodLog ),
			typeof( HeartwoodLog ),
			typeof( Log ),
			typeof( OakLog ),
			typeof( YewLog ),
		};
		public static Type[] Logs{ get{ return m_Logs; } }
		
		
		
		private static Type[] m_Boards = new Type[]
		{
			typeof( AshBoard ),
			typeof( BloodwoodBoard ),
			typeof( Board ),
			typeof( FrostwoodBoard ),
			typeof( HeartwoodBoard ),
			typeof( OakBoard ),
			typeof( YewBoard ),
		};
		public static Type[] Boards{ get{ return m_Boards; } }
		
		
		
		private static Type[] m_Ingots = new Type[]
		{
			typeof( AgapiteIngot ),
			typeof( BronzeIngot ),
			typeof( CopperIngot ),
			typeof( DullCopperIngot ),
			typeof( GoldIngot ),
			typeof( IronIngot ),
			typeof( ShadowIronIngot ),
			typeof( ValoriteIngot ),
			typeof( VeriteIngot ),
		};
		public static Type[] Ingots{ get{ return m_Ingots; } }
		
		
		
		private static Type[] m_Granites = new Type[]
		{
			typeof( AgapiteGranite ),
			typeof( BronzeGranite ),
			typeof( CopperGranite ),
			typeof( DullCopperGranite ),
			typeof( GoldGranite ),
			typeof( Granite ),
			typeof( ShadowIronGranite ),
			typeof( ValoriteGranite ),
			typeof( VeriteGranite ),
		};
		public static Type[] Granites{ get{ return m_Granites; } }
		
		
		
		private static Type[] m_Scales = new Type[]
		{
			typeof( BlackScales ),
			typeof( BlueScales ),
			typeof( GreenScales ),
			typeof( HydraScale ),
			typeof( RedScales ),
			typeof( WhiteScales ),
			typeof( YellowScales ),
		};
		public static Type[] Scales{ get{ return m_Scales; } }
		
		
		
		private static Type[] m_Leathers = new Type[]
		{
			typeof( Leather ),
			typeof( SpinedLeather ),
			typeof( HornedLeather ),
			typeof( BarbedLeather ),
		};
		public static Type[] Leathers{ get{ return m_Leathers; } }
		
		
		
		private static Type[] m_Misc = new Type[]
		{
			typeof( Sand ),

			typeof( Bandage ),
			typeof( BoltOfCloth ),
			typeof( Cloth ),
			typeof( Cotton ),
			typeof( DarkYarn ),
			typeof( Flax ),
			typeof( SpoolOfThread ),
			typeof( UncutCloth ),
			typeof( Wool ),
			typeof( Beeswax ),
			typeof( BlankScroll ),
			typeof( Bone ),
			typeof( Bottle ),
			typeof( FertileDirt ),
			typeof( FishSteak ),
			typeof( KeyRing ),
			typeof( Kindling ),
			typeof( Lockpick ),
			typeof( OilCloth ),
			typeof( PotionKeg ),
			typeof( RawFishSteak ),
			typeof( RecallRune),
			typeof( Arrow ),
			typeof( Bolt ),
			typeof( Feather ),
			typeof( Shaft ),
		};
		public static Type[] Misc{ get{ return m_Misc; } }
		
		
		
		private static Type[] m_Reagents = new Type[]
		{
			typeof( BatWing ),
			typeof( BlackPearl ),
			typeof( Bloodmoss ),
			typeof( DaemonBlood ),
			typeof( DaemonBone ),
			typeof( DeadWood ),
			typeof( Garlic ),
			typeof( Ginseng ),
			typeof( GraveDust ),
			typeof( MandrakeRoot ),
			typeof( Nightshade ),
			typeof( NoxCrystal ),
			typeof( PigIron ),
			typeof( SpidersSilk ),
			typeof( SulfurousAsh ),
		};
		public static Type[] Reagents{ get{ return m_Reagents; } }
		
		
		
		private static Type[] m_Gems = new Type[]
		{
			typeof( Amber ),
			typeof( Amethyst ),
			typeof( BlueDiamond ),
			typeof( BrilliantAmber ),
			typeof( Citrine ),
			typeof( DarkSapphire ),
			typeof( Diamond ),
			typeof( EcruCitrine ),
			typeof( Emerald ),
			typeof( FireRuby ),
			typeof( PerfectEmerald ),
			typeof( Ruby ),
			typeof( Sapphire ),
			typeof( StarSapphire ),
			typeof( Tourmaline ),
			typeof( Turquoise ),
			typeof( WhitePearl )
		};
		public static Type[] Gems{ get{ return m_Gems; } }
	}
}
