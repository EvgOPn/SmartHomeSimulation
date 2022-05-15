using UnityEngine;

namespace Core.DI
{
    [Dependent]
    public abstract class DependentMonoBehaviour : MonoBehaviour
    {
        /// <summary>
        /// This method replaces the Awake() method in all child classes in DependentMonoBehaviour class
        /// Be careful and do not refer to injected references in Awake() and OnAwake() methods! Refer only in the Start()
        /// </summary>
        protected abstract void OnAwake();

        /// <summary>
        /// Be careful. If you have to use Awake() in any subclass, do not forget to call base.Awake() ore use OnAwake()
        /// </summary>
        protected void Awake()
        {
            Binder.UpdateDependencies();
            OnAwake();
        }
    }
}