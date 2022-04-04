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
    public GameObject M4;
    public GameObject M5;
    public GameObject ScreenBoundTxt;
    public NPCConversation myConvo;
    public NPCConversation BlockingStairs;
    public NPCConversation DizzyStairs;
    public NPCConversation goodMomConvo;
    public NPCConversation badMomConvo;
    public GameObject ConvoBox;
    public GameObject stairways;
    public GameObject lightTrigger;
    public GameObject Cam;
    private SpriteRenderer _renderer;
    Animator _animator;
    Rigidbody2D rb;
    Light lt;
    GameObject lightPower;
    float tryingUp = 0f;
    public bool isNormal = false;
 
    void Start () {
        _animator = gameObject.GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        rb = GetComponent <Rigidbody2D> ();
        lt = GetComponent<Light>();
        lightPower = GameObject.Find("LightPower");
        isNormal = false;
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

        // Close control panel
        if(Input.anyKey) {
            Invoke("CloseControls",3f); 
        }
    }

    // Hides controls after player moves
    void CloseControls () {
        controls.gameObject.SetActive(false);
    }

    // Displays different light  color
    public void regainSanity() {
        lightPower.GetComponent<Light>().color =  new Color(0.5f,0.76f,0.099f);
        lightTrigger.SetActive(true);
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

        // 3rd
        else if (col.gameObject.tag == "MT3") {
            RectTransform rt = ConvoBox.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(25.67f,-0.77f,0f);
            M3.gameObject.SetActive(true);
        }

        // 4th
        else if (col.gameObject.name == "MonsterT4") {
            M4.gameObject.SetActive(true);
        }

        // 5th
        else if (col.gameObject.name == "MonsterT5") {
            M5.gameObject.SetActive(true);
        }

        // WITHIN SCREEN BOUND RANGE
        else if (col.gameObject == ScreenBoundTxt) {
            Debug.Log("stop");
            ConversationManager.Instance.StartConversation(myConvo);
            RectTransform rt = ConvoBox.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(-10.39f,-0.77f,0f);
            this.GetComponent<Move>().speed =0f;
        }

        // WHEN GOING UP
        if(col.gameObject == stairways) {
            Cam.GetComponent<CameraFollow>().isUpstairs = true;
        }

        // REGAIN SANITY UNDER ONE LIGHT
        if(col.gameObject.name == "LightSanity") {
            if(Cam.GetComponent<SanityLvl>().sanityLvl >= 0f && Cam.GetComponent<SanityLvl>().sanityLvl <= 75f ) {
                Cam.GetComponent<SanityLvl>().sanityLvl += 25;
                Cam.GetComponent<SanityLvl>().fillBar.fillAmount += Cam.GetComponent<SanityLvl>().sanityLvl/100 + 0.25f;
            }

            if (Cam.GetComponent<SanityLvl>().sanityLvl == 100) {
                Cam.GetComponent<SanityLvl>().sanityLvl = 100f;
            }

            Invoke("IncreaseSanityLight",2f);
            Destroy(col.gameObject,2f);
        }
    }

    //When out of range 
     void OnTriggerExit2D(Collider2D col)
    {
        M1.gameObject.SetActive(false);
        M2.gameObject.SetActive(false);
        M3.gameObject.SetActive(false);
        M4.gameObject.SetActive(false);
        M5.gameObject.SetActive(false);
        Cam.GetComponent<CameraFollow>().isUpstairs = false;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // Blocking the stairs if punpun sanity is low
        if(col.gameObject.name == "BlockStairs") {
            ConversationManager.Instance.StartConversation(BlockingStairs);
            RectTransform rt = ConvoBox.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(66.51f,-0.77f,0f);
            this.GetComponent<Move>().speed =0f;
            tryingUp++;

            // If punpun tries more than 3 times --> bad ending
            if(tryingUp >=3) {
            ConversationManager.Instance.StartConversation(DizzyStairs);
            rt.localPosition = new Vector3(66.51f,-0.77f,0f);
            this.GetComponent<Move>().speed =0f;
            Destroy(col.gameObject,1f);
            }
        }

        if(col.gameObject.name == "mother") {
            RectTransform rt = ConvoBox.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(80.02f,4.54f,0f);
            this.GetComponent<Move>().speed =0f;
            
            // Good Sanity Lvl
            if (Cam.GetComponent<SanityLvl>().sanityLvl ==1f) {
                ConversationManager.Instance.StartConversation(goodMomConvo);
            }
            // Bad Sanity Lvl
            else if (Cam.GetComponent<SanityLvl>().sanityLvl != 1f) {
                ConversationManager.Instance.StartConversation(badMomConvo);
            }
        }
    }

    //Resets everything back to normal 
    public void IncreaseSanityLight() {
        Cam.GetComponent<SanityLvl>().sanityLvl = 100f;
        Cam.GetComponent<SanityLvl>().fillBar.fillAmount += Cam.GetComponent<SanityLvl>().sanityLvl = 1f;
        Cam.GetComponent<SanityLvl>().resetNormal();

        lightPower.GetComponent<Light>().color =  new Color(0.8f,0.5f,0.07f);
        isNormal = true;
        
        GameObject stairs = GameObject.Find("BlockStairs");
        stairs.SetActive(false);
    }
}


