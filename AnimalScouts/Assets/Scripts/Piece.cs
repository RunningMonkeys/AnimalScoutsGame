using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    // Start is called before the first frame update
    public int CurrentX{set;get;}
	public int CurrentY{set;get;}
	public int xSize=8;
	public int ySize=16;
	public int numMoves = 4;
	
	
	public bool isRed;
	
	
	private AudioSource PieceNoise;
	
	void Start()
	{
    	PieceNoise = GetComponent<AudioSource>();
	}
	
	public virtual void playSelectNoise()
	{
		PieceNoise.Play();
	}
	
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
