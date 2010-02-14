using System;
using Server.Network;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Items
{
    public class StatSkillHelper
    {
        public static double m_PurchaseSkillCap = 90.0;

        public static int GoldForGainOneTenth(double currentSkill)
        {
            return Math.Max(1, (int)(currentSkill * Math.Pow(2, currentSkill/20.0)/2));
        }

        public static void IncreaseSkillByOneTenth(Mobile from, int skillID)
        {
            Skill m_Skill = from.Skills[skillID];

            if (m_Skill == null)
                return;

            if (m_Skill.Base >= m_PurchaseSkillCap)
            {
                from.SendMessage("You can not buy that skill any further.");
                return;
            }

            int count = 0;
            for (int i = 0; i < from.Skills.Length; ++i)
                count += from.Skills[i].BaseFixedPoint;

            int skillToLower = -1;
            bool reachCap = false;
            if (count >= from.SkillsCap)
            {
                reachCap = true;
                skillToLower = SkillToLower(from, skillID);
                if (skillToLower < 0)
                {
                    from.SendMessage("You have reached the total skill cap, You need to a skill arrow down first.");
                    return;
                }
            }

            int fee = GoldForGainOneTenth(m_Skill.Base);
            if (!Banker.Withdraw(from, fee))
            {
                from.SendMessage("You need in your bankbox " + fee.ToString() + " gp to raise " + m_Skill.SkillName.ToString() + " by 0.1 point from " + m_Skill.Base.ToString() + ".");
                return;
            }
            if (reachCap)
                from.Skills[skillToLower].BaseFixedPoint -= 1;

            m_Skill.BaseFixedPoint += 1;

        }
        public static int SkillToLower(Mobile from, int excluded)
        {
            int number;
            if (Core.AOS)
                number = 0;
            else
                number = 3;

            int startI = 0;
            int endI = from.Skills.Length - number;

            for (int i = startI; i < endI; i++)
            {
                if (i== excluded)
                    continue;
                Skill skill = from.Skills[i];
                if (skill.Lock == SkillLock.Down && skill.Base > 0)
                    return i;
            }
            return -1; // didn't find any skill to lower
        }

        public static void IncreaseStrengthByOne(Mobile from)
        {
            if (from.RawStr >= 125)
            {
                from.SendMessage("You have reached the cap of strength.");
                return;
            }
            else if (from.StrLock == StatLockType.Locked || from.StrLock == StatLockType.Down)
            {
                from.SendMessage("You need to set your strength arrow up on the status gump before you can increase it.");
                return;
            }
            else if ((from.RawStr + from.RawDex + from.RawInt) >= from.StatCap)
            {
                if (from.DexLock != StatLockType.Down && from.IntLock != StatLockType.Down)
                {
                    from.SendMessage("You have reached the total stat cap. You need to set either dexterity or intelligence arrow down on the status gump first.");
                    return;
                }
                bool canLower = false;
                if (from.DexLock == StatLockType.Down && from.RawDex > 10)
                    canLower = true;
                if (from.IntLock == StatLockType.Down && from.RawInt > 10)
                    canLower = true;
                if (!canLower)
                {
                    from.SendMessage("You can not lower a stat to below 10.");
                    return;
                }
            }
            
            if (!Banker.Withdraw(from, 500))
            {
                from.SendMessage("You must have at least 500gp in your bank before you can buy one stat point.");
            }
            else
            {
                if ((from.RawStr + from.RawDex + from.RawInt) >= from.StatCap)
                {
                    if (from.DexLock == StatLockType.Down && from.RawDex > 10)
                        from.RawDex--;
                    else if (from.IntLock == StatLockType.Down && from.RawInt > 10)
                        from.RawInt--;
                }
                from.RawStr++;
                from.SendMessage("You raised your strength by 1 point.");
            }
        }
        public static void IncreaseDexterityByOne(Mobile from)
        {
            if (from.RawDex >= 125)
            {
                from.SendMessage("You have reached the cap of dexterity.");
                return;
            }
            else if (from.DexLock == StatLockType.Locked || from.DexLock == StatLockType.Down)
            {
                from.SendMessage("You need to set your dexterity arrow up on the status gump before you can increase it.");
                return;
            }
            else if ((from.RawStr + from.RawDex + from.RawInt) >= from.StatCap)
            {
                if (from.StrLock != StatLockType.Down && from.IntLock != StatLockType.Down)
                {
                    from.SendMessage("You have reached the total stat cap. You need to set either strength or intelligence arrow down on the status gump first.");
                    return;
                }
                bool canLower = false;
                if (from.StrLock == StatLockType.Down && from.RawStr > 10)
                    canLower = true;
                if (from.IntLock == StatLockType.Down && from.RawInt > 10)
                    canLower = true;
                if (!canLower)
                {
                    from.SendMessage("You can not lower a stat to below 10.");
                    return;
                }
            }
            
            if (!Banker.Withdraw(from, 500))
            {
                from.SendMessage("You must have at least 500gp in your bank before you can buy one stat point.");
            }
            else
            {
                if ((from.RawStr + from.RawDex + from.RawInt) >= from.StatCap)
                {
                    if (from.IntLock == StatLockType.Down && from.RawInt > 10)
                        from.RawInt--;
                    else if (from.StrLock == StatLockType.Down && from.RawStr > 10)
                        from.RawStr--;
                }
                from.RawDex++;
                from.SendMessage("You raised your dexterity by 1 point.");
            }
        }
        public static void IncreaseIntelligenceByOne(Mobile from)
        {
            if (from.RawInt >= 125)
            {
                from.SendMessage("You have reached the cap of intelligence.");
                return;
            }
            else if (from.IntLock == StatLockType.Locked || from.IntLock == StatLockType.Down)
            {
                from.SendMessage("You need to set your intelligence arrow up on the status gump before you can increase it.");
                return;
            }
            else if ((from.RawStr + from.RawDex + from.RawInt) >= from.StatCap)
            {
                if (from.DexLock != StatLockType.Down && from.StrLock != StatLockType.Down)
                {
                    from.SendMessage("You have reached the total stat cap. You need to set either strength or dexterity arrow down on the status gump first.");
                    return;
                }
                bool canLower = false;
                if (from.DexLock == StatLockType.Down && from.RawDex > 10)
                    canLower = true;
                if (from.StrLock == StatLockType.Down && from.RawStr > 10)
                    canLower = true;
                if (!canLower)
                {
                    from.SendMessage("You can not lower a stat to below 10.");
                    return;
                }
            }

            if (!Banker.Withdraw(from, 500))
            {
                from.SendMessage("You must have at least 500gp in your bank before you can buy one stat point.");
            }
            else
            {
                if ((from.RawStr + from.RawDex + from.RawInt) >= from.StatCap)
                {
                    if (from.StrLock == StatLockType.Down && from.RawStr > 10)
                        from.RawStr--;
                    else if (from.DexLock == StatLockType.Down && from.RawDex > 10)
                        from.RawDex--;
                }
                from.RawInt++;
                from.SendMessage("You raised your intelligence by 1 point.");
            }
        }
    }
    public class StatSkillPurchaseCrystal : Item
    {
        [Constructable]
        public StatSkillPurchaseCrystal()
            : base(7961)
        {
            LootType = LootType.Blessed;
            Name = "Stat/Skill Purchase Crystal";
        }

        public StatSkillPurchaseCrystal(Serial serial)
            : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            from.SendGump(new StatSkillPurchaseGump(from));
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
    }
}

