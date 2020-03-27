using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Declare a RigidBody for the controller.  Use Unity to attach player object's rigidbody 2d to the public variable on the RigidBody Component.
    public Rigidbody2D theRB;

    // Declare a variable to set the speed of the player object.  Axis is a float value from the Unity Input.
    public float moveSpeed;

    // Decalare a variable to associate Animator with player object.
    public Animator myAnim;

    // We are making a reference to itself.  Sounds like a singleton.
    // The static declaration makes it so there can only be one instance of this script, controller, ie the player in the world
    public static PlayerController instance;

    // This is to declare what area the palyer is in.
    public string areaTransitionName;

    // Set map boundaries for player to keep him on the map.
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    // stops player from moving
    public bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        // This checks to see if there is an instance of this script ie the Player
        // if there isn't one it creates one.  Should only run the first time the game is started.
        if(instance == null)
        {
            // This script is the only script in existance.
            instance = this;
        }
        else
        {
            // Make sure instance is not this object.  If instance is me don't destroy me.
            if(instance != this)
            {
                // if there is one than destroy the new object.
                Destroy(gameObject);
            }

        }
        // tell unity not to destroy the player when it loads into scene.
        // gameObject refers to the player in this instance.  By default the game
        // object is whatever this script is attached to.
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // if player can move than move
        if(canMove)
        {
            // This declares movement(velocity) for player controller.  Grabs input from the "Horizontal" and "Vertical" input controls of Unity.
            theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;

        }
        else
        {
            // stop player from moving if they cannot move.  Stops the player from continuing to move it was moving when it lost ability to move
            theRB.velocity = Vector2.zero;
        }


        // Set the moveX parameter on myAnim which is attached to the player to theRB (rigidbody) velocity.
        myAnim.SetFloat("moveX", theRB.velocity.x);
        myAnim.SetFloat("moveY", theRB.velocity.y);

        // Check to see if our input is a whole number and determine direction of lastMove to set direction of player when he stops moving.
        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            if(canMove)
            {
                myAnim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                myAnim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
            }
        }

        // keep the player inside the bounds.
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x), Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z);
    }

    // This to allow the camera to call this method and set the values that serve as the boundary limits for the player.  bottomleft topright.
    public void SetBounds(Vector3 botLeft, Vector3 topRight)
    {
        bottomLeftLimit = botLeft + new Vector3(.5f, .5f, 0f);
        topRightLimit = topRight + new Vector3(-.5f, -.5f, 0f);
    }
}
