/* Created by Hammerhand*/

using System;
using Server;
using Server.Items;

namespace Server.Items
{
    [FlipableAttribute(0x2B71, 0x3168)]
    public class HelmOfTheValkyrie : BaseArmor
    {
        public override int InitMinHits { get { return 100; } }
        public override int InitMaxHits { get { return 255; } }
        public override int ArtifactRarity { get { return 64; } }

        public override bool AllowMaleWearer { get { return false; } }
        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Plate; } }

        [Constructable]
        public HelmOfTheValkyrie()
            : base(0x2B71)
        {
            
            Hue = 1153;
            Name = "Helm Of The Valkyrie";
            Weight = 3.0;

            Attributes.BonusStr = Utility.RandomMinMax(5, 9);
            Attributes.BonusInt = Utility.RandomMinMax(4, 9);
            Attributes.BonusStam = Utility.RandomMinMax(3, 7);
            Attributes.Luck = Utility.RandomMinMax(80, 47);
            Attributes.ReflectPhysical = Utility.RandomMinMax(10, 25);

            ColdBonus = Utility.RandomMinMax(1, 10);
            PoisonBonus = Utility.RandomMinMax(1, 10);

        }

        public HelmOfTheValkyrie(Serial serial)
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
