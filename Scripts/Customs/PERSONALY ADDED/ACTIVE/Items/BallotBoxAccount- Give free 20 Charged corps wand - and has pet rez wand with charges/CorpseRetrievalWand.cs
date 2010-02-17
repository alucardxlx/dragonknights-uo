using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
    public class CorpseRetrievalWand : MagicWand
    {
        private int m_Charges;

        [CommandProperty(AccessLevel.GameMaster)]
        public int Charges
        {
            get { return m_Charges; }
            set { m_Charges = value; InvalidateProperties(); }
        }

        [Constructable]
        public CorpseRetrievalWand()
            : this(10)
        {
        }

        [Constructable]
        public CorpseRetrievalWand(int charges)
        {
            Name = "a corpse retrieval wand";
            m_Charges = charges;
            LootType = LootType.Blessed;
        }

        public CorpseRetrievalWand(Serial serial)
            : base(serial)
        {
        }

        public override void AddNameProperties(ObjectPropertyList list)
        {
            base.AddNameProperties(list);

            list.Add(1060741, m_Charges.ToString()); // charges: ~1_val~
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!from.Alive)
            {
                from.SendMessage("You can not do that.");
                return;
            }
//            if (from.Map == Map.Felucca)
//            {
//                from.SendMessage("That does not work in Felucca.");
//                return;
//            }
            if (!IsChildOf(from.Backpack) && from.FindItemOnLayer(Layer.OneHanded)!=this)
            {
                from.SendMessage("You must be holding that wand or have that wand in your backpack to use it.");
                return;
            }
            if (m_Charges <= 0)
            {
                Delete();
                from.SendMessage("That wand has no uses left.");
            }
            if (from.Corpse != null)
            {
                if (from.Corpse.Map == Map.Felucca)
                {
                    from.SendMessage("That does not work when your corpse is in Felucca.");
                    return;
                }
                from.Corpse.MoveToWorld(from.Location, from.Map);
                from.SendMessage("Your last corpse is now moved to your current location.");
                Charges--;
                if (m_Charges <= 0)
                {
                    this.Delete();
                    from.SendMessage("That wand used up its last charge.");
                }
            }
            else
            {
                from.SendMessage("You do not seem to need that right now.");
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

            writer.Write((int)m_Charges);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        m_Charges = reader.ReadInt();
                        break;
                    }
            }
        }
    }
}