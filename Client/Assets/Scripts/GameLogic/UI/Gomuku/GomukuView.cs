using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace RedStone
{
	public class GomukuView : ViewBase
	{
		public CellGenerator generator;
		public GomukuChess chessTemplate;
		public Transform chessRoot;
		public PlaceStatistics statisticsTemplate;
		public Transform statisticsRoot;
		public PlaceStatistics selfPlace;
		public GomukuChess selfTake;
		public Text nameText;
		public Text whoseTurnText
;
		public GomukuBattleResult result;


		private List<GomukuChess> m_chesses = new List<GomukuChess>();
		private List<PlaceStatistics> m_statisticsChesses = new List<PlaceStatistics>();

		private int m_curPlaceNum = -1;
		private bool m_isPlaced = false;

		private PlayerData playerData { get { return GetProxy<HallProxy>().mainPlayerData; } }
		private bool isOurTurn { get { return proxy.whosTursn == playerData.camp && playerData.camp != ECamp.None; } }
		private bool canPlace { get { return !m_isPlaced && isOurTurn; } }
		private bool isShowSelfPlace { get { return m_curPlaceNum >= 0 && m_isPlaced && isOurTurn; } }

		private GomukuProxy proxy { get { return GetProxy<GomukuProxy>(); } }

		public override void OnInit()
		{
			base.OnInit();

			isBottom = true;

			generator.Init();
			generator.onClickCallback = OnCellClick;

			result.onClickCallback = OnCloseClick;
			result.gameObject.SetActive(false);

			Register(Event.Gomuku.PlaceStatisticSync, OnPlaceStatisticSync);
			Register<ECamp>(Event.Gomuku.NewTurn, OnNewTurn);
			Register<ECamp>(Event.Gomuku.BattleResult, OnBattleResult);
		}

		public override void OnDestory()
		{
			UnRegister(Event.Gomuku.PlaceStatisticSync, OnPlaceStatisticSync);
			UnRegister<ECamp>(Event.Gomuku.NewTurn, OnNewTurn);
			UnRegister<ECamp>(Event.Gomuku.BattleResult, OnBattleResult);

			base.OnDestory();
		}

		private void OnCellClick(int num)
		{
			ChessData chess = proxy.GetChess(num);
			if (!canPlace || chess == null || proxy.GetChess(num).type != message.Enums.ChessType.None)
			{
				Debug.Log("can not place!");
				return;
			}

			m_isPlaced = true;
			m_curPlaceNum = num;
			proxy.PlaceChess(num);
		}

		private void OnCloseClick()
		{
			proxy.network.Close();
			result.gameObject.SetActive(false);
			UIManager.instance.Show<LoginView>();
		}

		private void OnNewTurn(ECamp camp)
		{
			m_isPlaced = false;

			GameObjectHelper.SetListContent(chessTemplate, chessRoot, m_chesses, proxy.chesses.Values,
			(index, item, data) =>
			{
				item.SetData(data);
			});
		}

		private void OnPlaceStatisticSync()
		{
			GameObjectHelper.SetListContent(statisticsTemplate, statisticsRoot, m_statisticsChesses, proxy.placeStatistics,
			(index, item, data) =>
			{
				item.SetData(data);
			});
		}

		private void OnBattleResult(ECamp camp)
		{
			result.gameObject.SetActive(true);
			result.SetData(camp);
		}

		private void Update()
		{
			UpdateCurPlace();
			UpdateSelfTake();
			UpdateWhoseTurn();
			UpdatePlaceStatistic();
		}

		private void UpdateCurPlace()
		{
			selfPlace.gameObject.SetActive(isShowSelfPlace);
			selfPlace.SetNum(m_curPlaceNum);
		}

		private void UpdateSelfTake()
		{
			selfTake.SetType((message.Enums.ChessType)playerData.camp);
			nameText.text = playerData.name;
		}

		private void UpdateWhoseTurn()
		{
			if (isOurTurn)
			{
				whoseTurnText.color = Color.green;
				whoseTurnText.text = "我方下棋";
			}
			else if (proxy.whosTursn == playerData.enemyCamp)
			{
				whoseTurnText.color = Color.white;
				whoseTurnText.text = "敌方下棋";
			}
			else
			{
				whoseTurnText.color = Color.gray;
				whoseTurnText.text = "回合未开始";
			}
		}

		private void UpdatePlaceStatistic()
		{
			statisticsRoot.gameObject.SetActive(isOurTurn);
		}
	}
}
