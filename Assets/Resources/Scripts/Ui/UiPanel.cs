using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiPanel : MonoBehaviour
{
    [SerializeField] private GameObject popUp;
    [SerializeField] private TextMeshProUGUI popUpText;

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
