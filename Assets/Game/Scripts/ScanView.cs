using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanView : MonoBehaviour
{
    GameManager gameManager;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
