using MonsterEconomy.Items;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace MonsterEconomy
{
    public class MonsterEconomyModSystem : ModSystem
    {

        // Called on server and client
        // Useful for registering block/entity classes on both sides
        public override void Start(ICoreAPI api)
        {
            api.RegisterItemClass(Mod.Info.ModID + ".ItemRustyGear", typeof(ItemRustyGear));
            api.RegisterItemClass(Mod.Info.ModID + ".ItemGearRope", typeof(ItemGearRope));
            api.RegisterItemClass(Mod.Info.ModID + ".ItemGearScrap", typeof(ItemGearScrap));
            api.RegisterItemClass(Mod.Info.ModID + ".WorkItemGear", typeof(ItemWorkedGear));
        }

        public override void StartServerSide(ICoreServerAPI api)
        {
        }

        public override void StartClientSide(ICoreClientAPI api)
        {
        }

    }
}
