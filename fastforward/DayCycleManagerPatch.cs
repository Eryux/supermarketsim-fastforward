// Copyright(c) 2024 - C.Nicolas <contact@bark.tf>
// github.com/eryux

using HarmonyLib;

namespace FastForward
{
    public static class DayCycleManagerPatch
    {
        public static Plugin plugin = null;


        [HarmonyPatch(typeof(DayCycleManager), "FinishTheDay")]
        [HarmonyPostfix]
        static void FinishTheDayPostfixPatch(DayCycleManager __instance)
        {
            if (plugin != null)
            {
                plugin.GamePaused = true;
            }
        }


        [HarmonyPatch(typeof(DayCycleManager), "StartNextDay")]
        [HarmonyPostfix]
        static void StartNextDayPostfixPatch(DayCycleManager __instance)
        {
            if (plugin != null) 
            {
                plugin.GamePaused = false;
                plugin.SetGameSpeed(1f);
            }
        }


        [HarmonyPatch(typeof(DayCycleManager), "StartDayCycle")]
        [HarmonyPostfix]
        static void StartDayCycle(DayCycleManager __instance)
        {
            if (plugin != null)
            {
                plugin.SetGameSpeed(Plugin._speed);
            }
        }
    }
}
