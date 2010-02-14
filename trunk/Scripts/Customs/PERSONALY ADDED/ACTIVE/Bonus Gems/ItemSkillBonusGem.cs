using System;
using Server;
using Server.Targeting;

namespace Server.Items
{
    //Declares the ItemSkillBonusGem class and inherits the Item class.
    public sealed class ItemSkillBonusGem : Item
    {
        //Default 'uses' total: 1
        private int _Charges = 1;
        /// <summary>
        /// Gets or Sets the total amount of charges for this Gem.
        /// </summary>
        [CommandProperty(AccessLevel.GameMaster)]
        public int Charges { get { return _Charges; } set { _Charges = value; InvalidateProperties(); } }

        //Default 'useless' skill: Focus
        private SkillName _Skill = SkillName.Focus;
        /// <summary>
        /// Gets or Sets the skill (to apply bonus to) for this Gem.
        /// </summary>
        [CommandProperty(AccessLevel.GameMaster)]
        public SkillName Skill { get { return _Skill; } set { _Skill = value; InvalidateProperties(); } }

        //Default skill bonus: 1.0
        private double _Value = 1.0;
        /// <summary>
        /// Gets or Sets the skill bonus value for this Gem.
        /// </summary>
        [CommandProperty(AccessLevel.GameMaster)]
        public double Value { get { return _Value; } set { _Value = value; InvalidateProperties(); } }

        [Constructable]
        public ItemSkillBonusGem()
            : this(1, SkillName.Focus, 1.0)
        { }

        [Constructable]
        public ItemSkillBonusGem(SkillName skill)
            : this(1, skill, 1.0)
        { }

        [Constructable]
        public ItemSkillBonusGem(SkillName skill, double value)
            : this(1, skill, value)
        { }

        [Constructable]
        public ItemSkillBonusGem(int charges)
            : this(charges, SkillName.Focus, 1.0)
        { }

        [Constructable]
        public ItemSkillBonusGem(int charges, SkillName skill)
            : this(charges, skill, 1.0)
        { }

        [Constructable]
        public ItemSkillBonusGem(int charges, SkillName skill, double value)
            : base(0x1870)
        {
            //Set our custom variables...
            _Charges = charges;
            _Skill = skill;
            _Value = value;

            Stackable = false;
            Weight = 0.1;
            Hue = 0;
            Name = "Item Enhancement Gem";
        }

        public void Use()
        {
            _Charges--;

            if (_Charges <= 0)
                Delete();
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            string desc = String.Format("Charges: {0:#,#}", _Charges) + "\n" + String.Format("<BASEFONT COLOR=#00FF00>Use: Increases the {0} bonus of an item by {1:f1}<BASEFONT COLOR=#FFFFFF>", _Skill.ToString(), _Value);

            list.Add(1070722, desc); // ~1_NOTHING~
        }

        public ItemSkillBonusGem(Serial serial)
            : base(serial)
        { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt((int)0); // version

            writer.Write((int)_Charges);
            writer.Write((int)_Skill);
            writer.Write((double)_Value);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();

            _Charges = reader.ReadInt();
            _Skill = (SkillName)reader.ReadInt();
            _Value = reader.ReadDouble();
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
                return;
            }

