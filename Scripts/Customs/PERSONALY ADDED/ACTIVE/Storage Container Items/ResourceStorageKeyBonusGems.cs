/*
 created by:
     /\            888                   888     .d8888b.   .d8888b.  
____/_ \____       888                   888    d88P  Y88b d88P  Y88b 
\  ___\ \  /       888                   888    888    888 888    888 
 \/ /  \/ /    .d88888  8888b.   8888b.  888888 Y88b. d888 Y88b. d888 
 / /\__/_/\   d88" 888     "88b     "88b 888     "Y888P888  "Y888P888 
/__\ \_____\  888  888 .d888888 .d888888 888           888        888 
    \  /      Y88b 888 888  888 888  888 Y88b.  Y88b  d88P Y88b  d88P 
     \/        "Y88888 "Y888888 "Y888888  "Y888  "Y8888P"   "Y8888P"  

Sources:

Ingot Key script by GoldDrac13
Granite Box script by (unknown)
BankCrystal script by (unknown)
////////////////////////////////
////////////////////////////////////////
Modified by Ashlar, beloved of Morrigan.  
Modified by Tylius.
Modified gump and added custom ingots by daat99.
Modified a lof of the code by daat99.
//////////////////////////////////////////
This item is a resource storage key as well as a forge (backpack or ground), an anvil (ground only), and a banker (spoken). 
Add or remove references to fit your shard.  
Note however, that adding such items that have a number of uses, like shovels, will allow the player to put a almost caput 
shovel in and pop it back out with 50 uses left.
*/
using System;					//To be honest, I am not sure if all this is needed, but the script works!
using System.Collections;
using Server;
using Server.Prompts;
using Server.Mobiles;
using Server.ContextMenus;
using Server.Gumps;
using Server.Items;
using Server.Misc;
using Server.Network;
using Server.Targeting;
using Server.Multis;
using Server.Regions;


namespace Server.Items
{
    [FlipableAttribute(0xFEF, 0xFF0, 0xFF1, 0xFF2, 0xFF3, 0xFF4, 0xFBD, 0xFBE)]

    public class ResourceStorageKeyBonusGems : Item
    {

    	private int m_plus1alchemygem;
    	private int m_plus2alchemygem;
    	private int m_plus1anatomygem;
    	private int m_plus2anatomygem;
    	private int m_plus1animalloregem;
    	private int m_plus2animalloregem;
    	private int m_plus1animaltaminggem;
    	private int m_plus2animaltaminggem;
    	private int m_plus1archerygem;
    	private int m_plus2archerygem;
    	private int m_plus1armsloregem;
    	private int m_plus2armsloregem;
    	private int m_plus1begginggem;
    	private int m_plus2begginggem;
    	private int m_plus1blacksmithgem;
    	private int m_plus2blacksmithgem;
    	private int m_plus1bushidogem;
    	private int m_plus2bushidogem;
    	private int m_plus1campinggem;
    	private int m_plus2campinggem;
    	private int m_plus1carpentrygem;
    	private int m_plus2carpentrygem;
    	private int m_plus1cartographygem;
    	private int m_plus2cartographygem;
    	private int m_plus1chivalrygem;
    	private int m_plus2chivalrygem;
    	private int m_plus1cookinggem;
    	private int m_plus2cookinggem;
    	private int m_plus1detecthiddengem;
    	private int m_plus2detecthiddengem;
    	private int m_plus1discordancegem;
    	private int m_plus2discordancegem;
    	private int m_plus1evalintgem;
    	private int m_plus2evalintgem;
    	private int m_plus1fencinggem;
    	private int m_plus2fencinggem;
    	private int m_plus1fishinggem;
    	private int m_plus2fishinggem;
    	private int m_plus1fletchinggem;
    	private int m_plus2fletchinggem;
    	private int m_plus1focusgem;
    	private int m_plus2focusgem;
    	private int m_plus1forensicsgem;
    	private int m_plus2forensicsgem;
    	private int m_plus1healinggem;
    	private int m_plus2healinggem;
    	private int m_plus1herdinggem;
    	private int m_plus2herdinggem;
    	private int m_plus1hidinggem;
    	private int m_plus2hidinggem;
    	private int m_plus1inscribegem;
    	private int m_plus2inscribegem;
    	private int m_plus1itemidgem;
    	private int m_plus2itemidgem;
    	private int m_plus1lockpickinggem;
    	private int m_plus2lockpickinggem;
    	private int m_plus1lumberjackinggem;
    	private int m_plus2lumberjackinggem;
    	private int m_plus1macinggem;
    	private int m_plus2macinggem;
    	private int m_plus1magerygem;
    	private int m_plus2magerygem;
    	private int m_plus1magicresistgem;
    	private int m_plus2magicresistgem;
    	private int m_plus1meditationgem;
    	private int m_plus2meditationgem;
    	private int m_plus1mininggem;
    	private int m_plus2mininggem;
    	private int m_plus1musicianshipgem;
    	private int m_plus2musicianshipgem;
    	private int m_plus1necromancygem;
    	private int m_plus2necromancygem;
    	private int m_plus1ninjitsugem;
    	private int m_plus2ninjitsugem;
    	private int m_plus1parrygem;
    	private int m_plus2parrygem;
    	private int m_plus1peacemakinggem;
    	private int m_plus2peacemakinggem;
    	private int m_plus1poisoninggem;
    	private int m_plus2poisoninggem;
    	private int m_plus1provocationgem;
    	private int m_plus2provocationgem;
    	private int m_plus1removetrapgem;
    	private int m_plus2removetrapgem;
    	private int m_plus1snoopinggem;
    	private int m_plus2snoopinggem;
    	private int m_plus1spellweavinggem;
    	private int m_plus2spellweavinggem;
    	private int m_plus1spiritspeakgem;
    	private int m_plus2spiritspeakgem;
    	private int m_plus1stealinggem;
    	private int m_plus2stealinggem;
    	private int m_plus1stealthgem;
    	private int m_plus2stealthgem;
    	private int m_plus1swordsgem;
    	private int m_plus2swordsgem;
    	private int m_plus1tacticsgem;
    	private int m_plus2tacticsgem;
    	private int m_plus1tailoringgem;
    	private int m_plus2tailoringgem;
    	private int m_plus1tasteidgem;
    	private int m_plus2tasteidgem;
    	private int m_plus1tinkeringgem;
    	private int m_plus2tinkeringgem;
    	private int m_plus1trackinggem;
    	private int m_plus2trackinggem;
    	private int m_plus1veterinarygem;
    	private int m_plus2veterinarygem;
    	private int m_plus1wrestlinggem;
    	private int m_plus2wrestlinggem;
    	private int m_nightsightgem;
    	private int m_SpellChannelingGem;

    	private int m_plus0skilleracegem;
    	
    	private int m_ColdResistSewingKit;
    	private int m_EnergyResistSewingKit;
    	private int m_FireResistSewingKit;
    	private int m_PoisonResistSewingKit;
    	
    	
        private int m_StorageLimit;
        private int m_WithdrawIncrement;

