using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    // Start is called before the first frame update
    public int CurrentX{set;get;}
	public int CurrentY{set;get;}
	public bool isFlag{set;get;}
	public bool hasItem{set;get;}
	
	public void setPosition(int x, int y)
	{
		CurrentX = x;
		CurrentY = y;
	}
	
	
}



