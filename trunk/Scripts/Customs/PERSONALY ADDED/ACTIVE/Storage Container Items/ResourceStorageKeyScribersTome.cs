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

description: a small tome that let you store scrolls.
Idea by Beldr
*/
using System;
using System.Collections;
using Server.Mobiles;
using Server.Gumps;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
    public class ResourceStorageKeyScribersTome : Item
    {
        private static int m_Capacity = 60000;
        private ArrayList al_GlobalEntry;
        public ArrayList GlobalEntry { get { return al_GlobalEntry; } }

        [Constructable]
        public ResourceStorageKeyScribersTome()
            : base(0xEFA)
        {
            Movable = true;
            Weight = 1.0;
            Hue = 88;
            Name = "Scriber's Tome";
            LootType = LootType.Blessed;
            InitArray();
        }

        public void InitArray()
        {
            if (al_GlobalEntry == null)
                al_GlobalEntry = new ArrayList();
            int index = 0;
            for (int i = 0; i < 116; i++, index++)
            {
                if (i == 64)
                    i = 100;
                al_GlobalEntry.Add(new ScrollEntry(i, 1, 0, ScrollsTypes[index]));
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
//            if (from.Map == Map.Felucca)
//            {
//                from.SendMessage("That does not work in Felucca.");
//                return;
//            }

            if (al_GlobalEntry == null)
                InitArray();

            if (IsChildOf(from.Backpack) || IsChildOf(from.BankBox))
                from.SendGump(new ResourceStorageKeyScribersTomeGump(from, this));
            else
                from.SendMessage("This must be in your backpack or bankbox to use.");
        }

        public override bool OnDragDrop(Mobile from, Item dropped)
        {
            if (!IsChildOf(from.Backpack) && !IsChildOf(from.BankBox))
            {
                from.SendMessage("That is not in your backpack or bankbox.");
                return false;
            }
            if (dropped is SpellScroll && dropped.Amount >= 1 && dropped.Amount <= m_Capacity)
                AddScroll((SpellScroll)dropped, from, false);
            return false;
        }

        public void BeginCombine(Mobile from) { from.Target = new ResourceStorageKeyScribersTomeTarget(this); }

        public void BeginFillSpellBook(Mobile from) { from.Target = new FillSpellBookTarget(this); }

        public void EndCombine(Mobile from, object o)
        {
            if (!IsChildOf(from.Backpack) && !IsChildOf(from.BankBox))
            {
                from.SendMessage("This must be in your backpack or bankbox to use.");
                return;
            }

            if (!(o is SpellScroll))
            {
                from.SendMessage(32, "That isn't a Scroll");
                return;
            }

            SpellScroll scroll = (SpellScroll)o;

            if (!scroll.IsChildOf(from.Backpack) && !scroll.IsChildOf(from.BankBox))
            {
                from.SendMessage("That scroll must be in your backpack or bankbox for you to store it in the tome.");
                return;
            }

            AddScroll(scroll, from, true);
        }

        public void AddScroll(SpellScroll sps, Mobile from, bool gump)
        {
            if (al_GlobalEntry == null)
                InitArray();
            int sid = sps.SpellID;
            if (sid >= 100 && sid <= 115)
                sid -= 36;
            else if (sid > 64)
            {
                from.SendMessage(33, "You can't add this scroll");
                return;
            }
            if (((ScrollEntry)al_GlobalEntry[sid]).Amount >= 60000)
            {
                from.SendMessage(33, "You can't add more charges, the limit is 60,000.");
                return;
            }
            else if (((ScrollEntry)al_GlobalEntry[sid]).Amount + sps.Amount > 60000)
            {
                sps.Amount = (((ScrollEntry)al_GlobalEntry[sid]).Amount + sps.Amount) - 60000;
                ((ScrollEntry)al_GlobalEntry[sid]).Amount = 60000;
            }
            else
            {
                ((ScrollEntry)al_GlobalEntry[sid]).Amount += sps.Amount;
                sps.Delete();
            }
            if (((ScrollEntry)al_GlobalEntry[sid]).SItemID == 1)
                ((ScrollEntry)al_GlobalEntry[sid]).SItemID = sps.ItemID;
            from.SendMessage(88, "You added the scrolls.");
            if (gump)
                from.SendGump(new ResourceStorageKeyScribersTomeGump(from, this));
        }

        public void EndFill(Mobile from, object o)
        {
            if (!IsChildOf(from.Backpack) && !IsChildOf(from.BankBox))
            {
                from.SendMessage("That tome must be in your backpack or bankbox for you to use it.");
                return;
            }

            if (!(o is Spellbook))
            {
                from.SendMessage(32, "That isn't a Spellbook.");
                return;
            }

            Spellbook book = (Spellbook)o;

            if (!book.IsChildOf(from.Backpack) && !book.IsChildOf(from.BankBox))
            {
                from.SendMessage("That spell book must be in your backpack or bankbox for you to fill it from the tome.");
                return;
            }

            FillBook(book, from, true);
        }

        public void FillBook(Spellbook spb, Mobile from, bool gump)
        {
        	if (spb is BookOfChivalry || spb is BookOfBushido || spb is BookOfNinjitsu)//NOTE:tookout druid cause dont have:ENDNOTE || spb is DruidSpellbook)
            {
                from.SendMessage("That spellbook is not supported.");
                return;
            }

            if (al_GlobalEntry == null)
                InitArray();

            int numSpellMissing = 0;
            int numSpellFilled = 0;
            for (int sid = spb.BookOffset; sid < spb.BookOffset + spb.BookCount; sid++)
            {
                int tid = sid;
                if (spb.HasSpell(sid))
                    continue;

                if (tid >= 100 && tid <= 115)
                    tid -= 36;
                else if (tid > 64)
                    continue;

                if (((ScrollEntry)al_GlobalEntry[tid]).Amount <= 0)
                {
                    numSpellMissing++;
                    continue;
                }

                ((ScrollEntry)al_GlobalEntry[tid]).Amount --;
					int val = sid - spb.BookOffset;

				if ( val >= 0 && val < spb.BookCount ) // double check
				{
					spb.Content |= (ulong)1 << val;

                    numSpellFilled++;

					spb.InvalidateProperties();
                }
            
            }

            from.SendMessage(numSpellFilled.ToString() + " spells are added to the book. ");
            if (numSpellMissing > 0)
                from.SendMessage("There are still " + numSpellMissing.ToString() + " spells missing.");
            else
                from.SendMessage("Now the spell book is complete.");

            if (gump)
                from.SendGump(new ResourceStorageKeyScribersTomeGump(from, this));
        }

        public void CollectFromBackpack(Mobile from, bool showMessage)
        {
            if (!from.Alive)
                return;
            if (!this.IsChildOf(from.Backpack) && !this.IsChildOf(from.BankBox))
            {
                from.SendMessage("That scribe's tomb has to be in your backpack or bankbox for you to use it.");
                return;
            }

            if (al_GlobalEntry == null)
                InitArray();

            Item[] scrolls = from.Backpack.FindItemsByType(typeof(SpellScroll), true);

            for (int i = 0; i < scrolls.Length; i++)
            {
                SpellScroll sps = (SpellScroll)scrolls[i];
                if (!sps.Movable)
                    continue;

                int sid = sps.SpellID;
                if (sid >= 100 && sid <= 115)
                    sid -= 36;
                else if (sid > 64)
                    continue;

                if (((ScrollEntry)al_GlobalEntry[sid]).Amount >= m_Capacity)
                    continue;
                else if (((ScrollEntry)al_GlobalEntry[sid]).Amount + sps.Amount > m_Capacity)
                {
                    sps.Amount = (((ScrollEntry)al_GlobalEntry[sid]).Amount + sps.Amount) - m_Capacity;
                    ((ScrollEntry)al_GlobalEntry[sid]).Amount = m_Capacity;
                }
                else
                {
                    ((ScrollEntry)al_GlobalEntry[sid]).Amount += sps.Amount;
                    sps.Delete();
                }
                if (((ScrollEntry)al_GlobalEntry[sid]).SItemID == 1)
                    ((ScrollEntry)al_GlobalEntry[sid]).SItemID = sps.ItemID;
            }

            if (showMessage)
            from.SendMessage("Scrolls are collected from your backpack into that tome, subject to storage limit.");
        }

        public ResourceStorageKeyScribersTome(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
            if (al_GlobalEntry == null)
                writer.Write((int)0);
            else
            {
                writer.Write((int)al_GlobalEntry.Count);
                for (int i = 0; i < al_GlobalEntry.Count; i++)
                {
                    writer.Write((int)((ScrollEntry)al_GlobalEntry[i]).SpellID);
                    writer.Write((int)((ScrollEntry)al_GlobalEntry[i]).SItemID);
                    writer.Write((int)((ScrollEntry)al_GlobalEntry[i]).Amount);
                }
            }
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            if (al_GlobalEntry == null)
                al_GlobalEntry = new ArrayList();
            int l = reader.ReadInt();
            for (int i = 0; i < l; i++)
                al_GlobalEntry.Add(new ScrollEntry(reader.ReadInt(), reader.ReadInt(), reader.ReadInt(), ScrollsTypes[i]));
        }

        public static string[] ScrollsNames = new string[80]
		{
			"Clumsy",			"Create Food",				"Feeblemind",				"Heal",
			"Magic Arrow",		"Night Sight",				"Reactive Armor",			"Weaken",
			"Agility",			"Cunning",					"Cure",						"Harm",
			"Magic Trap",		"Magic Untrap",				"Protection",				"Strength",
			"Bless",			"Fireball",					"Magic Lock",				"Poison",
			"Telekinisis",		"Teleport",					"Unlock",					"Wall Of Stone",
			"Arch Cure",		"Arch Protection",			"Curse",					"Fire Field",
			"Greater Heal",		"Lightning",				"Mana Drain",				"Recall",
			"Blade Spirits",	"Dispel Field",				"Incognito",				"Magic Reflect",
			"Mind Blast",		"Paralyze",					"Poison Field",				"Summon Creature",
			"Dispel",			"Energy Bolt",				"Explosion",				"Invisibility",
			"Mark",				"Mass Curse",				"Paralyze Field",			"Reveal",
			"Chain Lightning",	"Energy Field",				"Flamestrike",				"Gate Travel",
			"Mana Vampire",		"Mass Dispel",				"Meteor Swarm",				"Polymorph",
			"Earthquake",		"Energy Vortex",			"Resurrection",				"Summon Air Elemental",
			"Summon Daemon",	"Summon Earth Elemental",	"Summon Fire Elemental",	"Summon Water Elemental",
			"Animate Dead",		"BloodOath",				"Corpse Skin",				"Curse Weapon",
			"Evil Omen",		"Horrific Beast",			"Lich Form",				"Mind Rot",
			"Pain Spike",		"Poison Strike",			"Strangle",					"Summon Familiar",
			"Vampiric Embrace",	"Vengeful Spirit",			"Wither",					"Wraith Form"

		};

        public static Type[] ScrollsTypes = new Type[80]
		{
			typeof(ClumsyScroll),			typeof(CreateFoodScroll),				typeof(FeeblemindScroll),				typeof(HealScroll),
			typeof(MagicArrowScroll),		typeof(NightSightScroll),				typeof(ReactiveArmorScroll),			typeof(WeakenScroll),
			typeof(AgilityScroll),			typeof(CunningScroll),					typeof(CureScroll),						typeof(HarmScroll),
			typeof(MagicTrapScroll),		typeof(MagicUnTrapScroll),				typeof(ProtectionScroll),				typeof(StrengthScroll),
			typeof(BlessScroll),			typeof(FireballScroll),					typeof(MagicLockScroll),				typeof(PoisonScroll),
			typeof(TelekinisisScroll),		typeof(TeleportScroll),					typeof(UnlockScroll),					typeof(WallOfStoneScroll),
			typeof(ArchCureScroll),		typeof(ArchProtectionScroll),			typeof(CurseScroll),					typeof(FireFieldScroll),
			typeof(GreaterHealScroll),		typeof(LightningScroll),				typeof(ManaDrainScroll),				typeof(RecallScroll),
			typeof(BladeSpiritsScroll),	typeof(DispelFieldScroll),				typeof(IncognitoScroll),				typeof(MagicReflectScroll),
			typeof(MindBlastScroll),		typeof(ParalyzeScroll),					typeof(PoisonFieldScroll),				typeof(SummonCreatureScroll),
			typeof(DispelScroll),			typeof(EnergyBoltScroll),				typeof(ExplosionScroll),				typeof(InvisibilityScroll),
			typeof(MarkScroll),				typeof(MassCurseScroll),				typeof(ParalyzeFieldScroll),			typeof(RevealScroll),
			typeof(ChainLightningScroll),	typeof(EnergyFieldScroll),				typeof(FlamestrikeScroll),				typeof(GateTravelScroll),
			typeof(ManaVampireScroll),		typeof(MassDispelScroll),				typeof(MeteorSwarmScroll),				typeof(PolymorphScroll),
			typeof(EarthquakeScroll),		typeof(EnergyVortexScroll),			typeof(ResurrectionScroll),				typeof(SummonAirElementalScroll),
			typeof(SummonDaemonScroll),	typeof(SummonEarthElementalScroll),	typeof(SummonFireElementalScroll),	typeof(SummonWaterElementalScroll),
			typeof(AnimateDeadScroll),		typeof(BloodOathScroll),				typeof(CorpseSkinScroll),				typeof(CurseWeaponScroll),
			typeof(EvilOmenScroll),		typeof(HorrificBeastScroll),			typeof(LichFormScroll),				typeof(MindRotScroll),
			typeof(PainSpikeScroll),		typeof(PoisonStrikeScroll),			typeof(StrangleScroll),					typeof(SummonFamiliarScroll),
			typeof(VampiricEmbraceScroll),	typeof(VengefulSpiritScroll),			typeof(WitherScroll),					typeof(WraithFormScroll)

		};
    }

    public class ResourceStorageKeyScribersTomeGump : Gump
    {
        private Mobile m_From;
        private ResourceStorageKeyScribersTome st_Tome;

        public ResourceStorageKeyScribersTomeGump(Mobile from, ResourceStorageKeyScribersTome tome)
            : base(25, 25)
        {
            if (tome.GlobalEntry == null)
                tome.InitArray();

            m_From = from;
            st_Tome = tome;

            from.CloseGump(typeof(ResourceStorageKeyScribersTomeGump));

            AddPage(0);

            AddBackground(0, 0, 790, 470, 3000);
            AddPage(0);
            AddLabel(310, 7, 25, "Scribers Tome");

            AddButton(5, 10, 2462, 2461, 1, GumpButtonType.Reply, 0);
            AddLabel(70, 7, 25, "Add individual scroll");
            AddButton(730, 10, 2462, 2461, 2, GumpButtonType.Reply, 0);
            AddLabel(520, 7, 25, "Collect all scrolls from backpack");
            AddButton(300, 440, 0xFBE, 0xFBF, 3, GumpButtonType.Reply, 0);
            AddLabel(350, 437, 25, "Auto withdraw and fill a spellbook");

            int x = 5, y = 0;
            for (int i = 0; i < 80; i++)
            {
                switch (i)
                {
                    case 20: x = 175; y = 0; break;
                    case 40: x = 355; y = 0; break;
                    case 60: x = 560; y = 0; break;
                }
                AddButton(x, 28 + y * 20, 2443, 2444, i + 100, GumpButtonType.Reply, 0);
                AddHtml(x + 10, 30 + y * 20, 70, 20, ((i % 2 == 0) ? "<basefont color=#f8f8f8>" : "<basefont color=#0000ff>") + ((ScrollEntry)tome.GlobalEntry[i]).Amount.ToString() + "</basefont>", false, false);
                AddHtml(x + 70, 30 + y * 20, 155, 20, "<basefont color=#0000ff>" + ResourceStorageKeyScribersTome.ScrollsNames[i] + "</basefont>", false, false);
                y++;
            }
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            if (st_Tome.Deleted || st_Tome.GlobalEntry == null)
                return;
            else if (!st_Tome.IsChildOf(m_From.Backpack) && !st_Tome.IsChildOf(m_From.BankBox))
            {
                m_From.SendMessage("This must be in your backpack or bankbox to use.");
                return;
            }
            if (info.ButtonID == 1 )
            {
                st_Tome.BeginCombine(m_From);
                m_From.SendGump(new ResourceStorageKeyScribersTomeGump(m_From, st_Tome));
            }
            else if (info.ButtonID == 2)
            {
                st_Tome.CollectFromBackpack(m_From, true);
                m_From.SendGump(new ResourceStorageKeyScribersTomeGump(m_From, st_Tome));
            }
            else if (info.ButtonID == 3)
            {
                st_Tome.BeginFillSpellBook(m_From);
                m_From.SendGump(new ResourceStorageKeyScribersTomeGump(m_From, st_Tome));
            }
            else if (info.ButtonID >= 100 && info.ButtonID < 180)
            {
                int amount = ((ScrollEntry)st_Tome.GlobalEntry[info.ButtonID - 100]).Amount;
                if (amount == 0)
                    st_Tome.BeginCombine(m_From);
                else
                {
                    if ((int)amount / 100 >= 1)
                        amount = 100;
                    else if ((int)amount / 10 >= 1)
                        amount = 10;
                    ((ScrollEntry)st_Tome.GlobalEntry[info.ButtonID - 100]).Amount -= amount;

                    try
                    {
                        Item scrolls = (Item)Activator.CreateInstance(ResourceStorageKeyScribersTome.ScrollsTypes[info.ButtonID - 100], new object[] { amount });
                        m_From.AddToBackpack(scrolls);
                    }
                    catch
                    {
                        m_From.SendMessage("Something is wrong with the tome. Contact a staff please.");
                    }
                }
                m_From.SendGump(new ResourceStorageKeyScribersTomeGump(m_From, st_Tome));
            }
        }
    }

    public class ResourceStorageKeyScribersTomeTarget : Target
    {
        private ResourceStorageKeyScribersTome st_Tome;

        public ResourceStorageKeyScribersTomeTarget(ResourceStorageKeyScribersTome house)
            : base(18, false, TargetFlags.None)
        {
            st_Tome = house;
        }

        protected override void OnTarget(Mobile from, object targeted)
        {
            if (st_Tome.Deleted)
                return;

            st_Tome.EndCombine(from, targeted);
        }
    }

    public class FillSpellBookTarget : Target
    {
        private ResourceStorageKeyScribersTome st_Tome;

        public FillSpellBookTarget(ResourceStorageKeyScribersTome house)
            : base(18, false, TargetFlags.None)
        {
            st_Tome = house;
        }

        protected override void OnTarget(Mobile from, object targeted)
        {
            if (st_Tome.Deleted)
                return;

            st_Tome.EndFill(from, targeted);
        }
    }

    public class ScrollEntry
    {
        private int i_SpellID, i_SItemID, i_Amount;
        private Type i_Type;

        public int SpellID { get { return i_SpellID; } set { i_SpellID = value; } }
        public int SItemID { get { return i_SItemID; } set { i_SItemID = value; } }
        public int Amount { get { return i_Amount; } set { i_Amount = value; } }
        public Type Type { get { return i_Type; } set { i_Type = value; } }

        public ScrollEntry(int spellid, int sitemid, int amount, Type type)
        {
            i_SpellID = spellid;
            i_SItemID = sitemid;
            i_Amount = amount;
            i_Type = type;
        }
    }
}
