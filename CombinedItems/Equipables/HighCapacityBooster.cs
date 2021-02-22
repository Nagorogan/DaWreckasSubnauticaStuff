﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Reflection;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using SMLHelper.V2.Handlers;
using SMLHelper.V2.Utility;
using UnityEngine;
using UWE;
using Logger = QModManager.Utility.Logger;
using FMODUnity;

namespace CombinedItems.Equipables
{
    class HighCapacityBooster : Equipable
    {
        private static GameObject prefab;
        /*private static ModelPlug viewModel;
        private static StudioEventEmitter boostSound;
        private static ParticleSystem boostVFX;
        private static SimpleMotor simpleMotor;*/

        public HighCapacityBooster() : base("HighCapacityBooster", "High Capacity Booster Tank", "Booster tank with increased oxygen capacity.")
        {
            OnFinishedPatching += () =>
            {
                CraftTreeHandler.AddTabNode(CraftTree.Type.Workbench, "ModTanks", "Tank Upgrades", SpriteManager.Get(TechType.HighCapacityTank));
                CraftTreeHandler.RemoveNode(CraftTree.Type.Workbench, new string[] { "HighCapacityTank" });
                CraftTreeHandler.AddCraftingNode(CraftTree.Type.Workbench, TechType.HighCapacityTank);
            };
        }

        public override EquipmentType EquipmentType => EquipmentType.Tank;

        public override Vector2int SizeInInventory => new Vector2int(3, 4);

        public override QuickSlotType QuickSlotType => QuickSlotType.None;

        public override TechType RequiredForUnlock => TechType.SuitBoosterTank;

        public override TechCategory CategoryForPDA => TechCategory.Equipment;

        public override CraftTree.Type FabricatorType => CraftTree.Type.Workbench;

        public override string[] StepsToFabricatorTab => new string[] { "ModTanks" };

        protected override RecipeData GetBlueprintRecipe()
        {
            return new RecipeData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[]
                    {
                        new Ingredient(TechType.HighCapacityTank, 1),
                        new Ingredient(TechType.WiringKit, 1)
                    }
                )
            };
        }

        protected override Sprite GetItemSprite()
        {
            return SpriteManager.Get(TechType.HighCapacityTank);
        }

        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            CoroutineTask<GameObject> task;
            Oxygen oxy;
            if (HighCapacityBooster.prefab == null)
            {
                task = CraftData.GetPrefabForTechTypeAsync(TechType.SuitBoosterTank, true);
                yield return task;

                var prefab = GameObject.Instantiate(task.GetResult());
                prefab.SetActive(false); // Keep the prefab inactive until we're done editing it.

                // Editing prefab
                /*booster = prefab.EnsureComponent<SuitBoosterTank>();
                viewModel = booster.viewModel;
                boostSound = booster.boostSound;
                boostVFX = booster.boostVFXPrefab;
                simpleMotor = booster.motor;*/
                HighCapacityBooster.prefab = prefab;
                HighCapacityBooster.prefab.SetActive(true);
            }

            GameObject go = GameObject.Instantiate(HighCapacityBooster.prefab);
            /*oxy = go.GetComponent<Oxygen>();
            if (oxy != null)
            {
                booster = HighCapacityBooster.prefab.EnsureComponent<SuitBoosterTank>();
                booster.oxygenSource = oxy;
                booster.viewModel = HighCapacityBooster.viewModel;
                booster.boostSound = HighCapacityBooster.boostSound;
                booster.boostVFXPrefab = HighCapacityBooster.boostVFX;
                booster.motor = HighCapacityBooster.simpleMotor;
            }*/
            task = CraftData.GetPrefabForTechTypeAsync(TechType.HighCapacityTank, true);
            yield return task;
            Oxygen highCapOxygen = GameObject.Instantiate(task.GetResult()).GetComponent<Oxygen>();

            if (highCapOxygen != null)
            {
                oxy = go.EnsureComponent<Oxygen>();
                if (oxy != null)
                {
                    float oxygenCapacity = highCapOxygen.oxygenCapacity;
                    Logger.Log(Logger.Level.Debug, $"Found oxygen capacity of {oxygenCapacity} for prefab HighCapacityTank and existing oxygen capacity of {oxy.oxygenCapacity} for prefab HighCapacityBooster.");
                    oxy.oxygenCapacity = oxygenCapacity;
                }
                else
                {
                    Logger.Log(Logger.Level.Error, $"Could not get Oxygen component of SuitBoosterTank while generating HighCapacityBooster prefab");
                }
            }
            else
                Logger.Log(Logger.Level.Error, $"Could not get Oxygen component of HighCapacityTank while generating HighCapacityBooster prefab");

            GameObject.Destroy(highCapOxygen);

            float oxyCap = go.GetComponent<Oxygen>().oxygenCapacity;
            Logger.Log(Logger.Level.Debug, $"GameObject created with oxygenCapacity of {oxyCap}");
            gameObject.Set(go);
        }
    }
}