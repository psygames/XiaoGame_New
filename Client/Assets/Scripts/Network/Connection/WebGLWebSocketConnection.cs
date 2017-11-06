using UnityEngine;
using System.Collections;
using System;
using YLWebSocket;

namespace RedStone
{
	public class WebGLWebSocketConnection : IConnection
	{
		private WebSocket m_socket;

		public void Init(string addr, Action<byte[]> onMessage, Action onOpen, Action onClose, Action<string> onError)
		{
			m_socket = WebSocketManager.instance.GetSocket(addr);
			m_socket.onReceived += (data) =>
			{
				onMessage.Invoke(data);
			};
			m_socket.onConnected += () =>
			{
				onOpen.Invoke();
			};
			m_socket.onClosed += () =>
			{
				onClose.Invoke();
			};
		}


		public bool isInit() { return m_socket != null; }
		public bool isConnected() { return m_socket.state == WebSocket.State.Connected; }

		public bool isConnecting() { return m_socket.state == WebSocket.State.Connecting; }
		public bool isClosing() { return m_socket.state == WebSocket.State.Closing; }
		public bool isSending() { return false; }

		public void Connect()
		{
			if (isInit() && !isConnected())
			{
				m_socket.Connect();
			}
		}

		public void Close()
		{
			if (isInit() && isConnecting())
			{
				m_socket.Close();
			}
		}

		public void Send(byte[] content)
		{
			if (isInit() && isConnected())
			{
				m_socket.Send(content);
			}
		}
	}
}