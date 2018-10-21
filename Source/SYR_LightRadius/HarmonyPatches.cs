﻿using Harmony;
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
                else if (__instance.def.defName.Contains("Lighting_MURWallLight"))
                {
                    if (LightRadiusSettings.innerLight)
                    {
                        GenDraw.DrawRadiusRing(__instance.Position + new IntVec3(0, 0, 1).RotatedBy(__instance.Rotation), LightRadiusSettings.wallLightRadius * 0.91f - 2f);
                    }
                    if (LightRadiusSettings.outerLight)
                    {
                        GenDraw.DrawRadiusRing(__instance.Position + new IntVec3(0, 0, 1).RotatedBy(__instance.Rotation), LightRadiusSettings.wallLightRadius * 0.91f - 0.5f);
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
                    else if (thingDef.defName.Contains("Lighting_MURWallLight"))
                    {
                        Rot4 rotation = Traverse.Create(__instance).Field("placingRot").GetValue<Rot4>();
                        if (LightRadiusSettings.innerLight)
                        {
                            GenDraw.DrawRadiusRing(UI.MouseCell() + new IntVec3(0, 0, 1).RotatedBy(rotation), LightRadiusSettings.wallLightRadius * 0.91f - 2f);
                        }
                        if (LightRadiusSettings.outerLight)
                        {
                            GenDraw.DrawRadiusRing(UI.MouseCell() + new IntVec3(0, 0, 1).RotatedBy(rotation), LightRadiusSettings.wallLightRadius * 0.91f - 0.5f);
                        }
                        
                    }
                }
            }
        }
    }
}