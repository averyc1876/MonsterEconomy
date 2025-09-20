using System;
using System.Linq;
using System.Text;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;

namespace MonsterEconomy.Items
{
    internal class ItemGearRope : Item
    {
        ItemStack contents;

        public override void GetHeldItemInfo(ItemSlot inSlot, StringBuilder dsc, IWorldAccessor world, bool withDebugInfo)
        {
            if (contents == null)
                dsc.AppendLine(Lang.Get("Empty"));
            else
                dsc.AppendLine(Lang.Get("monstereconomy:gearrope-contents", contents.StackSize, contents.GetName()));

            base.GetHeldItemInfo(inSlot, dsc, world, withDebugInfo);
            return;
        }


        public override void OnBeforeRender(ICoreClientAPI capi, ItemStack itemstack, EnumItemRenderTarget target, ref ItemRenderInfo renderinfo)
        {
            Shape.SelectiveElements = new string[] {"KnotRoot"};
            base.OnBeforeRender(capi, itemstack, target, ref renderinfo);
        }

        public MeshData ModifyMesh(ICoreClientAPI capi, ItemStack contentStack)
        {
            return null;
        }
    }
}
