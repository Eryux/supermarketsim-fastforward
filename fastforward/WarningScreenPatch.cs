using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastForward
{
    public static class WarningScreenPatch
    {
        public static Plugin plugin = null;


        [HarmonyPatch(typeof(WarningScreen), "Show", MethodType.Setter)]
        [HarmonyPostfix]
        static void ShowPostfixPatch(WarningScreen __instance, bool value)
        {
            if (plugin != null)
            {
                if (value)
                {
                    plugin.GamePaused = true;
                }
                else
                {
                    plugin.GamePaused = false;
                    plugin.SetGameSpeed(1f);
                }
            }
        }
    }
}
