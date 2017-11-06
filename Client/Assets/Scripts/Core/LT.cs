using System;
using System.Collections.Generic;
using System.Text;
using Hotfire.UI;
using Coolfish.System;

namespace Hotfire
{
    public class LT
    {
		public interface IVariableGetter
		{
			bool GetVariable (string key, out string variable);
		}
        public static string Get(string text, params object[] data)
        {
            s_dataObjects = data;
            return Parse(text);
        }

        public static string GetText(string text, params object[] placeholderStr)
        {
            return Localization.instance.GetText(text, placeholderStr);
        }

        public static void SetTextStyle(Text text)
        {
            Localization.instance.SetTextStyle(text);
        }

        public static void SetTextColor(Text text)
        {
            Localization.instance.SetTextColor(text);
        }

        class Node
        {
			public static ObjectPool<Node> nodePool = new ObjectPool<Node>(null, (n)=>n.Clear());
            protected List<Node> _list = new List<Node>();
			public Node()
			{
			}
			protected virtual void Clear()
			{
				for (int i = 0; i < _list.Count; ++i)
				{
					ReleaseNode (_list[i]);
				}
				_list.Clear ();
			}

			public static void ReleaseNode(Node node)
			{
				if (node is TokenNode)
				{
					TokenNode.tokenNodePool.Release (node as TokenNode);
				}
				else if (node is TextNode)
				{
					TextNode.textNodePool.Release (node as TextNode);
				}
				else if (node is VariableNode)
				{
					VariableNode.variableNodePool.Release (node as VariableNode);
				}
				else
				{
					Node.nodePool.Release (node);
				}
			}
            public void Add(Node node)
            {
                _list.Add(node);
            }
            public void AddToken(string text)
            {
				var tokenNode = TokenNode.tokenNodePool.Get ();
				tokenNode.value = text;
				_list.Add(tokenNode);
            }
            public override string ToString()
			{
				var nodeText = StringTools.GetStringBuilderFromPool ();
				for(int i = 0; i < _list.Count; ++i)
                {
					var node = _list [i];
                    nodeText.Append(node.ToString());
                }
				var nodeStr = nodeText.ToString ();
				StringTools.ReleaseStringBuilder (nodeText);
				Clear ();
				return nodeStr;
            }
        }
        class TokenNode : Node
		{
			public static ObjectPool<TokenNode> tokenNodePool = new ObjectPool<TokenNode>(null, (n)=>n.Clear());
            public string value;
			public TokenNode()
			{
			}
			protected override void Clear ()
			{
				base.Clear ();
				value = null;
			}
            public TokenNode(string text)
            {
                value = text;
            }
            public override string ToString()
            {
				var str = value;
				Clear ();
				return str;
            }
        }
        class VariableNode : Node
		{
			public static ObjectPool<VariableNode> variableNodePool = new ObjectPool<VariableNode>(null, (n)=>n.Clear());
            public override string ToString()
            {
				var nodeText = StringTools.GetStringBuilderFromPool ();
                foreach (Node node in _list)
                {
                    nodeText.Append(node.ToString());
                }
				Clear ();
				string nodeStr = nodeText.ToString ().Trim ();
				var index = nodeStr.IndexOf (':');
				StringTools.ReleaseStringBuilder (nodeText);
				if (index == 1)
				{
					if(nodeStr [0] == 'i')
						return ReplaceImage (nodeStr);
					else if(nodeStr [0] == 'c')
						return ReplaceColor (nodeStr.Substring(index + 1));
					else if(nodeStr[0] == 's')
						return ReplaceText (nodeStr.Substring(index + 1));
				}
				bool isDigit = false;
				for (int i = 0; i < index; ++i)
				{
					if (char.IsDigit (nodeStr [i]))
						isDigit = true;
					else
					{
						isDigit = false;
						break;
					}
				}
				if (isDigit)
					nodeStr = nodeStr.Substring (index + 1);
				return ReplaceVariable(nodeStr, s_dataObjects);
            }
			private string ReplaceColor(string nodeStr)
			{
				return ColorConfig.GetColorCode (nodeStr);
			}
			private string ReplaceImage(string nodeStr)
			{
				var nodeText = StringTools.GetStringBuilderFromPool ();
				nodeText.Length = 0;
				nodeText.AppendFormat ("<quad name={0} />", nodeStr.Substring (2).Trim());
				nodeStr = nodeText.ToString ();
				StringTools.ReleaseStringBuilder (nodeText);
				return nodeStr;
			}
        }
        class TextNode : Node
		{
			public static ObjectPool<TextNode> textNodePool = new ObjectPool<TextNode>(null, (n)=>n.Clear());
            public override string ToString()
			{
				var nodeText = StringTools.GetStringBuilderFromPool ();
                foreach (Node node in _list)
                {
                    nodeText.Append(node.ToString());
                }
				var nodeStr = nodeText.ToString ();
				StringTools.ReleaseStringBuilder (nodeText);
				Clear ();
				return ReplaceText(nodeStr);
            }
        }

