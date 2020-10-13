using FlightUIModeControllerUtil;
using KSP.IO;
using KSP.UI;
using UnityEngine;
using UnityEngine.EventSystems;

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
        private bool sticky;
        private bool stickyOnLoad = false;
        private AltimeterHoverHandler hoverHandler;

        public void Start()
        {
            LoadConfig();
            GameEvents.OnMapEntered.Add(mapViewEnteredHandler);
            GameEvents.OnMapExited.Add(mapViewExitedHandler);
            altimeterFrame = FlightUIModeController.Instance.altimeterFrame;
            IVACollapseGroup = GameObject.Find("IVACollapseGroup").transform;
            topFrame = ((RectTransform)IVACollapseGroup.transform.parent);
            IVACollapseGroup.gameObject.AddComponent<AltimeterHoverHandler>();
            hoverHandler = IVACollapseGroup.GetComponent<AltimeterHoverHandler>();
            sticky = stickyOnLoad;
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(1) && hoverHandler.hover)
            {
                    sticky = !sticky;
            }

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
                    if (!sticky && !keepArea.Contains(Mouse.screenPos))
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
            stickyOnLoad = config.GetValue<bool>("stickyOnLoad", stickyOnLoad);
            config.save();
        }
    }

    /* Couldn't get IPointerClickHandler to work so we use this as a workaround */
    public class AltimeterHoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        public bool hover { get; private set; } = false;

        public void OnPointerEnter(PointerEventData eventData)
        {
            hover = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            hover = false;
        }
    }
}
