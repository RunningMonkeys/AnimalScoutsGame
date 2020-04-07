﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnChecker : MonoBehaviour
{
	private bool isRed;
	public GameObject redGo;
	public GameObject blueGo;
    // Start is called before the first frame update
    void Start()
    {
		isRed = true;
		redGo.SetActive(true);
		blueGo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(BoardManager.Instance.isRedTurn && !this.isRed)
		{
			redGo.SetActive(true);
			blueGo.SetActive(false);
			isRed = true;
		}
		else if(this.isRed && !BoardManager.Instance.isRedTurn)
		{
			redGo.SetActive(false);
			blueGo.SetActive(true);
			isRed = false;
		}
    }
}