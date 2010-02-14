using System;
using Server;

namespace Server.Items
{
	public class LoomSouthAddon : BaseAddon, ILoom
	{
		public override BaseAddonDeed Deed{ get{ return new LoomSouthDeed(); } }

		private int m_Phase;

		public int Phase{ get{ return m_Phase; } set{ m_Phase = value; } }

		[Constructable]
		public LoomSouthAddon()
		{
			AddComponent( new AddonComponent( 0x1061 ), 0, 0, 0 );
			AddComponent( new AddonComponent( 0x1062 ), 1, 0, 0 );
		}

		public LoomSouthAddon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

			writer.Write( (int) m_Phase );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
					m_Phase = reader.ReadInt();
					break;
				}
			}
		}

        private Timer m_Timer;

        /*
        public override void OnComponentLoaded(AddonComponent c)
        {
            switch (c.ItemID)
            {
                case 0x1016:
                case 0x101A:
                case 0x101D:
                case 0x10A5: --c.ItemID; break;
            }
        }
         */

        public bool Looming { get { return m_Timer != null; } }

        public void BeginLoom(LoomCallback callback, Mobile from, int hue, Item resource)
        {
            m_Timer = new LoomTimer(this, callback, from, hue, resource);
            m_Timer.Start();

            /*
            foreach (AddonComponent c in Components)
            {
                switch (c.ItemID)
                {
                    case 0x1015:
                    case 0x1019:
                    case 0x101C:
                    case 0x10A4: ++c.ItemID; break;
                }
            }
             */
        }

        public void EndLoom(LoomCallback callback, Mobile from, int hue, Item resource)
        {
            if (m_Timer != null)
                m_Timer.Stop();

            m_Timer = null;

            m_Phase++;
            if (m_Phase == 3)
                PublicOverheadMessage(Server.Network.MessageType.Emote, 51, false, "(cha-cha)");

            /*
            foreach (AddonComponent c in Components)
            {
                switch (c.ItemID)
                {
                    case 0x1016:
                    case 0x101A:
                    case 0x101D:
                    case 0x10A5: --c.ItemID; break;
                }
            }
             */

            if (callback != null)
                callback(this, from, hue, resource);
        }

        private class LoomTimer : Timer
        {
            private LoomSouthAddon m_Loom;
            private LoomCallback m_Callback;
            private Mobile m_From;
            private int m_Hue;
            private Item m_Resource;

            public LoomTimer(LoomSouthAddon loom, LoomCallback callback, Mobile from, int hue, Item resource)
                : base(TimeSpan.FromSeconds(1.5))
            {
                m_Loom = loom;
                m_Callback = callback;
                m_From = from;
                m_Hue = hue;
                m_Resource = resource;
                Priority = TimerPriority.TwoFiftyMS;
            }

            protected override void OnTick()
            {
                m_Loom.EndLoom(m_Callback, m_From, m_Hue, m_Resource);
            }
        }
	}

	public class LoomSouthDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new LoomSouthAddon(); } }
		public override int LabelNumber{ get{ return 1044344; } } // loom (south)

		[Constructable]
		public LoomSouthDeed()
		{
		}

		public LoomSouthDeed( Serial serial ) : base( serial )
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