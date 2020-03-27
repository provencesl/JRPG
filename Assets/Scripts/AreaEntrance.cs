using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{

    // declare the transition name which will associate the player, exit, and entrance with one another.
    public string transitionName;



    // Start is called before the first frame update
    void Start()
    {
        // if the transition name of the playercontroller which is assigned by the areaexit transition name is the
        // same transition name as the area entrance than send the player controller/player to this spot.
        if (transitionName == PlayerController.instance.areaTransitionName)
        {
            // grab the player's transform position and set it to the transform position of the areaEntrance.
            PlayerController.instance.transform.position = transform.position;
        }

        // tells screen to fade from black when executed.
        UIFade.instance.FadeFromBlack();
        GameManager.instance.fadingBetweenAreas = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
