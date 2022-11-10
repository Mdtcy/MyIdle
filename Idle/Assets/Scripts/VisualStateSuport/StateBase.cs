#pragma warning disable 0649
namespace VisualStateSupport
{
    public abstract class StateBase
    {
        public abstract void OnEnter();

        public abstract void OnUpdate();

        public abstract void OnExit();
    }
}
#pragma warning restore 0649