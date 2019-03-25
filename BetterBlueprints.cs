// Decompiled with JetBrains decompiler
// Type: BetterBlueprints.BetterBlueprintsCore
// Assembly: BetterBlueprints, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 949B4F57-3651-4C23-B4C4-E713EBCAD46C
// Assembly location: D:\SteamLibrary\steamapps\common\The Forest\TheForest_Data\Managed\BetterBlueprints.dll

using ModAPI;
using ModAPI.Attributes;
using System;
using System.IO;
using TheForest.Buildings.Creation;
using TheForest.Utils;
using UnityEngine;

namespace BetterBlueprints
{
    internal class BetterBlueprintsCore : MonoBehaviour
    {
        public static int MaxAnchorPoints = 10000;
        private readonly Config _config = new Config("Mods/BetterBlueprints.settings", "BetterBlueprints");
        public static Color TintColor;
        public static bool BuildAnywhereToggle;
        public static bool InfiniteZiplineToggle;
        private const string ConfigPath = "Mods/BetterBlueprints.settings";

        [ExecuteOnGameStart]
        private static void AddMeToScene()
        {
            if (!Application.loadedLevelName.ToLower().Contains("forest"))
                return;
            new GameObject("__BetterBlueprints__").AddComponent<BetterBlueprintsCore>();
        }

        private void Awake()
        {
            this.ReadConfig();
            this.InvokeRepeating("SetMaxAnchorpointsWall", 1f, 5f);
            this.InvokeRepeating("ReadConfig", 1f, 2f);
        }

        private void Update()
        {
            LocalPlayer.Create.BuildingPlacer.ClearMat.SetColor("_TintColor", BetterBlueprintsCore.TintColor);
        }

        private void SetMaxAnchorpointsWall()
        {
            ((WallArchitect)UnityEngine.Object.FindObjectOfType(typeof(WallArchitect)))._maxPoints = BetterBlueprintsCore.MaxAnchorPoints;
        }

        private void CheckConfig()
        {
            if (File.Exists("Mods/BetterBlueprints.settings"))
                return;
            Log.Write("Configfile is missing", "BetterBlueprints");
            this.CreateConfig();
        }

        private void CreateConfig()
        {
            Log.Write("Creating new default config...", "BetterBlueprints");
            this._config.WriteInt("red", 0, "BlueprintColor");
            this._config.WriteInt("green", 0, "");
            this._config.WriteInt("blue", 100, "");
            this._config.WriteInt("alpha", 25, "");
            this._config.WriteBool("buildAnywhere", true, "Blueprints");
            this._config.WriteBool("infiniteZipline", true, "Blueprints");
        }

        private void ReadConfig()
        {
            this.CheckConfig();
            try
            {
                BetterBlueprintsCore.TintColor.r = this._config.ReadFloat("red", 0.0f, "BlueprintColor") / 100f;
                BetterBlueprintsCore.TintColor.g = this._config.ReadFloat("green", 0.0f, "") / 100f;
                BetterBlueprintsCore.TintColor.b = this._config.ReadFloat("blue", 100f, "") / 100f;
                BetterBlueprintsCore.TintColor.a = this._config.ReadFloat("alpha", 25f, "") / 100f;
                BetterBlueprintsCore.BuildAnywhereToggle = this._config.ReadBool("buildAnywhere", true, "Blueprints");
                BetterBlueprintsCore.InfiniteZiplineToggle = this._config.ReadBool("infiniteZipline", true, "Blueprints");
            }
            catch (Exception ex)
            {
                Log.Write("Error while reading the config from: Mods/BetterBlueprints.settings", "BetterBlueprints");
                Log.Write(ex.Message, "BetterBlueprints");
                Log.Write(ex.StackTrace, "BetterBlueprints");
                if (File.Exists("Mods/BetterBlueprints.settings"))
                    File.Move("Mods/BetterBlueprints.settings", "Mods/BetterBlueprints.settings.old");
                this.CreateConfig();
                this.ReadConfig();
            }
        }
    }
}
