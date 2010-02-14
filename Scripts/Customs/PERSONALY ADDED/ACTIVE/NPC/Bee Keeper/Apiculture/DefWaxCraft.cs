using System;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Engines.Craft
{
    public class DefWaxCraft : CraftSystem
    {
        public override SkillName MainSkill
        {
            get { return SkillName.Tinkering; }
        }

        public override string GumpTitleString
        {
            get { return "<BASEFONT COLOR=#FFFFFF><CENTER>WAX CRAFT MENU</CENTER></BASEFONT>"; }
        }

        private static CraftSystem m_CraftSystem;

        public static CraftSystem CraftSystem
        {
            get
            {
                if (m_CraftSystem == null)
                    m_CraftSystem = new DefWaxCraft();

                return m_CraftSystem;
            }
        }

        public override double GetChanceAtMin(CraftItem item)
        {
            return 0.0;
        }

        private DefWaxCraft()
            : base(1, 1, 1.25)
        {
        }

        public static bool CheckWaxPot(Mobile from)
        {
            if (from.Backpack.FindItemByType(typeof(apiLargeWaxPot)) == null)
                return false;
            else
                return true;
        }

        public override int CanCraft(Mobile from, BaseTool tool, Type itemType)
        {
            if (tool == null || tool.Deleted || tool.UsesRemaining < 0)
                return 1044038; // You have worn out your tool!
            else if (!BaseTool.CheckAccessible(tool, from))
                return 1044263; // The tool must be on your person to use.

            return 0;
        }

        public override void PlayCraftEffect(Mobile from)
        {
        }

        public override int PlayEndingEffect(Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item)
        {
            if (toolBroken)
                from.SendLocalizedMessage(1044038); // You have worn out your tool

            if (failed)
            {
                if (lostMaterial)
                    return 1044043; // You failed to create the item, and some of your materials are lost.
                else
                    return 1044157; // You failed to create the item, but no materials were lost.
            }
            else
            {
                return 1044154; // You create the item.
            }
        }

        public override void InitCraftList()
        {
            int index = -1;

            //Candles
            AddCraft(typeof(Candle), "Candles", "Candle", 40.0, 60.0, typeof(Beeswax), "Beeswax", 3);
            AddCraft(typeof(CandleShort), "Candles", "Short Candle", 40.0, 60.0, typeof(Beeswax), "Beeswax", 4);
            AddCraft(typeof(CandleLong), "Candles", "Large Candle", 40.0, 60.0, typeof(Beeswax), "Beeswax", 6);
            AddCraft(typeof(CandleLarge), "Candles", "Long Candle", 80.0, 100.0, typeof(Beeswax), "Beeswax", 8);
            index = AddCraft(typeof(CandleSkull), "Candles", "Skull Candle", 100.0, 120.0, typeof(Beeswax), "Beeswax", 12);
            AddRes(index, typeof(BoneHelm), "Bone Helmet", 1);
        }
    }
}
