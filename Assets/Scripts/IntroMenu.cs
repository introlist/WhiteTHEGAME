using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class IntroMenu : MonoBehaviour
{
    public string sceneName;
    public Button skip;
    public Button nextFrameB;
    public Image[] frames;
    public Image dialog;
    private int i = 0;
    public AudioSource tensiona;
    public AudioSource piano;
    public AudioSource chase1;

    void Start()
    {

        chase1 = gameObject.AddComponent<AudioSource>() as AudioSource;
        AudioClip chaseClip = Resources.Load<AudioClip>("Chase1");
        chase1.clip = chaseClip;
        chase1.loop = true;
        tensiona = gameObject.AddComponent<AudioSource>() as AudioSource;
        AudioClip tensClip = Resources.Load<AudioClip>("tension");
        tensiona.clip = tensClip;
        tensiona.loop = true;
        piano= gameObject.AddComponent<AudioSource>() as AudioSource;
        AudioClip pianoClip = Resources.Load<AudioClip>("Piano");
        piano.clip = pianoClip;
        piano.loop = true;

        skip.onClick.AddListener(letsGo);
        nextFrameB.onClick.AddListener(nextFrame);

        for (int x = 0;  x < frames.Length; x++)
        {
            frames[x].enabled = false;
        }

        piano.Play();
    }
	
	void Update ()
	{
       
		
	}

    void letsGo()
    {
        SceneManager.LoadScene(sceneName);
    }

    void nextFrame()
    {
        if (i < frames.Length)
        {

            if(i == 4)
            {
                piano.Stop();
                tensiona.Play();

            }

            if(i == 6)
            {
                tensiona.Stop();
                chase1.Play();
            }
            if (i > 0)
            {
                frames[i - 1].enabled = false;
            }
            frames[i].enabled = true;
            i++;
        }else
        {
            SceneManager.LoadScene(sceneName);
        }
     
        
           
    }
}
