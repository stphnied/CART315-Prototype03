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
    SpriteRenderer sprite;
    GameObject[] monsterArray;
    GameObject [] lightsArray;
    float colorAlpha = 0f;
    

    void Start() {
        // Find all monsters 
        monsterArray = GameObject.FindGameObjectsWithTag ("Monster");
        foreach(GameObject monster in monsterArray)
            {
                monster.SetActive(false);
                sprite = monster.GetComponent<SpriteRenderer>();
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
                if(colorAlpha < 0.8f) {
                    colorAlpha += 0.2f;
                }
                else {colorAlpha = 0.8f;}
                
                monster.GetComponent<SpriteRenderer>().color = new Color (0.32f,0.32f,0.32f,colorAlpha);
                monster.SetActive(true);

               
            }
        }

        if (sanityLvl <=25) {
                // Lights
                lightsArray = GameObject.FindGameObjectsWithTag ("Light");
                 foreach(GameObject light in lightsArray)
                {
                    light.GetComponent<LightFlickering>().enabled = true;
                }
        }

        // If sanity lvl reaches 0 ==> INSANE
        // Changes vignette color
        if(sanityLvl<=0) {
            colorParameter.value = Color.magenta;
            bloom.color.Override(colorParameter);
            monsterTrigger.gameObject.SetActive(true);

            foreach(GameObject monster in monsterArray)
            {
                monster.GetComponent<SpriteRenderer>().color = new Color (0.63f,0.36f,1f,1f);
                monster.SetActive(true);
            }
        }

        // Not insane ==> Regular color
        else {
            colorParameter.value = new Color(0.06172125f, 0.06631926f, 0.1792453f);
            bloom.color.Override(colorParameter);
        }
    }

    public void regainSanity(int value=25) {
        sanityLvl+=value;
        fillBar.fillAmount = sanityLvl/100;
    }
}
