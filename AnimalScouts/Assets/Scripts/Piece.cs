using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public abstract class Piece : NetworkBehaviour
{
    // Start is called before the first frame update
    public int CurrentX{set;get;}
	public int CurrentY{set;get;}
	public int xSize=8;
	public int ySize=16;
	public int numMoves = 4;
	
	public bool isRed;
	
	
	
	public void setPosition(int x, int y)
	{
		CurrentX = x;
		CurrentY = y;
	}
	
	public virtual bool[,] PossibleMove()
	{
		return new bool[xSize,ySize];
	}
}
