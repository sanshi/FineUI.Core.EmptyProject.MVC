# FineUI.Core.EmptyProject.MVC

FineUI.Core.EmptyProject.MVC 是 FineUI 官方最小空项目模板。本仓库是该项目的唯一真相源，欢迎通过 Issue 和 Pull Request 参与技术讨论与改进。

<!-- fineui-community:start -->
## 加入 FineUI 社区

欢迎[加入 FineUI 社区](https://fineui.com/fans/)，一站式获取社区版、完整示例、空项目、快速入门和 AppBox 等配套资源，及时了解版本更新，交流控件用法与项目实践。**FineUI.Core、FineUI.Pro 与 FineUI.Java 社区版均可永久免费商用**，欢迎一起分享经验、讨论问题。
<!-- fineui-community:end -->

## 依赖方式

项目文件已声明从公共软件包仓库获取的 NuGet 包 `FineUI.Core`。正常联网构建时，包管理器会自动还原依赖。

## 构建

安装 .NET 8 SDK 后，在仓库根目录运行：

```powershell
dotnet restore FineUI.Core.EmptyProject.MVC.sln
dotnet build FineUI.Core.EmptyProject.MVC.sln -c Release --no-restore
```

## 运行

在仓库根目录启动（首次会先还原 NuGet 包并编译）：

```powershell
dotnet run --project FineUI.Core.EmptyProject.MVC/FineUI.Core.EmptyProject.MVC.csproj
```

启动后打开 <http://localhost:52416/> —— 地址来自 `FineUI.Core.EmptyProject.MVC/Properties/launchSettings.json` 里的 `FineUI.Core.EmptyProject.MVC` 配置。

也可以用 Visual Studio 打开 `FineUI.Core.EmptyProject.MVC.sln`，把启动配置切成 `IIS Express`（<http://localhost:52420/>）。

端口被占用时，改 `Properties/launchSettings.json` 里对应配置的 `applicationUrl` 即可。

**不需要授权文件**：本仓库引用的是公共 NuGet 包 `FineUI.Core`（社区版），社区版不做授权校验，克隆下来就能直接跑。

## 页面配置

全站默认值放在 `appsettings.json` 的 `FineUI` 节。每用户的主题 Cookie 由应用的 `AppPageManagerInitializer.Initialize(pm, request)` 读取；页面基类在 GET 处理器之前调用它。

单页配置放在页面模型的 `OnGet`（MVC 放在返回视图的 Action），使用 `PageManager.Instance`：

```csharp
var pm = PageManager.Instance;
pm.EnableWatermark = true;
pm.WatermarkText = "I❤︎FineUI";
```

配置顺序是“全站默认 → 应用公共初始化 → 单页设置 → 渲染视图”。布局保留 `@F.PageManager`、样式与脚本输出；RazorForms 中依赖控件字段的数据绑定仍放在 `Page_Load`。正常 AJAX 回发沿用已有页面状态，不重复读取 Cookie 初始化配置。

## Core 三种模式的 JSON 与页面模式

RazorForms、MVC 和 RazorPages 共用 JSON 首屏与 UI 响应解释器。MVC/RazorPages 保留 Action、OnPost、参数绑定及 `UIHelper.Result()`，使用无状态子集，不重跑模板，也不上传 RF 控件状态和事件身份字段。请求仍使用 URL 编码表单或 multipart，不要求改成 JSON body；独立业务 JSON 使用明确的数据响应通道。

官方项目显式开启严格内容安全策略（Content Security Policy，CSP）：允许同源脚本与每请求 nonce，禁止 unsafe-inline 和 unsafe-eval。三种模式未显式配置 unsafe-eval 时都默认禁止字符串执行。RazorForms 直接调用已有控件的动作方法，原生模式继续使用 UIHelper；共同操作可调用 `PageContext.InvokeFunction` 或结构化 ClientCommand。

应用级 `EnableRazorForms` 配置保留。全局开启 RF 时，特定原生 FineUI 页面在模型类或共同基类上添加可继承的 `[NoRazorForms]`；PageManager 的旧单页模式属性及对应标签、流式设置已废弃，设置时会报告迁移错误。普通独立上传、下载或 JSON OnPost 接口不要求此标记，不带 RF 专属字段时自然进入宿主处理器。

服务器与客户端资源一起升级，已经打开的旧页签应刷新后使用。首屏不增加数字协议版本，也不发送固定模式标记或签名模式凭据。

## 许可边界

本仓库中由合肥三生石上软件有限公司拥有著作权的示例或应用项目源代码采用 [MIT 许可证](LICENSE)。FineUI 各端框架源码、二进制软件包、内嵌的 FineUI.js 运行时以及 FineUI 名称、标识和商标不属于 MIT 授权范围，仍适用各自的商业或社区版许可。具体边界见 [NOTICE.md](NOTICE.md)。

## 参与贡献

请先阅读 `CONTRIBUTING.md`。安全问题请按 `SECURITY.md` 私下报告。
