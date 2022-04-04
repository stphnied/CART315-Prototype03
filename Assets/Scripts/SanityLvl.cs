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
    public GameObject house;
    public GameObject mother;
    public Canvas canvasMonster;
    public GameObject monsterTrigger;
    SpriteRenderer sprite;
    GameObject[] monsterArray;
    GameObject [] lightsArray;
    GameObject blockStairs;
    GameObject player;
    float colorAlpha = 0f;
    

    void Start() {
        // Find all monsters 
        monsterArray = GameObject.FindGameObjectsWithTag ("Monster");
        foreach(GameObject monster in monsterArray)
            {
                monster.SetActive(false);
                sprite = monster.GetComponent<SpriteRenderer>();
            }
        blockStairs= GameObject.Find("BlockStairs");
        blockStairs.SetActive(false);

        player = GameObject.Find("punpun");
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


        // Sanity 50% = SEES MONSTERS
        // Too unstable to walk upstairs
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
            blockStairs.SetActive(true);

            // Change Mother and House color to red
            house.GetComponent<SpriteRenderer>().color = new Color(0.5f,0.036f,0.036f);
            mother.GetComponent<SpriteRenderer>().color = new Color(0.5f,0.036f,0.036f);
        }

        // Sanity at 25%
        // Street lamps start flickering
        if (sanityLvl <=25) {
            // Lights
            lightsArray = GameObject.FindGameObjectsWithTag ("Light");
                foreach(GameObject light in lightsArray)
            {
                light.GetComponent<LightFlickering>().enabled = true;
                }
        }

        // Sanity at 0%
        // Changes vignette color
        // NIGHTMARE MODE
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

        // Not insane = resets everything to normal
        else {
            colorParameter.value = new Color(0.06172125f, 0.06631926f, 0.1792453f);
            bloom.color.Override(colorParameter);
        }
        
    }

    // Resets the state of the game to normal
    public void resetNormal() {
        // Reset PP
        var postProcessVolume = GameObject.FindObjectOfType<UnityEngine.Rendering.PostProcessing.PostProcessVolume>();
        Vignette bloom = postProcessVolume.profile.GetSetting<UnityEngine.Rendering.PostProcessing.Vignette>();
        var colorParameter = new UnityEngine.Rendering.PostProcessing.ColorParameter();
        colorParameter.value = new Color(0.06172125f, 0.06631926f, 0.1792453f);
        bloom.color.Override(colorParameter);

        // Monsters
        foreach(GameObject monster in monsterArray)
        {
            monster.SetActive(false);
            monsterTrigger.gameObject.SetActive(false);
        }

        // Lights
        lightsArray = GameObject.FindGameObjectsWithTag ("Light");
        foreach(GameObject light in lightsArray)
        {
            light.GetComponent<LightFlickering>().enabled = false;
        }

        // Mom and house color back to normal
        mother.GetComponent<SpriteRenderer>().color = new Color(0.66f,0.63f,0.77f);
        house.GetComponent<SpriteRenderer>().color = new Color(0.37f,0.37f,0.37f);
    } 
}
