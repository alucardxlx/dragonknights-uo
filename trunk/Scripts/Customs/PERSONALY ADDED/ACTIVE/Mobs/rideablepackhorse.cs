using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.ContextMenus;
using Server.Items;
using Server.Gumps;
using Server.Network;

namespace Server.Mobiles
{       
	[CorpseName( "A Rideable Pack Horse Corpse" )]
	public class rideablepackhorse : BaseMount

	{

	       [Constructable]
	       public rideablepackhorse() : this( "A Rideable Pack Horse" )
	       {
	       }
 
               [Constructable]
		public rideablepackhorse( string name ) : base( name, 0x123, 0x3E9F, AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			BaseSoundID = 0xA8;

			SetStr( 44, 120 );
			SetDex( 36, 55 );
			SetInt( 6, 10 );

			SetHits( 61, 80 );
			SetStam( 81, 100 );
			SetMana( 0 );

			SetDamage( 5, 11 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 20, 25 );
			SetResistance( ResistanceType.Fire, 10, 15 );
			SetResistance( ResistanceType.Poison, 10, 15 );
			SetResistance( ResistanceType.Energy, 10, 15 );
			SetSkill( SkillName.MagicResist, 25.1, 30.0 );
			SetSkill( SkillName.Wrestling, 29.3, 44.0 );
			SetSkill( SkillName.Tactics, 29.3, 44.0 );

			
			Tamable = true; 
         		ControlSlots = 1; 
         		MinTameSkill = 0.0;
			Fame = 0;
			Karma = 200;

			VirtualArmor = 16;


			Container pack = Backpack;

			if ( pack != null )
				pack.Delete();

			pack = new StrongBackpack();
			pack.Movable = false;

			AddItem( pack );
			PackItem( new Gold( 100 ) );
			//PackItem( new KevinD() );


		}

		#region Pack Animal Methods
		public override bool OnBeforeDeath()
		{
			if ( !base.OnBeforeDeath() )
				return false;

			PackAnimal.CombineBackpacks( this );

			return true;
		}

		public override bool IsSnoop( Mobile from )
		{
			if ( PackAnimal.CheckAccess( this, from ) )
				return false;

			return base.IsSnoop( from );
		}

		public override bool OnDragDrop( Mobile from, Item item )
		{
			if ( CheckFeed( from, item ) )
				return true;

			if ( PackAnimal.CheckAccess( this, from ) )
			{
				AddToBackpack( item );
				return true;
			}

			return base.OnDragDrop( from, item );
		}

		public override bool CheckNonlocalDrop( Mobile from, Item item, Item target )
		{
			return PackAnimal.CheckAccess( this, from );
		}

		public override bool CheckNonlocalLift( Mobile from, Item item )
		{
			return PackAnimal.CheckAccess( this, from );
		}

		//public override void OnDoubleClick( Mobile from )
		//{
			//PackAnimal.TryPackOpen( this, from );
		//}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );

			PackAnimal.GetContextMenuEntries( this, from, list );
		}
		#endregion
//I added
		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 12; } }
		public override FoodType FavoriteFood{ get{ return FoodType.FruitsAndVegies | FoodType.GrainsAndHay; } }


//

		public rideablepackhorse( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
