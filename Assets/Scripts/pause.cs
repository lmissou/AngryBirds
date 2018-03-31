using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour {

    private Animator anim;

    public GameObject pauseButton;
    public GameObject skillBt;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Pause() {
        if (GameManager._instance.birds.Count > 0) {
            if (GameManager._instance.birds[0].isRelease == false) {
                GameManager._instance.birds[0].canMove = false;
                GameManager._instance.birds[0].isFly = false;
            }
        }
        Time.timeScale = 1;
        anim.SetBool("isPause", true);
        pauseButton.SetActive(false);
        skillBt.SetActive(false);
    }

    public void Resume() {
        if (GameManager._instance.birds.Count > 0)
        {
            if (GameManager._instance.birds[0].isRelease == false)
            {
                GameManager._instance.birds[0].canMove = true;
                GameManager._instance.birds[0].isFly = true;
            }
        }
        Time.timeScale = 1;
        anim.SetBool("isPause", false);
        pauseButton.SetActive(true);
        skillBt.SetActive(true);
    }
    public void PauseEnd()
    {
        Time.timeScale = 0;
    }

    public void ResumeEnd()
    {
        Time.timeScale = 1;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
