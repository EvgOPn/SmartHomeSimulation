using Core.DI;
using UnityEngine;

namespace Core.UIFramework
{
    /// <summary>
    /// UIPresentationModel is responsible for displaying, binding views data,
    /// handling events from views, and providing events to the controller.
    /// It should not contain any business logic.
    /// </summary>
    /// <typeparam name="ControllerType">Controller class type for this model</typeparam>
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class UIPresentationModel<ControllerType> : DependentMonoBehaviour
    {
        /// <summary>
        /// Controller - contains a reference to the presentation model controller.
        /// Reference to the Controller provides by DI container in Awake()
        /// </summary>
        [HideInInspector]
        [Inject] public ControllerType Controller;

        /// <summary>
        /// Content - is a root object in view hierarchy
        /// </summary>
        //[Header("Presentation Model")]
        //[SerializeField] protected GameObject _content;

        protected readonly UIAnimator _uiAnimator = new UIAnimator();

        private CanvasGroup _canvasGroup;

        protected void VisibilityChange(bool visible)
        {
            //_content.SetActive(visible);
            if (visible)
            {
                _canvasGroup.alpha = 1f;
                _canvasGroup.interactable = true;
                _canvasGroup.blocksRaycasts = true;
                return;
            }

            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        protected override void OnAwake()
        {
            UIController controller = Controller as UIController;
            _canvasGroup = GetComponent<CanvasGroup>();

            if (controller == null || _canvasGroup == null)
            {
                return;
            }

            //_content.SetActive(controller.Visibility.Get());
            VisibilityChange(controller.Visibility.Get());
            controller.Visibility.onChanged.AddListener(VisibilityChange);
        }
    }
}