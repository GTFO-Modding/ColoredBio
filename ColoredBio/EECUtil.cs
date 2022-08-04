using EEC.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColoredBio
{
    internal static class EECUtil
    {
        private static bool? _enabled;

        public static bool Enabled
        {
            get
            {
                if (_enabled == null)
                {
                    _enabled = BepInEx.IL2CPP.IL2CPPChainloader.Instance.Plugins.ContainsKey("GTFO.EECustomization");
                }
                return (bool)_enabled;
            }
        }

        public static bool HasScannerCustom(uint enemyID)
        {
            return CustomizationAPI.HasCustomization(enemyID, "ScannerCustom");
        }
    }
}
