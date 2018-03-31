using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelect : MonoBehaviour {

    public int starsNum = 0;
    private bool isSelect = false;

    public GameObject locks;
    public GameObject stars;

    public GameObject map;
    public GameObject panel;
    public GameObject num;

    private void Start()
    {
        if (PlayerPrefs.GetInt("totalNum", 0) >= starsNum) {
            isSelect = true;
        }
        if (isSelect) {
            locks.SetActive(false);
            stars.SetActive(true);
            num.GetComponent<Text>().text = PlayerPrefs.GetInt(gameObject.name + "Num") + "/30";
        }
    }

    public void Selected() {
        if (isSelect) {
            map.SetActive(false);
            panel.SetActive(true);
        }
    }

}
