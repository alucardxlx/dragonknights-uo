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
Modified by Tylius.Also Modified by Morpheus and Dave
Modified by daat99 to fit a custom ore\wood pack.
Modified by daat99 to fit custom craftable pack.
MOdified by daat99 to accept both boards and logs and give back boards.
Modified by Kazuha to allow the user to select how much to withdraw at a time.  
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
    public class ResourceStorageKeyWood : Item
    {

//        private int m_WillowBoard;
//        private int m_CedarBoard;
//        private int m_CypressBoard;
//        private int m_WalnutBoard;
//        private int m_SequoiaBoard;
    	

    	private int m_AshBoards;
    	private int m_BloodwoodBoards;
    	private int m_FrostwoodBoards;
    	private int m_HeartwoodBoards;
        private int m_LogBoards;
        private int m_OakBoards;
        private int m_YewBoards;

        private int m_Arrow;
        private int m_Bolt;
        private int m_Feather;
        private int m_Shaft;

        private int m_StorageLimit;
        private int m_WithdrawIncrement;
        
//        [CommandProperty(AccessLevel.GameMaster)]
//        public int WillowBoard { get { return m_WillowBoard; } set { m_WillowBoard = value; InvalidateProperties(); } }
//
//        [CommandProperty(AccessLevel.GameMaster)]
//        public int CedarBoard { get { return m_CedarBoard; } set { m_CedarBoard = value; InvalidateProperties(); } }
//
//        [CommandProperty(AccessLevel.GameMaster)]
//        public int CypressBoard { get { return m_CypressBoard; } set { m_CypressBoard = value; InvalidateProperties(); } }
//
//        [CommandProperty(AccessLevel.GameMaster)]
//        public int WalnutBoard { get { return m_WalnutBoard; } set { m_WalnutBoard = value; InvalidateProperties(); } }
//
//        [CommandProperty(AccessLevel.GameMaster)]
//        public int SequoiaBoard { get { return m_SequoiaBoard; } set { m_SequoiaBoard = value; InvalidateProperties(); } }
        


        [CommandProperty(AccessLevel.GameMaster)]
        public int AshBoards { get { return m_AshBoards; } set { m_AshBoards = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int BloodwoodBoards { get { return m_BloodwoodBoards; } set { m_BloodwoodBoards = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int FrostwoodBoards { get { return m_FrostwoodBoards; } set { m_FrostwoodBoards = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int HeartwoodBoards { get { return m_HeartwoodBoards; } set { m_HeartwoodBoards = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int LogBoards { get { return m_LogBoards; } set { m_LogBoards = value; InvalidateProperties(); } }
        
        [CommandProperty(AccessLevel.GameMaster)]
        public int OakBoards { get { return m_OakBoards; } set { m_OakBoards = value; InvalidateProperties(); } }
        
        [CommandProperty(AccessLevel.GameMaster)]
        public int YewBoards { get { return m_YewBoards; } set { m_YewBoards = value; InvalidateProperties(); } }
        
        
        
        [CommandProperty(AccessLevel.GameMaster)]
        public int Arrow { get { return m_Arrow; } set { m_Arrow = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Bolt { get { return m_Bolt; } set { m_Bolt = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Feather { get { return m_Feather; } set { m_Feather = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Shaft { get { return m_Shaft; } set { m_Shaft = value; InvalidateProperties(); } }

        
        [CommandProperty(AccessLevel.GameMaster)]
        public int StorageLimit { get { return m_StorageLimit; } set { m_StorageLimit = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int WithdrawIncrement { get { return m_WithdrawIncrement; } set { m_WithdrawIncrement = value; InvalidateProperties(); } }
        
        
        
        

        public void CollectFromBackpack(Mobile from, bool showMessage)
        {
            if (!from.Alive)
                return;
            if (!this.IsChildOf(from.Backpack) && !this.IsChildOf(from.BankBox))
            {
                from.SendMessage("That wood worker's key has to be in your backpack or bankbox for you to use it.");
                return;
            }

            Type[] type = new Type[]{typeof(Board), typeof(Log)};
            Item[] items = from.Backpack.FindItemsByType(type, true);
            for (int j = 0; j < items.Length; j++)
            {
                int currentAmount = 0;

                Item item = items[j];
                if (!item.Movable)
                    continue;
                int amount = item.Amount;

//                if (item is Board || item is Log)
//                    currentAmount = m_Board;
//                else if (item is WillowBoard || item is WillowLog)
//                    currentAmount = m_WillowBoard;
//                else if (item is CedarBoard || item is CedarLog)
//                    currentAmount = m_CedarBoard;
//                else if (item is CypressBoard || item is CypressLog)
//                    currentAmount = m_CypressBoard;
//                else if (item is WalnutBoard || item is WalnutLog)
//                    currentAmount = m_WalnutBoard;
//                else if (item is SequoiaBoard || item is SequoiaLog)
//                    currentAmount = m_SequoiaBoard;




                if (item is AshBoard || item is AshLog)
                	currentAmount = m_AshBoards;
                else if (item is BloodwoodBoard || item is BloodwoodLog)
                	currentAmount = m_BloodwoodBoards;
                else if (item is FrostwoodBoard || item is FrostwoodLog)
                    currentAmount = m_FrostwoodBoards;
                else if (item is HeartwoodBoard || item is HeartwoodLog)
                    currentAmount = m_HeartwoodBoards;
                else if (item is OakBoard || item is OakLog)
                	currentAmount = m_OakBoards;
                else if (item is YewBoard || item is YewLog)
                	currentAmount = m_YewBoards;
                else if (item is Log || item is Board)
                	currentAmount = m_LogBoards;

                if (amount + currentAmount > m_StorageLimit)
                    continue;
                else
                {
//                	if (item is Board || item is Log)
//                        m_Board += amount;
//                    else if (item is WillowBoard || item is WillowLog)
//                        m_WillowBoard += amount;
//                    else if (item is CedarBoard || item is CedarLog)
//                        m_CedarBoard += amount;
//                    else if (item is CypressBoard || item is CypressLog)
//                        m_CypressBoard += amount;
//                    else if (item is WalnutBoard || item is WalnutLog)
//                        m_WalnutBoard += amount;
//                    else if (item is SequoiaBoard || item is SequoiaLog)
//                        m_SequoiaBoard += amount;
//                    if (item is OakBoard || item is OakLog)
//                        m_OakBoard += amount;
                    if (item is AshBoard || item is AshLog)
                        m_AshBoards += amount;
                    else if (item is BloodwoodBoard || item is BloodwoodLog)
                        m_BloodwoodBoards += amount;
                    else if (item is FrostwoodBoard || item is FrostwoodLog)
                        m_FrostwoodBoards += amount;
                    else if (item is HeartwoodBoard || item is HeartwoodLog)
                        m_HeartwoodBoards += amount;
                    else if (item is OakBoard || item is OakLog)
                        m_OakBoards += amount;
                    else if (item is YewBoard || item is YewLog)
                        m_YewBoards += amount;
                    else if (item is Log || item is Board)
                    	m_LogBoards += amount;

                    item.Delete();
                    
                    
                    
                    
                    
                    
                    
                }
            }

            type = new Type[]{typeof(Feather), typeof(Shaft)};

            items = from.Backpack.FindItemsByType(type, true);
            for (int j = 0; j < items.Length; j++)
            {
                int currentAmount = 0;

                Item item = items[j];
                if (!item.Movable)
                    continue;
                int amount = item.Amount;

                if (item is Feather)
                    currentAmount = m_Feather;
                else if (item is Shaft)
                    currentAmount = m_Shaft;

                if (amount + currentAmount > m_StorageLimit)
                    continue;
                else
                {
                    if (item is Feather)
                        m_Feather += amount;
                    else if (item is Shaft)
                        m_Shaft += amount;

                    item.Delete();
                }
            }

            if (showMessage)
            from.SendMessage("Boards and logs and the rest are collected from your backpack into that key, subject to storage limit.");
            if (showMessage)
            from.SendMessage("Arrows and bolts will NOT be collected for players' own convenience.");
        }

        [Constructable]
        public ResourceStorageKeyWood()
            : base(0x176B)
        {
            Movable = true;
            Weight = 1.0;
            Hue = 50;
            Name = "Wood Worker's Keys";
            LootType = LootType.Blessed;
            StorageLimit = 60000;
            WithdrawIncrement = 100;
        }

        [Constructable]
        public ResourceStorageKeyWood(int storageLimit, int withdrawIncrement)
            : base(0x176B)
        {
            Movable = true;
            Weight = 1.0;
            Hue = 50;
            Name = "Wood Worker's Keys";
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
                from.SendGump(new ResourceStorageKeyWoodGump((PlayerMobile)from, this));
            else
                from.SendMessage("This must be in your backpack or bankbox to use.");
        }
        public void BeginCombine(Mobile from)
        {
            from.Target = new ResourceStorageKeyWoodTarget(this);
        }
        public void EndCombine(Mobile from, object o)
        {
            if (o is Item && (((Item)o).IsChildOf(from.Backpack) || ((Item)o).IsChildOf(from.BankBox)))
            {
                Item curItem = o as Item;
                if (curItem is AshBoard || curItem is AshLog)
                {
                    if (AshBoards + curItem.Amount > StorageLimit)
                        from.SendMessage("You are trying to add " + ((AshBoards + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                    else
                    {
                        AshBoards += curItem.Amount;
                        curItem.Delete();
                        from.SendGump(new ResourceStorageKeyWoodGump((PlayerMobile)from, this));
                        BeginCombine(from);
                    }
                }
                else if (curItem is BloodwoodBoard || curItem is BloodwoodLog)
                {
                    if (BloodwoodBoards + curItem.Amount > StorageLimit)
                        from.SendMessage("You are trying to add " + ((BloodwoodBoards + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                    else
                    {
                        BloodwoodBoards += curItem.Amount;
                        curItem.Delete();
                        from.SendGump(new ResourceStorageKeyWoodGump((PlayerMobile)from, this));
                        BeginCombine(from);
                    }
                }
                else if (curItem is FrostwoodBoard || curItem is FrostwoodLog)
                {
                    if (FrostwoodBoards + curItem.Amount > StorageLimit)
                        from.SendMessage("You are trying to add " + ((FrostwoodBoards + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                    else
                    {
                        FrostwoodBoards += curItem.Amount;
                        curItem.Delete();
                        from.SendGump(new ResourceStorageKeyWoodGump((PlayerMobile)from, this));
                        BeginCombine(from);
                    }
                }
                else if (curItem is HeartwoodBoard || curItem is HeartwoodLog)
                {
                    if (HeartwoodBoards + curItem.Amount > StorageLimit)
                        from.SendMessage("You are trying to add " + ((HeartwoodBoards + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                    else
                    {
                        HeartwoodBoards += curItem.Amount;
                        curItem.Delete();
                        from.SendGump(new ResourceStorageKeyWoodGump((PlayerMobile)from, this));
                        BeginCombine(from);
                    }
                }
                else if (curItem is OakBoard || curItem is OakLog)
                {
                    if (OakBoards + curItem.Amount > StorageLimit)
                        from.SendMessage("You are trying to add " + ((OakBoards + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                    else
                    {
                        OakBoards += curItem.Amount;
                        curItem.Delete();
                        from.SendGump(new ResourceStorageKeyWoodGump((PlayerMobile)from, this));
                        BeginCombine(from);
                    }
                }
                else if (curItem is YewBoard || curItem is YewLog)
                {
                    if (YewBoards + curItem.Amount > StorageLimit)
                        from.SendMessage("You are trying to add " + ((YewBoards + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                    else
                    {
                        YewBoards += curItem.Amount;
                        curItem.Delete();
                        from.SendGump(new ResourceStorageKeyWoodGump((PlayerMobile)from, this));
                        BeginCombine(from);
                    }
                }
                else if (curItem is Board || curItem is Log)
                {
                    if (LogBoards + curItem.Amount > StorageLimit)
                        from.SendMessage("You are trying to add " + ((LogBoards + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                    else
                    {
                        LogBoards += curItem.Amount;
                        curItem.Delete();
                        from.SendGump(new ResourceStorageKeyWoodGump((PlayerMobile)from, this));
                        BeginCombine(from);
                    }
                }
                else if (curItem is Arrow)
                {
                    if (Arrow + curItem.Amount > StorageLimit)
                        from.SendMessage("You are trying to add " + ((Arrow + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                    else
                    {
                        Arrow += curItem.Amount;
                        curItem.Delete();
                        from.SendGump(new ResourceStorageKeyWoodGump((PlayerMobile)from, this));
                        BeginCombine(from);
                    }
                }
                else if (curItem is Bolt)
                {
                    if (Bolt + curItem.Amount > StorageLimit)
                        from.SendMessage("You are trying to add " + ((Bolt + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                    else
                    {
                        Bolt += curItem.Amount;
                        curItem.Delete();
                        from.SendGump(new ResourceStorageKeyWoodGump((PlayerMobile)from, this));
                        BeginCombine(from);
                    }
                }
                else if (curItem is Feather)
                {
                    if (Feather + curItem.Amount > StorageLimit)
                        from.SendMessage("You are trying to add " + ((Feather + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                    else
                    {
                        Feather += curItem.Amount;
                        curItem.Delete();
                        from.SendGump(new ResourceStorageKeyWoodGump((PlayerMobile)from, this));
                        BeginCombine(from);
                    }
                }
                else if (curItem is Shaft)
                {
                    if (Shaft + curItem.Amount > StorageLimit)
                        from.SendMessage("You are trying to add " + ((Shaft + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                    else
                    {
                        Shaft += curItem.Amount;
                        curItem.Delete();
                        from.SendGump(new ResourceStorageKeyWoodGump((PlayerMobile)from, this));
                        BeginCombine(from);
                        
//                else if (curItem is WillowBoard)
//                {
//                    if (WillowBoard + curItem.Amount > StorageLimit)
//                        from.SendMessage("You are trying to add " + ((WillowBoard + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
//                    else
//                    {
//                        WillowBoard += curItem.Amount;
//                        curItem.Delete();
//                        from.SendGump(new ResourceStorageKeyWoodGump((PlayerMobile)from, this));
//                        BeginCombine(from);
//                    }
//                }
//                else if (curItem is CedarBoard)
//                {
//                    if (CedarBoard + curItem.Amount > StorageLimit)
//                        from.SendMessage("You are trying to add " + ((CedarBoard + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
//                    else
//                    {
//                        CedarBoard += curItem.Amount;
//                        curItem.Delete();
//                        from.SendGump(new ResourceStorageKeyWoodGump((PlayerMobile)from, this));
//                        BeginCombine(from);
//                    }
//                }
//                else if (curItem is CypressBoard)
//                {
//                    if (CypressBoard + curItem.Amount > StorageLimit)
//                        from.SendMessage("You are trying to add " + ((CypressBoard + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
//                    else
//                    {
//                        CypressBoard += curItem.Amount;
//                        curItem.Delete();
//                        from.SendGump(new ResourceStorageKeyWoodGump((PlayerMobile)from, this));
//                        BeginCombine(from);
//                    }
//                }
//                else if (curItem is WalnutBoard)
//                {
//                    if (WalnutBoard + curItem.Amount > StorageLimit)
//                        from.SendMessage("You are trying to add " + ((WalnutBoard + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
//                    else
//                    {
//                        WalnutBoard += curItem.Amount;
//                        curItem.Delete();
//                        from.SendGump(new ResourceStorageKeyWoodGump((PlayerMobile)from, this));
//                        BeginCombine(from);
//                    }
//                }
//                else if (curItem is SequoiaBoard)
//                {
//                    if (SequoiaBoard + curItem.Amount > StorageLimit)
//                        from.SendMessage("You are trying to add " + ((SequoiaBoard + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
//                    else
//                    {
//                        SequoiaBoard += curItem.Amount;
//                        curItem.Delete();
//                        from.SendGump(new ResourceStorageKeyWoodGump((PlayerMobile)from, this));
//                        BeginCombine(from);
//                    }
//                }
//                else if (curItem is WillowLog)
//                {
//                    if (WillowBoard + curItem.Amount > StorageLimit)
//                        from.SendMessage("You are trying to add " + ((WillowBoard + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
//                    else
//                    {
//                        WillowBoard += curItem.Amount;
//                        curItem.Delete();
//                        from.SendGump(new ResourceStorageKeyWoodGump((PlayerMobile)from, this));
//                        BeginCombine(from);
//                    }
//                }
//                else if (curItem is CedarLog)
//                {
//                    if (CedarBoard + curItem.Amount > StorageLimit)
//                        from.SendMessage("You are trying to add " + ((CedarBoard + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
//                    else
//                    {
//                        CedarBoard += curItem.Amount;
//                        curItem.Delete();
//                        from.SendGump(new ResourceStorageKeyWoodGump((PlayerMobile)from, this));
//                        BeginCombine(from);
//                    }
//                }
//                else if (curItem is CypressLog)
//                {
//                    if (CypressBoard + curItem.Amount > StorageLimit)
//                        from.SendMessage("You are trying to add " + ((CypressBoard + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
//                    else
//                    {
//                        CypressBoard += curItem.Amount;
//                        curItem.Delete();
//                        from.SendGump(new ResourceStorageKeyWoodGump((PlayerMobile)from, this));
//                        BeginCombine(from);
//                    }
//                }
//                else if (curItem is WalnutLog)
//                {
//                    if (WalnutBoard + curItem.Amount > StorageLimit)
//                        from.SendMessage("You are trying to add " + ((WalnutBoard + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
//                    else
//                    {
//                        WalnutBoard += curItem.Amount;
//                        curItem.Delete();
//                        from.SendGump(new ResourceStorageKeyWoodGump((PlayerMobile)from, this));
//                        BeginCombine(from);
//                    }
//                }
//                else if (curItem is SequoiaLog)
//                {
//                    if (SequoiaBoard + curItem.Amount > StorageLimit)
//                        from.SendMessage("You are trying to add " + ((SequoiaBoard + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
//                    else
//                    {
//                        SequoiaBoard += curItem.Amount;
//                        curItem.Delete();
//                        from.SendGump(new ResourceStorageKeyWoodGump((PlayerMobile)from, this));
//                        BeginCombine(from);
//                    }
//                }
                        
                    }
                }
            }
            else
            {
                from.SendLocalizedMessage(1045158); // You must have the item in your backpack to target it.
            }
        }
        public ResourceStorageKeyWood(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version

//            writer.Write((int)m_WillowBoard);
//            writer.Write((int)m_CedarBoard);
//            writer.Write((int)m_CypressBoard);
//            writer.Write((int)m_WalnutBoard);
//            writer.Write((int)m_SequoiaBoard);
            
            
            
            writer.Write((int)m_AshBoards);
            writer.Write((int)m_BloodwoodBoards);
            writer.Write((int)m_FrostwoodBoards);
            writer.Write((int)m_HeartwoodBoards);
            writer.Write((int)m_LogBoards);
            writer.Write((int)m_OakBoards);
            writer.Write((int)m_YewBoards);

            writer.Write((int)m_Arrow);
            writer.Write((int)m_Bolt);
            writer.Write((int)m_Feather);
            writer.Write((int)m_Shaft);

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
                    goto case 0;
                case 0:
                    {
//                        m_WillowBoard = reader.ReadInt();
//                        m_CedarBoard = reader.ReadInt();
//                        m_CypressBoard = reader.ReadInt();
//                        m_WalnutBoard = reader.ReadInt();
//                        m_SequoiaBoard = reader.ReadInt();
                        m_AshBoards = reader.ReadInt();
                        m_BloodwoodBoards = reader.ReadInt();
                        m_FrostwoodBoards = reader.ReadInt();
                        m_HeartwoodBoards = reader.ReadInt();
                        m_LogBoards = reader.ReadInt();
                        m_OakBoards = reader.ReadInt();
                        m_YewBoards = reader.ReadInt();                        
                        
                        m_Arrow = reader.ReadInt();
                        m_Bolt = reader.ReadInt();
                        m_Feather = reader.ReadInt();
                        m_Shaft = reader.ReadInt();
                        
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
    public class ResourceStorageKeyWoodGump : Gump
    {
        private PlayerMobile m_From;
        private ResourceStorageKeyWood m_Key;

        public ResourceStorageKeyWoodGump(PlayerMobile from, ResourceStorageKeyWood key)
            : base(25, 25)
        {
            m_From = from;
            m_Key = key;

            m_From.CloseGump(typeof(ResourceStorageKeyWoodGump));

            AddPage(0);

            AddBackground(50, 10, 455, 305, 5054);
            AddImageTiled(58, 20, 438, 291, 2624);
            AddAlphaRegion(58, 20, 438, 291);

            AddLabel(200, 25, 88, "Wood Warehouse");
//Kazuha
	    AddLabel(125, 50, 0x486, "Withdraw Increment");
	    AddLabel(275, 50, 0x480, key.WithdrawIncrement.ToString());
	    AddButton(330, 50, 4011, 4012, 20, GumpButtonType.Reply, 0);
            AddButton(360, 50, 4011, 4012, 21, GumpButtonType.Reply, 0);
            AddButton(390, 50, 4011, 4012, 22, GumpButtonType.Reply, 0);
//Kazuha


            AddLabel(125, 75, 0x486, "Ash Wood");
            AddLabel(225, 75, 0x480, key.AshBoards.ToString());
            AddButton(75, 75, 4005, 4007, 1, GumpButtonType.Reply, 0);


            AddLabel(125, 100, 0x486, "Bloodwood");
            AddLabel(225, 100, 0x480, key.BloodwoodBoards.ToString());
            AddButton(75, 100, 4005, 4007, 2, GumpButtonType.Reply, 0);


            AddLabel(125, 125, 0x486, "Frostwood");
            AddLabel(225, 125, 0x480, key.FrostwoodBoards.ToString());
            AddButton(75, 125, 4005, 4007, 3, GumpButtonType.Reply, 0);


            AddLabel(125, 150, 0x486, "Heartwood");
            AddLabel(225, 150, 0x480, key.HeartwoodBoards.ToString());
            AddButton(75, 150, 4005, 4007, 4, GumpButtonType.Reply, 0);

            AddLabel(125, 175, 0x486, "Logs / Boards");
            AddLabel(225, 175, 0x480, key.LogBoards.ToString());
            AddButton(75, 175, 4005, 4007, 5, GumpButtonType.Reply, 0);
            
            
            AddLabel(125, 200, 0x486, "Oak wood");
            AddLabel(225, 200, 0x480, key.OakBoards.ToString());
            AddButton(75, 200, 4005, 4007, 6, GumpButtonType.Reply, 0);
            
            
            AddLabel(125, 225, 0x486, "Yew Wood");
            AddLabel(225, 225, 0x480, key.YewBoards.ToString());
            AddButton(75, 225, 4005, 4007, 7, GumpButtonType.Reply, 0);
          
            
            

//            AddLabel(125, 75, 0x486, "Board");
//            AddLabel(225, 75, 0x480, key.LogBoards.ToString());
//            AddButton(75, 75, 4005, 4007, 1, GumpButtonType.Reply, 0);

//            AddLabel(125, 100, 0x486, "Willow");
//            AddLabel(225, 100, 0x480, key.WillowBoard.ToString());
//            AddButton(75, 100, 4005, 4007, 2, GumpButtonType.Reply, 0);

//            AddLabel(125, 125, 0x486, "Cedar");
//            AddLabel(225, 125, 0x480, key.CedarBoard.ToString());
//            AddButton(75, 125, 4005, 4007, 3, GumpButtonType.Reply, 0);

//            AddLabel(125, 150, 0x486, "Cypress");
//            AddLabel(225, 150, 0x480, key.CypressBoard.ToString());
//            AddButton(75, 150, 4005, 4007, 4, GumpButtonType.Reply, 0);

//            AddLabel(125, 225, 0x486, "Yew");
//            AddLabel(225, 225, 0x480, key.YewBoards.ToString());
//            AddButton(75, 225, 4005, 4007, 5, GumpButtonType.Reply, 0);

//			AddLabel(125, 250, 0x486, "Oak");
//            AddLabel(225, 250, 0x480, key.OakBoards.ToString());
//            AddButton(75, 250, 4005, 4007, 6, GumpButtonType.Reply, 0);

//            AddLabel(125, 175, 0x486, "Walnut");
//            AddLabel(225, 175, 0x480, key.WalnutBoard.ToString());
//            AddButton(75, 175, 4005, 4007, 7, GumpButtonType.Reply, 0);

//            AddLabel(125, 200, 0x486, "Sequoia");
//            AddLabel(225, 200, 0x480, key.SequoiaBoard.ToString());
//            AddButton(75, 200, 4005, 4007, 8, GumpButtonType.Reply, 0);



//            AddLabel(125, 275, 0x486, "Heartwood");
//            AddLabel(225, 275, 0x480, key.HeartwoodBoards.ToString());
//            AddButton(75, 275, 4005, 4007, 9, GumpButtonType.Reply, 0);

            AddLabel(325, 125, 0x486, "Arrow");
            AddLabel(425, 125, 0x480, key.Arrow.ToString());
            AddButton(275, 125, 4005, 4007, 13, GumpButtonType.Reply, 0);

            AddLabel(325, 150, 0x486, "Bolt");
            AddLabel(425, 150, 0x480, key.Bolt.ToString());
            AddButton(275, 150, 4005, 4007, 14, GumpButtonType.Reply, 0);

            AddLabel(325, 175, 0x486, "Feather");
            AddLabel(425, 175, 0x480, key.Feather.ToString());
            AddButton(275, 175, 4005, 4007, 15, GumpButtonType.Reply, 0);

            AddLabel(325, 200, 0x486, "Shaft");
            AddLabel(425, 200, 0x480, key.Shaft.ToString());
            AddButton(275, 200, 4005, 4007, 16, GumpButtonType.Reply, 0);

            AddLabel(325, 225, 88, "Each Max:");
            AddLabel(425, 225, 0x480, key.StorageLimit.ToString());

            AddLabel(325, 250, 88, "Add resource");
            AddButton(275, 250, 4005, 4007, 17, GumpButtonType.Reply, 0);

            AddLabel(325, 275, 88, "Collect Board From Backpack");
            AddButton(275, 275, 4005, 4007, 18, GumpButtonType.Reply, 0);
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
	    else if (info.ButtonID == 20) 
	    {
		m_Key.WithdrawIncrement = 100;
		m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
	    }
            else if (info.ButtonID == 21)
            {
                m_Key.WithdrawIncrement = 500;
                m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
            }
            else if (info.ButtonID == 22)
            {
                m_Key.WithdrawIncrement = 1000;
                m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
            }
            else if (info.ButtonID == 1)
            {
                if (m_Key.AshBoards >= m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new AshBoard(m_Key.WithdrawIncrement));
                    m_Key.AshBoards = m_Key.AshBoards - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
                }
                else if (m_Key.AshBoards > 0)
                {
                    m_From.AddToBackpack(new AshBoard(m_Key.AshBoards));
                    m_Key.AshBoards = 0;
                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that board!");
                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 2)
            {
                if (m_Key.BloodwoodBoards >= m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new BloodwoodBoard(m_Key.WithdrawIncrement));
                    m_Key.BloodwoodBoards = m_Key.BloodwoodBoards - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
                }
                else if (m_Key.BloodwoodBoards > 0)
                {
                    m_From.AddToBackpack(new BloodwoodBoard(m_Key.BloodwoodBoards));
                    m_Key.BloodwoodBoards = 0;
                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that board!");
                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 3)
            {
                if (m_Key.FrostwoodBoards >= m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new FrostwoodBoard(m_Key.WithdrawIncrement));
                    m_Key.FrostwoodBoards = m_Key.FrostwoodBoards - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
                }
                else if (m_Key.FrostwoodBoards > 0)
                {
                    m_From.AddToBackpack(new FrostwoodBoard(m_Key.FrostwoodBoards));
                    m_Key.FrostwoodBoards = 0;
                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that board!");
                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 4)
            {
                if (m_Key.HeartwoodBoards >= m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new HeartwoodBoard(m_Key.WithdrawIncrement));
                    m_Key.HeartwoodBoards = m_Key.HeartwoodBoards - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
                }
                else if (m_Key.HeartwoodBoards > 0)
                {
                    m_From.AddToBackpack(new HeartwoodBoard(m_Key.HeartwoodBoards));
                    m_Key.HeartwoodBoards = 0;
                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that board!");
                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 5)
            {
                if (m_Key.LogBoards >= m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new Board(m_Key.WithdrawIncrement));
                    m_Key.LogBoards = m_Key.LogBoards - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
                }
                else if (m_Key.LogBoards > 0)
                {
                    m_From.AddToBackpack(new Board(m_Key.LogBoards));
                    m_Key.LogBoards = 0;
                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that board!");
                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 6)
            {
                if (m_Key.OakBoards >= m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new OakBoard(m_Key.WithdrawIncrement));
                    m_Key.OakBoards = m_Key.OakBoards - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
                }
                else if (m_Key.OakBoards > 0)
                {
                    m_From.AddToBackpack(new OakBoard(m_Key.OakBoards));
                    m_Key.OakBoards = 0;
                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that board!");
                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 7)
            {
                if (m_Key.YewBoards >= m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new YewBoard(m_Key.WithdrawIncrement));
                    m_Key.YewBoards = m_Key.YewBoards - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
                }
                else if (m_Key.YewBoards > 0)
                {
                    m_From.AddToBackpack(new YewBoard(m_Key.YewBoards));
                    m_Key.YewBoards = 0;
                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that board!");
                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
           
            
            
            
            
            
            
            
            
            
            
            
            
            
            
//            else if (info.ButtonID == 2)
//            {
//                if (m_Key.WillowBoard >= m_Key.WithdrawIncrement)
//                {
//                    m_From.AddToBackpack(new WillowBoard(m_Key.WithdrawIncrement));
//                    m_Key.WillowBoard = m_Key.WillowBoard - m_Key.WithdrawIncrement;
//                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
//                }
//                else if (m_Key.WillowBoard > 0)
//                {
//                    m_From.AddToBackpack(new WillowBoard(m_Key.WillowBoard));
//                    m_Key.WillowBoard = 0;
//                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
//                }
//                else
//                {
//                    m_From.SendMessage("You do not have any of that board!");
//                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
//                    m_Key.BeginCombine(m_From);
//                }
//            }
//            else if (info.ButtonID == 3)
//            {
//                if (m_Key.CedarBoard >= m_Key.WithdrawIncrement)
//                {
//                    m_From.AddToBackpack(new CedarBoard(m_Key.WithdrawIncrement));
//                    m_Key.CedarBoard = m_Key.CedarBoard - m_Key.WithdrawIncrement;
//                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
//                }
//                else if (m_Key.CedarBoard > 0)
//                {
//                    m_From.AddToBackpack(new CedarBoard(m_Key.CedarBoard));
//                    m_Key.CedarBoard = 0;
//                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
//                }
//                else
//                {
//                    m_From.SendMessage("You do not have any of that board!");
//                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
//                    m_Key.BeginCombine(m_From);
//                }
//            }
//            else if (info.ButtonID == 4)
//            {
//                if (m_Key.CypressBoard >= m_Key.WithdrawIncrement)
//                {
//                    m_From.AddToBackpack(new CypressBoard(m_Key.WithdrawIncrement));
//                    m_Key.CypressBoard = m_Key.CypressBoard - m_Key.WithdrawIncrement;
//                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
//                }
//                else if (m_Key.CypressBoard > 0)
//                {
//                    m_From.AddToBackpack(new CypressBoard(m_Key.CypressBoard));
//                    m_Key.CypressBoard = 0;
//                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
//                }
//                else
//                {
//                    m_From.SendMessage("You do not have any of that board!");
//                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
//                    m_Key.BeginCombine(m_From);
//                }
//            }
//            else if (info.ButtonID == 5)
//            {
//                if (m_Key.YewBoards >= m_Key.WithdrawIncrement)
//                {
//                    m_From.AddToBackpack(new YewBoard(m_Key.WithdrawIncrement));
//                    m_Key.YewBoards = m_Key.YewBoards - m_Key.WithdrawIncrement;
//                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
//                }
//                else if (m_Key.YewBoards > 0)
//                {
//                    m_From.AddToBackpack(new YewBoard(m_Key.YewBoards));
//                    m_Key.YewBoards = 0;
//                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
//                }
//                else
//                {
//                    m_From.SendMessage("You do not have any of that board!");
//                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
//                    m_Key.BeginCombine(m_From);
//                }
//            }
//            else if (info.ButtonID == 6)
//            {
//                if (m_Key.OakBoards >= m_Key.WithdrawIncrement)
//                {
//                    m_From.AddToBackpack(new OakBoard(m_Key.WithdrawIncrement));
//                    m_Key.OakBoards = m_Key.OakBoards - m_Key.WithdrawIncrement;
//                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
//                }
//                else if (m_Key.OakBoards > 0)
//                {
//                    m_From.AddToBackpack(new OakBoard(m_Key.OakBoards));
//                    m_Key.OakBoards = 0;
//                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
//                }
//                else
//                {
//                    m_From.SendMessage("You do not have any of that board!");
//                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
//                    m_Key.BeginCombine(m_From);
//                }
//            }
//            else if (info.ButtonID == 7)
//            {
//                if (m_Key.WalnutBoard >= m_Key.WithdrawIncrement)
//                {
//                    m_From.AddToBackpack(new WalnutBoard(m_Key.WithdrawIncrement));
//                    m_Key.WalnutBoard = m_Key.WalnutBoard - m_Key.WithdrawIncrement;
//                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
//                }
//                else if (m_Key.WalnutBoard > 0)
//                {
//                    m_From.AddToBackpack(new WalnutBoard(m_Key.WalnutBoard));
//                    m_Key.WalnutBoard = 0;
//                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
//                }
//                else
//                {
//                    m_From.SendMessage("You do not have any of that board!");
//                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
//                    m_Key.BeginCombine(m_From);
//                }
//            }
//            else if (info.ButtonID == 8)
//            {
//                if (m_Key.SequoiaBoard >= m_Key.WithdrawIncrement)
//                {
//                    m_From.AddToBackpack(new SequoiaBoard(m_Key.WithdrawIncrement));
//                    m_Key.SequoiaBoard = m_Key.SequoiaBoard - m_Key.WithdrawIncrement;
//                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
//                }
//                else if (m_Key.SequoiaBoard > 0)
//                {
//                    m_From.AddToBackpack(new SequoiaBoard(m_Key.SequoiaBoard));
//                    m_Key.SequoiaBoard = 0;
//                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
//                }
//                else
//                {
//                    m_From.SendMessage("You do not have any of that board!");
//                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
//                    m_Key.BeginCombine(m_From);
//                }
//            }
            else if (info.ButtonID == 9)
            {
                if (m_Key.HeartwoodBoards >= m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new HeartwoodBoard(m_Key.WithdrawIncrement));
                    m_Key.HeartwoodBoards = m_Key.HeartwoodBoards - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
                }
                else if (m_Key.HeartwoodBoards > 0)
                {
                    m_From.AddToBackpack(new HeartwoodBoard(m_Key.HeartwoodBoards));
                    m_Key.HeartwoodBoards = 0;
                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that board!");
                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 13)
            {
                if (m_Key.Arrow >= m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new Arrow(m_Key.WithdrawIncrement));
                    m_Key.Arrow = m_Key.Arrow - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
                }
                else if (m_Key.Arrow > 0)
                {
                    m_From.AddToBackpack(new Arrow(m_Key.Arrow));
                    m_Key.Arrow = 0;
                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any arrows stored!");
                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 14)
            {
                if (m_Key.Bolt > m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new Bolt(m_Key.WithdrawIncrement));
                    m_Key.Bolt = m_Key.Bolt - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
                }
                else if (m_Key.Bolt > 0)
                {
                    m_From.AddToBackpack(new Bolt(m_Key.Bolt));
                    m_Key.Bolt = 0;
                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any bolts stored!");
                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 15)
            {
                if (m_Key.Feather > m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new Feather(m_Key.WithdrawIncrement));
                    m_Key.Feather = m_Key.Feather - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
                }
                else if (m_Key.Feather > 0)
                {
                    m_From.AddToBackpack(new Feather(m_Key.Feather));
                    m_Key.Feather = 0;
                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any feathers stored!");
                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 16)
            {
                if (m_Key.Shaft > m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new Shaft(m_Key.WithdrawIncrement));
                    m_Key.Shaft = m_Key.Shaft - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
                }
                else if (m_Key.Shaft > 0)
                {
                    m_From.AddToBackpack(new Shaft(m_Key.Shaft));
                    m_Key.Shaft = 0;
                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any shafts stored!");
                    m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 17)
            {
                m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
                m_Key.BeginCombine(m_From);
            }
            else if (info.ButtonID == 18)
            {
                m_Key.CollectFromBackpack(m_From, true);
                m_From.SendGump(new ResourceStorageKeyWoodGump(m_From, m_Key));
            }
        }
    }
}

namespace Server.Items
{
    public class ResourceStorageKeyWoodTarget : Target
    {
        private ResourceStorageKeyWood m_Key;

        public ResourceStorageKeyWoodTarget(ResourceStorageKeyWood key)
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
