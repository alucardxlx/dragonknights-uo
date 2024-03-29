using System;
using Server;

namespace Xanthos.Evo
{
	public sealed class RaelisDragonSpec : BaseEvoSpec
	{
		// This class implements a singleton pattern; meaning that no matter how many times the
		// Instance attribute is used, there will only ever be one of these created in the entire system.
		// Copy this template and give it a new name.  Assign all of the data members of the EvoSpec
		// base class in the constructor.  Your subclass must not be abstract.
		// Never call new on this class, use the Instance attribute to get the instance instead.

		RaelisDragonSpec()
		{
			m_Tamable = true;
			m_MinTamingToHatch = 99.9; //originaly was 99.9
			m_PercentFemaleChance = 0.00;	// Made small to limit access to eggs. originaly was 0.02
			m_GuardianEggOrDeedChance = .01;
			m_AlwaysHappy = false;
			m_ProducesYoung = false; // originaly was true
			m_PregnancyTerm = 0.0; // originaly was 0.10
			m_AbsoluteStatValues = false;
			m_MaxEvoResistance = 100;
			m_MaxTrainingStage = 7;// At what stage stop atack training dummy and leveling
			m_CanAttackPlayers = false;

			m_RandomHues = new int[] { 1157, 1175, 1172, 1170, 2703, 2473, 2643, 1156, 2704, 2734, 2669, 2621, 2859, 2716, 2791, 2927, 2974, 1161, 2717, 2652, 2821, 2818, 2730, 2670, 2678, 2630, 2641, 2644, 2592, 2543, 2526, 2338, 2339, 1793, 1980, 1983 };

			m_Skills = new SkillName[7] { SkillName.Magery, SkillName.EvalInt, SkillName.Meditation, SkillName.MagicResist,
										  SkillName.Tactics, SkillName.Wrestling, SkillName.Anatomy };
			m_MinSkillValues = new int[7] { 50, 50, 50, 15, 19, 19, 19 };
			m_MaxSkillValues = new int[7] { 120, 120, 110, 110, 100, 100, 100 };

			m_Stages = new BaseEvoStage[] { new RaelisDragonStageOne(), new RaelisDragonStageTwo(),
											  new RaelisDragonStageThree(), new RaelisDragonStageFour(),
											  new RaelisDragonStageFive(), new RaelisDragonStageSix(),
											  new RaelisDragonStageSeven() };
		}

		// These next 2 lines facilitate the singleton pattern.  In your subclass only change the
		// BaseEvoSpec class name to your subclass of BaseEvoSpec class name and uncomment both lines.
		public static RaelisDragonSpec Instance { get { return Nested.instance; } }
		class Nested { static Nested() { } internal static readonly RaelisDragonSpec instance = new RaelisDragonSpec();}
	}	

	// Define a subclass of BaseEvoStage for each stage in your creature and place them in the
	// array in your subclass of BaseEvoSpec.  See the example classes for how to do this.
	// Your subclass must not be abstract.

	public class RaelisDragonStageOne : BaseEvoStage
	{
		public RaelisDragonStageOne()
		{
			EvolutionMessage = "has evolved.";//stage 1 - green
			NextEpThreshold = 25000; EpMinDivisor = 10; EpMaxDivisor = 5; DustMultiplier = 20;
			BaseSoundID = 0xDB;
			BodyValue = 733;
			ControlSlots = 2;
			MinTameSkill = 99.9;
			VirtualArmor = 30;
//			Hue = Evo.Flags.kRandomHueFlag;
//			Hue = 0;

			DamagesTypes = new ResistanceType[1] { ResistanceType.Physical };
			MinDamages = new int[1] { 100 };
			MaxDamages = new int[1] { 100 };

			ResistanceTypes = new ResistanceType[1] { ResistanceType.Physical };
			MinResistances = new int[1] { 15 };
			MaxResistances = new int[1] { 15 };

			DamageMin = 11; DamageMax = 17; HitsMin = 200; HitsMax = 250;
			StrMin = 296; StrMax = 325; DexMin = 56; DexMax = 75; IntMin = 76; IntMax = 96;
		}
	}

