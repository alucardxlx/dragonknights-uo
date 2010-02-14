using System;
using Server.Items;
using Server.Targeting;
using Server.Engines.XmlSpawner2;

namespace Server.Items
{
	public class Wool : Item, IDyable
	{
		[Constructable]
		public Wool() : this( 1 )
		{
		}

		[Constructable]
		public Wool( int amount ) : base( 0xDF8 )
		{
			Stackable = true;
			Weight = 4.0;
			Amount = amount;
		}

		public Wool( Serial serial ) : base( serial )
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
				from.SendLocalizedMessage( 502655 ); // What spinning wheel do you wish to spin this on?
				from.Target = new PickWheelTarget( this );
			}
			else
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
		}

		public static void OnSpun( ISpinningWheel wheel, Mobile from, int hue )
		{
			Item item = new DarkYarn( 3 );
			item.Hue = hue;

			from.AddToBackpack( item );
			from.SendLocalizedMessage( 1010576 ); // You put the balls of yarn in your backpack.
		}

        public static void OnSpunLoop(ISpinningWheel wheel, Mobile from, int hue, Item cotton)
        {
            Item item = new DarkYarn(3);
            item.Hue = hue;
            from.AddToBackpack(item);

            SpinningWheelQuotaAttachment att = (SpinningWheelQuotaAttachment)XmlAttach.FindAttachment(from, typeof(SpinningWheelQuotaAttachment));
            if (att == null)
            {
                att = new SpinningWheelQuotaAttachment();
                XmlAttach.AttachTo(from, att);
            }
            att.RemoveWheels((Item)wheel);

            if (from.NetState == null) // player logged off
                return;
            if (cotton.Deleted || cotton.Amount < 1 || !(cotton is Wool))
                from.SendMessage("You finished processing all the wool.");
            else if (!cotton.IsChildOf(from.Backpack))
                from.SendMessage("You can not continue without the wool in your backpack.");
            else if (wheel is Item)
            {
                Item wheel1 = (Item)wheel;

                if (wheel1.Deleted)
                    from.SendMessage("Where did the spinning wheel go?");
                else if (!from.InRange(wheel1.GetWorldLocation(), 3))
                    from.SendMessage("You are too far away from the spinning wheel to continue your work.");
                else if (wheel.Spinning)
                    from.SendLocalizedMessage(502656); // That spinning wheel is being used.
                else
                {
                    if (att.getNumWheels() < SpinningWheelQuotaAttachment.m_WheelQuotaCap)
                    {
                        att.AddWheels(wheel1);
                        if (Utility.Random(6 * att.getNumWheels()) < 1)
                            from.PublicOverheadMessage(Server.Network.MessageType.Emote, 51, false, "*spinning*");
                        cotton.Consume();
                        wheel.BeginSpin(new SpinCallback(Wool.OnSpunLoop), from, cotton.Hue, cotton);
                        return;
                    }
                    else
                        from.SendMessage("You are too occupied with the " + SpinningWheelQuotaAttachment.m_WheelQuotaCap.ToString() + " spinning wheels you are running.");
                }
            }
            from.SendLocalizedMessage(1010577); // You put the spools of thread in your backpack.
        }

		private class PickWheelTarget : Target
		{
			private Wool m_Wool;

			public PickWheelTarget( Wool wool ) : base( 3, false, TargetFlags.None )
			{
				m_Wool = wool;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Wool.Deleted )
					return;

				ISpinningWheel wheel = targeted as ISpinningWheel;

				if ( wheel == null && targeted is AddonComponent )
					wheel = ((AddonComponent)targeted).Addon as ISpinningWheel;

				if ( wheel is Item )
				{
					Item item = (Item)wheel;

					if ( !m_Wool.IsChildOf( from.Backpack ) )
					{
						from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
					}
					else if ( wheel.Spinning )
					{
						from.SendLocalizedMessage( 502656 ); // That spinning wheel is being used.
					}
					else
					{
                        SpinningWheelQuotaAttachment att = (SpinningWheelQuotaAttachment)XmlAttach.FindAttachment(from, typeof(SpinningWheelQuotaAttachment));

                        if (att == null)
                        {
                            att = new SpinningWheelQuotaAttachment();
                            XmlAttach.AttachTo(from, att);
                        }
                        if (att.getNumWheels() < SpinningWheelQuotaAttachment.m_WheelQuotaCap)
                        {
                            att.AddWheels(item);
                            from.PublicOverheadMessage(Server.Network.MessageType.Emote, 51, false, "*spinning*");
                            m_Wool.Consume();
                            wheel.BeginSpin(new SpinCallback(Wool.OnSpunLoop), from, m_Wool.Hue, m_Wool);
                        }
                        else
                            from.SendMessage("You are too occupied with the " + SpinningWheelQuotaAttachment.m_WheelQuotaCap.ToString() + " spinning wheels you are running.");
                    }
				}
				else
				{
					from.SendLocalizedMessage( 502658 ); // Use that on a spinning wheel.
				}
			}
		}
	}
}