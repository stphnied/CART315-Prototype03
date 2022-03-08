using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakePills : MonoBehaviour
{
    int currentCount = 0;
    public GameObject Pills;
    public Button yourButton;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
        Invoke("AskQuestion",4f);
    }

    void TaskOnClick() {
        currentCount++;
        Pills.gameObject.SetActive(false);
        Invoke("AskQuestion",4f);
    }

    void AskQuestion() {
        if(currentCount < 4) {
            Pills.gameObject.SetActive(true);
        }
    }

}
