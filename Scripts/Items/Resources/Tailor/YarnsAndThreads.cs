using System;
using Server.Items;
using Server.Targeting;
using Server.Engines.XmlSpawner2;

namespace Server.Items
{
	public abstract class BaseClothMaterial : Item, IDyable
	{
		public BaseClothMaterial( int itemID ) : this( itemID, 1 )
		{
		}

		public BaseClothMaterial( int itemID, int amount ) : base( itemID )
		{
			Stackable = true;
			Weight = 1.0;
			Amount = amount;
		}

		public BaseClothMaterial( Serial serial ) : base( serial )
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

		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;

			Hue = sender.DyedHue;

			return true;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 500366 ); // Select a loom to use that on.
				from.Target = new PickLoomTarget( this );
			}
			else
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
		}

		private class PickLoomTarget : Target
		{
			private BaseClothMaterial m_Material;

			public PickLoomTarget( BaseClothMaterial material ) : base( 3, false, TargetFlags.None )
			{
				m_Material = material;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Material.Deleted )
					return;

				ILoom loom = targeted as ILoom;

				if ( loom == null && targeted is AddonComponent )
					loom = ((AddonComponent)targeted).Addon as ILoom;

				if ( loom != null && loom is Item)
				{
                    Item item = (Item)loom;
					if ( !m_Material.IsChildOf( from.Backpack ) )
					{
						from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
					}
                    else if (loom.Looming)
                    {
                        from.SendMessage("That loom is being used.");
                    }
					else
					{
                        LoomQuotaAttachment att = (LoomQuotaAttachment)XmlAttach.FindAttachment(from, typeof(LoomQuotaAttachment));

                        if (att == null)
                        {
                            att = new LoomQuotaAttachment();
                            XmlAttach.AttachTo(from, att);
                        }
                        if (att.getNumLooms() < LoomQuotaAttachment.m_LoomQuotaCap)
                        {
                            att.AddLooms(item);
                            from.PublicOverheadMessage(Server.Network.MessageType.Emote, 51, false, "*looming*");
                            m_Material.Consume();
                            loom.BeginLoom(new LoomCallback(BaseClothMaterial.OnLoomLoop), from, m_Material.Hue, m_Material);
                        }
                        else
                            from.SendMessage("You are too occupied with the " + LoomQuotaAttachment.m_LoomQuotaCap.ToString() + " looms you are running.");
					}
				}
				else
				{
					from.SendLocalizedMessage( 500367 ); // Try using that on a loom.
				}
			}
		}

        public static void OnLoomLoop(ILoom loom, Mobile from, int hue, Item thread)
        {
            if (loom.Phase > 4)
            {
                loom.Phase = 0;
                Item item = new BoltOfCloth();
                item.Hue = hue;
                from.AddToBackpack(item);
                from.SendLocalizedMessage(500368); // You create some cloth and put it in your backpack.
            }

            LoomQuotaAttachment att = (LoomQuotaAttachment)XmlAttach.FindAttachment(from, typeof(LoomQuotaAttachment));
            if (att == null)
            {
                att = new LoomQuotaAttachment();
                XmlAttach.AttachTo(from, att);
            }
            att.RemoveLooms((Item)loom);

            if (from.NetState == null) // player logged off
                return;
            if (thread.Deleted || thread.Amount < 1 || !(thread is BaseClothMaterial))
                from.SendMessage("You finished processing all the threads/yarns.");
            else if (!thread.IsChildOf(from.Backpack))
                from.SendMessage("You can not continue without the threads/yarns in your backpack.");
            else if (loom is Item)
            {
                Item loom1 = (Item)loom;

                if (loom1.Deleted)
                    from.SendMessage("Where did the loom go?");
                else if (!from.InRange(loom1.GetWorldLocation(), 3))
                    from.SendMessage("You are too far away from the loom to continue your work.");
                else if (loom.Looming)
                    from.SendMessage("That loom is being used.");
                else
                {
                    if (att.getNumLooms() < LoomQuotaAttachment.m_LoomQuotaCap)
                    {
                        att.AddLooms(loom1);
                        if (Utility.Random(20 * att.getNumLooms()) < 1)
                            from.PublicOverheadMessage(Server.Network.MessageType.Emote, 51, false, "*looming*");
                        thread.Consume();
                        loom.BeginLoom(new LoomCallback(BaseClothMaterial.OnLoomLoop), from, thread.Hue, thread);
                        return;
                    }
                    else
                        from.SendMessage("You are too occupied with the " + LoomQuotaAttachment.m_LoomQuotaCap.ToString() + " looms you are running.");
                }
            }
        }
	}

	public class DarkYarn : BaseClothMaterial
	{
		[Constructable]
		public DarkYarn() : this( 1 )
		{
		}

		[Constructable]
		public DarkYarn( int amount ) : base( 0xE1D, amount )
		{
		}

		public DarkYarn( Serial serial ) : base( serial )
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

	public class LightYarn : BaseClothMaterial
	{
		[Constructable]
		public LightYarn() : this( 1 )
		{
		}

		[Constructable]
		public LightYarn( int amount ) : base( 0xE1E, amount )
		{
		}

		public LightYarn( Serial serial ) : base( serial )
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

	public class LightYarnUnraveled : BaseClothMaterial
	{
		[Constructable]
		public LightYarnUnraveled() : this( 1 )
		{
		}

		[Constructable]
		public LightYarnUnraveled( int amount ) : base( 0xE1F, amount )
		{
		}

		public LightYarnUnraveled( Serial serial ) : base( serial )
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

	public class SpoolOfThread : BaseClothMaterial
	{
		[Constructable]
		public SpoolOfThread() : this( 1 )
		{
		}

		[Constructable]
		public SpoolOfThread( int amount ) : base( 0xFA0, amount )
		{
		}

		public SpoolOfThread( Serial serial ) : base( serial )
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