namespace Server.Gumps
{
    public class StatSkillPurchaseGump : Gump
    {
        public StatSkillPurchaseGump(Mobile from)
            : base(20, 30)
        {
            from.CloseGump(typeof(StatSkillPurchaseGump));
            from.CloseGump(typeof(StatPurchaseGump));
            from.CloseGump(typeof(SkillPurchaseGump));

            AddPage(0);
            AddBackground(0, 0, 220, 170, 5054);
            AddBackground(10, 10, 200, 150, 3000);

            AddHtml(20, 20, 180, 80, "You pay with gold/bankcheck directly from your bankbox.", false, false);

            int i = 0;

            AddHtml(55, 75 + i * 25, 140, 25, "Buy Stats", false, false);
            AddButton(20, 75 + i * 25, 4005, 4007, 1, GumpButtonType.Reply, 0);

            i++;

            AddHtml(55, 75 + i * 25, 140, 25, "Buy Skills", false, false);
            AddButton(20, 75 + i * 25, 4005, 4007, 2, GumpButtonType.Reply, 0);

            AddHtml(55, 125, 140, 25, "Player Command Guide", false, false);
            AddButton(20, 125, 4005, 4007, 3, GumpButtonType.Reply, 0);

        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;

            switch (info.ButtonID)
            {
                case 1:
                    from.SendGump(new StatPurchaseGump(from));
                    break;
                case 2:
                    from.SendGump(new SkillPurchaseGump(from, 0));
                    break;
                case 3:
                    from.SendGump(new CommandManualGump());
                    break;
                default:
                    break;
            }
        }
    }
    

