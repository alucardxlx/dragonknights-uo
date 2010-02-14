/*Created by Hammerhand*/

using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class ChestplateOfTheValkyrie : FemalePlateChest
    {
        public override int InitMinHits { get { return 100; } }
        public override int InitMaxHits { get { return 255; } }

        public override int ArtifactRarity { get { return 68; } }

        public override bool AllowMaleWearer { get { return false; } }

        [Constructable]
        public ChestplateOfTheValkyrie()
        {
            
            Hue = 1153;
            Name = "Chestplate Of The Valkyrie";
            Weight = 8.0;

            Attributes.AttackChance = Utility.RandomMinMax(8, 21);
            Attributes.BonusInt = Utility.RandomMinMax(10, 25);
            Attributes.BonusMana = Utility.RandomMinMax(3, 15);
            Attributes.CastRecovery = Utility.RandomMinMax(2, 7);
            Attributes.CastSpeed = Utility.RandomMinMax(3, 9);
            Attributes.DefendChance = Utility.RandomMinMax(8, 15);
            Attributes.Luck = Utility.RandomMinMax(100, 250);
            Attributes.NightSight = 1;
            Attributes.ReflectPhysical = Utility.RandomMinMax(8, 16);
            Attributes.RegenHits = Utility.RandomMinMax(8, 20);
            Attributes.WeaponDamage = Utility.RandomMinMax(10, 25);

            PhysicalBonus = Utility.RandomMinMax(1, 10);
            PoisonBonus = Utility.RandomMinMax(1, 10);

        }

        public ChestplateOfTheValkyrie(Serial serial)
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
