using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{

    // makes the camera follow it's target.  Transform contains position and scale.
    public Transform target;

    // creates a reference to the tilemap
    public Tilemap theMap;

    // Create map limits for the camera.
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    // This is to stop the camera at the border of the camera view instead of
    // letting the actual camera hit the border.
    private float halfHeight;
    private float halfWidth;

    public int musicToPlay;
    private bool musicStarted;

    // Start is called before the first frame update
    void Start()
    {
        // declare that the target of the camera is the instance of the playercontroller.
        // it needs to be done in the script so that the player object carries over from scene to scene.
        // Instead of using the instance that is available. (Commented out in Essentials Loader video).
        //target = PlayerController.instance.transform;
        // Search every object in the scene and find the playercontroller and attach camera to it.
        target = FindObjectOfType<PlayerController>().transform;

        // Measure the height of the tilemap and cut it.
        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;


        // We need to make sure ground (largest tilemap) is the limiter.
        // this sets the boundaries to the limit of the map.
        bottomLeftLimit = theMap.localBounds.min + new Vector3(halfWidth, halfHeight, 0f);
        topRightLimit = theMap.localBounds.max + new Vector3(-halfWidth,-halfHeight, 0f);

        // Send the boundaries to the player.
        PlayerController.instance.SetBounds(theMap.localBounds.min, theMap.localBounds.max);

    }

    // LateUpdate is called once per frame after Update
    void LateUpdate()
    {
        // make the position of this object to the position of the player controller (or target)
        // current x and y of target but camera z remains the same.
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        // keep the camera inside the bounds.
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x), Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z);

        if(!musicStarted)
        {
            musicStarted = true;
            AudioManager.instance.PlayBGM(musicToPlay);
        }
    }
}
