using System;
using System.Collections.Generic;
using Server;
using Server.Engines.XmlSpawner2;

namespace Server.Items
{
    public delegate void LoomCallback(ILoom sender, Mobile from, int hue, Item resource);

	public interface ILoom
	{
		int Phase{ get; set; }
        bool Looming { get; }
        void BeginLoom(LoomCallback callback, Mobile from, int hue, Item resource);
    }

    public class LoomQuotaAttachment : XmlAttachment
    {
        public static int m_LoomQuotaCap = 3;

        private List<Item> m_Looms;

        [CommandProperty(AccessLevel.GameMaster)]
        public List<Item> Looms
        { get { return m_Looms; } set { m_Looms = value; } }

        public int getNumLooms()
        {
            for (int i = m_Looms.Count - 1; i >= 0; i--)
            {
                Item loom = m_Looms[i];
                if (loom == null || loom.Deleted)
                    m_Looms.RemoveAt(i);
            }
            return m_Looms.Count;
        }

        public bool AddLooms(Item loom)
        {
            if (getNumLooms() < m_LoomQuotaCap && !m_Looms.Contains(loom))
            {
                m_Looms.Add(loom);
                return true;
            }
            else
                return false;
        }

        public void RemoveLooms(Item loom)
        {
            m_Looms.Remove(loom);
        }

        public LoomQuotaAttachment(ASerial serial)
            : base(serial)
        {
        }

        [Attachable]
        public LoomQuotaAttachment()
        {
            m_Looms = new List<Item>();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    break;
            }

            m_Looms = new List<Item>();
        }
    }


	public class LoomEastAddon : BaseAddon, ILoom
	{
		public override BaseAddonDeed Deed{ get{ return new LoomEastDeed(); } }

		private int m_Phase;
		public int Phase{ get{ return m_Phase; } set{ m_Phase = value; } }

		[Constructable]
		public LoomEastAddon()
		{
			AddComponent( new AddonComponent( 0x1060 ), 0, 0, 0 );
			AddComponent( new AddonComponent( 0x105F ), 0, 1, 0 );
		}

		public LoomEastAddon( Serial serial ) : base( serial )
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
            PublicOverheadMessage(Server.Network.MessageType.Emote, 51, false, m_Phase.ToString());

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
            private LoomEastAddon m_Loom;
            private LoomCallback m_Callback;
            private Mobile m_From;
            private int m_Hue;
            private Item m_Resource;

            public LoomTimer(LoomEastAddon loom, LoomCallback callback, Mobile from, int hue, Item resource)
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

	public class LoomEastDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new LoomEastAddon(); } }
		public override int LabelNumber{ get{ return 1044343; } } // loom (east)

		[Constructable]
		public LoomEastDeed()
		{
		}

		public LoomEastDeed( Serial serial ) : base( serial )
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