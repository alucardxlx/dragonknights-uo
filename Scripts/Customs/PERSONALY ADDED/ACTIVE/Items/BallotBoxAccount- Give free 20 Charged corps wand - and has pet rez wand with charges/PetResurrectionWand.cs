using System;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Items
{
    public class PetResurrectionWand : MagicWand
    {
        private int m_Charges;

        [CommandProperty(AccessLevel.GameMaster)]
        public int Charges
        {
            get { return m_Charges; }
            set { m_Charges = value; InvalidateProperties(); }
        }

        [Constructable]
        public PetResurrectionWand()
            : this(10)
        {
        }

        [Constructable]
        public PetResurrectionWand(int charges)
        {
            Name = "a pet resurrection wand";
            m_Charges = charges;
            LootType = LootType.Blessed;
        }

        public PetResurrectionWand(Serial serial)
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
            if (from.Map == Map.Felucca)
            {
                from.SendMessage("That does not work in Felucca.");
                return;
            }
            if (!IsChildOf(from.Backpack) && from.FindItemOnLayer(Layer.OneHanded) != this)
            {
                from.SendMessage("You must be holding that wand or have that wand in your backpack to use it.");
                return;
            }
            if (m_Charges <= 0)
            {
                Delete();
                from.SendMessage("That wand has no uses left.");
            }
            from.SendMessage("Target the pet or henchman you wish to resurrect.");
            from.Target = new PetRezTarget(this); 
        }

        public void RezAPet(Mobile f, object t)
        {
            BaseCreature c = t as BaseCreature;

            if (c == null)
            {
                f.SendMessage("That's not a pet.");
                return;
            }

            if (c.Map == Map.Felucca)
                f.SendMessage("That does not work in Felucca.");
            if (m_Charges < 1)
            {
                this.Delete();
                f.SendMessage("That wand is used up already.");
            }
            else if (!c.Controlled)
            {
                f.SendMessage("That's not a pet.");
            }
            else if (c.ControlMaster != f)
            {
                f.SendMessage("That's not your pet.");
            }
            else if (!c.IsDeadPet)
            {
                f.SendMessage("That pet is still alive.");
            }
            else if (!c.InRange(f.Location, 3))
            {
                f.SendMessage("Your pet needs to be closer to you for the wand to work.");
            }
            else if (c.Map == null || !c.Map.CanFit(c.Location, 16, false, false))
            {
                f.SendMessage("You can not resurrect the pet at that location.");
            }
            else
            {
                c.PlaySound(0x214);
                c.FixedEffect(0x376A, 10, 16);
                c.ResurrectPet();
                Charges--;
                if (m_Charges <= 0)
                {
                    this.Delete();
                    f.SendMessage("That wand used up its last charge.");
                }

            }
        }

        public class PetRezTarget : Target
        {
            private PetResurrectionWand m_Wand;

            public PetRezTarget(PetResurrectionWand wand)
                : base(3, false, TargetFlags.None)
            {
                m_Wand = wand;
            }

            protected override void OnTarget(Mobile f, object t)
            {
                m_Wand.RezAPet(f, t);
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