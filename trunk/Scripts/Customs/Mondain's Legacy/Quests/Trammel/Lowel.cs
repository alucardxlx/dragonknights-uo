using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Quests
{
	public class ComfortableSeatingQuest : BaseQuest
	{					
		/* Comfortable Seating */
		public override object Title{ get{ return 1075517; } }
		
		/* Hail friend, hast thou a moment? A mishap with a saw hath left me in a sorry state, for it shall be a while 
		before I canst return to carpentry. In the meantime, I need a comfortable chair that I may rest. Could thou craft 
		a straw chair?  Only a tool, such as a dovetail saw, a few boards, and some skill as a carpenter is needed. Remember, 
		this is a piece of furniture, so please pay attention to detail. */	
		public override object Description{ get{ return 1075518; } }
		
		/* I quite understand your reluctance.  If you reconsider, I'll be here. */
		public override object Refuse{ get{ return 1072687; } }
		
		/* Is all going well? I look forward to the simple comforts in my very own home. */
		public override object Uncomplete{ get{ return 1075509; } }
		
		/* This is perfect! */
		public override object Complete{ get{ return 1074720; } }
		
		public ComfortableSeatingQuest() : base()
		{								
			AddObjective( new ObtainObjective( typeof( BambooChair ), "straw chair", 1, 0xB5B ) );
						
			AddReward( new BaseReward( typeof( CarpentersSatchel ), 1074282 ) ); // Craftsman's Satchel
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}		
	}
	
	public class Lowel : MondainQuester
	{
		public override Type[] Quests
		{ 
			get{ return new Type[] 
			{ 
				typeof( ComfortableSeatingQuest )
			};} 
		}		
		
		[Constructable]
		public Lowel() : base( "Lowel", "the carpenter" )
		{			
		}
		
		public Lowel( Serial serial ) : base( serial )
		{
		}		
		
		public override void InitBody()
		{
			InitStats( 100, 100, 25 );
			
			Female = false;
			CantWalk = true;
			Race = Race.Human;
			
			Hue = 0x83F6;
			HairItemID = 0x203C;
			HairHue = 0x6B1;			
		}
		
		public override void InitOutfit()
		{
			AddItem( new Backpack() );		
			AddItem( new Boots( 0x543 ) );
			AddItem( new ShortPants( 0x758 ) );
			AddItem( new FancyShirt( 0x53A ) );
			AddItem( new HalfApron( 0x6D2 ) );
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
	
	public class CarpentersSatchel : Backpack
	{
		[Constructable]
		public CarpentersSatchel() : base()
		{
			Hue = BaseReward.SatchelHue();
			
			AddItem( new Board( 10 ) );
			AddItem( new DovetailSaw() );
		}
		
		public CarpentersSatchel( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}