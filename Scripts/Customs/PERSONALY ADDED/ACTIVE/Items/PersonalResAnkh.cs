using System;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Multis;
using Server.Gumps;
using System.Collections;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Items
{
    public class BaseCustomAddonComponent : AddonComponent
    {
        private Mobile m_Owner;

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Owner { get { return m_Owner; } set { m_Owner = value; InvalidateProperties(); } }

        [Constructable]
		public BaseCustomAddonComponent( int itemID ) : base( itemID )
		{

            Movable = false;
            Name = "Personal Ankh";
            LootType = LootType.Blessed;
		}

        public BaseCustomAddonComponent(Serial serial)
            : base(serial)
		{
		}

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.WriteEncodedInt(0); // version

            writer.Write((Mobile)m_Owner);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadEncodedInt();

            m_Owner = reader.ReadMobile();
        }

        #region custom methods
        public bool IsHomeOwner(Mobile from)
        {
            if (from.AccessLevel >= AccessLevel.GameMaster)
                return true;

            BaseHouse house = BaseHouse.FindHouseAt(this);
            return (house != null && house.IsOwner(from));
        }
        #endregion

        #region overrided Methods
        public override bool OnDragDrop(Mobile from, Item dropped)
        {
            from.SendMessage("This is not a container!");
            return false;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!from.InRange(this.GetWorldLocation(), 2))
            {
                from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 1019045); // I can't reach that.
            }
            else
            {
                bool isHomeOwner = IsHomeOwner(from);

                //Console.WriteLine("OnDoubleClick");
                // set owner if not already set -- this is only done the first time.
                if (m_Owner == null && isHomeOwner)
                {
                    m_Owner = from;
                    //this.Name = m_Owner.Name.ToString() + "'s Personal Ankh";
                    this.Name = m_Owner.Name.ToString() + "'s Shrine";
                    from.SendMessage("Ownership of this ankh has been bestowed upon you.");
                }
                else
                {
                    if (m_Owner != from)
                    {
                        from.SendMessage("This is not your's to use.");
                        return;
                    }
                    else if (!isHomeOwner)
                    {
                        from.SendMessage("This is not your home!");
                    }
                }
            }
        }

        public override bool HandlesOnMovement { get { return true; } } // Tell the core that we implement OnMovement

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            if (Parent == null && Utility.InRange(Location, m.Location, 1) && !Utility.InRange(Location, oldLocation, 1))
                Ankhs.Resurrect(m, this);
        }
        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);
            Ankhs.GetContextMenuEntries(from, this, list);
        }
        public override void OnDoubleClickDead(Mobile m)
        {
            Ankhs.Resurrect(m, this);
        }

        //public override DeathMoveResult OnInventoryDeath(Mobile parent)
        //{
        //    if (parent == m_Owner)
        //    {
        //        Console.WriteLine("DeathMoveResult OnInventoryDeath");
        //        new AutoResTimer(parent).Start();
        //    }
        //    return base.OnInventoryDeath(parent);
        //}

        private class AutoResTimer : Timer
        {
            private Mobile m_Mobile;
            public AutoResTimer(Mobile mob)
                : base(TimeSpan.FromSeconds(5.0))
            {
                m_Mobile = mob;
            }

            protected override void OnTick()
            {
                m_Mobile.Resurrect();
                m_Mobile.SendMessage("¤Res¤ You have been resurrected!");

                new BlessedTimer(m_Mobile).Start();
                m_Mobile.SendMessage("¤Res¤ You will be blessed for 60 seconds.");

                m_Mobile.Blessed = true;
                Stop();
            }
        }

        private class BlessedTimer : Timer
        {
            private Mobile m_Mobile;
            public int cnt;

            public BlessedTimer(Mobile mob)
                : base(TimeSpan.FromSeconds(15.0), TimeSpan.FromSeconds(15.0))
            {
                m_Mobile = mob;
                cnt = 60;
            }

            protected override void OnTick()
            {
                if (cnt > 0)
                {
                    cnt -= 15;
                    m_Mobile.SendMessage("You will be blessed for {0} more seconds.", cnt);
                }
                if (cnt == 0)
                {
                    m_Mobile.SendMessage("You are no longer blessed.");
                    m_Mobile.Blessed = false;
                    this.Stop();
                }
                if (cnt < 0)
                {
                    cnt = 0;
                    m_Mobile.SendMessage("You are no longer blessed.");
                    m_Mobile.Blessed = false;
                    this.Stop();
                }
            }
        }
        #endregion
    }

    #region PersonalResAnkh West
    public class PersonalResAnkhWestPartA : BaseCustomAddonComponent
    {
        [Constructable]
        public PersonalResAnkhWestPartA()
            : base(0x2)
        {
        }

        public PersonalResAnkhWestPartA(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.WriteEncodedInt(0); // version
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadEncodedInt();
        }
    }

    public class PersonalResAnkhWestPartB : BaseCustomAddonComponent
    {
        [Constructable]
        public PersonalResAnkhWestPartB()
            : base(0x3)
        {
        }

        public PersonalResAnkhWestPartB(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.WriteEncodedInt(0); // version
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadEncodedInt();
        }
    }
    #endregion

    #region PersonalResAnkh North
    public class PersonalResAnkhNorthPartA : BaseCustomAddonComponent
    {
        [Constructable]
        public PersonalResAnkhNorthPartA()
            : base(0x5)
        {
        }

        public PersonalResAnkhNorthPartA(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.WriteEncodedInt(0); // version
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadEncodedInt();
        }
    }

    public class PersonalResAnkhNorthPartB : BaseCustomAddonComponent
    {
        [Constructable]
        public PersonalResAnkhNorthPartB()
            : base(0x4)
        {
        }

        public PersonalResAnkhNorthPartB(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.WriteEncodedInt(0); // version
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadEncodedInt();
        }
    }
    #endregion

    #region class PersonalResAnkh
    public class PersonalResAnkh : BaseAddon
    {
        public override BaseAddonDeed Deed { get { return new PersonalResAnkhDeed(); } }

        private AddonComponent GetComponent(BaseCustomAddonComponent component, int hue)
        {
            BaseCustomAddonComponent ac = new BaseCustomAddonComponent(component.ItemID);

            ac.Hue = hue;
            ac.Name = "personal shrine ankh";

            return ac;
        }

        public PersonalResAnkh(bool east)
        {
            int hue = Utility.RandomList(2970);

            if (east)
            {
                AddComponent(GetComponent(new PersonalResAnkhWestPartA(), hue), 0, 0, 0);
                AddComponent(GetComponent(new PersonalResAnkhWestPartB(), hue), 0, -1, 0);
            }
            else
            {
                AddComponent(GetComponent(new PersonalResAnkhNorthPartA(), hue), 0, 0, 0);
                AddComponent(GetComponent(new PersonalResAnkhNorthPartB(), hue), -1, 0, 0);
            }
        }

        public PersonalResAnkh(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
    #endregion

    #region class PersonalResAnkhDeed
    public class PersonalResAnkhDeed : BaseAddonDeed
    {
        private bool m_East;
        public override BaseAddon Addon { get { return new PersonalResAnkh(m_East); } }

        [Constructable]
        public PersonalResAnkhDeed()
        {
            Name = "a deed for a personal shrine ankh";
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (IsChildOf(from.Backpack))
            {
                from.CloseGump(typeof(InternalGump));
                from.SendGump(new InternalGump(this));
            }
            else
            {
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
            }
        }

        private void SendTarget(Mobile m)
        {
            base.OnDoubleClick(m);
        }

        private class InternalGump : Gump
        {
            private PersonalResAnkhDeed m_Deed;

            public InternalGump(PersonalResAnkhDeed deed)
                : base(150, 50)
            {
                m_Deed = deed;

                AddBackground(0, 0, 350, 200, 0xA28);

                AddItem(100, 35, 0x4);
                AddItem(122, 35, 0x5);
                AddButton(70, 35, 0x868, 0x869, 1, GumpButtonType.Reply, 0); // South

                AddItem(205, 35, 0x2);
                AddItem(227, 35, 0x3);
                AddButton(185, 35, 0x868, 0x869, 2, GumpButtonType.Reply, 0); // East
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
                if (m_Deed.Deleted || info.ButtonID == 0)
                    return;

                try
                {
                    m_Deed.m_East = (info.ButtonID != 1);
                    m_Deed.SendTarget(sender.Mobile);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + "InternalGump OnResponse try/catch");
                }
            }
        }

        public PersonalResAnkhDeed(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadEncodedInt();
        }
    }
    #endregion
}