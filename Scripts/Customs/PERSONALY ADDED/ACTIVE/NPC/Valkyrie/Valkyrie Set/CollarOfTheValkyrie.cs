/*Created by Hammerhand*/

using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class CollarOfTheValkyrie : BaseArmor
    {
        public override int InitMinHits { get { return 100; } }
        public override int InitMaxHits { get { return 255; } }
        public override int ArtifactRarity { get { return 66; } }

        public override bool AllowMaleWearer { get { return false; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Plate; } }

        [Constructable]
        public CollarOfTheValkyrie()
            : base(0x1413)
        {
            
            Hue = 1153;
            Name = "Collar Of The Valkyrie";
            Weight = 1.0;

            Attributes.AttackChance = Utility.RandomMinMax(5, 15);
            Attributes.BonusInt = Utility.RandomMinMax(2, 7);
            Attributes.BonusStam = Utility.RandomMinMax(2, 9);
            Attributes.BonusStr = Utility.RandomMinMax(4, 10);
            Attributes.NightSight = 1;
            Attributes.RegenMana = Utility.RandomMinMax(2, 5);
            Attributes.SpellDamage = Utility.RandomMinMax(10, 25);

            ColdBonus = Utility.RandomMinMax(1, 10);
            PoisonBonus = Utility.RandomMinMax(1, 10);


        }

        public CollarOfTheValkyrie(Serial serial)
            : base(serial)
        {
        }
             
        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );
            writer.Write( (int) 0 );
        }
              
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize( reader );
            int version = reader.ReadInt();
        }
    }
}
