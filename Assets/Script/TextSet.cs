using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextSet : MonoBehaviour {

    int iteration = 0;
    TextMeshProUGUI textmeshPro;
    int nbQuote = 4;

    // Use this for initialization
    void Start () {

        textmeshPro = GetComponent<TextMeshProUGUI>();

        InvokeRepeating("SetText", 0f, 5f);
    }
	
	void SetText()
    {
        if (iteration % nbQuote == 0)
            textmeshPro.SetText("\"Imperfection is beauty, madness is genius \n and it's better to be absolutely ridiculous than absolutely boring.\"");
        else if (iteration % nbQuote == 1)
            textmeshPro.SetText("\"If you can make a woman laugh, \n you can make her do anything.\"");
        else if (iteration % nbQuote == 2)
            textmeshPro.SetText("\"A wise girl kisses but doesn't love, \n listens but doesn't believe, \n and leaves before she is left.\"");
        else if (iteration % nbQuote == 3)
            textmeshPro.SetText("\"I am good, but not an angel. I do sin, but I am not the devil. \n I am just a small girl in a big world trying to find someone to love.\"");

        iteration++;
    }
}

