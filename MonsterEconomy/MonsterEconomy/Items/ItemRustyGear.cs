using System.Linq;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Util;

namespace MonsterEconomy.Items
{
    internal class ItemRustyGear : Item
    {
        public ItemRustyGear()
        {
        }

        public override void OnHeldInteractStart(ItemSlot slot, EntityAgent byEntity, BlockSelection blockSel, EntitySelection entitySel, bool firstEvent, ref EnumHandHandling handling)
        {
            base.OnHeldInteractStart(slot, byEntity, blockSel, entitySel, firstEvent, ref handling);
        }

        //public override WorldInteraction[] GetHeldInteractionHelp(ItemSlot inSlot)
        //{
        //    return new WorldInteraction[]{
        //        new WorldInteraction
        //        {
        //            HotKeyCode = "shift",
        //            ActionLangCode = "heldhelp-place",
        //            MouseButton = EnumMouseButton.Right
        //        }
        //    }.Append(base.GetHeldInteractionHelp(inSlot));
        //}
    }
}
