//Animal Crackers Made This xD
using System;
using Server;

namespace Server.Items
{
	public class NewPlayerWelcomeHandbook : BlueBook
  {
        public static readonly BookContent Content = new BookContent
        (
        		"READ ME FIRST","DragonKnights",
        		new BookPageInfo
        		(
        			"Welcome home...",
        			"The book has information",
        			"on some of the added",
        			"features and commands",
        			"we have here. There is",
        			"several new and",
        			"interesting things here",
        			"for you to enjoy."),
        		new BookPageInfo
        		(
        			"COMMANDS:",
        			"*Just type these",
        			"commands and hit",
        			"enter. Some need to",
        			"have a [ for them to",
        			"work.*",
        			"motd - Message of",
        			"the day." ),
        		new BookPageInfo
        		(
        			"[grab options- setups",
        			"your looting options.",
        			"",
        			"[grab - loots things in a",
        			"5 square area.",
        			"",
        			"[craftbag - sets a bag",
        			"to send all the items you"),
        		new BookPageInfo
        		(
        			"craft to, when you craft",
        			"them.",
        			"",
        			"[dump - dump items from",
        			"one container to another",
        			"",
        			"[mail - to access your",
        			"ingame mail."),
        		new BookPageInfo
        		(
        			"[sell options - to set",
        			"items you would like to",
        			"sell when you go to the",
        			"junk buyer and type",
        			"sell bag",
        			"",
        			"[c message - to say a",
        			"message in whole chat."),
        		new BookPageInfo
        		(
        			"[getpet - just incase you",
        			"lost sight of it and need",
        			"help to get it back.",
        			"",
        			"[point - to point at",
        			"something.",
        			"",
        			"[afk message - to display"),
        		new BookPageInfo
        		("a message and showing",
        		 "you are afk.",
        		 "",
        		 "[emote - to make a",
        		 "emotion.",
        		 "",
        		 "[myauction - to bring up",
        		 "a limited menu. Better"),
        		new BookPageInfo
        		(
        			"to see an autioner in",
        			"moonglow.",
        			"",
        			"[what is my status - ",
        			"Funny but good",
        			"information. Just move",
        			"after the words",
        			""),
        		new BookPageInfo
        		(
        			"ADDITIONAL INFORMATION:",
        			"There are several perks..",
        			"The list is getting longer",
        			"and longer. I listed some",
        			"of the different things",
        			"we have on the Motd,",
        			"but with a list that",
        			"keeps growing, its getting"),
        		new BookPageInfo
        		(
        			"harder to keep up. I will",
        			"leave it up to you to",
        			"explore the world and",
        			"learn. Hope we meet",
        			"soon. Be safe, and travel",
        			"well...",
        			"Sincerely yours,",
        			"DragonKnights")
        		);

            public override BookContent DefaultContent{ get{ return Content; } }


         [Constructable]
        public NewPlayerWelcomeHandbook() : base( false )
        {
        	Hue = 1153;
                LootType=LootType.Blessed;
        }

        public NewPlayerWelcomeHandbook( Serial serial ) : base( serial )
        {
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}
