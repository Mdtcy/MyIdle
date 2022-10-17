/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-12-23 13:12:41
 * @modify date 2020-12-23 13:12:41
 * @desc [继承该接口表示可以被注入]
 */

using Zenject;

namespace HM.Extenject
{
    public interface IInjectable
    {
        /// <summary>
        /// 用指定容器注入依赖
        /// </summary>
        /// <param name="container"></param>
        void OnWillInject(DiContainer container);
    }
}