using System; 
using System.Collections; 
using Server.Items;
using System.Collections.Generic; 

namespace Server.Mobiles 
{ 
	public class SBgroomer : SBInfo 
	{ 
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo(); 
		private IShopSellInfo m_SellInfo = new InternalSellInfo(); 

		public SBgroomer() 
		{ 
		} 

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } } 
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } } 

		public class InternalBuyInfo : List<GenericBuyInfo> 
		{ 
			public InternalBuyInfo() 
			{ 
				Add( new GenericBuyInfo( typeof( PetColorResetToDefaultPotion ), 100000, 20, 3836, 1150 ) );
				Add( new GenericBuyInfo( typeof( spetdyepi ), 100000, 20, 3836, 1172 ) );
				Add( new GenericBuyInfo( typeof( spetdyep ), 100000, 20, 3836, 1278 ) );
				Add( new GenericBuyInfo( typeof( spetdyebv ), 100000, 20, 3836, 1282 ) );
				Add( new GenericBuyInfo( typeof( spetdyeb ), 100000, 20, 3836, 1266 ) );
				Add( new GenericBuyInfo( typeof( spetdyeg ), 100000, 20, 3836, 1285 ) );
				Add( new GenericBuyInfo( typeof( spetdyey ), 100000, 20, 3836, 1281 ) );
				Add( new GenericBuyInfo( typeof( spetdyef ), 100000, 20, 3836, 1260 ) );
				Add( new GenericBuyInfo( typeof( spetdyeo ), 100000, 20, 3836, 1259 ) );
				Add( new GenericBuyInfo( typeof( spetdyer ), 100000, 20, 3836, 1157 ) );
				Add( new GenericBuyInfo( typeof( petdye ), 100000, 20, 3836, 1175 ) );
				Add( new GenericBuyInfo( typeof( spetdye ), 100000, 20, 3836, 1153 ) );
				Add( new GenericBuyInfo( typeof( petdyer ), 50000, 20, 3836, 38 ) );
				Add( new GenericBuyInfo( typeof( petdyeo ), 50000, 20, 3836, 44 ) );
				Add( new GenericBuyInfo( typeof( petdyey ), 50000, 20, 3836, 53 ) );
				Add( new GenericBuyInfo( typeof( petdyeg ), 50000, 20, 3836, 1267 ) );
				Add( new GenericBuyInfo( typeof( petdyeb ), 50000, 20, 3836, 2 ) );
				Add( new GenericBuyInfo( typeof( petdyep ), 50000, 20, 3836, 1275 ) );
				Add( new GenericBuyInfo( typeof( petdyebr ), 50000, 20, 3836, 1256 ) );
				Add( new GenericBuyInfo( typeof( petdyebl ), 50000, 20, 3836, 1109 ) );
				Add( new GenericBuyInfo( typeof( petdyegr ), 50000, 20, 3836, 1102 ) );
				Add( new GenericBuyInfo( typeof( petdyew ), 50000, 20, 3836, 1150 ) );
				Add( new GenericBuyInfo( typeof( petdyepi ), 50000, 20, 3836, 1207 ) );
				Add( new GenericBuyInfo( typeof( cpetdyew ), 10000, 20, 3836, 1154 ) );
				Add( new GenericBuyInfo( typeof( cpetdyeib ), 10000, 20, 3836, 1151 ) );
				Add( new GenericBuyInfo( typeof( cpetdyegr ), 10000, 20, 3836, 1105 ) );
				Add( new GenericBuyInfo( typeof( cpetdyey ), 10000, 20, 3836, 56 ) );
				Add( new GenericBuyInfo( typeof( cpetdyefb ), 10000, 20, 3836, 1121 ) );
				Add( new GenericBuyInfo( typeof( cpetdyeb ), 10000, 20, 3836, 1126 ) );
				Add( new GenericBuyInfo( typeof( cpetdyebr ), 10000, 20, 3836, 1141 ) );
			} 
		} 

		public class InternalSellInfo : GenericSellInfo 
		{ 
			public InternalSellInfo() 
			{ 
				Add( typeof( PetColorResetToDefaultPotion ), 100000 );
				Add( typeof( spetdyeb ), 50000 );
				Add( typeof( spetdyepi ), 50000 );
				Add( typeof( spetdyep ), 50000 );
				Add( typeof( spetdyebv ), 50000 );
				Add( typeof( spetdyeg ), 50000 );
				Add( typeof( spetdyey ), 50000 );
				Add( typeof( spetdyef ), 50000 );
				Add( typeof( spetdyeo ), 50000 );
				Add( typeof( spetdyer ), 50000 );
				Add( typeof( petdye ), 50000 );
				Add( typeof( spetdye ), 50000 );
				Add( typeof( cpetdyeib ), 5000 );
				Add( typeof( cpetdyew ), 5000 );
				Add( typeof( cpetdyegr ), 5000 );
				Add( typeof( cpetdyey ), 5000 );
				Add( typeof( cpetdyefb ), 5000 );
				Add( typeof( cpetdyeb ), 5000 );
				Add( typeof( cpetdyebr ), 5000 );
				Add( typeof( petdyer ), 25000 );
				Add( typeof( petdyeo ), 25000 );
				Add( typeof( petdyey ), 25000 );
				Add( typeof( petdyeg ), 25000 );
				Add( typeof( petdyeb ), 25000 );
				Add( typeof( petdyep ), 25000 );
				Add( typeof( petdyebr ), 25000 );
				Add( typeof( petdyebl ), 25000 );
				Add( typeof( petdyegr ), 25000 );
				Add( typeof( petdyew ), 25000 );
				Add( typeof( petdyepi ), 25000 );
				
			} 
		} 
	} 
}
