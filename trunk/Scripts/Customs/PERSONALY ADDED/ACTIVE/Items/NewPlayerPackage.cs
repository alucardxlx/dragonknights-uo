using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class NewPlayerPackage : WoodenBox
    {
        [Constructable]
        public NewPlayerPackage()
        {
            Name = "New Player Welcome Package";
            Hue = 1281;
            LootType = LootType.Newbied;
            

            /*
            Spellbook book1 = new Spellbook();
            book1.Content = ulong.MaxValue;
            DropItem(book1);
            book1.Location = new Point3D(16, 83, 0);

            BookOfChivalry book2 = new BookOfChivalry();
            book2.Content = 1023;//all spells
            DropItem(book2);
            book2.Location = new Point3D(33, 83, 0);

            NecromancerSpellbook book3 = new NecromancerSpellbook();
            book3.Content = 0x1FFFF;
            DropItem(book3);
            book3.Location = new Point3D(49, 83, 0);
            */ 

            BaseArmor armor = new LeatherChest();
            armor.Hue = 537;
            armor.LootType = LootType.Newbied;
            armor.ArmorAttributes.DurabilityBonus = 20;
            armor.Attributes.LowerManaCost = 2;
            armor.Attributes.LowerRegCost = 5;
            armor.Attributes.Luck = 10;
            armor.Attributes.BonusMana = 3;
            armor.Attributes.RegenMana = 1;
            armor.ColdBonus = 1;
            armor.EnergyBonus = 1;
            armor.FireBonus = 1;
            //armor.ArmorRatingBonus = 2;
            armor.PoisonBonus = 1;
            DropItem(armor);
            armor.Location = new Point3D(61, 74, 0);

            armor = new LeatherLegs();
            armor.Hue = 537;
            armor.LootType = LootType.Newbied;
            armor.ArmorAttributes.DurabilityBonus = 20;
            armor.Attributes.RegenHits = 1;
            armor.Attributes.BonusHits = 3;
            armor.Attributes.LowerManaCost = 2;
            armor.Attributes.LowerRegCost = 5;
            armor.Attributes.Luck = 10;
            armor.ColdBonus = 1;
            armor.EnergyBonus = 1;
            armor.FireBonus = 1;
            //armor.ArmorRatingBonus = 2;
            armor.PoisonBonus = 1;
            DropItem(armor);
            armor.Location = new Point3D(71, 78, 0);

            armor = new LeatherArms();
            armor.Hue = 537;
            armor.LootType = LootType.Newbied;
            armor.ArmorAttributes.DurabilityBonus = 20;
            armor.Attributes.LowerManaCost = 2;
            armor.Attributes.LowerRegCost = 5;
            armor.Attributes.Luck = 10;
            armor.Attributes.BonusStam = 3;
            armor.Attributes.RegenStam = 1;
            armor.ColdBonus = 1;
            armor.EnergyBonus = 1;
            armor.FireBonus = 1;
            //armor.ArmorRatingBonus = 2;
            armor.PoisonBonus = 1;
            DropItem(armor);
            armor.Location = new Point3D(94, 84, 0);

            armor = new LeatherGloves();
            armor.Hue = 537;
            armor.LootType = LootType.Newbied;
            armor.ArmorAttributes.DurabilityBonus = 20;
            armor.Attributes.LowerManaCost = 2;
            armor.Attributes.LowerRegCost = 5;
            armor.Attributes.Luck = 10;
            armor.Attributes.BonusStr = 2;
            armor.ColdBonus = 1;
            armor.EnergyBonus = 1;
            armor.FireBonus = 1;
            //armor.ArmorRatingBonus = 2;
            armor.PoisonBonus = 1;
            DropItem(armor);
            armor.Location = new Point3D(108, 85, 0);

            armor = new LeatherGorget();
            armor.Hue = 537;
            armor.LootType = LootType.Newbied;
            armor.ArmorAttributes.DurabilityBonus = 20;
            armor.Attributes.LowerManaCost = 2;
            armor.Attributes.LowerRegCost = 5;
            armor.Attributes.BonusInt = 2;
            armor.Attributes.Luck = 10;
            armor.ColdBonus = 1;
            armor.EnergyBonus = 1;
            armor.FireBonus = 1;
            //armor.ArmorRatingBonus = 2;
            armor.PoisonBonus = 1;
            DropItem(armor);
            armor.Location = new Point3D(122, 79, 0);

            armor = new LeatherCap();
            armor.Hue = 537;
            armor.LootType = LootType.Newbied;
            armor.ArmorAttributes.DurabilityBonus = 20;
            armor.Attributes.BonusDex = 2;
            armor.Attributes.LowerManaCost = 2;
            armor.Attributes.LowerRegCost = 5;
            armor.Attributes.Luck = 10;
            armor.ColdBonus = 1;
            armor.EnergyBonus = 1;
            armor.FireBonus = 1;
            //armor.ArmorRatingBonus = 2;
            armor.PoisonBonus = 1;
            DropItem(armor);
            armor.Location = new Point3D(115, 88, 0);

            armor = new MetalShield();
            armor.Hue = 537;
            armor.LootType = LootType.Newbied;
            armor.ArmorAttributes.DurabilityBonus = 20;
            armor.Attributes.AttackChance = 5;
            armor.Attributes.DefendChance = 5;
            armor.Attributes.SpellChanneling = 1;
            armor.Attributes.Luck = 10;
            armor.ColdBonus = 1;
            armor.EnergyBonus = 1;
            armor.FireBonus = 1;
            //armor.ArmorRatingBonus = 2;
            armor.PoisonBonus = 1;
            DropItem(armor);
            armor.Location = new Point3D(132, 85, 0);

            BaseJewel ring = new GoldRing();
            ring.LootType = LootType.Newbied;
            ring.Attributes.NightSight = 1;
            ring.Attributes.SpellDamage = 5;
            ring.Attributes.WeaponDamage = 5;
            DropItem(ring);
            ring.Location = new Point3D(140, 60, 0);

            Item item = new Bandage(100);
            DropItem(item);
            item.Location = new Point3D(135, 66, 0);
        }

        public NewPlayerPackage(Serial serial)
            : base(serial)
        {
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
