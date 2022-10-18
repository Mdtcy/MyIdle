/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-09-08 15:44:23
 * @modify date 2022-09-08 15:44:23
 * @desc [邮件]
 */

using System;
using System.Collections.Generic;
using HM.Extensions;
using HM.GameBase;
using HM.Interface;

namespace NewLife.Defined
{
    public class Mail
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id;

        /// <summary>
        /// 标题
        /// </summary>
        public string Title;

        /// <summary>
        /// 内容
        /// </summary>
        public string Content;

        /// <summary>
        /// 奖励列表
        /// </summary>
        public List<CountingItem> Rewards;

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpireDate;

        /// <summary>
        /// 是否已读
        /// </summary>
        public bool HasRead { get; private set; }

        /// <summary>
        /// 是否已领取
        /// </summary>
        public bool HasClaimed { get; private set; }

        /// <summary>
        /// 是否附带奖励
        /// </summary>
        public bool RewardsAttached => !Rewards.IsNullOrEmpty();

        #region Overrides of Object

        /// <inheritdoc />
        public override string ToString()
        {
            return $"[Mail {Id} / {Title} / {HasRead} / {HasClaimed} / {Content} / {ExpireDate} / {Rewards.ListToString()}]";
        }

        public void MarkAsRead()
        {
            HasRead = true;
        }

        public void MarkAsClaimed()
        {
            HasClaimed = true;
        }

        #endregion

        public class Factory
        {
            private readonly IConfigGetter configGetter;

            public Factory(IConfigGetter configGetter)
            {
                this.configGetter = configGetter;
            }

            public Mail Create(int id, string title, bool hasRead, bool hasClaimed, string content, DateTime expireDate, ItemParams rewards)
            {
                var inst = new Mail
                {
                    Id         = id,
                    Title      = title,
                    Content    = content,
                    ExpireDate = expireDate,
                    HasRead    = hasRead,
                    Rewards    = rewards != null ? new List<CountingItem>(rewards.Count) : new List<CountingItem>(),
                    HasClaimed = hasClaimed,
                };

                if (rewards != null)
                {
                    foreach (var reward in rewards)
                    {
                        inst.Rewards.Add(CountingItem.Claim(configGetter.GetConfig<BaseConfig>(reward.ItemId),
                                                            reward.Num));
                    }
                }

                return inst;
            }
        }
    }
}