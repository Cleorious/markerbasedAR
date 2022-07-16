using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class GameManager : MonoBehaviour
{
    [SerializeField] ARSession arSession;

    [SerializeField] GameplayManager gameplayPrefab;

    [HideInInspector] public GameplayManager gameplayManager;
    
    //UI
    public TitlePopup titlePopup;
    public ScanView scanView;
    public GameplayView gameplayView;

    private bool initializingAR;
    
    // Start is called before the first frame update
    void Start()
    {
        titlePopup.Init(this);
        scanView.Init(this);
        
        titlePopup.Show();
    }

    public void DoInitializeARSession()
    {
        if (!initializingAR && ARSession.state != ARSessionState.Ready) 
        {
            StartCoroutine(InitializeARSession());
        }
    }
    
    IEnumerator InitializeARSession() {
        if (ARSession.state == ARSessionState.None ||
            ARSession.state == ARSessionState.CheckingAvailability)
        {
            yield return ARSession.CheckAvailability();
        }

        if (ARSession.state == ARSessionState.Unsupported)
        {
            // Start some fallback experience for unsupported devices
            titlePopup.RefreshDescText(ARSessionState.Unsupported);
        }
        else
        {
            // Start the AR session
            arSession.enabled = true;

            while (ARSession.state != ARSessionState.Ready)
            {
                titlePopup.RefreshDescText(ARSession.state);
                yield return new WaitForEndOfFrame();
            }

            titlePopup.RefreshDescText(ARSessionState.Ready);
            yield return new WaitForSeconds(2f);
            titlePopup.Hide();
            scanView.Show();
            
        }
    }

    public void StartGame()
    {
        titlePopup.Hide();
        scanView.Hide();
        //TODO: instantiate gameplayprefab/initializegameplayprefab
        //gameplayManager.Init(this);
        gameplayView.Show();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: check if image has been scanned,
        //once image has been scanned, turn off scanview and change to gameplayview
    }
}
