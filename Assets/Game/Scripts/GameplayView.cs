using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayView : MonoBehaviour
{
    public Button fireButton;

    private GameManager gameManager;
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
        //TODO: need to check if gameplaymanager is instantiated and ready?
        gameManager.gameplayManager.ShootProjectile();
    }
}
