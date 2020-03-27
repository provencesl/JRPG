using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{

    // Set up references to control dialog box, name box, and text in box.
    // Whatever speech is happening
    public Text dialogText;

    // Who is speaking
    public Text nameText;

    // Whether we should be showing a dialogBox;
    public GameObject dialogBox;

    // Whether we should be showing a nameBox;
    public GameObject nameBox;

    // a list of lines we need to show to players.  Will make into an string array.
    public string[] dialogLines;

    // need to keep track of what line we are showing.
    public int currentLine;

    // make it so we don't skip the first line of text on first release of mouse button (fire1)
    public bool justStarted;

    // Make sure there is only one manager.
    public static DialogManager instance;

    private string questToMark;
    private bool markQuestComplete;
    private bool shouldMarkQuest;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        // Make first line show up in box (Demo)
        //dialogText.text = dialogLines[currentLine];
    }

    // Update is called once per frame
    void Update()
    {
        // We can only do this if the dialog button is open.  Prevents player from reading text when
        // it is not open.
        if(dialogBox.activeInHierarchy)
        {
            // Upon the release of the button it moves forward.
            if(Input.GetButtonUp("Fire1"))
            {
                // make sure we don't skip the first line
                if(!justStarted)
                {
                    // add one and go to next line
                    currentLine++;

                    // prevents currentline from moving beyond the array.
                    if (currentLine >= dialogLines.Length)
                    {
                        // deactivate the dialog box
                        dialogBox.SetActive(false);

                        //// release player to move when at end of dialog
                        //PlayerController.instance.canMove = true;

                        // Change code to take advantage of Boolean in GameManager
                        GameManager.instance.dialogActive = false;

                        if(shouldMarkQuest)
                        {
                            shouldMarkQuest = false;
                            if(markQuestComplete)
                            {
                                QuestManager.instance.MarkQuestComplete(questToMark);
                            }
                            else
                            {
                                QuestManager.instance.MarkQuestIncomplete(questToMark);
                            }
                        }
                    }
                    else
                    {
                        // check to see if it is a name box before displaying dialog.
                        CheckIfName();
                        // update text that is being shown.
                        dialogText.text = dialogLines[currentLine];
                    }
                }
                else
                {
                    // first line already started so don't move to next line on release of button.
                    justStarted = false;
                }

            }
        }
    }

    // allows activation of Dialog and pull in array of newLines
    // need to know if it is a person
    public void ShowDialog(string[] newLines, bool isPerson)
    {
        // array length will adjust size of dialog lines box
        dialogLines = newLines;

        // set current line
        currentLine = 0;

        // Check to see if the dialog is a namebox
        CheckIfName();

        // show lines on the screen
        // never explicity tell it what lines.  Use variable.
        // dialogText.text = dialogLines[0];
        dialogText.text = dialogLines[currentLine];
        dialogBox.SetActive(true);

        justStarted = true;

        // make namebox active if it is a person.
        nameBox.SetActive(isPerson);

        // when we activate dialog stop the character from moving
        //PlayerController.instance.canMove = false;

        // Change code to take advantage of Boolean in GameManager
        GameManager.instance.dialogActive = true;
    }

    // check to see if the line is a name
    public void CheckIfName()
    {
        if(dialogLines[currentLine].StartsWith("n-"))
        {
            nameText.text = dialogLines[currentLine].Replace("n-", "");
            currentLine++;
        }
    }

    public void ShouldActivateQuestAtEnd(string questName, bool markComplete)
    {
        questToMark = questName;
        markQuestComplete = markComplete;

        shouldMarkQuest = true;
    }
}
