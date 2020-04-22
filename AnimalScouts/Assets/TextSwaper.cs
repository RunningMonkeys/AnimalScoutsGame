using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSwaper : MonoBehaviour
{
	
	private bool loreOn;
	private string startingText;
	
    // Start is called before the first frame update
    void Start()
    {
		startingText = gameObject.GetComponent<Text>().text;
        loreOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            TextChange();
        }
    }
	
	public void TextChange()
	{
		if(!loreOn)
		{
			gameObject.GetComponent<Text>().text = "The animal scouts have desided to play a game of capture the flag.\nHelp them search for the enemies flag \nwhile keeping your players safe from being taged.\nBe careful to keep in mind the abilities of your Scouts, \nthe Lizard, Cougar and Beaver have trouble with \nforests, water and mountains respectively\nThe owl doesn't like fighting but is a great spotter.\nThe wolf won't touch the water \nand it takes all his energy to climb the mountain. \nPress L to go back";
			loreOn = true;
		}
		else
		{
			gameObject.GetComponent<Text>().text = startingText;
			loreOn = false;
		}
		
		
	}
}
