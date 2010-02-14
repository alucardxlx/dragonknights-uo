/*

$Id: //depot/c%23/RunUO Core Scripts/RunUO Core Scripts/Customs/Items/BellOfSummoning.cs#1 $

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation; either version 2 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

*/

using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{

    public class BellOfSummoning : Item, IUsesRemaining
    {
        [Flags]
        private enum BellOfSummoningFlags
        {
            None				= 0x00000000,
            DeleteOnZeroCharges = 0x00000001,
            ShowUsesRemaining   = 0x00000002,
            Rechargable         = 0x00000004
        };

        private bool GetFlag(BellOfSummoningFlags flag)
        {
            return ((m_Flags & flag) != 0);
        }

        private void SetFlag(BellOfSummoningFlags flag, bool value)
        {
            if (value)
                m_Flags |= flag;
            else
                m_Flags &= ~flag;
        }

        private int m_Sound;
        private int m_UsesRemaining;
        private BellOfSummoningFlags m_Flags;
        private double m_DelaySeconds;
        private const double DefaultDelaySeconds = 10.0;

        [CommandProperty(AccessLevel.GameMaster)]
        public int Sound { get { return m_Sound; } set { m_Sound = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int UsesRemaining { get { return m_UsesRemaining; } set { m_UsesRemaining = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool DeleteOnZeroCharges
        {
            get { return GetFlag(BellOfSummoningFlags.DeleteOnZeroCharges); }
            set { SetFlag(BellOfSummoningFlags.DeleteOnZeroCharges, value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool ShowUsesRemaining
        {
            get { return GetFlag(BellOfSummoningFlags.ShowUsesRemaining); }
            set { SetFlag(BellOfSummoningFlags.ShowUsesRemaining, value); InvalidateProperties(); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Rechargable
        {
            get { return GetFlag(BellOfSummoningFlags.Rechargable); }
            set { SetFlag(BellOfSummoningFlags.Rechargable, value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public double DelaySeconds { get { return m_DelaySeconds; } set { m_DelaySeconds = value; } }

        public override void OnDoubleClick(Mobile from)
        {
            if (UsesRemaining == 0)
            {
                from.SendMessage(32, "This bell is out of charges.");
                return;
            }

            if (from != null && from is PlayerMobile)
            {
                PlayerMobile p = (PlayerMobile)from;

                if (p.Followers == 0)
                {
                    p.SendMessage(32, "You decide not to use the magic of the bell, since you have no pets right now!");
                    return;
                }

                new SummonTimer(this, p).Start();
            }
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            if (m_UsesRemaining != -1 && ShowUsesRemaining)
                list.Add(1060584, m_UsesRemaining.ToString()); // uses remaining: ~1_val~
        }
        
        [Constructable]
        public BellOfSummoning(int hue, int uses, int sound, double delay)
            : base(7186)
        {
            Name = "Bell Of Summoning";
            UsesRemaining = uses;
            Sound = sound;
            Hue = hue;
            DelaySeconds = delay;
            ShowUsesRemaining = true;
        }

        [Constructable]
        public BellOfSummoning()
            : this(Utility.RandomMinMax(500, 1000), 10, 1297, DefaultDelaySeconds)
        {
        }

        public BellOfSummoning(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);       // version

            writer.Write((int)m_Sound);
            writer.Write((int)m_UsesRemaining);
            writer.Write((int)m_Flags);
            writer.Write((double)m_DelaySeconds);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    m_Sound = reader.ReadInt();
                    m_UsesRemaining = reader.ReadInt();
                    m_Flags = (BellOfSummoningFlags)reader.ReadInt();
                    m_DelaySeconds = reader.ReadDouble();
                    break;
            }
        }

        private class SummonTimer : Timer
        {
            private PlayerMobile m_Player;
            private BellOfSummoning m_Bell;

            public SummonTimer(BellOfSummoning b, PlayerMobile p)
                : base(TimeSpan.FromSeconds(b.DelaySeconds))
            {
                m_Bell = b;
                m_Player = p;
                p.Frozen = true;
                p.FixedParticles(0x373A, 10, 15, 5018, EffectLayer.RightHand);
                p.PlaySound(b.Sound);
                p.NonlocalOverheadMessage(Server.Network.MessageType.Emote, 123, true, "*begins to ring a magical bell*");
                p.LocalOverheadMessage(Server.Network.MessageType.Emote, 123, true, "*You begin to ring the magical bell*");
                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                List<BaseCreature> pets = new List<BaseCreature>();
                m_Player.Frozen = false;

                foreach (Mobile m in World.Mobiles.Values)
                {
                    if (m is BaseCreature)
                    {
                        BaseCreature bc = (BaseCreature)m;

                        if ((bc.Controlled && bc.ControlMaster == m_Player) || (bc.Summoned && bc.SummonMaster == m_Player))
                            pets.Add(bc);
                    }
                }

                if (pets.Count > 0)
                {
                    for (int i = 0; i < pets.Count; i++)
                    {
                        if (pets[i] is IMount)
                        {
                            if (((IMount)pets[i]).Rider != null)
                                continue;
                        }
                        pets[i].MoveToWorld(m_Player.Location, m_Player.Map);
                        if (pets[i].Controlled)
                        {
                            pets[i].ControlTarget = m_Player;
                            pets[i].ControlOrder = OrderType.Follow;
                        }
                    }
                    m_Player.PlaySound(1480);
                    m_Player.LocalOverheadMessage(Server.Network.MessageType.Regular, 78, true, "Your pets have been summoned!");
                    if (m_Bell.UsesRemaining != -1)
                    {
                        m_Bell.UsesRemaining--;
                        if (m_Bell.UsesRemaining == 0 && m_Bell.DeleteOnZeroCharges)
                        {
                            m_Player.SendMessage(78, "Your bell crumbles to dust.");
                            m_Bell.Delete();
                        }
                    }
                }
                else
                {
                    m_Player.LocalOverheadMessage(Server.Network.MessageType.Regular, 32, true, "You have no pets to summon.");
                }
            }
        }
    }
}
