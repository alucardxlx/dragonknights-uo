using System;
using System.Collections;
using Server;
using Server.Mobiles;
using Server.Gumps;
using Server.Misc;
using Server.Network;

namespace Server.Items
{
    public class ResourceStorageKeyMaster : Item
    {
        [Constructable]
        public ResourceStorageKeyMaster()
            : base(0x176B)
        {
            Movable = true;
            Weight = 1.0;
            Hue = 0;
            Name = "Master Worker's Keys";
            LootType = LootType.Blessed;
        }

        public ResourceStorageKeyMaster(Serial serial)
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

        public override void OnDoubleClick(Mobile from)
        {
//            if (from.Map == Map.Felucca)
//            {
//                from.SendMessage("That does not work in Felucca.");
//                return;
//            }

            if (IsChildOf(from.Backpack))
                from.SendGump(new ResourceStorageKeyMasterGump((PlayerMobile)from));
            else
                from.SendMessage("This must be in your backpack to use.");
        }
    }

    public class ResourceStorageKeyMasterGump : Gump
    {
        private PlayerMobile m_From;

        public ResourceStorageKeyMasterGump(PlayerMobile from)
            : base(25, 25)
        {
            m_From = from;
            m_From.CloseGump(typeof(ResourceStorageKeyMasterGump));

            AddPage(0);

            AddBackground(50, 10, 455, 260, 5054);
            AddImageTiled(58, 20, 438, 241, 2624);
            AddAlphaRegion(58, 20, 438, 241);

            AddLabel(200, 25, 88, "Master Worker's Key");
            AddLabel(65, 50, 88, "This master key is linked to resource keys in your bank.");
            AddLabel(65, 75, 88, "For it to work properly, keep a single set in your bank top layer only.");

            AddLabel(125, 100, 0x486, "BlackSmith Keys");
            AddButton(75, 100, 4005, 4007, 1, GumpButtonType.Reply, 0);

            AddLabel(125, 125, 0x486, "Masonry Keys");
            AddButton(75, 125, 4005, 4007, 2, GumpButtonType.Reply, 0);

            AddLabel(125, 150, 0x486, "Wood Keys");
            AddButton(75, 150, 4005, 4007, 3, GumpButtonType.Reply, 0);

            AddLabel(125, 175, 0x486, "Tailor Keys");
            AddButton(75, 175, 4005, 4007, 4, GumpButtonType.Reply, 0);

            AddLabel(125, 200, 0x486, "Spell Caster Keys");
            AddButton(75, 200, 4005, 4007, 5, GumpButtonType.Reply, 0);

            AddLabel(125, 225, 0x486, "Scriber's Tome");
            AddButton(75, 225, 4005, 4007, 6, GumpButtonType.Reply, 0);

            AddLabel(325, 100, 0x486, "Jewel Keys");
            AddButton(275, 100, 4005, 4007, 7, GumpButtonType.Reply, 0);

            AddLabel(325, 125, 0x486, "Bonus Gems & Sewing Kits");
            AddButton(275, 125, 4005, 4007, 8, GumpButtonType.Reply, 0);

            AddLabel(325, 225, 88, "Combine All from backpack");
            AddButton(275, 225, 4005, 4007, 10, GumpButtonType.Reply, 0);
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            if (info.ButtonID == 1)
            {
                Item item = m_From.BankBox.FindItemByType(typeof(ResourceStorageKeyBlackSmith));
                if (item != null)
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, (ResourceStorageKeyBlackSmith)item));
                else
                {
                    m_From.SendMessage("You do not have a BlackSmith key in the top layer of your bank.");
                    m_From.SendGump(new ResourceStorageKeyMasterGump(m_From));
                }
            }
            else if (info.ButtonID == 2)
            {
                Item item = m_From.BankBox.FindItemByType(typeof(ResourceStorageKeyMasonry));
                if (item != null)
                    m_From.SendGump(new ResourceStorageKeyMasonryGump(m_From, (ResourceStorageKeyMasonry)item));
                else
                {
                    m_From.SendMessage("You do not have a Masonry key in the top layer of your bank.");
                    m_From.SendGump(new ResourceStorageKeyMasterGump(m_From));
                }
            }
            else if (info.ButtonID == 3)
            {
                Item item = m_From.BankBox.FindItemByType(typeof(ResourceStorageKeyWood));
                if (item != null)
                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, (ResourceStorageKeyWood)item));
                else
                {
                    m_From.SendMessage("You do not have a wood worker's key in the top layer of your bank.");
                    m_From.SendGump(new ResourceStorageKeyMasterGump(m_From));
                }
            }
            else if (info.ButtonID == 4)
            {
                Item item = m_From.BankBox.FindItemByType(typeof(ResourceStorageKeyTailor));
                if (item != null)
                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, (ResourceStorageKeyTailor)item));
                else
                {
                    m_From.SendMessage("You do not have a tailor's key in the top layer of your bank.");
                    m_From.SendGump(new ResourceStorageKeyMasterGump(m_From));
                }
            }
            else if (info.ButtonID == 5)
            {
                Item item = m_From.BankBox.FindItemByType(typeof(ResourceStorageKeySpellCasters));
                if (item != null)
                    m_From.SendGump(new ResourceStorageKeySpellCastersGump(m_From, (ResourceStorageKeySpellCasters)item));
                else
                {
                    m_From.SendMessage("You do not have a spell caster's key in the top layer of your bank.");
                    m_From.SendGump(new ResourceStorageKeyMasterGump(m_From));
                }
            }
            else if (info.ButtonID == 6)
            {
                Item item = m_From.BankBox.FindItemByType(typeof(ResourceStorageKeyScribersTome));
                if (item != null)
                    m_From.SendGump(new ResourceStorageKeyScribersTomeGump(m_From, (ResourceStorageKeyScribersTome)item));
                else
                {
                    m_From.SendMessage("You do not have a scribe's tomb in the top layer of your bank.");
                    m_From.SendGump(new ResourceStorageKeyMasterGump(m_From));
                }
            }
            else if (info.ButtonID == 7)
            {
                Item item = m_From.BankBox.FindItemByType(typeof(ResourceStorageKeyJewel));
                if (item != null)
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, (ResourceStorageKeyJewel)item));
                else
                {
                    m_From.SendMessage("You do not have a Jewel key in the top layer of your bank.");
                    m_From.SendGump(new ResourceStorageKeyMasterGump(m_From));
                }
            }
            else if (info.ButtonID == 8)
            {
                Item item = m_From.BankBox.FindItemByType(typeof(ResourceStorageKeyBonusGems));
                if (item != null)
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, (ResourceStorageKeyBonusGems)item));
                else
                {
                    m_From.SendMessage("You do not have a BonusGems key in the top layer of your bank.");
                    m_From.SendGump(new ResourceStorageKeyMasterGump(m_From));
                }
            }
            else if (info.ButtonID == 10)
            {
                Item item = m_From.BankBox.FindItemByType(typeof(ResourceStorageKeyBlackSmith));
                if (item != null)
                    ((ResourceStorageKeyBlackSmith)item).CollectFromBackpack(m_From, false);

                item = m_From.BankBox.FindItemByType(typeof(ResourceStorageKeyMasonry));
                if (item != null)
                    ((ResourceStorageKeyMasonry)item).CollectFromBackpack(m_From, false);

                item = m_From.BankBox.FindItemByType(typeof(ResourceStorageKeyWood));
                if (item != null)
                    ((ResourceStorageKeyWood)item).CollectFromBackpack(m_From, false);

                item = m_From.BankBox.FindItemByType(typeof(ResourceStorageKeyTailor));
                if (item != null)
                    ((ResourceStorageKeyTailor)item).CollectFromBackpack(m_From, false);

                item = m_From.BankBox.FindItemByType(typeof(ResourceStorageKeySpellCasters));
                if (item != null)
                    ((ResourceStorageKeySpellCasters)item).CollectFromBackpack(m_From, false);

                item = m_From.BankBox.FindItemByType(typeof(ResourceStorageKeyScribersTome));
                if (item != null)
                    ((ResourceStorageKeyScribersTome)item).CollectFromBackpack(m_From, false);

                item = m_From.BankBox.FindItemByType(typeof(ResourceStorageKeyJewel));
                if (item != null)
                    ((ResourceStorageKeyJewel)item).CollectFromBackpack(m_From, false);

                item = m_From.BankBox.FindItemByType(typeof(ResourceStorageKeyBonusGems));
                if (item != null)
                    ((ResourceStorageKeyBonusGems)item).CollectFromBackpack(m_From, false);

                m_From.SendMessage("You have Collected Resources!");

                m_From.SendGump(new ResourceStorageKeyMasterGump(m_From));
            }
        }
    }
}