	public class RaelisDragonStageTwo : BaseEvoStage
	{
		public RaelisDragonStageTwo()
		{
			EvolutionMessage = "has evolved.";//Stage-2 green with firewings
			NextEpThreshold = 75000; EpMinDivisor = 20; EpMaxDivisor = 10; DustMultiplier = 20;
			BaseSoundID = 219;
			ControlSlots = 2;
			MinTameSkill = 103.9;
			BodyValue = 718;
			VirtualArmor = 40;
//			Hue = 0;
		
			DamagesTypes = new ResistanceType[5] { ResistanceType.Physical, ResistanceType.Fire, ResistanceType.Cold,
													ResistanceType.Poison, ResistanceType.Energy };
			MinDamages = new int[5] { 100, 25, 25, 25, 25 };
			MaxDamages = new int[5] { 100, 25, 25, 25, 25 };

			ResistanceTypes = new ResistanceType[5] { ResistanceType.Physical, ResistanceType.Fire, ResistanceType.Cold,
														ResistanceType.Poison, ResistanceType.Energy };
			MinResistances = new int[5] { 20, 20, 20, 20, 20 };
			MaxResistances = new int[5] { 20, 20, 20, 20, 20 };

			DamageMin = 1; DamageMax = 1; HitsMin= 500; HitsMax = 500;
			StrMin = 200; StrMax = 200; DexMin = 20; DexMax = 20; IntMin = 30; IntMax = 30;
		}
	}

	public class RaelisDragonStageThree : BaseEvoStage
	{
		public RaelisDragonStageThree()
		{
			EvolutionMessage = "has evolved.";//stage-3 stone gargoyle
			NextEpThreshold = 175000; EpMinDivisor = 30; EpMaxDivisor = 20; DustMultiplier = 20;
			BaseSoundID = 0x5A;
			ControlSlots = 3;
			MinTameSkill = 106.9;
			BodyValue = 293;
			VirtualArmor = 50;
//			Hue = 0;
		
			DamagesTypes = null;
			MinDamages = null;
			MaxDamages = null;

			ResistanceTypes = new ResistanceType[5] { ResistanceType.Physical, ResistanceType.Fire, ResistanceType.Cold,
														ResistanceType.Poison, ResistanceType.Energy };
			MinResistances = new int[5] { 40, 40, 40, 40, 40 };
			MaxResistances = new int[5] { 40, 40, 40, 40, 40 };

			DamageMin = 1; DamageMax = 1; HitsMin= 100; HitsMax = 100;
			StrMin = 100; StrMax = 100; DexMin = 10; DexMax = 10; IntMin = 20; IntMax = 20;
		}
	}

	public class RaelisDragonStageFour : BaseEvoStage
	{
		public RaelisDragonStageFour()
		{
			EvolutionMessage = "has evolved.";//stage-4 Red Drake
			NextEpThreshold = 3750000; EpMinDivisor = 50; EpMaxDivisor = 40; DustMultiplier = 20;
			BaseSoundID = 362;
			BodyValue = 61;
			ControlSlots = 3;
			MinTameSkill = 109.9;
			VirtualArmor = 60;
//			Hue = 0;
		
			DamagesTypes = null;
			MinDamages = null;
			MaxDamages = null;

			ResistanceTypes = new ResistanceType[5] { ResistanceType.Physical, ResistanceType.Fire, ResistanceType.Cold,
														ResistanceType.Poison, ResistanceType.Energy };
			MinResistances = new int[5] { 50, 50, 50, 50, 50 };
			MaxResistances = new int[5] { 50, 50, 50, 50, 50 };	

			DamageMin = 1; DamageMax = 1; HitsMin= 100; HitsMax = 100;
			StrMin = 100; StrMax = 100; DexMin = 10; DexMax = 10; IntMin = 120; IntMax = 120;
		}
	}

