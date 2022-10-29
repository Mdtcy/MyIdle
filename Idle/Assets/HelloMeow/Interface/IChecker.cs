using System;
namespace HM.Interface
{
    public interface IChecker
    {
        event Action<IChecker> OnConditionMet;
        bool IsConditionMet();
        void Stop();
    }
}
