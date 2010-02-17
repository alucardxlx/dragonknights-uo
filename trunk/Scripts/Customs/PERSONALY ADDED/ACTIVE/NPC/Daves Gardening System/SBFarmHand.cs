using System; 
using System.Collections; 
using Server.Items; 
using Server.Items.Crops;

namespace Server.Mobiles 
{ 
	public class SBFarmHand : SBInfo 
	{ 
		private ArrayList m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBFarmHand() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override ArrayList BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : ArrayList 
		{ 
			public InternalBuyInfo() 
			{ 
				Add( new GenericBuyInfo( typeof( Apple ), 3, 20, 0x9D0, 0 ) );
				Add( new GenericBuyInfo( typeof( Grapes ), 3, 20, 0x9D1, 0 ) );
				Add( new GenericBuyInfo( typeof( Watermelon ), 7, 20, 0xC5C, 0 ) );
				Add( new GenericBuyInfo( typeof( YellowGourd ), 3, 20, 0xC64, 0 ) );
				Add( new GenericBuyInfo( typeof( Pumpkin ), 11, 20, 0xC6A, 0 ) );
				Add( new GenericBuyInfo( typeof( Onion ), 3, 20, 0xC6D, 0 ) );
				Add( new GenericBuyInfo( typeof( Lettuce ), 5, 20, 0xC70, 0 ) );
				Add( new GenericBuyInfo( typeof( Squash ), 3, 20, 0xC72, 0 ) );
				Add( new GenericBuyInfo( typeof( HoneydewMelon ), 7, 20, 0xC74, 0 ) );
				Add( new GenericBuyInfo( typeof( Carrot ), 3, 20, 0xC77, 0 ) );
				Add( new GenericBuyInfo( typeof( Cantaloupe ), 6, 20, 0xC79, 0 ) );
				Add( new GenericBuyInfo( typeof( Cabbage ), 5, 20, 0xC7B, 0 ) );
				//Add( new GenericBuyInfo( typeof( EarOfCorn ), 3, 20, XXXXXX, 0 ) );
				//Add( new GenericBuyInfo( typeof( Turnip ), 6, 20, XXXXXX, 0 ) );
				//Add( new GenericBuyInfo( typeof( SheafOfHay ), 2, 20, XXXXXX, 0 ) );
				Add( new GenericBuyInfo( typeof( Lemon ), 3, 20, 0x1728, 0 ) );
				Add( new GenericBuyInfo( typeof( Lime ), 3, 20, 0x172A, 0 ) );
				Add( new GenericBuyInfo( typeof( Peach ), 3, 20, 0x9D2, 0 ) );
				Add( new GenericBuyInfo( typeof( Pear ), 3, 20, 0x994, 0 ) );
				Add( new GenericBuyInfo( "Bloodmoss Seed", typeof( BloodmossSeed ), 150, 20, 0xF27, 0x5E2 ) );
				Add( new GenericBuyInfo( "Cabbage Seed", typeof( CabbageSeed ), 150, 20, 0xF27, 0x5E2 ) );
				Add( new GenericBuyInfo( "Carrot Seed", typeof( CarrotSeed ), 150, 20, 0xF27, 0x5E2 ) );
				Add( new GenericBuyInfo( "Corn Seed", typeof( CornSeed ), 150, 20, 0xF27, 0x5E2 ) );
				Add( new GenericBuyInfo( "Cotton Seed", typeof( CottonSeed ), 150, 20, 0xF27, 0x5E2 ) );
				Add( new GenericBuyInfo( "Flax Seed", typeof( FlaxSeed ), 150, 20, 0xF27, 0x5E2 ) );
				Add( new GenericBuyInfo( "Garlic Seed", typeof( GarlicSeed ), 150, 20, 0xF27, 0x5E2 ) );
				Add( new GenericBuyInfo( "Ginseng Seed", typeof( GinsengSeed ), 150, 20, 0xF27, 0x5E2 ) );
				Add( new GenericBuyInfo( "Grapes Seed", typeof( GrapesSeed ), 150, 20, 0xF27, 0x5E2 ) );
				Add( new GenericBuyInfo( "Lettuce Seed", typeof( LettuceSeed ), 150, 20, 0xF27, 0x5E2 ) );
				Add( new GenericBuyInfo( "MandrakeRoot Seed", typeof( MandrakeRootSeed ), 150, 20, 0xF27, 0x5E2 ) );
				Add( new GenericBuyInfo( "Nightshade Seed", typeof( NightshadeSeed ), 150, 20, 0xF27, 0x5E2 ) );
				Add( new GenericBuyInfo( "Onion Seed", typeof( OnionSeed ), 150, 20, 0xF27, 0x5E2 ) );
				Add( new GenericBuyInfo( "Pumpkin Seed", typeof( PumpkinSeed ), 150, 20, 0xF27, 0x5E2 ) );
				Add( new GenericBuyInfo( "Wheat Seed", typeof( WheatSeed ), 150, 20, 0xF27, 0x5E2 ) );
				
				
			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 
				Add( typeof( Apple ), 1 );
				Add( typeof( Grapes ), 1 );
				Add( typeof( Watermelon ), 3 );
				Add( typeof( YellowGourd ), 1 );
				Add( typeof( Pumpkin ), 5 );
				Add( typeof( Onion ), 1 );
				Add( typeof( Lettuce ), 2 );
				Add( typeof( Squash ), 1 );
				Add( typeof( Carrot ), 1 );
				Add( typeof( HoneydewMelon ), 3 );
				Add( typeof( Cantaloupe ), 3 );
				Add( typeof( Cabbage ), 2 );
				Add( typeof( Lemon ), 1 );
				Add( typeof( Lime ), 1 );
				Add( typeof( Peach ), 1 );
				Add( typeof( Pear ), 1 );
			} 
		} 
	} 
}
