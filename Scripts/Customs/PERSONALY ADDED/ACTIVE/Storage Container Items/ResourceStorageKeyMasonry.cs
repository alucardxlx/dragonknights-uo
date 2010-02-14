/*
 created by:
     /\            888                   888     .d8888b.   .d8888b.  
____/_ \____       888                   888    d88P  Y88b d88P  Y88b 
\  ___\ \  /       888                   888    888    888 888    888 
 \/ /  \/ /    .d88888  8888b.   8888b.  888888 Y88b. d888 Y88b. d888 
 / /\__/_/\   d88" 888     "88b     "88b 888     "Y888P888  "Y888P888 
/__\ \_____\  888  888 .d888888 .d888888 888           888        888 
    \  /      Y88b 888 888  888 888  888 Y88b.  Y88b  d88P Y88b  d88P 
     \/        "Y88888 "Y888888 "Y888888  "Y888  "Y8888P"   "Y8888P"  

////////////////////////////////
Sources:

Granite Key script by GoldDrac13
Granite Box script by (unknown)
////////////////////////////////
////////////////////////////////////////
Modified by Ashlar, beloved of Morrigan.  
Modified by Tylius.
Modified gump and added custom granites by daat99.
Modified a lof of the code by daat99.
////////////////////////////////////////
This item is a resource storage key.
Add or remove references to fit your shard.  See IngotKey.cs for comments
*/

using System;
using System.Collections;
using Server;
using Server.Prompts;
using Server.Mobiles;
using Server.ContextMenus;
using Server.Gumps;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.Multis;
using Server.Regions;


namespace Server.Items
{
    [FlipableAttribute(0xFEF, 0xFF0, 0xFF1, 0xFF2, 0xFF3, 0xFF4, 0xFBD, 0xFBE)]
    public class ResourceStorageKeyMasonry : Item
    {
        private int m_Granite;
        private int m_DullCopper;
        private int m_ShadowIron;
        private int m_Copper;
        private int m_Bronze;
        private int m_Gold;
        private int m_Agapite;
        private int m_Verite;
        private int m_Valorite;
//        private int m_Fluorite;
//        private int m_Platinum;

        private int m_StorageLimit;
        private int m_WithdrawIncrement;

