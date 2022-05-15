using System;
using Core.DI;
using System.Linq;
using System.Reflection;

namespace Core.UIFramework
{
    /// <summary>
    /// UIController contains all business logic and provides data to the UIPresentationModel by Observable values
    /// </summary>
    public abstract class UIController : DependentMonoBehaviour
    {
        public readonly Observable<bool> Visibility = new Observable<bool>(false);
        
        public void Show() =>  Visibility.Set(true);
        public void Hide() => Visibility.Set(false);
        public void ReverseVisibility() => Visibility.Set(!Visibility.Get());

        /// <summary>
        /// Reset all child objects with IResettable interface to default state
        /// </summary>
        public void ResetState()
        {
            foreach (FieldInfo field in GetType().GetFields())
            {
                Type fieldType = field.FieldType;
                if (fieldType.GetInterfaces().Contains(typeof(IResettable)))
                {
                    IResettable resettable = field.GetValue(this) as IResettable;
                    resettable?.ResetState();
                }
            }
        }
    }
}