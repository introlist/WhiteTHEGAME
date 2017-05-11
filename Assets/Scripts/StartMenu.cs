using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public Toggle cinem;
    public Toggle tutos;
    string sceneName;


    void Start ()
	{
        if (PlayerPrefs.GetInt("IntroOnOff") == 1)
        {
            cinem.isOn = true;
        }
        if (PlayerPrefs.GetInt("IntroOnOff") == 0)
        {
            cinem.isOn = false;
        }
        if (PlayerPrefs.GetInt("Tutos") == 1)
        {
            tutos.isOn = true;
        }
        if (PlayerPrefs.GetInt("Tutos") == 0)
        {
            tutos.isOn = false;
        }
        cinem.onValueChanged.AddListener(checkIntros);
        tutos.onValueChanged.AddListener(checkTutos);
	}
	
	void Update ()
	{

       
        if (Input.GetKey(KeyCode.Escape))
        {
           if( PlayerPrefs.GetInt("IntroOnOff") == 1)
            {
                SceneManager.LoadScene("Intro");
            }
            if (PlayerPrefs.GetInt("IntroOnOff") == 0)
            {
                SceneManager.LoadScene("Game");
             }
        }


    }

    private void checkIntros(bool boolean)
    {
        if (boolean == true)
        {
            PlayerPrefs.SetInt("IntroOnOff", 1);
           // Debug.Log("IntroOnOff = " + PlayerPrefs.GetInt("IntroOnOff"));
            
        }
        else
        {
            PlayerPrefs.SetInt("IntroOnOff", 0);
           // Debug.Log("IntroOnOff = " + PlayerPrefs.GetInt("IntroOnOff"));
        }
    }

    private void checkTutos(bool boolean) { 
                  if (tutos.isOn)
        {
            PlayerPrefs.SetInt("Tutos", 1);
            // Debug.Log("Hey");
        }
        else
        {
            PlayerPrefs.SetInt("Tutos", 0);
            // Debug.Log("Listen");
        }
    }
}
