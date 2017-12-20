using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedStone.Data;
using Message;


namespace RedStone
{
    public class SOS_Logic
    {
        BattleServerProxy battleProxy { get { return ProxyManager.instance.GetProxy<BattleServerProxy>(); } }
        RoomProxy roomProxy { get { return ProxyManager.instance.GetProxy<RoomProxy>(); } }
        UserProxy userProxy { get { return ProxyManager.instance.GetProxy<UserProxy>(); } }
        private int m_roomID;
        RoomData room { get { return roomProxy.GetRoom(m_roomID); } }

        private State m_state = State.WaitJoin;
        private List<Player> m_players = new List<Player>();

        public void Init(int roomID)
        {
            m_roomID = roomID;
            InitPlayers();

            // Register Msg
            RegisterMsg<CBJoinBattleRequest>(OnJoinBattle);
            RegisterMsg<CBReady>(OnReady);
        }

        void InitPlayers()
        {
            m_players.Clear();
            int incrID = 1;
            foreach (var uid in room.users)
            {
                var user = userProxy.GetUser(uid);
                var player = new Player();
                player.Init(user, incrID);
                m_players.Add(player);
                incrID++;
            }
        }


        public void Update()
        {
            CheckAllJoined();
            CheckAllReady();
        }

        public void CheckAllJoined()
        {
            if (m_state != State.WaitJoin)
                return;

            if (m_players.All(a => a.state == Player.State.Joined))
                m_state = State.WaitReady;
        }

        public void CheckAllReady()
        {
            if (m_state != State.WaitReady)
                return;

            if (m_players.All(a => a.state == Player.State.Ready))
                m_state = State.Started;
        }


        void OnJoinBattle(Player player, CBJoinBattleRequest msg)
        {
            if (m_state != State.WaitJoin)
                return;

            player.user.SetState(UserState.Battle);
            player.SetState(Player.State.Joined);

            var rep = new CBJoinBattleReply();
            rep.Info = new BattleRoomInfo();
            rep.Info.Id = m_roomID;
            rep.Info.Name = room.name;

            foreach (var d in m_players)
            {
                BattlePlayerInfo info = new BattlePlayerInfo();
                info.Id = d.id;
                info.IsSelf = d == player;
                info.Level = d.user.level;
                info.Name = d.user.name;
                info.Gold = d.gold;
                info.Seat = d.seat;
                info.Joined = d.state == Player.State.Joined;
                rep.PlayerInfos.Add(info);
            }

            SendToAll(rep);
        }

        public void SendToAll<T>(T msg)
        {
            foreach (var p in m_players)
            {
                battleProxy.SendMessage(p.user.sessionID, msg);
            }
        }

        public void SendTo<T>(int playerId, T msg)
        {
            var p = m_players.First(a => a.id == playerId);
            battleProxy.SendMessage(p.user.sessionID, msg);
        }


        void OnReady(Player player, CBReady msg)
        {
            if (m_state != State.WaitReady)
                return;

            player.SetState(Player.State.Ready);
        }




        private void RegisterMsg<T>(Action<Player, T> action)
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
            WaitJoin,
            WaitReady,
            Started,
            End,
        }


        public class Player
        {
            public long uid { get; private set; }
            public int id { get; private set; } //临时ID
            public int gold { get; private set; }
            public int seat { get; private set; }
            public State state { get; private set; }
            public UserData user { get { return ProxyManager.instance.GetProxy<UserProxy>().GetUser(uid); } }

            public void Init(UserData user, int id)
            {
                this.uid = user.uid;
                this.id = id;
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
                Joined = 1,
                Ready = 2,
                Turn = 3,
                Wait = 4,
                Out = 5,
            }
        }

    }
}
