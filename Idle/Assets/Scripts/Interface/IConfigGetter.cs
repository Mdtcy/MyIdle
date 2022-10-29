using HM.GameBase;

namespace HM.Interface
{
    public interface IConfigGetter
    {
        T GetConfig<T>(int itemId) where T : BaseConfig;
    }
}