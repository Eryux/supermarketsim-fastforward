// Copyright(c) 2024 - C.Nicolas <contact@bark.tf>
// github.com/eryux

using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace FastForward
{
    public static class PlayerInteractionPatch
    {
        [HarmonyPatch(typeof(PlayerInteraction), "CheckForHoldingInteraction")]
        [HarmonyPostfix]
        static void CheckForHoldingInteractionPrefixPatch(PlayerInteraction __instance)
        {
            var m_HoldingTimeField = typeof(PlayerInteraction).GetField("m_HoldingTime", BindingFlags.Instance | BindingFlags.NonPublic);

            if (m_HoldingTimeField != null)
            {
                float value = ((float)m_HoldingTimeField.GetValue(__instance) - Time.deltaTime) + Time.deltaTime * (1f / Plugin._speed);
                m_HoldingTimeField.SetValue(__instance, value);
            }
        }
    }
}
