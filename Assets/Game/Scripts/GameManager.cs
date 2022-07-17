using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class GameManager : MonoBehaviour
{
    [SerializeField] ARSession arSession;
    [SerializeField] ARTrackedImageManager arTrackedImageManager;

    [SerializeField] GameplayManager gameplayPrefab;

    [HideInInspector] public GameplayManager gameplayManager;
    
    //UI
    public TitlePopup titlePopup;
    public ScanView scanView;
    public GameplayView gameplayView;
    public WinView WinView;

    bool initializingAR;

    bool listenersAttached;
    bool tracked;
    
    // Start is called before the first frame update
    void Start()
    {
        titlePopup.Init(this);
        scanView.Init(this);
        gameplayView.Init(this);
        WinView.Init(this);
        gameplayManager = Instantiate(gameplayPrefab);
        gameplayManager.Init(this);
        gameplayManager.Hide();
        tracked = false;

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
            PrepareScan();
        }
    }

    void AttachListener()
    {
        if (!listenersAttached)
        {
            listenersAttached = true;
            arTrackedImageManager.trackedImagesChanged += ImageChanged;
        }
    }

    void DetachListener()
    {
        if (listenersAttached)
        {
            arTrackedImageManager.trackedImagesChanged -= ImageChanged;
            listenersAttached = false;
        }
    }

    void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage arTrackedImage in eventArgs.added)
        {
            UpdateImage(arTrackedImage);
        }
        foreach (ARTrackedImage arTrackedImage in eventArgs.updated)
        {
            UpdateImage(arTrackedImage);
        }
        foreach (ARTrackedImage arTrackedImage in eventArgs.removed)
        {
            if (arTrackedImage.referenceImage.name == "marker")
            {
                //!TODO: find out why code doesn't seem to reach here
                gameplayManager.Hide();
                scanView.Show();
            }
            
        }
    }

    void UpdateImage(ARTrackedImage arTrackedImage)
    {
        gameplayView.UpdateTrackedText(arTrackedImage.referenceImage.name);
        if (arTrackedImage.referenceImage.name == "marker")
        {
            if (arTrackedImage.trackingState != TrackingState.None)
            {
                tracked = true;
            }

            if (tracked)
            {
                Vector3 arPos = arTrackedImage.transform.position;
                gameplayManager.transform.position = arPos;
                Quaternion arRot = arTrackedImage.transform.rotation;
                gameplayManager.transform.rotation = arRot;
                gameplayManager.Show();
                gameplayView.Show();
        
                scanView.Hide();
            }
            else
            {
                gameplayManager.Hide();
            }
            
            
        }
    }

    public void PrepareScan()
    {
        tracked = false;
        gameplayManager.ResetLevel();
        scanView.Show();
        AttachListener();
    }

    public void EndGame()
    {
        DetachListener();
        gameplayView.Hide();
        gameplayManager.Hide();
        WinView.Show();
    }

    // Update is called once per frame
    void Update()
    {
        gameplayManager.DoUpdate(Time.deltaTime);
        WinView.DoUpdate(Time.deltaTime);
        
    }
}