            from.Target = new InternalTarget(this);
            from.SendMessage(0x55, "Target an item of clothing, armor, jewelry or a weapon...");
        }
    }

    public class InternalTarget : Target
    {
        private ItemSkillBonusGem _Gem;

        public InternalTarget(ItemSkillBonusGem gem)
            : base(-1, false, TargetFlags.None)
        {
            _Gem = gem;
            AllowNonlocal = true;
        }

        protected override void OnTarget(Mobile from, object target)
        {
            //Is our target an Item?
            if (target is Item)
            {
                //Yes: So convert target to Item...
                Item item = target as Item;

                //Is our Item inside the Player's backpack?
                if (!item.IsChildOf(from.Backpack))
                {
                    //No: Send warning message and exit (return) gracefully...
                    from.SendMessage(0x22, "You must own that item before you can enhance it.");
                    return;
                }

                //Is the target Skill of the layer adequate?
                if (from.CheckTargetSkill(SkillName.ItemID, item, 0, 100))
                {
                    //Yes: Identify the Item Type and set the SkillBonus values...
                    if (item is BaseWeapon)
                    {
                        //Is the Player a Blacksmith? (We are enhancing a weapon...)
                        if (from.Skills.Blacksmith.Value < 100.0)
                        {
                            //No: Send warning and exit (return) gracefully.
                            from.SendMessage(0x22, "You don't have the required Blacksmithing skill to enhance this weapon.");
                            return;
                        }

                        //Success: Identified Item Type and set SkillBonus values, send a message, play a random sound, etc.
                        ((BaseWeapon)item).SkillBonuses.SetValues(0, _Gem.Skill, _Gem.Value);
                        from.SendMessage(0x55, "You Successfully enhance the weapon.");
                        from.PlaySound(Utility.Random(0x520, 0));
                        Server.Engines.Craft.DefBlacksmithy.CraftSystem.PlayCraftEffect(from);
                        _Gem.Use();
                    }
                    else if (item is BaseArmor)
                    {
                        //Is the Player a Blacksmith? (We are enhancing armor...)
                        if (from.Skills.Blacksmith.Value < 100.0)
                        {
                            //No: Send warning and exit (return) gracefully.
                            from.SendMessage(0x22, "You don't have the required Blacksmithing skill to enhance this armor.");
                            return;
                        }

                        //Success: Identified Item Type and set SkillBonus values, send a message, play a random sound, etc.
                        ((BaseArmor)item).SkillBonuses.SetValues(0, _Gem.Skill, _Gem.Value);
                        from.SendMessage(0x55, "You Successfully enhance the clothing.");
                        from.PlaySound(Utility.Random(0x520, 0));
                        Server.Engines.Craft.DefBlacksmithy.CraftSystem.PlayCraftEffect(from);
                        _Gem.Use();
                    }
                    else if (item is BaseJewel)
                    {
                        //Is the Player a, Inscriptor? (We are enhancing jewels...)
                        if (from.Skills.Inscribe.Value < 100.0)
                        {
                            //No: Send warning and exit (return) gracefully.
                            from.SendMessage(0x22, "You don't have the required Inscription skill to enhance this jewel.");
                            return;
                        }

                        //Success: Identified Item Type and set SkillBonus values, send a message, play a random sound, etc.
                        ((BaseJewel)item).SkillBonuses.SetValues(0, _Gem.Skill, _Gem.Value);
                        from.SendMessage(0x55, "You successfully enhance the jewel.");
                        from.PlaySound(Utility.Random(0x520, 0));
                        Server.Engines.Craft.DefInscription.CraftSystem.PlayCraftEffect(from);
                        _Gem.Use();
                    }
                    else if (item is BaseClothing)
                    {
                        //Is the Player a Tailor? (We are enhancing clothing...)
                        if (from.Skills.Tailoring.Value < 100.0)
                        {
                            //No: Send warning and exit (return) gracefully.
                            from.SendMessage(0x22, "You don't have the required Tailoring skill to enhance this clothing.");
                            return;
                        }

                        //Success: Identified Item Type and set SkillBonus values, send a message, play a random sound, etc.
                        ((BaseClothing)item).SkillBonuses.SetValues(0, _Gem.Skill, _Gem.Value);
                        from.SendMessage(0x55, "You successfully enhance the clothing.");
                        from.PlaySound(Utility.Random(0x520, 0));
                        Server.Engines.Craft.DefTailoring.CraftSystem.PlayCraftEffect(from);
                        _Gem.Use();
                    }
                    else
                    {
                        //Fail: Could not identify the Item Type, send warning message and exit (return) gracefully...
                        from.SendMessage(0x22, "That item can not be enhanced.");
                        return;
                    }
                }
                else
                {
                    //No: Send warning message and exit (return) gracefully.
                    from.SendMessage(0x22, "You have no idea about what you're trying to do, do you?");
                    return;
                }
            }
            else
            {
                //No: Send warning message and exit (return) gracefully.
                from.SendMessage(0x22, "You must target an item of clothing, armor, jewelry or a weapon.");
                return;
            }
        }
    }
}  
