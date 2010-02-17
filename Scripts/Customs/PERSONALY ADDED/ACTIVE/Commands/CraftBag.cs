using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Engines.XmlSpawner2;
using Server.Targeting;

namespace Server.Items
{
    public class CraftBagAtt : XmlAttachment
    {
        private Container m_Craftbag;

        [CommandProperty(AccessLevel.GameMaster)]
        public Container CraftBag
        { get { return m_Craftbag; } set { m_Craftbag = value; } }

        public CraftBagAtt(ASerial serial)
            : base(serial)
        {
        }

        [Attachable]
        public CraftBagAtt()
        {
        }

        [Attachable]
        public CraftBagAtt(Container bag)
        {
            m_Craftbag = bag;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
            // version 0
            writer.Write(m_Craftbag);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    // version 0
                    m_Craftbag = reader.ReadItem<Container>();
                    break;
            }
        }
    }
}

namespace Server.Commands
{
    public class CraftBagCommand
    {
        public static void Initialize()
        {
            CommandSystem.Register("Craftbag", AccessLevel.Player, new CommandEventHandler(Own_OnCommand));
        }

        [Usage("Craftbag")]
        [Description("Assign a bag as the container where all items crafted by you will move into.")]
        private static void Own_OnCommand(CommandEventArgs e)
        {
            e.Mobile.Target = new InternalTarget();
            e.Mobile.SendMessage("Which bag you want to assign as your craft bag (where crafted items will move into)?");
        }

        private class InternalTarget : Target
        {
            public InternalTarget()
                : base(2, false, TargetFlags.None)
            {
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (!(targeted is Container))
                {
                    from.SendMessage("That is not a container.");
                    return;
                }

                Container craftbag = (Container)targeted;
                if (!craftbag.IsChildOf(from.Backpack))
                {
                    from.SendMessage("That is not inside your backpack.");
                    return;
                }
                XmlAttachment att = XmlAttach.FindAttachment(from, typeof(CraftBagAtt));
                if (att == null)
                {
                    att = new CraftBagAtt(craftbag);
                    XmlAttach.AttachTo(from, att);
                }
                else
                {
                    CraftBagAtt lb = att as CraftBagAtt;
                    lb.CraftBag = craftbag;
                }
                from.SendMessage("You will now craft into that container.");
            }
        }
    }
}