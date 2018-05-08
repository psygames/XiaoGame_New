using System;
using System.Collections.Generic;
using WebSocketSharp.Server;
using WebSocketSharp;

namespace NetworkLib
{
    public class WebSocketSession : SessionBase
    {
        private WebSocketBehaviorImp _behavior = null;
        public WebSocketBehavior behavior { get { return _behavior; } }
        private Action<string, byte[]> m_sendAction = null;
        public override string ID { get { return _behavior.ID; } }

        public override void OnConnected()
        {
            base.OnConnected();
        }

        public WebSocketSession(Action<string, byte[]> sendAction)
        {
            _behavior = new WebSocketBehaviorImp();
            m_sendAction = sendAction;
            _behavior.onOpen = base.OnConnected;
            _behavior.onMessage = base.OnReceived;
            _behavior.onClose = base.OnClosed;
        }

        public override void Send(byte[] data)
        {
            base.Send(data);
            if (m_sendAction != null)
                m_sendAction.Invoke(ID, data);
        }


        public class WebSocketBehaviorImp : WebSocketBehavior
        {
            public Action<byte[]> onMessage = null;
            public Action onOpen = null;
            public Action onClose = null;

            protected override void OnMessage(MessageEventArgs e)
            {
                base.OnMessage(e);
                if (onMessage != null)
                    onMessage.Invoke(e.RawData);
            }

            protected override void OnClose(CloseEventArgs e)
            {
                base.OnClose(e);
                if (onClose != null)
                    onClose.Invoke();
            }

            protected override void OnError(ErrorEventArgs e)
            {
                base.OnError(e);
            }

            protected override void OnOpen()
            {
                base.OnOpen();
                if (onOpen != null)
                    onOpen.Invoke();
            }
        }
    }
}
