using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedStone.UI;
using RedStone.Data.SOS;

namespace RedStone
{
    public class SosRoom : MonoBehaviour
    {
        public Text whosTurnText;
        public Text stateText;
        public GameObject readyBtn;
        public GameObject playBtn;

        public SosMainPlayer mainPlayer;
        public SosPlayer leftPlayer;
        public SosPlayer rightPlayer;
        public SosPlayer oppositePlayer;
        public SosTurnClock clock;
        public SosAimTargetLine aimTargetLine;

        private List<SosPlayer> m_players = new List<SosPlayer>();
        private RoomData data { get { return GF.GetProxy<SosProxy>().room; } }
        private CardData selectedCard { get { return mainPlayer.selectedCard; } }
        private SosPlayer whosTurn { get { return m_players.First(a => a.data.isTurned); } }

        public void Init()
        {
            GF.Register(EventDef.SOS.Joined, OnJoined);
            GF.Register<int>(EventDef.SOS.Ready, OnReady);
            GF.Register(EventDef.SOS.RoomSync, OnRoomSync);
            GF.Register<Message.CBSendCardSync>(EventDef.SOS.SendCard, OnSendCard);
            GF.Register<Message.CBPlayCardSync>(EventDef.SOS.PlayCard, OnPlayCard);
        }

        public void Reset()
        {
            clock.gameObject.SetActive(false);
        }

        public void InitPlayers()
        {
            m_players.Clear();

            mainPlayer.gameObject.SetActive(true);
            m_players.Add(mainPlayer);

            if (data.players.Count == 2)
            {
                m_players.Add(oppositePlayer);
            }
            else if (data.players.Count == 3)
            {
                m_players.Add(rightPlayer);
                m_players.Add(leftPlayer);
            }
            else if (data.players.Count == 4)
            {
                m_players.Add(rightPlayer);
                m_players.Add(oppositePlayer);
                m_players.Add(leftPlayer);
            }

            leftPlayer.gameObject.SetActive(m_players.Contains(leftPlayer));
            rightPlayer.gameObject.SetActive(m_players.Contains(rightPlayer));
            oppositePlayer.gameObject.SetActive(m_players.Contains(oppositePlayer));
        }

        // On Event
        void OnJoined()
        {
            Reset();
            InitPlayers();

            // seats
            for (int i = 0; i < m_players.Count; i++)
            {
                int seat = i + data.mainPlayer.seat;
                seat = (seat - 1) % m_players.Count + 1;
                m_players[i].Init(data.players.First(a => a.seat == seat));
            }

            foreach (var p in m_players)
            {
                p.onClickCallback = OnPlayerClick;
            }
        }

        private PlayerData m_selectedPlayer = null;
        void OnPlayerClick(PlayerData playerData)
        {
            if (data.whosTurn != mainPlayer.data)
                return;

            m_selectedPlayer = playerData;
            RefreshPlayers();
        }

        void OnReady(int id)
        {
            GetPlayer(id).SetReady(true);
        }

        void OnRoomSync()
        {
            if (data.whosTurn != null
                && !data.whosTurn.isMain)
            {
                m_selectedPlayer = null;
            }

            RefreshUI();
            RefreshClock();
        }

        private PlayerData m_lastTurn = null;
        void RefreshClock()
        {
            if (data.state != RoomData.State.Started)
            {
                clock.gameObject.SetActive(false);
                return;
            }

            clock.gameObject.SetActive(true);
            if (data.whosTurn != null && data.whosTurn != m_lastTurn)
            {
                Vector2 pos = UIHelper.GetUIPosition(whosTurn.playedCardRoot) - UIConfig.virtualScreenCenter;
                clock.transform.localPosition = pos;
                clock.Reset();
                m_lastTurn = data.whosTurn;
            }
        }

        void OnSendCard(Message.CBSendCardSync msg)
        {
            GetPlayer(msg.TargetID).TakeCard(msg.CardID);
        }

        void OnPlayCard(Message.CBPlayCardSync msg)
        {
            var from = GetPlayer(msg.FromID);
            from.PlayCard(data.GetCard(msg.CardID));

            var target = GetPlayer(msg.TargetID);
            if (target != null)
                ShowAimTargetLine(from.playedCardRoot, target.headIcon.transform);
        }

        void ShowAimTargetLine(Transform from, Transform to)
        {
            aimTargetLine.Show(UIHelper.GetUIPosition(from), UIHelper.GetUIPosition(to));
        }


        // Refresh
        void RefreshUI()
        {
            if (data.whosTurn != null)
                this.whosTurnText.text = data.whosTurn.name;
            else
                this.whosTurnText.text = "空";


            if (data.state == RoomData.State.WaitReady)
            {
                readyBtn.SetActive(true);
            }
            else
            {
                readyBtn.SetActive(false);
            }

            stateText.text = data.state.ToString();

            RefreshPlayers();
        }

        private void Update()
        {
            if (data.state == RoomData.State.Started
                && data.whosTurn == mainPlayer.data
                && selectedCard != null)
            {
                playBtn.SetActive(true);
            }
            else
            {
                playBtn.SetActive(false);
            }
        }


        void RefreshPlayers()
        {
            foreach (var p in m_players)
            {
                if (m_selectedPlayer != null && m_selectedPlayer == p.data)
                {
                    p.BeSelected(true);
                }
                else
                {
                    p.BeSelected(false);
                }
            }
        }

        // Interface
        public SosPlayer GetPlayer(int playerID)
        {
            return m_players.First(a => a.data.id == playerID);
        }

        public void OnClickPlayCard()
        {
            if (selectedCard == null)
            {
                Toast.instance.Show("必须选择一张卡牌!");
                return;
            }

            if (selectedCard.type == CardType.ForOneTarget
                && m_selectedPlayer == null)
            {
                Toast.instance.Show("请选择一个玩家作为释放对象！");
                return;
            }

            GF.GetProxy<SosProxy>().PlayCard(selectedCard.id
                , m_selectedPlayer == null ? 0 : m_selectedPlayer.id);
        }
    }
}