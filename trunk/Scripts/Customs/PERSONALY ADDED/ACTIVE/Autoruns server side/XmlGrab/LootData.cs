// LootData.cs
// Author: Oak (ssalter)
// Version: 1.0
// Requirements: Runuo 2.0, XmlSpawner2
// Server Tested with: 2.0 build 64
// Revision Date: 7/1/2006
// Purpose: Player can type 'grab options' to get a gump and select what types of items they want to transfer
// to their lootbag when using the 'claim' command. Uses XMLAttachment for loot options

using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;

namespace Server.Engines.XmlSpawner2
{
    public class LootData : XmlAttachment
    {
		private bool m_getall;
        private bool m_getartifacts;
		private bool m_getweapons;
		private bool m_getarmor;
        private bool m_getclothing;
		private bool m_getjewelry;
        private bool m_getgems;
        private bool m_getarrows;
        private bool m_getfood;
        private bool m_gethides;
        private bool m_getscales;
        private bool m_getwood;
        private bool m_getores;
        private bool m_getreagents;
		private bool m_getpotions;
		private bool m_getother;
        private bool m_getgrounditems;
		

        [CommandProperty( AccessLevel.GameMaster )]
        public bool GetAll { get { return m_getall; } set { m_getall  = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool GetArtifacts { get { return m_getartifacts; } set { m_getartifacts = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
        public bool GetWeapons { get { return m_getweapons; } set { m_getweapons  = value; } }
        
		[CommandProperty( AccessLevel.GameMaster )]
        public bool GetArmor { get { return m_getarmor; } set { m_getarmor  = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool GetClothing { get { return m_getclothing; } set { m_getclothing = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
        public bool GetJewelry { get { return m_getjewelry; } set { m_getjewelry  = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool GetGems { get { return m_getgems; } set { m_getgems = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool GetArrows { get { return m_getarrows; } set { m_getarrows = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool GetFood { get { return m_getfood; } set { m_getfood = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool GetHides { get { return m_gethides; } set { m_gethides = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool GetScales { get { return m_getscales; } set { m_getscales = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool GetWood { get { return m_getwood; } set { m_getwood = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool GetOres { get { return m_getores; } set { m_getores = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool GetReagents { get { return m_getreagents; } set { m_getreagents = value; } }

		[CommandProperty( AccessLevel.GameMaster )]
        public bool GetPotions { get { return m_getpotions; } set { m_getpotions  = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool GetOther { get { return m_getother; } set { m_getother = value; } }
        
		[CommandProperty( AccessLevel.GameMaster )]
        public bool GetGroundItems { get { return m_getgrounditems; } set { m_getgrounditems  = value; } }

        public LootData(ASerial serial) : base(serial)
        {
        }

        [Attachable]
        public LootData()
        {
        }

        [Attachable]
        public LootData(bool allflag, bool artifactsflag, bool weaponsflag, bool armorflag, bool clothingflag, bool jewelryflag, bool gemsflag, bool arrowsflag, bool foodflag, bool hidesflag, bool scalesflag, bool woodflag, bool oresflag, bool reagentsflag, bool potionsflag, bool otherflag, bool grounditems)
        {

            m_getall = allflag;
            m_getartifacts = artifactsflag;
            m_getweapons = weaponsflag;
            m_getarmor = armorflag;
            m_getclothing = clothingflag;
            m_getjewelry = jewelryflag;
            m_getgems = gemsflag;
            m_getarrows = arrowsflag;
            m_getfood = foodflag;
            m_gethides = hidesflag;
            m_getscales = scalesflag;
            m_getwood = woodflag;
            m_getores = oresflag;
            m_getreagents = reagentsflag;
            m_getpotions = potionsflag;
            m_getother = otherflag;
            m_getgrounditems = grounditems;
        }
        
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize(writer);

			writer.Write( (int) 0 );
			// version 0

            writer.Write(m_getall);
            writer.Write(m_getartifacts);
            writer.Write(m_getweapons);
            writer.Write(m_getarmor);
            writer.Write(m_getclothing);
            writer.Write(m_getjewelry);
            writer.Write(m_getgems);
            writer.Write(m_getarrows);
            writer.Write(m_getfood);
            writer.Write(m_gethides);
            writer.Write(m_getscales);
            writer.Write(m_getwood);
            writer.Write(m_getores);
            writer.Write(m_getreagents);
            writer.Write(m_getpotions);
            writer.Write(m_getother);
            writer.Write(m_getgrounditems);            

		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			// version 0
			int version = reader.ReadInt();
            m_getall = reader.ReadBool();
            m_getartifacts = reader.ReadBool();
            m_getweapons = reader.ReadBool();
            m_getarmor = reader.ReadBool();
            m_getclothing = reader.ReadBool();
            m_getjewelry = reader.ReadBool();
            m_getgems = reader.ReadBool();
            m_getarrows = reader.ReadBool();
            m_getfood = reader.ReadBool();
            m_gethides = reader.ReadBool();
            m_getscales = reader.ReadBool();
            m_getwood = reader.ReadBool();
            m_getores = reader.ReadBool();
            m_getreagents = reader.ReadBool();
            m_getpotions = reader.ReadBool();
            m_getother = reader.ReadBool();
            m_getgrounditems = reader.ReadBool();
            


		}
    }
}
