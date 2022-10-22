namespace NewLife.BusinessLogic.Request
{
    public class RequestErrorDef
    {

        // app level error code starts with 110001xxx
        public static readonly int ErrCode_DupSkin = 110001001;
        public static readonly int ErrCode_ConfNotFound = 110001002;
        public static readonly int ErrCode_ItemNotOwn = 110001003;
        public static readonly int ErrCode_ItemNotEnough = 110001004;
        public static readonly int ErrCode_SkinNotWear = 110001005;
        public static readonly int ErrCode_InvalidParams = 110001006;
        public static readonly int ErrCode_NotEnoughCoin = 110001007;

        public static string GetErrMsg(int errCode)
        {
            // TODO: read from ConfError
            return errCode.ToString();
        }
    }
}