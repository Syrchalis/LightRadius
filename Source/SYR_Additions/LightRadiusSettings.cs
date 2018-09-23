using Harmony;
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
    public class LightRadiusSettings : ModSettings
    {
        public static bool innerLight = false;
        public static bool outerLight = false;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<bool>(ref innerLight, "SyrLightRadius_innerLight", true, true);
            Scribe_Values.Look<bool>(ref outerLight, "SyrLightRadius_outerLight", false, true);
        }
    }
}
