using FlightUIModeControllerUtil;
using KSP.IO;
using KSP.UI;
using SaveUpgradePipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AltimeterAutoHide
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class AltimeterAutoHide : MonoBehaviour
    {
        private Transform IVACollapseGroup;
        private RectTransform topFrame;
        private UIPanelTransition altimeterFrame;
        private bool mapView = false;
        private Vector2 activationPadding = new Vector2(20, 40);

        public void Start()
        {
            LoadConfig();
            GameEvents.OnMapEntered.Add(mapViewEnteredHandler);
            GameEvents.OnMapExited.Add(mapViewExitedHandler);
            altimeterFrame = FlightUIModeController.Instance.altimeterFrame;
            IVACollapseGroup = GameObject.Find("IVACollapseGroup").transform;
            topFrame = ((RectTransform)IVACollapseGroup.transform.parent);
        }

        public void Update()
        {
            if (!mapView && IVACollapseGroup != null && topFrame != null && altimeterFrame != null)
            {
                float height = GameSettings.UI_SCALE * topFrame.localScale.y * topFrame.rect.height / 2;
                float width = GameSettings.UI_SCALE * topFrame.localScale.x * topFrame.rect.width;
                // Using IVACollapseGroup instead of TopFrame for x-postion to be compatible with Draggable Altimeter
                float leftBorder = IVACollapseGroup.position.x + (Screen.width / 2) - (width / 2);

                Rect showArea = new Rect(leftBorder - activationPadding.x, 0, width + activationPadding.x, activationPadding.y);
                Rect keepArea = new Rect(showArea);
                keepArea.height += height;

                if (altimeterFrame.State == TabAction.COLLAPSE.TransitionStateName())
                {
                    if (showArea.Contains(Mouse.screenPos))
                    {
                        altimeterFrame.Transition(TabAction.EXPAND.TransitionStateName());
                    }
                }
                else
                {
                    if (!keepArea.Contains(Mouse.screenPos))
                    {
                        altimeterFrame.Transition(TabAction.COLLAPSE.TransitionStateName());
                    }
                }
            }
        }

        public void OnDisable()
        {
            GameEvents.OnMapEntered.Remove(mapViewEnteredHandler);
            GameEvents.OnMapExited.Remove(mapViewExitedHandler);
        }

        private void mapViewEnteredHandler() => mapView = true;
        private void mapViewExitedHandler() => mapView = false;

        private void LoadConfig()
        {
            PluginConfiguration config = PluginConfiguration.CreateForType<AltimeterAutoHide>();
            config.load();
            activationPadding = config.GetValue<Vector2>("activationPadding", activationPadding);
            config.save();
        }
    }
}
