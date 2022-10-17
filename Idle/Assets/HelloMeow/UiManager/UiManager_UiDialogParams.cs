/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-06-17 18:06:50
 * @modify date 2020-06-17 18:06:50
 * @desc [description]
 */

#if HM_ZENJECT
using Zenject;
#endif

namespace HM.GameBase
{
    public partial class UiManager
    {
#if HM_ZENJECT
        [Inject]
#endif
        private UiDialogParams.Factory paramFactory;

        public UiDialogParams CreateParams(params object[] parameters)
        {
            return ParamsFactory.Create(parameters);
        }

        private UiDialogParams.Factory ParamsFactory
        {
            get
            {
                if (paramFactory == null)
                {
                    HMLog.LogWarning("Create UiDialogParams.Factory");
                    paramFactory = new UiDialogParams.Factory();
                }
                return paramFactory;
            }
        }


    }
}