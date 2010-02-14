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
    public class ResourceStorageKeyJewel : Item
    {
        private int m_Diamond;
        private int m_Sapphire;
        private int m_StarSapphire;
        private int m_Emerald;
        private int m_Ruby;
        private int m_Amethyst;
        private int m_Citrine;
        private int m_Tourmaline;
        private int m_Amber;
        private int m_FireRuby;
        private int m_DarkSapphire;
        private int m_WhitePearl;

        private int m_Gold;
//        private int m_Apple;

        private int m_StorageLimit;
        private int m_WithdrawIncrement;

        [CommandProperty(AccessLevel.GameMaster)]
        public int StorageLimit { get { return m_StorageLimit; } set { m_StorageLimit = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int WithdrawIncrement { get { return m_WithdrawIncrement; } set { m_WithdrawIncrement = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Diamond { get { return m_Diamond; } set { m_Diamond = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Sapphire { get { return m_Sapphire; } set { m_Sapphire = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int StarSapphire { get { return m_StarSapphire; } set { m_StarSapphire = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Emerald { get { return m_Emerald; } set { m_Emerald = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Ruby { get { return m_Ruby; } set { m_Ruby = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Amethyst { get { return m_Amethyst; } set { m_Amethyst = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Citrine { get { return m_Citrine; } set { m_Citrine = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Tourmaline { get { return m_Tourmaline; } set { m_Tourmaline = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Amber { get { return m_Amber; } set { m_Amber = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int FireRuby { get { return m_FireRuby; } set { m_FireRuby = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int DarkSapphire { get { return m_DarkSapphire; } set { m_DarkSapphire = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int WhitePearl { get { return m_WhitePearl; } set { m_WhitePearl = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Gold { get { return m_Gold; } set { m_Gold = value; InvalidateProperties(); } }

//        [CommandProperty(AccessLevel.GameMaster)]
//        public int Apple { get { return m_Apple; } set { m_Apple = value; InvalidateProperties(); } }

        public void CollectFromBackpack(Mobile from, bool showMessage)
        {
            if (!from.Alive)
                return;
            if (!this.IsChildOf(from.Backpack) && !this.IsChildOf(from.BankBox))
            {
                from.SendMessage("That tinker's key has to be in your backpack or bankbox for you to use it.");
                return;
            }

            Type[] type = new Type[] { typeof(Diamond), typeof(Sapphire), typeof(StarSapphire),
                typeof(Emerald), typeof(Ruby), typeof(Amethyst), typeof(Citrine), typeof(Tourmaline),
                typeof(Amber), typeof(FireRuby), typeof(DarkSapphire), typeof(WhitePearl)
            };
            Item[] items = from.Backpack.FindItemsByType(type, true);
            for (int j = 0; j < items.Length; j++)
            {
                int currentAmount = 0;

                Item item = items[j];
                if (!item.Movable)
                    continue;
                int amount = item.Amount;

                if (item is Diamond)
                    currentAmount = m_Diamond;
                else if (item is Sapphire)
                    currentAmount = m_Sapphire;
                else if (item is StarSapphire)
                    currentAmount = m_StarSapphire;
                else if (item is Emerald)
                    currentAmount = m_Emerald;
                else if (item is Ruby)
                    currentAmount = m_Ruby;
                else if (item is Amethyst)
                    currentAmount = m_Amethyst;
                else if (item is Citrine)
                    currentAmount = m_Citrine;
                else if (item is Tourmaline)
                    currentAmount = m_Tourmaline;
                else if (item is Amber)
                    currentAmount = m_Amber;
                else if (item is FireRuby)
                	currentAmount = m_FireRuby;
                else if (item is DarkSapphire)
                	currentAmount = m_DarkSapphire;
                else if (item is WhitePearl)
                	currentAmount = m_WhitePearl;
                else if (item is Gold)
                    currentAmount = m_Gold;
                	
//                else if (item is Apple)
//                    currentAmount = m_Apple;

                if (amount + currentAmount > m_StorageLimit)
                    continue;
                else
                {
                    if (item is Diamond)
                        m_Diamond += amount;
                    else if (item is Sapphire)
                        m_Sapphire += amount;
                    else if (item is StarSapphire)
                        m_StarSapphire += amount;
                    else if (item is Emerald)
                        m_Emerald += amount;
                    else if (item is Ruby)
                        m_Ruby += amount;
                    else if (item is Amethyst)
                        m_Amethyst += amount;
                    else if (item is Citrine)
                        m_Citrine += amount;
                    else if (item is Tourmaline)
                        m_Tourmaline += amount;
                    else if (item is Amber)
                        m_Amber += amount;
                    else if (item is FireRuby)
                        m_FireRuby += amount;
                    else if (item is DarkSapphire)
                        m_DarkSapphire += amount;
                    else if (item is WhitePearl)
                        m_WhitePearl += amount;
                    else if (item is Gold)
                        m_Gold += amount;
//                    else if (item is Apple)
//                        m_Apple += amount;

                    item.Delete();
                }
            }

            if (showMessage)
            from.SendMessage("Gems are collected from your backpack into that key, subject to storage limit.");
        }

        [Constructable]
        public ResourceStorageKeyJewel()
            : base(0x176B)
        {
            Movable = true;
            Weight = 1.0;
            Hue = 1161;
            Name = "Jewel Worker's Keys";
            LootType = LootType.Blessed;
            StorageLimit = 30000;
            WithdrawIncrement = 100;
        }

        [Constructable]
        public ResourceStorageKeyJewel(int storageLimit, int withdrawIncrement)
            : base(0x176B)
        {
            Movable = true;
            Weight = 1.0;
            Hue = 2213;
            Name = "Jewel Worker's Keys";
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
                from.SendGump(new ResourceStorageKeyJewelGump((PlayerMobile)from, this));
            else
                from.SendMessage("This must be in your backpack or bankbox.");
        }

        public void BeginCombine(Mobile from)
        {
            from.Target = new ResourceStorageKeyJewelTarget(this);
        }

        public void EndCombine(Mobile from, object o)
        {
            if (o is Item && (((Item)o).IsChildOf(from.Backpack) || ((Item)o).IsChildOf(from.BankBox)))
            {
                Item curItem = o as Item;
                if (curItem is Item)
                {
                    if (curItem is Sapphire)
                    {
                        if (Sapphire + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((Sapphire + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");

                        else
                        {
                            curItem.Delete();
                            Sapphire = (Sapphire + curItem.Amount);
                            from.SendGump(new ResourceStorageKeyJewelGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is StarSapphire)
                    {

                        if (StarSapphire + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((StarSapphire + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");

                        else
                        {
                            curItem.Delete();
                            StarSapphire = (StarSapphire + curItem.Amount);
                            from.SendGump(new ResourceStorageKeyJewelGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is Emerald)
                    {
                        if (Emerald + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((Ruby + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            curItem.Delete();
                            Emerald = (Emerald + curItem.Amount);
                            from.SendGump(new ResourceStorageKeyJewelGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is Ruby)
                    {
                        if (Ruby + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((Ruby + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            curItem.Delete();
                            Ruby = (Ruby + curItem.Amount);
                            from.SendGump(new ResourceStorageKeyJewelGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is Amethyst)
                    {

                        if (Amethyst + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((Amethyst + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            curItem.Delete();
                            Amethyst = (Amethyst + curItem.Amount);
                            from.SendGump(new ResourceStorageKeyJewelGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is Citrine)
                    {

                        if (Citrine + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((Citrine + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            curItem.Delete();
                            Citrine = (Citrine + curItem.Amount);
                            from.SendGump(new ResourceStorageKeyJewelGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is Tourmaline)
                    {

                        if (Tourmaline + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((Tourmaline + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            curItem.Delete();
                            Tourmaline = (Tourmaline + curItem.Amount);
                            from.SendGump(new ResourceStorageKeyJewelGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is Amber)
                    {

                        if (Amber + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((Amber + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            curItem.Delete();
                            Amber = (Amber + curItem.Amount);
                            from.SendGump(new ResourceStorageKeyJewelGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is Gold)
                    {

                        if (Gold + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((Gold + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            curItem.Delete();
                            Gold = (Gold + curItem.Amount);
                            from.SendGump(new ResourceStorageKeyJewelGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is FireRuby)
                    {

                        if (FireRuby + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((FireRuby + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            curItem.Delete();
                            FireRuby = (FireRuby + curItem.Amount);
                            from.SendGump(new ResourceStorageKeyJewelGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is DarkSapphire)
                    {

                        if (DarkSapphire + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((DarkSapphire + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            curItem.Delete();
                            DarkSapphire = (DarkSapphire + curItem.Amount);
                            from.SendGump(new ResourceStorageKeyJewelGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is WhitePearl)
                    {

                        if (WhitePearl + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((WhitePearl + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            curItem.Delete();
                            WhitePearl = (WhitePearl + curItem.Amount);
                            from.SendGump(new ResourceStorageKeyJewelGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
//                    else if (curItem is Apple)
//                    {
//
//                        if (Apple + curItem.Amount > StorageLimit)
//                            from.SendMessage("You are trying to add " + ((Apple + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
//                        else
//                        {
//                            curItem.Delete();
//                            Apple = (Apple + curItem.Amount);
//                            from.SendGump(new ResourceStorageKeyJewelGump((PlayerMobile)from, this));
//                            BeginCombine(from);
//                        }
//                    }
                    else if (curItem is Diamond)
                    {

                        if (Diamond + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + (( + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            curItem.Delete();
                            Diamond = ( Diamond + curItem.Amount);
                            from.SendGump(new ResourceStorageKeyJewelGump((PlayerMobile)from, this));
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
        public ResourceStorageKeyJewel(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version

            writer.Write((int)m_Gold);
//            writer.Write((int)m_Apple);

            writer.Write((int)m_Diamond);
            writer.Write((int)m_Sapphire);
            writer.Write((int)m_StarSapphire);
            writer.Write((int)m_Emerald);
            writer.Write((int)m_Ruby);
            writer.Write((int)m_Amethyst);
            writer.Write((int)m_Citrine);
            writer.Write((int)m_Tourmaline);
            writer.Write((int)m_Amber);
            writer.Write((int)m_StorageLimit);
            writer.Write((int)m_WithdrawIncrement);
            writer.Write((int)m_FireRuby);
            writer.Write((int)m_DarkSapphire);
            writer.Write((int)m_WhitePearl);
            
            
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            m_Gold = reader.ReadInt();
//            m_Apple = reader.ReadInt();
            m_Diamond = reader.ReadInt();
            m_Sapphire = reader.ReadInt();
            m_StarSapphire = reader.ReadInt();
            m_Emerald = reader.ReadInt();
            m_Ruby = reader.ReadInt();
            m_Amethyst = reader.ReadInt();
            m_Citrine = reader.ReadInt();
            m_Tourmaline = reader.ReadInt();
            m_Amber = reader.ReadInt();
            m_StorageLimit = reader.ReadInt();
            m_WithdrawIncrement = reader.ReadInt();
            m_FireRuby = reader.ReadInt();
            m_DarkSapphire = reader.ReadInt();
            m_WhitePearl = reader.ReadInt();            
        }
    }

    public class ResourceStorageKeyJewelGump : Gump
    {
        private PlayerMobile m_From;
        private ResourceStorageKeyJewel m_Key;

        public ResourceStorageKeyJewelGump(PlayerMobile from, ResourceStorageKeyJewel key)
            : base(25, 25)
        {
            m_From = from;
            m_Key = key;

            m_From.CloseGump(typeof(ResourceStorageKeyJewelGump));

            AddPage(0);

            AddBackground(50, 10, 455, 280, 5054);
            AddImageTiled(58, 20, 438, 260, 2624);
            AddAlphaRegion(58, 20, 438, 260);

            AddLabel(200, 25, 88, "Jewel  Warehouse");
	    AddLabel(125, 50, 0x486, "Withdraw Increment:");
	    AddLabel(275, 50, 0x480, key.WithdrawIncrement.ToString());
	    AddButton(330, 50, 4011, 4012, 17, GumpButtonType.Reply, 0);
	    AddButton(360, 50, 4011, 4012, 18, GumpButtonType.Reply, 0);
	    AddButton(390, 50, 4011, 4012, 19, GumpButtonType.Reply, 0);

            AddLabel(125, 75, 0x486, "Diamond");
            AddLabel(225, 75, 0x480, key.Diamond.ToString());
            AddButton(75, 75, 4005, 4007, 1, GumpButtonType.Reply, 0);

            AddLabel(125, 100, 0x486, "Sapphire");
            AddLabel(225, 100, 0x480, key.Sapphire.ToString());
            AddButton(75, 100, 4005, 4007, 2, GumpButtonType.Reply, 0);

            AddLabel(125, 125, 0x486, "Star Sapphire");
            AddLabel(225, 125, 0x480, key.StarSapphire.ToString());
            AddButton(75, 125, 4005, 4007, 3, GumpButtonType.Reply, 0);

            AddLabel(125, 150, 0x486, "Emerald");
            AddLabel(225, 150, 0x480, key.Emerald.ToString());
            AddButton(75, 150, 4005, 4007, 4, GumpButtonType.Reply, 0);

            AddLabel(125, 175, 0x486, "Ruby");
            AddLabel(225, 175, 0x480, key.Ruby.ToString());
            AddButton(75, 175, 4005, 4007, 5, GumpButtonType.Reply, 0);

            AddLabel(125, 200, 0x486, "Amethyst");
            AddLabel(225, 200, 0x480, key.Amethyst.ToString());
            AddButton(75, 200, 4005, 4007, 6, GumpButtonType.Reply, 0);

            AddLabel(125, 225, 0x486, "Citrine");
            AddLabel(225, 225, 0x480, key.Citrine.ToString());
            AddButton(75, 225, 4005, 4007, 7, GumpButtonType.Reply, 0);

            AddLabel(125, 250, 0x486, "Tourmaline");
            AddLabel(225, 250, 0x480, key.Tourmaline.ToString());
            AddButton(75, 250, 4005, 4007, 8, GumpButtonType.Reply, 0);

            AddLabel(325, 75, 0x486, "Amber");
            AddLabel(425, 75, 0x480, key.Amber.ToString());
            AddButton(275, 75, 4005, 4007, 9, GumpButtonType.Reply, 0);

            AddLabel(325, 100, 0x486, "Gold");
            AddLabel(425, 100, 0x480, key.Gold.ToString());
            AddButton(275, 100, 4005, 4007, 10, GumpButtonType.Reply, 0);

            AddLabel(325, 125, 0x486, "DarkSapphire");
            AddLabel(425, 125, 0x480, key.DarkSapphire.ToString());
            AddButton(275, 125, 4005, 4007, 11, GumpButtonType.Reply, 0);

            AddLabel(325, 150, 0x486, "WhitePearl");
            AddLabel(425, 150, 0x480, key.WhitePearl.ToString());
            AddButton(275, 150, 4005, 4007, 12, GumpButtonType.Reply, 0);

            AddLabel(325, 175, 0x486, "FireRuby");
            AddLabel(425, 175, 0x480, key.FireRuby.ToString());
            AddButton(275, 175, 4005, 4007, 13, GumpButtonType.Reply, 0);

            //AddLabel(325, 125, 0x486, "Apple");
            //AddLabel(425, 125, 0x480, key.Apple.ToString());
            //AddButton(275, 125, 4005, 4007, 11, GumpButtonType.Reply, 0);

            AddLabel(325, 200, 88, "Each Max:");
            AddLabel(425, 200, 0x480, key.StorageLimit.ToString());

            AddLabel(325, 225, 88, "Add ");
            AddButton(275, 225, 4005, 4007, 15, GumpButtonType.Reply, 0);

            AddLabel(325, 250, 88, "Collect all from backpack");
            AddButton(275, 250, 4005, 4007, 16, GumpButtonType.Reply, 0);
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
	    else if (info.ButtonID == 17)
	    {
		m_Key.WithdrawIncrement = 100;
		m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));
	    }
	    else if (info.ButtonID == 18)
	    {
		m_Key.WithdrawIncrement = 500;
		m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));
	    }
	    else if (info.ButtonID == 19)
	    {
		m_Key.WithdrawIncrement = 1000;
		m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));
	    }
            else if (info.ButtonID == 1)
            {
                if (m_Key.Diamond > m_Key.WithdrawIncrement)								//if the key currently holds more ot this type than the increment amount
                {
                    m_From.AddToBackpack(new Diamond(m_Key.WithdrawIncrement));  	//Send the increment amount of this type to players backpack
                    m_Key.Diamond = m_Key.Diamond - m_Key.WithdrawIncrement;				//removes that many from the keys count
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else if (m_Key.Diamond > 0)
                {
                    m_From.AddToBackpack(new Diamond(m_Key.Diamond));  					//Sends all stored  of whichever type to players backpack
                    m_Key.Diamond = 0;						     						//Sets the count in the key back to 0
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else
                {
                    m_From.SendMessage("You do not have any of that !");
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 2)
            {
                if (m_Key.Sapphire > m_Key.WithdrawIncrement)								//if the key currently holds more ot this type than the increment amount
                {
                    m_From.AddToBackpack(new Sapphire(m_Key.WithdrawIncrement));  	//Send the increment amount of this type to players backpack
                    m_Key.Sapphire = m_Key.Sapphire - m_Key.WithdrawIncrement;				//removes that many from the keys count
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else if (m_Key.Sapphire > 0)
                {
                    m_From.AddToBackpack(new Sapphire(m_Key.Sapphire));  					//Sends all stored Sapphire of whichever type to players backpack
                    m_Key.Sapphire = 0;						     						//Sets the count in the key back to 0
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else
                {
                    m_From.SendMessage("You do not have any of that !");
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 3)
            {
                if (m_Key.StarSapphire > m_Key.WithdrawIncrement)								//if the key currently holds more ot this type than the increment amount
                {
                    m_From.AddToBackpack(new StarSapphire(m_Key.WithdrawIncrement));  	//Send the increment amount of this type to players backpack
                    m_Key.StarSapphire = m_Key.StarSapphire - m_Key.WithdrawIncrement;				//removes that many from the keys count
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else if (m_Key.StarSapphire > 0)
                {
                    m_From.AddToBackpack(new StarSapphire(m_Key.StarSapphire));  					//Sends all stored StarSapphire of whichever type to players backpack
                    m_Key.StarSapphire = 0;						     						//Sets the count in the key back to 0
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else
                {
                    m_From.SendMessage("You do not have any of that !");
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 4)
            {
                if (m_Key.Emerald > m_Key.WithdrawIncrement)								//if the key currently holds more ot this type than the increment amount
                {
                    m_From.AddToBackpack(new Emerald(m_Key.WithdrawIncrement));  	//Send the increment amount of this type to players backpack
                    m_Key.Emerald = m_Key.Emerald - m_Key.WithdrawIncrement;				//removes that many from the keys count
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else if (m_Key.Emerald > 0)
                {
                    m_From.AddToBackpack(new Emerald(m_Key.Emerald));  					//Sends all stored Emerald of whichever type to players backpack
                    m_Key.Emerald = 0;						     						//Sets the count in the key back to 0
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else
                {
                    m_From.SendMessage("You do not have any of that !");
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 5)
            {
                if (m_Key.Ruby > m_Key.WithdrawIncrement)								//if the key currently holds more ot this type than the increment amount
                {
                    m_From.AddToBackpack(new Ruby(m_Key.WithdrawIncrement));  	//Send the increment amount of this type to players backpack
                    m_Key.Ruby = m_Key.Ruby - m_Key.WithdrawIncrement;				//removes that many from the keys count
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else if (m_Key.Ruby > 0)
                {
                    m_From.AddToBackpack(new Ruby(m_Key.Ruby));  					//Sends all stored Ruby of whichever type to players backpack
                    m_Key.Ruby = 0;						     						//Sets the count in the key back to 0
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else
                {
                    m_From.SendMessage("You do not have any of that !");
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 6)
            {
                if (m_Key.Amethyst > m_Key.WithdrawIncrement)								//if the key currently holds more ot this type than the increment amount
                {
                    m_From.AddToBackpack(new Amethyst(m_Key.WithdrawIncrement));  	//Send the increment amount of this type to players backpack
                    m_Key.Amethyst = m_Key.Amethyst - m_Key.WithdrawIncrement;				//removes that many from the keys count
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else if (m_Key.Amethyst > 0)
                {
                    m_From.AddToBackpack(new Amethyst(m_Key.Amethyst));  					//Sends all stored Amethyst of whichever type to players backpack
                    m_Key.Amethyst = 0;						     						//Sets the count in the key back to 0
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else
                {
                    m_From.SendMessage("You do not have any of that !");
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 7)
            {
                if (m_Key.Citrine > m_Key.WithdrawIncrement)								//if the key currently holds more ot this type than the increment amount
                {
                    m_From.AddToBackpack(new Citrine(m_Key.WithdrawIncrement));  	//Send the increment amount of this type to players backpack
                    m_Key.Citrine = m_Key.Citrine - m_Key.WithdrawIncrement;				//removes that many from the keys count
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else if (m_Key.Citrine > 0)
                {
                    m_From.AddToBackpack(new Citrine(m_Key.Citrine));  					//Sends all stored Citrine of whichever type to players backpack
                    m_Key.Citrine = 0;						     						//Sets the count in the key back to 0
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else
                {
                    m_From.SendMessage("You do not have any of that !");
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 8)
            {
                if (m_Key.Tourmaline > m_Key.WithdrawIncrement)								//if the key currently holds more ot this type than the increment amount
                {
                    m_From.AddToBackpack(new Tourmaline(m_Key.WithdrawIncrement));  	//Send the increment amount of this type to players backpack
                    m_Key.Tourmaline = m_Key.Tourmaline - m_Key.WithdrawIncrement;				//removes that many from the keys count
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else if (m_Key.Tourmaline > 0)
                {
                    m_From.AddToBackpack(new Tourmaline(m_Key.Tourmaline));  					//Sends all stored Tourmaline of whichever type to players backpack
                    m_Key.Tourmaline = 0;						     						//Sets the count in the key back to 0
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else
                {
                    m_From.SendMessage("You do not have any of that !");
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 9)
            {
                if (m_Key.Amber > m_Key.WithdrawIncrement)								//if the key currently holds more ot this type than the increment amount
                {
                    m_From.AddToBackpack(new Amber(m_Key.WithdrawIncrement));  	//Send the increment amount of this type to players backpack
                    m_Key.Amber = m_Key.Amber - m_Key.WithdrawIncrement;				//removes that many from the keys count
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else if (m_Key.Amber > 0)
                {
                    m_From.AddToBackpack(new Amber(m_Key.Amber));  					//Sends all stored Amber of whichever type to players backpack
                    m_Key.Amber = 0;						     						//Sets the count in the key back to 0
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else
                {
                    m_From.SendMessage("You do not have any of that !");
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 10)
            {
                int withdrawIncrement1 = 2500;
                int withdrawIncrement2 = 1000;
                if (m_Key.Gold > withdrawIncrement1)								//if the key currently holds more ot this type than the increment amount
                {
                    m_From.AddToBackpack(new Gold(withdrawIncrement1));  	//Send the increment amount of this type to players backpack
                    m_Key.Gold = m_Key.Gold - withdrawIncrement1;				//removes that many from the keys count
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else if (m_Key.Gold > withdrawIncrement2)								//if the key currently holds more ot this type than the increment amount
                {
                    m_From.AddToBackpack(new Gold(withdrawIncrement2));  	//Send the increment amount of this type to players backpack
                    m_Key.Gold = m_Key.Gold - withdrawIncrement2;				//removes that many from the keys count
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else if (m_Key.Gold > 0)
                {
                    m_From.AddToBackpack(new Gold(m_Key.Gold));  					//Sends all stored Amber of whichever type to players backpack
                    m_Key.Gold = 0;						     						//Sets the count in the key back to 0
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else
                {
                    m_From.SendMessage("You do not have any of that !");
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 11)
            {
                if (m_Key.DarkSapphire > m_Key.WithdrawIncrement)								//if the key currently holds more ot this type than the increment amount
                {
                    m_From.AddToBackpack(new DarkSapphire(m_Key.WithdrawIncrement));  	//Send the increment amount of this type to players backpack
                    m_Key.DarkSapphire = m_Key.DarkSapphire - m_Key.WithdrawIncrement;				//removes that many from the keys count
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else if (m_Key.DarkSapphire > 0)
                {
                    m_From.AddToBackpack(new DarkSapphire(m_Key.DarkSapphire));  					//Sends all stored Amber of whichever type to players backpack
                    m_Key.DarkSapphire = 0;						     						//Sets the count in the key back to 0
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else
                {
                    m_From.SendMessage("You do not have any of that !");
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 12)
            {
                if (m_Key.WhitePearl > m_Key.WithdrawIncrement)								//if the key currently holds more ot this type than the increment amount
                {
                    m_From.AddToBackpack(new WhitePearl(m_Key.WithdrawIncrement));  	//Send the increment amount of this type to players backpack
                    m_Key.WhitePearl = m_Key.WhitePearl - m_Key.WithdrawIncrement;				//removes that many from the keys count
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else if (m_Key.WhitePearl > 0)
                {
                    m_From.AddToBackpack(new WhitePearl(m_Key.WhitePearl));  					//Sends all stored Amber of whichever type to players backpack
                    m_Key.WhitePearl = 0;						     						//Sets the count in the key back to 0
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else
                {
                    m_From.SendMessage("You do not have any of that !");
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 13)
            {
                if (m_Key.FireRuby > m_Key.WithdrawIncrement)								//if the key currently holds more ot this type than the increment amount
                {
                    m_From.AddToBackpack(new FireRuby(m_Key.WithdrawIncrement));  	//Send the increment amount of this type to players backpack
                    m_Key.FireRuby = m_Key.FireRuby - m_Key.WithdrawIncrement;				//removes that many from the keys count
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else if (m_Key.FireRuby > 0)
                {
                    m_From.AddToBackpack(new FireRuby(m_Key.FireRuby));  					//Sends all stored Amber of whichever type to players backpack
                    m_Key.FireRuby = 0;						     						//Sets the count in the key back to 0
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else
                {
                    m_From.SendMessage("You do not have any of that !");
                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
//            else if (info.ButtonID == 11)
//            {
//                if (m_Key.Apple > m_Key.WithdrawIncrement)								//if the key currently holds more ot this type than the increment amount
//                {
//                    m_From.AddToBackpack(new Apple(m_Key.WithdrawIncrement));  	//Send the increment amount of this type to players backpack
//                    m_Key.Apple = m_Key.Apple - m_Key.WithdrawIncrement;				//removes that many from the keys count
//                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));					//Resets the gump with the new info
//                }
//                else if (m_Key.Apple > 0)
//                {
//                    m_From.AddToBackpack(new Apple(m_Key.Apple));  					//Sends all stored Amber of whichever type to players backpack
//                    m_Key.Apple = 0;						     						//Sets the count in the key back to 0
//                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));					//Resets the gump with the new info
//                }
//                else
//                {
//                    m_From.SendMessage("You do not have any of that !");
//                    m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));
//                    m_Key.BeginCombine(m_From);
//                }
//            }
            else if (info.ButtonID == 15)
            {
                m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));
                m_Key.BeginCombine(m_From);
            }
            else if (info.ButtonID == 16)
            {
                m_Key.CollectFromBackpack(m_From, true);
                m_From.SendGump(new ResourceStorageKeyJewelGump(m_From, m_Key));
            }
        }
    }
}

namespace Server.Items
{
    public class ResourceStorageKeyJewelTarget : Target
    {
        private ResourceStorageKeyJewel m_Key;

        public ResourceStorageKeyJewelTarget(ResourceStorageKeyJewel key)
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