    public class StatPurchaseGump : Gump
    {
        public StatPurchaseGump(Mobile from)
            : base(20, 30)
        {
            from.CloseGump(typeof(StatSkillPurchaseGump));
            from.CloseGump(typeof(StatPurchaseGump));
            from.CloseGump(typeof(SkillPurchaseGump));

            AddPage(0);
            AddBackground(0, 0, 260, 351, 5054);

            AddImageTiled(10, 10, 240, 23, 0x52);
            AddImageTiled(11, 11, 238, 21, 0xBBC);

            AddLabel(65, 11, 0, "Buy stats - 500gp/point");

            int index = 0;

            AddImageTiled(10, 32 + (index * 22), 240, 23, 0x52);
            AddImageTiled(11, 33 + (index * 22), 238, 21, 0xBBC);

            AddLabelCropped(13, 33 + (index * 22), 150, 21, 0, "Strength");
            AddImageTiled(180, 34 + (index * 22), 50, 19, 0x52);
            AddImageTiled(181, 35 + (index * 22), 48, 17, 0xBBC);

            AddLabelCropped(182, 35 + (index * 22), 234, 21, 0, from.RawStr.ToString("F1"));
            if (from.RawStr < 125)
                AddButton(231, 35 + (index * 22), 0x15E0, 0x15E4, 1, GumpButtonType.Reply, 0);

            ++index;

            AddImageTiled(10, 32 + (index * 22), 240, 23, 0x52);
            AddImageTiled(11, 33 + (index * 22), 238, 21, 0xBBC);

            AddLabelCropped(13, 33 + (index * 22), 150, 21, 0, "Dexerity");
            AddImageTiled(180, 34 + (index * 22), 50, 19, 0x52);
            AddImageTiled(181, 35 + (index * 22), 48, 17, 0xBBC);

            AddLabelCropped(182, 35 + (index * 22), 234, 21, 0, from.RawDex.ToString("F1"));
            if (from.RawDex < 125)
                AddButton(231, 35 + (index * 22), 0x15E0, 0x15E4, 2, GumpButtonType.Reply, 0);

            ++index;

            AddImageTiled(10, 32 + (index * 22), 240, 23, 0x52);
            AddImageTiled(11, 33 + (index * 22), 238, 21, 0xBBC);

            AddLabelCropped(13, 33 + (index * 22), 150, 21, 0, "Intelligence");
            AddImageTiled(180, 34 + (index * 22), 50, 19, 0x52);
            AddImageTiled(181, 35 + (index * 22), 48, 17, 0xBBC);

            AddLabelCropped(182, 35 + (index * 22), 234, 21, 0, from.RawInt.ToString("F1"));
            if (from.RawInt < 125)
                AddButton(231, 35 + (index * 22), 0x15E0, 0x15E4, 3, GumpButtonType.Reply, 0);
        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;

            switch (info.ButtonID)
            {
                case 1:
                    StatSkillHelper.IncreaseStrengthByOne(from);
                    from.SendGump(new StatPurchaseGump(from));
                    break;
                case 2:
                    StatSkillHelper.IncreaseDexterityByOne(from);
                    from.SendGump(new StatPurchaseGump(from));
                    break;
                case 3:
                    StatSkillHelper.IncreaseIntelligenceByOne(from);
                    from.SendGump(new StatPurchaseGump(from));
                    break;
                default:
                    break;
            }
        }
    }

