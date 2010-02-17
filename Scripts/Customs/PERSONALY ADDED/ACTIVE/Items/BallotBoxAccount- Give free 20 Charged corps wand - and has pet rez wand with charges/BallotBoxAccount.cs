using System;
using System.Collections;
using Server;
using Server.Network;
using Server.Gumps;
using Server.Prompts;
using System.Collections.Generic;
using Server.Accounting;
using Server.Commands;
using Server.Targeting;

namespace Server.Items
{
    public class BallotBoxAccountDummy : AddonComponent
    {
        public static BallotBoxAccount CBBCurrent;
        private BallotBoxAccount m_CBB; // central ballot box
        public BallotBoxAccount CBB
        {
            get { return m_CBB; }
            set { m_CBB = value; }
        }

        public BallotBoxAccountDummy()
            : base(0x9A8)
        {
            Name = "DragonKnights Shard Ballot Box";
            if (CBBCurrent != null && !CBBCurrent.Deleted)
            {
                m_CBB = CBBCurrent;
                if (m_CBB.Open)
                    Name = "DragonKnights Shard Ballot Box (open)";
                else
                    Name = "DragonKnights Shard Ballot Box (closed)";
            }
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            list.Add("voter receives a small reward");

//            if (m_CBB == null || m_CBB.Deleted)
//                return;
//            if (m_CBB.RemainingTime == TimeSpan.Zero)
//                m_CBB.Open = false;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (m_CBB == null || m_CBB.Deleted)
                return;
            m_CBB.OnDoubleClick(from);
        }

        public BallotBoxAccountDummy(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
            writer.WriteItem<BallotBoxAccount>(m_CBB);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
            m_CBB = reader.ReadItem<BallotBoxAccount>();
            if (m_CBB != null)
                CBBCurrent = m_CBB;
        }
    }

    public class BallotBoxAccount : AddonComponent
    {

        public static void Initialize()
        {
            CommandSystem.Register("CBB", AccessLevel.Administrator, new CommandEventHandler(CBB_OnCommand));
        }

        [Usage("CBB")]
        [Description("Sets the current shard wide central ballot box.")]
        private static void CBB_OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendMessage("Please target a ballot box to be used as the current shard wide central ballot box.");
            e.Mobile.Target = new InternalTarget();
        }

