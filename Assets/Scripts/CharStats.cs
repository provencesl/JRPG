using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{

    // Name of Character
    public string charName;

    // Level of player
    public int playerLevel = 1;

    // Exp to gain for levels
    public int currentExp;

    // Values to each level to level up
    public int[] expToNextLevel;

    // Set Max Level
    public int maxLevel = 100;

    // base exp?
    public int baseEXP = 1000;

    // Current Hitpoints
    public int currentHP;

    // Max Hitpoints
    public int maxHP = 100;

    // Current Magic Points
    public int currentMP;

    // Max Magic Points
    public int maxMP = 30;

    //
    public int[] mpLvlBonus;

    // Strength Level
    public int strength;

    // Defence Level
    public int defence;

    // Weapon Power
    public int wpnPwr;

    // Armor Power
    public int armrPwr;

    // Name of Equipped Weapon
    public string equippedWpn;

    // Name of Equipped Armour
    public string equippedArmr;

    // Character Image
    public Sprite charImage;

    // Start is called before the first frame update
    void Start()
    {
        // Establish the EXP Tree
        expToNextLevel = new int[maxLevel];
        expToNextLevel[1] = baseEXP;

        // a For loop to make the EXP levels
        for(int i = 2; i < expToNextLevel.Length; i++)
        {
            // Improve this, kind of lame
            expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.05f);
            
        }

    }

    // Update is called once per frame
    void Update()
    {
        // To test levelling up
        if (Input.GetKeyDown(KeyCode.K))
        {
            AddExp(1000);
        }
    }

    // To test levelling
    public void AddExp(int expToAdd)
    {
        // take current exp and add expToAdd to it
        currentExp += expToAdd;

        if (playerLevel < maxLevel)
        {
            // test to see if we level up
            if (currentExp > expToNextLevel[playerLevel])
            {
                // Take the exp needed to level up out of the players exp
                currentExp -= expToNextLevel[playerLevel];

                // Level the player up one level
                playerLevel++;

                // determine whether to add to str or def based on odd or even level (even number strength, odd number def)
                if (playerLevel % 2 == 0)
                {
                    strength++;
                }
                else
                {
                    defence++;
                }

                // max increase
                maxHP = Mathf.FloorToInt(maxHP * 1.05f);
                // give character currenthp of maxhp on level up
                currentHP = maxHP;

                //
                maxMP += mpLvlBonus[playerLevel];
                currentMP = maxMP;
            }
        }

        // Bug, doesn't zero out the exp after the player hits level 10.
        if(playerLevel >= maxLevel)
        {
            currentExp = 0;
        }
    }
}
