using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.App_Start
{
    public class AliPayParams
    {
        // 应用ID,您的APPID(沙箱)
        //public static string app_id = "2016102100733255";

        // 应用ID,您的APPID
        public static string app_id = "2021001163654685";

        // 支付宝网关(正式地址)
        public static string gatewayUrl = "https://openapi.alipay.com/gateway.do";

        //支付宝网关(沙箱地址)
        //public static string gatewayUrl = " https://openapi.alipaydev.com/gateway.do";


        // 商户私钥，您的原始格式RSA私钥
        public static string private_key = "MIIEowIBAAKCAQEAg/lzlH/VmY9z6VuFtNAdlp1zOHfz8jwJTm4dxc/x7WW+yWwsYz+jL1kNo/hJyOv252bE1hC8SDvCQ1QRZyr0vJJMo6rRFc6jtRtPqSTfWUEnvaYmHs6qZUhZycum2q0NDcEhwdBTRRDPA6ldJdDkbzA1QERmGdpKt6E7y+GfNUtQmX34HtXYcrqmYrjp2HJuTNij5J83PhJfA35tM8BKqhYfjznFGTHprVvOUOYpPdp0fDt59nN0lgKV//F03I9Vwxe56HDddshCrGUl5wrzI9WwSfNxerMH7JL+iexoNc9QyhoUXX+3jToja03Q0VDDc+OB5B90Zm8fsz5klfooRQIDAQABAoIBAEV9d8pddU8iJdpeAo0ueCXySdUV9DE8Q3o0qSCbWbuysz7D4qtvurEXpwMLnW5aMOuvRT2iGkG+lY92GKPGDo+sSQZ0zxs4byLReqSJccFBeXd2oOPf1gD2k7CZtwW10QXJB8lRtEbNjvNSnmVnFKEnNSLd4RUDyAvgreewqXAZsNBdTfB0gm9Xp5afZfng9q+cT/mEf6kUr1N5xrMx2+1U4C0CQpYZ1EZ/cFHI+k+xoNwFtNFQ02V3KtzLNbXdNjeClvKT1QTqDpl62K+QCrjdHi+qQLz0xUEvFTHq1jhwyGW9y3jIQV4Ujw1EQ86zAv6NKZuYUwoIbZfYUdqIZhECgYEA0gUjUMoQdTCIMp3TL0Gg8gXLey1eBZUs7aijPNCBxnKcoHXb4cC26PHisunmhwUCgnemtYhz9WaxWxfQWUmqadJKtIBfjhhC6wIsqChh6bzq5KxTFt0dIb8yiSVjVtmPuTP5F6ukpdLkXKgTdUCMJaeZTEznC1v87IaMhgJr7TMCgYEAoN4jaaepriC9GS+z3yNsQyZ5+kqsR5tkU7WIWho9x/gVmgeDsjPMzBZ031UgAfI/tlSnKLxqOtuOtniCTVRg5nIOWHxftmSNNbD5xfvX4B5OLnwPv4stIe0/iDp8moUtN7Cry1tjDO7YIIsedLURhQIlxKUQXDQ7PvQITEpr5KcCgYBMY1wF/Yjg5wcSyf7PGGLOxLbPFaA2DZ0q0ASXkQsyv+siHdwdD3g34ArholubzoGsLJLxQjFTF8f4Zv/8CeQ22ysa3fpjd4WXbtIJU67RUkppZBhQn5oB9UYPRg8MvtMcvL+kEWUwfzVaUjPJmPGgnIYQj4QVyJ6E9SdVcroIqwKBgES7RIQi56OHR2QYdpCzENGMAffHqd4abX46hJyCs7zZqVkbNtCPw0O7oIm/VCKgv0oG1zjXb8fgARTDXmDt2Uz8lFaIJVjHk3HKQJ2voaeXKy4/QXdEXq8tL8TjwFNA7XXq3SvQyvw+nEcjSYrxxh9/MGXZZKkQg/O8zkLSVVDnAoGBAJSzhxYAXvazm1FI8+wT4Ghe/G4u6sUsdZvLtElC0EYdUBPFHqgoYpF7STGsrIl+mSzjcdQpiY9Kf2x8ueDIfeRG/I0l5o3FyhX/5mm/MYBm2xtf9nZD9jQnd0z+wp5IrQDCYzko/+e6lt3VVkzjiQ+0WkvDBRJwturFkNUEYR9F";

        // 支付宝公钥,查看地址：https://openhome.alipay.com/platform/keyManage.htm 对应APPID下的支付宝公钥。
        public static string alipay_public_key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAs4dY+d1S5Kpi36R9buu7e8rBKPdveT6UF+woOAscMNfuxKf/j+icMd2LCEH8qyCHTza7VvFsgLQXYtGZNQXZ6lm9QiHw5gGCaHN410jPRuUNOeONsHJBn7/wZxrtiSCY6YkNMvuV/3cJnUnRcoCG0wG1p/7MsUZCkvHdyrMUBFmiD9h6mxOLmTr9Er7ytMHpzwccJHYw6AkhGOGQUo8G0CtUz4a6fvlK98X8gEQW2/pAf9dZMy0I68HXOPDHtlywUhqyXvnDSyX3OiQZc1KBY4WwSEeglo3Br93XIvPZA7OraP3RHJTPoZUmMGafOapPBYXivABghU6/vDqoWSz+LQIDAQAB";

        // 签名方式
        public static string sign_type = "RSA2";

        // 编码格式 
        public static string charset = "UTF-8";

        // 设置支付完成同步回调地址
        public static string SetReturnUrl = "http://www.hubeizhenfu.cn";

        // 设置支付完成异步通知接收地址
        public static string SetNotifyUrl = "http://www.hubeizhenfu.cn/shop.api/api/AliPayNotify/AsyAliPayNotifyInfo";
    }
}
