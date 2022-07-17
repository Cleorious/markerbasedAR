using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayView : MonoBehaviour
{
    [SerializeField] Button fireButton;

    [SerializeField] TextMeshProUGUI trackedText;

    GameManager gameManager;
    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
        
        fireButton.onClick.AddListener(OnFireButtonClicked);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    void OnFireButtonClicked()
    {
        gameManager.gameplayManager.ShootProjectile();
    }

    public void UpdateTrackedText(string trackedName)
    {
        trackedText.SetText("Tracked: " + trackedName);
    }
}
