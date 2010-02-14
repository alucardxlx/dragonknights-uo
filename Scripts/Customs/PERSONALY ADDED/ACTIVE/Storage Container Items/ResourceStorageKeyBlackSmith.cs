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

Sources:

Ingot Key script by GoldDrac13
Granite Box script by (unknown)
BankCrystal script by (unknown)
////////////////////////////////
////////////////////////////////////////
Modified by Ashlar, beloved of Morrigan.  
Modified by Tylius.
Modified gump and added custom ingots by daat99.
Modified a lof of the code by daat99.
//////////////////////////////////////////
This item is a resource storage key as well as a forge (backpack or ground), an anvil (ground only), and a banker (spoken). 
Add or remove references to fit your shard.  
Note however, that adding such items that have a number of uses, like shovels, will allow the player to put a almost caput 
shovel in and pop it back out with 50 uses left.
*/
using System;					//To be honest, I am not sure if all this is needed, but the script works!
using System.Collections;
using Server;
using Server.Prompts;
using Server.Mobiles;
using Server.ContextMenus;
using Server.Gumps;
using Server.Items;
using Server.Misc;
using Server.Network;
using Server.Targeting;
using Server.Multis;
using Server.Regions;


namespace Server.Items
{
    [FlipableAttribute(0xFEF, 0xFF0, 0xFF1, 0xFF2, 0xFF3, 0xFF4, 0xFBD, 0xFBE)]

    public class ResourceStorageKeyBlackSmith : Item
    {
        private int m_Iron;		//Declare all our resources as integer (number) variables.
        private int m_Sand;//added
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

        private int m_RedScales;
        private int m_YellowScales;
        private int m_BlackScales;
        private int m_GreenScales;
        private int m_WhiteScales;
        private int m_BlueScales;

        private int m_StorageLimit;
        private int m_WithdrawIncrement;

