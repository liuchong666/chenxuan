using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Common
{
    public class ConfigContext
    {
        public static string LayUiResource => "http://123.56.29.119:8080/layui";

        public static string ConnctionString => ConfigurationManager.ConnectionStrings["SunMedicalDB"].ConnectionString ?? string.Empty;
        public static string Db =>ConfigurationManager.AppSettings["db"];

        /// <summary>
        /// 公司名称
        /// </summary>
        public static string CompanyName => ConfigurationManager.AppSettings["CompaneyName"];

        public static string MeetingManageSiteDomain => ConfigurationManager.AppSettings["ManageUrl"]; 
        public static string MeetingViewSiteDomain => ConfigurationManager.AppSettings["ViewUrl"];

        public static string LoginUrl = ConfigurationManager.AppSettings["LoginUrl"] ?? string.Empty;

        // 应用ID,您的APPID
        public static string app_id = "2016091400508817";

        // 支付宝网关
        public static string gatewayUrl = "https://openapi.alipaydev.com/gateway.do";

        // 商户私钥，您的原始格式RSA私钥
        public static string private_key = "MIIEpQIBAAKCAQEAv5Lg1CSZip8fkYoUGvypV0GKLEOFFSGYSTxFbxUPI77pkl9q4ag3L/BDkpgLzPnH7NM0EmdZ8Ku4UjHYrolVc3LA/4jD6Z0T3oO7xQDuXNNY5YrhqwX7aukLbTrhx0cyJRbJDBPkTWW2qjmsjOnOry4wFjgT8l0DBldAtZstPGg+I9eo9YNlqCzgDHwLMKGO1CuQzK3fB9QsSge7MlhcWz+GUGb+jwtU/I7PG9ez5kg+3axTSQnqdfo9xkLZTbEqjF/+W+nX9qA5Zs0U+LHPkgAB/jl9A3aSLnxInBWDsCTi1gBWGEjwgvDBs9e5DKz1Yb6qZl6G56qTLB5JZ9wAYQIDAQABAoIBAQCvmIq6CR4vwLDn7z+EiQkTMMDc17rZZqS2pCckrZHl+u0PPKS0WELVjw3xBarzZBRL0D+PiuoWvt/Bf20Udk879fhPgWXJ8S+wKuFmrvbNYO+/3vAAmggcs6XEpk8jIPAmrN71UhpWkx++ogS2WO0ZB2YOR7KI2ZaXLkzi6WyqS2xpJBJ870/BJisvYGPgaTVw3mz8+J14fkfTidd0+GrQ+9uDHUzRZ8m2JALvVOBgYiHtH6UEJd0dWyJyd97OfRIPrdAUVfCQ02kq+UQZ56lyQaFQgQkDmdvAfjbJMwOb+VHzpYJn2qYNDqTrF8QXp3A9ZZkI3V8QQ0hm2nt01VTdAoGBAP9TcMYzvRo/+X2uMGayFrEboLvXJ/5rHuBpCRxl8+Jrof/pz0CdUNJCyAC2YNcjjWEnszGzrCxUbU3p/kOxI9ugcL6TxR5G5ztTOnASNaEm9SP7O+/U6OmHMisWvRHAI0KI5axiZ9MKutD/hUNbfxiydGJOFOvNB40knA+QvMzvAoGBAMAUWfdS0MeL0dfAoDAVqvPWnxVudgScD71Omck6WffSmW1umMyUddjMSk6M6SOXjaLZIz1cknDZjtAawB+bCzynMfLn3wS0bF0/6cXQ/qZBXiH2c4UBXi7Mo3ac1Qe6uTv6e2oTCLF22BHPTty04D/zkPrpH1XLaFEdQrE8pKevAoGBAJDGlna8laiRx/1czPMeXkGUweJhELtxsDNdO55PrSehEl3TDQK9cTuYRGIrB+RVs2/4m+I92A3W3ZfFa7ikdAmwAt39WlNdrvXzrnonySMUvQHnDkhiuKLaVzcZkKHFCflLOLgzoF2Jgv7JNJ5J/lpZriaj0bcXLgX5UUh+MKAfAoGBAINjKGsaNf/ZL3ASrkBWAfsLWwRQ69+ZVNbcViukg3gWkaaeWVzPTJFApK94id0zxMmZGBbYJL+Wwa6uLa8w4g3aHDuYyh+wvtNGbcVHgXj+C8LVjw1y8xa2GjZ/buT7n8tcOmFDpdWmgoQkN0CXZTG5jPlkz29jVPbvOfh8j/C3AoGAapTAMCj4JO54D2SJifreFnVbpJhV5aSMQYvxRjhzM//l14CYBH9gg0gIInwmpPObBjbWiHVHcoUHF0B4sKp3CLvzPgneBT9DGIEMoIG3l3HWn9n4W2xBTKmspX6MhSKXdH39CryPtx/w4nIHjV0Csl4wNh5hj7dE0/4f8xqvY40=";

        // 支付宝公钥,查看地址：https://openhome.alipay.com/platform/keyManage.htm 对应APPID下的支付宝公钥。
        public static string alipay_public_key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA6QbUesW4d5idZ6TF/to/7a2r1YK0dxUQ+zV8XRuk2oRJaRCsw2zY4x4vTkggdMgPXBhZrr9hseIDVDjL+nZH/Vc44HeRoHJHgMRvgM10CGgKoHun41Yoj5rGALjxL6qrKh4y6ig3ltPInovYKz+bZWyCPIpRewmOW14EykfdEGazYOAVkjyATD00XsqER6Ce1e5HK1LoNM6sL4M+9pmFuSdy6Ey3+33Gp//qbj6lmpcu5cdRh/S5PXDM51dHhYN/BtTDA6oqCwNH3MCcIXFY+1FxTPwJih3DW+NkALumRRCLqvECB/RcA+2lU4Pxqkmy7PrWt6i6Nc9XxdVwMOSCmQIDAQAB";

        // 签名方式
        public static string sign_type = "RSA2";

        // 编码格式
        public static string charset = "UTF-8";
    }
}
