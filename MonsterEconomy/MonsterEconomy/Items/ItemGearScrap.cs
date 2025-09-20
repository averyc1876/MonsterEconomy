
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Datastructures;
using Vintagestory.GameContent;

namespace MonsterEconomy.Items
{
    internal class ItemGearScrap : Item, IAnvilWorkable
    {
        private bool placementMarker;
        private byte[,,] voxelsToPlace;
        private (int,int,int) offset;

        public bool PlacementMarker
        {
            get => placementMarker;
            set 
            {
                if (value)
                    throw new System.NotImplementedException();
                placementMarker = value;
            }
        }

        public bool CanWork(ItemStack stack)
        {
            float temperature = stack.Collectible.GetTemperature(this.api.World, stack);

            JsonObject attributes = stack.Collectible.Attributes;
            if (attributes != null && attributes["workableTemperature"].Exists)
                return attributes["workableTemperature"].AsFloat() <= temperature;

            return false;
        }

        public ItemStack TryPlaceOn(ItemStack stack, BlockEntityAnvil beAnvil)
        {
            if (!CanWork(stack))
                return null;

            if (beAnvil.WorkItemStack == null)
            {
                Item workItem = api.World.GetItem(new AssetLocation("workitem-gear-" + Variant["type"]));
                if (workItem == null)
                    return null;
                ItemStack workItemStack = new ItemStack(workItem);

                CreateVoxelsFromScrap(api, ref beAnvil.Voxels);

                return workItemStack;
            }

            if (!string.Equals(beAnvil.WorkItemStack.Collectible.Variant["type"], Variant["type"]))
            {
                if (api.Side == EnumAppSide.Client)
                    (api as ICoreClientAPI).TriggerIngameError(this, "notequal", Lang.Get("Must be the same metal to add voxels"));
                return null;
            }

            if (AddVoxelsFromScrap(ref beAnvil.Voxels) == 0)
            {
                if (api.Side == EnumAppSide.Client)
                    (api as ICoreClientAPI).TriggerIngameError(this, "requireshammering", Lang.Get("Try hammering down before adding additional voxels"));
                return null;
            }

            return beAnvil.WorkItemStack;
        }

        public static void CreateVoxelsFromScrap(ICoreAPI api, ref byte[,,] voxels)
        {
            AddVoxelsFromScrap(ref voxels);
        }

        public static int AddVoxelsFromScrap(ref byte[,,] voxels)
        {
            return 0;
        }

        public override void OnHeldInteractStart(ItemSlot slot, EntityAgent byEntity, BlockSelection blockSel, EntitySelection entitySel, bool firstEvent, ref EnumHandHandling handling)
        {
            BlockEntityAnvil beAnvil = byEntity.World.BlockAccessor.GetBlockEntity(blockSel.Position) as BlockEntityAnvil;
            if (beAnvil != null && api.Side == EnumAppSide.Client)
                PlacementMarker = true;

            base.OnHeldInteractStart(slot, byEntity, blockSel, entitySel, firstEvent, ref handling);
        }

        public List<SmithingRecipe> GetMatchingRecipes(ItemStack stack)
        {
            return (from r in api.GetSmithingRecipes()
                    where r.Ingredient.SatisfiesAsIngredient(stack, true)
                    orderby r.Output.ResolvedItemstack.Collectible.Code
                    select r).ToList<SmithingRecipe>();
        }

        public int VoxelCountForHandbook(ItemStack stack)
        {
            return 8;
        }

        public int GetRequiredAnvilTier(ItemStack stack)
        {
            int tier = 0;

            JsonObject attributes = stack.Collectible.Attributes;
            if (attributes != null && attributes["requiresAnvilTier"].Exists)
                tier = attributes["requiresAnvilTier"].AsInt(tier);

            return tier;
        }

        public EnumHelveWorkableMode GetHelveWorkableMode(ItemStack stack, BlockEntityAnvil beAnvil)
        {
            return EnumHelveWorkableMode.NotWorkable;
        }

        public ItemStack GetBaseMaterial(ItemStack stack)
        {
            return stack;
        }
    }
}
