using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    // Name of the scene we are going to load
    public string areaToLoad;

    // This is to declare what area the player is in.
    public string areaTransitionName;

    // Associate the AreaEntrance with the AreaExit
    public AreaEntrance theEntrance;

    // set the wait to load to 1;
    public float waitToLoad = 1f;

    // make sure scene loads after the screen fades from black to clear again.
    private bool shouldLoadAfterFade;



    // Start is called before the first frame update
    void Start()
    {
        // permanently associates the entrance with the area so only the one field needs to be entered in.
        theEntrance.transitionName = areaTransitionName;
    }

    // Update is called once per frame
    void Update()
    {
        // Check to see if we should be loading after fade.
        if(shouldLoadAfterFade)
        {
            // Make it slowly load based on machine.
            waitToLoad -= Time.deltaTime;
            //
            if(waitToLoad <= 0)
            {
                // once screen is black cancel should fade and load scene.
                shouldLoadAfterFade = false;
                // If player has load new scene
                // What scene do we load?  Load the scene delcared in the AreaExit Script public variable.
                SceneManager.LoadScene(areaToLoad);
            }
        }
    }

    // To determine if the player collides with triggerbox
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check to see what object triggered the triggerbox to ensure on the player can change the scene
        if(other.tag == "Player")
        {

            // Tells screen to fade to black.
            shouldLoadAfterFade = true;
            UIFade.instance.FadeToBlack();

            // Stop player from moving
            GameManager.instance.fadingBetweenAreas = true;

            // this declares that the areaTransitionName of the player is the
            // areaTransitionName of the areaExit.  It will then test against the entrance
            // and the entrance will place the player at the location of the entrance.
            PlayerController.instance.areaTransitionName = areaTransitionName;
        }
    }

}
