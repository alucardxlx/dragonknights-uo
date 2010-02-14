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
Ingot Key script by GoldDrac13
Granite Box script by (unknown)
////////////////////////////////
////////////////////////////////////////
Modified by Ashlar, beloved of Morrigan.  
Modified by Tylius.
added scales + custom by daat99.
Modified a lof of the code by daat99.
Rewrote a lot of the code by daat99 and added custom leather on 13/01/2005.
////////////////////////////////////////
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
    public class ResourceStorageKeyTailor : Item
    {
        private int m_Leather;
        private int m_Spined;
        private int m_Horned;
        private int m_Barbed;

        private int m_Cloth;
        private int m_UncutCloth;
        private int m_BoltOfCloth;
        private int m_Yarn;
        private int m_SpoolOfThread;
        private int m_Wool;
        private int m_Flax;
        private int m_Cotton;
        private int m_Bandage;

        private int m_Bone;

//        private int m_RedScales;
//        private int m_YellowScales;
//        private int m_BlackScales;
//        private int m_GreenScales;
//        private int m_WhiteScales;
//        private int m_BlueScales;

        private int m_StorageLimit;
        private int m_WithdrawIncrement;

        [CommandProperty(AccessLevel.GameMaster)]
        public int StorageLimit { get { return m_StorageLimit; } set { m_StorageLimit = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int WithdrawIncrement { get { return m_WithdrawIncrement; } set { m_WithdrawIncrement = value; InvalidateProperties(); } }  //  I'm guessing this lets GMs set the value?  

        [CommandProperty(AccessLevel.GameMaster)]
        public int Leather { get { return m_Leather; } set { m_Leather = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Spined { get { return m_Spined; } set { m_Spined = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Horned { get { return m_Horned; } set { m_Horned = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Barbed { get { return m_Barbed; } set { m_Barbed = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Cloth { get { return m_Cloth; } set { m_Cloth = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int UncutCloth { get { return m_UncutCloth; } set { m_UncutCloth = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int BoltOfCloth { get { return m_BoltOfCloth; } set { m_BoltOfCloth = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Yarn { get { return m_Yarn; } set { m_Yarn = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int SpoolOfThread { get { return m_SpoolOfThread; } set { m_SpoolOfThread = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Wool { get { return m_Wool; } set { m_Wool = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Cotton { get { return m_Cotton; } set { m_Cotton = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Flax { get { return m_Flax; } set { m_Flax = value; InvalidateProperties(); } }

//        [CommandProperty(AccessLevel.GameMaster)]
//        public int RedScales { get { return m_RedScales; } set { m_RedScales = value; InvalidateProperties(); } }
//
//        [CommandProperty(AccessLevel.GameMaster)]
//        public int YellowScales { get { return m_YellowScales; } set { m_YellowScales = value; InvalidateProperties(); } }
//
//        [CommandProperty(AccessLevel.GameMaster)]
//        public int BlackScales { get { return m_BlackScales; } set { m_BlackScales = value; InvalidateProperties(); } }
//
//        [CommandProperty(AccessLevel.GameMaster)]
//        public int GreenScales { get { return m_GreenScales; } set { m_GreenScales = value; InvalidateProperties(); } }
//
//        [CommandProperty(AccessLevel.GameMaster)]
//        public int WhiteScales { get { return m_WhiteScales; } set { m_WhiteScales = value; InvalidateProperties(); } }
//
//        [CommandProperty(AccessLevel.GameMaster)]
//        public int BlueScales { get { return m_BlueScales; } set { m_BlueScales = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Bone { get { return m_Bone; } set { m_Bone = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Bandage { get { return m_Bandage; } set { m_Bandage = value; InvalidateProperties(); } }


        public void CollectFromBackpack(Mobile from, bool showMessage)
        {
            if (!from.Alive)
                return;
            if (!this.IsChildOf(from.Backpack) && !this.IsChildOf(from.BankBox))
            {
                from.SendMessage("That tailor's key has to be in your backpack or bankbox for you to use it.");
                return;
            }

            Type[] type = new Type[]{typeof(BaseHides), typeof(BaseLeather), 
                typeof(DarkYarn), typeof(LightYarn), typeof(LightYarnUnraveled),
                typeof(SpoolOfThread), typeof(Cotton), typeof(Wool), typeof(Flax), typeof(Bone)};

            Item[] items = from.Backpack.FindItemsByType(type, true);
            for (int j = 0; j < items.Length; j++)
            {
                int currentAmount = 0;

                Item item = items[j];
                if (!item.Movable)
                    continue;
                int amount = item.Amount;

                if (item is Leather || item is Hides)
                    currentAmount = m_Leather;
                else if (item is SpinedLeather || item is SpinedHides)
                    currentAmount = m_Spined;
                else if (item is HornedLeather || item is HornedHides)
                    currentAmount = m_Horned;
                else if (item is BarbedLeather || item is BarbedHides)
                    currentAmount = m_Barbed;
                else if (item is DarkYarn || item is LightYarn || item is LightYarnUnraveled)
                    currentAmount = m_Yarn;
                else if (item is SpoolOfThread)
                    currentAmount = m_SpoolOfThread;
                else if (item is Wool)
                    currentAmount = m_Wool;
                else if (item is Flax)
                    currentAmount = m_Flax;
                else if (item is Cotton)
                    currentAmount = m_Cotton;
                else if (item is Bone)
                    currentAmount = m_Bone;

                if (amount + currentAmount > m_StorageLimit)
                    continue;
                else
                {
                    if (item is Leather || item is Hides)
                        m_Leather += amount;
                    else if (item is SpinedLeather || item is SpinedHides)
                        m_Spined += amount;
                    else if (item is HornedLeather || item is HornedHides)
                        m_Horned += amount;
                    else if (item is BarbedLeather || item is BarbedHides)
                        m_Barbed += amount;
                    else if (item is DarkYarn || item is LightYarn || item is LightYarnUnraveled)
                        m_Yarn += amount;
                    else if (item is SpoolOfThread)
                        m_SpoolOfThread += amount;
                    else if (item is Wool)
                        m_Wool += amount;
                    else if (item is Flax)
                        m_Flax += amount;
                    else if (item is Cotton)
                        m_Cotton += amount;
                    else if (item is Bone)
                        m_Bone += amount;

                    item.Delete();
                }
            }

            type = new Type[] { typeof(Cloth), typeof(UncutCloth), typeof(BoltOfCloth) };

            items = from.Backpack.FindItemsByType(type, true);
            for (int j = 0; j < items.Length; j++)
            {
                Item item = items[j];

                if (!item.Movable)
                    continue;
                if (item.Hue != 0)
                    continue;

                int amount = item.Amount;
                int currentAmount = 0;

                if (item is Cloth)
                    currentAmount = m_Cloth;
                else if (item is UncutCloth)
                    currentAmount = m_UncutCloth;
                else if (item is BoltOfCloth)
                    currentAmount = m_BoltOfCloth;

                if (amount + currentAmount > m_StorageLimit)
                    continue;
                else
                {
                    if (item is Cloth)
                        m_Cloth += amount;
                    else if (item is UncutCloth)
                        m_UncutCloth += amount;
                    else if (item is BoltOfCloth)
                        m_BoltOfCloth += amount;

                    item.Delete();
                }
            }


            if (showMessage)
            from.SendMessage("Tailor related items are collected from your backpack into that key, subject to storage limit.");
            if (showMessage)
            from.SendMessage("Bandages and hued cloth will NOT be collected for players' own convenience.");
        }
        [Constructable]
        public ResourceStorageKeyTailor()
            : base(0x176B)
        {
            Movable = true;
            Weight = 1.0;
            Hue = 643;
            Name = "Tailor's Keys";
            LootType = LootType.Blessed;
            StorageLimit = 60000;
            WithdrawIncrement = 100;
        }

        [Constructable]
        public ResourceStorageKeyTailor(int storageLimit, int withdrawIncrement)
            : base(0x176B)
        {
            Movable = true;
            Weight = 1.0;
            Hue = 643;
            Name = "Tailor's Keys";
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
                from.SendGump(new ResourceStorageKeyTailorGump((PlayerMobile)from, this));
            else
                from.SendMessage("This must be in your backpack or bankbox to use.");
        }

        public void BeginCombine(Mobile from)
        {
            from.Target = new ResourceStorageKeyTailorTarget(this);
        }

        public void EndCombine(Mobile from, object o)
        {
            if (o is Item && (((Item)o).IsChildOf(from.Backpack) || ((Item)o).IsChildOf(from.BankBox)))
            {
                Item curItem = o as Item;
                if (curItem is Leather)
                {
                    if (Leather + curItem.Amount > StorageLimit)
                        from.SendMessage("You are trying to add " + ((Leather + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                    else
                    {
                        Leather += curItem.Amount;
                        curItem.Delete();
                        from.SendGump(new ResourceStorageKeyTailorGump((PlayerMobile)from, this));
                        BeginCombine(from);
                    }
                }
                else if (curItem is SpinedLeather)
                {
                    if (Spined + curItem.Amount > StorageLimit)
                        from.SendMessage("You are trying to add " + ((Spined + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                    else
                    {
                        Spined += curItem.Amount;
                        curItem.Delete();
                        from.SendGump(new ResourceStorageKeyTailorGump((PlayerMobile)from, this));
                        BeginCombine(from);
                    }
                }
                else if (curItem is HornedLeather)
                {
                    if (Horned + curItem.Amount > StorageLimit)
                        from.SendMessage("You are trying to add " + ((Horned + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                    else
                    {
                        Horned += curItem.Amount;
                        curItem.Delete();
                        from.SendGump(new ResourceStorageKeyTailorGump((PlayerMobile)from, this));
                        BeginCombine(from);
                    }
                }
                else if (curItem is BarbedLeather)
                {
                    if (Barbed + curItem.Amount > StorageLimit)
                        from.SendMessage("You are trying to add " + ((Barbed + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                    else
                    {
                        Barbed += curItem.Amount;
                        curItem.Delete();
                        from.SendGump(new ResourceStorageKeyTailorGump((PlayerMobile)from, this));
                        BeginCombine(from);
                    }
                }
                else if (curItem is Cloth)
                {
                    if (Cloth + curItem.Amount > StorageLimit)
                        from.SendMessage("You are trying to add " + ((Cloth + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                    else
                    {
                        Cloth += curItem.Amount;
                        curItem.Delete();
                        from.SendGump(new ResourceStorageKeyTailorGump((PlayerMobile)from, this));
                        BeginCombine(from);
                    }
                }
                else if (curItem is UncutCloth)
                {
                    if (UncutCloth + curItem.Amount > StorageLimit)
                        from.SendMessage("You are trying to add " + ((UncutCloth + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                    else
                    {
                        UncutCloth += curItem.Amount;
                        curItem.Delete();
                        from.SendGump(new ResourceStorageKeyTailorGump((PlayerMobile)from, this));
                        BeginCombine(from);
                    }
                }
                else if (curItem is BoltOfCloth)
                {
                    if (BoltOfCloth + curItem.Amount > StorageLimit)
                        from.SendMessage("You are trying to add " + ((BoltOfCloth + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                    else
                    {
                        BoltOfCloth += curItem.Amount;
                        curItem.Delete();
                        from.SendGump(new ResourceStorageKeyTailorGump((PlayerMobile)from, this));
                        BeginCombine(from);
                    }
                }
                else if (curItem is SpoolOfThread)
                {
                    if (SpoolOfThread + curItem.Amount > StorageLimit)
                        from.SendMessage("You are trying to add " + ((SpoolOfThread + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                    else
                    {
                        SpoolOfThread += curItem.Amount;
                        curItem.Delete();
                        from.SendGump(new ResourceStorageKeyTailorGump((PlayerMobile)from, this));
                        BeginCombine(from);
                    }
                }
                else if (curItem is DarkYarn || curItem is LightYarn || curItem is LightYarnUnraveled)
                {
                    if (Yarn + curItem.Amount > StorageLimit)
                        from.SendMessage("You are trying to add " + ((Yarn + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                    else
                    {
                        Yarn += curItem.Amount;
                        curItem.Delete();
                        from.SendGump(new ResourceStorageKeyTailorGump((PlayerMobile)from, this));
                        BeginCombine(from);
                    }
                }
                else if (curItem is Cotton)
                {
                    if (Cotton + curItem.Amount > StorageLimit)
                        from.SendMessage("You are trying to add " + ((Cotton + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                    else
                    {
                        Cotton += curItem.Amount;
                        curItem.Delete();
                        from.SendGump(new ResourceStorageKeyTailorGump((PlayerMobile)from, this));
                        BeginCombine(from);
                    }
                }
                else if (curItem is Wool)
                {
                    if (Wool + curItem.Amount > StorageLimit)
                        from.SendMessage("You are trying to add " + ((Wool + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                    else
                    {
                        Wool += curItem.Amount;
                        curItem.Delete();
                        from.SendGump(new ResourceStorageKeyTailorGump((PlayerMobile)from, this));
                        BeginCombine(from);
                    }
                }
                else if (curItem is Flax)
                {
                    if (Flax + curItem.Amount > StorageLimit)
                        from.SendMessage("You are trying to add " + ((Flax + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                    else
                    {
                        Flax += curItem.Amount;
                        curItem.Delete();
                        from.SendGump(new ResourceStorageKeyTailorGump((PlayerMobile)from, this));
                        BeginCombine(from);
                    }
                }
                else if (curItem is Bandage)
                {
                    if (Bandage + curItem.Amount > StorageLimit)
                        from.SendMessage("You are trying to add " + ((Bandage + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                    else
                    {
                        Bandage += curItem.Amount;
                        curItem.Delete();
                        from.SendGump(new ResourceStorageKeyTailorGump((PlayerMobile)from, this));
                        BeginCombine(from);
                    }
                }
//                else if (curItem is RedScales)
//                {
//                    from.SendMessage("The tailor's key is not accepting scales anymore, please use the metal worker's key instead.");
//                    /*
//                    if (RedScales + curItem.Amount > StorageLimit)
//                        from.SendMessage("You are trying to add " + ((RedScales + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
//                    else
//                    {
//                        RedScales += curItem.Amount;
//                        curItem.Delete();
//                        from.SendGump(new ResourceStorageKeyTailorGump((PlayerMobile)from, this));
//                        BeginCombine(from);
//                    }
//                     */ 
//                }
//                else if (curItem is YellowScales)
//                {
//                    from.SendMessage("The tailor's key is not accepting scales anymore, please use the metal worker's key instead.");
//                    /*
//                    if (YellowScales + curItem.Amount > StorageLimit)
//                        from.SendMessage("You are trying to add " + ((YellowScales + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
//                    else
//                    {
//                        YellowScales += curItem.Amount;
//                        curItem.Delete();
//                        from.SendGump(new ResourceStorageKeyTailorGump((PlayerMobile)from, this));
//                        BeginCombine(from);
//                    }
//                     */ 
//                }
//                else if (curItem is BlackScales)
//                {
//                    from.SendMessage("The tailor's key is not accepting scales anymore, please use the metal worker's key instead.");
//                    /*
//                    if (BlackScales + curItem.Amount > StorageLimit)
//                        from.SendMessage("You are trying to add " + ((BlackScales + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
//                    else
//                    {
//                        BlackScales += curItem.Amount;
//                        curItem.Delete();
//                        from.SendGump(new ResourceStorageKeyTailorGump((PlayerMobile)from, this));
//                        BeginCombine(from);
//                    }
//                     */ 
//                }
//                else if (curItem is GreenScales)
//                {
//                    from.SendMessage("The tailor's key is not accepting scales anymore, please use the metal worker's key instead.");
//                    /*
//                    if (GreenScales + curItem.Amount > StorageLimit)
//                        from.SendMessage("You are trying to add " + ((GreenScales + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
//                    else
//                    {
//                        GreenScales += curItem.Amount;
//                        curItem.Delete();
//                        from.SendGump(new ResourceStorageKeyTailorGump((PlayerMobile)from, this));
//                        BeginCombine(from);
//                    }
//                     */ 
//                }
//                else if (curItem is WhiteScales)
//                {
//                    from.SendMessage("The tailor's key is not accepting scales anymore, please use the metal worker's key instead.");
//                    /*
//                    if (WhiteScales + curItem.Amount > StorageLimit)
//                        from.SendMessage("You are trying to add " + ((WhiteScales + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
//                    else
//                    {
//                        WhiteScales += curItem.Amount;
//                        curItem.Delete();
//                        from.SendGump(new ResourceStorageKeyTailorGump((PlayerMobile)from, this));
//                        BeginCombine(from);
//                    }
//                     */ 
//                }
//                else if (curItem is BlueScales)
//                {
//                    from.SendMessage("The tailor's key is not accepting scales anymore, please use the metal worker's key instead.");
//                    /*
//                    if (BlueScales + curItem.Amount > StorageLimit)
//                        from.SendMessage("You are trying to add " + ((BlueScales + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
//                    else
//                    {
//                        BlueScales += curItem.Amount;
//                        curItem.Delete();
//                        from.SendGump(new ResourceStorageKeyTailorGump((PlayerMobile)from, this));
//                        BeginCombine(from);
//                    }
//                     */ 
//                }
                else if (curItem is Bone)
                {
                    if (Bone + curItem.Amount > StorageLimit)
                        from.SendMessage("You are trying to add " + ((Bone + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                    else
                    {
                        Bone += curItem.Amount;
                        curItem.Delete();
                        from.SendGump(new ResourceStorageKeyTailorGump((PlayerMobile)from, this));
                        BeginCombine(from);
                    }
                }
            }
            else
                from.SendLocalizedMessage(1045158); // You must have the item in your backpack to target it.
        }

        public ResourceStorageKeyTailor(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)3); // version
            writer.Write(m_Bandage);
            writer.Write(m_Bone);
            writer.Write(m_Flax);
            writer.Write(m_SpoolOfThread);
            writer.Write(m_Cotton);
            writer.Write(m_Leather);
            writer.Write(m_Spined);
            writer.Write(m_Horned);
            writer.Write(m_Barbed);
            writer.Write(m_Cloth);
            writer.Write(m_UncutCloth);
            writer.Write(m_BoltOfCloth);
            writer.Write(m_Yarn);
            writer.Write(m_Wool);
//            writer.Write(m_RedScales);
//            writer.Write(m_YellowScales);
//            writer.Write(m_BlackScales);
//            writer.Write(m_GreenScales);
//            writer.Write(m_WhiteScales);
//            writer.Write(m_BlueScales);
            writer.Write(m_StorageLimit);
            writer.Write(m_WithdrawIncrement);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            switch (version)
            {
                case 3:
                    {
                        m_Bandage = reader.ReadInt();
                        goto case 2;
                    }
                case 2:
                    {
                        m_Bone = reader.ReadInt();
                        goto case 1;
                    }
                case 1:
                    {
                        m_Flax = reader.ReadInt();
                        goto case 0;
                    }
                case 0:
                    {
                        m_SpoolOfThread = reader.ReadInt();
                        m_Cotton = reader.ReadInt();
                        m_Leather = reader.ReadInt();
                        m_Spined = reader.ReadInt();
                        m_Horned = reader.ReadInt();
                        m_Barbed = reader.ReadInt();
                        m_Cloth = reader.ReadInt();
                        m_UncutCloth = reader.ReadInt();
                        m_BoltOfCloth = reader.ReadInt();
                        m_Yarn = reader.ReadInt();
                        m_Wool = reader.ReadInt();
//                        m_RedScales = reader.ReadInt();
//                        m_YellowScales = reader.ReadInt();
//                        m_BlackScales = reader.ReadInt();
//                        m_GreenScales = reader.ReadInt();
//                        m_WhiteScales = reader.ReadInt();
//                        m_BlueScales = reader.ReadInt();
                        m_StorageLimit = reader.ReadInt();
                        m_WithdrawIncrement = reader.ReadInt();
                        break;
                    }
            }

            LootType = LootType.Blessed;
        }
    }
}

namespace Server.Items
{
    public class ResourceStorageKeyTailorGump : Gump
    {
        private PlayerMobile m_From;
        private ResourceStorageKeyTailor m_Key;

        public ResourceStorageKeyTailorGump(PlayerMobile from, ResourceStorageKeyTailor key)
            : base(25, 25)
        {
            m_From = from;
            m_Key = key;

            m_From.CloseGump(typeof(ResourceStorageKeyTailorGump));

            AddPage(0);

            AddBackground(50, 10, 455, 450, 5054);
            AddImageTiled(60, 20, 435, 430, 2624);
            AddAlphaRegion(60, 20, 435, 430);

            AddLabel(110, 25, 88, "Tailor's Warehouse (Caution: color cloth will lose hue)");
            AddLabel(125, 50, 0x486, "Withdraw Increment: ");
	    AddLabel(275, 50, 0x480, key.WithdrawIncrement.ToString());
	    AddButton(330, 50, 4011, 4012, 32, GumpButtonType.Reply, 0); 
	    AddButton(360, 50, 4011, 4012, 33, GumpButtonType.Reply, 0);
	    AddButton(390, 50, 4011, 4012, 34, GumpButtonType.Reply, 0);

            AddLabel(125, 75, 0x486, "Leather");
            AddLabel(225, 75, 0x480, key.Leather.ToString());
            AddButton(75, 75, 4005, 4007, (m_Key.Leather <= 0) ? 999 : 1, GumpButtonType.Reply, 0);

            AddLabel(125, 100, 0x486, "Spined Leather");
            AddLabel(225, 100, 0x480, key.Spined.ToString());
            AddButton(75, 100, 4005, 4007, (m_Key.Spined <= 0) ? 999 : 2, GumpButtonType.Reply, 0);

            AddLabel(125, 125, 0x486, "Horned Leather");
            AddLabel(225, 125, 0x480, key.Horned.ToString());
            AddButton(75, 125, 4005, 4007, (m_Key.Horned <= 0) ? 999 : 3, GumpButtonType.Reply, 0);

            AddLabel(125, 150, 0x486, "Barbed Leather");
            AddLabel(225, 150, 0x480, key.Barbed.ToString());
            AddButton(75, 150, 4005, 4007, (m_Key.Barbed <= 0) ? 999 : 4, GumpButtonType.Reply, 0);

//            AddLabel(325, 75, 0x486, "Red Scales");
//            AddLabel(425, 75, 0x480, key.RedScales.ToString());
//            AddButton(275, 75, 4005, 4007, (m_Key.RedScales <= 0) ? 999 : 12, GumpButtonType.Reply, 0);
//
//            AddLabel(325, 100, 0x486, "Yellow Scales");
//            AddLabel(425, 100, 0x480, key.YellowScales.ToString());
//            AddButton(275, 100, 4005, 4007, (m_Key.YellowScales <= 0) ? 999 : 13, GumpButtonType.Reply, 0);
//
//            AddLabel(325, 125, 0x486, "Black Scales");
//            AddLabel(425, 125, 0x480, key.BlackScales.ToString());
//            AddButton(275, 125, 4005, 4007, (m_Key.BlackScales <= 0) ? 999 : 14, GumpButtonType.Reply, 0);
//
//            AddLabel(325, 150, 0x486, "Green Scales");
//            AddLabel(425, 150, 0x480, key.GreenScales.ToString());
//            AddButton(275, 150, 4005, 4007, (m_Key.GreenScales <= 0) ? 999 : 15, GumpButtonType.Reply, 0);
//
//            AddLabel(325, 175, 0x486, "White Scales");
//            AddLabel(425, 175, 0x480, key.WhiteScales.ToString());
//            AddButton(275, 175, 4005, 4007, (m_Key.WhiteScales <= 0) ? 999 : 16, GumpButtonType.Reply, 0);
//
//            AddLabel(325, 200, 0x486, "Blue Scales");
//            AddLabel(425, 200, 0x480, key.BlueScales.ToString());
//            AddButton(275, 200, 4005, 4007, (m_Key.BlueScales <= 0) ? 999 : 17, GumpButtonType.Reply, 0);

            AddLabel(125, 325, 0x486, "Bandage");
            AddLabel(225, 325, 0x480, key.Bandage.ToString());
            AddButton(75, 325, 4005, 4007, (m_Key.Bandage <= 0) ? 999 : 30, GumpButtonType.Reply, 0);

            AddLabel(125, 350, 0x486, "Cloth");
            AddLabel(225, 350, 0x480, key.Cloth.ToString());
            AddButton(75, 350, 4005, 4007, (m_Key.Cloth <= 0) ? 999 : 21, GumpButtonType.Reply, 0);

            AddLabel(125, 375, 0x486, "Uncut Cloth");
            AddLabel(225, 375, 0x480, key.UncutCloth.ToString());
            AddButton(75, 375, 4005, 4007, (m_Key.UncutCloth <= 0) ? 999 : 22, GumpButtonType.Reply, 0);

            AddLabel(125, 400, 0x486, "Bolt of Cloth");
            AddLabel(225, 400, 0x480, key.BoltOfCloth.ToString());
            AddButton(75, 400, 4005, 4007, (m_Key.BoltOfCloth <= 0) ? 999 : 23, GumpButtonType.Reply, 0);

            AddLabel(325, 250, 0x486, "Bone");
            AddLabel(425, 250, 0x480, key.Bone.ToString());
            AddButton(275, 250, 4005, 4007, (m_Key.Bone <= 0) ? 999 : 29, GumpButtonType.Reply, 0);

            AddLabel(325, 275, 0x486, "Flax");
            AddLabel(425, 275, 0x480, key.Flax.ToString());
            AddButton(275, 275, 4005, 4007, (m_Key.Flax <= 0) ? 999 : 28, GumpButtonType.Reply, 0);

            AddLabel(325, 300, 0x486, "Yarn");
            AddLabel(425, 300, 0x480, key.Yarn.ToString());
            AddButton(275, 300, 4005, 4007, (m_Key.Yarn <= 0) ? 999 : 24, GumpButtonType.Reply, 0);

            AddLabel(325, 325, 0x486, "Wool");
            AddLabel(425, 325, 0x480, key.Wool.ToString());
            AddButton(275, 325, 4005, 4007, (m_Key.Wool <= 0) ? 999 : 25, GumpButtonType.Reply, 0);

            AddLabel(325, 350, 0x486, "Cotton");
            AddLabel(425, 350, 0x480, key.Cotton.ToString());
            AddButton(275, 350, 4005, 4007, (m_Key.Cotton <= 0) ? 999 : 26, GumpButtonType.Reply, 0);

            AddLabel(125, 425, 0x486, "Spool of Thread");
            AddLabel(225, 425, 0x480, key.SpoolOfThread.ToString());
            AddButton(75, 425, 4005, 4007, (m_Key.SpoolOfThread <= 0) ? 999 : 27, GumpButtonType.Reply, 0);

            AddLabel(325, 375, 88, "Each Max:");
            AddLabel(425, 375, 0x480, key.StorageLimit.ToString());

            AddLabel(325, 400, 88, "Add resource");
            AddButton(275, 400, 4005, 4007, 999, GumpButtonType.Reply, 0);

            AddLabel(325, 425, 88, "Collect from backpack");
            AddButton(275, 425, 4005, 4007, 31, GumpButtonType.Reply, 0);
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            if (m_Key.Deleted)
                return;
            else if (!m_Key.IsChildOf(m_From.Backpack) && !m_Key.IsChildOf(m_From.BankBox))
            {
                m_From.SendMessage("This must be in your backpack or bankbox to use.");
                return;
            }
	    else if (info.ButtonID == 32) 
	    {
		m_Key.WithdrawIncrement = 100;
		m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
	    }
	    else if (info.ButtonID == 33)
            {
		m_Key.WithdrawIncrement = 500;
		m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
	    }
	    else if (info.ButtonID == 34)
	    {
		m_Key.WithdrawIncrement = 1000;
		m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
	    }
            else if (info.ButtonID == 1)
            {
                if (m_Key.Leather >= m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new Leather(m_Key.WithdrawIncrement));
                    m_Key.Leather = m_Key.Leather - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
                }
                else
                {
                    m_From.AddToBackpack(new Leather(m_Key.Leather));
                    m_Key.Leather = 0;
                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
                }
            }
            else if (info.ButtonID == 2)
            {
                if (m_Key.Spined >= m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new SpinedLeather(m_Key.WithdrawIncrement));
                    m_Key.Spined = m_Key.Spined - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
                }
                else
                {
                    m_From.AddToBackpack(new SpinedLeather(m_Key.Spined));
                    m_Key.Spined = 0;
                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
                }
            }
            else if (info.ButtonID == 3)
            {
                if (m_Key.Horned >= m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new HornedLeather(m_Key.WithdrawIncrement));
                    m_Key.Horned = m_Key.Horned - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
                }
                else
                {
                    m_From.AddToBackpack(new HornedLeather(m_Key.Horned));
                    m_Key.Horned = 0;
                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
                }
            }
            else if (info.ButtonID == 4)
            {
                if (m_Key.Barbed >= m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new BarbedLeather(m_Key.WithdrawIncrement));
                    m_Key.Barbed = m_Key.Barbed - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
                }
                else
                {
                    m_From.AddToBackpack(new BarbedLeather(m_Key.Barbed));
                    m_Key.Barbed = 0;
                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
                }
            }
//            else if (info.ButtonID == 12)
//            {
//                if (m_Key.RedScales >= m_Key.WithdrawIncrement)
//                {
//                    m_From.AddToBackpack(new RedScales(m_Key.WithdrawIncrement));
//                    m_Key.RedScales = m_Key.RedScales - m_Key.WithdrawIncrement;
//                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
//                }
//                else
//                {
//                    m_From.AddToBackpack(new RedScales(m_Key.RedScales));
//                    m_Key.RedScales = 0;
//                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
//                }
//            }
//            else if (info.ButtonID == 13)
//            {
//                if (m_Key.YellowScales >= m_Key.WithdrawIncrement)
//                {
//                    m_From.AddToBackpack(new YellowScales(m_Key.WithdrawIncrement));
//                    m_Key.YellowScales = m_Key.YellowScales - m_Key.WithdrawIncrement;
//                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
//                }
//                else
//                {
//                    m_From.AddToBackpack(new YellowScales(m_Key.YellowScales));
//                    m_Key.YellowScales = 0;
//                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
//                }
//            }
//            else if (info.ButtonID == 14)
//            {
//                if (m_Key.BlackScales >= m_Key.WithdrawIncrement)
//                {
//                    m_From.AddToBackpack(new BlackScales(m_Key.WithdrawIncrement));
//                    m_Key.BlackScales = m_Key.BlackScales - m_Key.WithdrawIncrement;
//                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
//                }
//                else
//                {
//                    m_From.AddToBackpack(new BlackScales(m_Key.BlackScales));
//                    m_Key.BlackScales = 0;
//                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
//                }
//            }
//            else if (info.ButtonID == 15)
//            {
//                if (m_Key.GreenScales >= m_Key.WithdrawIncrement)
//                {
//                    m_From.AddToBackpack(new GreenScales(m_Key.WithdrawIncrement));
//                    m_Key.GreenScales = m_Key.GreenScales - m_Key.WithdrawIncrement;
//                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
//                }
//                else
//                {
//                    m_From.AddToBackpack(new GreenScales(m_Key.GreenScales));
//                    m_Key.GreenScales = 0;
//                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
//                }
//            }
//            else if (info.ButtonID == 16)
//            {
//                if (m_Key.WhiteScales >= m_Key.WithdrawIncrement)
//                {
//                    m_From.AddToBackpack(new WhiteScales(m_Key.WithdrawIncrement));
//                    m_Key.WhiteScales = m_Key.WhiteScales - m_Key.WithdrawIncrement;
//                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
//                }
//                else
//                {
//                    m_From.AddToBackpack(new WhiteScales(m_Key.WhiteScales));
//                    m_Key.WhiteScales = 0;
//                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
//                }
//            }
//            else if (info.ButtonID == 17)
//            {
//                if (m_Key.BlueScales >= m_Key.WithdrawIncrement)
//                {
//                    m_From.AddToBackpack(new BlueScales(m_Key.WithdrawIncrement));
//                    m_Key.BlueScales = m_Key.BlueScales - m_Key.WithdrawIncrement;
//                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
//                }
//                else
//                {
//                    m_From.AddToBackpack(new BlueScales(m_Key.BlueScales));
//                    m_Key.BlueScales = 0;
//                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
//                }
//            }
            else if (info.ButtonID == 21)
            {
                if (m_Key.Cloth >= m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new Cloth(m_Key.WithdrawIncrement));
                    m_Key.Cloth = m_Key.Cloth - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
                }
                else
                {
                    m_From.AddToBackpack(new Cloth(m_Key.Cloth));
                    m_Key.Cloth = 0;
                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
                }
            }
            else if (info.ButtonID == 22)
            {
                if (m_Key.UncutCloth >= m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new UncutCloth(m_Key.WithdrawIncrement));
                    m_Key.UncutCloth = m_Key.UncutCloth - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
                }
                else
                {
                    m_From.AddToBackpack(new UncutCloth(m_Key.UncutCloth));
                    m_Key.UncutCloth = 0;
                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
                }
            }
            else if (info.ButtonID == 23)
            {
                if (m_Key.BoltOfCloth >= m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new BoltOfCloth(m_Key.WithdrawIncrement));
                    m_Key.BoltOfCloth = m_Key.BoltOfCloth - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
                }
                else
                {
                    m_From.AddToBackpack(new BoltOfCloth(m_Key.BoltOfCloth));
                    m_Key.BoltOfCloth = 0;
                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
                }
            }
            else if (info.ButtonID == 24)
            {
                if (m_Key.Yarn >= m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new DarkYarn(m_Key.WithdrawIncrement));
                    m_Key.Yarn = m_Key.Yarn - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
                }
                else
                {
                    m_From.AddToBackpack(new DarkYarn(m_Key.Yarn));
                    m_Key.Yarn = 0;
                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
                }
            }
            else if (info.ButtonID == 25)
            {
                if (m_Key.Wool >= m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new Wool(m_Key.WithdrawIncrement));
                    m_Key.Wool = m_Key.Wool - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
                }
                else
                {
                    m_From.AddToBackpack(new Wool(m_Key.Wool));
                    m_Key.Wool = 0;
                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
                }
            }
            if (info.ButtonID == 26)
            {
                if (m_Key.Cotton >= m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new Cotton(m_Key.WithdrawIncrement));
                    m_Key.Cotton = m_Key.Cotton - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
                }
                else
                {
                    m_From.AddToBackpack(new Cotton(m_Key.Cotton));
                    m_Key.Cotton = 0;
                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
                }
            }
            if (info.ButtonID == 27)
            {
                if (m_Key.SpoolOfThread >= m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new SpoolOfThread(m_Key.WithdrawIncrement));
                    m_Key.SpoolOfThread = m_Key.SpoolOfThread - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
                }
                else
                {
                    m_From.AddToBackpack(new SpoolOfThread(m_Key.SpoolOfThread));
                    m_Key.SpoolOfThread = 0;
                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
                }
            }
            else if (info.ButtonID == 28)
            {
                if (m_Key.Flax >= m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new Flax(m_Key.WithdrawIncrement));
                    m_Key.Flax = m_Key.Flax - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
                }
                else
                {
                    m_From.AddToBackpack(new Flax(m_Key.Flax));
                    m_Key.Flax = 0;
                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
                }
            }
            else if (info.ButtonID == 29)
            {
                if (m_Key.Bone >= m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new Bone(m_Key.WithdrawIncrement));
                    m_Key.Bone = m_Key.Bone - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
                }
                else
                {
                    m_From.AddToBackpack(new Bone(m_Key.Bone));
                    m_Key.Bone = 0;
                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
                }
            }
            else if (info.ButtonID == 30)
            {
                if (m_Key.Bandage >= m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new Bandage(m_Key.WithdrawIncrement));
                    m_Key.Bandage = m_Key.Bandage - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
                }
                else
                {
                    m_From.AddToBackpack(new Bandage(m_Key.Bandage));
                    m_Key.Bandage = 0;
                    m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
                }
            }
            else if (info.ButtonID == 31)
            {
                m_Key.CollectFromBackpack(m_From, true);
                m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
	    
            }
            else if (info.ButtonID == 999)
            {
                m_From.SendMessage("Please select items to add to the tailors key.");
                m_From.SendGump(new ResourceStorageKeyTailorGump(m_From, m_Key));
                m_Key.BeginCombine(m_From);
            }
        }
    }
}

namespace Server.Items
{
    public class ResourceStorageKeyTailorTarget : Target
    {
        private ResourceStorageKeyTailor m_Key;

        public ResourceStorageKeyTailorTarget(ResourceStorageKeyTailor key)
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
