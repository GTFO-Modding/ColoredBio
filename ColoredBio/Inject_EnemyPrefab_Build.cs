using Enemies;
using GameData;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ColoredBio
{
    [HarmonyPatch(typeof(EnemyPrefabManager), nameof(EnemyPrefabManager.BuildEnemyPrefab))]
    internal static class Inject_EnemyPrefab_Build
    {
        private static void Postfix(GameObject __result, EnemyDataBlock data)
        {
            if (EECUtil.Enabled)
            {
                if (EECUtil.HasScannerCustom(data.persistentID))
                {
                    Logger.Debug("Skipping Prefab with ScannerCustom in EEC set!");
                    return;
                }
            }

            var agent = __result.GetComponent<EnemyAgent>();
            var handler = __result.AddComponent<BioColorHandler>();
            handler.Owner.Set(agent);
        }
    }
}
