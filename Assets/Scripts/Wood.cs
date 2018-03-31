using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour {
    public float maxSpeed=10;
    public float minSpeed = 5;
    public Sprite Hurt;
    public GameObject boom;
    public GameObject pigScore;
    private SpriteRenderer render;

    public bool isPig=false;
    public AudioClip hurtClip;
    public AudioClip deadClip;
    public AudioClip birdCollision;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            AudioPlay(birdCollision);
        }
        if (collision.relativeVelocity.magnitude > maxSpeed) {//直接死亡
            dead();
        } else if (collision.relativeVelocity.magnitude > minSpeed && collision.relativeVelocity.magnitude < maxSpeed) {
            AudioPlay(hurtClip);
            render.sprite = Hurt;
        }
    }

    // Use this for initialization
    void Start () {
        render = GetComponent<SpriteRenderer>();
	}


    public void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
    public void dead() {
        if (isPig)
        {
            AudioPlay(deadClip);
            GameManager._instance.pigs.Remove(this);
        }
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        Destroy(Instantiate(pigScore, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity), 1.5f);
    }
}
