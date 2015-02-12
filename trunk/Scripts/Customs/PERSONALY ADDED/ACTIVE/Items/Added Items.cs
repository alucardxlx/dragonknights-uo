using System;

namespace Server.Items
{
	
	#region Clay Items
	
//	[FlipableAttribute( 0x?, 0x? )]
	public class ClayModernPlanter0x44f1 : Item
	{
		[Constructable]
		public ClayModernPlanter0x44f1() : base(0x44f1)
		{
			Name = "A Clay Modern Planter";
			Weight = 5.0;
		}

		public ClayModernPlanter0x44f1(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	
//	[FlipableAttribute( 0x?, 0x? )]
	public class ClayGargishProtectiveTotemStatue0x42bb : Item
	{
		[Constructable]
		public ClayGargishProtectiveTotemStatue0x42bb() : base(0x42bb)
		{
			Name = "A Clay Gargish Protective Totem Statue";
			Weight = 10.0;
		}

		public ClayGargishProtectiveTotemStatue0x42bb(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	
//	[FlipableAttribute( 0x?, 0x? )]
	public class ClayGargishVigilanceTotemStatue0x42bc : Item
	{
		[Constructable]
		public ClayGargishVigilanceTotemStatue0x42bc() : base(0x42bc)
		{
			Name = "A Clay Gargish Vigilance Totem Statue";
			Weight = 10.0;
		}

		public ClayGargishVigilanceTotemStatue0x42bc(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	
	[FlipableAttribute( 0x4688, 0x4689 )]
	public class ClayBlackCatStatue0x4688 : Item
	{
		[Constructable]
		public ClayBlackCatStatue0x4688() : base(0x4688)
		{
			Name = "A Clay Black Cat Statue";
			Weight = 10.0;
		}

		public ClayBlackCatStatue0x4688(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	
	[FlipableAttribute( 0x42c0, 0x42c1 )]
	public class ClayGargishKnowledgeTotemStatue0x42c0 : Item
	{
		[Constructable]
		public ClayGargishKnowledgeTotemStatue0x42c0() : base(0x42c0)
		{
			Name = "A Clay Gargish Knowledge Totem Statue";
			Weight = 15.0;
		}

		public ClayGargishKnowledgeTotemStatue0x42c0(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	
//	[FlipableAttribute( 0x?, 0x? )]
	public class ClayGargishBentasVase0x42b3 : Item
	{
		[Constructable]
		public ClayGargishBentasVase0x42b3() : base(0x42b3)
		{
			Name = "A Clay Gargish Bentas Vase";
			Weight = 15.0;
		}

		public ClayGargishBentasVase0x42b3(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	
//	[FlipableAttribute( 0x?, 0x? )]
	public class ClayGargishTraditionalVase0x42b2 : Item
	{
		[Constructable]
		public ClayGargishTraditionalVase0x42b2() : base(0x42b2)
		{
			Name = "A Clay Gargish Traditional Vase";
			Weight = 20.0;
		}

		public ClayGargishTraditionalVase0x42b2(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	
//	[FlipableAttribute( 0x?, 0x? )]
	public class ClayGrecianPlanter0x44f0 : Item
	{
		[Constructable]
		public ClayGrecianPlanter0x44f0() : base(0x44f0)
		{
			Name = "A Clay Grecian Planter";
			Weight = 25.0;
		}

		public ClayGrecianPlanter0x44f0(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	
//	[FlipableAttribute( 0x?, 0x? )]
	public class ClayGargishMemorialStatue0x42c3 : Item
	{
		[Constructable]
		public ClayGargishMemorialStatue0x42c3() : base(0x42c3)
		{
			Name = "A Clay Gargish Memorial Statue";
			Weight = 65.0;
		}

		public ClayGargishMemorialStatue0x42c3(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	
//	[FlipableAttribute( 0x?, 0x? )]
	public class ClayMedusaStatue0x40bc : Item
	{
		[Constructable]
		public ClayMedusaStatue0x40bc() : base(0x40bc)
		{
			Name = "A Clay Medusa Statue";
			Weight = 70.0;
		}

		public ClayMedusaStatue0x40bc(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	
//	[FlipableAttribute( 0x?, 0x? )]
	public class ClayGargishWarriorStatue0x42c2 : Item
	{
		[Constructable]
		public ClayGargishWarriorStatue0x42c2() : base(0x42c2)
		{
			Name = "A Clay Gargish Warrior Statue";
			Weight = 65.0;
		}

		public ClayGargishWarriorStatue0x42c2(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	
//	[FlipableAttribute( 0x?, 0x? )]
	public class ClayShadowStatue0x364b : Item
	{
		[Constructable]
		public ClayShadowStatue0x364b() : base(0x364b)
		{
			Name = "A Clay Shadow Statue";
			Weight = 70.0;
		}

		public ClayShadowStatue0x364b(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	
//	[FlipableAttribute( 0x?, 0x? )]
	public class ClayGargoyleStatue0x42c5 : Item
	{
		[Constructable]
		public ClayGargoyleStatue0x42c5() : base(0x42c5)
		{
			Name = "A Clay Gargoyle Statue";
			Weight = 70.0;
		}

		public ClayGargoyleStatue0x42c5(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	
	#endregion Clay Items
	
	#region Cloth Items
	
	[FlipableAttribute( 0x13ab, 0x13ac )]
	public class PillowMedium0x13ab : Item
	{
		[Constructable]
		public PillowMedium0x13ab() : base(0x13ab)
		{
			Name = "A Medium Pillow";
			Weight = 5.0;
		}

		public PillowMedium0x13ab(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	
//	[FlipableAttribute( 0x?, 0x? )]
	public class PillowSmall0x1397 : Item
	{
		[Constructable]
		public PillowSmall0x1397() : base(0x1397)
		{
			Name = "A Small Pillow";
			Weight = 4.0;
		}

		public PillowSmall0x1397(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	
	[FlipableAttribute( 0x13a4, 0x13a5 )]
	public class PillowLarge0x13a4 : Item
	{
		[Constructable]
		public PillowLarge0x13a4() : base(0x13a4)
		{
			Name = "A Large Pillow";
			Weight = 6.0;
		}

		public PillowLarge0x13a4(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	
//	[FlipableAttribute( 0x?, 0x? )]
	public class PillowExtraLarge0x163a : Item
	{
		[Constructable]
		public PillowExtraLarge0x163a() : base(0x163a)
		{
			Name = "A Extra Large Pillow";
			Weight = 7.0;
		}

		public PillowExtraLarge0x163a(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	
	#endregion Cloth Items
	
	#region Crystal Items
	
	[FlipableAttribute( 0x35fc, 0x35fd )]
	public class CrystalStatue0x35fc : Item
	{
		[Constructable]
		public CrystalStatue0x35fc() : base(0x35fc)
		{
			Name = "A Crystal Statue";
			Weight = 65.0;
		}

		public CrystalStatue0x35fc(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	
	[FlipableAttribute( 0x35ed, 0x35ee )]
	public class CrystalThrone0x35ed : Item
	{
		[Constructable]
		public CrystalThrone0x35ed() : base(0x35ed)
		{
			Name = "A Crystal Throne";
			Weight = 60.0;
		}

		public CrystalThrone0x35ed(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	
//	[FlipableAttribute( 0x?, 0x? )]
	public class CrystalBrazier0x35ef : Item
	{
		[Constructable]
		public CrystalBrazier0x35ef() : base(0x35ef)
		{
			Name = "A Crystal Brazier";
			Weight = 70.0;
		}

		public CrystalBrazier0x35ef(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}



	#endregion Crystal Items
	
	#region Mixed Items
	
	[FlipableAttribute( 0x0f72, 0x0f74 )]
	public class RackOfCanvasesTall0x0f72 : Item
	{
		[Constructable]
		public RackOfCanvasesTall0x0f72() : base(0x0f72)
		{
			Name = "A Rack Of Tall Canvases";
			Weight = 50.0;
		}

		public RackOfCanvasesTall0x0f72(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}

	[FlipableAttribute( 0x0f71, 0x0f73 )]
	public class RackOfCanvasesWide0x0f71 : Item
	{
		[Constructable]
		public RackOfCanvasesWide0x0f71() : base(0x0f71)
		{
			Name = "A Rack Of Wide Canvases";
			Weight = 50.0;
		}

		public RackOfCanvasesWide0x0f71(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	
	[FlipableAttribute( 0x469B, 0x469C )]
	public class PumpkinScareCrow0x469B : Item
	{
		[Constructable]
		public PumpkinScareCrow0x469B() : base(0x469B)
		{
			Name = "A Pumpkin Scarecrow";
			Weight = 10.0;
		}

		public PumpkinScareCrow0x469B(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	
//	[FlipableAttribute( 0x?, 0x? )]
	public class SmallSoulForge0x44c7 : Item
	{
		[Constructable]
		public SmallSoulForge0x44c7() : base(0x44c7)
		{
			Name = "A Small Soul Forge";
			Weight = 30.0;
		}

		public SmallSoulForge0x44c7(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	
//	[FlipableAttribute( 0x?, 0x? )]
	public class PedestalWithCrystal0x2fd4 : Item
	{
		[Constructable]
		public PedestalWithCrystal0x2fd4() : base(0x2fd4)
		{
			Name = "A Pedestal With Crystal";
			Weight = 70.0;
		}

		public PedestalWithCrystal0x2fd4(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}


	#endregion Mixed Items
	
	#region Resource Items
	
//	[FlipableAttribute( 0x?, 0x? )]
	public class FeatherPileX6 : Item
	{
		[Constructable]
		public FeatherPileX6() : base(0x1bd3)
		{
			Name = "A Pile Of Feathers";
			Weight = 6.0;
		}

		public FeatherPileX6(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	
	#endregion Resource Items
	
	#region Stone Items
	
//	[FlipableAttribute( 0x3F19, 0x3F1A )]
	public class StonePedestal0x1223 : Item
	{
		[Constructable]
		public StonePedestal0x1223() : base(0x1223)
		{
			Name = "A Stone Pedestal";
			Weight = 25.0;
		}

		public StonePedestal0x1223(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	
	[FlipableAttribute( 0x3F19, 0x3F1A )]
	public class StonePosedStatue0x3F19 : Item
	{
		[Constructable]
		public StonePosedStatue0x3F19() : base(0x3f19)
		{
			Name = "A Posed Statue";
			Weight = 10.0;
		}

		public StonePosedStatue0x3F19(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	
	[FlipableAttribute( 0x12ca, 0x12cb )]
	public class StoneBustStatue0x12ca : Item
	{
		[Constructable]
		public StoneBustStatue0x12ca() : base(0x12ca)
		{
			Name = "A Bust Statue";
			Weight = 10.0;
		}

		public StoneBustStatue0x12ca(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	
	[FlipableAttribute( 0x12d8, 0x12d9 )]
	public class StoneAlantianStatue0x12d8 : Item
	{
		[Constructable]
		public StoneAlantianStatue0x12d8() : base(0x12d8)
		{
			Name = "A Alantian Statue";
			Weight = 75.0;
		}

		public StoneAlantianStatue0x12d8(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}

	[FlipableAttribute( 0x48a8, 0x48a9 )]
	public class StoneStatue0x48a8 : Item
	{
		[Constructable]
		public StoneStatue0x48a8() : base(0x48a8)
		{
			Name = "A Dragon Head Statue";
			Weight = 25.0;
		}

		public StoneStatue0x48a8(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}

	[FlipableAttribute( 0x4578, 0x4579 )]
	public class StoneSeahorseStatue0x4578 : Item
	{
		[Constructable]
		public StoneSeahorseStatue0x4578() : base(0x4578)
		{
			Name = "A Seahorse Statue";
			Weight = 60.0;
		}

		public StoneSeahorseStatue0x4578(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	
	[FlipableAttribute( 0x457a, 0x457b )]
	public class StoneMermaidStatue0x457a : Item
	{
		[Constructable]
		public StoneMermaidStatue0x457a() : base(0x457a)
		{
			Name = "A Mermaid Statue";
			Weight = 60.0;
		}

		public StoneMermaidStatue0x457a(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}

	[FlipableAttribute( 0x457c, 0x457d )]
	public class StoneGriffinStatue0x457c : Item
	{
		[Constructable]
		public StoneGriffinStatue0x457c() : base(0x457c)
		{
			Name = "A Griffin Statue";
			Weight = 65.0;
		}

		public StoneGriffinStatue0x457c(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}

//	[FlipableAttribute( 0x?, 0x? )]
	public class StoneAncientStonePlanter0x44ef : Item
	{
		[Constructable]
		public StoneAncientStonePlanter0x44ef() : base(0x44ef)
		{
			Name = "A Ancient Stone Planter";
			Weight = 5.0;
		}

		public StoneAncientStonePlanter0x44ef(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	
	[FlipableAttribute( 0x4042, 0x4043 )]
	public class StoneGargoyleVase0x4042 : Item
	{
		[Constructable]
		public StoneGargoyleVase0x4042() : base(0x4042)
		{
			Name = "A Gargoyle Vase";
			Weight = 10;
		}

		public StoneGargoyleVase0x4042(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	

	#endregion Stone Items
	
	#region Wood Items

//	[FlipableAttribute( 0x?, 0x? )]
	public class TreeLog : Item
	{
		[Constructable]
		public TreeLog() : base(0x3A5)
		{
			Name = "A Tree Log";
			Weight = 25.0;
		}

		public TreeLog(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	
	[FlipableAttribute( 0x1bde, 0x1BE1 )]
	public class LogPileX3 : Item
	{
		[Constructable]
		public LogPileX3() : base(0x1bde)
		{
			Name = "A Pile Of Logs";
			Weight = 3.0;
		}

		public LogPileX3(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	
	[FlipableAttribute( 0x1bdf, 0x1BE2 )]
	public class LogPileX20 : Item
	{
		[Constructable]
		public LogPileX20() : base(0x1bdf)
		{
			Name = "A Pile Of Logs";
			Weight = 20.0;
		}

		public LogPileX20(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	
	[FlipableAttribute( 0x1bd8, 0x1BDB )]
	public class BoardPileX3 : Item
	{
		[Constructable]
		public BoardPileX3() : base(0x1bd8)
		{
			Name = "A Pile of Boards";
			Weight = 3.0;
		}

		public BoardPileX3(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	
	[FlipableAttribute( 0x1bd9, 0x1BDC )]
	public class BoardPileX7 : Item
	{
		[Constructable]
		public BoardPileX7() : base(0x1bd9)
		{
			Name = "A Pile of Boards";
			Weight = 7.0;
		}

		public BoardPileX7(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	
//	[FlipableAttribute( 0x?, 0x? )]
	public class ShaftPileX12 : Item
	{
		[Constructable]
		public ShaftPileX12() : base(0x1bd6)
		{
			Name = "A Pile of Shafts";
			Weight = 12.0;
		}

		public ShaftPileX12(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	
//	[FlipableAttribute( 0x?, 0x? )]
	public class ArrowPileX13 : Item
	{
		[Constructable]
		public ArrowPileX13() : base(0xf41)
		{
			Name = "A Pile Of Arrows";
			Weight = 13.0;
		}

		public ArrowPileX13(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	
//	[FlipableAttribute( 0x?, 0x? )]
	public class CrossBowBoltPileX13 : Item
	{
		[Constructable]
		public CrossBowBoltPileX13() : base(0x1bfd)
		{
			Name = "A Pile Of CrossBow Bolts";
			Weight = 13.0;
		}

		public CrossBowBoltPileX13(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	
//	[FlipableAttribute( 0x?, 0x? )]
	public class SaddleHolder : Item
	{
		[Constructable]
		public SaddleHolder() : base(0xf37)
		{
			Name = "A Saddle Holder";
			Weight = 16.0;
		}

		public SaddleHolder(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	
	[FlipableAttribute( 0x44d5, 0x44d9 )]
	public class GrandFatherClock : Item
	{
		[Constructable]
		public GrandFatherClock() : base(0x44d5)
		{
			Name = "A Grandfather Clock";
			Weight = 25.0;
		}

		public GrandFatherClock(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	
//	[FlipableAttribute( 0x?, 0x? )]
	public class OakBarrel : Item
	{
		[Constructable]
		public OakBarrel() : base(0x44F2)
		{
			Name = "A Oak Barrel";
			Weight = 25.0;
		}

		public OakBarrel(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
	
	#endregion Wood Items



}
