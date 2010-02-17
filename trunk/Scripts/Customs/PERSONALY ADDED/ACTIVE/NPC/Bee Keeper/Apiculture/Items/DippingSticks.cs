using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
    [Flipable(0x1428, 0x1429)]
    public class DippingStick : BaseTool
    {
        public override CraftSystem CraftSystem { get { return DefWaxCraft.CraftSystem; } }

        [Constructable]
        public DippingStick()
            : this(50)
        {
        }

        [Constructable]
        public DippingStick(int uses)
            : base(uses, 0x1428)
        {
            Weight = 1.0;
            Name = "Dipping stick";
            Stackable = false;
        }

        public DippingStick(Serial serial)
            : base(serial)
        {
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
        }
    }
}