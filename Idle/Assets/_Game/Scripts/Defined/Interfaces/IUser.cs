namespace NewLife.Defined.Interfaces
{
    public interface IUser
    {
        /// <summary>
        /// UserId
        /// </summary>
        string Id { get; }

        /// <summary>
        /// 年龄
        /// </summary>
        int Age { get; }

        /// <summary>
        /// 是否同意了用户隐私协议
        /// </summary>
        bool PrivacyAgreed { get; }

        void AgreePrivacy();
    }
}