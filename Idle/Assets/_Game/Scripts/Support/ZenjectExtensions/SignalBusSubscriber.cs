/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-08-29 16:12:54
 * @modify date 2022-08-29 16:12:54
 * @desc [description]
 */

using System;
using System.Collections.Generic;
using Zenject;

#pragma warning disable 0649
namespace NewLife.Support
{
    public class SignalBusSubscriber
    {
        #region FIELDS

        // * injected
        [Inject]
        private SignalBus signalBus;

        // * local
        private Action callback;

        private readonly List<bool> states = new List<bool>(2);

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// 同时收到指定两个信号后回调
        /// </summary>
        /// <param name="callback"></param>
        /// <typeparam name="TSignal1"></typeparam>
        /// <typeparam name="TSignal2"></typeparam>
        public void Subscribe<TSignal1, TSignal2>(Action callback)
        {
            states.Clear();
            states.Add(false);
            states.Add(false);

            this.callback = callback;

            signalBus.Subscribe<TSignal1>(OnState0);
            signalBus.Subscribe<TSignal2>(OnState1);
        }

        /// <summary>
        /// 取消监听
        /// </summary>
        /// <typeparam name="TSignal1"></typeparam>
        /// <typeparam name="TSignal2"></typeparam>
        public void Unsubscribe<TSignal1, TSignal2>()
        {
            callback = null;

            signalBus.TryUnsubscribe<TSignal1>(OnState0);
            signalBus.TryUnsubscribe<TSignal2>(OnState1);
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void OnState0()
        {
            states[0] = true;
            CheckStates();
        }

        private void OnState1()
        {
            states[1] = true;
            CheckStates();
        }

        private void CheckStates()
        {
            foreach (bool state in states)
            {
                if (!state)
                {
                    return;
                }
            }

            callback?.Invoke();
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649