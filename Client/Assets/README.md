# 目录结构  


### 编辑器工具
*  /Editor/Config/AtlasTextureFormat.json  
Atlas 图集打包配置文件  

*  /Editor/Scripts/  
图集打包工具，~~Proto生成工具（弃用）~~  

*  /Editor/UI/  
所有UI控件编辑器下的内容展示的重写6


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
EventDef 事件声明，发送和监听的时间名在这里写。
