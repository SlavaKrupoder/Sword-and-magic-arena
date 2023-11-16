using UnityEngine;
using SF = UnityEngine.SerializeField;
using MainLogic;

namespace UiLogic
{
    public class BoostPanel : MonoBehaviour
    {
        [SF] private int[] boostValues;

        private void Start()
        {
            boostValues = new int[2];
        }

        public void HpoinBoost(int hp)
        {
            boostValues[0] = hp;
            SendBoost(boostValues);
        }

        public void DamageBoost(int dm)
        {
            boostValues[1] = dm;
            SendBoost(boostValues);
        }

        private void SendBoost(int[] boostValues)
        {
            EventManager.EventManagerObjectLink.SendBoostEvent(boostValues);
        }
    }
}
