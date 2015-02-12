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
        			"we have here. There are",
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
        			"work, and some don't.*",
        			"",
        			"To get a menu with a" ),
        		new BookPageInfo
        		(
        			"list of commands type:",
        			"[commands",
        			"and then hit enter.",
        			"",
        			"Here is a quick list",
        			"of some of the commands:",
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
        		(
        			"a message and showing",
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
        			"what is my status - ",
        			"Just say the words.",
        			"Information. Just move",
        			"after the words",
        			""),
        		new BookPageInfo
        		(
        			"[viewforums - ",
        			"Pops up the ingame",
        			"forum, where you can",
        			"post messages the same",
        			"way as you do in a",
        			"regular web site forum.",
        			"There are also some",
        			"special Bulletin boards" ),
        		new BookPageInfo
        		(
        			"ingame that you can",
        			"double click on to",
        			"get the same ingame",
        			"forums menu.",
        			"This bulletin board",
        			"can also be placed",
        			"in your home to use.",
        			"" ),
        		new BookPageInfo
        		(
        			"[findplayervendor -",
        			"Pops up a menu with",
        			"a list of player's",
        			"vendors. The arrow",
        			"to the far right of",
        			"players vendor will",
        			"take you directly",
        			"to that vendor."),
        		new BookPageInfo
        		(
        			"ADDITIONAL INFORMATION:",
        			"ACCOUNTS:",
        			"You can have 3 accounts.",
        			"So if you, your spouse,",
        			"and child would like to all",
        			"play together. If you need",
        			"more then 3 accounts",
        			"please contact a GM."),
        		new BookPageInfo
        		(
        			"CHARACTER SLOTS:",
        			"You can have 5 different",
        			"characters per account.",
        			"",
        			"SKILL CAP:",
        			"Is 5800.0 points per",
        			"character. With bouses",
        			"max skill is 125."),
        		new BookPageInfo
        		(
        			"STAT CAP:",
        			"Max stat per chatacter",
        			"is 225 points. Stats are",
        			"INT / STR / DEX.",
        			"",
        			"SPECIAL ADDONS:",
        			"",
        			"STAT/SKILL PURCHASE"),
        		new BookPageInfo
        		(
        			"CHRYSTAL:",
        			"This item will allow you",
        			"to purchase Stats and",
        			"Skills. You can purchase",
        			"up to skill level 90 on a",
        			"skill. It will not allow you",
        			"to purchase past 90. Skill",
        			"gain after 90, you will"),
        		new BookPageInfo
        		(
        			"have to work on yourself.",
        			"",
        			"A CORPSE WAND:",
        			"For a fee, your corpse",
        			"will be brought to your",
        			"location.",
        			"",
        			"SKILL GEMS / BONUS"),
        		new BookPageInfo
        		(
        			"SEWING KITS:",
        			"If you have the correct",
        			"level skill, you can use",
        			"these items, and add skill",
        			"bonuses to your armor,",
        			"weapons, cloths, and",
        			"jewlery from the Skill",
        			"Gems, Also you can add"),
        		new BookPageInfo
        		(
        			"Cold, Fire, Energy, and",
        			"Poison resists from using",
        			"the Resist Sewing Kits.",
        			"",
        			"EVOLUTION DRAGONS:",
        			"A dragons egg, which if",
        			"you have the right Animal",
        			"Taming and Animal Lore,"),
        		new BookPageInfo
        		(
        			"you can hatch it, and",
        			"raise your dragon to be",
        			"stronger then a normal",
        			"tamed animal. As it evos",
        			"you will see it grow.",
        			"",
        			"SPECIAL STORAGE KEYS:",
        			"Which help a great deal"),
        		new BookPageInfo
        		(
        			"with holding a lot of",
        			"different items, giving you",
        			"easy access to the",
        			"stored items anywhere in",
        			"the world.",
        			"",
        			"GOLD VENDOR STONE",
        			"It is a vending machine"),
        		new BookPageInfo
        		(
        			"that sells additional items",
        			"that you may or may not",
        			"find on the regular",
        			"vendors. Some of the",
        			"special items are sold",
        			"only here.",
        			"",
        			"PET RES STONE:"),
        		new BookPageInfo
        		(
        			"Placed in several",
        			"different areas in the",
        			"world, for a fee will res",
        			"your pets if they are",
        			"dead",
        			"",
        			"These are only some of",
        			"the items and addons. "),
        		new BookPageInfo
        		(
        			"There are several perks..",
        			"The list is getting longer",
        			"and longer. I listed some",
        			"of the different things",
        			"we have on the Motd,",
        			"but with a list that",
        			"keeps growing, its getting",
        			"harder to keep up. I will"),
        		

        		new BookPageInfo
        		(
        			"leave it up to you to",
        			"explore the world and",
        			"learn. Hope we meet",
        			"soon. Be safe, and travel",
        			"well...",
        			"Sincerely yours,",
        			"DragonKnights")
        		
        		
        		
//        		new BookPageInfo
//        		(
//        			"",
//        			"",
//        			"",
//        			"",
//        			"",
//        			"",
//        			"",
//        			""),
        		
        		
        		
        		
        		
        		
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
