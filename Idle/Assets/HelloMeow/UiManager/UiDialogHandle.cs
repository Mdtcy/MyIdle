using System;
using System.Collections.Generic;
using MEC;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace HM.GameBase
{
    [Serializable]
    public class UiDialogHandle : IHMPooledObject
    {
        public UiDialog Dialog;
        public Action<UiDialog, object> Hidden;
        public bool Visible;
        public object ReturnValue;
        public AsyncOperationHandle AsyncHandle;

        public IEnumerator<float> WaitForHide()
        {
            while (Visible)
            {
                yield return Timing.WaitForOneFrame;
            }
        }

        public T GetDialog<T>() where T : UiDialog
        {
            return Dialog as T;
        }

        public void OnEnterPool()
        {
            Hidden = null;
            Visible = false;
            Dialog = null;
            ReturnValue = null;
        }
    }
}