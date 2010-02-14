using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Gumps;
using Server.Network;
using Server.ContextMenus;

namespace Server.Mobiles
{
	[CorpseName( "a giant Pickup beetle corpse" )]
	public class ThePickUpBeetle : BaseMount
	{
		[Constructable]
		public ThePickUpBeetle() : this( "a pickup beetle" )
		{
		}

		public override bool SubdueBeforeTame{ get{ return true; } } // Must be beaten into submission

		[Constructable]
		public ThePickUpBeetle( string name ) : base( name, 0x317, 0x3EBC, AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			SetStr( 000 );
			SetDex( 110 );
			SetInt( 500 );

		    SetHits( 200 );
		    
			SetStam( 110 );
			SetMana( 100 );

			SetDamage( 7, 20 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 50, 60 );
			SetResistance( ResistanceType.Fire, 50, 55 );
			
			SetResistance( ResistanceType.Poison, 25, 30 );
			SetResistance( ResistanceType.Energy, 25, 30 );

			SetSkill( SkillName.Wrestling, 70.1, 75.0 );
			SetSkill( SkillName.Tactics, 50.0 );
			

			Tamable = true;
			ControlSlots = 2;
			MinTameSkill = 80.0;

			Container pack = Backpack;

			if ( pack != null )
				pack.Delete();

			pack = new StrongBackpack();
			pack.Movable = false;

			AddItem( pack );
		}

        private DateTime m_NextPickup;

		public override void OnThink()
		{
			base.OnThink();

			if ( DateTime.Now < m_NextPickup )
				return;

			m_NextPickup = DateTime.Now + TimeSpan.FromSeconds( Utility.RandomMinMax( 0, 0 ) );

			Container pack = this.Backpack;

			if ( pack == null )
				return;

			ArrayList list = new ArrayList();

			foreach ( Item item in this.GetItemsInRange( 3 ) )
			{
				if ( item.Movable && item.Stackable )
					list.Add( item );
			}

			int pickedUp = 0;

			for ( int i = 0; i < list.Count; ++i )
			{
				Item item = (Item)list[i];

				if ( !pack.CheckHold( this, item, false, true ) )
					return;

				bool rejected;
				LRReason reject;

				NextActionTime = DateTime.Now;

				Lift( item, item.Amount, out rejected, out reject );

				if ( rejected )
					continue;

				Drop( this, Point3D.Zero );

				if ( ++pickedUp == 3 )
					break;
         }
	                
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
                
        public override void OnDoubleClick( Mobile from )
		{
			PackAnimal.TryPackOpen( this, from );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );

			PackAnimal.GetContextMenuEntries( this, from, list );
		}
		#endregion

                public ThePickUpBeetle( Serial serial ) : base( serial )
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
