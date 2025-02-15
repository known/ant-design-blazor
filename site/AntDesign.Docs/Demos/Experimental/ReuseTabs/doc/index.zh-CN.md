---
category: Experimental
type: 布局
title: ReuseTabs
subtitle: 路由复用
cols: 1
cover: https://gw.alipayobjects.com/zos/antfincdn/lkI2hNEDr2V/Tabs.svg
---

利用选项卡切换组件，实现应用内的页面标签与页面缓存。

## 何时使用

- 需要使用应用内页面标签，保留打开过的页面
- 需要缓存页面状态，返回已打开页面状态不丢失

## 使用方法
 
修改 `MainLayout.razor` 文件, 增加 `ReuseTabs` 组件。注意，不级联传入 RouteData 时，`@Body` 是必须的。

   ```razor
   @inherits LayoutComponentBase

   <div class="page">
       <div class="sidebar">
           <NavMenu />
       </div>

       <div class="main">
         <ReuseTabs Class="top-row px-4" TabPaneClass="content px-4" Body="Body" / >
       </div>
   </div>

   ```

只需这一步，就可以轻松实现一个简单的多标签页功能。

但如果您还需要更强大的页面配置功能，如以下示例中使用 `ReuseTabsPageAttribute` 或 `IReuseTabsPage`，
则还需要额外修改项目中的 `Routes.razor` 文件，使用 `<CascadingValue Value="routeData">` 包裹 `RouteView` 或者 `AuthorizeRouteView`。

   ```razor
   <Router AppAssembly="@typeof(Program).Assembly">
       <Found Context="routeData">
           <CascadingValue Value="routeData">
               <RouteView RouteData="@routeData" DefaultLayout="@typeof(MainLayout)" / >
           </CascadingValue>
      </Found>
       ...
   </Router>
   ```

## API

### ReuseTabs

| 属性 | 描述 | 类型 | 默认值 | 
| --- | --- | --- | --- |
| Body | 用于设置 Layout 组件的 Body，无级联 RouteData 模式下必须设置。| RenderFragment | - |
| TabPaneClass | 可设置 Pane 容器的类名 | string | - |
| Draggable | 是否可拖拽调整顺序 | bool | false |
| Size | Tabs 组件大小 | TabSize | - |
| TabPaneTemplate | 渲染类容的模板，可以给 TabPane 中页面周围增加样式，传入上下文为 ReuseTabsPageItem，其中的 Body 即为页面内容 | `RenderFragment<ReuseTabsPageItem>` | context => context.Body |
| Locale | 本地化对象 | - | - |
| HidePages | 是否隐藏在 Tabs 中的页面，搭配 ReusePages 使用 | bool | false |
| ReuseTabsRouteData | 当前页的路由信息，RouteData 的可序列化版本，用于从静态页面传递到动态页面 | RouteData | - |

其他属性继承自 [Tabs](/components/tabs#API)

### ReuseTabsPageAttribute 特性

**必须级联传入 RouteData，否则无效。**

| 属性 | 描述 | 类型 | 默认值 | 
| --- | --- | --- | --- |
| Title | 设置在 tab 上的固定标题 | string | current path |
| Ignore | 如果 `Ignore=true`, 页面将不会显示在tab中，而是跟原来一样在整个页面展示。 | boolean | false |
| Closable | 是否显示关闭按钮并且不可被关闭。 | boolean | false |
| Pin | 是否在启动时就固定加载该页面，一般用于主页或默认页。 | boolean | false |
| PinUrl | 固定加载页面的 Url，在打开路由有参数的页面时需要，比如 `/order/1` | string | - |
| KeepAlive| 是否缓存页面状态 | bool | true |
| Order | 标签顺序 | int | 999 |
| TypeName | 当前页面对应的类别 | string | - |
| Key | 当前标签页的关键字 | string | - |
| Singleton | 开启页面单例模式，使相同的路由模版会复用同一个页面 | bool | false |

### IReuseTabsPage 接口

**必须级联传入 RouteData，否则无效。**

| 方法 | Description | 
| --- | --- | 
| RenderFragment GetPageTitle() | 设置动态标题，用于需要使用模板或者加载时才确定的标题 |


### ReuseTabsService

用于在页面中控制 ReuseTabs

| 属性/方法 | Description | 
| --- | --- | 
| Pages | 当前打开过的页面信息列表，可自行用于缓存和恢复 | 
| CreateTab(string pageUrl, RenderFragment? title = null) | 创建一个标签，但不导航到该页面，等导航到该页面时才初始化这个页面。|
| ClosePage(string url) | 关闭指定url的页面。 |
| CloseOther(string url) | 关闭除了指定url的页面，或者设置了 `Cloasable=false` 或 `Pin=true` 的页面。 |
| CloseAll() | 关闭除了设置了 `Cloasable=false` 或者 `Pin=true` 的页面。  |
| CloseCurrent() | 关闭当前页面。 |
| Update() | 更新 Tab 状态。当 `GetPageTitle()` 中引用的变量发生变化时，需要调用 `Update()` 来更新 tab 的显示。 |
| UpdatePage(string url, Action<ReuseTabsPageItem> optionsAction) | 更新指定页面的属性。 | 
| UpdatePage(Action<ReuseTabsPageItem> optionsAction) | 更当前定页面的属性。 | 
| ReloadPage(string url) | 重新加载指定标签的页面，让页面组件重新初始化，且无需刷新浏览器。不传url时重新加载当前页面。 | 