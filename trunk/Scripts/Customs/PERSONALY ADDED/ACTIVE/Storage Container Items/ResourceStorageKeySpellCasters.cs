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
Added Druidic reagents by daat99.
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
using Server.Misc;
using Server.Network;
using Server.Targeting;
using Server.Multis;
using Server.Regions;


namespace Server.Items
{
    public class ResourceStorageKeySpellCasters : Item
    {
        private int m_Bone;
        private int m_PotionKeg;
        private int m_BlankScroll;
        private int m_Bottle;
        private int m_Sand;
        private int m_KeyRing; //daat keyring 1/7
        private int m_StorageLimit;
        private int m_WithdrawIncrement;
        private int m_Rune;

        [CommandProperty(AccessLevel.GameMaster)]
        public int StorageLimit { get { return m_StorageLimit; } set { m_StorageLimit = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int WithdrawIncrement { get { return m_WithdrawIncrement; } set { m_WithdrawIncrement = value; InvalidateProperties(); } }

        public int Bone { get { return m_Bone; } set { m_Bone = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int PotionKeg { get { return m_PotionKeg; } set { m_PotionKeg = value; InvalidateProperties(); } }

        public int BlankScroll { get { return m_BlankScroll; } set { m_BlankScroll = value; InvalidateProperties(); } }

        public int Bottle { get { return m_Bottle; } set { m_Bottle = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Sand { get { return m_Sand; } set { m_Sand = value; InvalidateProperties(); } }
        //start daat99 keyring 2/7
        [CommandProperty(AccessLevel.GameMaster)]
        public int KeyRing { get { return m_KeyRing; } set { m_KeyRing = value; InvalidateProperties(); } }
        //end daat99 keyring 2/7

        //Styx
        [CommandProperty(AccessLevel.GameMaster)]
        public int Rune { get { return m_Rune; } set { m_Rune = value; InvalidateProperties(); } }
        //Styx

        private static Type[] m_RegTypes = new Type[17]//changed from 18 cause tookout the executionerscap
		{
			typeof( BlackPearl ), typeof( Bloodmoss ), typeof( Garlic ),
			typeof( Ginseng ), typeof( MandrakeRoot ), typeof( Nightshade ),
			typeof( SulfurousAsh ), typeof( SpidersSilk ), typeof( BatWing ),
			typeof( GraveDust ), typeof( DaemonBlood ), typeof( NoxCrystal ),
			typeof( PigIron ), typeof( DeadWood ), 
			typeof( FertileDirt ), typeof( DaemonBone ), typeof(Beeswax),
//NOTE:TOOK OUT CAUSE CRASHED SYS:ENDNOTE typeof( ExecutionersCap ),
		};

        private Hashtable m_Regs = new Hashtable();

        public Hashtable Regs { get { return m_Regs; } set { m_Regs = value; } }

        public bool CheckAdd(Item dropped, Mobile from)
        {
            if (from is PlayerMobile)
            {
                foreach (Type type in m_RegTypes)
                {
                    if (dropped.GetType() == type)
                    {
                        AddReg(type, dropped.Amount, dropped, from);
                        return true;
                    }
                }
            }
            return false;
        }

        public void AddReg(Type type, int amount, Item dropped, Mobile from)
        {
            if (m_Regs == null)
                m_Regs = new Hashtable();
            if (m_Regs[type] == null)
            {
                if (amount <= StorageLimit)
                {
                    m_Regs.Add(type, amount);
                    dropped.Delete();
                    BeginCombine(from);
                    from.SendGump(new ResourceStorageKeySpellCastersGump((PlayerMobile)from, this));
                    from.SendMessage("You added the item.");
                }
                else
                {
                    BeginCombine(from);
                    from.SendMessage("You have too much of that item.");
                }
            }
            else if ((amount + (int)m_Regs[type]) <= StorageLimit)
            {
                if (m_Regs.ContainsKey(type))
                {
                    m_Regs[type] = (int)m_Regs[type] + amount;
                }
                else
                    m_Regs.Add(type, amount);
                dropped.Delete();
                BeginCombine(from);
                from.SendGump(new ResourceStorageKeySpellCastersGump((PlayerMobile)from, this));
                from.SendMessage("You added the item.");
            }
            else
            {
                BeginCombine(from);
                from.SendMessage("You have too much of that item.");
            }
        }

        public bool ConsumeReg(Type[] type, int[] amount)
        {
            try
            {
                for (int i = 0; i < type.Length; i++)
                {
                    if (!ConsumeReg(type[i], amount[i]))
                        return false;
                }
            }
            catch
            {
                Console.WriteLine("Error occured in consuming reagent from regcontainer.");
                return false;
            }
            ConsumeReagents(type, amount);
            return true;
        }

        private bool ConsumeReg(Type type, int amount)
        {
            if (!m_Regs.ContainsKey(type))
                return false;

            if ((int)m_Regs[type] < amount)
                return false;
            return true;
        }

        private void ConsumeReagents(Type[] type, int[] amount)
        {
            for (int i = 0; i < type.Length; i++)
                m_Regs[type[i]] = (int)m_Regs[type[i]] - amount[i];
        }

        public void WithdrawRegs(Type type, Mobile from)
        {
            if (!this.IsChildOf(from.Backpack) && !this.IsChildOf(from.BankBox))
            {
                from.SendMessage("That spell caster's key has to be in your backpack or bankbox for you to withdraw anything.");
                return;
            }

            if (!m_Regs.ContainsKey(type))
            {
                from.SendMessage("You don't have any " + type.Name + " to withdraw.");
                BeginCombine(from);
                return;//if the hash doesn't have the key we don't have any
            }
            else if ((int)m_Regs[type] > m_WithdrawIncrement)
            {
                Item oc = (Item)Activator.CreateInstance(type, new object[] { m_WithdrawIncrement });//*1
                from.AddToBackpack(oc);
                m_Regs[type] = (int)m_Regs[type] - m_WithdrawIncrement;
            }
            else if ((int)m_Regs[type] == 0)
            {
                from.SendMessage("You don't have any " + type.Name + " to withdraw.");
                BeginCombine(from);
                return;//if the hash doesn't have the key we don't have any
            }
            else
            {
                Item oc = (Item)Activator.CreateInstance(type, new object[] { (int)m_Regs[type] });//*1
                from.AddToBackpack(oc);
                m_Regs.Remove(type);//Delete the hastable entry to save space
            }
            from.SendGump(new ResourceStorageKeySpellCastersGump((PlayerMobile)from, this));
        }


        public void CollectFromBackpack(Mobile from, bool showMessage)
        {
            if (!from.Alive)
                return;
            if (!this.IsChildOf(from.Backpack) && !this.IsChildOf(from.BankBox))
            {
                from.SendMessage("That spell caster's key has to be in your backpack or bankbox for you to use it.");
                return;
            }

            for(int i=0; i < 17; i++)//canged from 18
            {
                Type type = m_RegTypes[i];
                Item[] items = from.Backpack.FindItemsByType(type, true);
                int currentAmount = 0;
                if (!m_Regs.ContainsKey(type))
                    m_Regs.Add(type, (int)0);
                else
                    currentAmount = (int)m_Regs[type];
                for (int j = 0; j < items.Length; j++)
                {
                    Item item = items[j];
                    if (!item.Movable)
                        continue;
                    int amount = item.Amount;
                    if (amount + currentAmount > m_StorageLimit)
                        break;
                    else
                    {
                        currentAmount += amount;
                        item.Delete();
                    }
                }
                m_Regs[type] = currentAmount;
            }

            Type[] type1 = new Type[] { typeof(BlankScroll), typeof(Bottle), typeof(RecallRune), typeof(Sand) };

            Item[] items1 = from.Backpack.FindItemsByType(type1, true);
            for (int j = 0; j < items1.Length; j++)
            {
                Item item = items1[j];

                if (!item.Movable)
                    continue;
                if (item.Hue != 0)
                    continue;

                int amount = item.Amount;
                int currentAmount = 0;

                if (item is BlankScroll)
                    currentAmount = m_BlankScroll;
                else if (item is Bottle)
                    currentAmount = m_Bottle;
                else if (item is RecallRune && !((RecallRune)item).Marked)
                    currentAmount = m_Rune;
                else if (item is Sand)
                	currentAmount = m_Sand;

                if (amount + currentAmount > m_StorageLimit)
                    continue;
                else
                {
                    if (item is BlankScroll)
                        m_BlankScroll += amount;
                    else if (item is Bottle)
                        m_Bottle += amount;
                    else if (item is RecallRune && !((RecallRune)item).Marked)
                        m_Rune += amount;
                    else if (item is Sand)
                    	m_Sand += amount;

                    item.Delete();
                }
            }

            if (showMessage)
            from.SendMessage("Reagents etc are collected from your backpack into that key, subject to storage limit.");
        }

        [Constructable]
        public ResourceStorageKeySpellCasters()
            : base(0x176B)
        {
            Movable = true;
            Weight = 1.0;
            Hue = 33;
            Name = "Spell Caster's Keys";
            LootType = LootType.Blessed;
            StorageLimit = 60000;
            WithdrawIncrement = 100;

        }

        [Constructable]
        public ResourceStorageKeySpellCasters(int storageLimit, int withdrawIncrement)
            : base(0x176B)
        {
            Movable = true;
            Weight = 1.0;
            Hue = 33;
            Name = "Spell Caster's Keys";
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
                from.SendGump(new ResourceStorageKeySpellCastersGump((PlayerMobile)from, this));
            else
                from.SendMessage("This must be in your backpack or bankbox to use.");
        }

        public void BeginCombine(Mobile from)
        {
            from.Target = new ResourceStorageKeySpellCastersTarget(this);
        }

        public void EndCombine(Mobile from, object o)
        {
            if (o is Item && (((Item)o).IsChildOf(from.Backpack) || ((Item)o).IsChildOf(from.BankBox)))
            {
                Item curItem = o as Item;
                if (!(curItem is BaseReagent))
                {
                    if (curItem is Sand)
                    {
                        if (Sand + curItem.Amount > StorageLimit)
                            from.SendMessage("That resource type cannot hold the amount you're trying to store, in addition to what it currently has.");
                        else
                        {
                            Sand += + curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeySpellCastersGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
//                    else if (curItem is Bone)
//                    {
//                        from.SendMessage("The spell caster's key is not accepting bones anymore. Please use the tailor's key instead.");
//                        /*
//                        if (Bone + curItem.Amount > StorageLimit)
//                            from.SendMessage("You are trying to add " + ((Bone + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
//                        else
//                        {
//                            Bone += curItem.Amount;
//                            curItem.Delete();
//                            from.SendGump(new ResourceStorageKeySpellCastersGump((PlayerMobile)from, this));
//                            BeginCombine(from);
//                        }
//                         */ 
//                    }
//                    // daat99 keyring 3/7
                    else if (curItem is KeyRing)
                    {
                        if (KeyRing + curItem.Amount > StorageLimit)
                            from.SendMessage("That resource type cannot hold the amount you're trying to store, in addition to what it currently has.");
                        else
                        {
                            curItem.Delete();
                            KeyRing = (KeyRing + 1);
                            from.SendGump(new ResourceStorageKeySpellCastersGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    //
                    else if (curItem is PotionKeg)
                    {
                        if (((PotionKeg)curItem).Held > 0)
                            from.SendMessage("You can add only empty potion kegs.");
                        else if (PotionKeg + curItem.Amount > StorageLimit)
                            from.SendMessage("I think you already have enough.");

                        else
                        {
                            curItem.Delete();
                            PotionKeg = (PotionKeg + 1);
                            from.SendGump(new ResourceStorageKeySpellCastersGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is BlankScroll)
                    {

                        if (BlankScroll + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((BlankScroll + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            BlankScroll += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeySpellCastersGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is Bottle)
                    {
                        if (Bottle + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((Bottle + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            Bottle += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeySpellCastersGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is RecallRune)
                    {
                        RecallRune rune = (RecallRune)curItem;

                        if (rune.Marked)
                        {
                            from.SendMessage("You are trying to add a marked recall rune to the key, that is not allowed.");
                        }
                        else if (Rune + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add" + ((Rune + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this item.");
                        else
                        {
                            Rune += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeySpellCastersGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                }
                else if (curItem is BlackPearl || curItem is Bloodmoss || curItem is Garlic || curItem is Ginseng ||
                    curItem is MandrakeRoot || curItem is Nightshade || curItem is SulfurousAsh ||
                    curItem is SpidersSilk || curItem is BatWing || curItem is GraveDust || curItem is DaemonBlood ||
                    curItem is NoxCrystal || curItem is PigIron || curItem is DaemonBone || curItem is DeadWood ||
                    curItem is FertileDirt || curItem is Beeswax) //curItem is ExecutionersCap || 
                    this.CheckAdd((Item)curItem, from);
                else
                {
                    from.SendMessage("That does not belong in here."); //If the item is not a resource, player gets this message
                }
            }
            else
            {
                from.SendLocalizedMessage(1045158); // You must have the item in your backpack to target it.
            }
        }
        public ResourceStorageKeySpellCasters(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)1); // version

            writer.Write((int)m_Rune);

            writer.Write(m_Regs.Count);
            foreach (DictionaryEntry de in m_Regs)
            {
                writer.Write(((Type)de.Key).Name);
                writer.Write((int)de.Value);
            }
            writer.Write((int)m_Bone);
            writer.Write((int)m_PotionKeg);
            writer.Write((int)m_BlankScroll);
            writer.Write((int)m_Bottle);
            writer.Write((int)m_Sand);
            writer.Write((int)m_KeyRing); // daat99 keyring 4/7
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
                    {
                        m_Rune = reader.ReadInt();
                        goto case 0;
                    }
                case 0:
                    {
                        int len = reader.ReadInt();
                        for (int i = 0; i < len; i++)
                            m_Regs.Add(ScriptCompiler.FindTypeByName(reader.ReadString()), reader.ReadInt());
                        m_Bone = reader.ReadInt();
                        m_PotionKeg = reader.ReadInt();
                        m_BlankScroll = reader.ReadInt();
                        m_Bottle = reader.ReadInt();
                        m_Sand = reader.ReadInt();
                        m_KeyRing = reader.ReadInt(); //daat99 keyring 5/7
                        m_StorageLimit = reader.ReadInt();
                        m_WithdrawIncrement = reader.ReadInt();                        
                        break;
                    }
            }

            LootType = LootType.Blessed;
        }
    }

    public class ResourceStorageKeySpellCastersGump : Gump
    {
        private PlayerMobile m_From;
        private ResourceStorageKeySpellCasters m_Key;

        public ResourceStorageKeySpellCastersGump(PlayerMobile from, ResourceStorageKeySpellCasters key)
            : base(25, 25)
        {
            m_From = from;
            m_Key = key;

            m_From.CloseGump(typeof(ResourceStorageKeySpellCastersGump));

            AddPage(0);

            AddBackground(50, 10, 455, 430, 5054);
            AddImageTiled(58, 20, 438, 410, 2624);
            AddAlphaRegion(58, 20, 438, 410);

            AddLabel(200, 25, 88, "Spell Caster's Warehouse");
// Kazuha
	    AddLabel(125, 50, 0x486, "Withdraw Increment");
	    AddLabel(275, 50, 0x480, key.WithdrawIncrement.ToString());
	    AddButton(330, 50, 4011, 4012, 28, GumpButtonType.Reply, 0);
	    AddButton(360, 50, 4011, 4012, 29, GumpButtonType.Reply, 0);
	    AddButton(390, 50, 4011, 4012, 30, GumpButtonType.Reply, 0);
// /Kazuha

            AddLabel(125, 75, 0x486, "BlackPearl");
            AddButton(75, 75, 4005, 4007, 1, GumpButtonType.Reply, 0);
            if (m_Key.Regs.ContainsKey(typeof(BlackPearl)))
                AddLabel(225, 75, 0x480, m_Key.Regs[typeof(BlackPearl)].ToString());
            else
                AddLabel(225, 75, 0x480, "0");

            AddLabel(125, 100, 0x486, "Bloodmoss");
            AddButton(75, 100, 4005, 4007, 2, GumpButtonType.Reply, 0);
            if (m_Key.Regs.ContainsKey(typeof(Bloodmoss)))
                AddLabel(225, 100, 0x480, m_Key.Regs[typeof(Bloodmoss)].ToString());
            else
                AddLabel(225, 100, 0x480, "0");

            AddLabel(125, 125, 0x486, "Garlic");
            AddButton(75, 125, 4005, 4007, 3, GumpButtonType.Reply, 0);
            if (m_Key.Regs.ContainsKey(typeof(Garlic)))
                AddLabel(225, 125, 0x480, m_Key.Regs[typeof(Garlic)].ToString());
            else
                AddLabel(225, 125, 0x480, "0");

            AddLabel(125, 150, 0x486, "Ginseng");
            AddButton(75, 150, 4005, 4007, 4, GumpButtonType.Reply, 0);
            if (m_Key.Regs.ContainsKey(typeof(Ginseng)))
                AddLabel(225, 150, 0x480, m_Key.Regs[typeof(Ginseng)].ToString());
            else
                AddLabel(225, 150, 0x480, "0");

            AddLabel(125, 175, 0x486, "MandrakeRoot");
            AddButton(75, 175, 4005, 4007, 5, GumpButtonType.Reply, 0);
            if (m_Key.Regs.ContainsKey(typeof(MandrakeRoot)))
                AddLabel(225, 175, 0x480, m_Key.Regs[typeof(MandrakeRoot)].ToString());
            else
                AddLabel(225, 175, 0x480, "0");

            AddLabel(125, 200, 0x486, "Nightshade");
            AddButton(75, 200, 4005, 4007, 6, GumpButtonType.Reply, 0);
            if (m_Key.Regs.ContainsKey(typeof(Nightshade)))
                AddLabel(225, 200, 0x480, m_Key.Regs[typeof(Nightshade)].ToString());
            else
                AddLabel(225, 200, 0x480, "0");

            AddLabel(125, 225, 0x486, "SulfurousAsh");
            AddButton(75, 225, 4005, 4007, 7, GumpButtonType.Reply, 0);
            if (m_Key.Regs.ContainsKey(typeof(SulfurousAsh)))
                AddLabel(225, 225, 0x480, m_Key.Regs[typeof(SulfurousAsh)].ToString());
            else
                AddLabel(225, 225, 0x480, "0");

            AddLabel(125, 250, 0x486, "SpidersSilk");
            AddButton(75, 250, 4005, 4007, 8, GumpButtonType.Reply, 0);
            if (m_Key.Regs.ContainsKey(typeof(SpidersSilk)))
                AddLabel(225, 250, 0x480, m_Key.Regs[typeof(SpidersSilk)].ToString());
            else
                AddLabel(225, 250, 0x480, "0");

            AddLabel(125, 275, 0x486, "BatWing");
            AddButton(75, 275, 4005, 4007, 9, GumpButtonType.Reply, 0);
            if (m_Key.Regs.ContainsKey(typeof(BatWing)))
                AddLabel(225, 275, 0x480, m_Key.Regs[typeof(BatWing)].ToString());
            else
                AddLabel(225, 275, 0x480, "0");

            AddLabel(125, 300, 0x486, "GraveDust");
            AddButton(75, 300, 4005, 4007, 10, GumpButtonType.Reply, 0);
            if (m_Key.Regs.ContainsKey(typeof(GraveDust)))
                AddLabel(225, 300, 0x480, m_Key.Regs[typeof(GraveDust)].ToString());
            else
                AddLabel(225, 300, 0x480, "0");

            AddLabel(125, 325, 0x486, "DaemonBlood");
            AddButton(75, 325, 4005, 4007, 11, GumpButtonType.Reply, 0);
            if (m_Key.Regs.ContainsKey(typeof(DaemonBlood)))
                AddLabel(225, 325, 0x480, m_Key.Regs[typeof(DaemonBlood)].ToString());
            else
                AddLabel(225, 325, 0x480, "0");

            AddLabel(125, 350, 0x486, "NoxCrystal");
            AddButton(75, 350, 4005, 4007, 12, GumpButtonType.Reply, 0);
            if (m_Key.Regs.ContainsKey(typeof(NoxCrystal)))
                AddLabel(225, 350, 0x480, m_Key.Regs[typeof(NoxCrystal)].ToString());
            else
                AddLabel(225, 350, 0x480, "0");

            AddLabel(125, 375, 0x486, "PigIron");
            AddButton(75, 375, 4005, 4007, 13, GumpButtonType.Reply, 0);
            if (m_Key.Regs.ContainsKey(typeof(PigIron)))
                AddLabel(225, 375, 0x480, m_Key.Regs[typeof(PigIron)].ToString());
            else
                AddLabel(225, 375, 0x480, "0");
           
//            AddLabel(325, 75, 0x486, "Bone");
//            AddLabel(425, 75, 0x480, key.Bone.ToString());
//            AddButton(275, 75, 4005, 4007, 18, GumpButtonType.Reply, 0);

            AddLabel(325, 100, 0x486, "Daemon Bone");
            AddButton(275, 100, 4005, 4007, 19, GumpButtonType.Reply, 0);
            if (m_Key.Regs.ContainsKey(typeof(DaemonBone)))
                AddLabel(425, 100, 0x480, m_Key.Regs[typeof(DaemonBone)].ToString());
            else
                AddLabel(425, 100, 0x480, "0");

            AddLabel(325, 125, 0x486, "PotionKeg");
            AddLabel(425, 125, 0x480, key.PotionKeg.ToString());
            AddButton(275, 125, 4005, 4007, 20, GumpButtonType.Reply, 0);

            AddLabel(325, 150, 0x486, "BlankScroll");
            AddLabel(425, 150, 0x480, key.BlankScroll.ToString());
            AddButton(275, 150, 4005, 4007, 21, GumpButtonType.Reply, 0);

             //Styx
            AddLabel(125, 400, 0x486, "Recall Rune");
            AddLabel(225, 400, 0x480, key.Rune.ToString());
            AddButton(75, 400, 4005, 4007, 27, GumpButtonType.Reply, 0);
            //Styx

            AddLabel(325, 175, 0x486, "Bottle");
            AddLabel(425, 175, 0x480, key.Bottle.ToString());
            AddButton(275, 175, 4005, 4007, 22, GumpButtonType.Reply, 0);

            AddLabel(325, 200, 0x486, "Sand");
            AddLabel(425, 200, 0x480, key.Sand.ToString());
            AddButton(275, 200, 4005, 4007, 23, GumpButtonType.Reply, 0);
            // daat99 keyring 6/7			
            AddLabel(325, 225, 0x486, "KeyRing");
            AddLabel(425, 225, 0x480, key.KeyRing.ToString());
            AddButton(275, 225, 4005, 4007, 24, GumpButtonType.Reply, 0);
            //
            AddLabel(325, 250, 0x486, "Bees Wax");
            AddButton(275, 250, 4005, 4007, 17, GumpButtonType.Reply, 0);
            if (m_Key.Regs.ContainsKey(typeof(Beeswax)))
                AddLabel(425, 250, 0x480, m_Key.Regs[typeof(Beeswax)].ToString());
            else
                AddLabel(425, 250, 0x480, "0");

            AddLabel(325, 275, 0x486, "Dead Wood");
            AddButton(275, 275, 4005, 4007, 14, GumpButtonType.Reply, 0);
            if (m_Key.Regs.ContainsKey(typeof(DeadWood)))
                AddLabel(425, 275, 0x480, m_Key.Regs[typeof(DeadWood)].ToString());
            else
                AddLabel(425, 275, 0x480, "0");

//            AddLabel(325, 300, 0x486, "Executioner's Cap");
//            AddButton(275, 300, 4005, 4007, 15, GumpButtonType.Reply, 0);
//            if (m_Key.Regs.ContainsKey(typeof(ExecutionersCap)))
//                AddLabel(425, 300, 0x480, m_Key.Regs[typeof(ExecutionersCap)].ToString());
//            else
//                AddLabel(425, 300, 0x480, "0");
//
            AddLabel(325, 325, 0x486, "Fertile Dirt");
            AddButton(275, 325, 4005, 4007, 16, GumpButtonType.Reply, 0);
            if (m_Key.Regs.ContainsKey(typeof(FertileDirt)))
                AddLabel(425, 325, 0x480, m_Key.Regs[typeof(FertileDirt)].ToString());
            else
                AddLabel(425, 325, 0x480, "0");

            AddLabel(325, 350, 88, "Each Max:");
            AddLabel(425, 350, 0x480, key.StorageLimit.ToString());

            AddLabel(325, 375, 88, "Add resource or item");
            AddButton(275, 375, 4005, 4007, 25, GumpButtonType.Reply, 0);

            AddLabel(325, 400, 88, "Collect regs from backpack");
            AddButton(275, 400, 4005, 4007, 26, GumpButtonType.Reply, 0);
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
	    else if (info.ButtonID == 28)
	    {
		m_Key.WithdrawIncrement = 100;
		m_From.SendGump(new ResourceStorageKeySpellCastersGump(m_From, m_Key));
	    }
	    else if (info.ButtonID == 29)
	    {
	  	m_Key.WithdrawIncrement = 500;
		m_From.SendGump(new ResourceStorageKeySpellCastersGump(m_From, m_Key));
	    }
	    else if (info.ButtonID == 30)
	    {
		m_Key.WithdrawIncrement = 1000;
		m_From.SendGump(new ResourceStorageKeySpellCastersGump(m_From, m_Key));
	    }
            else if (info.ButtonID == 1)
                m_Key.WithdrawRegs(typeof(BlackPearl), m_From);
            else if (info.ButtonID == 2)
                m_Key.WithdrawRegs(typeof(Bloodmoss), m_From);
            else if (info.ButtonID == 3)
                m_Key.WithdrawRegs(typeof(Garlic), m_From);
            else if (info.ButtonID == 4)
                m_Key.WithdrawRegs(typeof(Ginseng), m_From);
            else if (info.ButtonID == 5)
                m_Key.WithdrawRegs(typeof(MandrakeRoot), m_From);
            else if (info.ButtonID == 6)
                m_Key.WithdrawRegs(typeof(Nightshade), m_From);
            else if (info.ButtonID == 7)
                m_Key.WithdrawRegs(typeof(SulfurousAsh), m_From);
            else if (info.ButtonID == 8)
                m_Key.WithdrawRegs(typeof(SpidersSilk), m_From);
            else if (info.ButtonID == 9)
                m_Key.WithdrawRegs(typeof(BatWing), m_From);
            else if (info.ButtonID == 10)
                m_Key.WithdrawRegs(typeof(GraveDust), m_From);
            else if (info.ButtonID == 11)
                m_Key.WithdrawRegs(typeof(DaemonBlood), m_From);
            else if (info.ButtonID == 12)
                m_Key.WithdrawRegs(typeof(NoxCrystal), m_From);
            else if (info.ButtonID == 13)
                m_Key.WithdrawRegs(typeof(PigIron), m_From);
            else if (info.ButtonID == 14)
                m_Key.WithdrawRegs(typeof(DeadWood), m_From);
//            else if (info.ButtonID == 15)
//                m_Key.WithdrawRegs(typeof(ExecutionersCap), m_From);
            else if (info.ButtonID == 16)
                m_Key.WithdrawRegs(typeof(FertileDirt), m_From);
            else if (info.ButtonID == 17)
                m_Key.WithdrawRegs(typeof(Beeswax), m_From);

//            else if (info.ButtonID == 18)
//            {
//                if (m_Key.Bone > m_Key.WithdrawIncrement)
//                {
//                    m_From.AddToBackpack(new Bone(m_Key.WithdrawIncrement));
//                    m_Key.Bone = m_Key.Bone - m_Key.WithdrawIncrement;
//                    m_From.SendGump(new ResourceStorageKeySpellCastersGump(m_From, m_Key));
//                }
//                else if (m_Key.Bone > 0)
//                {
//                    m_From.AddToBackpack(new Bone(m_Key.Bone));
//                    m_Key.Bone = 0;
//                    m_From.SendGump(new ResourceStorageKeySpellCastersGump(m_From, m_Key));
//                }
//                else
//                {
//                    m_From.SendMessage("You do not have any of that resource!");
//                    m_From.SendGump(new ResourceStorageKeySpellCastersGump(m_From, m_Key));
//                    m_Key.BeginCombine(m_From);
//                }
//            }
            else if (info.ButtonID == 19)
                m_Key.WithdrawRegs(typeof(DaemonBone), m_From);
            else if (info.ButtonID == 20)
            {
                if (m_Key.PotionKeg > 0)
                {
                    m_From.AddToBackpack(new PotionKeg());
                    m_Key.PotionKeg = (m_Key.PotionKeg - 1);
                    m_From.SendGump(new ResourceStorageKeySpellCastersGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that resource!");
                    m_From.SendGump(new ResourceStorageKeySpellCastersGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 21)
            {
                if (m_Key.BlankScroll > m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new BlankScroll(m_Key.WithdrawIncrement));
                    m_Key.BlankScroll = m_Key.BlankScroll - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeySpellCastersGump(m_From, m_Key));
                }
                else if (m_Key.BlankScroll > 0)
                {
                    m_From.AddToBackpack(new BlankScroll(m_Key.BlankScroll));
                    m_Key.BlankScroll = 0;
                    m_From.SendGump(new ResourceStorageKeySpellCastersGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that resource!");
                    m_From.SendGump(new ResourceStorageKeySpellCastersGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 22)
            {
                if (m_Key.Bottle > m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new Bottle(m_Key.WithdrawIncrement));
                    m_Key.Bottle = m_Key.Bottle - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeySpellCastersGump(m_From, m_Key));
                }
                else if (m_Key.Bottle > 0)
                {
                    m_From.AddToBackpack(new Bottle(m_Key.Bottle));
                    m_Key.Bottle = 0;
                    m_From.SendGump(new ResourceStorageKeySpellCastersGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that resource!");
                    m_From.SendGump(new ResourceStorageKeySpellCastersGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            
            //STYX>>

            else if (info.ButtonID == 27)
            {
                if (m_Key.Rune > 0)
                {
                    m_From.AddToBackpack(new RecallRune());
                    m_Key.Rune = (m_Key.Rune - 1);
                    m_From.SendGump(new ResourceStorageKeySpellCastersGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any recall runes!");
                    m_From.SendGump(new ResourceStorageKeySpellCastersGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
                //STYX<<
            }
            else if (info.ButtonID == 23)
            {
                if (m_Key.Sand > m_Key.WithdrawIncrement)
                {
                    m_From.AddToBackpack(new Sand(m_Key.WithdrawIncrement));
                    m_Key.Sand = m_Key.Sand - m_Key.WithdrawIncrement;
                    m_From.SendGump(new ResourceStorageKeySpellCastersGump(m_From, m_Key));
                }
                else if (m_Key.Sand > 0)
                {
                	m_From.AddToBackpack(new Sand (m_Key.Sand));
                	m_Key.Sand = 0;
                	m_From.SendGump(new ResourceStorageKeySpellCastersGump(m_From, m_Key));                	
                }
                else
                {
                    m_From.SendMessage("You do not have any of that resource!");
                    m_From.SendGump(new ResourceStorageKeySpellCastersGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            // daat99 keyring 7/7
            else if (info.ButtonID == 24)
            {
                if (m_Key.KeyRing > 0)
                {
                    m_From.AddToBackpack(new KeyRing());
                    m_Key.KeyRing = (m_Key.KeyRing - 1);
                    m_From.SendGump(new ResourceStorageKeySpellCastersGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that resource!");
                    m_From.SendGump(new ResourceStorageKeySpellCastersGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            //
            else if (info.ButtonID == 25)
            {
                m_Key.BeginCombine(m_From);
                m_From.SendGump(new ResourceStorageKeySpellCastersGump(m_From, m_Key));
            }
            else if (info.ButtonID == 26)
            {
                m_Key.CollectFromBackpack(m_From, true);
                m_From.SendGump(new ResourceStorageKeySpellCastersGump(m_From, m_Key));
            }
        }
    }

    public class ResourceStorageKeySpellCastersTarget : Target
    {
        private ResourceStorageKeySpellCasters m_Key;

        public ResourceStorageKeySpellCastersTarget(ResourceStorageKeySpellCasters key)
            : base(17, false, TargetFlags.None)//changed from 18
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
