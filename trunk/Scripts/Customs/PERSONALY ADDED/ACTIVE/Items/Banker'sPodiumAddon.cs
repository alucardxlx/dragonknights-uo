using System;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Multis;
using Server.Gumps;

namespace Server.Items
{
    // Banker's Podium Addon
    public class BankersPodiumAddon : BaseAddon
    {
        public override bool ShareHue { get { return false; } }

        public override BaseAddonDeed Deed
        {
            get
            {
                return new BankersPodiumAddonDeed();
            }
        }

        [Constructable]
        public BankersPodiumAddon()
        {
            Visible = true;
            Name = "Banker's Podium Bell";

            AddonComponent ac = null;
            ac = new AddonComponent(6425);
            ac.Name = "Banker's Podium";
            AddComponent(ac, 0, 0, 0);

            ac = new BankersPodiumBell();            
            AddComponent(ac, 0, 0, 10);

            /*
            foreach (AddonComponent comp in Components)
            {
                comp.Name = "Ancient Tomb";
            }
             */ 
        }

        public BankersPodiumAddon(Serial serial)
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
            switch (version)
            {
                case 0:
                    break;
            }
        }
    }

    public class BankersPodiumAddonDeed : BaseAddonDeed
    {
        public override BaseAddon Addon
        {
            get
            {
                return new BankersPodiumAddon();
            }
        }

        [Constructable]
        public BankersPodiumAddonDeed()
        {
            Name = "Banker's Podium Bell Deed";
            Hue = 1281;
            
        }

        public BankersPodiumAddonDeed(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // Version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }


    public class BankersPodiumBell : AddonComponent, ISecurable
    {
        private static TimeSpan m_Delay = TimeSpan.Zero;

        private DateTime m_NextUse;

        [CommandProperty(AccessLevel.GameMaster)]
        public TimeSpan NextUse
        {
            get
            {
                TimeSpan ts = m_NextUse - DateTime.Now;
                return ts;
            }
            set
            {
                try { m_NextUse = DateTime.Now + value; }
                catch { }
            }
        }

        private SecureLevel m_Level;

        [CommandProperty(AccessLevel.GameMaster)]
        public SecureLevel Level
        {
            get { return m_Level; }
            set { m_Level = value; }
        }

        public BankersPodiumBell()
            : base(7186)
        {
            Name = "Banker's Podium Bell";
            Hue = 1281;
            NextUse = m_Delay;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!from.Alive)
                return;

            if (!from.InRange(this.GetWorldLocation(), 3))
            {
                from.SendMessage("You are too far away to ring the banker's podium bell.");
                return;
            }

            if (NextUse > TimeSpan.Zero)
            {
                from.SendMessage("The magical bank clerk is busy at the moment. Try back later.");
                return;
            }

            if (from.Criminal)
            {
                from.SendMessage("Thou art a criminal and cannot access thy bank box.");
            }
            else
            {
                BankBox box = from.BankBox;

                if (box != null)
                {
                    box.Open();
                    NextUse = m_Delay;
                }
            }
        }

        public override bool HandlesOnSpeech { get { return true; } }

        public override void OnSpeech(SpeechEventArgs e)
        {
            if (!e.Handled)
            {
                for (int i = 0; i < e.Keywords.Length; ++i)
                {
                    int keyword = e.Keywords[i];

                    if (e.Mobile.Criminal)
                    {
                        e.Mobile.SendMessage("Thou art a criminal and cannot access thy balance.");
                    }
                    else
                    {
                        switch (keyword)
                        {
                                /*
                            case 0x0000: // *withdraw* 
                                {
                                    e.Handled = true;

                                    string[] split = e.Speech.Split(' ');

                                    if (split.Length >= 2)
                                    {
                                        int amount;

                                        try
                                        {
                                            amount = Convert.ToInt32(split[1]);
                                        }
                                        catch
                                        {
                                            break;
                                        }

                                        if (amount > 5000)
                                        {
                                            this.Say(500381); // Thou canst not withdraw so much at one time! 
                                        }
                                        else if (amount > 0)
                                        {
                                            BankBox box = e.Mobile.BankBox;

                                            if (box == null || !box.ConsumeTotal(typeof(Gold), amount))
                                            {
                                                this.Say(500384); // Ah, art thou trying to fool me? Thou hast not so much gold! 
                                            }
                                            else
                                            {
                                                e.Mobile.AddToBackpack(new Gold(amount));

                                                this.Say(1010005); // Thou hast withdrawn gold from thy account. 
                                            }
                                        }
                                    }

                                    break;
                                }
                                 */ 
                            case 0x0001: // *balance* 
                                {
                                    e.Handled = true;

                                    BankBox box = e.Mobile.BankBox;

                                    if (box != null)
                                    {
                                        this.Say(String.Format("Thy current bank balance is {0} gold.", box.TotalGold.ToString()));
                                    }

                                    break;
                                }
                                /*
                            case 0x0002: // *bank* 
                                {
                                    e.Handled = true;

                                    BankBox box = e.Mobile.BankBox;

                                    if (box != null)
                                        box.Open();

                                    break;
                                }
                                */
                            case 0x0003: // *check* 
                                {
                                    e.Handled = true;

                                    string[] split = e.Speech.Split(' ');

                                    if (split.Length >= 2)
                                    {
                                        int amount;

                                        try
                                        {
                                            amount = Convert.ToInt32(split[1]);
                                        }
                                        catch
                                        {
                                            break;
                                        }

                                        if (amount < 5000)
                                        {
                                            this.Say(1010006); // We cannot create checks for such a paltry amount of gold! 
                                        }
                                        else if (amount > 1000000)
                                        {
                                            this.Say(1010007); // Our policies prevent us from creating checks worth that much! 
                                        }
                                        else
                                        {
                                            BankCheck check = new BankCheck(amount);

                                            BankBox box = e.Mobile.BankBox;

                                            if (box == null || !box.TryDropItem(e.Mobile, check, false))
                                            {
                                                this.Say(500386); // There's not enough room in your bankbox for the check! 
                                                check.Delete();
                                            }
                                            else if (!box.ConsumeTotal(typeof(Gold), amount))
                                            {
                                                this.Say(500384); // Ah, art thou trying to fool me? Thou hast not so much gold! 
                                                check.Delete();
                                            }
                                            else
                                            {
                                                this.Say(String.Format("Into your bank box I have placed a check in the amount of: {0}", amount.ToString()));
                                            }
                                        }
                                    }

                                    break;
                                }
                                  
                        }
                    }
                }
            }
        }

        public void Say(int number)
        {
            PublicOverheadMessage(MessageType.Regular, 0x3B2, number);
        }

        public void Say(string args)
        {
            PublicOverheadMessage(MessageType.Regular, 0x3B2, false, args);
        }

        public BankersPodiumBell(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
            writer.WriteEncodedInt((int)m_Level);
            writer.Write(NextUse);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            m_Level = (SecureLevel)reader.ReadEncodedInt();
            NextUse = reader.ReadTimeSpan();
        }
    }
}
