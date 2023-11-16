using UnityEngine;

namespace MainLogic
{
    public class EventManager : MonoBehaviour
    {
        public static EventManager EventManagerObjectLink;

        public delegate void BoostEvent(int[] boostValues);
        public static event BoostEvent OnBoostEvent;
        public delegate void EndGameEvent(string winnerTeam);
        public static event EndGameEvent OnEndGameEvent;

        private void Awake()
        {
            EventManagerObjectLink = this;
        }

        public void SendBoostEvent(int[] boostValues)
        {
            OnBoostEvent?.Invoke(boostValues);
        }

        public void SendEndGameEvent(string winnerTeam)
        {
            OnEndGameEvent?.Invoke(winnerTeam);
        }
    }
}
