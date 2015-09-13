// -----------------------------------------------------------------------
//  <copyright file="AlipayNotifyInfo.cs" company="OSharp开源团队">
//      Copyright (c) 2014 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2014-12-23 21:03</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

using OSharp.Utility.Extensions;


namespace OSharp.Web.Net.Alipay
{
    /// <summary>
    /// 支付宝通知信息类
    /// </summary>
    public class AlipayNotifyInfo
    {
        /// <summary>
        /// 初始化一个 支付宝通知信息类 的新实例
        /// </summary>
        public AlipayNotifyInfo(IDictionary<string, string> dict)
        {
            NotifyId = dict["notify_id"];
            NotifyType = dict["notify_type"];
            NotifyTime = dict["notify_time"].CastTo<DateTime>();
            Subject = dict["subject"];

            TradeNo = dict["trade_no"];
            Price = dict.ContainsKey("price") ? dict["price"].CastTo<decimal>() : 0;
            Quantity = dict.ContainsKey("quantity") ? dict["quantity"].CastTo<int>() : 0;
            DisCount = dict.ContainsKey("discount") ? dict["discount"].CastTo<decimal>() : 0;
            TotalFee = dict["total_fee"].CastTo<decimal>();
            SellerEmail = dict["seller_email"];
            SellerId = dict["seller_id"];
            BuyerEmail = dict["buyer_email"];
            BuyerId = dict["buyer_id"];
            TradeStatus = dict["trade_status"].CastTo<TradeStatus>();
            IsTotalFeeAdjust = dict.ContainsKey("is_total_fee_adjust") && dict["is_total_fee_adjust"] != "N";
            UseCoupon = dict.ContainsKey("use_coupon") && dict["use_coupon"] != "N";
            OutTradeNo = dict.ContainsKey("out_trade_no") ? dict["out_trade_no"] : null;
            Body = dict.ContainsKey("body") ? dict["body"] : null;
            LogisticsType = dict.ContainsKey("logistics_type") ? dict["logistics_type"].CastTo<LogisticsType>() : LogisticsType.EXPRESS;
            LogisticsPayment = dict.ContainsKey("logistics_payment")
                ? dict["logistics_payment"].CastTo<LogisticsPayment>()
                : LogisticsPayment.SELLER_PAY;
            LogisticsFee = dict.ContainsKey("logistics_fee") ? dict["logistics_fee"].CastTo<decimal>() : 0;
            ReceiveName = dict.ContainsKey("receive_name") ? dict["receive_name"] : null;
            ReceiveAddress = dict.ContainsKey("receive_address") ? dict["receive_address"] : null;
            ReceiveZip = dict.ContainsKey("receive_zip") ? dict["receive_zip"] : null;
            ReceivePhone = dict.ContainsKey("receive_phone") ? dict["receive_phone"] : null;
            ReceiveMobile = dict.ContainsKey("receive_mobile") ? dict["receive_mobile"] : null;
            RefundStatus = dict.ContainsKey("refund_status") ? dict["refund_status"].CastTo<RefundStatus>() : RefundStatus.NONE;
            ShowUrl = dict.ContainsKey("show_url") ? dict["show_url"] : null;
            BuyerActions = dict.ContainsKey("buyer_actions") ? dict["buyer_actions"] : null;
            SellerActions = dict.ContainsKey("seller_actions") ? dict["seller_actions"] : null;
            GmtCreate = dict.ContainsKey("gmt_create") ? dict["gmt_create"].CastTo<DateTime>() : dict["notify_time"].CastTo<DateTime>();
            GmtPayment = dict.ContainsKey("gmt_payment") ? dict["gmt_payment"].CastTo<DateTime>() : dict["notify_time"].CastTo<DateTime>();
            GmtSendGoods = dict.ContainsKey("gmt_send_goods") ? dict["gmt_send_goods"].CastTo<DateTime>() : dict["notify_time"].CastTo<DateTime>();
            GmtLogisticsModify = dict.ContainsKey("gmt_logistics_modify") ? dict["gmt_logistics_modify"].CastTo<DateTime>() : dict["notify_time"].CastTo<DateTime>();
            GmtClose = dict.ContainsKey("gmt_close") ? dict["gmt_close"].CastTo<DateTime>() : dict["notify_time"].CastTo<DateTime>();
            GmtRefund = dict.ContainsKey("gmt_refund") ? dict["gmt_refund"].CastTo<DateTime>() : dict["notify_time"].CastTo<DateTime>();
        }

