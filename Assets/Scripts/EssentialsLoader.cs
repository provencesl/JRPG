using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{

    // Make EssentialsLoader load UIScreen and Player
    public GameObject UIScreen;
    public GameObject Player;
    // Make it gameMan because GameManager refers to actual script
    public GameObject gameMan;
    public GameObject battleManager;
    public GameObject audioManager;

    // Start is called before the first frame update
    void Start()
    {
        // If UIFade doesn't exist make one.
        if(UIFade.instance == null)
        {
            UIFade.instance = Instantiate(UIScreen).GetComponent<UIFade>();
        }

        //
        if(PlayerController.instance == null)
        {
            PlayerController clone = Instantiate(Player).GetComponent<PlayerController>();
            PlayerController.instance = clone;
        }

        if(GameManager.instance == null)
        {
            Instantiate(gameMan);
        }

        if(AudioManager.instance == null)
        {
            Instantiate(audioManager);
        }

        if(BattleManager.instance == null)
        {
            Instantiate(battleManager);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
