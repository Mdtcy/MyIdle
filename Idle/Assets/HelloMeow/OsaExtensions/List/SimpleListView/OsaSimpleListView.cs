/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-11-25 14:11:13
 * @modify date 2020-11-25 14:11:13
 * @desc [简单列表UI模板（只有一种prefab且需使用pool）]
 */

#pragma warning disable 0649
namespace HM.OsaExtensions
{
    public class OsaSimpleListView<TModel, TParams, TItemViewsHolder> : OsaBaseListView<TModel, TParams, TItemViewsHolder>
        where TModel : OsaBaseModel
        where TParams : OsaSimpleListParams
        where TItemViewsHolder : OsaBaseViewsHolder, new()
    {
        /// <inheritdoc />
        protected override TItemViewsHolder CreateViewsHolder(int itemIndex)
        {
            var inst = new TItemViewsHolder {InstantiateDelegate = _Params.Pool.Spawn};
            inst.Init(_Params.Prefab, Content, itemIndex);
            return inst;
        }
    }
}
#pragma warning restore 0649