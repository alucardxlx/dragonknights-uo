using System;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Engines.XmlSpawner2;
using Server.Gumps;
using System.Collections;
using Server.Commands;

namespace Server.Items
{
    public class SellData : XmlAttachment
    {
//        private bool m_sellmagic = true;
        private bool m_sellweapons = true;
        private bool m_sellarmor = true;
        private bool m_selljewelry = true;
        private bool m_sellarrows = true;
        private bool m_sellpotions = true;
        private bool m_sellregs = true;
        private bool m_sellclothes = true;
        private bool m_sellgems = true;
        private bool m_sellhides = true;
        private bool m_sellfood = true;
        private bool m_sellresources = true;

//        [CommandProperty(AccessLevel.GameMaster)]
//        public bool SellMagic { get { return m_sellmagic; } set { m_sellmagic = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool SellWeapons { get { return m_sellweapons; } set { m_sellweapons = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool SellArmor { get { return m_sellarmor; } set { m_sellarmor = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool SellJewelry { get { return m_selljewelry; } set { m_selljewelry = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool SellArrows { get { return m_sellarrows; } set { m_sellarrows = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool SellPotions { get { return m_sellpotions; } set { m_sellpotions = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool SellRegs { get { return m_sellregs; } set { m_sellregs = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool SellClothes { get { return m_sellclothes; } set { m_sellclothes = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool SellGems { get { return m_sellgems; } set { m_sellgems = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool SellHides { get { return m_sellhides; } set { m_sellhides = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool SellFood { get { return m_sellfood; } set { m_sellfood = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool SellResources { get { return m_sellresources; } set { m_sellresources = value; } }

        public SellData(ASerial serial)
            : base(serial)
        {
        }

        [Attachable]
        public SellData()
        {
        }

        [Attachable]
        public SellData(bool weaponsflag, bool armorflag, bool jewelryflag, bool potionsflag, bool regsflag, bool clothesflag, bool gemsflag, bool arrowsflag, bool hidesflag, bool foodflag, bool resourcesflag) // took out* bool magicflag, 
        {
//            m_sellmagic = magicflag;
            m_sellweapons = weaponsflag;
            m_sellarmor = armorflag;
            m_selljewelry = jewelryflag;
            m_sellarrows = arrowsflag;
            m_sellpotions = potionsflag;
            m_sellregs = regsflag;
            m_sellclothes = clothesflag;
            m_sellgems = gemsflag;
            m_sellhides = hidesflag;
            m_sellfood = foodflag;
            m_sellresources = resourcesflag;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
            // version 0
//            writer.Write(m_sellmagic);
            writer.Write(m_sellweapons);
            writer.Write(m_sellarmor);
            writer.Write(m_selljewelry);
            writer.Write(m_sellarrows);
            writer.Write(m_sellpotions);
            writer.Write(m_sellregs);
            writer.Write(m_sellclothes);
            writer.Write(m_sellgems);
            writer.Write(m_sellhides);
            writer.Write(m_sellfood);
            writer.Write(m_sellresources);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            // version 0
            int version = reader.ReadInt();
//            m_sellmagic = reader.ReadBool();
            m_sellweapons = reader.ReadBool();
            m_sellarmor = reader.ReadBool();
            m_selljewelry = reader.ReadBool();
            m_sellarrows = reader.ReadBool();
            m_sellpotions = reader.ReadBool();
            m_sellregs = reader.ReadBool();
            m_sellclothes = reader.ReadBool();
            m_sellgems = reader.ReadBool();
            m_sellhides = reader.ReadBool();
            m_sellfood = reader.ReadBool();
            m_sellresources = reader.ReadBool();
        }

