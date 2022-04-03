using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DialogueEditor;

public class TakePills : MonoBehaviour
{
    int currentCount = 0;
    public NPCConversation myConvo;
    public GameObject ConvoBox;
    public GameObject Pills;
    public Button noButton, yesButton;
    public GameObject player;
    Rigidbody rigidB;

    // Start is called before the first frame update
    void Start()
    {
        // player = GameObject.Find("punpun");
        noButton.onClick.AddListener(noBtnTask);
        yesButton.onClick.AddListener(yesBtnTask);
        Invoke("AskQuestion",7f);
    }
    
    void AskQuestion() {
        if(currentCount < 4) {
            Pills.gameObject.SetActive(true);
            player.GetComponent<Move>().speed = 0f;
        }
    }

    void noBtnTask() {
        currentCount++;
        Pills.gameObject.SetActive(false);
        player.GetComponent<Move>().speed = 5f;
        Invoke("AskQuestion",5f);
    }

    public void yesBtnTask() {
        ConversationManager.Instance.StartConversation(myConvo);
        RectTransform rt = ConvoBox.GetComponent<RectTransform>();
        rt.localPosition = new Vector3(player.transform.position.x,0f,player.transform.position.x);
        currentCount++;
        Pills.gameObject.SetActive(false);
        player.GetComponent<Move>().speed = 5f;
        Invoke("AskQuestion",10f);
        
    }

    public void ResetSpeed() {
        player.GetComponent<Move>().speed = 5f;
    }

}