using System;
using Server;
using Server.Misc;
using Server.Network;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	public class DavidBanner : BaseCreature
	{

		public override bool ShowFameTitle{ get{ return false; } }
                 
                 private static bool m_Talked; // flag to prevent spam 

      string[] kfcsay = new string[] // things to say while greating 
      { 
         "Get out of here!",
         "Don't make me angry...",
         "You wouldn't like me when I'm angry.",
         "You wont like me angry.",
         }; 
		[Constructable]
		public DavidBanner():base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Body = 400;
			HairItemID = 8253;
			HairHue = 33780;
			Name = "Doctor David Banner";
			Hue = 33780;

			SetStr( 356, 396 );
			SetDex( 105, 135 );
			SetInt( 530, 653 );
                        SetHits( 73 );
			SetSkill( SkillName.Wrestling, 121.1, 128.1 );
			SetSkill( SkillName.Tactics, 91.5, 99.0 );
			SetSkill( SkillName.MagicResist, 90.6, 96.8);
			SetSkill( SkillName.Anatomy, 100.1, 100.1 );
			
			AddItem(new ShortHair(1044));
			AddItem( new FancyShirt( 137 ) );
			AddItem( new Boots( 1816 ) );
			AddItem( new ShortPants( 1264 ) );
			
			VirtualArmor = 36;
			SetFameLevel( 4 );
			SetKarmaLevel( 4 );


		}

//		public override bool AlwaysMurderer{ get{ return false; } }
		
		[CommandProperty( AccessLevel.GameMaster )]
		public override int HitsMax { get { return 753; } }

		public DavidBanner( Serial serial ) : base( serial )
		{
		}

                public override void OnMovement( Mobile m, Point3D oldLocation ) 
               {                                                    
         if( m_Talked == false ) 
         { 
            if ( m.InRange( this, 10 ) ) 
            {                
               m_Talked = true; 
               SayRandom( kfcsay, this ); 
               this.Move( GetDirectionTo( m.Location ) ); 
                  // Start timer to prevent spam 
               SpamTimer t = new SpamTimer(); 
               t.Start(); 
            } 
         } 
      } 

      private class SpamTimer : Timer 
      { 
         public SpamTimer() : base( TimeSpan.FromSeconds( 4 ) ) 
         { 
            Priority = TimerPriority.OneSecond; 
         } 

         protected override void OnTick() 
         { 
            m_Talked = false; 
         } 
      } 

      private static void SayRandom( string[] say, Mobile m ) 
      { 
         m.Say( say[Utility.Random( say.Length )] ); 
      } 

		public override bool OnBeforeDeath()
		{
                       

                        IncredibleHulk rm = new IncredibleHulk();

			rm.Team = this.Team;
			rm.MoveToWorld( this.Location, this.Map );

			Effects.SendLocationEffect( Location,Map, 0x3709, 13, 0x3B2, 0 );

			Container bag = new Bag();

			switch ( Utility.Random( 9 ))
			{
				case 0: bag.DropItem( new Amber() ); break;
				case 1: bag.DropItem( new Amethyst() ); break;
				case 2: bag.DropItem( new Citrine() ); break;
				case 3: bag.DropItem( new Diamond() ); break;
				case 4: bag.DropItem( new Emerald() ); break;
				case 5: bag.DropItem( new Ruby() ); break;
				case 6: bag.DropItem( new Sapphire() ); break;
				case 7: bag.DropItem( new StarSapphire() ); break;
				case 8: bag.DropItem( new Tourmaline() ); break;
			}

			switch ( Utility.Random( 8 ))
			{
				case 0: bag.DropItem( new SpidersSilk( 3 ) ); break;
				case 1: bag.DropItem( new BlackPearl( 3 ) ); break;
				case 2: bag.DropItem( new Bloodmoss( 3 ) ); break;
				case 3: bag.DropItem( new Garlic( 3 ) ); break;
				case 4: bag.DropItem( new MandrakeRoot( 3 ) ); break;
				case 5: bag.DropItem( new Nightshade( 3 ) ); break;
				case 6: bag.DropItem( new SulfurousAsh( 3 ) ); break;
				case 7: bag.DropItem( new Ginseng( 3 ) ); break;
			}
 
			bag.DropItem( new Gold( 1000, 1500 ) );
			rm.AddItem( bag );
                       
			this.Delete();

			return false;
		}

                public override int GetAngerSound()
		{
			return 0x2F8;
		}

		public override int GetIdleSound()
		{
			return 0x2F8;
		}

		public override int GetAttackSound()
		{
			return Utility.Random( 0x2F5, 2 );
		}

		public override int GetHurtSound()
		{
			return 0x2F9;
		}

		public override int GetDeathSound()
		{
			return 0x2F7;
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
