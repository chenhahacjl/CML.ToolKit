 _______  __   __  ___           _______  _______  _______  ___      ___   _  ____  _______
|       ||  |_|  ||   |         |       ||       ||       ||   |    |   | | ||    ||       |
|    ___||       ||   |         |_     _||   _   ||   _   ||   |    |   |_| ||    ||_     _|
|   |    |       ||   |           |   |  |  | |  ||  | |  ||   |    |     __||    |  |   |
|   |    | || || ||   |___  ___   |   |  |  |_|  ||  |_|  ||   |___ |    |__ |    |  |   |
|   |___ | ||_|| ||       ||   |  |   |  |       ||       ||       ||    _  ||    |  |   |
|_______||_|   |_||_______||___|  |___|  |_______||_______||_______||___| |_||____|  |___|

## 简介
CML.ToolKit是一个基于C#的编程工具包，其中包含WinForm控件、Socket通讯、配置操作、加密解密等功能。

## 开发环境
* Language: C# 7.1
* IDE: Visual Studio 2019
* Framework: .Net Framework 4.0

## 当前进度
- [ ] CML.CommonEx <常用工具包>
	- [ ] Configuration <配置操作工具>
		- [x] IniOperate <INI配置文件操作类>
		- [x] RegOperate <注册表操作类>
		- [x] RegOperateEF(ExFunction) <注册表操作类(扩展方法)>
	- [ ] DataBase <数据库操作工具>
		- [x] MySQL <MySQL数据库支持库>（需要引用[MySql.Data]NuGet项目或[MySql.Data.dll]文件）
		- [x] Oracle <Oracle数据库支持库>（需要引用[Oracle.ManagedDataAccessNuGet]项目或[Oracle.ManagedDataAccess.dll]文件）
		- [x] SqlServer <SqlServer数据库支持库>
	- [ ] Encode <数据编码工具>
		- [x] DESEncrypt <DES加密解密操作类>
		- [x] DESEncryptEF(ExFunction) <DES加密解密操作类(扩展方法)>
		- [x] MD5Encrypt <MD5加密操作类>
		- [x] MD5EncryptEF(ExFunction) <MD5加密操作类(扩展方法)>
	- [ ] Enum <枚举操作工具>
		- [x] EnumOperate <枚举操作类>
		- [x] EnumOperateEF(ExFunction) <枚举操作类(扩展方法)>
	- [ ] FTP <FTP操作工具>
		- [x] FTPOperate <FTP操作类>
		- [x] FTPOperateEF(ExFunction) <FTP操作类(扩展方法)>
	- [ ] IDNumber <身份证号操作工具>
		- [x] IDNumberOperate <身份证号操作类>
		- [x] IDNumberOperateEF(ExFunction) <身份证号操作类(扩展方法)>
	- [ ] Network <网络操作工具>
		- [x] DownloadOperate <下载操作类>
		- [x] DownloadOperateEF(ExFunction) <下载操作类(扩展方法)>
	- [ ] Regex <正则表达式工具>
		- [x] RegexOperate <正则表达式操作类>
		- [x] RegexOperateEF(ExFunction) <正则表达式操作类(扩展方法)>
		- [x] UserAgentHelper <UserAgent帮助类>
	- [ ] Singleton <单实例工具>
		- [x] SingletonBase <单实例类基类>
	- [ ] Thread <线程操作工具>
		- [x] InvokeOperate <委托操作类>
		- [x] InvokeOperateEF(ExFunction) <委托操作类(扩展方法)>
	- [ ] UIAutomation <UI自动化操作工具>
		- [x] UIAutomationOperate <UI自动化操作类>
		- [x] UIAutomationOperateEF(ExFunction) <UI自动化操作类(扩展方法)>
	- [ ] Version <版本管控工具>
		- [x] VersionBase <版本控制基类>
- [ ] CML.ControlEx <WinForm控件包>
	- [ ] Expand <拓展控件>
		- [x] CmlButtonEx <按钮控件>
		- [x] CmlCheckBoxEx <复选框控件>
		- [x] CmlDataGridViewEx <数据表控件>
		- [x] CmlTabControlEx <选项卡控件>
		- [x] CmlTextBoxEx <文本框控件>
	- [ ] Original <自定义控件>
		- [x] CmlChartCurve <图表控件>
		- [x] CmlFormMoveTool <窗体拖动控件>
		- [x] CmlLanternAlarm <警示灯控件>
		- [x] CmlLanternRound <圆形灯控件>
		- [x] CmlShapeDiamond <菱形形状控件>
		- [x] CmlShapeRectangle <矩形形状控件>
		- [x] CmlShapeRound <圆形形状控件>
		- [x] CmlPanelTitle <标题分组控件>
	- [ ] Form <窗体控件>
		- [x] CmlFormLogin <登录窗体控件>
		- [x] CmlFormValueInput <数值输入窗体控件>
- [ ] CML.EntertainmentEx <娱乐包>
	- [ ] Friend <朋友游戏>
- [ ] CML.SoftwareToolEx <软件工具包>
	- [ ] Socket <Socket通讯工具>
		- [x] SocketClient <Socket客户端>
		- [x] SocketServer <Socket服务端>

## GitHub
https://github.com/chenhahacjl/CML.ToolKit](https://github.com/chenhahacjl/CML.ToolKit

## 版权申明
Copyright (C) 2019 Cmile_96, All Rights Reserved.