        private static object[] s_dataObjects;
        /// <summary>
        /// 解析多国语言，关键词：TextKey，TextValue，Variable
        /// 
        /// TextKey用于查找多国语言表，格式：#TEXT_KEY，只允许：下划线、字母、数字
        /// TextValue多国语言表中的字符串值
        /// Variable用于自动替换变量，格式：{variable}
        /// 复杂Variable访问格式例子：{path.list[0].field}, 忽略大括号之间的空格
        /// 
        /// TextKey，TextValue，Variable的嵌套关系：
        /// 
        /// TextKey可以由Variable组成
        ///     格式#TEXT_ITEM_{variable}，variable=1，最终取TextKey为TEXT_ITEM_1的TextValue
        ///     
        /// TextValue可以包含TextKey
        ///     例如TextValue为"first #TEXT_KEY last"，再次替换其中的TextKey最终为"first TextValue last"
        ///     
        /// TextValue可以包含Variable
        ///     例如TextValue为"first {variable} last", variable=1，最终替换为"first 1 last"
        /// 
        /// Variable可以包含Variable
        ///     例如{path.list[{path.index}]}
        ///     
        /// </summary>
        /// <param name="aJSON"></param>
        /// <param name="dataObjects"></param>
        /// <returns></returns>
		static ObjectPool<Stack<Node>> nodeStackPool = new ObjectPool<Stack<Node>>();
        public static string Parse(string aJSON)
        {
			Stack<Node> stack = nodeStackPool.Get ();
			Node ctx = Node.nodePool.Get ();
            stack.Push(ctx);
            int i = 0;
			StringBuilder tokenBuilder = StringTools.GetStringBuilderFromPool();
            while (i < aJSON.Length)
            {
                switch (aJSON[i])
                {
                    case '{':
                        {
							stack.Push(VariableNode.variableNodePool.Get());
                            if (ctx != null)
                            {
								if (tokenBuilder.Length != 0)
                                {
									ctx.AddToken(tokenBuilder.ToString());
                                }
                                ctx.Add(stack.Peek());
                            }
							tokenBuilder.Length = 0;
                            ctx = stack.Peek();
                            break;
                        }
					case '}':
						if (stack.Count == 0)
							throw new Exception ("Parse: Too many closing brackets");

						var node = stack.Pop ();
						if (tokenBuilder.Length != 0)
						{
							ctx.AddToken (tokenBuilder.ToString ());
						}
						tokenBuilder.Length = 0;
                        ctx = stack.Peek();
						break;
                    
				default:
						if (ctx is TextNode && tokenBuilder.Length != 0)
						{
							bool isLetter = aJSON [i] == '_' || char.IsLetterOrDigit (aJSON [i]);
							if (!isLetter)
							{
								if (stack.Count == 0)
									throw new Exception ("Parse: Too many closing brackets");

								var nodePop = stack.Pop ();

								if (tokenBuilder.Length != 0)
								{
									ctx.AddToken (tokenBuilder.ToString ());
								}


								tokenBuilder.Length = 0;
								ctx = stack.Peek ();
							}
						}
						tokenBuilder.Append (aJSON [i]);
                        break;
                }
                ++i;
            }

			if (ctx != null && tokenBuilder.Length != 0)
				ctx.AddToken(tokenBuilder.ToString());
			StringTools.ReleaseStringBuilder (tokenBuilder);
            while (stack.Count > 1)
            {
                ctx = stack.Pop();
			}
			ctx = stack.Pop ();
			var str = ctx.ToString ();
			Node.ReleaseNode (ctx);
			nodeStackPool.Release (stack);
			return str;
        }
        private static string ReplaceText(string key)
        {
			object[] variables = null;
			if (key.Contains (","))
			{
				var variableList = ListPool<object>.Get ();
				var param = key.Split (',');
				for (int i = 1; i < param.Length; ++i)
				{
					variableList.Add(param[i]);
				}
				key = param [0];
				variables = variableList.ToArray ();
				variableList.ReleaseToPool ();
			}
			return Parse(GetText(key, variables));
        }

        private static string ReplaceVariable(string path, object[] data)
        {
            foreach (object obj in data)
            {
                object value;
				if (obj is IVariableGetter) 
				{
					string str = null;
					var getter = obj as IVariableGetter;
					if (getter.GetVariable (path, out str)) 
					{
						return Parse (str);
					}
				}
                else if (ObjectHelper.TryGetFieldPathValue(path, obj, out value))
                {
                    if (value is string)
						return Parse(value as string);
                    else
                        return value == null ? "" : value.ToString();
                }
            }
            return '{' + path + '}';
        }
    }
}
