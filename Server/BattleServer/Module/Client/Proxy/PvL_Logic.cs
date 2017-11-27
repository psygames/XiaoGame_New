using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedStone.Data;
using Message;


namespace RedStone
{
    public class PvL_Logic
    {
        BattleServerProxy battleProxy { get { return ProxyManager.instance.GetProxy<BattleServerProxy>(); } }
        RoomProxy roomProxy { get { return ProxyManager.instance.GetProxy<RoomProxy>(); } }
        UserProxy userProxy { get { return ProxyManager.instance.GetProxy<UserProxy>(); } }
        private int m_roomID;
        RoomData room { get { return roomProxy.GetRoom(m_roomID); } }

        private State m_state = State.Join;
        private List<PData> m_players = new List<PData>();

        public void Init(int roomID)
        {
            m_roomID = roomID;
            InitPlayers();

            // Register Msg
            RegisterMsg<CBJoinBattleRequest>(OnJoinBattle);
        }

        void InitPlayers()
        {
            m_players.Clear();
            foreach (var uid in room.users)
            {
                var user = userProxy.GetUser(uid);
                var player = new PData();
                player.Init(user);
                m_players.Add(player);
            }
        }


        public void Update()
        {

        }




        void OnJoinBattle(PData player, CBJoinBattleRequest msg)
        {
            if (m_state != State.Join)
                return;

            player.user.SetState(UserState.Battle);
            player.SetState(PData.State.Join);
        }


        void CheckAllJoin()
        {
            bool isAllJoined = m_players.All(a => a.state == PData.State.Join);
            // TODO: Check All Joined & Send Ready Enable
        }




        private void RegisterMsg<T>(Action<PData, T> action)
        {
            foreach (var uid in room.users)
            {
                battleProxy.RegisterUserMsg<T>(uid, (msg) =>
                {
                    action.Invoke(m_players.First(a => a.uid == uid), msg);
                });
            }
        }

        public enum State
        {
            Join,
            Ready,
            Started,
            End,
        }

    }

    public class PData
    {
        public long uid { get; private set; }
        public int gold { get; private set; }
        public State state { get; private set; }
        public UserData user { get { return ProxyManager.instance.GetProxy<UserProxy>().GetUser(uid); } }

        public void Init(UserData user)
        {
            this.uid = user.uid;
            gold = user.gold;
            state = State.None;
        }

        public void IncrGold(int count)
        {
            gold += count;
        }

        public void SetState(State state)
        {
            this.state = state;
        }

        public enum State
        {
            None = 0,
            Join = 1,
            Ready = 2,
            Turn = 3,
            Wait = 4,
            Out = 5,
        }
    }

}
