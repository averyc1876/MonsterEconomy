
using System.Collections.Generic;
using System.Linq;
using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.GameContent;

namespace MonsterEconomy.Items
{
    internal class ItemWorkedGear : Item, IAnvilWorkable
    {
        public bool CanWork(ItemStack stack)
        {
            return false;
        }

        public ItemStack TryPlaceOn(ItemStack stack, BlockEntityAnvil beAnvil)
        {
            if (beAnvil.WorkItemStack != null)
                return null;

            try
            {
                beAnvil.Voxels = BlockEntityAnvil.deserializeVoxels(stack.Attributes.GetBytes("voxels", null));
            }
            catch { }

            return stack.Clone();
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
            return 28;
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