        public class InternalTarget : Target
        {
            public InternalTarget()
                : base(10, false, TargetFlags.None)
            {
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is BallotBoxAccount)
                {
                    BallotBoxAccountDummy.CBBCurrent = (BallotBoxAccount)o;
                    from.SendMessage("That shard ballot box is now used as the current shard ballot box!");
                    from.SendMessage("Any newly created shard ballot box will now be linked to this one.");
                }
                else
                    from.SendMessage("That is not a shard ballot box!");
            }
        }

        public static readonly int MaxTopicLines = 6;
        public static readonly int MaxChoices = 5;
        public static readonly int DefaultBallotOpenDays = 7;
        public static readonly int RewardGoldForVote = 5000; // 5k gold per vote

        //public override int LabelNumber { get { return 1041006; } } // a ballot box

        private string[] m_Topic;
        private string[] m_Choices;
        private Dictionary<IAccount, int> m_Votes;
        private Dictionary<string, int> m_VotesByUsername;
        private bool m_Open;
        private DateTime m_OpenDate;
        private int m_OpenDays;

        public string[] Topic
        {
            get { return m_Topic; }
        }

        public string[] Choices
        {
            get { return m_Choices; }
        }

        public Dictionary<IAccount, int> Votes
        {
            get { return m_Votes; }
            set { m_Votes = value; }
        }

        public Dictionary<string, int> VotesByUsername
        {
            get { return m_VotesByUsername; }
            set { m_VotesByUsername = value; }
        }

        public bool Open
        {
            get { return m_Open; }
            set
            {
                m_Open = value;
                if (m_Open)
                {
                    Name = "DragonKnights Shard Ballot Box (open)";
                    foreach (Item item in World.Items.Values)
                        if (item is BallotBoxAccountDummy)
                        {
                            BallotBoxAccountDummy bb = (BallotBoxAccountDummy)item;
                            if (bb.CBB == this)
                                bb.Name = "DragonKnights Shard Ballot Box (open)";
                        }
                }
                else
                {
                    Name = "DragonKnights Shard Ballot Box (closed)";
                    foreach (Item item in World.Items.Values)
                        if (item is BallotBoxAccountDummy)
                        {
                            BallotBoxAccountDummy bb = (BallotBoxAccountDummy)item;
                            if (bb.CBB == this)
                                bb.Name = "DragonKnights Shard Ballot Box (closed)";
                        }
                }
            }
        }

        public int OpenDays
        {
            get { return m_OpenDays; }
            set { m_OpenDays = value; }
        }

        public DateTime OpenDate
        {
            get { return m_OpenDate; }
        }

        public TimeSpan RemainingTime
        {
            get
            {
                DateTime closeTime = m_OpenDate + TimeSpan.FromDays(m_OpenDays);
                if (closeTime < DateTime.Now)
                    return TimeSpan.Zero;
                else
                    return closeTime - DateTime.Now;
            }
        }

        [Constructable]
        public BallotBoxAccount()
            : base(0x9A8)
        {
            Name = "DragonKnights Shard Ballot Box (closed)";
            Hue = 1150;
            m_Topic = new string[0];
            m_Choices = new string[0];
            m_Votes = new Dictionary<IAccount, int>();
            m_VotesByUsername = new Dictionary<string, int>();
            m_Open = false;
            m_OpenDays = DefaultBallotOpenDays;
        }

        public override void GetProperties(ObjectPropertyList list)
        {
//            if (RemainingTime == TimeSpan.Zero)
//                Open = false;

            base.GetProperties(list);
            list.Add("voter receives a small reward");
        }

        public BallotBoxAccount(Serial serial)
            : base(serial)
        {
        }

        public void ClearTopic()
        {
            m_Topic = new string[0];

            ClearVotes();
        }

        public void ClearChoices()
        {
            m_Choices = new string[0];

            ClearVotes();
        }

        public void AddLineToTopic(string line)
        {
            if (m_Topic.Length >= MaxTopicLines)
                return;

            string[] newTopic = new string[m_Topic.Length + 1];
            m_Topic.CopyTo(newTopic, 0);
            newTopic[m_Topic.Length] = line;

            m_Topic = newTopic;

            ClearVotes();
        }

        public void AddLineToChoices(string line)
        {
            if (m_Choices.Length >= MaxChoices)
                return;

            string[] newChoices = new string[m_Choices.Length + 1];
            m_Choices.CopyTo(newChoices, 0);
            newChoices[m_Choices.Length] = line;

            m_Choices = newChoices;

            ClearVotes();
        }

        public void ClearVotes()
        {
            m_Votes.Clear();
        }

        public void ToggleOpen()
        {
            if (Open)
                Open = false;
            else
            {
                Open = true;
                m_OpenDate = DateTime.Now;
            }
        }

        public bool IsOwner(Mobile from)
        {
            if (from.AccessLevel >= AccessLevel.Administrator)
                return true;

            return false;
        }

        public bool HasVoted(Mobile from)
        {
            IAccount ac = from.Account;
            return (m_Votes.ContainsKey(ac));
        }

        public override bool OnDragDrop(Mobile from, Item dropped)
        {
            SendLocalizedMessageTo(from, 500369); // I'm a ballot box, not a container!
            return false;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (m_VotesByUsername != null)
            {
                foreach (string username in m_VotesByUsername.Keys)
                {
                    IAccount ac = Accounts.GetAccount(username);
                    if (ac!=null)
                        m_Votes.Add(ac, m_VotesByUsername[username]);
                }
                m_VotesByUsername.Clear();
                m_VotesByUsername = null;
            }

            if (Open && RemainingTime == TimeSpan.Zero)
                Open = false;

            bool isOwner = IsOwner(from);
            from.SendGump(new InternalGump(this, isOwner));
        }

        private class InternalGump : Gump
        {
            private BallotBoxAccount m_Box;

            public InternalGump(BallotBoxAccount box, bool isOwner)
                : base(110, 70)
            {
                m_Box = box;

                int lineCount = box.Topic.Length;
                int choicesCount = box.Choices.Length;

                AddBackground(0, 0, 400, 350 + 100, 0xA28);

                if (isOwner)
                    AddHtmlLocalized(0, 15, 400, 35, 1011000, false, false); // <center>Ballot Box Owner's Menu</center>
                else
                    AddHtml(0, 15, 400, 35, "<center>DragonKnights Ballot Box - 1 vote per account</center>", false, false);
                    //AddHtmlLocalized(0, 15, 400, 35, 1011001, false, false); // <center>Ballot Box -- Vote Here!</center>

                if (m_Box.Open)
                {
                    TimeSpan remaining = m_Box.RemainingTime;
                    AddHtml(0, 35, 400, 35, "<center>This poll is open for another " + remaining.Days + " days, " + remaining.Hours + " hours and " + remaining.Minutes + " minutes.", false, false);
                }
                else
                    AddHtml(0, 35, 400, 35, "<center>This poll is closed.", false, false);

                AddHtmlLocalized(0, 65, 400, 35, 1011002, false, false); // <center>Topic</center>

                AddBackground(25, 90, 350, Math.Max(20 * (lineCount), 20), 0x1400);

                for (int i = 0; i < lineCount; i++)
                {
                    string line = box.Topic[i];

                    if (!String.IsNullOrEmpty(line))
                        AddLabelCropped(30, 90 + i * 20, 340, 20, 0x3E3, line);
                }

                AddHtmlLocalized(0, 215, 400, 35, 1011003, false, false); // <center>votes</center>

                for (int i = 0; i < choicesCount; i++)
                {
                    string choice = box.Choices[i];
                    int votes = 0;
                    foreach(int vote in box.Votes.Values)
                    {
                        if (vote == i)
                            votes ++;
                    }
                    if (!String.IsNullOrEmpty(choice))
                    {
                        if (!isOwner && m_Box.Open)
                            AddButton(20, 240 + i* 25, 0xFA5, 0xFA7, 6 + i, GumpButtonType.Reply, 0);
                        AddLabel(85, 242 + i * 25, 0x3E3, choice);
                        AddLabel(55, 242 + i * 25, 0x0, String.Format("[{0}]", votes));
                    }
                }

                AddButton(45, 305 + 75, 0xFA5, 0xFA7, 0, GumpButtonType.Reply, 0);
                AddHtmlLocalized(80, 308 + 75, 40, 35, 1011008, false, false); // done

                if (isOwner)
                {
                    AddButton(120, 305 + 75, 0xFA5, 0xFA7, 1, GumpButtonType.Reply, 0);
                    AddHtmlLocalized(155, 308 + 75, 100, 35, 1011006, false, false); // change topic

                    AddButton(240, 330 + 75, 0xFA5, 0xFA7, 2, GumpButtonType.Reply, 0);
                    AddHtmlLocalized(275, 333 + 75, 300, 100, 1011007, false, false); // reset votes

                    AddButton(240, 305 + 75, 0xFA5, 0xFA7, 3, GumpButtonType.Reply, 0);
                    AddHtml(275, 308 + 75, 100, 35, "change choices", false, false);

                    AddButton(120, 330 + 75, 0xFA5, 0xFA7, 4, GumpButtonType.Reply, 0);
                    AddHtml(155, 333 + 75, 100, 35, "change days", false, false);

                    AddButton(45, 330 + 75, 0xFA5, 0xFA7, 5, GumpButtonType.Reply, 0);
                    if (m_Box.Open)
                        AddHtml(80, 333 + 75, 100, 35, "close", false, false);
                    else
                        AddHtml(80, 333 + 75, 100, 35, "open", false, false);
                }
            }

            public override void OnResponse(NetState sender, RelayInfo info)
            {
                if (m_Box.Deleted || info.ButtonID == 0)
                    return;

                Mobile from = sender.Mobile;

                bool isOwner = m_Box.IsOwner(from);

                if (info.ButtonID >= 6 && info.ButtonID <= 6 + MaxChoices)
                {
                    int choice = info.ButtonID - 6;
                    if (!isOwner)
                    {
                        if (m_Box.HasVoted(from))
                        {
                            from.SendLocalizedMessage(500374); // You have already voted on this ballot.
                        }
                        else
                        {
                            m_Box.Votes.Add(from.Account, choice);
                            from.SendLocalizedMessage(500373); // Your vote has been registered.
                            from.AddToBackpack(new CorpseRetrievalWand(5));
                            from.SendMessage("Thank you for providing your feedback. Your feedback is very important to us, and allows us to further improve the shard to better serve you and other players.");
                            from.SendMessage("You received a small gift as a token of appreciation.");
                        }
                        from.SendGump(new InternalGump(m_Box, isOwner));
                    }

                }
                else
                {

                    switch (info.ButtonID)
                    {
                        case 1: // change topic
                            {
                                if (isOwner)
                                {
                                    m_Box.ClearTopic();

                                    from.SendLocalizedMessage(500370, "", 0x35); // Enter a line of text for your ballot, and hit ENTER. Hit ESC after the last line is entered.
                                    from.Prompt = new TopicPrompt(m_Box, false);
                                }

                                break;
                            }
                        case 2: // reset votes
                            {
                                if (isOwner)
                                {
                                    m_Box.ClearVotes();
                                    from.SendLocalizedMessage(500371); // Votes zeroed out.
                                }

                                goto default;
                            }
                        case 3: // change choices
                            {
                                if (isOwner)
                                {
                                    m_Box.ClearChoices();

                                    from.SendLocalizedMessage(500370, "", 0x35); // Enter a line of text for your ballot, and hit ENTER. Hit ESC after the last line is entered.
                                    from.Prompt = new TopicPrompt(m_Box, true);
                                }

                                break;
                            }
                        case 4: // change days
                            {
                                if (isOwner)
                                {
                                    from.SendMessage(0x35, "Enter how many days in total for your ballot to run, and hit ENTER.");
                                    from.Prompt = new BallotDaysPrompt(m_Box);
                                }

                                break;
                            }
                        case 5: // toggle open/close
                            {
                                if (isOwner)
                                    m_Box.ToggleOpen();

                                goto default;
                            }
                        default:
                            {
                                from.SendGump(new InternalGump(m_Box, isOwner));
                                break;
                            }
                    }
                }
            }
        }

        private class TopicPrompt : Prompt
        {
            private BallotBoxAccount m_Box;
            private bool m_IsChoice;

            public TopicPrompt(BallotBoxAccount box, bool isChoice)
            {
                m_Box = box;
                m_IsChoice = isChoice;
            }

            public override void OnResponse(Mobile from, string text)
            {
                if (m_Box.Deleted || !m_Box.IsOwner(from))
                    return;

                if (m_IsChoice)
                {
                    m_Box.AddLineToChoices(text.TrimEnd());
                    if (m_Box.Choices.Length < MaxChoices)
                    {
                        from.SendLocalizedMessage(500377, "", 0x35); // Next line or ESC to finish:
                        from.Prompt = new TopicPrompt(m_Box, true);
                    }
                    else
                    {
                        from.SendLocalizedMessage(500376, "", 0x35); // Ballot entry complete.
                        from.SendGump(new InternalGump(m_Box, true));
                    }
                }
                else
                {
                    m_Box.AddLineToTopic(text.TrimEnd());
                    if (m_Box.Topic.Length < MaxTopicLines)
                    {
                        from.SendLocalizedMessage(500377, "", 0x35); // Next line or ESC to finish:
                        from.Prompt = new TopicPrompt(m_Box, false);
                    }
                    else
                    {
                        from.SendLocalizedMessage(500376, "", 0x35); // Ballot entry complete.
                        from.SendGump(new InternalGump(m_Box, true));
                    }
                }

            }

            public override void OnCancel(Mobile from)
            {
                if (m_Box.Deleted || !m_Box.IsOwner(from))
                    return;

                from.SendLocalizedMessage(500376, "", 0x35); // Ballot entry complete.
                from.SendGump(new InternalGump(m_Box, true));
            }
        }

        private class BallotDaysPrompt : Prompt
        {
            private BallotBoxAccount m_Box;

            public BallotDaysPrompt(BallotBoxAccount box)
            {
                m_Box = box;
            }

            public override void OnResponse(Mobile from, string text)
            {
                if (m_Box.Deleted || !m_Box.IsOwner(from))
                    return;

                int days = Utility.GetInt32(text, DefaultBallotOpenDays);
                if (days < 1 || days > 10)
                    days = DefaultBallotOpenDays;

                m_Box.OpenDays = days;

                from.SendMessage(0x35, "The poll will now run for " + days.ToString() + " days");
                from.SendGump(new InternalGump(m_Box, true));

            }

            public override void OnCancel(Mobile from)
            {
                if (m_Box.Deleted || !m_Box.IsOwner(from))
                    return;

                from.SendLocalizedMessage(500376, "", 0x35); // Ballot entry complete.
                from.SendGump(new InternalGump(m_Box, true));
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version

            writer.WriteEncodedInt(m_Topic.Length);

            for (int i = 0; i < m_Topic.Length; i++)
                writer.Write((string)m_Topic[i]);

            writer.WriteEncodedInt(m_Choices.Length);

            for (int i = 0; i < m_Choices.Length; i++)
                writer.Write((string)m_Choices[i]);

            writer.Write(m_Votes.Count);
            foreach(IAccount key in m_Votes.Keys)
            {
                writer.Write(key.Username);
                writer.Write(m_Votes[key]);
            }

            writer.Write(m_OpenDays);
            writer.Write(m_OpenDate);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();

            m_Topic = new string[reader.ReadEncodedInt()];

            for (int i = 0; i < m_Topic.Length; i++)
                m_Topic[i] = reader.ReadString();

            m_Choices = new string[reader.ReadEncodedInt()];

            for (int i = 0; i < m_Choices.Length; i++)
                m_Choices[i] = reader.ReadString();

            m_Votes = new Dictionary<IAccount,int>();
            m_VotesByUsername = new Dictionary<string, int>();
            int numVotes = reader.ReadInt();
            for (int i = 0; i<numVotes; i++)
            {
                string acName = reader.ReadString();
                int vote = reader.ReadInt();
//                IAccount ac = Accounts.GetAccount(acName);
//                if (ac!=null)
                    m_VotesByUsername.Add(acName, vote);
            }

            m_OpenDays = reader.ReadInt();
            m_OpenDate = reader.ReadDateTime();
            if (RemainingTime == TimeSpan.Zero)
                m_Open = false;
            else
                m_Open = true;
        }
    }

    public class BallotBoxAccountDummyAddon : BaseAddon
    {
        [Constructable]
        public BallotBoxAccountDummyAddon()
        {
            AddComponent(new BallotBoxAccountDummy(), 0, 0, 7);
            AddComponent(new AddonComponent(11596), 0, 0, 0);
        }

        public BallotBoxAccountDummyAddon(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt(0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();
        }
    }
}
