using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using RimWorld;
using Verse;
using UnityEngine;

namespace Syr_LightRadius
{
    [HarmonyPatch(typeof(Thing), nameof(Thing.DrawExtraSelectionOverlays))]
    public class DrawExtraSelectionOverlaysPatch
    {
        [HarmonyPostfix]
        public static void DrawExtraSelectionOverlays_Postfix(Thing __instance)
        {
            if (__instance?.def?.defName != null)
            {
                if (__instance.def.HasComp(typeof(CompGlower)))
                {
                    CompGlower glower = __instance.TryGetComp<CompGlower>();
                    if (LightRadiusSettings.innerLight)
                    {
                        GenDraw.DrawRadiusRing(__instance.Position, glower.Props.glowRadius * 0.91f - 2f);
                    }
                    if (LightRadiusSettings.outerLight)
                    {
                        GenDraw.DrawRadiusRing(__instance.Position, glower.Props.glowRadius * 0.91f - 0.5f);
                    }
                }
            }
        }
    }

    [HarmonyPatch(typeof(Designator_Place), nameof(Designator_Place.SelectedUpdate))]
    public class SelectedUpdatePatch
    {
        [HarmonyPostfix]
        public static void SelectedUpdate_Postfix(Designator_Place __instance)
        {
            if (!ArchitectCategoryTab.InfoRect.Contains(UI.MousePositionOnUIInverted))
            {
                ThingDef thingDef = __instance.PlacingDef as ThingDef;
                if (thingDef?.defName != null)
                {
                    if (thingDef.HasComp(typeof(CompGlower)) && thingDef.selectable)
                    {
                        CompProperties_Glower glowerProps = thingDef.GetCompProperties<CompProperties_Glower>();
                        if (LightRadiusSettings.innerLight)
                        {
                            GenDraw.DrawRadiusRing(UI.MouseCell(), glowerProps.glowRadius * 0.91f - 2f);
                        }
                        if (LightRadiusSettings.outerLight)
                        {
                            GenDraw.DrawRadiusRing(UI.MouseCell(), glowerProps.glowRadius * 0.91f - 0.5f);
                        }
                    }
                }
            }
        }
    }
}