        [CommandProperty(AccessLevel.GameMaster)]
        public int StorageLimit { get { return m_StorageLimit; } set { m_StorageLimit = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int WithdrawIncrement { get { return m_WithdrawIncrement; } set { m_WithdrawIncrement = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Granite { get { return m_Granite; } set { m_Granite = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int DullCopper { get { return m_DullCopper; } set { m_DullCopper = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int ShadowIron { get { return m_ShadowIron; } set { m_ShadowIron = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Copper { get { return m_Copper; } set { m_Copper = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Bronze { get { return m_Bronze; } set { m_Bronze = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Gold { get { return m_Gold; } set { m_Gold = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Agapite { get { return m_Agapite; } set { m_Agapite = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Verite { get { return m_Verite; } set { m_Verite = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Valorite { get { return m_Valorite; } set { m_Valorite = value; InvalidateProperties(); } }

//        [CommandProperty(AccessLevel.GameMaster)]
//        public int Fluorite { get { return m_Fluorite; } set { m_Fluorite = value; InvalidateProperties(); } }
//
//        [CommandProperty(AccessLevel.GameMaster)]
//        public int Platinum { get { return m_Platinum; } set { m_Platinum = value; InvalidateProperties(); } }

        public void CollectFromBackpack(Mobile from, bool showMessage)
        {
            if (!from.Alive)
                return;
            if (!this.IsChildOf(from.Backpack) && !this.IsChildOf(from.BankBox))
            {
                from.SendMessage("That masonry worker's key has to be in your backpack or bankbox for you to use it.");
                return;
            }

            Type type = typeof(BaseGranite);
            Item[] items = from.Backpack.FindItemsByType(type, true);
            for (int j = 0; j < items.Length; j++)
            {
                int currentAmount = 0;

                Item item = items[j];
                if (!item.Movable)
                    continue;
                int amount = item.Amount;

                if (item is Granite)
                    currentAmount = m_Granite;
                else if (item is DullCopperGranite)
                    currentAmount = m_DullCopper;
                else if (item is ShadowIronGranite)
                    currentAmount = m_ShadowIron;
                else if (item is CopperGranite)
                    currentAmount = m_Copper;
                else if (item is BronzeGranite)
                    currentAmount = m_Bronze;
                else if (item is GoldGranite)
                    currentAmount = m_Gold;
                else if (item is AgapiteGranite)
                    currentAmount = m_Agapite;
                else if (item is VeriteGranite)
                    currentAmount = m_Verite;
                else if (item is ValoriteGranite)
                    currentAmount = m_Valorite;
//                else if (item is FluoriteGranite)
//                    currentAmount = m_Fluorite;
//                else if (item is PlatinumGranite)
//                    currentAmount = m_Platinum;

                if (amount + currentAmount > m_StorageLimit)
                    continue;
                else
                {
                    if (item is Granite)
                        m_Granite += amount;
                    else if (item is DullCopperGranite)
                        m_DullCopper += amount;
                    else if (item is ShadowIronGranite)
                        m_ShadowIron += amount;
                    else if (item is CopperGranite)
                        m_Copper += amount;
                    else if (item is BronzeGranite)
                        m_Bronze += amount;
                    else if (item is GoldGranite)
                        m_Gold += amount;
                    else if (item is AgapiteGranite)
                        m_Agapite += amount;
                    else if (item is VeriteGranite)
                        m_Verite += amount;
                    else if (item is ValoriteGranite)
                        m_Valorite += amount;
//                    else if (item is FluoriteGranite)
//                        m_Fluorite += amount;
//                    else if (item is PlatinumGranite)
//                        m_Platinum += amount;

                    item.Delete();
                }
            }

            if (showMessage)
            from.SendMessage("Granites are collected from your backpack into that key, subject to storage limit.");
        }

        [Constructable]
        public ResourceStorageKeyMasonry()
            : base(0x176B)
        {
            Movable = true;
            Weight = 1.0;
            Hue = 656;
            Name = "Masonry Worker's Keys";
            LootType = LootType.Blessed;
            StorageLimit = 60000;
            WithdrawIncrement = 1;
        }

        [Constructable]
        public ResourceStorageKeyMasonry(int storageLimit, int withdrawIncrement)
            : base(0x176B)
        {
            Movable = true;
            Weight = 1.0;
            Hue = 656;
            Name = "Masonry Worker's Keys";
            LootType = LootType.Blessed;
            StorageLimit = storageLimit;
            WithdrawIncrement = withdrawIncrement;
        }

        public override void OnDoubleClick(Mobile from)
        {
//            if (from.Map == Map.Felucca)
//            {
//                from.SendMessage("That does not work in Felucca.");
//                return;
//            }

            if (IsChildOf(from.Backpack) || IsChildOf(from.BankBox))
                from.SendGump(new ResourceStorageKeyMasonryGump((PlayerMobile)from, this));
            else
                from.SendMessage("This must be in your backpack or bankbox.");
        }

        public void BeginCombine(Mobile from)
        {
            from.Target = new ResourceStorageKeyMasonryTarget(this);
        }

        public void EndCombine(Mobile from, object o)
        {
            if (o is Item && (((Item)o).IsChildOf(from.Backpack) || ((Item)o).IsChildOf(from.BankBox)))
            {
                Item curItem = o as Item;
                if (curItem is BaseGranite)
                {
                    if (curItem is DullCopperGranite)
                    {
                        if (DullCopper + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((DullCopper + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");

                        else
                        {
                            curItem.Delete();
                            DullCopper = (DullCopper + curItem.Amount);
                            from.SendGump(new ResourceStorageKeyMasonryGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is ShadowIronGranite)
                    {

                        if (ShadowIron + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((ShadowIron + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");

                        else
                        {
                            curItem.Delete();
                            ShadowIron = (ShadowIron + curItem.Amount);
                            from.SendGump(new ResourceStorageKeyMasonryGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is CopperGranite)
                    {
                        if (Copper + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((Bronze + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            curItem.Delete();
                            Copper = (Copper + curItem.Amount);
                            from.SendGump(new ResourceStorageKeyMasonryGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is BronzeGranite)
                    {
                        if (Bronze + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((Bronze + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            curItem.Delete();
                            Bronze = (Bronze + curItem.Amount);
                            from.SendGump(new ResourceStorageKeyMasonryGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is GoldGranite)
                    {

                        if (Gold + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((Gold + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            curItem.Delete();
                            Gold = (Gold + curItem.Amount);
                            from.SendGump(new ResourceStorageKeyMasonryGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is AgapiteGranite)
                    {

                        if (Agapite + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((Agapite + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            curItem.Delete();
                            Agapite = (Agapite + curItem.Amount);
                            from.SendGump(new ResourceStorageKeyMasonryGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is VeriteGranite)
                    {

                        if (Verite + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((Verite + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            curItem.Delete();
                            Verite = (Verite + curItem.Amount);
                            from.SendGump(new ResourceStorageKeyMasonryGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is ValoriteGranite)
                    {

                        if (Valorite + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((Valorite + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            curItem.Delete();
                            Valorite = (Valorite + curItem.Amount);
                            from.SendGump(new ResourceStorageKeyMasonryGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
//                    else if (curItem is FluoriteGranite)
//                    {
//
//                        if (Fluorite + curItem.Amount > StorageLimit)
//                            from.SendMessage("You are trying to add " + ((Fluorite + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
//                        else
//                        {
//                            curItem.Delete();
//                            Fluorite = (Fluorite + curItem.Amount);
//                            from.SendGump(new ResourceStorageKeyMasonryGump((PlayerMobile)from, this));
//                            BeginCombine(from);
//                        }
//                    }
//                    else if (curItem is PlatinumGranite)
//                    {
//
//                        if (Platinum + curItem.Amount > StorageLimit)
//                            from.SendMessage("You are trying to add " + ((Platinum + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
//                        else
//                        {
//                            curItem.Delete();
//                            Platinum = (Platinum + curItem.Amount);
//                            from.SendGump(new ResourceStorageKeyMasonryGump((PlayerMobile)from, this));
//                            BeginCombine(from);
//                        }
//                    }
                    else if (curItem is Granite)
                    {

                        if (Granite + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((Granite + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            curItem.Delete();
                            Granite = (Granite + curItem.Amount);
                            from.SendGump(new ResourceStorageKeyMasonryGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                }
            }
            else
            {
                from.SendLocalizedMessage(1045158); // You must have the item in your backpack to target it.
            }
        }
        public ResourceStorageKeyMasonry(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)1); // version

//            writer.Write((int)m_Fluorite);
//            writer.Write((int)m_Platinum);

            writer.Write((int)m_Granite);
            writer.Write((int)m_DullCopper);
            writer.Write((int)m_ShadowIron);
            writer.Write((int)m_Copper);
            writer.Write((int)m_Bronze);
            writer.Write((int)m_Gold);
            writer.Write((int)m_Agapite);
            writer.Write((int)m_Verite);
            writer.Write((int)m_Valorite);
            writer.Write((int)m_StorageLimit);
            writer.Write((int)m_WithdrawIncrement);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            switch (version)
            {
                case 1:
//                    m_Fluorite = reader.ReadInt();
//                    m_Platinum = reader.ReadInt();
                    goto case 0;
                case 0:
                    {
                        m_Granite = reader.ReadInt();
                        m_DullCopper = reader.ReadInt();
                        m_ShadowIron = reader.ReadInt();
                        m_Copper = reader.ReadInt();
                        m_Bronze = reader.ReadInt();
                        m_Gold = reader.ReadInt();
                        m_Agapite = reader.ReadInt();
                        m_Verite = reader.ReadInt();
                        m_Valorite = reader.ReadInt();
                        m_StorageLimit = reader.ReadInt();
                        m_WithdrawIncrement = reader.ReadInt();
                        break;
                    }
            }

            LootType = LootType.Blessed;
        }
    }

    public class ResourceStorageKeyMasonryGump : Gump
    {
        private PlayerMobile m_From;
        private ResourceStorageKeyMasonry m_Key;

        public ResourceStorageKeyMasonryGump(PlayerMobile from, ResourceStorageKeyMasonry key)
            : base(25, 25)
        {
            m_From = from;
            m_Key = key;

            m_From.CloseGump(typeof(ResourceStorageKeyMasonryGump));

            AddPage(0);

            AddBackground(50, 10, 455, 260, 5054);
            AddImageTiled(58, 20, 438, 241, 2624);
            AddAlphaRegion(58, 20, 438, 241);

            AddLabel(200, 25, 88, "Masonry Warehouse");

            AddLabel(125, 50, 0x486, "Granite");
            AddLabel(225, 50, 0x480, key.Granite.ToString());
            AddButton(75, 50, 4005, 4007, 1, GumpButtonType.Reply, 0);

            AddLabel(125, 75, 0x486, "Dull Copper");
            AddLabel(225, 75, 0x480, key.DullCopper.ToString());
            AddButton(75, 75, 4005, 4007, 2, GumpButtonType.Reply, 0);

            AddLabel(125, 100, 0x486, "Shadow Iron");
            AddLabel(225, 100, 0x480, key.ShadowIron.ToString());
            AddButton(75, 100, 4005, 4007, 3, GumpButtonType.Reply, 0);

            AddLabel(125, 125, 0x486, "Copper");
            AddLabel(225, 125, 0x480, key.Copper.ToString());
            AddButton(75, 125, 4005, 4007, 4, GumpButtonType.Reply, 0);

            AddLabel(125, 150, 0x486, "Bronze");
            AddLabel(225, 150, 0x480, key.Bronze.ToString());
            AddButton(75, 150, 4005, 4007, 5, GumpButtonType.Reply, 0);

            AddLabel(125, 175, 0x486, "Gold");
            AddLabel(225, 175, 0x480, key.Gold.ToString());
            AddButton(75, 175, 4005, 4007, 6, GumpButtonType.Reply, 0);

            AddLabel(125, 200, 0x486, "Agapite");
            AddLabel(225, 200, 0x480, key.Agapite.ToString());
            AddButton(75, 200, 4005, 4007, 7, GumpButtonType.Reply, 0);

            AddLabel(125, 225, 0x486, "Verite");
            AddLabel(225, 225, 0x480, key.Verite.ToString());
            AddButton(75, 225, 4005, 4007, 8, GumpButtonType.Reply, 0);

            AddLabel(325, 50, 0x486, "Valorite");
            AddLabel(425, 50, 0x480, key.Valorite.ToString());
            AddButton(275, 50, 4005, 4007, 9, GumpButtonType.Reply, 0);

//            AddLabel(325, 75, 0x486, "Fluorite");
//            AddLabel(425, 75, 0x480, key.Fluorite.ToString());
//            AddButton(275, 75, 4005, 4007, 10, GumpButtonType.Reply, 0);
//
//            AddLabel(325, 100, 0x486, "Platinum");
//            AddLabel(425, 100, 0x480, key.Platinum.ToString());
//            AddButton(275, 100, 4005, 4007, 11, GumpButtonType.Reply, 0);
			
            AddLabel(325, 175, 88, "Each Max:");
            AddLabel(425, 175, 0x480, key.StorageLimit.ToString());

            AddLabel(325, 200, 88, "Add Granite");
            AddButton(275, 200, 4005, 4007, 15, GumpButtonType.Reply, 0);

            AddLabel(325, 225, 88, "Collect all from backpack");
            AddButton(275, 225, 4005, 4007, 16, GumpButtonType.Reply, 0);
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            if (m_Key.Deleted)
                return;
            else if (!m_Key.IsChildOf(m_From.Backpack) && !m_Key.IsChildOf(m_From.BankBox))
            {
                m_From.SendMessage("That key must be in your backpack or bankbox for you to use it.");
                return;
            }
            else if (info.ButtonID == 1)
            {
                if (m_Key.Granite > 0)								//if the key currently holds more ot this type than the increment amount
                {
                    m_From.AddToBackpack(new Granite());  	//Send the increment amount of this type to players backpack
                    m_Key.Granite = (m_Key.Granite - 1);				//removes that many from the keys count
                    m_From.SendGump(new ResourceStorageKeyMasonryGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else if (m_Key.Granite > 0)
                {
                    m_From.AddToBackpack(new Granite(m_Key.Granite));  					//Sends all stored granite of whichever type to players backpack
                    m_Key.Granite = 0;						     						//Sets the count in the key back to 0
                    m_From.SendGump(new ResourceStorageKeyMasonryGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else
                {
                    m_From.SendMessage("You do not have any of that Granite!");
                    m_From.SendGump(new ResourceStorageKeyMasonryGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 2)
            {
                if (m_Key.DullCopper > 0)								//if the key currently holds more ot this type than the increment amount
                {
                    m_From.AddToBackpack(new DullCopperGranite());  	//Send the increment amount of this type to players backpack
                    m_Key.DullCopper = (m_Key.DullCopper - 1);				//removes that many from the keys count
                    m_From.SendGump(new ResourceStorageKeyMasonryGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else if (m_Key.DullCopper > 0)
                {
                    m_From.AddToBackpack(new DullCopperGranite(m_Key.DullCopper));  					//Sends all stored DullCopper of whichever type to players backpack
                    m_Key.DullCopper = 0;						     						//Sets the count in the key back to 0
                    m_From.SendGump(new ResourceStorageKeyMasonryGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else
                {
                    m_From.SendMessage("You do not have any of that Granite!");
                    m_From.SendGump(new ResourceStorageKeyMasonryGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 3)
            {
                if (m_Key.ShadowIron > 0)								//if the key currently holds more ot this type than the increment amount
                {
                    m_From.AddToBackpack(new ShadowIronGranite());  	//Send the increment amount of this type to players backpack
                    m_Key.ShadowIron = (m_Key.ShadowIron - 1);				//removes that many from the keys count
                    m_From.SendGump(new ResourceStorageKeyMasonryGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else if (m_Key.ShadowIron > 0)
                {
                    m_From.AddToBackpack(new ShadowIronGranite(m_Key.ShadowIron));  					//Sends all stored ShadowIron of whichever type to players backpack
                    m_Key.ShadowIron = 0;						     						//Sets the count in the key back to 0
                    m_From.SendGump(new ResourceStorageKeyMasonryGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else
                {
                    m_From.SendMessage("You do not have any of that Granite!");
                    m_From.SendGump(new ResourceStorageKeyMasonryGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 4)
            {
                if (m_Key.Copper > 0)								//if the key currently holds more ot this type than the increment amount
                {
                    m_From.AddToBackpack(new CopperGranite());  	//Send the increment amount of this type to players backpack
                    m_Key.Copper = (m_Key.Copper - 1);				//removes that many from the keys count
                    m_From.SendGump(new ResourceStorageKeyMasonryGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else if (m_Key.Copper > 0)
                {
                    m_From.AddToBackpack(new CopperGranite(m_Key.Copper));  					//Sends all stored Copper of whichever type to players backpack
                    m_Key.Copper = 0;						     						//Sets the count in the key back to 0
                    m_From.SendGump(new ResourceStorageKeyMasonryGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else
                {
                    m_From.SendMessage("You do not have any of that Granite!");
                    m_From.SendGump(new ResourceStorageKeyMasonryGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 5)
            {
                if (m_Key.Bronze > 0)								//if the key currently holds more ot this type than the increment amount
                {
                    m_From.AddToBackpack(new BronzeGranite());  	//Send the increment amount of this type to players backpack
                    m_Key.Bronze = (m_Key.Bronze - 1);				//removes that many from the keys count
                    m_From.SendGump(new ResourceStorageKeyMasonryGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else if (m_Key.Bronze > 0)
                {
                    m_From.AddToBackpack(new BronzeGranite(m_Key.Bronze));  					//Sends all stored Bronze of whichever type to players backpack
                    m_Key.Bronze = 0;						     						//Sets the count in the key back to 0
                    m_From.SendGump(new ResourceStorageKeyMasonryGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else
                {
                    m_From.SendMessage("You do not have any of that Granite!");
                    m_From.SendGump(new ResourceStorageKeyMasonryGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 6)
            {
                if (m_Key.Gold > 0)								//if the key currently holds more ot this type than the increment amount
                {
                    m_From.AddToBackpack(new GoldGranite());  	//Send the increment amount of this type to players backpack
                    m_Key.Gold = (m_Key.Gold - 1);				//removes that many from the keys count
                    m_From.SendGump(new ResourceStorageKeyMasonryGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else if (m_Key.Gold > 0)
                {
                    m_From.AddToBackpack(new GoldGranite(m_Key.Gold));  					//Sends all stored Gold of whichever type to players backpack
                    m_Key.Gold = 0;						     						//Sets the count in the key back to 0
                    m_From.SendGump(new ResourceStorageKeyMasonryGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else
                {
                    m_From.SendMessage("You do not have any of that Granite!");
                    m_From.SendGump(new ResourceStorageKeyMasonryGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 7)
            {
                if (m_Key.Agapite > 0)								//if the key currently holds more ot this type than the increment amount
                {
                    m_From.AddToBackpack(new AgapiteGranite());  	//Send the increment amount of this type to players backpack
                    m_Key.Agapite = (m_Key.Agapite - 1);				//removes that many from the keys count
                    m_From.SendGump(new ResourceStorageKeyMasonryGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else if (m_Key.Agapite > 0)
                {
                    m_From.AddToBackpack(new AgapiteGranite(m_Key.Agapite));  					//Sends all stored Agapite of whichever type to players backpack
                    m_Key.Agapite = 0;						     						//Sets the count in the key back to 0
                    m_From.SendGump(new ResourceStorageKeyMasonryGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else
                {
                    m_From.SendMessage("You do not have any of that Granite!");
                    m_From.SendGump(new ResourceStorageKeyMasonryGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 8)
            {
                if (m_Key.Verite > 0)								//if the key currently holds more ot this type than the increment amount
                {
                    m_From.AddToBackpack(new VeriteGranite());  	//Send the increment amount of this type to players backpack
                    m_Key.Verite = (m_Key.Verite - 1);				//removes that many from the keys count
                    m_From.SendGump(new ResourceStorageKeyMasonryGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else if (m_Key.Verite > 0)
                {
                    m_From.AddToBackpack(new VeriteGranite(m_Key.Verite));  					//Sends all stored Verite of whichever type to players backpack
                    m_Key.Verite = 0;						     						//Sets the count in the key back to 0
                    m_From.SendGump(new ResourceStorageKeyMasonryGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else
                {
                    m_From.SendMessage("You do not have any of that Granite!");
                    m_From.SendGump(new ResourceStorageKeyMasonryGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 9)
            {
                if (m_Key.Valorite > 0)								//if the key currently holds more ot this type than the increment amount
                {
                    m_From.AddToBackpack(new ValoriteGranite());  	//Send the increment amount of this type to players backpack
                    m_Key.Valorite = (m_Key.Valorite - 1);				//removes that many from the keys count
                    m_From.SendGump(new ResourceStorageKeyMasonryGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else if (m_Key.Valorite > 0)
                {
                    m_From.AddToBackpack(new ValoriteGranite(m_Key.Valorite));  					//Sends all stored Valorite of whichever type to players backpack
                    m_Key.Valorite = 0;						     						//Sets the count in the key back to 0
                    m_From.SendGump(new ResourceStorageKeyMasonryGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else
                {
                    m_From.SendMessage("You do not have any of that Granite!");
                    m_From.SendGump(new ResourceStorageKeyMasonryGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
//            else if (info.ButtonID == 10)
//            {
//                if (m_Key.Fluorite > m_Key.WithdrawIncrement)								//if the key currently holds more ot this type than the increment amount
//                {
//                    m_From.AddToBackpack(new FluoriteGranite(m_Key.WithdrawIncrement));  	//Send the increment amount of this type to players backpack
//                    m_Key.Fluorite = m_Key.Fluorite - m_Key.WithdrawIncrement;				//removes that many from the keys count
//                    m_From.SendGump(new ResourceStorageKeyMasonryGump(m_From, m_Key));					//Resets the gump with the new info
//                }
//                else if (m_Key.Fluorite > 0)
//                {
//                    m_From.AddToBackpack(new FluoriteGranite(m_Key.Fluorite));  					//Sends all stored Valorite of whichever type to players backpack
//                    m_Key.Fluorite = 0;						     						//Sets the count in the key back to 0
//                    m_From.SendGump(new ResourceStorageKeyMasonryGump(m_From, m_Key));					//Resets the gump with the new info
//                }
//                else
//                {
//                    m_From.SendMessage("You do not have any of that Granite!");
//                    m_From.SendGump(new ResourceStorageKeyMasonryGump(m_From, m_Key));
//                    m_Key.BeginCombine(m_From);
//                }
//            }
//            else if (info.ButtonID == 11)
//            {
//                if (m_Key.Platinum > m_Key.WithdrawIncrement)								//if the key currently holds more ot this type than the increment amount
//                {
//                    m_From.AddToBackpack(new PlatinumGranite(m_Key.WithdrawIncrement));  	//Send the increment amount of this type to players backpack
//                    m_Key.Platinum = m_Key.Platinum - m_Key.WithdrawIncrement;				//removes that many from the keys count
//                    m_From.SendGump(new ResourceStorageKeyMasonryGump(m_From, m_Key));					//Resets the gump with the new info
//                }
//                else if (m_Key.Platinum > 0)
//                {
//                    m_From.AddToBackpack(new PlatinumGranite(m_Key.Platinum));  					//Sends all stored Valorite of whichever type to players backpack
//                    m_Key.Platinum = 0;						     						//Sets the count in the key back to 0
//                    m_From.SendGump(new ResourceStorageKeyMasonryGump(m_From, m_Key));					//Resets the gump with the new info
//                }
//                else
//                {
//                    m_From.SendMessage("You do not have any of that Granite!");
//                    m_From.SendGump(new ResourceStorageKeyMasonryGump(m_From, m_Key));
//                    m_Key.BeginCombine(m_From);
//                }
//            }
            else if (info.ButtonID == 15)
            {
                m_From.SendGump(new ResourceStorageKeyMasonryGump(m_From, m_Key));
                m_Key.BeginCombine(m_From);
            }
            else if (info.ButtonID == 16)
            {
                m_Key.CollectFromBackpack(m_From, true);
                m_From.SendGump(new ResourceStorageKeyMasonryGump(m_From, m_Key));
            }
        }
    }
}

namespace Server.Items
{
    public class ResourceStorageKeyMasonryTarget : Target
    {
        private ResourceStorageKeyMasonry m_Key;

        public ResourceStorageKeyMasonryTarget(ResourceStorageKeyMasonry key)
            : base(18, false, TargetFlags.None)
        {
            m_Key = key;
        }

        protected override void OnTarget(Mobile from, object targeted)
        {
            if (m_Key.Deleted)
                return;

            m_Key.EndCombine(from, targeted);
        }
    }
}



