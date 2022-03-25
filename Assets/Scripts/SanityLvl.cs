using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class SanityLvl : MonoBehaviour
{
    public Image fillBar;
    public float sanityLvl;
    public GameObject Pills;
    public Canvas canvasMonster;
    public GameObject monsterTrigger;
    GameObject[] monsterArray;
    

    void Start() {
       
        monsterArray = GameObject.FindGameObjectsWithTag ("Monster");
        foreach(GameObject monster in monsterArray)
            {
                monster.SetActive(false);
            }
    }

    public void LoseSanity(int value=25) {
        // Accessing the postprocessingVolume settings
        var postProcessVolume = GameObject.FindObjectOfType<UnityEngine.Rendering.PostProcessing.PostProcessVolume>();
        Vignette bloom = postProcessVolume.profile.GetSetting<UnityEngine.Rendering.PostProcessing.Vignette>();
        var colorParameter = new UnityEngine.Rendering.PostProcessing.ColorParameter();

        // Reduce sanity
        sanityLvl-=value;
        fillBar.fillAmount = sanityLvl/100;

        // Hide Questions
        Pills.gameObject.SetActive(false);


        // Half sanity --> SEE MONSTERS
        if (sanityLvl <=50) {
            foreach(GameObject monster in monsterArray)
            {
                monster.SetActive(true);
            }
        }

        // If sanity lvl reaches 0 ==> INSANE
        // Changes vignette color
        if(sanityLvl<=0) {
            colorParameter.value = Color.magenta;
            bloom.color.Override(colorParameter);
            // canvasMonster.gameObject.SetActive(true);
            monsterTrigger.gameObject.SetActive(true);
        }

        // Not insane ==> Regular color
        else {
            colorParameter.value = new Color(0.06172125f, 0.06631926f, 0.1792453f);
            bloom.color.Override(colorParameter);
        }
    }
}
