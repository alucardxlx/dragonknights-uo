/*Created by Hammerhand*/

using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class SkirtOfTheValkyrie : LeatherSkirt
    {
        public override int InitMinHits { get { return 100; } }
        public override int InitMaxHits { get { return 255; } }
        public override int ArtifactRarity { get { return 61; } }

        public override bool AllowMaleWearer { get { return false; } }

        [Constructable]
        public SkirtOfTheValkyrie()
        {
            
            Hue = 1153;
            Name = "Skirt Of The Valkyrie";
            Weight = 2.0;

            Attributes.AttackChance = Utility.RandomMinMax(10, 25);
            Attributes.BonusDex = Utility.RandomMinMax(1, 5);
            Attributes.BonusHits = Utility.RandomMinMax(2, 5);
            Attributes.BonusInt = Utility.RandomMinMax(1, 4);
            Attributes.DefendChance = Utility.RandomMinMax(7, 15);
            Attributes.Luck = Utility.RandomMinMax(47, 105);
            Attributes.RegenStam = 5;

            FireBonus = Utility.RandomMinMax(1, 10);
            PhysicalBonus = Utility.RandomMinMax(1, 10);

        }

        public SkirtOfTheValkyrie(Serial serial)
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