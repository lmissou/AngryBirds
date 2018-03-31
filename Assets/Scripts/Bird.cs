using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bird : MonoBehaviour {

	private bool isClick=false;
    protected SpriteRenderer render;
	public Transform rightPos;
	public Transform leftPos;
	public float maxDis=1.5f;
    [HideInInspector]
	public SpringJoint2D sp;
    protected Rigidbody2D rg;

    public LineRenderer right;
    public LineRenderer left;

    public GameObject boom;
    public GameObject skillBt;

    protected TestMyTrail myTrail;
    [HideInInspector]
    public bool canMove = false;

    public float smooth = 3;
    public bool isRelease = false;
    public bool isFly=false;

    public AudioClip select;
    public AudioClip fly;
    public Sprite Hurt;

    public void Awake()
    {
        skillBt.SetActive(false);
        sp = GetComponent<SpringJoint2D> ();
		rg = GetComponent<Rigidbody2D> ();
        myTrail = GetComponent<TestMyTrail>();
        render = GetComponent<SpriteRenderer>();
	}

	private void OnMouseDown(){//鼠标按下
        if (canMove) {
            AudioPlay(select);
		    isClick=true;
		    rg.isKinematic = true;
        }
	}
	private void OnMouseUp(){//鼠标抬起
        if (canMove) {
            isClick=false;
		    rg.isKinematic = false;
		    Invoke ("Fly",0.1f);
            right.enabled = false;
            left.enabled = false;
            canMove = false;
        }
	}
	private void Update(){

        //if (EventSystem.current.IsPointerOverGameObject())
        //    return;

		if (isClick) {//鼠标一直按下，进行位置跟随
			transform.position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			transform.position += new Vector3 (0,0,10);


            if (Vector3.Distance (transform.position, rightPos.position) > maxDis) {//进行位置限定
				Vector3 pos=(transform.position-rightPos.position).normalized;//单位化向量
				pos *= maxDis;//最大长度的向量
				transform.position=pos+rightPos.position;
            }
            Line();
        }
        float posX = transform.position.x;
        float posY = transform.position.y;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,new Vector3(Mathf.Clamp(posX,0,15), Mathf.Clamp(posY, 0, 4), Camera.main.transform.position.z),smooth*Time.deltaTime);
	}

    /*private void LateUpdate()
    {
        if (isFly && Input.GetMouseButtonDown(0))
        {
            ShowSkill();
        }
    }*/

    void Fly(){
        isRelease = true;
        isFly = true;
        AudioPlay(fly);
        myTrail.StartTrails();
		sp.enabled=false;
        skillBt.SetActive(true);
        Invoke("Next",4);
	}

    void Line() {//画线

        right.enabled = true;
        left.enabled = true;

        right.SetPosition(0,rightPos.position);
        right.SetPosition(1,transform.position);

        left.SetPosition(0,leftPos.position);
        left.SetPosition(1,transform.position);
    }

    protected virtual void Next() {//下一只小鸟飞出
        GameManager._instance.birds.Remove(this);
        Destroy(gameObject);
        Instantiate(boom,transform.position,Quaternion.identity);
        GameManager._instance.NextBird();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isFly = false;
        skillBt.SetActive(false);
        myTrail.ClearTrails();
        if (collision.gameObject.tag == "Enemy") {
            render.sprite = Hurt;
        }
    }

    public virtual void ShowSkill() {
        isFly = false;
        skillBt.SetActive(false);
    }

    public void AudioPlay(AudioClip clip) {
        AudioSource.PlayClipAtPoint(clip,transform.position);
    }

}
