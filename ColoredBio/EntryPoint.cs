using BepInEx;
using BepInEx.IL2CPP;
using HarmonyLib;
using Il2CppInterop.Runtime.Injection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColoredBio
{
    [BepInPlugin("ColoredBio", "ColoredBio", "1.0.0")]
    [BepInIncompatibility("GTFO.EECustomization")]
    internal class EntryPoint : BasePlugin
    {
        public override void Load()
        {
            _Harmony = new Harmony("ColoredBio.Harmony");
            _Harmony.PatchAll();

            ClassInjector.RegisterTypeInIl2Cpp<BioColorHandler>();

            BioColorInfo.SetColorByFormat(BioState.Hibernate, Config.Bind("ColorInfo", "1. Hibernate State", "#B3B3B3 | 1.0", "Format: \"Color Code | Size\"").Value);
            BioColorInfo.SetColorByFormat(BioState.Detect, Config.Bind("ColorInfo", "2. Detect State", "#F2CB61 | 0.8").Value);
            BioColorInfo.SetColorByFormat(BioState.Heartbeat, Config.Bind("ColorInfo", "3. Heartbeat State", "#B3B3B3 | 1.0").Value);
            BioColorInfo.SetColorByFormat(BioState.Aggressive, Config.Bind("ColorInfo", "4. Aggressive State", "#FF1A1A | 1.0").Value);
            BioColorInfo.SetColorByFormat(BioState.Scout, Config.Bind("ColorInfo", "5. Scout State", "#FFDC1A | 1.0").Value);
            BioColorInfo.SetColorByFormat(BioState.ScoutTentacle, Config.Bind("ColorInfo", "6. Scout Tentacle Out State", "#FF1A1A | 1.0").Value);
        }

        private Harmony _Harmony;
    }
}