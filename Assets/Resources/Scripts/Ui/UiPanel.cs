using TMPro;
using UnityEngine;
using SF = UnityEngine.SerializeField;
using MainLogic;

namespace UiLogic
{
    public class UiPanel : MonoBehaviour
    {
        [SF] private GameObject popUp;
        [SF] private TextMeshProUGUI popUpText;

        public void FinishedGame()
        {
            Application.Quit();
        }

        public void ShowPopup(string winnerTeam)
        {
            popUp.SetActive(true);
            popUpText.text = winnerTeam;

        }

        private void OnEnable()
        {
            EventManager.OnEndGameEvent += ShowPopup;
        }

        private void OnDisable()
        {
            EventManager.OnEndGameEvent -= ShowPopup;
        }
    }
}
