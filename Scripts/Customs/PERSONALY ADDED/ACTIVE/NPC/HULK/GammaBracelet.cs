using System;
using Server;
using Server.Items;

namespace Server.Items
{
   public class GammaBracelet : SilverBracelet
   {

      public override int ArtifactRarity{ get{ return 68; } }

      [Constructable]
      public GammaBracelet()
      {
         Weight = 5.0;
         Name = "Bracelet of Gamma Rays";
         Layer = Layer.Bracelet;
         Hue = 70;
      }

      public override void OnDoubleClick( Mobile m )
      {
         if( Parent != m )
         {
            m.SendMessage( "You must be wearing the bracelet to use it!" );
         }
         else
         {
            if ( m.Body == 400 )
            {
               m.SendMessage( "You feel yourself changing." );
               m.PlaySound( 232 );
               m.BodyMod = 83;
               m.Hue =2212;
               Attributes.BonusStr = 75;
               Attributes.BonusInt = -50;
               Attributes.BonusDex = 65;
               m.NameMod = "the Incredible Hulk";
               m.RemoveItem(this);
               m.EquipItem(this);
               if( m.Kills >= 5)
               {
               m.Criminal = true;
                }
                if( m.GuildTitle != null)
               {
                  m.DisplayGuildTitle = true;
                }
            }
            else if ( m.Body == 83 )
            {
               m.SendMessage( "You feel yourself changing." );
               m.PlaySound( 900 );
               m.Body = 400;
               m.BodyMod = 0x0;
               m.Hue = 33780;
               Attributes.BonusStr = 0;
               Attributes.BonusInt = 0;
               Attributes.BonusDex = 0;
               m.NameMod = null;
               m.DisplayGuildTitle = false;
               m.Criminal = false;
               m.RemoveItem(this);
               m.EquipItem(this);
            }
            else if ( m.Body == 401 )
            {
               m.SendMessage( "You feel yourself changing." );
               m.PlaySound( 232 );
               m.BodyMod = 1;
               m.Hue = 2212;
               Attributes.BonusStr = 75;
               Attributes.BonusInt = -50;
               Attributes.BonusDex = 60;
               m.NameMod = "the Incredible Hulk";
               m.DisplayGuildTitle = false;
               m.Criminal = false;
               m.RemoveItem(this);
               m.EquipItem(this);
            }
            else if ( m.Body == 1 )
            {
               m.SendMessage( "You feel yourself changing." );
               m.PlaySound( 900 );
               m.Body = 401;
               m.BodyMod = 0x0;
               m.Hue = 33780;
               m.Hits = m.HitsMax ;
               m.Mana = m.ManaMax ;
               m.Stam = m.StamMax ;
               m.NameMod = null;
               m.DisplayGuildTitle = false;
               m.Criminal = false;
               m.RemoveItem(this);
               m.EquipItem(this);
            }

         }
      }


      public override void OnRemoved( Object o )
      {
      if( o is Mobile )
      {
          ((Mobile)o).NameMod = null;
      }
      if( o is Mobile && ((Mobile)o).Kills >= 5)
               {
               ((Mobile)o).Criminal = true;
                }
      if( o is Mobile && ((Mobile)o).GuildTitle != null )
               {
          ((Mobile)o).DisplayGuildTitle = true;
                }
      base.OnRemoved( o );
      }

      public GammaBracelet( Serial serial ) : base( serial )
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
