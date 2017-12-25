using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedStone.UI;
using RedStone.Data.SOS;

namespace RedStone
{
    public class SosRoom : MonoBehaviour
    {
        public Text whosTurn;
        public Text state;
        public GameObject readyBtn;

        public SosMainPlayer mainPlayer;
        public SosPlayer leftPlayer;
        public SosPlayer rightPlayer;
        public SosPlayer oppositePlayer;

        private List<SosPlayer> m_players = new List<SosPlayer>();
        private RoomData data { get { return GF.GetProxy<SosProxy>().room; } }

        public void Init()
        {
            GF.Register(EventDef.SOS.Joined, OnJoined);
            GF.Register<int>(EventDef.SOS.Ready, OnReady);
            GF.Register(EventDef.SOS.RoomSync, OnRoomSync);
            GF.Register<Message.CBSendCardSync>(EventDef.SOS.SendCard, OnSendCard);

            InitPlayers();
        }

        void InitPlayers()
        {
            m_players.Add(mainPlayer);
            m_players.Add(oppositePlayer);
            m_players.Add(leftPlayer);
            m_players.Add(rightPlayer);
        }

        // On Event
        void OnJoined()
        {
            mainPlayer.Init(data.mainPlayer);
        }

        void OnReady(int id)
        {
            GetPlayer(id).SetReady(true);
        }

        void OnRoomSync()
        {
            RefreshUI();
        }

        void OnSendCard(Message.CBSendCardSync msg)
        {
            GetPlayer(msg.TargetID).TakeCard(msg.CardID);
        }


        // Refresh
        void RefreshUI()
        {
            if (data.whosTurn != null)
                this.whosTurn.text = data.whosTurn.name;
            else
                this.whosTurn.text = "空";


            if (data.state == RoomData.State.WaitReady)
            {
                readyBtn.SetActive(true);
            }
            else
            {
                readyBtn.SetActive(false);
            }

            state.text = data.state.ToString();
        }


        // Do Something
        public void PlayCard(int cardID)
        {

        }

        // Interface
        public SosPlayer GetPlayer(int playerID)
        {
            return m_players.First(a => a.data.id == playerID);
        }
    }
}