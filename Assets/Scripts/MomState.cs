using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DialogueEditor;
using UnityEngine.SceneManagement;

public class MomState : MonoBehaviour
{
    private SpriteRenderer spriteR;
    public GameObject punpun;
    public GameObject Cam;
    RectTransform rt;

    public bool isTalked = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteR = GetComponent<SpriteRenderer>();   
    }

    void Update() {
        if(isTalked) {
            if( transform.position.x != 87f) {
                float x = 0.025f;
                transform.position += new Vector3 (x,0f,0f);
                spriteR.flipX = false;

                if (transform.position.x == 87f || transform.position.x >= 87f) {

                    transform.position = new Vector3 (87.06f,1.27f,0f);
                    gameObject.GetComponent<Collider2D>().enabled = false;

                    punpun.transform.position += new Vector3 (x,0f,0f);
                    if (punpun.transform.position.x == 87f || punpun.transform.position.x >= 87f) {
                        transform.position = new Vector3 (86f,1.27f,0f);
                        gameObject.SetActive(false);
                        Invoke("Ending",4f);
                    }
                }
            }
        }
    }

    public void boolTalk() {
        isTalked = true;
    }

    public void Ending() {
        punpun.SetActive(false);

        if(Cam.GetComponent<SanityLvl>().sanityLvl ==1f ) {
            SceneManager.LoadScene(4);  
        }
        else {
            SceneManager.LoadScene(3);  
        }
    }

}
