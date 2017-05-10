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
	
	}
	
	void Update ()
	{

        if (tutos.isOn)
        {
            PlayerPrefs.SetInt("Tutos", 1);
           // Debug.Log("Hey");
        }else
        {
            PlayerPrefs.SetInt("Tutos", 0);
           // Debug.Log("Listen");
        }

        if (cinem.isOn)
        {
            sceneName = "Intro";
            //Debug.Log("YOLO");
        }
        else
        {
            sceneName = "Game";
            //Debug.Log("SWAG");
        }
        if (Input.GetButton("Submit"))
        {
            SceneManager.LoadScene(sceneName);
        }


    }
}
