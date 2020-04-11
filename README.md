# OtpHelpers
otp帮助类，提供otp算法secretkey生成，token计算、验证等功能（可配合谷歌认证器使用）

### 依赖
- netstandard2.0
- OtpSharp.Core (https://github.com/ByronAP/OtpSharp.Core)
### 模块
#### 谷歌认证器帮助类 GoogleTotpHelper
	//获取认证器二维码url
	GoogleTotpHelper.GetAuthenticatorUrl(secretKey,userName,appName);
	
	//生成新的secretkey
	GoogleTotpHelper.GenerateNewSecretKey();

	//验证token
	GoogleTotpHelper.VerifyToken(secretKey,token);

	//验证token-指定时间
	GoogleTotpHelper.VerifyToken(secretKey,token,timestamp);

	//计算token
	GoogleTotpHelper.ComputeToken(secretKey);

	//计算token-指定时间
	GoogleTotpHelper.ComputeToken(secretKey,timestamp);