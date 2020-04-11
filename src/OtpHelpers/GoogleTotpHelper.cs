using OtpSharp;
using System;
using System.Security.Cryptography;
using System.Text;
using Wiry.Base32;

namespace OtpHelpers
{
    /// <summary>
    /// 谷歌认证器帮助类
    /// </summary>
    public static class GoogleTotpHelper
    {
        /// <summary>
        /// 获取认证器二维码url
        /// </summary>
        /// <param name="secretKey"></param>
        /// <param name="userName"></param>
        /// <param name="appName"></param>
        /// <returns></returns>
        public static string GetAuthenticatorUrl(string secretKey, string userName, string appName)
        {
            var barcodeUrl = string.Empty;
            var secretKeyBytes = GetSecretKeyBytes(secretKey);
            barcodeUrl = $"{KeyUrl.GetTotpUrl(secretKeyBytes, userName)}&issuer={appName}";

            return barcodeUrl;
        }

        /// <summary>
        /// 获取google认证器secretkey-bytes
        /// </summary>
        /// <param name="stringSecretKey"></param>
        /// <returns></returns>
        private static byte[] GetSecretKeyBytes(string stringSecretKey)
        {
            return Base32Encoding.Standard.ToBytes(stringSecretKey);
        }

        /// <summary>
        /// 生成新的google认证器secretkey
        /// </summary>
        /// <returns></returns>
        public static string GenerateNewSecretKey()
        {
            const int length = 16;
            var ran = new Random();
            var s = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var str = "";
            for (int i = 0; i < length; i++)
            {
                str += s[ran.Next(0, s.Length)];
            }

            return str;
        }

        /// <summary>
        /// 验证google认证器token
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool VerifyToken(string secretKey, string token)
        {
            var timeStepMatched = 0L;
            var secretKeyBytes = GetSecretKeyBytes(secretKey);
            var otp = new Totp(secretKeyBytes);
            bool valid = otp.VerifyTotp(token, out timeStepMatched, new VerificationWindow(2, 2));

            return valid;
        }

        /// <summary>
        /// 验证token-指定时间
        /// </summary>
        /// <param name="secretKey"></param>
        /// <param name="token"></param>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static bool VerifyToken(string secretKey, string token, long timestamp)
        {
            var timeStepMatched = 0L;
            var secretKeyBytes = GetSecretKeyBytes(secretKey);
            var otp = new Totp(secretKeyBytes);
            var dt = DateTimeOffset.FromUnixTimeSeconds(timestamp);
            return otp.VerifyTotp(dt.UtcDateTime, token, out timeStepMatched, new VerificationWindow(2, 2));
        }

        /// <summary>
        /// 计算token
        /// </summary>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        public static string ComputeToken(string secretKey)
        {
            var otp = new Totp(GetSecretKeyBytes(secretKey));
            return otp.ComputeTotp();
        }

        /// <summary>
        /// 计算token-指定时间
        /// </summary>
        /// <param name="secretKey"></param>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static string ComputeToken(string secretKey, long timestamp)
        {
            var otp = new Totp(GetSecretKeyBytes(secretKey));
            var dt = DateTimeOffset.FromUnixTimeSeconds(timestamp);
            return otp.ComputeTotp(dt.UtcDateTime);
        }
    }
}
