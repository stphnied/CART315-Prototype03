using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    public NPCConversation myConvo;
    public GameObject ConvoBox;
    public Vector3 newPos;
    GameObject player;

    void Start() {
        player = GameObject.Find("punpun");
    }
    private void OnMouseOver() {
        // When left click pressed
        // Dialogue box will appear
        if(Input.GetMouseButtonDown(0)){
            ConversationManager.Instance.StartConversation(myConvo);
            RectTransform rt = ConvoBox.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(newPos.x,newPos.y,newPos.z);

            player.GetComponent<Move>().speed =0f; //Make the player unable to move
        }
    }
}
