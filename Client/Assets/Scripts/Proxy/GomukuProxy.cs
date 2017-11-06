using System;
using System.Collections.Generic;
using message;

namespace RedStone
{
	public class GomukuProxy : ProxyBase
	{
		public Dictionary<int, ChessData> chesses { get { return m_chesses; } }
		public List<PlaceStatisticsData> placeStatistics { get { return m_placeStatistics; } }

		private Dictionary<int, ChessData> m_chesses = new Dictionary<int, ChessData>();
		private List<PlaceStatisticsData> m_placeStatistics = new List<PlaceStatisticsData>();

		private ECamp m_whosTurn = ECamp.None;
		public ECamp whosTursn { get { return m_whosTurn; } }

		public GomukuProxy()
		{
			netType = NetType.Battle;
		}

		public override void OnInit()
		{
			base.OnInit();

			network.Register<BoardSync>(OnBoardSync);
			network.Register<NewTurnBroadcast>(OnNewTurn);
			network.Register<BattleResult>(OnBattleResult);
			network.Register<PlaceStatisticsSync>(OnPlaceStatisticsSync);
		}

		public override void OnDestroy()
		{
			network.UnRegister<BoardSync>(OnBoardSync);
			network.UnRegister<NewTurnBroadcast>(OnNewTurn);
			network.UnRegister<BattleResult>(OnBattleResult);
			network.UnRegister<PlaceStatisticsSync>(OnPlaceStatisticsSync);

			base.OnDestroy();
		}

		public void JoinRoom(int roomId)
		{
			JoinRoomRequest msg = new JoinRoomRequest();
			msg.deviceUID = UUID.DEVICE;
			msg.roomId = roomId;

			network.SendMessage<JoinRoomRequest, JoinRoomReply>(msg,
			   (reply) =>
			{
				//TODO: reply.roomId 这个字段暂时没用啊？
				GetProxy<HallProxy>().mainPlayerData.UpdateCamp(reply.camp);
				OnNewTurn(reply.newTurn);
			});
		}

		public void ConnectToBattleServer(string address, int roomId)
		{
			NetworkManager.instance.Get(NetType.Battle).onConnected =
			(obj) =>
			{
				UIManager.instance.Show<GomukuView>();
				JoinRoom(roomId);
			};

			NetworkManager.instance.Connect(NetType.Battle, address);
		}

		public void OnBoardSync(message.BoardSync board)
		{
			for (int i = 0; i < board.rows.Count; i++)
			{
				for (int j = 0; j < board.rows[i].types.Count; j++)
				{
					int num = i * 12 + j;
					ChessData chess;
					if (!m_chesses.TryGetValue(num, out chess))
					{
						chess = new ChessData();
						m_chesses.Add(num, chess);
					}
					chess.SetData(num, i, j, board.rows[i].types[j]);
				}
			}
		}

		public void OnPlaceStatisticsSync(message.PlaceStatisticsSync msg)
		{
			m_placeStatistics.Clear();
			for (int i = 0; i < msg.statistics.Count; i++)
			{
				PlaceStatisticsData sta = new PlaceStatisticsData();
				sta.SetData(msg.statistics[i].num, msg.statistics[i].ratio);
				m_placeStatistics.Add(sta);
			}

			EventManager.instance.Send(Event.Gomuku.PlaceStatisticSync);
		}

		public void OnNewTurn(NewTurnBroadcast msg)
		{
			m_whosTurn = (ECamp)msg.camp;
			OnBoardSync(msg.boardSync);
			SendEvent(Event.Gomuku.NewTurn, m_whosTurn);
		}

		public void OnBattleResult(BattleResult msg)
		{
			SendEvent(Event.Gomuku.BattleResult, (ECamp)msg.camp);
		}

		public ChessData GetChess(int num)
		{
			ChessData data = null;
			m_chesses.TryGetValue(num, out data);
			return data;
		}

		public void PlaceChess(int num)
		{
			PlaceRequest msg = new PlaceRequest();
			msg.chessNum = num;
			network.SendMessage<PlaceRequest, PlaceReply>(msg,
			(reply) =>
			{
				//TODO: process
			});
		}

		public override void OnUpdate()
		{
			base.OnUpdate();
		}
	}
}
