using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour {
    
    public GameObject map;

    public void Return()
    {
        map.SetActive(true);
        gameObject.SetActive(false);
    }

}
