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
    public class LightRadiusCore : Mod
    {
        public static LightRadiusSettings settings;

        public LightRadiusCore(ModContentPack content) : base(content)
        {
            settings = GetSettings<LightRadiusSettings>();
            var harmony = new Harmony("Syrchalis.Rimworld.LightRadius");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
        public override string SettingsCategory() => "SyrLightRadiusSettingsCategory".Translate();

        public override void DoSettingsWindowContents(Rect inRect)
        {
            checked
            {
                Listing_Standard listing_Standard = new Listing_Standard();
                listing_Standard.Begin(inRect);
                listing_Standard.CheckboxLabeled("SyrLightRadius_innerLightDesc".Translate(), ref LightRadiusSettings.innerLight, ("SyrLightRadius_innerLightTooltip".Translate()));
                listing_Standard.Gap(12f);
                listing_Standard.CheckboxLabeled("SyrLightRadius_outerLightDesc".Translate(), ref LightRadiusSettings.outerLight, ("SyrLightRadius_outerLightTooltip".Translate()));
                //listing_Standard.Gap(12f);
                //listing_Standard.Label("SyrLightRadius_WallLightRadius".Translate() + " : " + LightRadiusSettings.wallLightRadius, tooltip: "SyrLightRadius_WallLightRadiusTooltip".Translate());
                //LightRadiusSettings.wallLightRadius = (int)listing_Standard.Slider(LightRadiusSettings.wallLightRadius, 1, 15);
                listing_Standard.End();
                settings.Write();
            }
        }
        public override void WriteSettings()
        {
            base.WriteSettings();
        }
    }
}
