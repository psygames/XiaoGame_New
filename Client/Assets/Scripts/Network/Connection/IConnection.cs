using UnityEngine;
using System.Collections;
using WebSocketSharp;
using System;

namespace RedStone
{
	public interface IConnection
	{
		void Init(string addr, Action<byte[]> onMessage, Action onOpen, Action onClose, Action<string> onError);

		bool isInit();
		bool isConnected();

		bool isConnecting();
		bool isClosing();
		bool isSending();

		void Connect();
		void Close();
		void Send(byte[] content);
	}
}