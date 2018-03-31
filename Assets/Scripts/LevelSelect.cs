using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {

    public bool isSelect = false;
    public Sprite levelBg;
    public GameObject map;
    private Image image;

    public GameObject[] stars;

    private void Awake()
    {
        image = GetComponent<Image>();
    }
    // Use this for initialization
    void Start () {
        if (transform.parent.GetChild(0).name == gameObject.name)
        {
            isSelect = true;
        }
        else {
            int beforeNum = int.Parse(gameObject.name) - 1;
            if (PlayerPrefs.GetInt("level" + beforeNum) > 0) {
                isSelect = true;
            }
        }
        if (isSelect) {
            image.overrideSprite = levelBg;
            transform.Find("num").gameObject.SetActive(true);
            int count = PlayerPrefs.GetInt("level"+gameObject.name);
            if (count > 0) {
                for (int i=0;i<count;i++) {
                    stars[i].SetActive(true);
                }
            }
        }
	}

    public void Selected()
    {
        if (isSelect) {
            PlayerPrefs.SetString("nowLevel", "level" + gameObject.name);
            SceneManager.LoadScene(2);
            print("nihao");
        }
    }

}
