using HarmonyLib;
using Il2Cpp;
using MelonLoader;
using SingleFoodInFeeder;

[assembly: MelonInfo(
    typeof(Mod),
    "SingleFoodInFeeder",
    "1.0.0",
    "Bread-Chan",
    "https://www.nexusmods.com/slimerancher2/mods/109"
)]
[assembly: MelonGame("MonomiPark", "SlimeRancher2")]

namespace SingleFoodInFeeder;

public class Mod : MelonMod
{
    #region Patches

    [HarmonyPatch(typeof(SlimeFeeder), nameof(SlimeFeeder.ProcessFeedOperation))]
    [HarmonyPrefix]
    private static bool SlimeFeeder_ProcessFeedOperation(SlimeFeeder __instance)
    {
        if (__instance._storage.Ammo.Slots[0].Count > 1)
            return true;
        var model = __instance._model;
        model.remainingFeedOperations = Math.Max(0, model.remainingFeedOperations - 1);
        return false;
    }

    #endregion

    #region Setup

    public override void OnInitializeMelon()
    {
        var h = new HarmonyLib.Harmony("com.bread-chan_single_food_in_feeder");
        h.PatchAll(typeof(Mod));

        MelonLogger.Msg("Initialized.");
    }

    #endregion
}
