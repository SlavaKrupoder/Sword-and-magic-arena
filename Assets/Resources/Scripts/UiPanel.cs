using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiPanel : MonoBehaviour
{
    [SerializeField] private GameObject popup;
    [SerializeField] private TextMeshProUGUI popupText;
    public void FinishedGame()
    {
        Application.Quit();
    }

    public void ShowPopup(string winnerTeam)
    {
        popup.SetActive(true);
        popupText.text = winnerTeam;

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
