using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public List<Bird> birds;
    public List<Wood> pigs;
    public static GameManager _instance;
    private Vector3 originPos;

    public GameObject pause;

    public GameObject lose;
    public GameObject win;

    public GameObject[] stars;

    private int starsNum = 0;

    public int totalNum=30;

    private void Awake()
    {
        _instance = this;
        if(birds.Count>0)
            originPos = birds[0].transform.position;
    }
    private void Start()
    {
        Initialized();
    }

    private void Initialized()
    {
        Time.timeScale = 1;
        for (int i = 0; i < birds.Count; i++) {
            if (i == 0)
            {
                birds[i].gameObject.transform.position = originPos;
                birds[i].canMove = true;
                birds[i].enabled = true;
                birds[i].sp.enabled = true;
            }
            else {
                birds[i].enabled = false;
                birds[i].sp.enabled = false;
            }
        }
    }

    public void NextBird() {
        if (pigs.Count > 0)
        {
            if (birds.Count > 0)
            {
                //下一只小鸟
                Initialized();
            }
            else {
                //输了
                lose.SetActive(true);
            }
        }
        else {
            //赢了
            win.SetActive(true);
        }
    }
    public void ShowStars() {
        StartCoroutine("show");
    }

    IEnumerator show() {
        for (starsNum = 0; starsNum < birds.Count + 1 ; starsNum++)
        {
            if (starsNum >= stars.Length) break;
            yield return new WaitForSeconds(0.2f);
            stars[starsNum].SetActive(true);
        }
    }
    public void ReTry()
    {
        SaveData();
        SceneManager.LoadScene(2);
    }

    public void Home() {
        SaveData();
        SceneManager.LoadScene(1);
    }

    public void BirdSkill() {
        if (birds.Count > 0)
            birds[0].ShowSkill();
    }

    public void SaveData() {
        if (starsNum>PlayerPrefs.GetInt(PlayerPrefs.GetString("nowLevel"))) {
            PlayerPrefs.SetInt(PlayerPrefs.GetString("nowLevel"),starsNum);
        }
        int sum = 0;
        for (int i = 0; i < totalNum; i++) {
            sum += PlayerPrefs.GetInt("level" + i);
        }
        PlayerPrefs.SetInt("totalNum",sum);
        sum = 0;
        for (int i = 0; i < 11; i++) {
            sum += PlayerPrefs.GetInt("level" + i);
        }
        PlayerPrefs.SetInt("map1Num",sum);
        sum = 0;
        for (int i = 11; i < 21; i++) {
            sum += PlayerPrefs.GetInt("level" + i);
        }
        PlayerPrefs.SetInt("map2Num",sum);
        sum = 0;
        for (int i = 21; i < 31; i++) {
            sum += PlayerPrefs.GetInt("level" + i);
        }
        PlayerPrefs.SetInt("map3Num",sum);

    }
}
