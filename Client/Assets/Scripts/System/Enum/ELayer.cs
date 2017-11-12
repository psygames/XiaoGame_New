public enum ELayer
{
    Default = 0,
    TransparentFX = 1,
    IngoreRaycast = 2, //忽略所有的射线检测，大量用于
    Water = 4, //没用到
    UI = 5, //显示层
    MainPlayer = 8, //主角
    HitBox = 11, //碰撞盒

    //UI3D
    UI_NonEditable = 16, //美术编辑器工具，防止错误操作
    UI_3D = 17, //3D UI层
    //地雷，手雷，火箭筒都是用这个层
    ThrowObject = 18, //投掷物
    PlayerFriend = 19, //用于胶囊体碰撞，在根物体上设置，没用了
    PlayerEnimy = 20, //用于胶囊体碰撞，在根物体上设置，没用了

    //可以单手跨栏层
    CoverWall = 23, //翻越物体，做翻越动作的时候，可以穿过去
    //与玩家无碰撞的投掷物品层，仪器在使用改层
    Terrain = 25, //地面服务器用途，服务器用到了，用于检测地面
    //布娃娃层
    Ragdoll = 29,
}
public enum ECustomLayerMask
{
    Default = 1 << ELayer.Default,
    UI = 1 << ELayer.UI,
    MainPlayer = 1 << ELayer.MainPlayer,
    Player = 1 << ELayer.PlayerEnimy | 1 << ELayer.PlayerFriend | 1 << ELayer.HitBox,
    Terrain = 1 << ELayer.Terrain,
    Water = 1 << ELayer.Water,
    Environment = Default | Terrain | Water | 1 << ELayer.CoverWall,
    CanHit = HitBox | Environment | ThrowObject,
    HitBox = 1 << ELayer.HitBox,
    ThrowObject = 1 << ELayer.ThrowObject
}
public class CustomTags
{
    public const string campBlue = "Blue";
    public const string campRend = "Red";
    public const string mainPlayer = "MainPlayer";
    //public const string jumpInteractionTag = "jumpInteraction";
}