        public static bool isSellable(SellData selloptions, Item item)
        {
//            if (item is BaseWeapon)
//            {
//                if (!selloptions.SellWeapons)
//                    return false;
//                else if (!selloptions.SellMagic && MagicPoints.GetPoints((BaseWeapon)item) > 0)
//                    return false;
//                else
//                    return true;
//            }
//            else if (item is BaseArmor)
//            {
//                if (!selloptions.SellArmor)
//                    return false;
//                else if (!selloptions.SellMagic && MagicPoints.GetPoints((BaseArmor)item) > 0)
//                    return false;
//                else
//                    return true;
//            }
//            else if (item is BaseJewel)
//            {
//                if (!selloptions.SellJewelry)
//                    return false;
//                else if (!selloptions.SellMagic && MagicPoints.GetPoints((BaseJewel)item) > 0)
//                    return false;
//                else
//                    return true;
//            }
//            else if (item is BaseHat)
//            {
//                if (!selloptions.SellClothes)
//                    return false;
//                else if (!selloptions.SellMagic && MagicPoints.GetPoints((BaseHat)item) > 0)
//                    return false;
//                else
//                    return true;
//            }
//I ADDED
            if ((item is BaseWeapon) && !selloptions.SellWeapons)
                return false;
            else if ((item is BaseArmor) && !selloptions.SellArmor)
                return false;
            else if ((item is BaseJewel) && !selloptions.SellJewelry)
                return false;
//
            else if ((item is BaseClothing || item is BaseShoes || item is BaseHat) && !selloptions.SellClothes)
                return false;
            else if ((item is Arrow || item is Bolt) && !selloptions.SellArrows)
                return false;
            else if (item is BasePotion && !selloptions.SellPotions)
                return false;
            else if (item is BaseReagent && !selloptions.SellRegs)
                return false;
            else if ((item is BaseIngot || item is Shaft || item is Board || item is BaseOre || item is Log || item is Feather) && !selloptions.SellResources)
                return false;
            else if (
                (item is Diamond ||
                 item is Amber ||
                 item is Amethyst ||
                 item is Citrine ||
                 item is Emerald ||
                 item is Ruby ||
                 item is Sapphire ||
                 item is StarSapphire ||
                 item is DarkSapphire ||
                 item is WhitePearl ||
                 item is FireRuby ||
                 item is Tourmaline) && !selloptions.SellGems)
                return false;
            else if ((item is BaseHides || item is BaseScales) && !selloptions.SellHides)
                return false;
            else if ((item is CookableFood || item is Food) && !selloptions.SellFood)
                return false;
            else
                return true;
        }
    }

    // ================================  Loot Options gump

    public class SellOptionsGump : Gump
    {
        public static void Initialize()
        {
            CommandSystem.Register("Sell", AccessLevel.Player, new CommandEventHandler(Sell_OnCommand));
        }

        [Usage("Sell")]
        [Description("Sell options allow a user to set what items to sell and what not to sell, when using the \"sell bag\" keyword")]
        public static void Sell_OnCommand(CommandEventArgs e)
        {
            //   Check args to see if they want to change sell options
            // if we have args after  "sell"
            if (e.Length != 0)
            {
                // show loot options gump
                if (e.GetString(0).ToLower() == "options")
                {
                    e.Mobile.SendGump(new SellOptionsGump(e.Mobile));
                    return;
                }
                /*
                // we need to set the loot bag
                else if (e.GetString(0).ToLower() == "bag")
                {
                    e.Mobile.Target = new InternalTarget();
                    e.Mobile.SendMessage("Which container you want to loot into?");
                    return;
                }
                */
                else
                    e.Mobile.SendMessage("Command [sell options  will allow you to decide types of items you do not want to sell when you use the \"sell bag\" keyword next to a vendor.");
            }
            else
                e.Mobile.SendMessage("Command [sell options  will allow you to decide types of items you do not want to sell when you use the \"sell bag\" keyword next to a vendor.");
        }