        #region 属性

        /// <summary>
        /// 获取 通知ID，支付宝通知校验ID，商户可以用这个流水号询问支付宝该条通知的合法性
        /// </summary>
        public string NotifyId { get; private set; }

        /// <summary>
        /// 获取 通知类型
        /// </summary>
        public string NotifyType { get; private set; }

        /// <summary>
        /// 获取 通知时间
        /// </summary>
        public DateTime NotifyTime { get; private set; }

        /// <summary>
        /// 获取 付款交易号
        /// </summary>
        public string TradeNo { get; private set; }

        /// <summary>
        /// 获取 商品名称
        /// </summary>
        public string Subject { get; private set; }

        /// <summary>
        /// 获取 商品单价
        /// </summary>
        public decimal Price { get; private set; }

        /// <summary>
        /// 获取 商品数量
        /// </summary>
        public int Quantity { get; private set; }

        /// <summary>
        /// 获取 商品折扣
        /// </summary>
        public decimal DisCount { get; private set; }

        /// <summary>
        /// 获取 订单总额
        /// </summary>
        public decimal TotalFee { get; private set; }

        /// <summary>
        /// 获取 卖家支付宝账号
        /// </summary>
        public string SellerEmail { get; private set; }

        /// <summary>
        /// 获取 卖家Id
        /// </summary>
        public string SellerId { get; private set; }

        /// <summary>
        /// 获取 买家支付宝账号
        /// </summary>
        public string BuyerEmail { get; private set; }

        /// <summary>
        /// 获取 买家Id
        /// </summary>
        public string BuyerId { get; private set; }

        /// <summary>
        /// 获取 交易状态
        /// </summary>
        public TradeStatus TradeStatus { get; private set; }

        /// <summary>
        /// 获取 总价是否调整过
        /// </summary>
        public bool IsTotalFeeAdjust { get; private set; }

        /// <summary>
        /// 获取 是否使用红包
        /// </summary>
        public bool UseCoupon { get; private set; }

        /// <summary>
        /// 获取 订单号码
        /// </summary>
        public string OutTradeNo { get; private set; }

        /// <summary>
        /// 获取 商品描述
        /// </summary>
        public string Body { get; private set; }

        /// <summary>
        /// 获取 物流类型
        /// </summary>
        public LogisticsType LogisticsType { get; private set; }

        /// <summary>
        /// 获取 物流支付类型
        /// </summary>
        public LogisticsPayment LogisticsPayment { get; private set; }

        /// <summary>
        /// 获取 物流费用
        /// </summary>
        public decimal LogisticsFee { get; private set; }

        /// <summary>
        /// 获取 收货人姓名
        /// </summary>
        public string ReceiveName { get; private set; }

        /// <summary>
        /// 获取 收货人地址
        /// </summary>
        public string ReceiveAddress { get; private set; }

        /// <summary>
        /// 获取 收货人邮编
        /// </summary>
        public string ReceiveZip { get; private set; }

        /// <summary>
        /// 获取 收货人电话
        /// </summary>
        public string ReceivePhone { get; private set; }

        /// <summary>
        /// 获取 收货人手机
        /// </summary>
        public string ReceiveMobile { get; private set; }

        /// <summary>
        /// 获取 退款状态
        /// </summary>
        public RefundStatus RefundStatus { get; private set; }

        /// <summary>
        /// 获取 商品展示URL
        /// </summary>
        public string ShowUrl { get; private set; }

        /// <summary>
        /// 获取 买家动作集合
        /// </summary>
        public string BuyerActions { get; private set; }

        /// <summary>
        /// 获取 卖家动作集合
        /// </summary>
        public string SellerActions { get; private set; }

        /// <summary>
        /// 获取 交易创建时间
        /// </summary>
        public DateTime GmtCreate { get; private set; }

        /// <summary>
        /// 获取 交易支付时间
        /// </summary>
        public DateTime GmtPayment { get; private set; }

        /// <summary>
        /// 获取 发货时间
        /// </summary>
        public DateTime GmtSendGoods { get; private set; }

        /// <summary>
        /// 获取 物流状态更新时间
        /// </summary>
        public DateTime GmtLogisticsModify { get; private set; }

        /// <summary>
        /// 获取 交易结束时间
        /// </summary>
        public DateTime GmtClose { get; private set; }

        /// <summary>
        /// 获取 交易退款时间
        /// </summary>
        public DateTime GmtRefund { get; private set; }

        #endregion
    }
}