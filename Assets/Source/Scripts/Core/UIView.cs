using UnityEngine;

namespace EnergyRegen.Core
{
    public interface IUIViewModel { }

    /// <summary>
    /// Base MonoBehaviour for all views.
    /// Initialize/Release are called manually (not in Awake/OnDestroy)
    /// so the lifetime is controlled by the ViewModel or the scope.
    /// </summary>
    public abstract class UIView : MonoBehaviour
    {
        public abstract void Initialize();
        public abstract void Release();
    }

    /// <summary>
    /// Typed view that receives a ViewModel on initialization.
    /// Bindings are set up in Initialize() and torn down in Release().
    /// </summary>
    public abstract class UIView<TVm> : UIView where TVm : IUIViewModel
    {
        protected TVm ViewModel { get; private set; }

        public void Initialize(TVm vm)
        {
            ViewModel = vm;
            Initialize();
        }

        public override void Release()
        {
            ViewModel = default;
        }
    }
}
