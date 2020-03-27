using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{
    // Unsure, something about making sure this is the instance.  This allows
    // other scripts to call upon this since it will be the only one in existance.
    public static UIFade instance;

    // Script will check to see if we are fading to black or if we should be ... and vice versa
    // variable to represent the image in the script.
    public Image fadeScreen;

    // The speed at which the screen fades to and from black.
    public float fadeSpeed;

    public bool shouldFadeToBlack;
    public bool shouldFadeFromBlack;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        // makes canvas persistant
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // Conditionals to determine what is going on
        // Changes alpha value
        if (shouldFadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            // check to see if screen is black, if it is stop fading to black.
            if(fadeScreen.color.a == 1f)
            {
                shouldFadeToBlack = false;
            }
        }

        if (shouldFadeFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            // check to see if screen is not black, if it is stop fading from black.
            if(fadeScreen.color.a == 0f)
            {
                shouldFadeFromBlack = false;
            }
        }
    }

    // These methods allow other scripts to call them and fade the screen to and from black.
    public void FadeToBlack()
    {
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }

    public void FadeFromBlack()
    {
        shouldFadeToBlack = false;
        shouldFadeFromBlack = true;
    }
}
