using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class IntroMenu : MonoBehaviour
{
    public string sceneName;
    public Button skip;
    public Button final;


    void Start()
    {
        Button btn = skip.GetComponent<Button>();
        Button btnF = final.GetComponent<Button>();
        btn.onClick.AddListener(letsGo);
        btnF.onClick.AddListener(letsGo);
    }
	
	void Update ()
	{

		
	}

    void letsGo()
    {
        SceneManager.LoadScene(sceneName);
    }
}
