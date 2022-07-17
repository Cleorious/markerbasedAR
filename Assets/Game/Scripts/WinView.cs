using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinView : MonoBehaviour
{
    public TextMeshProUGUI countdownText;

    private GameManager gameManager;

    private bool isCountingDown;
    private float timer;
    private int countdown;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
        isCountingDown = false;
    }

    public void Show()
    {
        gameObject.SetActive(true);
        timer = 0f;
        isCountingDown = true;
        countdown = Parameter.WIN_TIMER;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        isCountingDown = false;
    }
    
    // Update is called once per frame
    public void DoUpdate(float dt)
    {
        if (isCountingDown)
        {
            timer += dt;
            if (timer >= 1f)
            {
                timer -= 1f;
                countdown--;
                countdownText.SetText(string.Format("Returning in {0}..", countdown));
            }

            if (countdown <= 0)
            {
                gameManager.PrepareScan();
            }
        }
    }
}
