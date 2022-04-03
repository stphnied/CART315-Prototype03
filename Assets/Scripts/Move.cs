using UnityEngine;
using System.Collections;
using DialogueEditor;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
 
public class Move : MonoBehaviour {
 
    public float speed;
    public float jump;
    public GameObject controls;
    public GameObject M1;
    public GameObject M2;
    public GameObject M3;
    public GameObject ScreenBoundTxt;
    public NPCConversation myConvo;
    public GameObject ConvoBox;
    public GameObject stairways;
    public GameObject Cam;
    private SpriteRenderer _renderer;
    Animator _animator;
    Rigidbody2D rb;
    Light lt;
    GameObject lightPower;
    GameObject [] lightsArray;
 
    void Start () {
        _animator = gameObject.GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        rb = GetComponent <Rigidbody2D> ();
        lt = GetComponent<Light>();
    }
 
    void Update () {
        // movement
        float x = Input.GetAxis ("Horizontal");

        // flips sprite
        if (Input.GetAxisRaw("Horizontal") > 0) {
            _renderer.flipX = false;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0) {
            _renderer.flipX = true;
        }   

        // jump
        if (Input.GetButtonDown("Jump") &&  Mathf.Abs(rb.velocity.y) < 0.001f) {
            rb.AddForce(new Vector2(0,jump), ForceMode2D.Impulse); 
            _animator.SetBool("isJumping", true);
        }
        else {
            _animator.SetBool("isJumping", false);
        }

        // update new position
        _animator.SetFloat("Speed",Mathf.Abs(x));
        transform.position += new Vector3 (x,0f,0f) * Time.deltaTime * speed;

        if(Input.anyKey) {
            Invoke("CloseControls",2f); 
        }
    }

    // Hides controls after player moves
    void CloseControls () {
        controls.gameObject.SetActive(false);
    }

    // When the player is within a range --> triggers a convo
    void OnTriggerEnter2D(Collider2D col) {

        // WITHIN MONSTERS RANGE
        // Near the first monster
        if(col.gameObject.tag == "MT1") {
            M1.gameObject.SetActive(true);
        }
        // Near the 2nd monster
        else if(col.gameObject.tag == "MT2") {
             M2.gameObject.SetActive(true);
        }

        else if (col.gameObject.tag == "MT3") {
            M3.gameObject.SetActive(true);
        }


        // WITHIN SCREEN BOUND RANGE
        else if (col.gameObject == ScreenBoundTxt) {
            Debug.Log("stop");
            ConversationManager.Instance.StartConversation(myConvo);
            RectTransform rt = ConvoBox.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(-10.39f,-0.77f,0f);
            this.GetComponent<Move>().speed =0f;
        }

        if(col.gameObject == stairways) {
            Cam.GetComponent<CameraFollow>().isUpstairs = true;
            Debug.Log("UP WE GO");
        }

        if(col.gameObject.tag == "Light") {
            if(Cam.GetComponent<SanityLvl>().sanityLvl >= 0f && Cam.GetComponent<SanityLvl>().sanityLvl <= 75f ) {
                Cam.GetComponent<SanityLvl>().sanityLvl += 25;
                Cam.GetComponent<SanityLvl>().fillBar.fillAmount += Cam.GetComponent<SanityLvl>().sanityLvl/100 + 0.25f;
                // Debug.Log(Cam.GetComponent<SanityLvl>().sanityLvl);
            }
            if (Cam.GetComponent<SanityLvl>().sanityLvl == 100) {
                Cam.GetComponent<SanityLvl>().sanityLvl = 100f;
            }
            lightPower = GameObject.Find("LightPower");
            lightPower.GetComponent<Light>().color =  new Color(0.8f,0.5f,0.07f);
            Destroy(col.gameObject,1f);

        }
    }

    //When out of range 
     void OnTriggerExit2D(Collider2D col)
    {
        M1.gameObject.SetActive(false);
        M2.gameObject.SetActive(false);
        M3.gameObject.SetActive(false);
        Cam.GetComponent<CameraFollow>().isUpstairs = false;
    }

}