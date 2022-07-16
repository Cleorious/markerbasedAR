using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class TitlePopup : MonoBehaviour
{
    public TextMeshProUGUI descText;
    public Button startButton;

    private GameManager gameManager;
    // Start is called before the first frame update
    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
        descText.gameObject.SetActive(true);
        RefreshDescText(ARSessionState.None);
        
        startButton.onClick.AddListener(gameManager.DoInitializeARSession);
    }

    public void RefreshDescText(ARSessionState arSessionState)
    {
        switch (arSessionState)
        {
            case ARSessionState.None:
                descText.SetText("ARSession not started..");
                break;
            case ARSessionState.CheckingAvailability:
                descText.SetText("Checking AR compatibility..");
                break;
            case ARSessionState.Unsupported:
                descText.SetText("Device unsupported!");
                break;
            case ARSessionState.Ready:
                descText.SetText("ARSession Ready!");
                break;
            default:
                descText.SetText("Waiting..");
                break;
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