        //This section allows GM's and above to change the amounts of the various properties of the key.
        [CommandProperty(AccessLevel.GameMaster)]
        public int StorageLimit { get { return m_StorageLimit; } set { m_StorageLimit = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int WithdrawIncrement { get { return m_WithdrawIncrement; } set { m_WithdrawIncrement = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Iron { get { return m_Iron; } set { m_Iron = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]//added
        public int Sand { get { return m_Sand; } set { m_Sand = value; InvalidateProperties(); } }//added

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


        [CommandProperty(AccessLevel.GameMaster)]
        public int RedScales { get { return m_RedScales; } set { m_RedScales = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int YellowScales { get { return m_YellowScales; } set { m_YellowScales = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int BlackScales { get { return m_BlackScales; } set { m_BlackScales = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int GreenScales { get { return m_GreenScales; } set { m_GreenScales = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int WhiteScales { get { return m_WhiteScales; } set { m_WhiteScales = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int BlueScales { get { return m_BlueScales; } set { m_BlueScales = value; InvalidateProperties(); } }


        public void CollectFromBackpack(Mobile from, bool showMessage)
        {
            if (!from.Alive)
                return;
            if (!this.IsChildOf(from.Backpack) && !this.IsChildOf(from.BankBox))
            {
                from.SendMessage("That metal worker's key has to be in your backpack or bankbox for you to use it.");
                return;
            }
            

            Type type = typeof(BaseIngot);
            Item[] items = from.Backpack.FindItemsByType(type, true);
            for (int j = 0; j < items.Length; j++)
            {
                int currentAmount = 0;

                Item item = items[j];
                if (!item.Movable)
                    continue;
                int amount = item.Amount;

                if (item is IronIngot)
                    currentAmount = m_Iron;
                else if (item is DullCopperIngot)
                    currentAmount = m_DullCopper;
                else if (item is ShadowIronIngot)
                    currentAmount = m_ShadowIron;
                else if (item is CopperIngot)
                    currentAmount = m_Copper;
                else if (item is BronzeIngot)
                    currentAmount = m_Bronze;
                else if (item is GoldIngot)
                    currentAmount = m_Gold;
                else if (item is AgapiteIngot)
                    currentAmount = m_Agapite;
                else if (item is VeriteIngot)
                    currentAmount = m_Verite;
                else if (item is ValoriteIngot)
                    currentAmount = m_Valorite;
//                else if (item is FluoriteIngot)
//                    currentAmount = m_Fluorite;
//                else if (item is PlatinumIngot)
//                    currentAmount = m_Platinum;

                if (amount + currentAmount > m_StorageLimit)
                    continue;
                else
                {
                    if (item is IronIngot)
                        m_Iron += amount;
                    else if (item is DullCopperIngot)
                        m_DullCopper += amount;
                    else if (item is ShadowIronIngot)
                        m_ShadowIron += amount;
                    else if (item is CopperIngot)
                        m_Copper += amount;
                    else if (item is BronzeIngot)
                        m_Bronze += amount;
                    else if (item is GoldIngot)
                        m_Gold += amount;
                    else if (item is AgapiteIngot)
                        m_Agapite += amount;
                    else if (item is VeriteIngot)
                        m_Verite += amount;
                    else if (item is ValoriteIngot)
                        m_Valorite += amount;
//                    else if (item is FluoriteIngot)
//                        m_Fluorite += amount;
//                    else if (item is PlatinumIngot)
//                        m_Platinum += amount;

                    item.Delete();
                }
            }

            type = typeof(BaseScales);
            items = from.Backpack.FindItemsByType(type, true);
            for (int j = 0; j < items.Length; j++)
            {
                int currentAmount = 0;

                Item item = items[j];
                if (!item.Movable)
                    continue;
                int amount = item.Amount;

                if (item is RedScales)
                    currentAmount = m_RedScales;
                else if (item is BlueScales)
                    currentAmount = m_BlueScales;
                else if (item is YellowScales)
                    currentAmount = m_YellowScales;
                else if (item is BlackScales)
                    currentAmount = m_BlackScales;
                else if (item is GreenScales)
                    currentAmount = m_GreenScales;
                else if (item is WhiteScales)
                    currentAmount = m_WhiteScales;

                if (amount + currentAmount > m_StorageLimit)
                    continue;
                else
                {
                    if (item is RedScales)
                        m_RedScales += amount;
                    else if (item is BlueScales)
                        m_BlueScales += amount;
                    else if (item is YellowScales)
                        m_YellowScales += amount;
                    else if (item is BlackScales)
                        m_BlackScales += amount;
                    else if (item is GreenScales)
                        m_GreenScales += amount;
                    else if (item is WhiteScales)
                        m_WhiteScales += amount;

                    item.Delete();
                }
            }
            
            if (showMessage)
                from.SendMessage("Ingots and Scales are collected from your backpack into that key, subject to storage limit.");
        }

        //This is the default item you get when you [add ResourceStorageKeyBlackSmith
        [Constructable]
        public ResourceStorageKeyBlackSmith()
            : base(0x176B)
        {
            Movable = true;
            Weight = 1.0;
            Hue = 893;
            Name = "BlackSmith Worker's Keys";
            LootType = LootType.Blessed;
            StorageLimit = 60000;
            WithdrawIncrement = 100;
        }

        [Constructable]
        public ResourceStorageKeyBlackSmith(int storageLimit, int withdrawIncrement)
            : base(0x176B)
        {
            Movable = true;
            Weight = 1.0;
            Hue = 893;
            Name = "BlackSmith Worker's Keys";
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
                from.SendGump(new ResourceStorageKeyBlackSmithGump((PlayerMobile)from, this));
            else
                from.SendMessage("This must be in your backpack or bankbox to use.");
        }
        public void BeginCombine(Mobile from)
        {
            from.Target = new ResourceStorageKeyBlackSmithTarget(this);
        }
        public void EndCombine(Mobile from, object o)
        {
            if (o is Item && (((Item)o).IsChildOf(from.Backpack) || ((Item)o).IsChildOf(from.BankBox)))
            {
                Item curItem = o as Item;
                if (curItem is BaseIngot)
                {
                    if (curItem is IronIngot)
                    {
                        if (Iron + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((Iron + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            Iron += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBlackSmithGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is DullCopperIngot)
                    {
                        if (DullCopper + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((DullCopper + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            DullCopper += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBlackSmithGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is ShadowIronIngot)
                    {
                        if (ShadowIron + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((ShadowIron + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            ShadowIron += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBlackSmithGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is CopperIngot)
                    {
                        if (Copper + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((Copper + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            Copper += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBlackSmithGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is BronzeIngot)
                    {

                        if (Bronze + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((Bronze + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            Bronze += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBlackSmithGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is GoldIngot)
                    {
                        if (Gold + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((Gold + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            Gold += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBlackSmithGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is AgapiteIngot)
                    {
                        if (Agapite + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((Agapite + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            Agapite += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBlackSmithGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is VeriteIngot)
                    {

                        if (Verite + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((Verite + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            Verite += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBlackSmithGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is ValoriteIngot)
                    {
                        if (Valorite + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((Valorite + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            Valorite += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBlackSmithGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
//                    else if (curItem is FluoriteIngot)
//                    {
//                        if (Fluorite + curItem.Amount > StorageLimit)
//                            from.SendMessage("You are trying to add " + ((Fluorite + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
//                        else
//                        {
//                            Fluorite += curItem.Amount;
//                            curItem.Delete();
//                            from.SendGump(new ResourceStorageKeyBlackSmithGump((PlayerMobile)from, this));
//                            BeginCombine(from);
//                        }
//                    }
//                    else if (curItem is PlatinumIngot)
//                    {
//                        if (Platinum + curItem.Amount > StorageLimit)
//                            from.SendMessage("You are trying to add " + ((Platinum + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
//                        else
//                        {
//                            Platinum += curItem.Amount;
//                            curItem.Delete();
//                            from.SendGump(new ResourceStorageKeyBlackSmithGump((PlayerMobile)from, this));
//                            BeginCombine(from);
//                        }
//                    }
                }
                else if (curItem is BaseScales)
                {
                    if (curItem is BlueScales)
                    {
                        if (BlueScales + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((BlueScales + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            BlueScales += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBlackSmithGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is RedScales)
                    {
                        if (RedScales + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((RedScales + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            RedScales += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBlackSmithGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is YellowScales)
                    {
                        if (YellowScales + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((YellowScales + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            YellowScales += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBlackSmithGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is WhiteScales)
                    {
                        if (WhiteScales + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((WhiteScales + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            WhiteScales += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBlackSmithGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is BlackScales)
                    {
                        if (BlackScales + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((BlackScales + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            BlackScales += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBlackSmithGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is GreenScales)
                    {
                        if (GreenScales + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((GreenScales + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            GreenScales += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBlackSmithGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                }
                else if (curItem is Sand)
                {
                	if (Sand + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((Sand + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                	else
                	{
                		Sand += curItem.Amount;
                		curItem.Delete();
                		from.SendGump(new ResourceStorageKeyBlackSmithGump((PlayerMobile)from, this));
                		BeginCombine(from);
                	}
                }
            }
            else
            {
                from.SendLocalizedMessage(1045158); // You must have the item in your backpack to target it.
            }
        }
        public ResourceStorageKeyBlackSmith(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)2); // version

            writer.Write((int)m_BlackScales);
            writer.Write((int)m_BlueScales);
            writer.Write((int)m_YellowScales);
            writer.Write((int)m_RedScales);
            writer.Write((int)m_GreenScales);
            writer.Write((int)m_WhiteScales);

//            writer.Write((int)m_Fluorite);
//            writer.Write((int)m_Platinum);

            writer.Write((int)m_Iron);
            writer.Write((int)m_Sand);
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
                case 2:
                    {
                        m_BlackScales = reader.ReadInt();
                        m_BlueScales = reader.ReadInt();
                        m_YellowScales = reader.ReadInt();
                        m_RedScales = reader.ReadInt();
                        m_GreenScales = reader.ReadInt();
                        m_WhiteScales = reader.ReadInt();
                        goto case 1;
                    }
                case 1:
                    {

//                        m_Fluorite = reader.ReadInt();
//                        m_Platinum = reader.ReadInt();
                        goto case 0;
                    }
                case 0:
                    {
                        m_Iron = reader.ReadInt();
            			m_Sand = reader.ReadInt();
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
}

namespace Server.Items
{
    public class ResourceStorageKeyBlackSmithGump : Gump
    {
        private PlayerMobile m_From;
        private ResourceStorageKeyBlackSmith m_Key;

        public ResourceStorageKeyBlackSmithGump(PlayerMobile from, ResourceStorageKeyBlackSmith key)
            : base(25, 25)
        {
            m_From = from;
            m_Key = key;

            m_From.CloseGump(typeof(ResourceStorageKeyBlackSmithGump));

            AddPage(0);

            AddBackground(50, 10, 455, 260 + 100, 5054);
            AddImageTiled(58, 20, 438, 241 + 100, 2624);
            AddAlphaRegion(58, 20, 438, 241 + 100);

            AddLabel(200, 25, 88, "BlackSmith Warehouse");
// Kazuha 
	    AddLabel(125, 50, 0x486, "Withdraw Increment:");
	    AddLabel(275, 50, 0x480, key.WithdrawIncrement.ToString());
	    AddButton(330, 50, 4011, 4012, 26, GumpButtonType.Reply, 0);
	    AddButton(360, 50, 4011, 4012, 27, GumpButtonType.Reply, 0);
	    AddButton(390, 50, 4011, 4012, 28, GumpButtonType.Reply, 0);
// /Kazuha

            AddLabel(125, 75, 0x486, "Iron");
            AddLabel(225, 75, 0x480, key.Iron.ToString());
            AddButton(75, 75, 4005, 4007, 1, GumpButtonType.Reply, 0);

            AddLabel(125, 100, 0x486, "Dull Copper");
            AddLabel(225, 100, 0x480, key.DullCopper.ToString());
            AddButton(75, 100, 4005, 4007, 2, GumpButtonType.Reply, 0);

            AddLabel(125, 125, 0x486, "Shadow Iron");
            AddLabel(225, 125, 0x480, key.ShadowIron.ToString());
            AddButton(75, 125, 4005, 4007, 3, GumpButtonType.Reply, 0);

            AddLabel(125, 150, 0x486, "Copper");
            AddLabel(225, 150, 0x480, key.Copper.ToString());
            AddButton(75, 150, 4005, 4007, 4, GumpButtonType.Reply, 0);

            AddLabel(125, 175, 0x486, "Bronze");
            AddLabel(225, 175, 0x480, key.Bronze.ToString());
            AddButton(75, 175, 4005, 4007, 5, GumpButtonType.Reply, 0);

            AddLabel(125, 200, 0x486, "Gold");
            AddLabel(225, 200, 0x480, key.Gold.ToString());
            AddButton(75, 200, 4005, 4007, 6, GumpButtonType.Reply, 0);

            AddLabel(125, 225, 0x486, "Agapite");
            AddLabel(225, 225, 0x480, key.Agapite.ToString());
            AddButton(75, 225, 4005, 4007, 7, GumpButtonType.Reply, 0);

            AddLabel(125, 250, 0x486, "Verite");
            AddLabel(225, 250, 0x480, key.Verite.ToString());
            AddButton(75, 250, 4005, 4007, 8, GumpButtonType.Reply, 0);

            AddLabel(125, 275, 0x486, "Valorite");
            AddLabel(225, 275, 0x480, key.Valorite.ToString());
            AddButton(75, 275, 4005, 4007, 9, GumpButtonType.Reply, 0);

            AddLabel(125, 300, 0x486, "Sand");
            AddLabel(225, 300, 0x480, key.Sand.ToString());
            AddButton(75, 300, 4005, 4007, 10, GumpButtonType.Reply, 0);
            
            
            
            
            
//            AddLabel(125, 300, 0x486, "Fluorite");
//            AddLabel(225, 300, 0x480, key.Fluorite.ToString());
//            AddButton(75, 300, 4005, 4007, 10, GumpButtonType.Reply, 0);
//
//            AddLabel(125, 325, 0x486, "Platinum");
//            AddLabel(225, 325, 0x480, key.Platinum.ToString());
//            AddButton(75, 325, 4005, 4007, 11, GumpButtonType.Reply, 0);


            AddLabel(325, 75, 0x486, "Red Scales");
            AddLabel(425, 75, 0x480, key.RedScales.ToString());
            AddButton(275, 75, 4005, 4007, 20, GumpButtonType.Reply, 0);

            AddLabel(325, 100, 0x486, "Yellow Scales");
            AddLabel(425, 100, 0x480, key.YellowScales.ToString());
            AddButton(275, 100, 4005, 4007, 21, GumpButtonType.Reply, 0);

            AddLabel(325, 125, 0x486, "Black Scales");
            AddLabel(425, 125, 0x480, key.BlackScales.ToString());
            AddButton(275, 125, 4005, 4007, 22, GumpButtonType.Reply, 0);

            AddLabel(325, 150, 0x486, "Green Scales");
            AddLabel(425, 150, 0x480, key.GreenScales.ToString());
            AddButton(275, 150, 4005, 4007, 23, GumpButtonType.Reply, 0);

            AddLabel(325, 175, 0x486, "White Scales");
            AddLabel(425, 175, 0x480, key.WhiteScales.ToString());
            AddButton(275, 175, 4005, 4007, 24, GumpButtonType.Reply, 0);

            AddLabel(325, 200, 0x486, "Blue Scales");
            AddLabel(425, 200, 0x480, key.BlueScales.ToString());
            AddButton(275, 200, 4005, 4007, 25, GumpButtonType.Reply, 0);

            AddLabel(325, 275, 88, "Each Max:");
            AddLabel(425, 275, 0x480, key.StorageLimit.ToString());

            AddLabel(325, 300, 88, "Add Metal or Scales");
            AddButton(275, 300, 4005, 4007, 15, GumpButtonType.Reply, 0);

            AddLabel(325, 325, 88, "Collect All From Backpack");
            AddButton(275, 325, 4005, 4007, 16, GumpButtonType.Reply, 0);
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
	    else if (info.ButtonID == 26)
	    {
		m_Key.WithdrawIncrement = 100;
		m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
	    }
            else if (info.ButtonID == 27)
            {
                m_Key.WithdrawIncrement = 500;
                m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
            }
            else if (info.ButtonID == 28)
            {
                m_Key.WithdrawIncrement = 1000;
                m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
            }
            else if (info.ButtonID == 1)
            {
                if (m_Key.Iron > m_Key.WithdrawIncrement)								//if the key currently holds more ot this type than the increment amount
                {
                    m_From.AddToBackpack(new IronIngot(m_Key.WithdrawIncrement));  	//Send the increment amount of this type to players backpack
                    m_Key.Iron = m_Key.Iron - m_Key.WithdrawIncrement;				//removes that many from the keys count
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else if (m_Key.Iron > 0)
                {
                    m_From.AddToBackpack(new IronIngot(m_Key.Iron));  					//Sends all stored ingots of whichever type to players backpack
                    m_Key.Iron = 0;						     						//Sets the count in the key back to 0
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else
                {
                    m_From.SendMessage("You do not have any of that Ingot!");			//Tell the player he is barking up the wrong tree
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));  				//Resets the gump 
                    m_Key.BeginCombine(m_From);										//Send the player a new in-game target in case more resources are to be added
                }
            }
            else if (info.ButtonID == 2)
            {
                if (m_Key.DullCopper > m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new DullCopperIngot(m_Key.WithdrawIncrement));
                    m_Key.DullCopper = m_Key.DullCopper - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                }
                else if (m_Key.DullCopper > 0)
                {
                    m_From.AddToBackpack(new DullCopperIngot(m_Key.DullCopper));
                    m_Key.DullCopper = 0;
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that Ingot!");
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 3)
            {
                if (m_Key.ShadowIron > m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new ShadowIronIngot(m_Key.WithdrawIncrement));
                    m_Key.ShadowIron = m_Key.ShadowIron - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                }
                else if (m_Key.ShadowIron > 0)
                {
                    m_From.AddToBackpack(new ShadowIronIngot(m_Key.ShadowIron));
                    m_Key.ShadowIron = 0;
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that Ingot!");
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 4)
            {
                if (m_Key.Copper > m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new CopperIngot(m_Key.WithdrawIncrement));
                    m_Key.Copper = m_Key.Copper - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                }
                else if (m_Key.Copper > 0)
                {
                    m_From.AddToBackpack(new CopperIngot(m_Key.Copper));
                    m_Key.Copper = 0;
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that Ingot!");
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 5)
            {
                if (m_Key.Bronze > m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new BronzeIngot(m_Key.WithdrawIncrement));
                    m_Key.Bronze = m_Key.Bronze - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                }
                else if (m_Key.Bronze > 0)
                {
                    m_From.AddToBackpack(new BronzeIngot(m_Key.Bronze));
                    m_Key.Bronze = 0;
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that Ingot!");
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 6)
            {
                if (m_Key.Gold > m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new GoldIngot(m_Key.WithdrawIncrement));
                    m_Key.Gold = m_Key.Gold - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                }
                else if (m_Key.Gold > 0)
                {
                    m_From.AddToBackpack(new GoldIngot(m_Key.Gold));
                    m_Key.Gold = 0;
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that Ingot!");
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 7)
            {
                if (m_Key.Agapite > m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new AgapiteIngot(m_Key.WithdrawIncrement));
                    m_Key.Agapite = m_Key.Agapite - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                }
                else if (m_Key.Agapite > 0)
                {
                    m_From.AddToBackpack(new AgapiteIngot(m_Key.Agapite));
                    m_Key.Agapite = 0;
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that Ingot!");
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 8)
            {
                if (m_Key.Verite > m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new VeriteIngot(m_Key.WithdrawIncrement));
                    m_Key.Verite = m_Key.Verite - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                }
                else if (m_Key.Verite > 0)
                {
                    m_From.AddToBackpack(new VeriteIngot(m_Key.Verite));
                    m_Key.Verite = 0;
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that Ingot!");
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 9)
            {
                if (m_Key.Valorite > m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new ValoriteIngot(m_Key.WithdrawIncrement));
                    m_Key.Valorite = m_Key.Valorite - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                }
                else if (m_Key.Valorite > 0)
                {
                    m_From.AddToBackpack(new ValoriteIngot(m_Key.Valorite));
                    m_Key.Valorite = 0;
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that Ingot!");
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }

            
            else if (info.ButtonID == 10)
            {
                if (m_Key.Sand > m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new Sand(m_Key.WithdrawIncrement));
                    m_Key.Sand = m_Key.Sand - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                }
                else if (m_Key.Sand > 0)
                {
                    m_From.AddToBackpack(new Sand(m_Key.Sand));
                    m_Key.Sand = 0;
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any Sand!");
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            
            
            
            
            
            
            
            
            

            
            
            
            
            
            
            
            
            
//            else if (info.ButtonID == 10)
//            {
//                if (m_Key.Fluorite > m_Key.WithdrawIncrement)
//                {
//                    m_From.AddToBackpack(new FluoriteIngot(m_Key.WithdrawIncrement));
//                    m_Key.Fluorite = m_Key.Fluorite - m_Key.WithdrawIncrement;
//                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
//                }
//                else if (m_Key.Fluorite > 0)
//                {
//                    m_From.AddToBackpack(new FluoriteIngot(m_Key.Fluorite));
//                    m_Key.Fluorite = 0;
//                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
//                }
//                else
//                {
//                    m_From.SendMessage("You do not have any of that Ingot!");
//                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
//                    m_Key.BeginCombine(m_From);
//               }
//            }
//            else if (info.ButtonID == 11)
//            {
//                if (m_Key.Platinum > m_Key.WithdrawIncrement)
//                {
//                    m_From.AddToBackpack(new PlatinumIngot(m_Key.WithdrawIncrement));
//                    m_Key.Platinum = m_Key.Platinum - m_Key.WithdrawIncrement;
//                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
//                }
//                else if (m_Key.Platinum > 0)
//                {
//                    m_From.AddToBackpack(new PlatinumIngot(m_Key.Platinum));
//                    m_Key.Platinum = 0;
//                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
//                }
//                else
//                {
//                    m_From.SendMessage("You do not have any of that Ingot!");
//                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
//                    m_Key.BeginCombine(m_From);
//                }
//            }

            else if (info.ButtonID == 20)
            {
                if (m_Key.RedScales > m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new RedScales(m_Key.WithdrawIncrement));
                    m_Key.RedScales = m_Key.RedScales - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                }
                else if (m_Key.RedScales > 0)
                {
                    m_From.AddToBackpack(new RedScales(m_Key.RedScales));
                    m_Key.RedScales = 0;
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that Scales!");
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }

            else if (info.ButtonID == 21)
            {
                if (m_Key.YellowScales > m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new YellowScales(m_Key.WithdrawIncrement));
                    m_Key.YellowScales = m_Key.YellowScales - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                }
                else if (m_Key.YellowScales > 0)
                {
                    m_From.AddToBackpack(new YellowScales(m_Key.YellowScales));
                    m_Key.YellowScales = 0;
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that Scales!");
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }

            else if (info.ButtonID == 22)
            {
                if (m_Key.BlackScales > m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new BlackScales(m_Key.WithdrawIncrement));
                    m_Key.BlackScales = m_Key.BlackScales - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                }
                else if (m_Key.BlackScales > 0)
                {
                    m_From.AddToBackpack(new BlackScales(m_Key.BlackScales));
                    m_Key.BlackScales = 0;
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that Scales!");
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }

            else if (info.ButtonID == 23)
            {
                if (m_Key.GreenScales > m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new GreenScales(m_Key.WithdrawIncrement));
                    m_Key.GreenScales = m_Key.GreenScales - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                }
                else if (m_Key.GreenScales > 0)
                {
                    m_From.AddToBackpack(new GreenScales(m_Key.GreenScales));
                    m_Key.GreenScales = 0;
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that Scales!");
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }

            else if (info.ButtonID == 24)
            {
                if (m_Key.WhiteScales > m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new WhiteScales(m_Key.WithdrawIncrement));
                    m_Key.WhiteScales = m_Key.WhiteScales - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                }
                else if (m_Key.WhiteScales > 0)
                {
                    m_From.AddToBackpack(new WhiteScales(m_Key.WhiteScales));
                    m_Key.WhiteScales = 0;
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that Scales!");
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }

            else if (info.ButtonID == 25)
            {
                if (m_Key.BlueScales > m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new BlueScales(m_Key.WithdrawIncrement));
                    m_Key.BlueScales = m_Key.BlueScales - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                }
                else if (m_Key.BlueScales > 0)
                {
                    m_From.AddToBackpack(new BlueScales(m_Key.BlueScales));
                    m_Key.BlueScales = 0;
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that Scales!");
                    m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }

            else if (info.ButtonID == 15)
            {
                m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
                m_Key.BeginCombine(m_From);
            }
            else if (info.ButtonID == 16)
            {
                m_Key.CollectFromBackpack(m_From, true);
                m_From.SendGump(new ResourceStorageKeyBlackSmithGump(m_From, m_Key));
            }
        }
    }
}

namespace Server.Items
{
    public class ResourceStorageKeyBlackSmithTarget : Target
    {
        private ResourceStorageKeyBlackSmith m_Key;

        public ResourceStorageKeyBlackSmithTarget(ResourceStorageKeyBlackSmith key)
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
