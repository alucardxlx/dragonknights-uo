using System;
using Server;
using Server.Network;
using Server.Commands;
using Server.Items;
using Server.Engines.PartySystem;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Server.Mobiles;

namespace Server.Gumps
{
    public class PartyGump : Gump
    {
        private Mobile m_Target, m_Leader;

        public PartyGump(Mobile leader, Mobile target)
            : base(0, 0)
        {
            m_Leader = leader;
            m_Target = target;


            Closable = true;
            Disposable = true;
            Dragable = true;
            Resizable = false;

            AddPage(0);

            AddBackground(111, 93, 280, 226, 9400);

            AddTextEntry(206, 97, 75, 20, 0, 0, @"Party Invite");
            AddTextEntry(116, 137, 258, 20, 0, 0, String.Format("{0} is asking you to join their party.", m_Leader.Name));
            AddTextEntry(146, 159, 190, 20, 0, 0, @"Please Click Accept Or Decline");

            AddButton(116, 224, 11400, 11402, 1, GumpButtonType.Reply, 0);

            AddButton(302, 224, 11410, 11412, 2, GumpButtonType.Reply, 0);

            AddTextEntry(140, 220, 47, 20, 0, 0, @"Accept");

            AddTextEntry(327, 220, 50, 20, 0, 0, @"Decline");

            AddImage(218, 254, 5608);
            AddImage(215, 221, 9804);

        }

        public override void OnResponse(NetState state, RelayInfo info)
        {
            if (m_Leader == null || m_Target == null)
                return;
            //Crash Prevention

            switch (info.ButtonID)
            {
                case 1:
                    {
                        PartyCommands.Handler.OnAccept(m_Target, m_Leader);
                        break;
                    }
                case 2:
                    {
                        PartyCommands.Handler.OnDecline(m_Target, m_Leader);
                        break;
                    }
            }
        }
    }
}
