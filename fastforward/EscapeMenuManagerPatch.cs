using HarmonyLib;

namespace FastForward
{
    public static class EscapeMenuManagerPatch
    {
        public static Plugin plugin = null;


        [HarmonyPatch(typeof(EscapeMenuManager), "OpenEscapeMenu", MethodType.Setter)]
        [HarmonyPostfix]
        static void OpenEscapeMenuPostfixPatch(EscapeMenuManager __instance, bool value)
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
