# 如何：使用菜单和悬浮窗状态

## 编写事件处理程序

事件处理程序必须为静态方法。

### 菜单项被点击

菜单项被点击事件没有任何形参，也没有返回值。

```csharp
static void OnMenuItemClicked();
```

### 悬浮窗状态更新

悬浮窗状态更新事件没有任何形参，返回一个字符串。返回的字符串是以 Base64 编码的状态数据。

```csharp
static string OnStatusUpdating();
```

通过 @"HuajiTech.CoolQ.Status" 类和 @"HuajiTech.CoolQ.Status.Encode" 方法可以方便地创建状态数据并进行编码。

```csharp
static string OnStatusUpdating()
{
	return new HuajiTech.CoolQ.Status(
		value: "值",
		unit: "单位",
		color: HuajiTech.CoolQ.StatusColor.Green
    ).Encode();
}
```

## 将托管方法导出为非托管方法

上面创建的事件处理程序是托管方法，但酷Q只能调用以 StdCall 导出的非托管方法。
通过 `HuajiTech.UnmanagedExports.DllExportAttribute` 可以标记要导出的方法。

```csharp
[HuajiTech.UnmanagedExports.DllExport]
static string OnStatusUpdating();
```

通过设置 `HuajiTech.UnmanagedExports.DllExportAttribute.EntryPoint` 属性，可以自定义导出后的方法的入口点名称。
如果 `EntryPoint` 属性为 `null`，将会使用方法名称作为入口点名称。
不能定义重复的入口点名称。

```csharp
[HuajiTech.UnmanagedExports.DllExport(EntryPoint = "OnMenuItemClicked")]
static void OpenConfiguration();
```

## 配置 `app.json`

在 `app.json` 中的 `status` 或 `menu` 下增加相应格式的元素，并将 `function` 的值设为导出的入口点名称。

```json
"menu": [
  {
    "name": "菜单项",
    "function": "OnMenuItemClicked"
  }
],
"status": [
  {
    "id": 1,
    "name": "悬浮窗",
    "title": "FLOAT",
    "function": "OnStatusUpdating",
    "period": "1000"
  }
]
```
