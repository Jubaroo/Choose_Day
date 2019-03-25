using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModAPI.Attributes;
using TheForest.Utils;
using UnityEngine;

namespace Choose_Day
{
    internal class ChooseDay : MonoBehaviour
    {
        [ExecuteOnGameStart]
        private static void AddMeToScene()
        {
            new GameObject("__BetterBlueprints__").AddComponent<ChooseDay>();
        }

        private bool visible;
        protected GUIStyle labelStyle;
        private string day = "";

        private void OnGUI()
        {
            if (visible)
            {
                // use ModAPI Skin
                GUI.skin = ModAPI.Interface.Skin;

                // apply label style if not existing
                if (this.labelStyle == null)
                {
                    this.labelStyle = new GUIStyle(GUI.skin.label)
                    {
                        fontSize = 12
                    };
                }

                // create box (background)
                GUI.Box(new Rect(10f, 10f, 300f, 150f), "", GUI.skin.window);

                // Label
                GUI.Label(new Rect(30f, 15f, 200f, 20f), "Choose The Current Game Day", this.labelStyle);

                // Text-input
                day = GUI.TextField(new Rect(30f, 50f, 200f, 30f), day, GUI.skin.textField);

                // Button
                if (GUI.Button(new Rect(30f, 100f, 80f, 20f), "Set Day"))
                {
                    Clock.Day = Int32.Parse(day);
                }
            }
        }

        private void Update()
        {
            // if clicked button
            if (ModAPI.Input.GetButtonDown("Menu"))
            {
                // show cursor
                if (visible)
                {
                    LocalPlayer.FpCharacter.UnLockView();
                }
                else
                {
                    LocalPlayer.FpCharacter.LockView(true);
                }
                // toggle menu
                visible = !visible;
            }
        }
    }
}