	public class RaelisDragonStageFive : BaseEvoStage
	{
		public RaelisDragonStageFive()
		{
			EvolutionMessage = "has evolved.";//stage-5 Red Dragon
			NextEpThreshold = 7750000; EpMinDivisor = 160; EpMaxDivisor = 40; DustMultiplier = 20;
			BodyValue = 59;
			VirtualArmor = 60;
			ControlSlots = 4;
			MinTameSkill = 113.9;
//			Hue = 0;
		
			DamagesTypes = new ResistanceType[5] { ResistanceType.Physical, ResistanceType.Fire, ResistanceType.Cold,
													 ResistanceType.Poison, ResistanceType.Energy };
			MinDamages = new int[5] { 100, 50, 50, 50, 50 };
			MaxDamages = new int[5] { 100, 50, 50, 50, 50 };

			ResistanceTypes = new ResistanceType[5] { ResistanceType.Physical, ResistanceType.Fire, ResistanceType.Cold,
														ResistanceType.Poison, ResistanceType.Energy };
			MinResistances = new int[5] { 60, 60, 60, 60, 60 };
			MaxResistances = new int[5] { 60, 60, 60, 60, 60 };	

			DamageMin = 5; DamageMax = 5; HitsMin= 100; HitsMax = 100;
			StrMin = 100; StrMax = 100; DexMin = 20; DexMax = 20; IntMin = 120; IntMax = 120;
		}
	}

	public class RaelisDragonStageSix : BaseEvoStage
	{
		public RaelisDragonStageSix()
		{
			EvolutionMessage = "has evolved.";//Stage-6 Red Crisom Dragon
			NextEpThreshold = 15000000; EpMinDivisor = 540; EpMaxDivisor = 480; DustMultiplier = 20;
			BodyValue = 197;
			VirtualArmor = 70;
			ControlSlots = 4;
			MinTameSkill = 116.9;
//			Hue = 0;
		
			DamagesTypes = null;
			MinDamages = null;
			MaxDamages = null;

			ResistanceTypes = new ResistanceType[5] { ResistanceType.Physical, ResistanceType.Fire, ResistanceType.Cold,
														ResistanceType.Poison, ResistanceType.Energy };
			MinResistances = new int[5] { 70, 70, 70, 70, 70 };
			MaxResistances = new int[5] { 70, 70, 70, 70, 70 };	

			DamageMin = 5; DamageMax = 5; HitsMin= 100; HitsMax = 100;
			StrMin = 100; StrMax = 100; DexMin = 20; DexMax = 20; IntMin = 120; IntMax = 120;
		}
	}

	public class RaelisDragonStageSeven : BaseEvoStage
	{
		public RaelisDragonStageSeven()
		{
			Title = "The Ancient Dragon";
			EvolutionMessage = "has evolved to its highest form and is now an Ancient Dragon.";
			NextEpThreshold = 30000000; EpMinDivisor = 740; EpMaxDivisor = 660; DustMultiplier = 20;
			BaseSoundID = 362;
			BodyValue = 826;
			ControlSlots = 5;
			VirtualArmor = 80;
			MinTameSkill = 119.9;
//			Hue = 0;
		
			DamagesTypes = new ResistanceType[5] { ResistanceType.Physical, ResistanceType.Fire, ResistanceType.Cold,
													 ResistanceType.Poison, ResistanceType.Energy };
			MinDamages = new int[5] { 100, 75, 75, 75, 75 };
			MaxDamages = new int[5] { 100, 75, 75, 75, 75 };

			ResistanceTypes = new ResistanceType[5] { ResistanceType.Physical, ResistanceType.Fire, ResistanceType.Cold,
														ResistanceType.Poison, ResistanceType.Energy };
			MinResistances = new int[5] { 80, 80, 80, 80, 80 };
			MaxResistances = new int[5] { 80, 80, 80, 80, 80 };

			DamageMin = 10; DamageMax = 15; HitsMin= 1350; HitsMax = 1400;
			StrMin = 125; StrMax = 125; DexMin = 125; DexMax = 35; IntMin = 125; IntMax = 125;
		}
	}
}
