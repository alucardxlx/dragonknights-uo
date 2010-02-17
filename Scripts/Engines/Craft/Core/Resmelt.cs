using System;
using System.Collections.Generic;
using Server;
using Server.Targeting;
using Server.Items;
using Server.Engines.XmlSpawner2;

namespace Server.Engines.Craft
{
	public class Resmelt
	{
		public Resmelt()
		{
		}

		public static void Do( Mobile from, CraftSystem craftSystem, BaseTool tool )
		{
			int num = craftSystem.CanCraft( from, tool, null );

			if ( num > 0 )
			{
				from.SendGump( new CraftGump( from, craftSystem, tool, num ) );
			}
			else
			{
				from.Target = new InternalTarget( craftSystem, tool );
                from.SendMessage("Target an item or a bag of items to recycle.");
				//from.SendLocalizedMessage( 1044273 ); // Target an item to recycle.
			}
		}

		private class InternalTarget : Target
		{
			private CraftSystem m_CraftSystem;
			private BaseTool m_Tool;

			public InternalTarget( CraftSystem craftSystem, BaseTool tool ) :  base ( 2, false, TargetFlags.None )
			{
				m_CraftSystem = craftSystem;
				m_Tool = tool;
			}

			private bool Resmelt( Mobile from, Item item, CraftResource resource )
			{
                try
                {
                    //if (CraftResources.GetType(resource) != CraftResourceType.Metal)
                    //    return false;

                    CraftResourceInfo info = CraftResources.GetInfo(resource);

                    if (info == null || info.ResourceTypes.Length == 0)
                        return false;

                    CraftItem craftItem = m_CraftSystem.CraftItems.SearchFor(item.GetType());

                    if (craftItem == null || craftItem.Resources.Count == 0)
                        return false;

                    CraftRes craftResource = craftItem.Resources.GetAt(0);

                    if (craftResource.Amount < 2)
                        return false; // Not enough metal to resmelt

                    Type resourceType = info.ResourceTypes[0];
                    Item ingot = (Item)Activator.CreateInstance(resourceType);

                    if (item is DragonBardingDeed || (item is BaseArmor && ((BaseArmor)item).PlayerConstructed) || (item is BaseWeapon && ((BaseWeapon)item).PlayerConstructed) || (item is BaseClothing && ((BaseClothing)item).PlayerConstructed))
                        ingot.Amount = craftResource.Amount / 2;
                    else
                        ingot.Amount = 1;

                    item.Delete();
                    from.AddToBackpack(ingot);

                    return true;
                }
                catch
                {
                }

				return false;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				int num = m_CraftSystem.CanCraft( from, m_Tool, null );

                if (num > 0)
                {
                    from.SendGump(new CraftGump(from, m_CraftSystem, m_Tool, num));
                }
                else if (targeted is Container)
                {
                    Container bag = (Container)targeted;
                    if (!bag.IsChildOf(from.Backpack))
                    {
                        from.SendGump(new CraftGump(from, m_CraftSystem, m_Tool, "That is not inside your backpack."));
                        return;
                    }
                    bool success = false;
                    List<Item> items = bag.Items;
                    for (int i = items.Count - 1; i >= 0; i--)
                    {
                        Item item = items[i];
//                        TimedAttachment att = (TimedAttachment)XmlAttach.FindAttachment(item, typeof(TimedAttachment));
//                        if (att == null)
//                        {
                            if (item is BaseArmor && Resmelt(from, (BaseArmor)item, ((BaseArmor)item).Resource))
                                success = true;
                            else if (item is BaseWeapon && Resmelt(from, (BaseWeapon)item, ((BaseWeapon)item).Resource))
                                success = true;
                            else if (targeted is BaseClothing && Resmelt(from, (BaseClothing)targeted, ((BaseClothing)targeted).Resource))
                                success = true;
                            else if (item is DragonBardingDeed && Resmelt(from, (DragonBardingDeed)item, ((DragonBardingDeed)item).Resource))
                                success = true;
//                        }
                    }
                    if (success)
                    {
                        from.PlaySound(0x2A);
                        from.PlaySound(0x240);
                        from.SendGump(new CraftGump(from, m_CraftSystem, m_Tool, "You recycled that bag of items into raw resources."));
                    }
                    else
                        from.SendGump(new CraftGump(from, m_CraftSystem, m_Tool, "There is nothing in that bag that can be recycled."));
                }
                else
				{
					bool success = false;
					bool isStoreBought = false;

                    if (targeted is Item && !((Item)targeted).IsChildOf(from.Backpack))
                    {
                        from.SendGump(new CraftGump(from, m_CraftSystem, m_Tool, "That is not inside your backpack."));
                        return;
                    }

//                    TimedAttachment att = (TimedAttachment)XmlAttach.FindAttachment(targeted, typeof(TimedAttachment));
//                    if (att == null)
//                    {
                        if (targeted is BaseArmor)
                        {
                            success = Resmelt(from, (BaseArmor)targeted, ((BaseArmor)targeted).Resource);
                            isStoreBought = !((BaseArmor)targeted).PlayerConstructed;
                        }
                        else if (targeted is BaseWeapon)
                        {
                            success = Resmelt(from, (BaseWeapon)targeted, ((BaseWeapon)targeted).Resource);
                            isStoreBought = !((BaseWeapon)targeted).PlayerConstructed;
                        }
                        else if (targeted is BaseClothing)
                        {
                            success = Resmelt(from, (BaseClothing)targeted, ((BaseClothing)targeted).Resource);
                            isStoreBought = !((BaseClothing)targeted).PlayerConstructed;
                        }
                        else if (targeted is DragonBardingDeed)
                        {
                            success = Resmelt(from, (DragonBardingDeed)targeted, ((DragonBardingDeed)targeted).Resource);
                            isStoreBought = false;
//                        }
                    }

                    if (success)
                    {
                        from.PlaySound(0x2A);
                        from.PlaySound(0x240);
                        from.SendGump(new CraftGump(from, m_CraftSystem, m_Tool, "You recycled that into raw resources."));// isStoreBought ? 500418 : 1044270)); // You melt the item down into ingots.
                    }
                    else
                        from.SendGump(new CraftGump(from, m_CraftSystem, m_Tool, "You can't recycle that into raw resources."));// 1044272)); // You can't melt that down into ingots.
				}
			}
		}
	}
}