    public class SkillPurchaseGump : Gump
    {
        private int m_Page;

        private const int FieldsPerPage = 14;

        public SkillPurchaseGump(Mobile from, int page)
            : base(20, 30)
        {
            if (page < 0 || page > 3)
                return;

            from.CloseGump(typeof(StatSkillPurchaseGump));
            from.CloseGump(typeof(StatPurchaseGump));
            from.CloseGump(typeof(SkillPurchaseGump));

            m_Page = page;

            AddPage(0);
            AddBackground(0, 0, 460, 351, 5054);

            AddImageTiled(10, 10, 440, 23, 0x52);
            AddImageTiled(11, 11, 438, 21, 0xBBC);

            AddLabel(65, 11, 0, "Buy skills by 0.1 up to 90      Cost");

            Skills skills = from.Skills;

            int number;
            if (Core.AOS)
                number = 0;
            else
                number = 3;

            int startI = m_Page * FieldsPerPage;
            int endI = Math.Min((m_Page+1) * FieldsPerPage, (skills.Length - number));
            int index = 0;

            for (int i = startI; i < endI; ++i)
            {
                if (m_Page >0)
                    AddButton(413, 13, 0x15E3, 0x15E7, 200 + page -1, GumpButtonType.Reply, 0);

                if (m_Page < 3)
                    AddButton(431, 13, 0x15E1, 0x15E5, 200 + page +1, GumpButtonType.Reply, 0);

                Skill skill = skills[i];
                
                AddImageTiled(10, 32 + (index * 22), 440, 23, 0x52);
                AddImageTiled(11, 33 + (index * 22), 438, 21, 0xBBC);

                AddLabelCropped(13, 33 + (index * 22), 150, 21, 0, skill.Name);
                AddImageTiled(180, 34 + (index * 22), 50, 19, 0x52);
                AddImageTiled(181, 35 + (index * 22), 48, 17, 0xBBC);
                AddLabelCropped(182, 35 + (index * 22), 234, 21, 0, skill.Base.ToString("F1"));
                if (skill.Base < StatSkillHelper.m_PurchaseSkillCap)
                	{
                	AddButton(231, 35 + (index * 22), 0x15E0, 0x15E4, i + 1, GumpButtonType.Reply, 0);
                	int price = StatSkillHelper.GoldForGainOneTenth(skill.Base);
                	AddLabelCropped(270, 33 + (index * 22), 150, 21, 0, "0.1 (" + price.ToString() + " gp)");
                	}
                ++index;
                
          }
            
    }
              
                
                
                
                
                
//                if (skill.SkillName == SkillName.Ninjitsu || skill.SkillName == SkillName.Bushido)
//                {
//                    AddLabelCropped(182, 33 + (index * 22), 234, 21, 0, "(UNUSED)");
//                }
//                else if (skill.SkillName == SkillName.Fletching)
//                {
//                    AddLabelCropped(130, 33 + (index * 22), 234, 21, 0, "(Merged 2 Carpentry)");
//                }
//                else
//                {
//                    AddLabelCropped(182, 35 + (index * 22), 234, 21, 0, skill.Base.ToString("F1"));
//                    if (skill.Base < StatSkillHelper.m_PurchaseSkillCap)
//                    {
//                        AddButton(231, 35 + (index * 22), 0x15E0, 0x15E4, i + 1, GumpButtonType.Reply, 0);
//                        int price = StatSkillHelper.GoldForGainOneTenth(skill.Base);
//                        AddLabelCropped(270, 33 + (index * 22), 150, 21, 0, "0.1 (" + price.ToString() + " gp)");
//                    }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;

            // turning page
            if (info.ButtonID >= 200 && info.ButtonID <= 203)
            {
                from.SendGump(new SkillPurchaseGump(from, info.ButtonID - 200));
                return;
            }

            // buying a skill
            if (info.ButtonID > 0)
            {
                StatSkillHelper.IncreaseSkillByOneTenth(from, info.ButtonID-1);
                from.SendGump(new SkillPurchaseGump(from, m_Page));
            }
        }
    }
}
