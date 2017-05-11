using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private Text pauseLabel;
    private Image pausePanel;
    private Button restartButton;
    private Button backButton;
    private Button exitButton;
    float tmSc = 0;



	void Start ()
    {

        // The UI elements of the menu
        pauseLabel = GameObject.Find("PauseLabel").GetComponent<Text>();
        pausePanel = GameObject.Find("PausePanel").GetComponent<Image>();
        restartButton = GameObject.Find("RestartButton").GetComponent<Button>();
        backButton = GameObject.Find("BackButton").GetComponent<Button>();
        exitButton = GameObject.Find("ExitButton").GetComponent<Button>();
        exitButton.onClick.AddListener(irMenuPrinc);

        pauseLabel.enabled = false;
        pausePanel.enabled = false;
        SwitchButtonState(restartButton, false);
        SwitchButtonState(backButton, false);
        SwitchButtonState(exitButton, false);
	}

    public void showMenu ()
    {
        tmSc = Time.timeScale;
        pauseLabel.enabled = true;
        pausePanel.enabled = true;
        SwitchButtonState(restartButton, true);
        SwitchButtonState(backButton, true);
        SwitchButtonState(exitButton, true);
        Time.timeScale = 0;
        EventSystem.current.SetSelectedGameObject(backButton.gameObject);
    }

    public void hideMenu()
    {
        pauseLabel.enabled = false;
        pausePanel.enabled = false;
        SwitchButtonState(restartButton, false);
        SwitchButtonState(backButton, false);
        SwitchButtonState(exitButton, false);
   
        Time.timeScale = tmSc;
        
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void SwitchButtonState (Button button, bool state)
    {
        button.enabled = state;
        button.image.enabled = state;
        button.GetComponentInChildren<Text>().enabled = state;
    }

    private void irMenuPrinc()
    {
        SceneManager.LoadScene("Menu");
    }
}
