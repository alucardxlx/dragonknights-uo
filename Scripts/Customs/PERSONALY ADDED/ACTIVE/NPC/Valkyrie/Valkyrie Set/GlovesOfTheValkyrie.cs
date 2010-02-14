/*Created by Hammerhand*/

using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class GlovesOfTheValkyrie : LeatherGloves
    {
        public override int InitMinHits { get { return 100; } }
        public override int InitMaxHits { get { return 255; } }
        public override int ArtifactRarity { get { return 65; } }

        public override bool AllowMaleWearer { get { return false; } }

        [Constructable]
        public GlovesOfTheValkyrie()
        {
            
            Hue = 1153;
            Name = "Gloves Of The Valkyrie";
            Weight = 2.0;

            Attributes.BonusDex = Utility.RandomMinMax(1, 5);
            Attributes.BonusMana = Utility.RandomMinMax(2, 7);
            Attributes.BonusStam = Utility.RandomMinMax(2, 15);
            Attributes.CastRecovery = Utility.RandomMinMax(2, 5);
            Attributes.CastSpeed = Utility.RandomMinMax(4, 9);
            Attributes.NightSight = 1;
            Attributes.ReflectPhysical = Utility.RandomMinMax(10, 25);
            Attributes.RegenHits = Utility.RandomMinMax(5, 12);
            Attributes.WeaponDamage = Utility.RandomMinMax(14, 26);

            FireBonus = Utility.RandomMinMax(1, 10);
            PoisonBonus = Utility.RandomMinMax(1, 10);

        }

        public GlovesOfTheValkyrie(Serial serial)
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