        public SellOptionsGump(Mobile m)
            : base(0, 0)
        {
            SellData selloptions = (SellData)XmlAttach.FindAttachment(m, typeof(SellData));
            if (selloptions == null)
            {
                selloptions = new SellData();
                XmlAttach.AttachTo(m, selloptions);
            }

            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;
            this.AddPage(0);
            this.AddBackground(0, 0, 368, 284, 9380);

            this.AddLabel(126, 7, 0, @"Sell Options");
//            this.AddLabel(81, 34, 0, @"Magic Items");
            this.AddLabel(81, 64, 0, @"Hides/Scales");
            this.AddLabel(81, 94, 0, @"Arrows/Bolts");
            this.AddLabel(81, 124, 0, @"All Weapons");
            this.AddLabel(81, 154, 0, @"All Armors");
            this.AddLabel(81, 184, 0, @"Reagents");

            this.AddLabel(259, 34, 0, @"Jewelry");
            this.AddLabel(259, 64, 0, @"Potions");
            this.AddLabel(259, 94, 0, @"Gems");
            this.AddLabel(259, 124, 0, @"Clothes");
            this.AddLabel(259, 154, 0, @"Food");
            this.AddLabel(259, 184, 0, @"Resources");

//            this.AddLabel(40, 210, 0, @"*Magic Items do not include clothes except hat*");

//            this.AddCheck(60, 34, 210, 211, selloptions.SellMagic, 1);
            this.AddCheck(60, 64, 210, 211, selloptions.SellHides, 2);
            this.AddCheck(60, 94, 210, 211, selloptions.SellArrows, 3);
            this.AddCheck(60, 124, 210, 211, selloptions.SellWeapons, 4);
            this.AddCheck(60, 154, 210, 211, selloptions.SellArmor, 5);
            this.AddCheck(60, 184, 210, 211, selloptions.SellRegs, 11);

            this.AddCheck(229, 34, 210, 211, selloptions.SellJewelry, 6);
            this.AddCheck(229, 64, 210, 211, selloptions.SellPotions, 7);
            this.AddCheck(229, 94, 210, 211, selloptions.SellGems, 8);
            this.AddCheck(229, 124, 210, 211, selloptions.SellClothes, 9);
            this.AddCheck(229, 154, 210, 211, selloptions.SellFood, 10);
            this.AddCheck(229, 184, 210, 211, selloptions.SellResources, 12);

            this.AddButton(67, 228, 247, 248, 1, GumpButtonType.Reply, 0);
            this.AddButton(244, 228, 241, 248, 0, GumpButtonType.Reply, 0);
        }
        public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
        {

            Mobile m = sender.Mobile;
            SellData selloptions = (SellData)XmlAttach.FindAttachment(m, typeof(SellData));
            if (selloptions == null)
            {
                selloptions = new SellData();
                XmlAttach.AttachTo(m, selloptions);
            }
            switch (info.ButtonID)
            {
                case 0: // Closed or Cancel
                    {
                        return;
                    }
                default:
                    {
                        // Make sure that the OK button was pressed
                        if (info.ButtonID == 1)
                        {
                            // see what types of stuff the user do not wants to sell and toggle the property
                            ArrayList Selections = new ArrayList(info.Switches);

//                            if (Selections.Contains(1) == true) { selloptions.SellMagic = true; }
//                            else { selloptions.SellMagic = false; }
                            if (Selections.Contains(2) == true) { selloptions.SellHides = true; }
                            else { selloptions.SellHides = false; }
                            if (Selections.Contains(3) == true) { selloptions.SellArrows = true; }
                            else { selloptions.SellArrows = false; }
                            if (Selections.Contains(4) == true) { selloptions.SellWeapons = true; }
                            else { selloptions.SellWeapons = false; }
                            if (Selections.Contains(5) == true) { selloptions.SellArmor = true; }
                            else { selloptions.SellArmor = false; }
                            if (Selections.Contains(6) == true) { selloptions.SellJewelry = true; }
                            else { selloptions.SellJewelry = false; }
                            if (Selections.Contains(7) == true) { selloptions.SellPotions = true; }
                            else { selloptions.SellPotions = false; }
                            if (Selections.Contains(8) == true) { selloptions.SellGems = true; }
                            else { selloptions.SellGems = false; }
                            if (Selections.Contains(9) == true) { selloptions.SellClothes = true; }
                            else { selloptions.SellClothes = false; }
                            if (Selections.Contains(10) == true) { selloptions.SellFood = true; }
                            else { selloptions.SellFood = false; }
                            if (Selections.Contains(11) == true) { selloptions.SellRegs = true; }
                            else { selloptions.SellRegs = false; }
                            if (Selections.Contains(12) == true) { selloptions.SellResources = true; }
                            else { selloptions.SellResources = false; }

                        }
                        break;
                    }
            }
        }
    }
}
