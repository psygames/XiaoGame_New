using UnityEngine;
using System.Collections;
using WebSocketSharp;
using System;

namespace RedStone
{
	public class WebSocketConnection : IConnection
	{
		private WebSocket m_socket;

		public void Init(string addr, Action<byte[]> onMessage, Action onOpen, Action onClose, Action<string> onError)
		{
			m_socket = new WebSocket(addr);
			m_socket.OnMessage += (sender, e) =>
			{
				onMessage.Invoke(e.RawData);
			};
			m_socket.OnOpen += (sender, e) =>
			{
				onOpen.Invoke();
			};
			m_socket.OnError += (sender, e) =>
			{
				onError.Invoke(e.Message);
			};
			m_socket.OnClose += (sender, e) =>
			{
				onClose.Invoke();
			};
		}


		public bool isInit() { return m_socket != null; }
		public bool isConnected() { return m_socket.IsConnected; }

		public bool isConnecting() { return m_socket.ReadyState == WebSocketState.Connecting; }
		public bool isClosing() { return m_socket.ReadyState == WebSocketState.Closing; }
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
			if (isInit() && isConnected())
			{
				m_socket.Close();
			}
		}

		public void Send(byte[] content)
		{
			if (isInit() && isConnected())
			{
				m_socket.SendAsync(content, (finish) => { });
			}
		}

	}
}