        //This section allows GM's and above to change the amounts of the various properties of the key.

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1alchemygem { get { return m_plus1alchemygem; } set { m_plus1alchemygem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2alchemygem { get { return m_plus2alchemygem; } set { m_plus2alchemygem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1anatomygem { get { return m_plus1anatomygem; } set { m_plus1anatomygem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2anatomygem { get { return m_plus2anatomygem; } set { m_plus2anatomygem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1animalloregem { get { return m_plus1animalloregem; } set { m_plus1animalloregem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2animalloregem { get { return m_plus2animalloregem; } set { m_plus2animalloregem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1animaltaminggem { get { return m_plus1animaltaminggem; } set { m_plus1animaltaminggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2animaltaminggem { get { return m_plus2animaltaminggem; } set { m_plus2animaltaminggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1archerygem { get { return m_plus1archerygem; } set { m_plus1archerygem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2archerygem { get { return m_plus2archerygem; } set { m_plus2archerygem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1armsloregem { get { return m_plus1armsloregem; } set { m_plus1armsloregem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2armsloregem { get { return m_plus2armsloregem; } set { m_plus2armsloregem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1begginggem { get { return m_plus1begginggem; } set { m_plus1begginggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2begginggem { get { return m_plus2begginggem; } set { m_plus2begginggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1blacksmithgem { get { return m_plus1blacksmithgem; } set { m_plus1blacksmithgem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2blacksmithgem { get { return m_plus2blacksmithgem; } set { m_plus2blacksmithgem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1bushidogem { get { return m_plus1bushidogem; } set { m_plus1bushidogem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2bushidogem { get { return m_plus2bushidogem; } set { m_plus2bushidogem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1campinggem { get { return m_plus1campinggem; } set { m_plus1campinggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2campinggem { get { return m_plus2campinggem; } set { m_plus2campinggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1carpentrygem { get { return m_plus1carpentrygem; } set { m_plus1carpentrygem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2carpentrygem { get { return m_plus2carpentrygem; } set { m_plus2carpentrygem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1cartographygem { get { return m_plus1cartographygem; } set { m_plus1cartographygem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2cartographygem { get { return m_plus2cartographygem; } set { m_plus2cartographygem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1chivalrygem { get { return m_plus1chivalrygem; } set { m_plus1chivalrygem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2chivalrygem { get { return m_plus2chivalrygem; } set { m_plus2chivalrygem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1cookinggem { get { return m_plus1cookinggem; } set { m_plus1cookinggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2cookinggem { get { return m_plus2cookinggem; } set { m_plus2cookinggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1detecthiddengem { get { return m_plus1detecthiddengem; } set { m_plus1detecthiddengem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2detecthiddengem { get { return m_plus2detecthiddengem; } set { m_plus2detecthiddengem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1discordancegem { get { return m_plus1discordancegem; } set { m_plus1discordancegem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2discordancegem { get { return m_plus2discordancegem; } set { m_plus2discordancegem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1evalintgem { get { return m_plus1evalintgem; } set { m_plus1evalintgem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2evalintgem { get { return m_plus2evalintgem; } set { m_plus2evalintgem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1fencinggem { get { return m_plus1fencinggem; } set { m_plus1fencinggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2fencinggem { get { return m_plus2fencinggem; } set { m_plus2fencinggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1fishinggem { get { return m_plus1fishinggem; } set { m_plus1fishinggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2fishinggem { get { return m_plus2fishinggem; } set { m_plus2fishinggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1fletchinggem { get { return m_plus1fletchinggem; } set { m_plus1fletchinggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2fletchinggem { get { return m_plus2fletchinggem; } set { m_plus2fletchinggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1focusgem { get { return m_plus1focusgem; } set { m_plus1focusgem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2focusgem { get { return m_plus2focusgem; } set { m_plus2focusgem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1forensicsgem { get { return m_plus1forensicsgem; } set { m_plus1forensicsgem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2forensicsgem { get { return m_plus2forensicsgem; } set { m_plus2forensicsgem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1healinggem { get { return m_plus1healinggem; } set { m_plus1healinggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2healinggem { get { return m_plus2healinggem; } set { m_plus2healinggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1herdinggem { get { return m_plus1herdinggem; } set { m_plus1herdinggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2herdinggem { get { return m_plus2herdinggem; } set { m_plus2herdinggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1hidinggem { get { return m_plus1hidinggem; } set { m_plus1hidinggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2hidinggem { get { return m_plus2hidinggem; } set { m_plus2hidinggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1inscribegem { get { return m_plus1inscribegem; } set { m_plus1inscribegem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2inscribegem { get { return m_plus2inscribegem; } set { m_plus2inscribegem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1itemidgem { get { return m_plus1itemidgem; } set { m_plus1itemidgem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2itemidgem { get { return m_plus2itemidgem; } set { m_plus2itemidgem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1lockpickinggem { get { return m_plus1lockpickinggem; } set { m_plus1lockpickinggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2lockpickinggem { get { return m_plus2lockpickinggem; } set { m_plus2lockpickinggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1lumberjackinggem { get { return m_plus1lumberjackinggem; } set { m_plus1lumberjackinggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2lumberjackinggem { get { return m_plus2lumberjackinggem; } set { m_plus2lumberjackinggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1macinggem { get { return m_plus1macinggem; } set { m_plus1macinggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2macinggem { get { return m_plus2macinggem; } set { m_plus2macinggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1magerygem { get { return m_plus1magerygem; } set { m_plus1magerygem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2magerygem { get { return m_plus2magerygem; } set { m_plus2magerygem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1magicresistgem { get { return m_plus1magicresistgem; } set { m_plus1magicresistgem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2magicresistgem { get { return m_plus2magicresistgem; } set { m_plus2magicresistgem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1meditationgem { get { return m_plus1meditationgem; } set { m_plus1meditationgem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2meditationgem { get { return m_plus2meditationgem; } set { m_plus2meditationgem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1mininggem { get { return m_plus1mininggem; } set { m_plus1mininggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2mininggem { get { return m_plus2mininggem; } set { m_plus2mininggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1musicianshipgem { get { return m_plus1musicianshipgem; } set { m_plus1musicianshipgem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2musicianshipgem { get { return m_plus2musicianshipgem; } set { m_plus2musicianshipgem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1necromancygem { get { return m_plus1necromancygem; } set { m_plus1necromancygem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2necromancygem { get { return m_plus2necromancygem; } set { m_plus2necromancygem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1ninjitsugem { get { return m_plus1ninjitsugem; } set { m_plus1ninjitsugem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2ninjitsugem { get { return m_plus2ninjitsugem; } set { m_plus2ninjitsugem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1parrygem { get { return m_plus1parrygem; } set { m_plus1parrygem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2parrygem { get { return m_plus2parrygem; } set { m_plus2parrygem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1peacemakinggem { get { return m_plus1peacemakinggem; } set { m_plus1peacemakinggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2peacemakinggem { get { return m_plus2peacemakinggem; } set { m_plus2peacemakinggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1poisoninggem { get { return m_plus1poisoninggem; } set { m_plus1poisoninggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2poisoninggem { get { return m_plus2poisoninggem; } set { m_plus2poisoninggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1provocationgem { get { return m_plus1provocationgem; } set { m_plus1provocationgem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2provocationgem { get { return m_plus2provocationgem; } set { m_plus2provocationgem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1removetrapgem { get { return m_plus1removetrapgem; } set { m_plus1removetrapgem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2removetrapgem { get { return m_plus2removetrapgem; } set { m_plus2removetrapgem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1snoopinggem { get { return m_plus1snoopinggem; } set { m_plus1snoopinggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2snoopinggem { get { return m_plus2snoopinggem; } set { m_plus2snoopinggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1spellweavinggem { get { return m_plus1spellweavinggem; } set { m_plus1spellweavinggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2spellweavinggem { get { return m_plus2spellweavinggem; } set { m_plus2spellweavinggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1spiritspeakgem { get { return m_plus1spiritspeakgem; } set { m_plus1spiritspeakgem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2spiritspeakgem { get { return m_plus2spiritspeakgem; } set { m_plus2spiritspeakgem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1stealinggem { get { return m_plus1stealinggem; } set { m_plus1stealinggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2stealinggem { get { return m_plus2stealinggem; } set { m_plus2stealinggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1stealthgem { get { return m_plus1stealthgem; } set { m_plus1stealthgem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2stealthgem { get { return m_plus2stealthgem; } set { m_plus2stealthgem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1swordsgem { get { return m_plus1swordsgem; } set { m_plus1swordsgem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2swordsgem { get { return m_plus2swordsgem; } set { m_plus2swordsgem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1tacticsgem { get { return m_plus1tacticsgem; } set { m_plus1tacticsgem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2tacticsgem { get { return m_plus2tacticsgem; } set { m_plus2tacticsgem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1tailoringgem { get { return m_plus1tailoringgem; } set { m_plus1tailoringgem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2tailoringgem { get { return m_plus2tailoringgem; } set { m_plus2tailoringgem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1tasteidgem { get { return m_plus1tasteidgem; } set { m_plus1tasteidgem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2tasteidgem { get { return m_plus2tasteidgem; } set { m_plus2tasteidgem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1tinkeringgem { get { return m_plus1tinkeringgem; } set { m_plus1tinkeringgem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2tinkeringgem { get { return m_plus2tinkeringgem; } set { m_plus2tinkeringgem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1trackinggem { get { return m_plus1trackinggem; } set { m_plus1trackinggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2trackinggem { get { return m_plus2trackinggem; } set { m_plus2trackinggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1veterinarygem { get { return m_plus1veterinarygem; } set { m_plus1veterinarygem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2veterinarygem { get { return m_plus2veterinarygem; } set { m_plus2veterinarygem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus1wrestlinggem { get { return m_plus1wrestlinggem; } set { m_plus1wrestlinggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus2wrestlinggem { get { return m_plus2wrestlinggem; } set { m_plus2wrestlinggem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int nightsightgem { get { return m_nightsightgem; } set { m_nightsightgem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int SpellChannelingGem { get { return m_SpellChannelingGem; } set { m_SpellChannelingGem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int plus0skilleracegem { get { return m_plus0skilleracegem; } set { m_plus0skilleracegem = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int ColdResistSewingKit { get { return m_ColdResistSewingKit; } set { m_ColdResistSewingKit = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int EnergyResistSewingKit { get { return m_EnergyResistSewingKit; } set { m_EnergyResistSewingKit = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int FireResistSewingKit { get { return m_FireResistSewingKit; } set { m_FireResistSewingKit = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int PoisonResistSewingKit { get { return m_PoisonResistSewingKit; } set { m_PoisonResistSewingKit = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int StorageLimit { get { return m_StorageLimit; } set { m_StorageLimit = value; InvalidateProperties(); } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int WithdrawIncrement { get { return m_WithdrawIncrement; } set { m_WithdrawIncrement = value; InvalidateProperties(); } }

        
        


        public void CollectFromBackpack(Mobile from, bool showMessage)
        {
            if (!from.Alive)
                return;
            if (!this.IsChildOf(from.Backpack) && !this.IsChildOf(from.BankBox))
            {
                from.SendMessage("That Skill Bonus Gems key has to be in your backpack or bankbox for you to use it.");
                return;
            }
            

            Type type = typeof(Item);
            Item[] items = from.Backpack.FindItemsByType(type, true);
            for (int j = 0; j < items.Length; j++)
            {
                int currentAmount = 0;

                Item item = items[j];
                if (!item.Movable)
                    continue;
                int amount = item.Amount;

                if (item is plus1alchemygem)
                    currentAmount = m_plus1alchemygem;
                else if (item is plus2alchemygem)
                    currentAmount = m_plus2alchemygem;
                else if (item is plus1anatomygem)
                    currentAmount = m_plus1anatomygem;
                else if (item is plus2anatomygem)
                    currentAmount = m_plus2anatomygem;
                else if (item is plus1animalloregem)
                    currentAmount = m_plus1animalloregem;
                else if (item is plus2animalloregem)
                    currentAmount = m_plus2animalloregem;
                else if (item is plus1animaltaminggem)
                    currentAmount = m_plus1animaltaminggem;
                else if (item is plus2animaltaminggem)
                    currentAmount = m_plus2animaltaminggem;
                else if (item is plus1archerygem)
                    currentAmount = m_plus1archerygem;
                else if (item is plus2archerygem)
                    currentAmount = m_plus2archerygem;
                else if (item is plus1armsloregem)
                    currentAmount = m_plus1armsloregem;
                else if (item is plus2armsloregem)
                    currentAmount = m_plus2armsloregem;
                else if (item is plus1begginggem)
                    currentAmount = m_plus1begginggem;
                else if (item is plus2begginggem)
                    currentAmount = m_plus2begginggem;
                else if (item is plus1blacksmithgem)
                    currentAmount = m_plus1blacksmithgem;
                else if (item is plus2blacksmithgem)
                    currentAmount = m_plus2blacksmithgem;
                else if (item is plus1bushidogem)
                    currentAmount = m_plus1bushidogem;
                else if (item is plus2bushidogem)
                    currentAmount = m_plus2bushidogem;
                else if (item is plus1campinggem)
                    currentAmount = m_plus1campinggem;
                else if (item is plus2campinggem)
                    currentAmount = m_plus2campinggem;
                else if (item is plus1carpentrygem)
                    currentAmount = m_plus1carpentrygem;
                else if (item is plus2carpentrygem)
                    currentAmount = m_plus2carpentrygem;
                else if (item is plus1cartographygem)
                    currentAmount = m_plus1cartographygem;
                else if (item is plus2cartographygem)
                    currentAmount = m_plus2cartographygem;
                else if (item is plus1chivalrygem)
                    currentAmount = m_plus1chivalrygem;
                else if (item is plus2chivalrygem)
                    currentAmount = m_plus2chivalrygem;
                else if (item is plus1cookinggem)
                    currentAmount = m_plus1cookinggem;
                else if (item is plus2cookinggem)
                    currentAmount = m_plus2cookinggem;
                else if (item is plus1detecthiddengem)
                    currentAmount = m_plus1detecthiddengem;
                else if (item is plus2detecthiddengem)
                    currentAmount = m_plus2detecthiddengem;
                else if (item is plus1discordancegem)
                    currentAmount = m_plus1discordancegem;
                else if (item is plus2discordancegem)
                    currentAmount = m_plus2discordancegem;
                else if (item is plus1evalintgem)
                    currentAmount = m_plus1evalintgem;
                else if (item is plus2evalintgem)
                    currentAmount = m_plus2evalintgem;
                else if (item is plus1fencinggem)
                    currentAmount = m_plus1fencinggem;
                else if (item is plus2fencinggem)
                    currentAmount = m_plus2fencinggem;
                else if (item is plus1fishinggem)
                    currentAmount = m_plus1fishinggem;
                else if (item is plus2fishinggem)
                    currentAmount = m_plus2fishinggem;
                else if (item is plus1fletchinggem)
                    currentAmount = m_plus1fletchinggem;
                else if (item is plus2fletchinggem)
                    currentAmount = m_plus2fletchinggem;
                else if (item is plus1focusgem)
                    currentAmount = m_plus1focusgem;
                else if (item is plus2focusgem)
                    currentAmount = m_plus2focusgem;
                else if (item is plus1forensicsgem)
                    currentAmount = m_plus1forensicsgem;
                else if (item is plus2forensicsgem)
                    currentAmount = m_plus2forensicsgem;
                else if (item is plus1healinggem)
                    currentAmount = m_plus1healinggem;
                else if (item is plus2healinggem)
                    currentAmount = m_plus2healinggem;
                else if (item is plus1herdinggem)
                    currentAmount = m_plus1herdinggem;
                else if (item is plus2herdinggem)
                    currentAmount = m_plus2herdinggem;
                else if (item is plus1hidinggem)
                    currentAmount = m_plus1hidinggem;
                else if (item is plus2hidinggem)
                    currentAmount = m_plus2hidinggem;
                else if (item is plus1inscribegem)
                    currentAmount = m_plus1inscribegem;
                else if (item is plus2inscribegem)
                    currentAmount = m_plus2inscribegem;
                else if (item is plus1itemidgem)
                    currentAmount = m_plus1itemidgem;
                else if (item is plus2itemidgem)
                    currentAmount = m_plus2itemidgem;
                else if (item is plus1lockpickinggem)
                    currentAmount = m_plus1lockpickinggem;
                else if (item is plus2lockpickinggem)
                    currentAmount = m_plus2lockpickinggem;
                else if (item is plus1lumberjackinggem)
                    currentAmount = m_plus1lumberjackinggem;
                else if (item is plus2lumberjackinggem)
                    currentAmount = m_plus2lumberjackinggem;
                else if (item is plus1macinggem)
                    currentAmount = m_plus1macinggem;
                else if (item is plus2macinggem)
                    currentAmount = m_plus2macinggem;
                else if (item is plus1magerygem)
                    currentAmount = m_plus1magerygem;
                else if (item is plus2magerygem)
                    currentAmount = m_plus2magerygem;
                else if (item is plus1magicresistgem)
                    currentAmount = m_plus1magicresistgem;
                else if (item is plus2magicresistgem)
                    currentAmount = m_plus2magicresistgem;
                else if (item is plus1meditationgem)
                    currentAmount = m_plus1meditationgem;
                else if (item is plus2meditationgem)
                    currentAmount = m_plus2meditationgem;
                else if (item is plus1mininggem)
                    currentAmount = m_plus1mininggem;
                else if (item is plus2mininggem)
                    currentAmount = m_plus2mininggem;
                else if (item is plus1musicianshipgem)
                    currentAmount = m_plus1musicianshipgem;
                else if (item is plus2musicianshipgem)
                    currentAmount = m_plus2musicianshipgem;
                else if (item is plus1necromancygem)
                    currentAmount = m_plus1necromancygem;
                else if (item is plus2necromancygem)
                    currentAmount = m_plus2necromancygem;
                else if (item is plus1ninjitsugem)
                    currentAmount = m_plus1ninjitsugem;
                else if (item is plus2ninjitsugem)
                    currentAmount = m_plus2ninjitsugem;
                else if (item is plus1parrygem)
                    currentAmount = m_plus1parrygem;
                else if (item is plus2parrygem)
                    currentAmount = m_plus2parrygem;
                else if (item is plus1peacemakinggem)
                    currentAmount = m_plus1peacemakinggem;
                else if (item is plus2peacemakinggem)
                    currentAmount = m_plus2peacemakinggem;
                else if (item is plus1poisoninggem)
                    currentAmount = m_plus1poisoninggem;
                else if (item is plus2poisoninggem)
                    currentAmount = m_plus2poisoninggem;
                else if (item is plus1provocationgem)
                    currentAmount = m_plus1provocationgem;
                else if (item is plus2provocationgem)
                    currentAmount = m_plus2provocationgem;
                else if (item is plus1removetrapgem)
                    currentAmount = m_plus1removetrapgem;
                else if (item is plus2removetrapgem)
                    currentAmount = m_plus2removetrapgem;
                else if (item is plus1snoopinggem)
                    currentAmount = m_plus1snoopinggem;
                else if (item is plus2snoopinggem)
                    currentAmount = m_plus2snoopinggem;
                else if (item is plus1spellweavinggem)
                    currentAmount = m_plus1spellweavinggem;
                else if (item is plus2spellweavinggem)
                    currentAmount = m_plus2spellweavinggem;
                else if (item is plus1spiritspeakgem)
                    currentAmount = m_plus1spiritspeakgem;
                else if (item is plus2spiritspeakgem)
                    currentAmount = m_plus2spiritspeakgem;
                else if (item is plus1stealinggem)
                    currentAmount = m_plus1stealinggem;
                else if (item is plus2stealinggem)
                    currentAmount = m_plus2stealinggem;
                else if (item is plus1stealthgem)
                    currentAmount = m_plus1stealthgem;
                else if (item is plus2stealthgem)
                    currentAmount = m_plus2stealthgem;
                else if (item is plus1swordsgem)
                    currentAmount = m_plus1swordsgem;
                else if (item is plus2swordsgem)
                    currentAmount = m_plus2swordsgem;
                else if (item is plus1tacticsgem)
                    currentAmount = m_plus1tacticsgem;
                else if (item is plus2tacticsgem)
                    currentAmount = m_plus2tacticsgem;
                else if (item is plus1tailoringgem)
                    currentAmount = m_plus1tailoringgem;
                else if (item is plus2tailoringgem)
                    currentAmount = m_plus2tailoringgem;
                else if (item is plus1tasteidgem)
                    currentAmount = m_plus1tasteidgem;
                else if (item is plus2tasteidgem)
                    currentAmount = m_plus2tasteidgem;
                else if (item is plus1tinkeringgem)
                    currentAmount = m_plus1tinkeringgem;
                else if (item is plus2tinkeringgem)
                    currentAmount = m_plus2tinkeringgem;
                else if (item is plus1trackinggem)
                    currentAmount = m_plus1trackinggem;
                else if (item is plus2trackinggem)
                    currentAmount = m_plus2trackinggem;
                else if (item is plus1veterinarygem)
                    currentAmount = m_plus1veterinarygem;
                else if (item is plus2veterinarygem)
                    currentAmount = m_plus2veterinarygem;
                else if (item is plus1wrestlinggem)
                    currentAmount = m_plus1wrestlinggem;
                else if (item is plus2wrestlinggem)
                    currentAmount = m_plus2wrestlinggem;
                else if (item is nightsightgem)
                    currentAmount = m_nightsightgem;
                else if (item is SpellChannelingGem)
                    currentAmount = m_SpellChannelingGem;
				else if (item is plus0skilleracegem)
                    currentAmount = m_plus0skilleracegem;

                
                
                if (amount + currentAmount > m_StorageLimit)
                    continue;
                else
                {
	                if (item is plus1alchemygem)
	                {
	                	m_plus1alchemygem += amount;
	                	item.Delete();
	                }
	                   
	                else if (item is plus2alchemygem)
	                {
	                	m_plus2alchemygem += amount;
	                	item.Delete();
	                }
	                    
    	            else if (item is plus1anatomygem)
    	            {
    	            	m_plus1anatomygem += amount;
    	            	item.Delete();
    	            }
    	                
        	        else if (item is plus2anatomygem)
        	        {
        	        	m_plus2anatomygem += amount;
        	        	item.Delete();
        	        }
        	           
            	    else if (item is plus1animalloregem)
            	    {
            	    	m_plus1animalloregem += amount;
            	    	item.Delete();
            	    }
            	        
                	else if (item is plus2animalloregem)
                	{
                		m_plus2animalloregem += amount;
                		item.Delete();
                	}
                	    
	                else if (item is plus1animaltaminggem)
	                {
	                	m_plus1animaltaminggem += amount;
	                	item.Delete();
	                }
                    	
    	            else if (item is plus2animaltaminggem)
    	            {
    	            	m_plus2animaltaminggem += amount;
    	            	item.Delete();
    	            }
	                    
        	        else if (item is plus1archerygem)
        	        {
        	        	m_plus1archerygem += amount;
        	        	item.Delete();
        	        }
        	        
        	        else if (item is plus2archerygem)
        	        {
        	        	m_plus2archerygem += amount;
        	        	item.Delete();
        	        }
        	            
                	else if (item is plus1armsloregem)
                	{
                		m_plus1armsloregem += amount;
                		item.Delete();
                	}
            	        
	                else if (item is plus2armsloregem)
	                {
	                	m_plus2armsloregem += amount;
	                	item.Delete();
	                }
                    	
    	            else if (item is plus1begginggem)
    	            {
    	            	m_plus1begginggem += amount;
    	            	item.Delete();
    	            }
                    	
        	        else if (item is plus2begginggem)
        	        {
        	        	m_plus2begginggem += amount;
        	        	item.Delete();
        	        }
                    	
            	    else if (item is plus1blacksmithgem)
            	    {
            	    	m_plus1blacksmithgem += amount;
            	    	item.Delete();
            	    }
                    	
            	    else if (item is plus2blacksmithgem)
            	    {
            	    	m_plus2blacksmithgem += amount;
            	    	item.Delete();
            	    }
                    	
	                else if (item is plus1bushidogem)
	                {
	                	m_plus1bushidogem += amount;
	                	item.Delete();
	                }
                    	
    	            else if (item is plus2bushidogem)
    	            {
    	            	m_plus2bushidogem += amount;
    	            	item.Delete();
    	            }
                    	
                    	else if (item is plus1campinggem)
                    	{
                    		m_plus1campinggem += amount;
                    		item.Delete();
                    	}
                    	
            	    else if (item is plus2campinggem)
            	    {
            	    	m_plus2campinggem += amount;
            	    	item.Delete();
            	    }
                    	
                	else if (item is plus1carpentrygem)
                	{
                		m_plus1carpentrygem += amount;
                		item.Delete();
                	}
                	
	                else if (item is plus2carpentrygem)
                	{
                		m_plus2carpentrygem += amount;
                		item.Delete();
                	}
                    	
    	            else if (item is plus1cartographygem)
                	{
                		m_plus1cartographygem += amount;
                		item.Delete();
                	}
                    	
        	        else if (item is plus2cartographygem)
                	{
                		m_plus2cartographygem += amount;
                		item.Delete();
                	}
                    	
            	    else if (item is plus1chivalrygem)
                	{
                		m_plus1chivalrygem += amount;
                		item.Delete();
                	}
                    	
                	else if (item is plus2chivalrygem)
                	{
                		m_plus2chivalrygem += amount;
                		item.Delete();
                	}
                    	
	                else if (item is plus1cookinggem)
                	{
                		m_plus1cookinggem += amount;
                		item.Delete();
                	}
                    	
    	            else if (item is plus2cookinggem)
                	{
                		m_plus2cookinggem += amount;
                		item.Delete();
                	}
                    	
        	        else if (item is plus1detecthiddengem)
                	{
                		m_plus1detecthiddengem += amount;
                		item.Delete();
                	}
                    	
            	    else if (item is plus2detecthiddengem)
                	{
                		m_plus2detecthiddengem += amount;
                		item.Delete();
                	}
                    	
                	else if (item is plus1discordancegem)
                	{
                		m_plus1discordancegem += amount;
                		item.Delete();
                	}
                    	
	                else if (item is plus2discordancegem)
                	{
                		m_plus2discordancegem += amount;
                		item.Delete();
                	}
                    	
    	            else if (item is plus1evalintgem)
                	{
                		m_plus1evalintgem += amount;
                		item.Delete();
                	}
                    	
        	        else if (item is plus2evalintgem)
                	{
                		m_plus2evalintgem += amount;
                		item.Delete();
                	}
                    	
            	    else if (item is plus1fencinggem)
                	{
                		m_plus1fencinggem += amount;
                		item.Delete();
                	}
                    	
                	else if (item is plus2fencinggem)
                	{
                		m_plus2fencinggem += amount;
                		item.Delete();
                	}
                    	
	                else if (item is plus1fishinggem)
                	{
                		m_plus1fishinggem += amount;
                		item.Delete();
                	}
                    	
        	        else if (item is plus2fishinggem)
                	{
                		m_plus2fishinggem += amount;
                		item.Delete();
                	}
                    	
            	    else if (item is plus1fletchinggem)
                	{
                		m_plus1fletchinggem += amount;
                		item.Delete();
                	}
                    	
                	else if (item is plus2fletchinggem)
                	{
                		m_plus2fletchinggem += amount;
                		item.Delete();
                	}
                    	
	                else if (item is plus1focusgem)
                	{
                		m_plus1focusgem += amount;
                		item.Delete();
                	}
                    	
    	            else if (item is plus2focusgem)
                	{
                		m_plus2focusgem += amount;
                		item.Delete();
                	}
                    	
        	        else if (item is plus1forensicsgem)
                	{
                		m_plus1forensicsgem += amount;
                		item.Delete();
                	}
                    	
            	    else if (item is plus2forensicsgem)
                	{
                		m_plus2forensicsgem += amount;
                		item.Delete();
                	}
                    	
                	else if (item is plus1healinggem)
                	{
                		m_plus1healinggem += amount;
                		item.Delete();
                	}
                    	
	                else if (item is plus2healinggem)
                	{
                		m_plus2healinggem += amount;
                		item.Delete();
                	}
                    	
    	            else if (item is plus1herdinggem)
                	{
                		m_plus1herdinggem += amount;
                		item.Delete();
                	}
                    	
        	        else if (item is plus2herdinggem)
                	{
                		m_plus2herdinggem += amount;
                		item.Delete();
                	}
                    	
            	    else if (item is plus1hidinggem)
                	{
                		m_plus1hidinggem += amount;
                		item.Delete();
                	}
                    	
                	else if (item is plus2hidinggem)
                	{
                		m_plus2hidinggem += amount;
                		item.Delete();
                	}
                    	
	                else if (item is plus1inscribegem)
                	{
                		m_plus1inscribegem += amount;
                		item.Delete();
                	}
                    	
    	            else if (item is plus2inscribegem)
                	{
                		m_plus2inscribegem += amount;
                		item.Delete();
                	}
                    	
        	        else if (item is plus1itemidgem)
                	{
                		m_plus1itemidgem += amount;
                		item.Delete();
                	}
                    	
            	    else if (item is plus2itemidgem)
                	{
                		m_plus2itemidgem += amount;
                		item.Delete();
                	}
                    	
                	else if (item is plus1lockpickinggem)
                	{
                		m_plus1lockpickinggem += amount;
                		item.Delete();
                	}
                    	
	                else if (item is plus2lockpickinggem)
                	{
                		m_plus2lockpickinggem += amount;
                		item.Delete();
                	}
                    	
    	            else if (item is plus1lumberjackinggem)
                	{
                		m_plus1lumberjackinggem += amount;
                		item.Delete();
                	}
                    	
        	        else if (item is plus2lumberjackinggem)
                	{
                		m_plus2lumberjackinggem += amount;
                		item.Delete();
                	}
                    	
            	    else if (item is plus1macinggem)
                	{
                		m_plus1macinggem += amount;
                		item.Delete();
                	}
                    	
                	else if (item is plus2macinggem)
                	{
                		m_plus2macinggem += amount;
                		item.Delete();
                	}
                    	
	                else if (item is plus1magerygem)
                	{
                		m_plus1magerygem += amount;
                		item.Delete();
                	}
                    	
    	            else if (item is plus2magerygem)
                	{
                		m_plus2magerygem += amount;
                		item.Delete();
                	}
                    	
        	        else if (item is plus1magicresistgem)
                	{
                		m_plus1magicresistgem += amount;
                		item.Delete();
                	}
                    	
            	    else if (item is plus2magicresistgem)
                	{
                		m_plus2magicresistgem += amount;
                		item.Delete();
                	}
                    	
                	else if (item is plus1meditationgem)
                	{
                		m_plus1meditationgem += amount;
                		item.Delete();
                	}
                    	
	                else if (item is plus2meditationgem)
                	{
                		m_plus2meditationgem += amount;
                		item.Delete();
                	}
                    	
    	            else if (item is plus1mininggem)
                	{
                		m_plus1mininggem += amount;
                		item.Delete();
                	}
                    	
        	        else if (item is plus2mininggem)
                	{
                		m_plus2mininggem += amount;
                		item.Delete();
                	}
                    	
            	    else if (item is plus1musicianshipgem)
                	{
                		m_plus1musicianshipgem += amount;
                		item.Delete();
                	}
                    	
                	else if (item is plus2musicianshipgem)
                	{
                		m_plus2musicianshipgem += amount;
                		item.Delete();
                	}
                    	
	                else if (item is plus1necromancygem)
                	{
                		m_plus1necromancygem += amount;
                		item.Delete();
                	}
                    	
    	            else if (item is plus2necromancygem)
                	{
                		m_plus2necromancygem += amount;
                		item.Delete();
                	}
                    	
        	        else if (item is plus1ninjitsugem)
                	{
                		m_plus1ninjitsugem += amount;
                		item.Delete();
                	}
                    	
            	    else if (item is plus2ninjitsugem)
                	{
                		m_plus2ninjitsugem += amount;
                		item.Delete();
                	}
                    	
                	else if (item is plus1parrygem)
                	{
                		m_plus1parrygem += amount;
                		item.Delete();
                	}
                    	
	                else if (item is plus2parrygem)
                	{
                		m_plus2parrygem += amount;
                		item.Delete();
                	}
                    	
    	            else if (item is plus1peacemakinggem)
                	{
                		m_plus1peacemakinggem += amount;
                		item.Delete();
                	}
                    	
        	        else if (item is plus2peacemakinggem)
                	{
                		m_plus2peacemakinggem += amount;
                		item.Delete();
                	}
                    	
            	    else if (item is plus1poisoninggem)
                	{
                		m_plus1poisoninggem += amount;
                		item.Delete();
                	}
                    	
                	else if (item is plus2poisoninggem)
                	{
                		m_plus2poisoninggem += amount;
                		item.Delete();
                	}
                    	
	                else if (item is plus1provocationgem)
                	{
                		m_plus1provocationgem += amount;
                		item.Delete();
                	}
                    	
    	            else if (item is plus2provocationgem)
                	{
                		m_plus2provocationgem += amount;
                		item.Delete();
                	}
                    	
        	        else if (item is plus1removetrapgem)
                	{
                		m_plus1removetrapgem += amount;
                		item.Delete();
                	}
                    	
            	    else if (item is plus2removetrapgem)
                	{
                		m_plus2removetrapgem += amount;
                		item.Delete();
                	}
                    	
	                else if (item is plus1snoopinggem)
                	{
                		m_plus1snoopinggem += amount;
                		item.Delete();
                	}
                    	
    	            else if (item is plus2snoopinggem)
                	{
                		m_plus2snoopinggem += amount;
                		item.Delete();
                	}
                    	
        	        else if (item is plus1spellweavinggem)
                	{
                		m_plus1spellweavinggem += amount;
                		item.Delete();
                	}
                    	
	                else if (item is plus2spellweavinggem)
                	{
                		m_plus2spellweavinggem += amount;
                		item.Delete();
                	}
                    	
    	            else if (item is plus1spiritspeakgem)
                	{
                		m_plus1spiritspeakgem += amount;
                		item.Delete();
                	}
                    	
        	        else if (item is plus2spiritspeakgem)
                	{
                		m_plus2spiritspeakgem += amount;
                		item.Delete();
                	}
                    	
            	    else if (item is plus1stealinggem)
                	{
                		m_plus1stealinggem += amount;
                		item.Delete();
                	}
                    	
                	else if (item is plus2stealinggem)
                	{
                		m_plus2stealinggem += amount;
                		item.Delete();
                	}
                    	
	                else if (item is plus1stealthgem)
                	{
                		m_plus1stealthgem += amount;
                		item.Delete();
                	}
                    	
    	            else if (item is plus2stealthgem)
                	{
                		m_plus2stealthgem += amount;
                		item.Delete();
                	}
                    	
        	        else if (item is plus1swordsgem)
                	{
                		m_plus1swordsgem += amount;
                		item.Delete();
                	}
                    	
            	    else if (item is plus2swordsgem)
                	{
                		m_plus2swordsgem += amount;
                		item.Delete();
                	}
                    	
	                else if (item is plus1tacticsgem)
                	{
                		m_plus1tacticsgem += amount;
                		item.Delete();
                	}
                    	
    	            else if (item is plus2tacticsgem)
                	{
                		m_plus2tacticsgem += amount;
                		item.Delete();
                	}
                    	
        	        else if (item is plus1tailoringgem)
                	{
                		m_plus1tailoringgem += amount;
                		item.Delete();
                	}
                    	
            	    else if (item is plus2tailoringgem)
                	{
                		m_plus2tailoringgem += amount;
                		item.Delete();
                	}
                    	
	                else if (item is plus1tasteidgem)
                	{
                		m_plus1tasteidgem += amount;
                		item.Delete();
                	}
                    	
    	            else if (item is plus2tasteidgem)
                	{
                		m_plus2tasteidgem += amount;
                		item.Delete();
                	}
                    	
        	        else if (item is plus1tinkeringgem)
                	{
                		m_plus1tinkeringgem += amount;
                		item.Delete();
                	}
                    	
            	    else if (item is plus2tinkeringgem)
                	{
                		m_plus2tinkeringgem += amount;
                		item.Delete();
                	}
                    	
                	else if (item is plus1trackinggem)
                	{
                		m_plus1trackinggem += amount;
                		item.Delete();
                	}
                    	
	                else if (item is plus2trackinggem)
                	{
                		m_plus2trackinggem += amount;
                		item.Delete();
                	}
                    	
    	            else if (item is plus1veterinarygem)
                	{
                		m_plus1veterinarygem += amount;
                		item.Delete();
                	}
                    	
        	        else if (item is plus2veterinarygem)
                	{
                		m_plus2veterinarygem += amount;
                		item.Delete();
                	}
                    	
            	    else if (item is plus1wrestlinggem)
                	{
                		m_plus1wrestlinggem += amount;
                		item.Delete();
                	}
                    	
                	else if (item is plus2wrestlinggem)
                	{
                		m_plus2wrestlinggem += amount;
                		item.Delete();
                	}
                    	
	                else if (item is nightsightgem)
                	{
                		m_nightsightgem += amount;
                		item.Delete();
                	}
                    	
    	            else if (item is SpellChannelingGem)
                	{
                		m_SpellChannelingGem += amount;
                		item.Delete();
                	}
                    	
    	            else if (item is plus0skilleracegem)
                	{
                		m_plus0skilleracegem += amount;
                		item.Delete();
                	}
                		


//                    item.Delete();
                }
            }
            

            
            type = typeof(ColdResistSewingKit);
            items = from.Backpack.FindItemsByType(type, true);
            for (int j = 0; j < items.Length; j++)
            {
                int currentAmount = 0;

                Item item = items[j];
                if (!item.Movable)
                    continue;
                int amount = item.Amount;

                if (item is ColdResistSewingKit)
                    currentAmount = m_ColdResistSewingKit;

                if (amount + currentAmount > m_StorageLimit)
                    continue;
                else
                {
                    if (item is ColdResistSewingKit)
                        m_ColdResistSewingKit += amount;

                    item.Delete();
                }
            }
            
            
            type = typeof(EnergyResistSewingKit);
            items = from.Backpack.FindItemsByType(type, true);
            for (int j = 0; j < items.Length; j++)
            {
                int currentAmount = 0;

                Item item = items[j];
                if (!item.Movable)
                    continue;
                int amount = item.Amount;

                if (item is EnergyResistSewingKit)
                    currentAmount = m_EnergyResistSewingKit;

                if (amount + currentAmount > m_StorageLimit)
                    continue;
                else
                {
                    if (item is EnergyResistSewingKit)
                        m_EnergyResistSewingKit += amount;

                    item.Delete();
                }
            }
            
            
            type = typeof(FireResistSewingKit);
            items = from.Backpack.FindItemsByType(type, true);
            for (int j = 0; j < items.Length; j++)
            {
                int currentAmount = 0;

                Item item = items[j];
                if (!item.Movable)
                    continue;
                int amount = item.Amount;

                if (item is FireResistSewingKit)
                    currentAmount = m_FireResistSewingKit;

                if (amount + currentAmount > m_StorageLimit)
                    continue;
                else
                {
                    if (item is FireResistSewingKit)
                        m_FireResistSewingKit += amount;

                    item.Delete();
                }
            }
            
            
            type = typeof(PoisonResistSewingKit);
            items = from.Backpack.FindItemsByType(type, true);
            for (int j = 0; j < items.Length; j++)
            {
                int currentAmount = 0;

                Item item = items[j];
                if (!item.Movable)
                    continue;
                int amount = item.Amount;

                if (item is PoisonResistSewingKit)
                    currentAmount = m_PoisonResistSewingKit;

                if (amount + currentAmount > m_StorageLimit)
                    continue;
                else
                {
                    if (item is PoisonResistSewingKit)
                        m_PoisonResistSewingKit += amount;

                    item.Delete();
                }
            }
            
            
            if (showMessage)
                from.SendMessage("Bonus Gems and Bonus Sewing Kits are collected from your backpack into that key, subject to storage limit.");
        }

        //This is the default item you get when you [add ResourceStorageKeyBonusGems
        [Constructable]
        public ResourceStorageKeyBonusGems()
            : base(5995)
        {
            Movable = true;
            Weight = 1.0;
            Hue = 255;
            Name = "Bonus Gems - Bonus Sewing Kits Storage Keys";
            LootType = LootType.Blessed;
            StorageLimit = 9999;
//            WithdrawIncrement = 100;
        }

        [Constructable]
        public ResourceStorageKeyBonusGems(int storageLimit, int withdrawIncrement)
            : base(5995)
        {
            Movable = true;
            Weight = 1.0;
            Hue = 255;
            Name = "Bonus Gems - Bonus Sewing Kits Storage Keys";
            LootType = LootType.Blessed;
            StorageLimit = storageLimit;
//            WithdrawIncrement = withdrawIncrement;

        }
        public override void OnDoubleClick(Mobile from)
        {
//            if (from.Map == Map.Felucca)
//            {
//                from.SendMessage("That does not work in Felucca.");
//                return;
//            }

            if (IsChildOf(from.Backpack) || IsChildOf(from.BankBox))
                from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
            else
                from.SendMessage("This must be in your backpack or bankbox to use.");
        }
        public void BeginCombine(Mobile from)
        {
            from.Target = new ResourceStorageKeyBonusGemsTarget(this);
        }
        public void EndCombine(Mobile from, object o)
        {
            if (o is Item && (((Item)o).IsChildOf(from.Backpack) || ((Item)o).IsChildOf(from.BankBox)))
            {
                Item curItem = o as Item;
                if (curItem is Item)
                {
                    if (curItem is plus1alchemygem)
                    {
                        if (plus1alchemygem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1alchemygem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1alchemygem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2alchemygem)
                    {
                        if (plus2alchemygem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2alchemygem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2alchemygem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1anatomygem)
                    {
                        if (plus1anatomygem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1anatomygem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1anatomygem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }

                    else if (curItem is plus2anatomygem)
                    {
                        if (plus2anatomygem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2anatomygem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2anatomygem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1animalloregem)
                    {
                        if (plus1animalloregem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1animalloregem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1animalloregem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2animalloregem)
                    {
                        if (plus2animalloregem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2animalloregem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2animalloregem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1animaltaminggem)
                    {
                        if (plus1animaltaminggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1animaltaminggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1animaltaminggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2animaltaminggem)
                    {
                        if (plus2animaltaminggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2animaltaminggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2animaltaminggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1archerygem)
                    {
                        if (plus1archerygem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1archerygem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1archerygem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2archerygem)
                    {
                        if (plus2archerygem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2archerygem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2archerygem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1armsloregem)
                    {
                        if (plus1armsloregem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1armsloregem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1armsloregem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2armsloregem)
                    {
                        if (plus2armsloregem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2armsloregem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2armsloregem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1begginggem)
                    {
                        if (plus1begginggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1begginggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1begginggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2begginggem)
                    {
                        if (plus2begginggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2begginggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2begginggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1blacksmithgem)
                    {
                        if (plus1blacksmithgem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1blacksmithgem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1blacksmithgem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2blacksmithgem)
                    {
                        if (plus2blacksmithgem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2blacksmithgem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2blacksmithgem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1bushidogem)
                    {
                        if (plus1bushidogem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1bushidogem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1bushidogem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2bushidogem)
                    {
                        if (plus2bushidogem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2bushidogem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2bushidogem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1campinggem)
                    {
                        if (plus1campinggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1campinggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1campinggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2campinggem)
                    {
                        if (plus2campinggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2campinggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2campinggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1carpentrygem)
                    {
                        if (plus1carpentrygem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1carpentrygem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1carpentrygem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2carpentrygem)
                    {
                        if (plus2carpentrygem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2carpentrygem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2carpentrygem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1cartographygem)
                    {
                        if (plus1cartographygem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1cartographygem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1cartographygem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2cartographygem)
                    {
                        if (plus2cartographygem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2cartographygem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2cartographygem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1chivalrygem)
                    {
                        if (plus1chivalrygem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1chivalrygem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1chivalrygem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2chivalrygem)
                    {
                        if (plus2chivalrygem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2chivalrygem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2chivalrygem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1cookinggem)
                    {
                        if (plus1cookinggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1cookinggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1cookinggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2cookinggem)
                    {
                        if (plus2cookinggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2cookinggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2cookinggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1detecthiddengem)
                    {
                        if (plus1detecthiddengem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1detecthiddengem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1detecthiddengem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2detecthiddengem)
                    {
                        if (plus2detecthiddengem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2detecthiddengem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2detecthiddengem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1discordancegem)
                    {
                        if (plus1discordancegem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1discordancegem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1discordancegem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2discordancegem)
                    {
                        if (plus2discordancegem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2discordancegem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2discordancegem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1evalintgem)
                    {
                        if (plus1evalintgem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1evalintgem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1evalintgem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2evalintgem)
                    {
                        if (plus2evalintgem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2evalintgem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2evalintgem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1fencinggem)
                    {
                        if (plus1fencinggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1fencinggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1fencinggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2fencinggem)
                    {
                        if (plus2fencinggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2fencinggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2fencinggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1fishinggem)
                    {
                        if (plus1fishinggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1fishinggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1fishinggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2fishinggem)
                    {
                        if (plus2fishinggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2fishinggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2fishinggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1fletchinggem)
                    {
                        if (plus1fletchinggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1fletchinggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1fletchinggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2fletchinggem)
                    {
                        if (plus2fletchinggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2fletchinggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2fletchinggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1focusgem)
                    {
                        if (plus1focusgem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1focusgem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1focusgem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2focusgem)
                    {
                        if (plus2focusgem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2focusgem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2focusgem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1forensicsgem)
                    {
                        if (plus1forensicsgem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1forensicsgem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1forensicsgem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2forensicsgem)
                    {
                        if (plus2forensicsgem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2forensicsgem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2forensicsgem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1healinggem)
                    {
                        if (plus1healinggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1healinggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1healinggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2healinggem)
                    {
                        if (plus2healinggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2healinggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2healinggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1herdinggem)
                    {
                        if (plus1herdinggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1herdinggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1herdinggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2herdinggem)
                    {
                        if (plus2herdinggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2herdinggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2herdinggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1hidinggem)
                    {
                        if (plus1hidinggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1hidinggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1hidinggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2hidinggem)
                    {
                        if (plus2hidinggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2hidinggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2hidinggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1inscribegem)
                    {
                        if (plus1inscribegem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1inscribegem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1inscribegem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2inscribegem)
                    {
                        if (plus2inscribegem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2inscribegem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2inscribegem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1itemidgem)
                    {
                        if (plus1itemidgem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1itemidgem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1itemidgem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2itemidgem)
                    {
                        if (plus2itemidgem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2itemidgem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2itemidgem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1lockpickinggem)
                    {
                        if (plus1lockpickinggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1lockpickinggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1lockpickinggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2lockpickinggem)
                    {
                        if (plus2lockpickinggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2lockpickinggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2lockpickinggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1lumberjackinggem)
                    {
                        if (plus1lumberjackinggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1lumberjackinggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1lumberjackinggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2lumberjackinggem)
                    {
                        if (plus2lumberjackinggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2lumberjackinggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2lumberjackinggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1macinggem)
                    {
                        if (plus1macinggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1macinggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1macinggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2macinggem)
                    {
                        if (plus2macinggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2macinggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2macinggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1magerygem)
                    {
                        if (plus1magerygem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1magerygem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1magerygem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2magerygem)
                    {
                        if (plus2magerygem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2magerygem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2magerygem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1magicresistgem)
                    {
                        if (plus1magicresistgem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1magicresistgem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1magicresistgem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2magicresistgem)
                    {
                        if (plus2magicresistgem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2magicresistgem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2magicresistgem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1meditationgem)
                    {
                        if (plus1meditationgem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1meditationgem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1meditationgem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2meditationgem)
                    {
                        if (plus2meditationgem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2meditationgem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2meditationgem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1mininggem)
                    {
                        if (plus1mininggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1mininggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1mininggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2mininggem)
                    {
                        if (plus2mininggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2mininggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2mininggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1musicianshipgem)
                    {
                        if (plus1musicianshipgem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1musicianshipgem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1musicianshipgem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2musicianshipgem)
                    {
                        if (plus2musicianshipgem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2musicianshipgem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2musicianshipgem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1necromancygem)
                    {
                        if (plus1necromancygem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1necromancygem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1necromancygem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2necromancygem)
                    {
                        if (plus2necromancygem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2necromancygem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2necromancygem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1ninjitsugem)
                    {
                        if (plus1ninjitsugem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1ninjitsugem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1ninjitsugem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2ninjitsugem)
                    {
                        if (plus2ninjitsugem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2ninjitsugem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2ninjitsugem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1parrygem)
                    {
                        if (plus1parrygem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1parrygem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1parrygem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2parrygem)
                    {
                        if (plus2parrygem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2parrygem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2parrygem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1peacemakinggem)
                    {
                        if (plus1peacemakinggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1peacemakinggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1peacemakinggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2peacemakinggem)
                    {
                        if (plus2peacemakinggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2peacemakinggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2peacemakinggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1poisoninggem)
                    {
                        if (plus1poisoninggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1poisoninggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1poisoninggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2poisoninggem)
                    {
                        if (plus2poisoninggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2poisoninggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2poisoninggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1provocationgem)
                    {
                        if (plus1provocationgem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1provocationgem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1provocationgem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2provocationgem)
                    {
                        if (plus2provocationgem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2provocationgem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2provocationgem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1removetrapgem)
                    {
                        if (plus1removetrapgem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1removetrapgem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1removetrapgem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2removetrapgem)
                    {
                        if (plus2removetrapgem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2removetrapgem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2removetrapgem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1snoopinggem)
                    {
                        if (plus1snoopinggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1snoopinggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1snoopinggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2snoopinggem)
                    {
                        if (plus2snoopinggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2snoopinggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2snoopinggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1spellweavinggem)
                    {
                        if (plus1spellweavinggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1spellweavinggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1spellweavinggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2spellweavinggem)
                    {
                        if (plus2spellweavinggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2spellweavinggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2spellweavinggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1spiritspeakgem)
                    {
                        if (plus1spiritspeakgem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1spiritspeakgem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1spiritspeakgem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2spiritspeakgem)
                    {
                        if (plus2spiritspeakgem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2spiritspeakgem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2spiritspeakgem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1stealinggem)
                    {
                        if (plus1stealinggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1stealinggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1stealinggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2stealinggem)
                    {
                        if (plus2stealinggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2stealinggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2stealinggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1stealthgem)
                    {
                        if (plus1stealthgem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1stealthgem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1stealthgem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2stealthgem)
                    {
                        if (plus2stealthgem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2stealthgem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2stealthgem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1swordsgem)
                    {
                        if (plus1swordsgem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1swordsgem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1swordsgem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2swordsgem)
                    {
                        if (plus2swordsgem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2swordsgem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2swordsgem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1tacticsgem)
                    {
                        if (plus1tacticsgem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1tacticsgem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1tacticsgem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2tacticsgem)
                    {
                        if (plus2tacticsgem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2tacticsgem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2tacticsgem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1tailoringgem)
                    {
                        if (plus1tailoringgem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1tailoringgem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1tailoringgem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2tailoringgem)
                    {
                        if (plus2tailoringgem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2tailoringgem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2tailoringgem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1tasteidgem)
                    {
                        if (plus1tasteidgem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1tasteidgem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1tasteidgem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2tasteidgem)
                    {
                        if (plus2tasteidgem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2tasteidgem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2tasteidgem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1tinkeringgem)
                    {
                        if (plus1tinkeringgem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1tinkeringgem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1tinkeringgem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2tinkeringgem)
                    {
                        if (plus2tinkeringgem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2tinkeringgem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2tinkeringgem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1trackinggem)
                    {
                        if (plus1trackinggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1trackinggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1trackinggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2trackinggem)
                    {
                        if (plus2trackinggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2trackinggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2trackinggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1veterinarygem)
                    {
                        if (plus1veterinarygem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1veterinarygem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1veterinarygem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2veterinarygem)
                    {
                        if (plus2veterinarygem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2veterinarygem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2veterinarygem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus1wrestlinggem)
                    {
                        if (plus1wrestlinggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus1wrestlinggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus1wrestlinggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus2wrestlinggem)
                    {
                        if (plus2wrestlinggem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus2wrestlinggem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus2wrestlinggem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is nightsightgem)
                    {
                        if (nightsightgem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((nightsightgem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            nightsightgem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is SpellChannelingGem)
                    {
                        if (SpellChannelingGem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((SpellChannelingGem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            SpellChannelingGem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is plus0skilleracegem)
                    {
                        if (plus0skilleracegem + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((plus0skilleracegem + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            plus0skilleracegem += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is ColdResistSewingKit)
                    {
                        if (ColdResistSewingKit + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((ColdResistSewingKit + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            ColdResistSewingKit += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is EnergyResistSewingKit)
                    {
                        if (EnergyResistSewingKit + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((EnergyResistSewingKit + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            EnergyResistSewingKit += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is FireResistSewingKit)
                    {
                        if (FireResistSewingKit + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((FireResistSewingKit + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            FireResistSewingKit += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
                    else if (curItem is PoisonResistSewingKit)
                    {
                        if (PoisonResistSewingKit + curItem.Amount > StorageLimit)
                            from.SendMessage("You are trying to add " + ((PoisonResistSewingKit + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
                        else
                        {
                            PoisonResistSewingKit += curItem.Amount;
                            curItem.Delete();
                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
                            BeginCombine(from);
                        }
                    }
//                    else if (curItem is CopperIngot)
//                    {
//                        if (Copper + curItem.Amount > StorageLimit)
//                            from.SendMessage("You are trying to add " + ((Copper + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
//                        else
//                        {
//                            Copper += curItem.Amount;
//                            curItem.Delete();
//                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
//                            BeginCombine(from);
//                        }
//                    }
//                    else if (curItem is BronzeIngot)
//                    {
//
//                        if (Bronze + curItem.Amount > StorageLimit)
//                            from.SendMessage("You are trying to add " + ((Bronze + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
//                        else
//                        {
//                            Bronze += curItem.Amount;
//                            curItem.Delete();
//                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
//                            BeginCombine(from);
//                        }
//                    }
//                    else if (curItem is GoldIngot)
//                    {
//                        if (Gold + curItem.Amount > StorageLimit)
//                            from.SendMessage("You are trying to add " + ((Gold + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
//                        else
//                        {
//                            Gold += curItem.Amount;
//                            curItem.Delete();
//                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
//                            BeginCombine(from);
//                        }
//                    }
//                    else if (curItem is AgapiteIngot)
//                    {
//                        if (Agapite + curItem.Amount > StorageLimit)
//                            from.SendMessage("You are trying to add " + ((Agapite + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
//                        else
//                        {
//                            Agapite += curItem.Amount;
//                            curItem.Delete();
//                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
//                            BeginCombine(from);
//                        }
//                    }
//                    else if (curItem is VeriteIngot)
//                    {
//
//                        if (Verite + curItem.Amount > StorageLimit)
//                            from.SendMessage("You are trying to add " + ((Verite + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
//                        else
//                        {
//                            Verite += curItem.Amount;
//                            curItem.Delete();
//                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
//                            BeginCombine(from);
//                        }
//                    }
//                    else if (curItem is ValoriteIngot)
//                    {
//                        if (Valorite + curItem.Amount > StorageLimit)
//                            from.SendMessage("You are trying to add " + ((Valorite + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
//                        else
//                        {
//                            Valorite += curItem.Amount;
//                            curItem.Delete();
//                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
//                            BeginCombine(from);
//                        }
//                    }
//                    else if (curItem is FluoriteIngot)
//                    {
//                        if (Fluorite + curItem.Amount > StorageLimit)
//                            from.SendMessage("You are trying to add " + ((Fluorite + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
//                        else
//                        {
//                            Fluorite += curItem.Amount;
//                            curItem.Delete();
//                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
//                            BeginCombine(from);
//                        }
//                    }
//                    else if (curItem is PlatinumIngot)
//                    {
//                        if (Platinum + curItem.Amount > StorageLimit)
//                            from.SendMessage("You are trying to add " + ((Platinum + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
//                        else
//                        {
//                            Platinum += curItem.Amount;
//                            curItem.Delete();
//                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
//                            BeginCombine(from);
//                        }
//                    }
                }
//                else if (curItem is BaseScales)
//                {
//                    if (curItem is BlueScales)
//                    {
//                        if (BlueScales + curItem.Amount > StorageLimit)
//                            from.SendMessage("You are trying to add " + ((BlueScales + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
//                        else
//                        {
//                            BlueScales += curItem.Amount;
//                            curItem.Delete();
//                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
//                            BeginCombine(from);
//                        }
//                    }
//                    else if (curItem is RedScales)
//                    {
//                        if (RedScales + curItem.Amount > StorageLimit)
//                            from.SendMessage("You are trying to add " + ((RedScales + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
//                        else
//                        {
//                            RedScales += curItem.Amount;
//                            curItem.Delete();
//                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
//                            BeginCombine(from);
//                        }
//                    }
//                    else if (curItem is YellowScales)
//                    {
//                        if (YellowScales + curItem.Amount > StorageLimit)
//                            from.SendMessage("You are trying to add " + ((YellowScales + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
//                        else
//                        {
//                            YellowScales += curItem.Amount;
//                            curItem.Delete();
//                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
//                            BeginCombine(from);
//                        }
//                    }
//                    else if (curItem is WhiteScales)
//                    {
//                        if (WhiteScales + curItem.Amount > StorageLimit)
//                            from.SendMessage("You are trying to add " + ((WhiteScales + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
//                        else
//                        {
//                            WhiteScales += curItem.Amount;
//                            curItem.Delete();
//                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
//                            BeginCombine(from);
//                        }
//                    }
//                    else if (curItem is BlackScales)
//                    {
//                        if (BlackScales + curItem.Amount > StorageLimit)
//                            from.SendMessage("You are trying to add " + ((BlackScales + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
//                        else
//                        {
//                            BlackScales += curItem.Amount;
//                            curItem.Delete();
//                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
//                            BeginCombine(from);
//                        }
//                    }
//                    else if (curItem is GreenScales)
//                    {
//                        if (GreenScales + curItem.Amount > StorageLimit)
//                            from.SendMessage("You are trying to add " + ((GreenScales + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
//                        else
//                        {
//                            GreenScales += curItem.Amount;
//                            curItem.Delete();
//                            from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
//                            BeginCombine(from);
//                        }
//                    }
//                }
//                else if (curItem is Sand)
//                {
//                	if (Sand + curItem.Amount > StorageLimit)
//                            from.SendMessage("You are trying to add " + ((Sand + curItem.Amount) - m_StorageLimit) + " too much! The warehouse can store only " + m_StorageLimit + " of this resource.");
//                	else
//                	{
//                		Sand += curItem.Amount;
//                		curItem.Delete();
//                		from.SendGump(new ResourceStorageKeyBonusGemsGump((PlayerMobile)from, this));
//                		BeginCombine(from);
//                	}
//                }
            }
            else
            {
                from.SendLocalizedMessage(1045158); // You must have the item in your backpack to target it.
            }
        }
        public ResourceStorageKeyBonusGems(Serial serial)
            : base(serial)
        {
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)2); // version
            
            writer.Write((int)m_plus1alchemygem);
            writer.Write((int)m_plus2alchemygem);
            writer.Write((int)m_plus1anatomygem);
            writer.Write((int)m_plus2anatomygem);
            writer.Write((int)m_plus1animalloregem);
            writer.Write((int)m_plus2animalloregem);
            writer.Write((int)m_plus1animaltaminggem);
            writer.Write((int)m_plus2animaltaminggem);
            writer.Write((int)m_plus1archerygem);
            writer.Write((int)m_plus2archerygem);
            writer.Write((int)m_plus1armsloregem);
            writer.Write((int)m_plus2armsloregem);
            writer.Write((int)m_plus1begginggem);
            writer.Write((int)m_plus2begginggem);
            writer.Write((int)m_plus1blacksmithgem);
            writer.Write((int)m_plus2blacksmithgem);
            writer.Write((int)m_plus1bushidogem);
            writer.Write((int)m_plus2bushidogem);
            writer.Write((int)m_plus1campinggem);
            writer.Write((int)m_plus2campinggem);
            writer.Write((int)m_plus1carpentrygem);
            writer.Write((int)m_plus2carpentrygem);
            writer.Write((int)m_plus1cartographygem);
            writer.Write((int)m_plus2cartographygem);
            writer.Write((int)m_plus1chivalrygem);
            writer.Write((int)m_plus2chivalrygem);
            writer.Write((int)m_plus1cookinggem);
            writer.Write((int)m_plus2cookinggem);
            writer.Write((int)m_plus1detecthiddengem);
            writer.Write((int)m_plus2detecthiddengem);
            writer.Write((int)m_plus1discordancegem);
            writer.Write((int)m_plus2discordancegem);
            writer.Write((int)m_plus1evalintgem);
            writer.Write((int)m_plus2evalintgem);
            writer.Write((int)m_plus1fencinggem);
            writer.Write((int)m_plus2fencinggem);
            writer.Write((int)m_plus1fishinggem);
            writer.Write((int)m_plus2fishinggem);
            writer.Write((int)m_plus1fletchinggem);
            writer.Write((int)m_plus2fletchinggem);
            writer.Write((int)m_plus1focusgem);
            writer.Write((int)m_plus2focusgem);
            writer.Write((int)m_plus1forensicsgem);
            writer.Write((int)m_plus2forensicsgem);
            writer.Write((int)m_plus1healinggem);
            writer.Write((int)m_plus2healinggem);
            writer.Write((int)m_plus1herdinggem);
            writer.Write((int)m_plus2herdinggem);
            writer.Write((int)m_plus1hidinggem);
            writer.Write((int)m_plus2hidinggem);
            writer.Write((int)m_plus1inscribegem);
            writer.Write((int)m_plus2inscribegem);
            writer.Write((int)m_plus1itemidgem);
            writer.Write((int)m_plus2itemidgem);
            writer.Write((int)m_plus1lockpickinggem);
            writer.Write((int)m_plus2lockpickinggem);
            writer.Write((int)m_plus1lumberjackinggem);
            writer.Write((int)m_plus2lumberjackinggem);
            writer.Write((int)m_plus1macinggem);
            writer.Write((int)m_plus2macinggem);
            writer.Write((int)m_plus1magerygem);
            writer.Write((int)m_plus2magerygem);
            writer.Write((int)m_plus1magicresistgem);
            writer.Write((int)m_plus2magicresistgem);
            writer.Write((int)m_plus1meditationgem);
            writer.Write((int)m_plus2meditationgem);
            writer.Write((int)m_plus1mininggem);
            writer.Write((int)m_plus2mininggem);
            writer.Write((int)m_plus1musicianshipgem);
            writer.Write((int)m_plus2musicianshipgem);
            writer.Write((int)m_plus1necromancygem);
            writer.Write((int)m_plus2necromancygem);
            writer.Write((int)m_plus1ninjitsugem);
            writer.Write((int)m_plus2ninjitsugem);
            writer.Write((int)m_plus1parrygem);
            writer.Write((int)m_plus2parrygem);
            writer.Write((int)m_plus1peacemakinggem);
            writer.Write((int)m_plus2peacemakinggem);
            writer.Write((int)m_plus1poisoninggem);
            writer.Write((int)m_plus2poisoninggem);
            writer.Write((int)m_plus1provocationgem);
            writer.Write((int)m_plus2provocationgem);
            writer.Write((int)m_plus1removetrapgem);
            writer.Write((int)m_plus2removetrapgem);
            writer.Write((int)m_plus1snoopinggem);
            writer.Write((int)m_plus2snoopinggem);
            writer.Write((int)m_plus1spellweavinggem);
            writer.Write((int)m_plus2spellweavinggem);
            writer.Write((int)m_plus1spiritspeakgem);
            writer.Write((int)m_plus2spiritspeakgem);
            writer.Write((int)m_plus1stealinggem);
            writer.Write((int)m_plus2stealinggem);
            writer.Write((int)m_plus1stealthgem);
            writer.Write((int)m_plus2stealthgem);
            writer.Write((int)m_plus1swordsgem);
            writer.Write((int)m_plus2swordsgem);
            writer.Write((int)m_plus1tacticsgem);
            writer.Write((int)m_plus2tacticsgem);
            writer.Write((int)m_plus1tailoringgem);
            writer.Write((int)m_plus2tailoringgem);
            writer.Write((int)m_plus1tasteidgem);
            writer.Write((int)m_plus2tasteidgem);
            writer.Write((int)m_plus1tinkeringgem);
            writer.Write((int)m_plus2tinkeringgem);
            writer.Write((int)m_plus1trackinggem);
            writer.Write((int)m_plus2trackinggem);
            writer.Write((int)m_plus1veterinarygem);
            writer.Write((int)m_plus2veterinarygem);
            writer.Write((int)m_plus1wrestlinggem);
            writer.Write((int)m_plus2wrestlinggem);

            writer.Write((int)m_nightsightgem);
            writer.Write((int)m_SpellChannelingGem);

            writer.Write((int)m_plus0skilleracegem);

            writer.Write((int)m_ColdResistSewingKit);
            writer.Write((int)m_EnergyResistSewingKit);
            writer.Write((int)m_FireResistSewingKit);
            writer.Write((int)m_PoisonResistSewingKit);


//            writer.Write((int)m_BlackScales);
//            writer.Write((int)m_BlueScales);
//            writer.Write((int)m_YellowScales);
//            writer.Write((int)m_RedScales);
//            writer.Write((int)m_GreenScales);
//            writer.Write((int)m_WhiteScales);

//            writer.Write((int)m_Fluorite);
//            writer.Write((int)m_Platinum);

//            writer.Write((int)m_Iron);
//            writer.Write((int)m_Sand);
//            writer.Write((int)m_DullCopper);
//            writer.Write((int)m_ShadowIron);
//            writer.Write((int)m_Copper);
//            writer.Write((int)m_Bronze);
//            writer.Write((int)m_Gold);
//            writer.Write((int)m_Agapite);
//            writer.Write((int)m_Verite);
//            writer.Write((int)m_Valorite);

            writer.Write((int)m_StorageLimit);
            writer.Write((int)m_WithdrawIncrement);
        }
        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            switch (version)
            {
                case 2:
                    {
//                        m_BlackScales = reader.ReadInt();
//                        m_BlueScales = reader.ReadInt();
//                        m_YellowScales = reader.ReadInt();
//                        m_RedScales = reader.ReadInt();
//                        m_GreenScales = reader.ReadInt();
//                        m_WhiteScales = reader.ReadInt();
                        goto case 1;
                    }
                case 1:
                    {

//                        m_Fluorite = reader.ReadInt();
//                        m_Platinum = reader.ReadInt();
                        goto case 0;
                    }
                case 0:
                    {
//                        m_Iron = reader.ReadInt();
//						  m_Sand = reader.ReadInt();
//                        m_DullCopper = reader.ReadInt();
//                        m_ShadowIron = reader.ReadInt();
//                        m_Copper = reader.ReadInt();
//                        m_Bronze = reader.ReadInt();
//                        m_Gold = reader.ReadInt();
//                        m_Agapite = reader.ReadInt();
//                        m_Verite = reader.ReadInt();
//                        m_Valorite = reader.ReadInt();
//

						m_plus1alchemygem = reader.ReadInt();
						m_plus2alchemygem = reader.ReadInt();
						m_plus1anatomygem = reader.ReadInt();
						m_plus2anatomygem = reader.ReadInt();
						m_plus1animalloregem = reader.ReadInt();
						m_plus2animalloregem = reader.ReadInt();
						m_plus1animaltaminggem = reader.ReadInt();
						m_plus2animaltaminggem = reader.ReadInt();
						m_plus1archerygem = reader.ReadInt();
						m_plus2archerygem = reader.ReadInt();
						m_plus1armsloregem = reader.ReadInt();
						m_plus2armsloregem = reader.ReadInt();
						m_plus1begginggem = reader.ReadInt();
						m_plus2begginggem = reader.ReadInt();
						m_plus1blacksmithgem = reader.ReadInt();
						m_plus2blacksmithgem = reader.ReadInt();
						m_plus1bushidogem = reader.ReadInt();
						m_plus2bushidogem = reader.ReadInt();
						m_plus1campinggem = reader.ReadInt();
						m_plus2campinggem = reader.ReadInt();
						m_plus1carpentrygem = reader.ReadInt();
						m_plus2carpentrygem = reader.ReadInt();
						m_plus1cartographygem = reader.ReadInt();
						m_plus2cartographygem = reader.ReadInt();
						m_plus1chivalrygem = reader.ReadInt();
						m_plus2chivalrygem = reader.ReadInt();
						m_plus1cookinggem = reader.ReadInt();
						m_plus2cookinggem = reader.ReadInt();
						m_plus1detecthiddengem = reader.ReadInt();
						m_plus2detecthiddengem = reader.ReadInt();
						m_plus1discordancegem = reader.ReadInt();
						m_plus2discordancegem = reader.ReadInt();
						m_plus1evalintgem = reader.ReadInt();
						m_plus2evalintgem = reader.ReadInt();
						m_plus1fencinggem = reader.ReadInt();
						m_plus2fencinggem = reader.ReadInt();
						m_plus1fishinggem = reader.ReadInt();
						m_plus2fishinggem = reader.ReadInt();
						m_plus1fletchinggem = reader.ReadInt();
						m_plus2fletchinggem = reader.ReadInt();
						m_plus1focusgem = reader.ReadInt();
						m_plus2focusgem = reader.ReadInt();
						m_plus1forensicsgem = reader.ReadInt();
						m_plus2forensicsgem = reader.ReadInt();
						m_plus1healinggem = reader.ReadInt();
						m_plus2healinggem = reader.ReadInt();
						m_plus1herdinggem = reader.ReadInt();
						m_plus2herdinggem = reader.ReadInt();
						m_plus1hidinggem = reader.ReadInt();
						m_plus2hidinggem = reader.ReadInt();
						m_plus1inscribegem = reader.ReadInt();
						m_plus2inscribegem = reader.ReadInt();
						m_plus1itemidgem = reader.ReadInt();
						m_plus2itemidgem = reader.ReadInt();
						m_plus1lockpickinggem = reader.ReadInt();
						m_plus2lockpickinggem = reader.ReadInt();
						m_plus1lumberjackinggem = reader.ReadInt();
						m_plus2lumberjackinggem = reader.ReadInt();
						m_plus1macinggem = reader.ReadInt();
						m_plus2macinggem = reader.ReadInt();
						m_plus1magerygem = reader.ReadInt();
						m_plus2magerygem = reader.ReadInt();
						m_plus1magicresistgem = reader.ReadInt();
						m_plus2magicresistgem = reader.ReadInt();
						m_plus1meditationgem = reader.ReadInt();
						m_plus2meditationgem = reader.ReadInt();
						m_plus1mininggem = reader.ReadInt();
						m_plus2mininggem = reader.ReadInt();
						m_plus1musicianshipgem = reader.ReadInt();
						m_plus2musicianshipgem = reader.ReadInt();
						m_plus1necromancygem = reader.ReadInt();
						m_plus2necromancygem = reader.ReadInt();
						m_plus1ninjitsugem = reader.ReadInt();
						m_plus2ninjitsugem = reader.ReadInt();
						m_plus1parrygem = reader.ReadInt();
						m_plus2parrygem = reader.ReadInt();
						m_plus1peacemakinggem = reader.ReadInt();
						m_plus2peacemakinggem = reader.ReadInt();
						m_plus1poisoninggem = reader.ReadInt();
						m_plus2poisoninggem = reader.ReadInt();
						m_plus1provocationgem = reader.ReadInt();
						m_plus2provocationgem = reader.ReadInt();
						m_plus1removetrapgem = reader.ReadInt();
						m_plus2removetrapgem = reader.ReadInt();
						m_plus1snoopinggem = reader.ReadInt();
						m_plus2snoopinggem = reader.ReadInt();
						m_plus1spellweavinggem = reader.ReadInt();
						m_plus2spellweavinggem = reader.ReadInt();
						m_plus1spiritspeakgem = reader.ReadInt();
						m_plus2spiritspeakgem = reader.ReadInt();
						m_plus1stealinggem = reader.ReadInt();
						m_plus2stealinggem = reader.ReadInt();
						m_plus1stealthgem = reader.ReadInt();
						m_plus2stealthgem = reader.ReadInt();
						m_plus1swordsgem = reader.ReadInt();
						m_plus2swordsgem = reader.ReadInt();
						m_plus1tacticsgem = reader.ReadInt();
						m_plus2tacticsgem = reader.ReadInt();
						m_plus1tailoringgem = reader.ReadInt();
						m_plus2tailoringgem = reader.ReadInt();
						m_plus1tasteidgem = reader.ReadInt();
						m_plus2tasteidgem = reader.ReadInt();
						m_plus1tinkeringgem = reader.ReadInt();
						m_plus2tinkeringgem = reader.ReadInt();
						m_plus1trackinggem = reader.ReadInt();
						m_plus2trackinggem = reader.ReadInt();
						m_plus1veterinarygem = reader.ReadInt();
						m_plus2veterinarygem = reader.ReadInt();
						m_plus1wrestlinggem = reader.ReadInt();
						m_plus2wrestlinggem = reader.ReadInt();
						m_nightsightgem = reader.ReadInt();
						m_SpellChannelingGem = reader.ReadInt();

						m_plus0skilleracegem = reader.ReadInt();
						
						m_ColdResistSewingKit = reader.ReadInt();
						m_EnergyResistSewingKit = reader.ReadInt();
						m_FireResistSewingKit = reader.ReadInt();
						m_PoisonResistSewingKit = reader.ReadInt();

                        m_StorageLimit = reader.ReadInt();
                        m_WithdrawIncrement = reader.ReadInt();
                        break;
                    }
            }

            LootType = LootType.Blessed;
        }
    }
}

namespace Server.Items
{
    public class ResourceStorageKeyBonusGemsGump : Gump
    {
        private PlayerMobile m_From;
        private ResourceStorageKeyBonusGems m_Key;

        public ResourceStorageKeyBonusGemsGump(PlayerMobile from, ResourceStorageKeyBonusGems key)
            : base(25, 25)
        {
            m_From = from;
            m_Key = key;

            m_From.CloseGump(typeof(ResourceStorageKeyBonusGemsGump));

            AddPage(0);

            AddBackground(50, 10, 640, 480, 5054);
            AddImageTiled(58, 20, 623, 461, 2624);
            AddAlphaRegion(58, 20, 623, 461);

            AddLabel(200, 16, 88, "BONUS GEMS & BONUS SEWING KITS WAREHOUSE");
// Kazuha 
//	    AddLabel(125, 50, 0x486, "Withdraw Increment:");
//	    AddLabel(275, 50, 0x480, key.WithdrawIncrement.ToString());
//	    AddButton(330, 50, 4011, 4012, 26, GumpButtonType.Reply, 0);
//	    AddButton(360, 50, 4011, 4012, 27, GumpButtonType.Reply, 0);
//	    AddButton(390, 50, 4011, 4012, 28, GumpButtonType.Reply, 0);
// /Kazuha
//ROW 1 MAX 34 lines
            AddLabel(107, 31, 0x486, "Alchemy +1");
            AddLabel(75, 31, 0x480, key.plus1alchemygem.ToString());
            AddButton(60, 34, 1209, 1210, 1, GumpButtonType.Reply, 0);

            AddLabel(107, 44, 0x486, "Alchemy +2");
            AddLabel(75, 44, 0x480, key.plus2alchemygem.ToString());
            AddButton(60, 47, 1209, 1210, 2, GumpButtonType.Reply, 0);

            AddLabel(107, 57, 0x486, "Anatomy +1");
            AddLabel(75, 57, 0x480, key.plus1anatomygem.ToString());
            AddButton(60, 60, 1209, 1210, 3, GumpButtonType.Reply, 0);

            AddLabel(107, 70, 0x486, "Anatomy +2");
            AddLabel(75, 70, 0x480, key.plus2anatomygem.ToString());
            AddButton(60, 73, 1209, 1210, 4, GumpButtonType.Reply, 0);

            AddLabel(107, 83, 0x486, "Animal Lore +1");
            AddLabel(75, 83, 0x480, key.plus1animalloregem.ToString());
            AddButton(60, 86, 1209, 1210, 5, GumpButtonType.Reply, 0);

            AddLabel(107, 96, 0x486, "Animal Lore +2");
            AddLabel(75, 96, 0x480, key.plus2animalloregem.ToString());
            AddButton(60, 99, 1209, 1210, 6, GumpButtonType.Reply, 0);

            AddLabel(107, 109, 0x486, "Animal Taming +1");
            AddLabel(75, 109, 0x480, key.plus1animaltaminggem.ToString());
            AddButton(60, 112, 1209, 1210, 7, GumpButtonType.Reply, 0);

            AddLabel(107, 122, 0x486, "Animal Taming +2");
            AddLabel(75, 122, 0x480, key.plus2animaltaminggem.ToString());
            AddButton(60, 125, 1209, 1210, 8, GumpButtonType.Reply, 0);

            AddLabel(107, 135, 0x486, "Archery +1");
            AddLabel(75, 135, 0x480, key.plus1archerygem.ToString());
            AddButton(60, 138, 1209, 1210, 9, GumpButtonType.Reply, 0);

            AddLabel(107, 148, 0x486, "Archery +2");
            AddLabel(75, 148, 0x480, key.plus2archerygem.ToString());
            AddButton(60, 151, 1209, 1210, 10, GumpButtonType.Reply, 0);
            
            AddLabel(107, 161, 0x486, "Arms Lore +1");
            AddLabel(75, 161, 0x480, key.plus1armsloregem.ToString());
            AddButton(60, 164, 1209, 1210, 11, GumpButtonType.Reply, 0);
            
            AddLabel(107, 174, 0x486, "Arms Lore +2");
            AddLabel(75, 174, 0x480, key.plus2armsloregem.ToString());
            AddButton(60, 177, 1209, 1210, 12, GumpButtonType.Reply, 0);
            
            AddLabel(107, 187, 0x486, "Begging +1");
            AddLabel(75, 187, 0x480, key.plus1begginggem.ToString());
            AddButton(60, 190, 1209, 1210, 13, GumpButtonType.Reply, 0);
            
            AddLabel(107, 200, 0x486, "Begging +2");
            AddLabel(75, 200, 0x480, key.plus2begginggem.ToString());
            AddButton(60, 203, 1209, 1210, 14, GumpButtonType.Reply, 0);
            
            AddLabel(107, 213, 0x486, "Blacksmith +1");
            AddLabel(75, 213, 0x480, key.plus1blacksmithgem.ToString());
            AddButton(60, 216, 1209, 1210, 15, GumpButtonType.Reply, 0);
            
            AddLabel(107, 226, 0x486, "Blacksmith +2");
            AddLabel(75, 226, 0x480, key.plus2blacksmithgem.ToString());
            AddButton(60, 229, 1209, 1210, 16, GumpButtonType.Reply, 0);
            
            AddLabel(107, 239, 0x486, "Bushido +1");
            AddLabel(75, 239, 0x480, key.plus1bushidogem.ToString());
            AddButton(60, 242, 1209, 1210, 17, GumpButtonType.Reply, 0);
            
            AddLabel(107, 252, 0x486, "Bushido +2");
            AddLabel(75, 252, 0x480, key.plus2bushidogem.ToString());
            AddButton(60, 255, 1209, 1210, 18, GumpButtonType.Reply, 0);
            
            AddLabel(107, 265, 0x486, "Camping +1");
            AddLabel(75, 265, 0x480, key.plus1campinggem.ToString());
            AddButton(60, 268, 1209, 1210, 19, GumpButtonType.Reply, 0);
            
            AddLabel(107, 278, 0x486, "Camping +2");
            AddLabel(75, 278, 0x480, key.plus2campinggem.ToString());
            AddButton(60, 281, 1209, 1210, 20, GumpButtonType.Reply, 0);
            
            AddLabel(107, 291, 0x486, "Carpentry +1");
            AddLabel(75, 291, 0x480, key.plus1carpentrygem.ToString());
            AddButton(60, 294, 1209, 1210, 21, GumpButtonType.Reply, 0);
            
            AddLabel(107, 304, 0x486, "Carpentry +2");
            AddLabel(75, 304, 0x480, key.plus2carpentrygem.ToString());
            AddButton(60, 307, 1209, 1210, 22, GumpButtonType.Reply, 0);
            
            AddLabel(107, 317, 0x486, "Cartography +1");
            AddLabel(75, 317, 0x480, key.plus1cartographygem.ToString());
            AddButton(60, 320, 1209, 1210, 23, GumpButtonType.Reply, 0);
            
            AddLabel(107, 330, 0x486, "Cartography +2");
            AddLabel(75, 330, 0x480, key.plus2cartographygem.ToString());
            AddButton(60, 333, 1209, 1210, 24, GumpButtonType.Reply, 0);
            
            AddLabel(107, 343, 0x486, "Chivalry +1");
            AddLabel(75, 343, 0x480, key.plus1chivalrygem.ToString());
            AddButton(60, 346, 1209, 1210, 25, GumpButtonType.Reply, 0);
            
            AddLabel(107, 356, 0x486, "Chivalry +2");
            AddLabel(75, 356, 0x480, key.plus2chivalrygem.ToString());
            AddButton(60, 359, 1209, 1210, 26, GumpButtonType.Reply, 0);
            
            AddLabel(107, 369, 0x486, "Cooking +1");
            AddLabel(75, 369, 0x480, key.plus1cookinggem.ToString());
            AddButton(60, 372, 1209, 1210, 27, GumpButtonType.Reply, 0);
            
            AddLabel(107, 382, 0x486, "Cooking +2");
            AddLabel(75, 382, 0x480, key.plus2cookinggem.ToString());
            AddButton(60, 385, 1209, 1210, 28, GumpButtonType.Reply, 0);
            
            AddLabel(107, 395, 0x486, "Detect Hidden +1");
            AddLabel(75, 395, 0x480, key.plus1detecthiddengem.ToString());
            AddButton(60, 398, 1209, 1210, 29, GumpButtonType.Reply, 0);
            
            AddLabel(107, 408, 0x486, "Detect Hidden +2");
            AddLabel(75, 408, 0x480, key.plus2detecthiddengem.ToString());
            AddButton(60, 411, 1209, 1210, 30, GumpButtonType.Reply, 0);
            
            AddLabel(107, 421, 0x486, "Discordance +1");
            AddLabel(75, 421, 0x480, key.plus1discordancegem.ToString());
            AddButton(60, 424, 1209, 1210, 31, GumpButtonType.Reply, 0);
            
            AddLabel(107, 434, 0x486, "Discordance +2");
            AddLabel(75, 434, 0x480, key.plus2discordancegem.ToString());
            AddButton(60, 437, 1209, 1210, 32, GumpButtonType.Reply, 0);
            
            AddLabel(107, 447, 0x486, "Eval Int +1");
            AddLabel(75, 447, 0x480, key.plus1evalintgem.ToString());
            AddButton(60, 450, 1209, 1210, 33, GumpButtonType.Reply, 0);
            
            AddLabel(107, 460, 0x486, "Eval Int +2");
            AddLabel(75, 460, 0x480, key.plus2evalintgem.ToString());
            AddButton(60, 463, 1209, 1210, 34, GumpButtonType.Reply, 0);
            
//ROW 2 MAX 34 lines            
            
            AddLabel(267, 31, 0x486, "Fencing +1");
            AddLabel(235, 31, 0x480, key.plus1fencinggem.ToString());
            AddButton(220, 34, 1209, 1210, 35, GumpButtonType.Reply, 0);

            AddLabel(267, 44, 0x486, "Fencing +2");
            AddLabel(235, 44, 0x480, key.plus2fencinggem.ToString());
            AddButton(220, 47, 1209, 1210, 36, GumpButtonType.Reply, 0);

            AddLabel(267, 57, 0x486, "Fishing +1");
            AddLabel(235, 57, 0x480, key.plus1fishinggem.ToString());
            AddButton(220, 60, 1209, 1210, 37, GumpButtonType.Reply, 0);

            AddLabel(267, 70, 0x486, "Fishing +2");
            AddLabel(235, 70, 0x480, key.plus2fishinggem.ToString());
            AddButton(220, 73, 1209, 1210, 38, GumpButtonType.Reply, 0);

            AddLabel(267, 83, 0x486, "Fletching +1");
            AddLabel(235, 83, 0x480, key.plus1fletchinggem.ToString());
            AddButton(220, 86, 1209, 1210, 39, GumpButtonType.Reply, 0);

            AddLabel(267, 96, 0x486, "Fletching +2");
            AddLabel(235, 96, 0x480, key.plus2fletchinggem.ToString());
            AddButton(220, 99, 1209, 1210, 40, GumpButtonType.Reply, 0);

            AddLabel(267, 109, 0x486, "Focus +1");
            AddLabel(235, 109, 0x480, key.plus1focusgem.ToString());
            AddButton(220, 112, 1209, 1210, 41, GumpButtonType.Reply, 0);

            AddLabel(267, 122, 0x486, "Focus +2");
            AddLabel(235, 122, 0x480, key.plus2focusgem.ToString());
            AddButton(220, 125, 1209, 1210, 42, GumpButtonType.Reply, 0);

            AddLabel(267, 135, 0x486, "Forensics +1");
            AddLabel(235, 135, 0x480, key.plus1forensicsgem.ToString());
            AddButton(220, 138, 1209, 1210, 43, GumpButtonType.Reply, 0);

            AddLabel(267, 148, 0x486, "Forensics +2");
            AddLabel(235, 148, 0x480, key.plus2forensicsgem.ToString());
            AddButton(220, 151, 1209, 1210, 44, GumpButtonType.Reply, 0);
            
            AddLabel(267, 161, 0x486, "Healing +1");
            AddLabel(235, 161, 0x480, key.plus1healinggem.ToString());
            AddButton(220, 164, 1209, 1210, 45, GumpButtonType.Reply, 0);
            
            AddLabel(267, 174, 0x486, "Healing +2");
            AddLabel(235, 174, 0x480, key.plus2healinggem.ToString());
            AddButton(220, 177, 1209, 1210, 46, GumpButtonType.Reply, 0);
            
            AddLabel(267, 187, 0x486, "Herding +1");
            AddLabel(235, 187, 0x480, key.plus1herdinggem.ToString());
            AddButton(220, 190, 1209, 1210, 47, GumpButtonType.Reply, 0);
            
            AddLabel(267, 200, 0x486, "Herding +2");
            AddLabel(235, 200, 0x480, key.plus2herdinggem.ToString());
            AddButton(220, 203, 1209, 1210, 48, GumpButtonType.Reply, 0);
            
            AddLabel(267, 213, 0x486, "Hiding +1");
            AddLabel(235, 213, 0x480, key.plus1hidinggem.ToString());
            AddButton(220, 216, 1209, 1210, 49, GumpButtonType.Reply, 0);
            
            AddLabel(267, 226, 0x486, "Hiding +2");
            AddLabel(235, 226, 0x480, key.plus2hidinggem.ToString());
            AddButton(220, 229, 1209, 1210, 50, GumpButtonType.Reply, 0);
            
            AddLabel(267, 239, 0x486, "Inscription +1");
            AddLabel(235, 239, 0x480, key.plus1inscribegem.ToString());
            AddButton(220, 242, 1209, 1210, 51, GumpButtonType.Reply, 0);
            
            AddLabel(267, 252, 0x486, "Inscription +2");
            AddLabel(235, 252, 0x480, key.plus2inscribegem.ToString());
            AddButton(220, 255, 1209, 1210, 52, GumpButtonType.Reply, 0);
            
            AddLabel(267, 265, 0x486, "Item ID +1");
            AddLabel(235, 265, 0x480, key.plus1itemidgem.ToString());
            AddButton(220, 268, 1209, 1210, 53, GumpButtonType.Reply, 0);
            
            AddLabel(267, 278, 0x486, "Item ID +2");
            AddLabel(235, 278, 0x480, key.plus2itemidgem.ToString());
            AddButton(220, 281, 1209, 1210, 54, GumpButtonType.Reply, 0);
            
            AddLabel(267, 291, 0x486, "Lockpicking +1");
            AddLabel(235, 291, 0x480, key.plus1lockpickinggem.ToString());
            AddButton(220, 294, 1209, 1210, 55, GumpButtonType.Reply, 0);
            
            AddLabel(267, 304, 0x486, "Lockpicking +2");
            AddLabel(235, 304, 0x480, key.plus2lockpickinggem.ToString());
            AddButton(220, 307, 1209, 1210, 56, GumpButtonType.Reply, 0);
            
            AddLabel(267, 317, 0x486, "Lumberjack +1");
            AddLabel(235, 317, 0x480, key.plus1lumberjackinggem.ToString());
            AddButton(220, 320, 1209, 1210, 57, GumpButtonType.Reply, 0);
            
            AddLabel(267, 330, 0x486, "Lumberjacking +2");
            AddLabel(235, 330, 0x480, key.plus2lumberjackinggem.ToString());
            AddButton(220, 333, 1209, 1210, 58, GumpButtonType.Reply, 0);
            
            AddLabel(267, 343, 0x486, "Mace Fighting +1");
            AddLabel(235, 343, 0x480, key.plus1macinggem.ToString());
            AddButton(220, 346, 1209, 1210, 59, GumpButtonType.Reply, 0);
            
            AddLabel(267, 356, 0x486, "Mace Fighting +2");
            AddLabel(235, 356, 0x480, key.plus2macinggem.ToString());
            AddButton(220, 359, 1209, 1210, 60, GumpButtonType.Reply, 0);
            
            AddLabel(267, 369, 0x486, "Magery +1");
            AddLabel(235, 369, 0x480, key.plus1magerygem.ToString());
            AddButton(220, 372, 1209, 1210, 61, GumpButtonType.Reply, 0);
            
            AddLabel(267, 382, 0x486, "Magery +2");
            AddLabel(235, 382, 0x480, key.plus2magerygem.ToString());
            AddButton(220, 385, 1209, 1210, 62, GumpButtonType.Reply, 0);
            
            AddLabel(267, 395, 0x486, "Magic Resist +1");
            AddLabel(235, 395, 0x480, key.plus1magicresistgem.ToString());
            AddButton(220, 398, 1209, 1210, 63, GumpButtonType.Reply, 0);
            
            AddLabel(267, 408, 0x486, "Magic Resist +2");
            AddLabel(235, 408, 0x480, key.plus2magicresistgem.ToString());
            AddButton(220, 411, 1209, 1210, 64, GumpButtonType.Reply, 0);
            
            AddLabel(267, 421, 0x486, "Meditation +1");
            AddLabel(235, 421, 0x480, key.plus1meditationgem.ToString());
            AddButton(220, 424, 1209, 1210, 65, GumpButtonType.Reply, 0);
            
            AddLabel(267, 434, 0x486, "Meditation +2");
            AddLabel(235, 434, 0x480, key.plus2meditationgem.ToString());
            AddButton(220, 437, 1209, 1210, 66, GumpButtonType.Reply, 0);
            
            AddLabel(267, 447, 0x486, "Mining +1");
            AddLabel(235, 447, 0x480, key.plus1mininggem.ToString());
            AddButton(220, 450, 1209, 1210, 67, GumpButtonType.Reply, 0);
            
            AddLabel(267, 460, 0x486, "Mining +2");
            AddLabel(235, 460, 0x480, key.plus2mininggem.ToString());
            AddButton(220, 463, 1209, 1210, 68, GumpButtonType.Reply, 0);
            
            
            
                        
//ROW 3 MAX 34 lines
            
            AddLabel(427, 31, 0x486, "Musicianship +1");
            AddLabel(395, 31, 0x480, key.plus1musicianshipgem.ToString());
            AddButton(380, 34, 1209, 1210, 69, GumpButtonType.Reply, 0);

            AddLabel(427, 44, 0x486, "Musicianship +2");
            AddLabel(395, 44, 0x480, key.plus2musicianshipgem.ToString());
            AddButton(380, 47, 1209, 1210, 70, GumpButtonType.Reply, 0);

            AddLabel(427, 57, 0x486, "Necromancy +1");
            AddLabel(395, 57, 0x480, key.plus1necromancygem.ToString());
            AddButton(380, 60, 1209, 1210, 71, GumpButtonType.Reply, 0);

            AddLabel(427, 70, 0x486, "Necromancy +2");
            AddLabel(395, 70, 0x480, key.plus2necromancygem.ToString());
            AddButton(380, 73, 1209, 1210, 72, GumpButtonType.Reply, 0);

            AddLabel(427, 83, 0x486, "Ninjitsu +1");
            AddLabel(395, 83, 0x480, key.plus1ninjitsugem.ToString());
            AddButton(380, 86, 1209, 1210, 73, GumpButtonType.Reply, 0);

            AddLabel(427, 96, 0x486, "Ninjitsu +2");
            AddLabel(395, 96, 0x480, key.plus2ninjitsugem.ToString());
            AddButton(380, 99, 1209, 1210, 74, GumpButtonType.Reply, 0);

            AddLabel(427, 109, 0x486, "Parrying +1");
            AddLabel(395, 109, 0x480, key.plus1parrygem.ToString());
            AddButton(380, 112, 1209, 1210, 75, GumpButtonType.Reply, 0);

            AddLabel(427, 122, 0x486, "Parrying +2");
            AddLabel(395, 122, 0x480, key.plus2parrygem.ToString());
            AddButton(380, 125, 1209, 1210, 76, GumpButtonType.Reply, 0);

            AddLabel(427, 135, 0x486, "Peacemaking +1");
            AddLabel(395, 135, 0x480, key.plus1peacemakinggem.ToString());
            AddButton(380, 138, 1209, 1210, 77, GumpButtonType.Reply, 0);

            AddLabel(427, 148, 0x486, "Peacemaking +2");
            AddLabel(395, 148, 0x480, key.plus2peacemakinggem.ToString());
            AddButton(380, 151, 1209, 1210, 78, GumpButtonType.Reply, 0);
            
            AddLabel(427, 161, 0x486, "Poisoning +1");
            AddLabel(395, 161, 0x480, key.plus1poisoninggem.ToString());
            AddButton(380, 164, 1209, 1210, 79, GumpButtonType.Reply, 0);
            
            AddLabel(427, 174, 0x486, "Poisoning +2");
            AddLabel(395, 174, 0x480, key.plus2poisoninggem.ToString());
            AddButton(380, 177, 1209, 1210, 80, GumpButtonType.Reply, 0);
            
            AddLabel(427, 187, 0x486, "Provocation +1");
            AddLabel(395, 187, 0x480, key.plus1provocationgem.ToString());
            AddButton(380, 190, 1209, 1210, 81, GumpButtonType.Reply, 0);
            
            AddLabel(427, 200, 0x486, "Provocation +2");
            AddLabel(395, 200, 0x480, key.plus2provocationgem.ToString());
            AddButton(380, 203, 1209, 1210, 82, GumpButtonType.Reply, 0);
            
            AddLabel(427, 213, 0x486, "Remove Trap +1");
            AddLabel(395, 213, 0x480, key.plus1removetrapgem.ToString());
            AddButton(380, 216, 1209, 1210, 83, GumpButtonType.Reply, 0);
            
            AddLabel(427, 226, 0x486, "Remove Trap +2");
            AddLabel(395, 226, 0x480, key.plus2removetrapgem.ToString());
            AddButton(380, 229, 1209, 1210, 84, GumpButtonType.Reply, 0);
            
            AddLabel(427, 239, 0x486, "Snooping +1");
            AddLabel(395, 239, 0x480, key.plus1snoopinggem.ToString());
            AddButton(380, 242, 1209, 1210, 85, GumpButtonType.Reply, 0);
            
            AddLabel(427, 252, 0x486, "Snooping +2");
            AddLabel(395, 252, 0x480, key.plus2snoopinggem.ToString());
            AddButton(380, 255, 1209, 1210, 86, GumpButtonType.Reply, 0);
            
            AddLabel(427, 265, 0x486, "Spellweaving +1");
            AddLabel(395, 265, 0x480, key.plus1spellweavinggem.ToString());
            AddButton(380, 268, 1209, 1210, 87, GumpButtonType.Reply, 0);
            
            AddLabel(427, 278, 0x486, "Spellweaving +2");
            AddLabel(395, 278, 0x480, key.plus2spellweavinggem.ToString());
            AddButton(380, 281, 1209, 1210, 88, GumpButtonType.Reply, 0);
            
            AddLabel(427, 291, 0x486, "Spiritspeak +1");
            AddLabel(395, 291, 0x480, key.plus1spiritspeakgem.ToString());
            AddButton(380, 294, 1209, 1210, 89, GumpButtonType.Reply, 0);
            
            AddLabel(427, 304, 0x486, "Spiritspeak +2");
            AddLabel(395, 304, 0x480, key.plus2spiritspeakgem.ToString());
            AddButton(380, 307, 1209, 1210, 90, GumpButtonType.Reply, 0);
            
            AddLabel(427, 317, 0x486, "Stealing +1");
            AddLabel(395, 317, 0x480, key.plus1stealinggem.ToString());
            AddButton(380, 320, 1209, 1210, 91, GumpButtonType.Reply, 0);
            
            AddLabel(427, 330, 0x486, "Stealing +2");
            AddLabel(395, 330, 0x480, key.plus2stealinggem.ToString());
            AddButton(380, 333, 1209, 1210, 92, GumpButtonType.Reply, 0);
            
            AddLabel(427, 343, 0x486, "Stealth +1");
            AddLabel(395, 343, 0x480, key.plus1stealthgem.ToString());
            AddButton(380, 346, 1209, 1210, 93, GumpButtonType.Reply, 0);
            
            AddLabel(427, 356, 0x486, "Stealth +2");
            AddLabel(395, 356, 0x480, key.plus2stealthgem.ToString());
            AddButton(380, 359, 1209, 1210, 94, GumpButtonType.Reply, 0);
            
            AddLabel(427, 369, 0x486, "Swordsmanship +1");
            AddLabel(395, 369, 0x480, key.plus1swordsgem.ToString());
            AddButton(380, 372, 1209, 1210, 95, GumpButtonType.Reply, 0);
            
            AddLabel(427, 382, 0x486, "Swordsmanship +2");
            AddLabel(395, 382, 0x480, key.plus2swordsgem.ToString());
            AddButton(380, 385, 1209, 1210, 96, GumpButtonType.Reply, 0);
            
            AddLabel(427, 395, 0x486, "Tactics +1");
            AddLabel(395, 395, 0x480, key.plus1tacticsgem.ToString());
            AddButton(380, 398, 1209, 1210, 97, GumpButtonType.Reply, 0);
            
            AddLabel(427, 408, 0x486, "Tactics +2");
            AddLabel(395, 408, 0x480, key.plus2tacticsgem.ToString());
            AddButton(380, 411, 1209, 1210, 98, GumpButtonType.Reply, 0);
            
            AddLabel(427, 421, 0x486, "Tailoring +1");
            AddLabel(395, 421, 0x480, key.plus1tailoringgem.ToString());
            AddButton(380, 424, 1209, 1210, 99, GumpButtonType.Reply, 0);
            
            AddLabel(427, 434, 0x486, "Tailoring +2");
            AddLabel(395, 434, 0x480, key.plus2tailoringgem.ToString());
            AddButton(380, 437, 1209, 1210, 100, GumpButtonType.Reply, 0);
            
            AddLabel(427, 447, 0x486, "Taste ID +1");
            AddLabel(395, 447, 0x480, key.plus1tasteidgem.ToString());
            AddButton(380, 450, 1209, 1210, 101, GumpButtonType.Reply, 0);
            
            AddLabel(427, 460, 0x486, "Taste ID +2");
            AddLabel(395, 460, 0x480, key.plus2tasteidgem.ToString());
            AddButton(380, 463, 1209, 1210, 102, GumpButtonType.Reply, 0);

//ROW 4 MAX 34 lines
            
            AddLabel(587, 31, 0x486, "Tinkering +1");
            AddLabel(555, 31, 0x480, key.plus1tinkeringgem.ToString());
            AddButton(540, 34, 1209, 1210, 103, GumpButtonType.Reply, 0);
            
            AddLabel(587, 44, 0x486, "Tinkering +2");
            AddLabel(555, 44, 0x480, key.plus2tinkeringgem.ToString());
            AddButton(540, 47, 1209, 1210, 104, GumpButtonType.Reply, 0);
          
            AddLabel(587, 57, 0x486, "Tracking +1");
            AddLabel(555, 57, 0x480, key.plus1trackinggem.ToString());
            AddButton(540, 60, 1209, 1210, 105, GumpButtonType.Reply, 0);

            AddLabel(587, 70, 0x486, "Tracking +2");
            AddLabel(555, 70, 0x480, key.plus2trackinggem.ToString());
            AddButton(540, 73, 1209, 1210, 106, GumpButtonType.Reply, 0);

            AddLabel(587, 83, 0x486, "Veterinary +1");
            AddLabel(555, 83, 0x480, key.plus1veterinarygem.ToString());
            AddButton(540, 86, 1209, 1210, 107, GumpButtonType.Reply, 0);

            AddLabel(587, 96, 0x486, "Veterinary +2");
            AddLabel(555, 96, 0x480, key.plus2veterinarygem.ToString());
            AddButton(540, 99, 1209, 1210, 108, GumpButtonType.Reply, 0);

            AddLabel(587, 109, 0x486, "Wrestling +1");
            AddLabel(555, 109, 0x480, key.plus1wrestlinggem.ToString());
            AddButton(540, 112, 1209, 1210, 109, GumpButtonType.Reply, 0);

            AddLabel(587, 122, 0x486, "Wrestling +2");
            AddLabel(555, 122, 0x480, key.plus2wrestlinggem.ToString());
            AddButton(540, 125, 1209, 1210, 110, GumpButtonType.Reply, 0);

            AddLabel(587, 135, 0x486, "Nightsight");
            AddLabel(555, 135, 0x480, key.nightsightgem.ToString());
            AddButton(540, 138, 1209, 1210, 111, GumpButtonType.Reply, 0);

            AddLabel(587, 148, 0x486, "Spell Channeling");
            AddLabel(555, 148, 0x480, key.SpellChannelingGem.ToString());
            AddButton(540, 151, 1209, 1210, 112, GumpButtonType.Reply, 0);

            AddLabel(587, 161, 0x486, "Skills Erace");
            AddLabel(555, 161, 0x480, key.plus0skilleracegem.ToString());
            AddButton(540, 164, 1209, 1210, 113, GumpButtonType.Reply, 0);

            AddLabel(587, 174, 0x486, "Cold R.S.K.");
            AddLabel(555, 174, 0x480, key.ColdResistSewingKit.ToString());
            AddButton(540, 177, 1209, 1210, 114, GumpButtonType.Reply, 0);
            
            AddLabel(587, 187, 0x486, "Energy R.S.K.");
            AddLabel(555, 187, 0x480, key.EnergyResistSewingKit.ToString());
            AddButton(540, 190, 1209, 1210, 115, GumpButtonType.Reply, 0);
            
            AddLabel(587, 200, 0x486, "Fire R.S.K.");
            AddLabel(555, 200, 0x480, key.FireResistSewingKit.ToString());
            AddButton(540, 203, 1209, 1210, 116, GumpButtonType.Reply, 0);
            
            AddLabel(587, 213, 0x486, "Poison R.S.K.");
            AddLabel(555, 213, 0x480, key.PoisonResistSewingKit.ToString());
            AddButton(540, 216, 1209, 1210, 117, GumpButtonType.Reply, 0);
            
            
//            AddLabel(587, 226, 0x486, "Blacksmith +2");
//            AddLabel(555, 226, 0x480, key.plus2blacksmithgem.ToString());
//            AddButton(540, 229, 1209, 1210, 118, GumpButtonType.Reply, 0);
//            
//            AddLabel(587, 239, 0x486, "Bushido +1");
//            AddLabel(555, 239, 0x480, key.plus1bushidogem.ToString());
//            AddButton(540, 242, 1209, 1210, 119, GumpButtonType.Reply, 0);
//            
//            AddLabel(587, 252, 0x486, "Bushido +2");
//            AddLabel(555, 252, 0x480, key.plus2bushidogem.ToString());
//            AddButton(540, 255, 1209, 1210, 120, GumpButtonType.Reply, 0);
//            
//            AddLabel(587, 265, 0x486, "Camping +1");
//            AddLabel(555, 265, 0x480, key.plus1campinggem.ToString());
//            AddButton(540, 268, 1209, 1210, 121, GumpButtonType.Reply, 0);
//            
//            AddLabel(587, 278, 0x486, "Camping +2");
//            AddLabel(555, 278, 0x480, key.plus2campinggem.ToString());
//            AddButton(540, 281, 1209, 1210, 122, GumpButtonType.Reply, 0);
//            
//            AddLabel(587, 291, 0x486, "Carpentry +1");
//            AddLabel(555, 291, 0x480, key.plus1carpentrygem.ToString());
//            AddButton(540, 294, 1209, 1210, 123, GumpButtonType.Reply, 0);
//            
//            AddLabel(587, 304, 0x486, "Carpentry +2");
//            AddLabel(555, 304, 0x480, key.plus2carpentrygem.ToString());
//            AddButton(540, 307, 1209, 1210, 124, GumpButtonType.Reply, 0);
//            
//            AddLabel(587, 317, 0x486, "Cartography +1");
//            AddLabel(555, 317, 0x480, key.plus1cartographygem.ToString());
//            AddButton(540, 320, 1209, 1210, 125, GumpButtonType.Reply, 0);
//            
//            AddLabel(587, 330, 0x486, "Cartography +2");
//            AddLabel(555, 330, 0x480, key.plus2cartographygem.ToString());
//            AddButton(540, 333, 1209, 1210, 126, GumpButtonType.Reply, 0);
//            
//            AddLabel(587, 343, 0x486, "Chivalry +1");
//            AddLabel(555, 343, 0x480, key.plus1chivalrygem.ToString());
//            AddButton(540, 346, 1209, 1210, 127, GumpButtonType.Reply, 0);
//            
//            AddLabel(587, 356, 0x486, "Chivalry +2");
//            AddLabel(555, 356, 0x480, key.plus2chivalrygem.ToString());
//            AddButton(540, 359, 1209, 1210, 128, GumpButtonType.Reply, 0);
//            
//            AddLabel(587, 369, 0x486, "Cooking +1");
//            AddLabel(555, 369, 0x480, key.plus1cookinggem.ToString());
//            AddButton(540, 372, 1209, 1210, 129, GumpButtonType.Reply, 0);
//            
//            AddLabel(587, 382, 0x486, "Cooking +2");
//            AddLabel(555, 382, 0x480, key.plus2cookinggem.ToString());
//            AddButton(540, 385, 1209, 1210, 130, GumpButtonType.Reply, 0);
//            
//            AddLabel(587, 395, 0x486, "Detect Hidden +1");
//            AddLabel(555, 395, 0x480, key.plus1detecthiddengem.ToString());
//            AddButton(540, 398, 1209, 1210, 131, GumpButtonType.Reply, 0);
//            
//            AddLabel(587, 408, 0x486, "Detect Hidden +2");
//            AddLabel(555, 408, 0x480, key.plus2detecthiddengem.ToString());
//            AddButton(540, 411, 1209, 1210, 132, GumpButtonType.Reply, 0);
//            
//            AddLabel(587, 421, 0x486, "Discordance +1");
//            AddLabel(555, 421, 0x480, key.plus1discordancegem.ToString());
//            AddButton(540, 424, 1209, 1210, 133, GumpButtonType.Reply, 0);
//            
//            AddLabel(587, 434, 0x486, "Discordance +2");
//            AddLabel(555, 434, 0x480, key.plus2discordancegem.ToString());
//            AddButton(540, 437, 1209, 1210, 134, GumpButtonType.Reply, 0);
//            
//            AddLabel(587, 447, 0x486, "Eval Int +1");
//            AddLabel(555, 447, 0x480, key.plus1evalintgem.ToString());
//            AddButton(540, 450, 1209, 1210, 135, GumpButtonType.Reply, 0);
//            
//            AddLabel(587, 460, 0x486, "Eval Int +2");
//            AddLabel(555, 460, 0x480, key.plus2evalintgem.ToString());
//            AddButton(540, 463, 1209, 1210, 136, GumpButtonType.Reply, 0);
            
//            AddLabel(480, 37, 0x486, "0 Marker to check next row");
            
            
            
            
            
            AddLabel(75, 16, 38, "Add individually.");
            AddButton(60, 19, 1209, 1210, 137, GumpButtonType.Reply, 0);

            AddLabel(555, 16, 38, "Collect all from bag.");
            AddButton(540, 19, 1209, 1210, 138, GumpButtonType.Reply, 0);
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            if (m_Key.Deleted)
                return;
            else if (!m_Key.IsChildOf(m_From.Backpack) && !m_Key.IsChildOf(m_From.BankBox))
            {
                m_From.SendMessage("This must be in your backpack or bankbox to use.");
                return;
            }
//	    else if (info.ButtonID == 26)
//	    {
//		m_Key.WithdrawIncrement = 100;
//		m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
//	    }
//            else if (info.ButtonID == 27)
//            {
//                m_Key.WithdrawIncrement = 500;
//                m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
//            }
//            else if (info.ButtonID == 28)
//            {
//                m_Key.WithdrawIncrement = 1000;
//                m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
//            }
            else if (info.ButtonID == 1)
            {
                if (m_Key.plus1alchemygem > 0)								//if the key currently holds more ot this type than the increment amount
                {
                    m_From.AddToBackpack(new plus1alchemygem());  	//Send the increment amount of this type to players backpack
                    m_Key.plus1alchemygem = (m_Key.plus1alchemygem - 1);				//removes that many from the keys count
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else if (m_Key.plus1alchemygem > 0)
                {
                    m_From.AddToBackpack(new plus1alchemygem(m_Key.plus1alchemygem));  					//Sends all stored ingots of whichever type to players backpack
                    m_Key.plus1alchemygem = 0;						     						//Sets the count in the key back to 0
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));					//Resets the gump with the new info
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");			//Tell the player he is barking up the wrong tree
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));  				//Resets the gump 
                    m_Key.BeginCombine(m_From);										//Send the player a new in-game target in case more resources are to be added
                }
            }
            else if (info.ButtonID == 2)
            {
                if (m_Key.plus2alchemygem > 0)
                {
                    m_From.AddToBackpack(new plus2alchemygem());
                    m_Key.plus2alchemygem = (m_Key.plus2alchemygem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2alchemygem > 0)
                {
                    m_From.AddToBackpack(new plus2alchemygem(m_Key.plus2alchemygem));
                    m_Key.plus2alchemygem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 3)
            {
                if (m_Key.plus1anatomygem > 0)
                {
                    m_From.AddToBackpack(new plus1anatomygem());
                    m_Key.plus1anatomygem = (m_Key.plus1anatomygem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1anatomygem > 0)
                {
                    m_From.AddToBackpack(new plus1anatomygem(m_Key.plus1anatomygem));
                    m_Key.plus1anatomygem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 4)
            {
                if (m_Key.plus2anatomygem > 0)
                {
                    m_From.AddToBackpack(new plus2anatomygem());
                    m_Key.plus2anatomygem = (m_Key.plus2anatomygem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2anatomygem > 0)
                {
                    m_From.AddToBackpack(new plus2anatomygem(m_Key.plus2anatomygem));
                    m_Key.plus2anatomygem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 5)
            {
                if (m_Key.plus1animalloregem > 0)
                {
                    m_From.AddToBackpack(new plus1animalloregem());
                    m_Key.plus1animalloregem = (m_Key.plus1animalloregem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1animalloregem > 0)
                {
                    m_From.AddToBackpack(new plus1animalloregem(m_Key.plus1animalloregem));
                    m_Key.plus1animalloregem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 6)
            {
                if (m_Key.plus2animalloregem > 0)
                {
                    m_From.AddToBackpack(new plus2animalloregem());
                    m_Key.plus2animalloregem = (m_Key.plus2animalloregem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2animalloregem > 0)
                {
                    m_From.AddToBackpack(new plus2animalloregem(m_Key.plus2animalloregem));
                    m_Key.plus2animalloregem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 7)
            {
                if (m_Key.plus1animaltaminggem > 0)
                {
                    m_From.AddToBackpack(new plus1animaltaminggem());
                    m_Key.plus1animaltaminggem = (m_Key.plus1animaltaminggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1animaltaminggem > 0)
                {
                    m_From.AddToBackpack(new plus1animaltaminggem(m_Key.plus1animaltaminggem));
                    m_Key.plus1animaltaminggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 8)
            {
                if (m_Key.plus2animaltaminggem > 0)
                {
                    m_From.AddToBackpack(new plus2animaltaminggem());
                    m_Key.plus2animaltaminggem = (m_Key.plus2animaltaminggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2animaltaminggem > 0)
                {
                    m_From.AddToBackpack(new plus2animaltaminggem(m_Key.plus2animaltaminggem));
                    m_Key.plus2animaltaminggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 9)
            {
                if (m_Key.plus1archerygem > 0)
                {
                    m_From.AddToBackpack(new plus1archerygem());
                    m_Key.plus1archerygem = (m_Key.plus1archerygem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1archerygem > 0)
                {
                    m_From.AddToBackpack(new plus1archerygem(m_Key.plus1archerygem));
                    m_Key.plus1archerygem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
                }
            }
            else if (info.ButtonID == 10)
            {
                if (m_Key.plus2archerygem > 0)
                {
                    m_From.AddToBackpack(new plus2archerygem());
                    m_Key.plus2archerygem = (m_Key.plus2archerygem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2archerygem > 0)
                {
                    m_From.AddToBackpack(new plus2archerygem(m_Key.plus2archerygem));
                    m_Key.plus2archerygem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 11)
            {
                if (m_Key.plus1armsloregem > 0)
                {
                    m_From.AddToBackpack(new plus1armsloregem());
                    m_Key.plus1armsloregem = (m_Key.plus1armsloregem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1armsloregem > 0)
                {
                    m_From.AddToBackpack(new plus2archerygem(m_Key.plus1armsloregem));
                    m_Key.plus1armsloregem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 12)
            {
                if (m_Key.plus2armsloregem > 0)
                {
                    m_From.AddToBackpack(new plus2armsloregem());
                    m_Key.plus2armsloregem = (m_Key.plus2armsloregem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2armsloregem > 0)
                {
                    m_From.AddToBackpack(new plus2armsloregem(m_Key.plus2armsloregem));
                    m_Key.plus2armsloregem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 13)
            {
                if (m_Key.plus1begginggem > 0)
                {
                    m_From.AddToBackpack(new plus1begginggem());
                    m_Key.plus1begginggem = (m_Key.plus1begginggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1begginggem > 0)
                {
                    m_From.AddToBackpack(new plus1begginggem(m_Key.plus1begginggem));
                    m_Key.plus1begginggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 14)
            {
                if (m_Key.plus2begginggem > 0)
                {
                    m_From.AddToBackpack(new plus2begginggem());
                    m_Key.plus2begginggem = (m_Key.plus2begginggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2begginggem > 0)
                {
                    m_From.AddToBackpack(new plus2begginggem(m_Key.plus2begginggem));
                    m_Key.plus2begginggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 15)
            {
                if (m_Key.plus1blacksmithgem > 0)
                {
                    m_From.AddToBackpack(new plus1blacksmithgem());
                    m_Key.plus1blacksmithgem = (m_Key.plus1blacksmithgem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1blacksmithgem > 0)
                {
                    m_From.AddToBackpack(new plus1blacksmithgem(m_Key.plus1blacksmithgem));
                    m_Key.plus1blacksmithgem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 16)
            {
                if (m_Key.plus2blacksmithgem > 0)
                {
                    m_From.AddToBackpack(new plus2blacksmithgem());
                    m_Key.plus2blacksmithgem = (m_Key.plus2blacksmithgem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2blacksmithgem > 0)
                {
                    m_From.AddToBackpack(new plus2blacksmithgem(m_Key.plus2blacksmithgem));
                    m_Key.plus2blacksmithgem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 17)
            {
                if (m_Key.plus1bushidogem > 0)
                {
                    m_From.AddToBackpack(new plus1bushidogem());
                    m_Key.plus1bushidogem = (m_Key.plus1bushidogem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1bushidogem > 0)
                {
                    m_From.AddToBackpack(new plus1bushidogem(m_Key.plus1bushidogem));
                    m_Key.plus1bushidogem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 18)
            {
                if (m_Key.plus2bushidogem > 0)
                {
                    m_From.AddToBackpack(new plus2bushidogem());
                    m_Key.plus2bushidogem = (m_Key.plus2bushidogem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2bushidogem > 0)
                {
                    m_From.AddToBackpack(new plus2bushidogem(m_Key.plus2bushidogem));
                    m_Key.plus2bushidogem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 19)
            {
                if (m_Key.plus1campinggem > 0)
                {
                    m_From.AddToBackpack(new plus1campinggem());
                    m_Key.plus1campinggem = (m_Key.plus1campinggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1campinggem > 0)
                {
                    m_From.AddToBackpack(new plus1campinggem(m_Key.plus1campinggem));
                    m_Key.plus1campinggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 20)
            {
                if (m_Key.plus2campinggem > 0)
                {
                    m_From.AddToBackpack(new plus2campinggem());
                    m_Key.plus2campinggem = (m_Key.plus2campinggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2campinggem > 0)
                {
                    m_From.AddToBackpack(new plus2campinggem(m_Key.plus2campinggem));
                    m_Key.plus2campinggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 21)
            {
                if (m_Key.plus1carpentrygem > 0)
                {
                    m_From.AddToBackpack(new plus1carpentrygem());
                    m_Key.plus1carpentrygem = (m_Key.plus1carpentrygem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1carpentrygem > 0)
                {
                    m_From.AddToBackpack(new plus1carpentrygem(m_Key.plus1carpentrygem));
                    m_Key.plus1carpentrygem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 22)
            {
                if (m_Key.plus2carpentrygem > 0)
                {
                    m_From.AddToBackpack(new plus2carpentrygem());
                    m_Key.plus2carpentrygem = (m_Key.plus2carpentrygem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2carpentrygem > 0)
                {
                    m_From.AddToBackpack(new plus2carpentrygem(m_Key.plus2carpentrygem));
                    m_Key.plus2carpentrygem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 23)
            {
                if (m_Key.plus1cartographygem > 0)
                {
                    m_From.AddToBackpack(new plus1cartographygem());
                    m_Key.plus1cartographygem = (m_Key.plus1cartographygem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1cartographygem > 0)
                {
                    m_From.AddToBackpack(new plus1cartographygem(m_Key.plus1cartographygem));
                    m_Key.plus1cartographygem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 24)
            {
                if (m_Key.plus2cartographygem > 0)
                {
                    m_From.AddToBackpack(new plus2cartographygem());
                    m_Key.plus2cartographygem = (m_Key.plus2cartographygem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2cartographygem > 0)
                {
                    m_From.AddToBackpack(new plus2cartographygem(m_Key.plus2cartographygem));
                    m_Key.plus2cartographygem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 25)
            {
                if (m_Key.plus1chivalrygem > 0)
                {
                    m_From.AddToBackpack(new plus1chivalrygem());
                    m_Key.plus1chivalrygem = (m_Key.plus1chivalrygem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1chivalrygem > 0)
                {
                    m_From.AddToBackpack(new plus1chivalrygem(m_Key.plus1chivalrygem));
                    m_Key.plus1chivalrygem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 26)
            {
                if (m_Key.plus2chivalrygem > 0)
                {
                    m_From.AddToBackpack(new plus2chivalrygem());
                    m_Key.plus2chivalrygem = (m_Key.plus2chivalrygem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2chivalrygem > 0)
                {
                    m_From.AddToBackpack(new plus2chivalrygem(m_Key.plus2chivalrygem));
                    m_Key.plus2chivalrygem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 27)
            {
                if (m_Key.plus1cookinggem > 0)
                {
                    m_From.AddToBackpack(new plus1cookinggem());
                    m_Key.plus1cookinggem = (m_Key.plus1cookinggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1cookinggem > 0)
                {
                    m_From.AddToBackpack(new plus1cookinggem(m_Key.plus1cookinggem));
                    m_Key.plus1cookinggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 28)
            {
                if (m_Key.plus2cookinggem > 0)
                {
                    m_From.AddToBackpack(new plus2cookinggem());
                    m_Key.plus2cookinggem = (m_Key.plus2cookinggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2cookinggem > 0)
                {
                    m_From.AddToBackpack(new plus2cookinggem(m_Key.plus2cookinggem));
                    m_Key.plus2cookinggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 29)
            {
                if (m_Key.plus1detecthiddengem > 0)
                {
                    m_From.AddToBackpack(new plus1detecthiddengem());
                    m_Key.plus1detecthiddengem = (m_Key.plus1detecthiddengem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1detecthiddengem > 0)
                {
                    m_From.AddToBackpack(new plus1detecthiddengem(m_Key.plus1detecthiddengem));
                    m_Key.plus1detecthiddengem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 30)
            {
                if (m_Key.plus2detecthiddengem > 0)
                {
                    m_From.AddToBackpack(new plus2detecthiddengem());
                    m_Key.plus2detecthiddengem = (m_Key.plus2detecthiddengem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2detecthiddengem > 0)
                {
                    m_From.AddToBackpack(new plus2detecthiddengem(m_Key.plus2detecthiddengem));
                    m_Key.plus2detecthiddengem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 31)
            {
                if (m_Key.plus1discordancegem > 0)
                {
                    m_From.AddToBackpack(new plus1discordancegem());
                    m_Key.plus1discordancegem = (m_Key.plus1discordancegem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1discordancegem > 0)
                {
                    m_From.AddToBackpack(new plus1discordancegem(m_Key.plus1discordancegem));
                    m_Key.plus1discordancegem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 32)
            {
                if (m_Key.plus2discordancegem > 0)
                {
                    m_From.AddToBackpack(new plus2discordancegem());
                    m_Key.plus2discordancegem = (m_Key.plus2discordancegem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2discordancegem > 0)
                {
                    m_From.AddToBackpack(new plus2discordancegem(m_Key.plus2discordancegem));
                    m_Key.plus2discordancegem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 33)
            {
                if (m_Key.plus1evalintgem > 0)
                {
                    m_From.AddToBackpack(new plus1evalintgem());
                    m_Key.plus1evalintgem = (m_Key.plus1evalintgem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1evalintgem > 0)
                {
                    m_From.AddToBackpack(new plus1evalintgem(m_Key.plus1evalintgem));
                    m_Key.plus1evalintgem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 34)
            {
                if (m_Key.plus2evalintgem > 0)
                {
                    m_From.AddToBackpack(new plus2evalintgem());
                    m_Key.plus2evalintgem = (m_Key.plus2evalintgem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2evalintgem > 0)
                {
                    m_From.AddToBackpack(new plus2evalintgem(m_Key.plus2evalintgem));
                    m_Key.plus2evalintgem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 35)
            {
                if (m_Key.plus1fencinggem > 0)
                {
                    m_From.AddToBackpack(new plus1fencinggem());
                    m_Key.plus1fencinggem = (m_Key.plus1fencinggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1fencinggem > 0)
                {
                    m_From.AddToBackpack(new plus1fencinggem(m_Key.plus1fencinggem));
                    m_Key.plus1fencinggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 36)
            {
                if (m_Key.plus2fencinggem > 0)
                {
                    m_From.AddToBackpack(new plus2fencinggem());
                    m_Key.plus2fencinggem = (m_Key.plus2fencinggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2fencinggem > 0)
                {
                    m_From.AddToBackpack(new plus2fencinggem(m_Key.plus2fencinggem));
                    m_Key.plus2fencinggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 37)
            {
                if (m_Key.plus1fishinggem > 0)
                {
                    m_From.AddToBackpack(new plus1fishinggem());
                    m_Key.plus1fishinggem = (m_Key.plus1fishinggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1fishinggem > 0)
                {
                    m_From.AddToBackpack(new plus1fishinggem(m_Key.plus1fishinggem));
                    m_Key.plus1fishinggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 38)
            {
                if (m_Key.plus2fishinggem > 0)
                {
                    m_From.AddToBackpack(new plus2fishinggem());
                    m_Key.plus2fishinggem = (m_Key.plus2fishinggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2fishinggem > 0)
                {
                    m_From.AddToBackpack(new plus2fishinggem(m_Key.plus2fishinggem));
                    m_Key.plus2fishinggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 39)
            {
                if (m_Key.plus1fletchinggem > 0)
                {
                    m_From.AddToBackpack(new plus1fletchinggem());
                    m_Key.plus1fletchinggem = (m_Key.plus1fletchinggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1fletchinggem > 0)
                {
                    m_From.AddToBackpack(new plus1fletchinggem(m_Key.plus1fletchinggem));
                    m_Key.plus1fletchinggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 40)
            {
                if (m_Key.plus2fletchinggem > 0)
                {
                    m_From.AddToBackpack(new plus2fletchinggem());
                    m_Key.plus2fletchinggem = (m_Key.plus2fletchinggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2fletchinggem > 0)
                {
                    m_From.AddToBackpack(new plus2fletchinggem(m_Key.plus2fletchinggem));
                    m_Key.plus2fletchinggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 41)
            {
                if (m_Key.plus1focusgem > 0)
                {
                    m_From.AddToBackpack(new plus1focusgem());
                    m_Key.plus1focusgem = (m_Key.plus1focusgem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1focusgem > 0)
                {
                    m_From.AddToBackpack(new plus1focusgem(m_Key.plus1focusgem));
                    m_Key.plus1focusgem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 42)
            {
                if (m_Key.plus2focusgem > 0)
                {
                    m_From.AddToBackpack(new plus2focusgem());
                    m_Key.plus2focusgem = (m_Key.plus2focusgem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2focusgem > 0)
                {
                    m_From.AddToBackpack(new plus2focusgem(m_Key.plus2focusgem));
                    m_Key.plus2focusgem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 43)
            {
                if (m_Key.plus1forensicsgem > 0)
                {
                    m_From.AddToBackpack(new plus1forensicsgem());
                    m_Key.plus1forensicsgem = (m_Key.plus1forensicsgem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1forensicsgem > 0)
                {
                    m_From.AddToBackpack(new plus1forensicsgem(m_Key.plus1forensicsgem));
                    m_Key.plus1forensicsgem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 44)
            {
                if (m_Key.plus2forensicsgem > 0)
                {
                    m_From.AddToBackpack(new plus2forensicsgem());
                    m_Key.plus2forensicsgem = (m_Key.plus2forensicsgem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2forensicsgem > 0)
                {
                    m_From.AddToBackpack(new plus2forensicsgem(m_Key.plus2forensicsgem));
                    m_Key.plus2forensicsgem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 45)
            {
                if (m_Key.plus1healinggem > 0)
                {
                    m_From.AddToBackpack(new plus1healinggem());
                    m_Key.plus1healinggem = (m_Key.plus1healinggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1healinggem > 0)
                {
                    m_From.AddToBackpack(new plus1healinggem(m_Key.plus1healinggem));
                    m_Key.plus1healinggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 46)
            {
                if (m_Key.plus2healinggem > 0)
                {
                    m_From.AddToBackpack(new plus2healinggem());
                    m_Key.plus2healinggem = (m_Key.plus2healinggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2healinggem > 0)
                {
                    m_From.AddToBackpack(new plus2healinggem(m_Key.plus2healinggem));
                    m_Key.plus2healinggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 47)
            {
                if (m_Key.plus1herdinggem > 0)
                {
                    m_From.AddToBackpack(new plus1herdinggem());
                    m_Key.plus1herdinggem = (m_Key.plus1herdinggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1herdinggem > 0)
                {
                    m_From.AddToBackpack(new plus1herdinggem(m_Key.plus1herdinggem));
                    m_Key.plus1herdinggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 48)
            {
                if (m_Key.plus2herdinggem > 0)
                {
                    m_From.AddToBackpack(new plus2herdinggem());
                    m_Key.plus2herdinggem = (m_Key.plus2herdinggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2herdinggem > 0)
                {
                    m_From.AddToBackpack(new plus2herdinggem(m_Key.plus2herdinggem));
                    m_Key.plus2herdinggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 49)
            {
                if (m_Key.plus1hidinggem > 0)
                {
                    m_From.AddToBackpack(new plus1hidinggem());
                    m_Key.plus1hidinggem = (m_Key.plus1hidinggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1hidinggem > 0)
                {
                    m_From.AddToBackpack(new plus1hidinggem(m_Key.plus1hidinggem));
                    m_Key.plus1hidinggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 50)
            {
                if (m_Key.plus2hidinggem > 0)
                {
                    m_From.AddToBackpack(new plus2hidinggem());
                    m_Key.plus2hidinggem = (m_Key.plus2hidinggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2hidinggem > 0)
                {
                    m_From.AddToBackpack(new plus2hidinggem(m_Key.plus2hidinggem));
                    m_Key.plus2hidinggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 51)
            {
                if (m_Key.plus1inscribegem > 0)
                {
                    m_From.AddToBackpack(new plus1inscribegem());
                    m_Key.plus1inscribegem = (m_Key.plus1inscribegem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1inscribegem > 0)
                {
                    m_From.AddToBackpack(new plus1inscribegem(m_Key.plus1inscribegem));
                    m_Key.plus1inscribegem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 52)
            {
                if (m_Key.plus2inscribegem > 0)
                {
                    m_From.AddToBackpack(new plus2inscribegem());
                    m_Key.plus2inscribegem = (m_Key.plus2inscribegem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2inscribegem > 0)
                {
                    m_From.AddToBackpack(new plus2inscribegem(m_Key.plus2inscribegem));
                    m_Key.plus2inscribegem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 53)
            {
                if (m_Key.plus1itemidgem > 0)
                {
                    m_From.AddToBackpack(new plus1itemidgem());
                    m_Key.plus1itemidgem = (m_Key.plus1itemidgem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1itemidgem > 0)
                {
                    m_From.AddToBackpack(new plus1itemidgem(m_Key.plus1itemidgem));
                    m_Key.plus1itemidgem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 54)
            {
                if (m_Key.plus2itemidgem > 0)
                {
                    m_From.AddToBackpack(new plus2itemidgem());
                    m_Key.plus2itemidgem = (m_Key.plus2itemidgem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2itemidgem > 0)
                {
                    m_From.AddToBackpack(new plus2itemidgem(m_Key.plus2itemidgem));
                    m_Key.plus2itemidgem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 55)
            {
                if (m_Key.plus1lockpickinggem > 0)
                {
                    m_From.AddToBackpack(new plus1lockpickinggem());
                    m_Key.plus1lockpickinggem = (m_Key.plus1lockpickinggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1lockpickinggem > 0)
                {
                    m_From.AddToBackpack(new plus1lockpickinggem(m_Key.plus1lockpickinggem));
                    m_Key.plus1lockpickinggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 56)
            {
                if (m_Key.plus2lockpickinggem > 0)
                {
                    m_From.AddToBackpack(new plus2lockpickinggem());
                    m_Key.plus2lockpickinggem = (m_Key.plus2lockpickinggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2lockpickinggem > 0)
                {
                    m_From.AddToBackpack(new plus2lockpickinggem(m_Key.plus2lockpickinggem));
                    m_Key.plus2lockpickinggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 57)
            {
                if (m_Key.plus1lumberjackinggem > 0)
                {
                    m_From.AddToBackpack(new plus1lumberjackinggem());
                    m_Key.plus1lumberjackinggem = (m_Key.plus1lumberjackinggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1lumberjackinggem > 0)
                {
                    m_From.AddToBackpack(new plus1lumberjackinggem(m_Key.plus1lumberjackinggem));
                    m_Key.plus1lumberjackinggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 58)
            {
                if (m_Key.plus2lumberjackinggem > 0)
                {
                    m_From.AddToBackpack(new plus2lumberjackinggem());
                    m_Key.plus2lumberjackinggem = (m_Key.plus2lumberjackinggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2lumberjackinggem > 0)
                {
                    m_From.AddToBackpack(new plus2lumberjackinggem(m_Key.plus2lumberjackinggem));
                    m_Key.plus2lumberjackinggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 59)
            {
                if (m_Key.plus1macinggem > 0)
                {
                    m_From.AddToBackpack(new plus1macinggem());
                    m_Key.plus1macinggem = (m_Key.plus1macinggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1macinggem > 0)
                {
                    m_From.AddToBackpack(new plus1macinggem(m_Key.plus1macinggem));
                    m_Key.plus1macinggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 60)
            {
                if (m_Key.plus2macinggem > 0)
                {
                    m_From.AddToBackpack(new plus2macinggem());
                    m_Key.plus2macinggem = (m_Key.plus2macinggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2macinggem > 0)
                {
                    m_From.AddToBackpack(new plus2macinggem(m_Key.plus2macinggem));
                    m_Key.plus2macinggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 61)
            {
                if (m_Key.plus1magerygem > 0)
                {
                    m_From.AddToBackpack(new plus1magerygem());
                    m_Key.plus1magerygem = (m_Key.plus1magerygem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1magerygem > 0)
                {
                    m_From.AddToBackpack(new plus1magerygem(m_Key.plus1magerygem));
                    m_Key.plus1magerygem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 62)
            {
                if (m_Key.plus2magerygem > 0)
                {
                    m_From.AddToBackpack(new plus2magerygem());
                    m_Key.plus2magerygem = (m_Key.plus2magerygem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2magerygem > 0)
                {
                    m_From.AddToBackpack(new plus2magerygem(m_Key.plus2magerygem));
                    m_Key.plus2magerygem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 63)
            {
                if (m_Key.plus1magicresistgem > 0)
                {
                    m_From.AddToBackpack(new plus1magicresistgem());
                    m_Key.plus1magicresistgem = (m_Key.plus1magicresistgem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1magicresistgem > 0)
                {
                    m_From.AddToBackpack(new plus1magicresistgem(m_Key.plus1magicresistgem));
                    m_Key.plus1magicresistgem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 64)
            {
                if (m_Key.plus2magicresistgem > 0)
                {
                    m_From.AddToBackpack(new plus2magicresistgem());
                    m_Key.plus2magicresistgem = (m_Key.plus2magicresistgem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2magicresistgem > 0)
                {
                    m_From.AddToBackpack(new plus2magicresistgem(m_Key.plus2magicresistgem));
                    m_Key.plus2magicresistgem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 65)
            {
                if (m_Key.plus1meditationgem > 0)
                {
                    m_From.AddToBackpack(new plus1meditationgem());
                    m_Key.plus1meditationgem = (m_Key.plus1meditationgem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1meditationgem > 0)
                {
                    m_From.AddToBackpack(new plus1meditationgem(m_Key.plus1meditationgem));
                    m_Key.plus1meditationgem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 66)
            {
                if (m_Key.plus2meditationgem > 0)
                {
                    m_From.AddToBackpack(new plus2meditationgem());
                    m_Key.plus2meditationgem = (m_Key.plus2meditationgem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2meditationgem > 0)
                {
                    m_From.AddToBackpack(new plus2meditationgem(m_Key.plus2meditationgem));
                    m_Key.plus2meditationgem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 67)
            {
                if (m_Key.plus1mininggem > 0)
                {
                    m_From.AddToBackpack(new plus1mininggem());
                    m_Key.plus1mininggem = (m_Key.plus1mininggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1mininggem > 0)
                {
                    m_From.AddToBackpack(new plus1mininggem(m_Key.plus1mininggem));
                    m_Key.plus1mininggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 68)
            {
                if (m_Key.plus2mininggem > 0)
                {
                    m_From.AddToBackpack(new plus2mininggem());
                    m_Key.plus2mininggem = (m_Key.plus2mininggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2mininggem > 0)
                {
                    m_From.AddToBackpack(new plus2mininggem(m_Key.plus2mininggem));
                    m_Key.plus2mininggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 69)
            {
                if (m_Key.plus1musicianshipgem > 0)
                {
                    m_From.AddToBackpack(new plus1musicianshipgem());
                    m_Key.plus1musicianshipgem = (m_Key.plus1musicianshipgem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1musicianshipgem > 0)
                {
                    m_From.AddToBackpack(new plus1musicianshipgem(m_Key.plus1musicianshipgem));
                    m_Key.plus1musicianshipgem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 70)
            {
                if (m_Key.plus2musicianshipgem > 0)
                {
                    m_From.AddToBackpack(new plus2musicianshipgem());
                    m_Key.plus2musicianshipgem = (m_Key.plus2musicianshipgem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2musicianshipgem > 0)
                {
                    m_From.AddToBackpack(new plus2musicianshipgem(m_Key.plus2musicianshipgem));
                    m_Key.plus2musicianshipgem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 71)
            {
                if (m_Key.plus1necromancygem > 0)
                {
                    m_From.AddToBackpack(new plus1necromancygem());
                    m_Key.plus1necromancygem = (m_Key.plus1necromancygem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1necromancygem > 0)
                {
                    m_From.AddToBackpack(new plus1necromancygem(m_Key.plus1necromancygem));
                    m_Key.plus1necromancygem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 72)
            {
                if (m_Key.plus2necromancygem > 0)
                {
                    m_From.AddToBackpack(new plus2necromancygem());
                    m_Key.plus2necromancygem = (m_Key.plus2necromancygem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2necromancygem > 0)
                {
                    m_From.AddToBackpack(new plus2necromancygem(m_Key.plus2necromancygem));
                    m_Key.plus2necromancygem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 73)
            {
                if (m_Key.plus1ninjitsugem > 0)
                {
                    m_From.AddToBackpack(new plus1ninjitsugem());
                    m_Key.plus1ninjitsugem = (m_Key.plus1ninjitsugem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1ninjitsugem > 0)
                {
                    m_From.AddToBackpack(new plus1ninjitsugem(m_Key.plus1ninjitsugem));
                    m_Key.plus1ninjitsugem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 74)
            {
                if (m_Key.plus2ninjitsugem > 0)
                {
                    m_From.AddToBackpack(new plus2ninjitsugem());
                    m_Key.plus2ninjitsugem = (m_Key.plus2ninjitsugem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2ninjitsugem > 0)
                {
                    m_From.AddToBackpack(new plus2ninjitsugem(m_Key.plus2ninjitsugem));
                    m_Key.plus2ninjitsugem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 75)
            {
                if (m_Key.plus1parrygem > 0)
                {
                    m_From.AddToBackpack(new plus1parrygem());
                    m_Key.plus1parrygem = (m_Key.plus1parrygem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1parrygem > 0)
                {
                    m_From.AddToBackpack(new plus1parrygem(m_Key.plus1parrygem));
                    m_Key.plus1parrygem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 76)
            {
                if (m_Key.plus2parrygem > 0)
                {
                    m_From.AddToBackpack(new plus2parrygem());
                    m_Key.plus2parrygem = (m_Key.plus2parrygem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2parrygem > 0)
                {
                    m_From.AddToBackpack(new plus2parrygem(m_Key.plus2parrygem));
                    m_Key.plus2parrygem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 77)
            {
                if (m_Key.plus1peacemakinggem > 0)
                {
                    m_From.AddToBackpack(new plus1peacemakinggem());
                    m_Key.plus1peacemakinggem = (m_Key.plus1peacemakinggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1peacemakinggem > 0)
                {
                    m_From.AddToBackpack(new plus1peacemakinggem(m_Key.plus1peacemakinggem));
                    m_Key.plus1peacemakinggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 78)
            {
                if (m_Key.plus2peacemakinggem > 0)
                {
                    m_From.AddToBackpack(new plus2peacemakinggem());
                    m_Key.plus2peacemakinggem = (m_Key.plus2peacemakinggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2peacemakinggem > 0)
                {
                    m_From.AddToBackpack(new plus2peacemakinggem(m_Key.plus2peacemakinggem));
                    m_Key.plus2peacemakinggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 79)
            {
                if (m_Key.plus1poisoninggem > 0)
                {
                    m_From.AddToBackpack(new plus1poisoninggem());
                    m_Key.plus1poisoninggem = (m_Key.plus1poisoninggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1poisoninggem > 0)
                {
                    m_From.AddToBackpack(new plus1poisoninggem(m_Key.plus1poisoninggem));
                    m_Key.plus1poisoninggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 80)
            {
                if (m_Key.plus2poisoninggem > 0)
                {
                    m_From.AddToBackpack(new plus2poisoninggem());
                    m_Key.plus2poisoninggem = (m_Key.plus2poisoninggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2poisoninggem > 0)
                {
                    m_From.AddToBackpack(new plus2poisoninggem(m_Key.plus2poisoninggem));
                    m_Key.plus2poisoninggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 81)
            {
                if (m_Key.plus1provocationgem > 0)
                {
                    m_From.AddToBackpack(new plus1provocationgem());
                    m_Key.plus1provocationgem = (m_Key.plus1provocationgem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1provocationgem > 0)
                {
                    m_From.AddToBackpack(new plus1provocationgem(m_Key.plus1provocationgem));
                    m_Key.plus1provocationgem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 82)
            {
                if (m_Key.plus2provocationgem > 0)
                {
                    m_From.AddToBackpack(new plus2provocationgem());
                    m_Key.plus2provocationgem = (m_Key.plus2provocationgem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2provocationgem > 0)
                {
                    m_From.AddToBackpack(new plus2provocationgem(m_Key.plus2provocationgem));
                    m_Key.plus2provocationgem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 83)
            {
                if (m_Key.plus1removetrapgem > 0)
                {
                    m_From.AddToBackpack(new plus1removetrapgem());
                    m_Key.plus1removetrapgem = (m_Key.plus1removetrapgem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1removetrapgem > 0)
                {
                    m_From.AddToBackpack(new plus1removetrapgem(m_Key.plus1removetrapgem));
                    m_Key.plus1removetrapgem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 84)
            {
                if (m_Key.plus2removetrapgem > 0)
                {
                    m_From.AddToBackpack(new plus2removetrapgem());
                    m_Key.plus2removetrapgem = (m_Key.plus2removetrapgem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2removetrapgem > 0)
                {
                    m_From.AddToBackpack(new plus2removetrapgem(m_Key.plus2removetrapgem));
                    m_Key.plus2removetrapgem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 85)
            {
                if (m_Key.plus1snoopinggem > 0)
                {
                    m_From.AddToBackpack(new plus1snoopinggem());
                    m_Key.plus1snoopinggem = (m_Key.plus1snoopinggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1snoopinggem > 0)
                {
                    m_From.AddToBackpack(new plus1snoopinggem(m_Key.plus1snoopinggem));
                    m_Key.plus1snoopinggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 86)
            {
                if (m_Key.plus2snoopinggem > 0)
                {
                    m_From.AddToBackpack(new plus2snoopinggem());
                    m_Key.plus2snoopinggem = (m_Key.plus2snoopinggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2snoopinggem > 0)
                {
                    m_From.AddToBackpack(new plus2snoopinggem(m_Key.plus2snoopinggem));
                    m_Key.plus2snoopinggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 87)
            {
                if (m_Key.plus1spellweavinggem > 0)
                {
                    m_From.AddToBackpack(new plus1spellweavinggem());
                    m_Key.plus1spellweavinggem = (m_Key.plus1spellweavinggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1spellweavinggem > 0)
                {
                    m_From.AddToBackpack(new plus1spellweavinggem(m_Key.plus1spellweavinggem));
                    m_Key.plus1spellweavinggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 88)
            {
                if (m_Key.plus2spellweavinggem > 0)
                {
                    m_From.AddToBackpack(new plus2spellweavinggem());
                    m_Key.plus2spellweavinggem = (m_Key.plus2spellweavinggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2spellweavinggem > 0)
                {
                    m_From.AddToBackpack(new plus2spellweavinggem(m_Key.plus2spellweavinggem));
                    m_Key.plus2spellweavinggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 89)
            {
                if (m_Key.plus1spiritspeakgem > 0)
                {
                    m_From.AddToBackpack(new plus1spiritspeakgem());
                    m_Key.plus1spiritspeakgem = (m_Key.plus1spiritspeakgem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1spiritspeakgem > 0)
                {
                    m_From.AddToBackpack(new plus1spiritspeakgem(m_Key.plus1spiritspeakgem));
                    m_Key.plus1spiritspeakgem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 90)
            {
                if (m_Key.plus2spiritspeakgem > 0)
                {
                    m_From.AddToBackpack(new plus2spiritspeakgem());
                    m_Key.plus2spiritspeakgem = (m_Key.plus2spiritspeakgem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2spiritspeakgem > 0)
                {
                    m_From.AddToBackpack(new plus2spiritspeakgem(m_Key.plus2spiritspeakgem));
                    m_Key.plus2spiritspeakgem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 91)
            {
                if (m_Key.plus1stealinggem > 0)
                {
                    m_From.AddToBackpack(new plus1stealinggem());
                    m_Key.plus1stealinggem = (m_Key.plus1stealinggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1stealinggem > 0)
                {
                    m_From.AddToBackpack(new plus1stealinggem(m_Key.plus1stealinggem));
                    m_Key.plus1stealinggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 92)
            {
                if (m_Key.plus2stealinggem > 0)
                {
                    m_From.AddToBackpack(new plus2stealinggem());
                    m_Key.plus2stealinggem = (m_Key.plus2stealinggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2stealinggem > 0)
                {
                    m_From.AddToBackpack(new plus2stealinggem(m_Key.plus2stealinggem));
                    m_Key.plus2stealinggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 93)
            {
                if (m_Key.plus1stealthgem > 0)
                {
                    m_From.AddToBackpack(new plus1stealthgem());
                    m_Key.plus1stealthgem = (m_Key.plus1stealthgem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1stealthgem > 0)
                {
                    m_From.AddToBackpack(new plus1stealthgem(m_Key.plus1stealthgem));
                    m_Key.plus1stealthgem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 94)
            {
                if (m_Key.plus2stealthgem > 0)
                {
                    m_From.AddToBackpack(new plus2stealthgem());
                    m_Key.plus2stealthgem = (m_Key.plus2stealthgem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2stealthgem > 0)
                {
                    m_From.AddToBackpack(new plus2stealthgem(m_Key.plus2stealthgem));
                    m_Key.plus2stealthgem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 95)
            {
                if (m_Key.plus1swordsgem > 0)
                {
                    m_From.AddToBackpack(new plus1swordsgem());
                    m_Key.plus1swordsgem = (m_Key.plus1swordsgem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1swordsgem > 0)
                {
                    m_From.AddToBackpack(new plus1swordsgem(m_Key.plus1swordsgem));
                    m_Key.plus1swordsgem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 96)
            {
                if (m_Key.plus2swordsgem > 0)
                {
                    m_From.AddToBackpack(new plus2swordsgem());
                    m_Key.plus2swordsgem = (m_Key.plus2swordsgem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2swordsgem > 0)
                {
                    m_From.AddToBackpack(new plus2swordsgem(m_Key.plus2swordsgem));
                    m_Key.plus2swordsgem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 97)
            {
                if (m_Key.plus1tacticsgem > 0)
                {
                    m_From.AddToBackpack(new plus1tacticsgem());
                    m_Key.plus1tacticsgem = (m_Key.plus1tacticsgem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1tacticsgem > 0)
                {
                    m_From.AddToBackpack(new plus1tacticsgem(m_Key.plus1tacticsgem));
                    m_Key.plus1tacticsgem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 98)
            {
                if (m_Key.plus2tacticsgem > 0)
                {
                    m_From.AddToBackpack(new plus2tacticsgem());
                    m_Key.plus2tacticsgem = (m_Key.plus2tacticsgem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2tacticsgem > 0)
                {
                    m_From.AddToBackpack(new plus2tacticsgem(m_Key.plus2tacticsgem));
                    m_Key.plus2tacticsgem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 99)
            {
                if (m_Key.plus1tailoringgem > 0)
                {
                    m_From.AddToBackpack(new plus1tailoringgem());
                    m_Key.plus1tailoringgem = (m_Key.plus1tailoringgem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1tailoringgem > 0)
                {
                    m_From.AddToBackpack(new plus1tailoringgem(m_Key.plus1tailoringgem));
                    m_Key.plus1tailoringgem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 100)
            {
                if (m_Key.plus2tailoringgem > 0)
                {
                    m_From.AddToBackpack(new plus2tailoringgem());
                    m_Key.plus2tailoringgem = (m_Key.plus2tailoringgem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2tailoringgem > 0)
                {
                    m_From.AddToBackpack(new plus2tailoringgem(m_Key.plus2tailoringgem));
                    m_Key.plus2tailoringgem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 101)
            {
                if (m_Key.plus1tasteidgem > 0)
                {
                    m_From.AddToBackpack(new plus1tasteidgem());
                    m_Key.plus1tasteidgem = (m_Key.plus1tasteidgem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1tasteidgem > 0)
                {
                    m_From.AddToBackpack(new plus1tasteidgem(m_Key.plus1tasteidgem));
                    m_Key.plus1tasteidgem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 102)
            {
                if (m_Key.plus2tasteidgem > 0)
                {
                    m_From.AddToBackpack(new plus2tasteidgem());
                    m_Key.plus2tasteidgem = (m_Key.plus2tasteidgem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2tasteidgem > 0)
                {
                    m_From.AddToBackpack(new plus2tasteidgem(m_Key.plus2tasteidgem));
                    m_Key.plus2tasteidgem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 103)
            {
                if (m_Key.plus1tinkeringgem > 0)
                {
                    m_From.AddToBackpack(new plus1tinkeringgem());
                    m_Key.plus1tinkeringgem = (m_Key.plus1tinkeringgem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1tinkeringgem > 0)
                {
                    m_From.AddToBackpack(new plus1tinkeringgem(m_Key.plus1tinkeringgem));
                    m_Key.plus1tinkeringgem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 104)
            {
                if (m_Key.plus2tinkeringgem > 0)
                {
                    m_From.AddToBackpack(new plus2tinkeringgem());
                    m_Key.plus2tinkeringgem = (m_Key.plus2tinkeringgem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2tinkeringgem > 0)
                {
                    m_From.AddToBackpack(new plus2tinkeringgem(m_Key.plus2tinkeringgem));
                    m_Key.plus2tinkeringgem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 105)
            {
                if (m_Key.plus1trackinggem > 0)
                {
                    m_From.AddToBackpack(new plus1trackinggem());
                    m_Key.plus1trackinggem = (m_Key.plus1trackinggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1trackinggem > 0)
                {
                    m_From.AddToBackpack(new plus1trackinggem(m_Key.plus1trackinggem));
                    m_Key.plus1trackinggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 106)
            {
                if (m_Key.plus2trackinggem > 0)
                {
                    m_From.AddToBackpack(new plus2trackinggem());
                    m_Key.plus2trackinggem = (m_Key.plus2trackinggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2trackinggem > 0)
                {
                    m_From.AddToBackpack(new plus2trackinggem(m_Key.plus2trackinggem));
                    m_Key.plus2trackinggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 107)
            {
                if (m_Key.plus1veterinarygem > 0)
                {
                    m_From.AddToBackpack(new plus1veterinarygem());
                    m_Key.plus1veterinarygem = (m_Key.plus1veterinarygem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1veterinarygem > 0)
                {
                    m_From.AddToBackpack(new plus1veterinarygem(m_Key.plus1veterinarygem));
                    m_Key.plus1veterinarygem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 108)
            {
                if (m_Key.plus2veterinarygem > 0)
                {
                    m_From.AddToBackpack(new plus2veterinarygem());
                    m_Key.plus2veterinarygem = (m_Key.plus2veterinarygem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2veterinarygem > 0)
                {
                    m_From.AddToBackpack(new plus2veterinarygem(m_Key.plus2veterinarygem));
                    m_Key.plus2veterinarygem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 109)
            {
                if (m_Key.plus1wrestlinggem > 0)
                {
                    m_From.AddToBackpack(new plus1wrestlinggem());
                    m_Key.plus1wrestlinggem = (m_Key.plus1wrestlinggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus1wrestlinggem > 0)
                {
                    m_From.AddToBackpack(new plus1wrestlinggem(m_Key.plus1wrestlinggem));
                    m_Key.plus1wrestlinggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 110)
            {
                if (m_Key.plus2wrestlinggem > 0)
                {
                    m_From.AddToBackpack(new plus2wrestlinggem());
                    m_Key.plus2wrestlinggem = (m_Key.plus2wrestlinggem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus2wrestlinggem > 0)
                {
                    m_From.AddToBackpack(new plus2wrestlinggem(m_Key.plus2wrestlinggem));
                    m_Key.plus2wrestlinggem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 111)
            {
                if (m_Key.nightsightgem > 0)
                {
                    m_From.AddToBackpack(new nightsightgem());
                    m_Key.nightsightgem = (m_Key.nightsightgem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.nightsightgem > 0)
                {
                    m_From.AddToBackpack(new nightsightgem(m_Key.nightsightgem));
                    m_Key.nightsightgem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 112)
            {
                if (m_Key.SpellChannelingGem > 0)
                {
                    m_From.AddToBackpack(new SpellChannelingGem());
                    m_Key.SpellChannelingGem = (m_Key.SpellChannelingGem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.SpellChannelingGem > 0)
                {
                    m_From.AddToBackpack(new SpellChannelingGem(m_Key.SpellChannelingGem));
                    m_Key.SpellChannelingGem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 113)
            {
                if (m_Key.plus0skilleracegem > 0)
                {
                    m_From.AddToBackpack(new plus0skilleracegem());
                    m_Key.plus0skilleracegem = (m_Key.plus0skilleracegem - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.plus0skilleracegem > 0)
                {
                    m_From.AddToBackpack(new plus0skilleracegem(m_Key.plus0skilleracegem));
                    m_Key.plus0skilleracegem = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 114)
            {
                if (m_Key.ColdResistSewingKit > 0)
                {
                    m_From.AddToBackpack(new ColdResistSewingKit());
                    m_Key.ColdResistSewingKit = (m_Key.ColdResistSewingKit - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.ColdResistSewingKit > 0)
                {
                    m_From.AddToBackpack(new ColdResistSewingKit(m_Key.ColdResistSewingKit));
                    m_Key.ColdResistSewingKit = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 115)
            {
                if (m_Key.EnergyResistSewingKit > 0)
                {
                    m_From.AddToBackpack(new EnergyResistSewingKit());
                    m_Key.EnergyResistSewingKit = (m_Key.EnergyResistSewingKit - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.EnergyResistSewingKit > 0)
                {
                    m_From.AddToBackpack(new EnergyResistSewingKit(m_Key.EnergyResistSewingKit));
                    m_Key.EnergyResistSewingKit = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 116)
            {
                if (m_Key.FireResistSewingKit > 0)
                {
                    m_From.AddToBackpack(new FireResistSewingKit());
                    m_Key.FireResistSewingKit = (m_Key.FireResistSewingKit - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.FireResistSewingKit > 0)
                {
                    m_From.AddToBackpack(new FireResistSewingKit(m_Key.FireResistSewingKit));
                    m_Key.FireResistSewingKit = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            else if (info.ButtonID == 117)
            {
                if (m_Key.PoisonResistSewingKit > 0)
                {
                    m_From.AddToBackpack(new PoisonResistSewingKit());
                    m_Key.PoisonResistSewingKit = (m_Key.PoisonResistSewingKit - 1);
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else if (m_Key.PoisonResistSewingKit > 0)
                {
                    m_From.AddToBackpack(new PoisonResistSewingKit(m_Key.PoisonResistSewingKit));
                    m_Key.PoisonResistSewingKit = 0;
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                }
                else
                {
                    m_From.SendMessage("You do not have any of that!");
                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                    m_Key.BeginCombine(m_From);
               }
            }
            
            
//            else if (info.ButtonID == 10)
//            {
//                if (m_Key.Fluorite > m_Key.WithdrawIncrement)
//                {
//                    m_From.AddToBackpack(new FluoriteIngot(m_Key.WithdrawIncrement));
//                    m_Key.Fluorite = m_Key.Fluorite - m_Key.WithdrawIncrement;
//                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
//                }
//                else if (m_Key.Fluorite > 0)
//                {
//                    m_From.AddToBackpack(new FluoriteIngot(m_Key.Fluorite));
//                    m_Key.Fluorite = 0;
//                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
//                }
//                else
//                {
//                    m_From.SendMessage("You do not have any of that Ingot!");
//                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
//                    m_Key.BeginCombine(m_From);
//               }
//            }
//            else if (info.ButtonID == 11)
//            {
//                if (m_Key.Platinum > m_Key.WithdrawIncrement)
//                {
//                    m_From.AddToBackpack(new PlatinumIngot(m_Key.WithdrawIncrement));
//                    m_Key.Platinum = m_Key.Platinum - m_Key.WithdrawIncrement;
//                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
//                }
//                else if (m_Key.Platinum > 0)
//                {
//                    m_From.AddToBackpack(new PlatinumIngot(m_Key.Platinum));
//                    m_Key.Platinum = 0;
//                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
//                }
//                else
//                {
//                    m_From.SendMessage("You do not have any of that Ingot!");
//                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
//                    m_Key.BeginCombine(m_From);
//                }
//            }

//            else if (info.ButtonID == 20)
//            {
//                if (m_Key.RedScales > m_Key.WithdrawIncrement)
//                {
//                    m_From.AddToBackpack(new RedScales(m_Key.WithdrawIncrement));
//                    m_Key.RedScales = m_Key.RedScales - m_Key.WithdrawIncrement;
//                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
//                }
//                else if (m_Key.RedScales > 0)
//                {
//                    m_From.AddToBackpack(new RedScales(m_Key.RedScales));
//                    m_Key.RedScales = 0;
//                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
//                }
//                else
//                {
//                    m_From.SendMessage("You do not have any of that Scales!");
//                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
//                    m_Key.BeginCombine(m_From);
//                }
//            }
//
//            else if (info.ButtonID == 21)
//            {
//                if (m_Key.YellowScales > m_Key.WithdrawIncrement)
//                {
//                    m_From.AddToBackpack(new YellowScales(m_Key.WithdrawIncrement));
//                    m_Key.YellowScales = m_Key.YellowScales - m_Key.WithdrawIncrement;
//                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
//                }
//                else if (m_Key.YellowScales > 0)
//                {
//                    m_From.AddToBackpack(new YellowScales(m_Key.YellowScales));
//                    m_Key.YellowScales = 0;
//                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
//                }
//                else
//                {
//                    m_From.SendMessage("You do not have any of that Scales!");
//                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
//                    m_Key.BeginCombine(m_From);
//                }
//            }
//
//            else if (info.ButtonID == 22)
//            {
//                if (m_Key.BlackScales > m_Key.WithdrawIncrement)
//                {
//                    m_From.AddToBackpack(new BlackScales(m_Key.WithdrawIncrement));
//                    m_Key.BlackScales = m_Key.BlackScales - m_Key.WithdrawIncrement;
//                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
//                }
//                else if (m_Key.BlackScales > 0)
//                {
//                    m_From.AddToBackpack(new BlackScales(m_Key.BlackScales));
//                    m_Key.BlackScales = 0;
//                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
//                }
//                else
//                {
//                    m_From.SendMessage("You do not have any of that Scales!");
//                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
//                    m_Key.BeginCombine(m_From);
//                }
//            }
//
//            else if (info.ButtonID == 23)
//            {
//                if (m_Key.GreenScales > m_Key.WithdrawIncrement)
//                {
//                    m_From.AddToBackpack(new GreenScales(m_Key.WithdrawIncrement));
//                    m_Key.GreenScales = m_Key.GreenScales - m_Key.WithdrawIncrement;
//                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
//                }
//                else if (m_Key.GreenScales > 0)
//                {
//                    m_From.AddToBackpack(new GreenScales(m_Key.GreenScales));
//                    m_Key.GreenScales = 0;
//                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
//                }
//                else
//                {
//                    m_From.SendMessage("You do not have any of that Scales!");
//                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
//                    m_Key.BeginCombine(m_From);
//                }
//            }
//
//            else if (info.ButtonID == 24)
//            {
//                if (m_Key.WhiteScales > m_Key.WithdrawIncrement)
//                {
//                    m_From.AddToBackpack(new WhiteScales(m_Key.WithdrawIncrement));
//                    m_Key.WhiteScales = m_Key.WhiteScales - m_Key.WithdrawIncrement;
//                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
//                }
//                else if (m_Key.WhiteScales > 0)
//                {
//                    m_From.AddToBackpack(new WhiteScales(m_Key.WhiteScales));
//                    m_Key.WhiteScales = 0;
//                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
//                }
//                else
//                {
//                    m_From.SendMessage("You do not have any of that Scales!");
//                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
//                    m_Key.BeginCombine(m_From);
//                }
//            }
//
//            else if (info.ButtonID == 25)
//            {
//                if (m_Key.BlueScales > m_Key.WithdrawIncrement)
//                {
//                    m_From.AddToBackpack(new BlueScales(m_Key.WithdrawIncrement));
//                    m_Key.BlueScales = m_Key.BlueScales - m_Key.WithdrawIncrement;
//                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
//                }
//                else if (m_Key.BlueScales > 0)
//                {
//                    m_From.AddToBackpack(new BlueScales(m_Key.BlueScales));
//                    m_Key.BlueScales = 0;
//                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
//                }
//                else
//                {
//                    m_From.SendMessage("You do not have any of that Scales!");
//                    m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
//                    m_Key.BeginCombine(m_From);
//                }
//            }

            else if (info.ButtonID == 137)
            {
                m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
                m_Key.BeginCombine(m_From);
            }
            else if (info.ButtonID == 138)
            {
                m_Key.CollectFromBackpack(m_From, true);
                m_From.SendGump(new ResourceStorageKeyBonusGemsGump(m_From, m_Key));
            }
        }
    }
}

namespace Server.Items
{
    public class ResourceStorageKeyBonusGemsTarget : Target
    {
        private ResourceStorageKeyBonusGems m_Key;

        public ResourceStorageKeyBonusGemsTarget(ResourceStorageKeyBonusGems key)
            : base(18, false, TargetFlags.None)
        {
            m_Key = key;
        }

        protected override void OnTarget(Mobile from, object targeted)
        {
            if (m_Key.Deleted)
                return;

            m_Key.EndCombine(from, targeted);
        }
    }
}
