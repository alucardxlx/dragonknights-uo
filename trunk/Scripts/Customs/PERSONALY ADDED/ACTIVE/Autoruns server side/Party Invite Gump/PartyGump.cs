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

            AddBackground(94, 80, 321, 304, 2600);
            AddImage(157, 138, 666);

            AddLabel(209, 94, 38, "PARTY INVITE");
            AddLabel(187, 120, 0, String.Format("{0}", m_Leader.Name));
            AddLabel(153, 141, 0, "is asking you to join their party.");
            AddLabel(156, 162, 0, "Please Click Accept Or Decline.");

            AddButton(116, 223, 11400, 11402, 1, GumpButtonType.Reply, 0);

            AddButton(321, 223, 11410, 11412, 2, GumpButtonType.Reply, 0);

            AddLabel(137, 220, 0, "Accept");

            AddLabel(341, 220, 0, "Decline");


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