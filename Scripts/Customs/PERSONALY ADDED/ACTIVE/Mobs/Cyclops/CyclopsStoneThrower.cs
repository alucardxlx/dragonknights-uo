//You can simply modify the script of a cyclop. The custom mob is named "Cyclop stone thrower". The custom behavior is the following:
//The cyclop will throw a large piece of bolder towards the target. The animation is slower than an explosion potion.
//You can script it so the bolder shatters into a few pieces upon landing to make it look cool, though doesn't have to.
//All players and pets within say 3 tiles radius will take damage. For a player, it would take on the artwork of falling down,
//and be frozen for a second or two.

using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Targeting;
using Server.Misc;
using Server.Spells;

namespace Server.Mobiles
{
    [CorpseName("a cyclops stone thrower corpse")]
    public class CyclopsStoner : BaseCreature
    {
        private bool m_Stunning;

        [Constructable]
        public CyclopsStoner()
            : base(AIType.AI_Archer, FightMode.Weakest, 10, 1, 0.2, 0.4)
        {
            Name = "a cyclops stone thrower";
            Body = 75;
            BaseSoundID = 604;

            SetStr(800);
            SetDex(300, 375);
            SetInt(75, 94);

            SetHits(500);

            SetDamage(25);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 45, 50);
            SetResistance(ResistanceType.Fire, 30, 40);
            SetResistance(ResistanceType.Cold, 25, 35);
            SetResistance(ResistanceType.Poison, 30, 40);
            SetResistance(ResistanceType.Energy, 30, 40);

            SetSkill(SkillName.MagicResist, 60.3, 105.0);
            SetSkill(SkillName.Tactics, 80.1, 100.0);
            SetSkill(SkillName.Wrestling, 80.1, 90.0);
            SetSkill(SkillName.Archery, 81.0, 101.0);
            SetSkill(SkillName.Anatomy, 81.0, 101.0);

            Fame = 10000;
            Karma = -10000;

            VirtualArmor = 50;

//            AddItem(new Bow());
//            PackItem(new Arrow(Utility.RandomMinMax(10, 20)));

            if (Utility.RandomDouble() < 0.01)
                PackItem(new Item(Utility.RandomList(4963, 4964, 4965, 4966, 4967, 4968, 4969, 4970, 4971, 4972, 4973)));
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
            AddLoot(LootPack.FilthyRich);
        }

        public override int Meat { get { return 4; } }
        public override int TreasureMapLevel { get { return 3; } }

        private DateTime m_NextStone;
        private int m_Thrown;

        public override void OnActionCombat()
        {
            Mobile combatant = Combatant;

            if (combatant == null || combatant.Deleted || combatant.Map != Map || !InRange(combatant, 12) || !CanBeHarmful(combatant) || !InLOS(combatant))
                return;

            if (DateTime.Now >= m_NextStone)
            {
                ThrowStone(combatant);

                m_Thrown++;

                if (0.75 >= Utility.RandomDouble() && (m_Thrown % 2) == 1) // 75% chance to quickly throw another bomb
                    m_NextStone = DateTime.Now + TimeSpan.FromSeconds(3.0);
                else
                    m_NextStone = DateTime.Now + TimeSpan.FromSeconds(5.0 + (10.0 * Utility.RandomDouble())); // 5-15 seconds
            }
        }

        public void ThrowStone(Mobile m)
        {
            if (m == null || m.Deleted || m.Map != Map || !InRange(m, 12) || !CanBeHarmful(m) || !InLOS(m))
                return;

            DoHarmful(m);

            Point3D loc = m.Location;
            Map map = m.Map;

            Direction = GetDirectionTo(m);
            MovingEffect(new Entity(Serial.Zero, loc, map), 4962, 4, 1, true, false, 0, 0);

            Timer.DelayCall(TimeSpan.FromSeconds(1.5), new TimerStateCallback(Stone_OnTick), new object[] { this, loc, map });
        }

        private class StoneItem : Item
        {
            public StoneItem(int itemID) : base(itemID)
            {
                Movable = false;
                Timer.DelayCall(TimeSpan.FromSeconds(5.0), new TimerCallback(SelfDestruct));
            }

            public StoneItem()
                : this(Utility.RandomList(4963, 4964, 4965, 4966, 4967, 4968, 4969, 4970, 4971, 4972, 4973))
            {
            }

            private void SelfDestruct()
            {
                Delete();
            }

            public StoneItem( Serial serial ) : base( serial )
			{
			}

			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );

				writer.Write( (int) 0 ); // version
			}

            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);

                int version = reader.ReadInt();

                Delete();
            }

        }

        private void Stone_OnTick(object state)
        {
            object[] states = (object[])state;
            Mobile from = (Mobile)states[0];
            Point3D loc = (Point3D)states[1];
            Map map = (Map)states[2];

            if (from == null || from.Deleted)
                return;

            Item stoneCenter = new StoneItem(Utility.RandomList(4963, 4967, 4970, 4973));
            stoneCenter.MoveToWorld(loc, map);

            Effects.SendLocationEffect(loc, map, 0x3728, 20);

            int stoneCount = 0;
            for (int i = -3; i <= 3; ++i)
                for (int j = -3; j <= 3; ++j)
                {
                    if (i == 0 && j == 0) continue;
                    if (Utility.Random(49) < 4 && stoneCount < 5)
                    {
                        Point3D loc1 = new Point3D(loc.X + i, loc.Y + j, loc.Z);

                        bool canFit = SpellHelper.AdjustField(ref loc1, map, 12, false);

                        if (canFit)
                        {
                            new StoneItem(Utility.RandomList(4964, 4965, 4966, 4968, 4969, 4971, 4972)).MoveToWorld(loc1, map);
                            stoneCount++;
                        }
                    }
                }

            List<Mobile> list = new List<Mobile>();

            foreach (Mobile m in stoneCenter.GetMobilesInRange(15))
            {
                if (m == from || !from.CanBeHarmful(m))
                    continue;
                if (m.AccessLevel > AccessLevel.Player)
                    continue;
                if (m is BaseCreature && (((BaseCreature)m).Controlled || ((BaseCreature)m).Summoned || ((BaseCreature)m).Team != this.Team))
                    list.Add(m);
                if (m.Player)
                    list.Add(m);

            }

            foreach (Mobile m in list)
            {
                m.SendMessage("The cyclops hurls a boulder, stunning you!");
                m.PlaySound(0x2F3); // sound effect from earthquake spell
                from.DoHarmful(m);
                AOS.Damage(m, from, 50, 100, 0, 0, 0, 0);
                m.Animate(21, 6, 1, true, false, 0);
                Timer.DelayCall(TimeSpan.FromSeconds(0.7), new TimerStateCallback(OnGround_CallBack), m);

                if (m.Alive)
                {
                    m.Frozen = true;
                    m.Combatant = null;
                    Timer.DelayCall(TimeSpan.FromSeconds(5.0), new TimerStateCallback(Recover_Callback), m);
                }
            }
        }

        private void OnGround_CallBack(object state)
        {
            Mobile defender = state as Mobile;
            defender.Animate(0, 6, 22, false, false, 200);
        }

        private void Recover_Callback(object state)
        {
            Mobile defender = state as Mobile;

            if (defender != null)
            {
                defender.Frozen = false;
                defender.Say(true,"*Recovered*");
            }

            m_Stunning = false;
        }

        public CyclopsStoner(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
