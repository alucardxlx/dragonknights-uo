// Rocko's AOE Dragon.

using System;
using System.Collections;
using Server;
using Server.Mobiles;
using Server.Misc;
using Server.Spells;

namespace Server.Items
{
    // AOE field items
    [DispellableField]
    public class AOEUnit : Item
    {
        private Timer m_Timer;
        private DateTime m_End;
        private BaseCreature m_Caster;

        public override bool BlocksFit { get { return true; } }

        public AOEUnit(int itemID, Point3D loc, BaseCreature caster, Map map, TimeSpan duration, int val)
            : base(itemID)
        {
            bool canFit = SpellHelper.AdjustField(ref loc, map, 12, false);

            Visible = false;
            Movable = false;
            Light = LightType.Circle300;
            Hue = caster.AOEBreathEffectHue;
            Name = caster.AOEBreathName;

            MoveToWorld(loc, map);

            m_Caster = caster;

            m_End = DateTime.Now + duration;

            m_Timer = new InternalTimer(this, TimeSpan.FromSeconds(Math.Abs(val) * 0.2), caster.InLOS(this), canFit);
            m_Timer.Start();
        }

        public override void OnAfterDelete()
        {
            base.OnAfterDelete();

            if (m_Timer != null)
                m_Timer.Stop();
        }

        public AOEUnit(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); // version

            writer.Write(m_Caster);
            writer.WriteDeltaTime(m_End);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 1:
                    {
                        m_Caster = (BaseCreature)reader.ReadMobile();

                        goto case 0;
                    }
                case 0:
                    {
                        m_End = reader.ReadDeltaTime();

                        m_Timer = new InternalTimer(this, TimeSpan.Zero, true, true);
                        m_Timer.Start();

                        break;
                    }
            }
        }

        public static bool ValidTarget(BaseCreature caster, Mobile m)
        {
            bool validTarget = false;

            if (m.AccessLevel > AccessLevel.Player) // can not hurt staff
                validTarget = false;
            else if (caster == null) // nobody cast the AOE, it hurts everybody
                validTarget = true;
            // player can not use this AOE
            /*
        else if (m_Caster.Player && m_Caster == m) // player can use AOE to hurt themselves under any condition
            validTarget = true;
             */
            else if (Core.AOS && caster == m) // NPC never hurt themselves with AOE in AOS context
                validTarget = false;
            else if (!caster.Controlled && !caster.Summoned)
            {   // potion thrown by a wild creature (the most typical case for now)
                if (m is BaseCreature)
                {
                    BaseCreature target = (BaseCreature)m;
                    if (target.Blessed)
                        validTarget = false; // obvious
                    else if (!target.Controlled && !target.Summoned)
                    {   // wild NPC can not hurt another wild NPC of same alliance (good can not hurt good, bad can not hurt bad, but can hurt across)
                        if (target.InitialInnocent == caster.InitialInnocent)
                            validTarget = false;
                        else
                            validTarget = true;
                    }
                    else if (!caster.InitialInnocent && ((target.Controlled && target.ControlMaster != null && target.ControlMaster.Player && !target.IsDeadPet) || (target.Summoned && target.SummonMaster != null && target.SummonMaster.Player)))
                        validTarget = true; // wild non-good NPC can always hurt player controlled/summoned NPC
                }
                else if (m.Player && !caster.InitialInnocent && !(m.AccessLevel > AccessLevel.Player && m.Hidden))
                    validTarget = true; // wild NPC of non-good alliance can always hurt a player, given it is not a hidden GM
            }
            else
                validTarget = (SpellHelper.ValidIndirectTarget(caster, m) && caster.CanBeHarmful(m, false));

            return validTarget;

        }

        public override bool OnMoveOver(Mobile m)
        {
            if (Visible && ValidTarget(m_Caster, m))
            {
                m_Caster.DoHarmful(m);

                m_Caster.BreathDealDamage(m);

                m_Caster.AOESpecialEffect(m);

                /*
                int damage = Utility.Random(8) + 3;

                if (!Core.AOS && m.CheckSkill(SkillName.MagicResist, 0.0, 30.0))
                {
                    damage = 1;

                    m.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
                }

                AOS.Damage(m, m_Caster, damage, 0, 0, 100, 0, 0);

                */
                m.PlaySound(0x15);
            }

            return true;
        }

        private class InternalTimer : Timer
        {
            private AOEUnit m_Item;
            private bool m_InLOS, m_CanFit;

            private static Queue m_Queue = new Queue();

            public InternalTimer(AOEUnit item, TimeSpan delay, bool inLOS, bool canFit)
                : base(delay, TimeSpan.FromSeconds(1.0))
            {
                m_Item = item;
                m_InLOS = inLOS;
                m_CanFit = canFit;

                Priority = TimerPriority.FiftyMS;
            }

            protected override void OnTick()
            {
                if (m_Item.Deleted)
                    return;

                if (!m_Item.Visible)
                {
                    if (m_InLOS && m_CanFit)
                        m_Item.Visible = true;
                    else
                        m_Item.Delete();

                    if (!m_Item.Deleted)
                    {
                        m_Item.ProcessDelta();
                        Effects.SendLocationParticles(EffectItem.Create(m_Item.Location, m_Item.Map, EffectItem.DefaultDuration), m_Item.ItemID, 9, 10, 5029);
                    }
                }
                else if (DateTime.Now > m_Item.m_End)
                {
                    m_Item.Delete();
                    Stop();
                }
                else
                {
                    Map map = m_Item.Map;
                    BaseCreature caster = m_Item.m_Caster;

                    if (map != null && caster != null)
                    {
                        foreach (Mobile m in m_Item.GetMobilesInRange(0))
                        {
                            if ((m.Z + 16) > m_Item.Z && (m_Item.Z + 12) > m.Z && ValidTarget(caster, m))
                                m_Queue.Enqueue(m);
                        }

                        while (m_Queue.Count > 0)
                        {
                            Mobile m = (Mobile)m_Queue.Dequeue();

                            caster.DoHarmful(m);

                            caster.BreathDealDamage(m);

                            caster.AOESpecialEffect(m);
                            /*
                            int damage = Utility.Random(8) + 3;

                            if (!Core.AOS && m.CheckSkill(SkillName.MagicResist, 0.0, 30.0))
                            {
                                damage = 1;

                                m.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
                            }

                            AOS.Damage(m, caster, damage, 0, 0, 100, 0, 0);
                             */
                            m.PlaySound(0x15);
                        }
                    }
                }
            }
        }
    }
}