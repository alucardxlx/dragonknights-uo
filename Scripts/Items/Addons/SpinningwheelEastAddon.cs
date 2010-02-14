using System;
using System.Collections.Generic;
using Server;
using Server.Engines.XmlSpawner2;

namespace Server.Items
{
	public delegate void SpinCallback( ISpinningWheel sender, Mobile from, int hue, Item resource );

	public interface ISpinningWheel
	{
		bool Spinning{ get; }
		void BeginSpin( SpinCallback callback, Mobile from, int hue, Item resource );
	}

    public class SpinningWheelQuotaAttachment : XmlAttachment
    {
        public static int m_WheelQuotaCap = 3;

        private List<Item> m_Wheels;

        [CommandProperty(AccessLevel.GameMaster)]
        public List<Item> Wheels
        { get { return m_Wheels; } set { m_Wheels = value; } }

        public int getNumWheels()
        {
            for (int i = m_Wheels.Count - 1; i >= 0; i--)
            {
                Item wheel = m_Wheels[i];
                if (wheel == null || wheel.Deleted)
                    m_Wheels.RemoveAt(i);
            }
            return m_Wheels.Count;
        }

        public bool AddWheels(Item wheel)
        {
            if (getNumWheels() < m_WheelQuotaCap && !m_Wheels.Contains(wheel))
            {
                m_Wheels.Add(wheel);
                return true;
            }
            else
                return false;
        }

        public void RemoveWheels(Item wheel)
        {
            m_Wheels.Remove(wheel);
        }

        public SpinningWheelQuotaAttachment(ASerial serial)
            : base(serial)
        {
        }

        [Attachable]
        public SpinningWheelQuotaAttachment()
        {
            m_Wheels = new List<Item>();
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

            m_Wheels = new List<Item>();
        }
    }

	public class SpinningwheelEastAddon : BaseAddon, ISpinningWheel
	{
		public override BaseAddonDeed Deed{ get{ return new SpinningwheelEastDeed(); } }

		[Constructable]
		public SpinningwheelEastAddon()
		{
			AddComponent( new AddonComponent( 0x1019 ), 0, 0, 0 );
		}

		public SpinningwheelEastAddon( Serial serial ) : base( serial )
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

		private Timer m_Timer;

		public override void OnComponentLoaded( AddonComponent c )
		{
			switch ( c.ItemID )
			{
				case 0x1016:
				case 0x101A:
				case 0x101D:
				case 0x10A5: --c.ItemID; break;
			}
		}

		public bool Spinning{ get{ return m_Timer != null; } }

		public void BeginSpin( SpinCallback callback, Mobile from, int hue, Item resource )
		{
			m_Timer = new SpinTimer( this, callback, from, hue, resource );
			m_Timer.Start();

			foreach ( AddonComponent c in Components )
			{
				switch ( c.ItemID )
				{
					case 0x1015:
					case 0x1019:
					case 0x101C:
					case 0x10A4: ++c.ItemID; break;
				}
			}
		}

		public void EndSpin( SpinCallback callback, Mobile from, int hue, Item resource )
		{
			if ( m_Timer != null )
				m_Timer.Stop();

			m_Timer = null;

			foreach ( AddonComponent c in Components )
			{
				switch ( c.ItemID )
				{
					case 0x1016:
					case 0x101A:
					case 0x101D:
					case 0x10A5: --c.ItemID; break;
				}
			}

			if ( callback != null )
				callback( this, from, hue, resource );
		}

		private class SpinTimer : Timer
		{
			private SpinningwheelEastAddon m_Wheel;
			private SpinCallback m_Callback;
			private Mobile m_From;
			private int m_Hue;
            private Item m_Resource;

			public SpinTimer( SpinningwheelEastAddon wheel, SpinCallback callback, Mobile from, int hue, Item resource ) : base( TimeSpan.FromSeconds( 3.0 ) )
			{
				m_Wheel = wheel;
				m_Callback = callback;
				m_From = from;
				m_Hue = hue;
                m_Resource = resource;
				Priority = TimerPriority.TwoFiftyMS;
			}

			protected override void OnTick()
			{
				m_Wheel.EndSpin( m_Callback, m_From, m_Hue, m_Resource );
			}
		}
	}

	public class SpinningwheelEastDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new SpinningwheelEastAddon(); } }
		public override int LabelNumber{ get{ return 1044341; } } // spining wheel (east)

		[Constructable]
		public SpinningwheelEastDeed()
		{
		}

		public SpinningwheelEastDeed( Serial serial ) : base( serial )
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