using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoader : MonoBehaviour
{

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        // Check to see if there is a player in the scene and if not spawn a new one.
        if(PlayerController.instance == null)
        {
            Instantiate(player);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
