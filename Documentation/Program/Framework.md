# 目录结构  


### 编辑器工具
*  /Editor/Config/AtlasTextureFormat.json  
Atlas 图集打包配置文件  

*  /Editor/Scripts/  
图集打包工具，~~Proto生成工具（弃用）~~  

*  /Editor/UI/  
所有UI控件编辑器下的内容展示的重写


### 资源文件  
*  /Resources/xml  
表格配置文件,生成后的简化文件，用于配置游戏不同模块的一些数值或资源路径等。

*  /Resources/Languages  
多语言文件，同样也是xml生成后的简化文件

*  /ResourcesUI
图集打包的原始图片文件，按照界面or功能模块进行划分  
ps:不要放在Resources目录下（不然会被打包到安装包中）



### 插件  
*  /Plugins/YLWebSocket  
对浏览器WebSocket的支持  

*  /Plugins/ugui_tween_tool  
uTween 插件

## 脚本
*  /Scripts/System/Core  
Singleton单例基类  
LT是多语言简化类  
ExtMethod是扩展方法  

*  /Scripts/System/Protocol
全部proto生成后的协议文件，通过 项目根路径/Tools/protogen 生成工具生成的。对应proto文件在/Tools/protogen/proto中

* /Scripts/System/Tools  
一些工具类，看名字大概知道其作用。  
特殊说明下：  
GameObjectHelper中SetListContent经常用于创建多个相同prefab。（参考脚本 /Scripts/Module/UI/Battle/SOS/SosGuessPanel.cs）  
UIHelper 包含N多UI的辅助功能，详见类方法。  

### Manager  
* GameFacade  
外观模式，对Manager的方法重新封装。方便Proxy的管理等。  

* GameManager  
游戏主入口，包括一些游戏内容预加载的等逻辑，状态机的控制。  

* NetworkManager  
管理网络连接，这里面有一个ClientNetworkMangerEx是对原有的ClientNetworkManager进行了消息处理队列的扩展，为了使接收到的异步网络消息放在游戏的主线程中处理。  

* ResourceManager  
资源加载，现在全部是用Resources.Load 加载的，后续会加入AssetBundle的方式。  

* TableManager
表格管理，这块是游戏比较重要的一块，项目的配置文件的管理（游戏的数值配置，资源路径配置，内容配置等等）。  
/项目根路径/Documentation/XML中的配置文件通过生成工具生成新的xml文件和对应的.cs类代码供程序使用。XML目录下的文件需要使用Excel打开。xml 生成工具在 /项目根路径/Tools/ConfigTool中，xmlList.txt 是需要生成的xml文件的配置，xmltools/config.ini 是生成路径等配置。  

* UIManager  
UI界面打开关闭的管理，包括UI栈和预加载界面管理。  

* EventManager （现在在Plugins中）  
事件管理器，一般用法是，Proxy发送事件，View或其他模块监听事件，事件加入了泛型机制，方便使用。

## /Scripts/Module  
*  这块名字起得有些问题，应该是一个游戏内容的目录，包括游戏数据/Data ，游戏内容代理 /Proxy ,游戏界面/UI (可以参考PureMVC结构，这个是一个缩减版，把View和Mediator合并了，去掉Command和Controller) 。
* Data是游戏的数据层。  
* Proxy管理数据，与服务器进行数据交互，更新数据，发送事件，对外提供数据访问接口。  
* UI是显示层，通过Proxy访问游戏数据，并展示在View上。同时需要监听一些Proxy发出的事件，并做不同的表现。View脚本直接挂在prefab上，VievBase继承自EventHandleItem来监听按钮的事件，并加入了一些Proxy和Event的方法，方便使用。
GameState是游戏的状态，放在该目录下有些不合理。~~（待整理）~~
