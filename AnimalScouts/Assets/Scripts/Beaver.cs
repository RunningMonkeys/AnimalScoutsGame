using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beaver : Piece
{
	
   public override bool[,] PossibleMove()
	{
		bool[,] r = new bool[xSize,ySize];
		Piece p;
		//right
		for(int i = 1; i <= numMoves && CurrentX+i< xSize; i++)
		{
			p = BoardManager.Instance.Pieces[CurrentX+i,CurrentY];
			if(p == null)
			{
				r[CurrentX+i,CurrentY] = true;
			}
			else if( p.isRed != isRed)
			{
				r[CurrentX+i, CurrentY] = true; 
				break;
			}
		}
		//left
		for(int i = 1; i <= numMoves && CurrentX-i >=0; i++)
		{
			p = BoardManager.Instance.Pieces[CurrentX-i,CurrentY];
			if(p == null)
			{
				r[CurrentX-i,CurrentY] = true;
			}
			else if( p.isRed != isRed)
			{
				r[CurrentX-i, CurrentY] =true; 
				break;
			}
		}
		//up
		for(int i = 1; i <= numMoves && CurrentY+i < ySize; i++)
		{
			p = BoardManager.Instance.Pieces[CurrentX,CurrentY+i];
			if(p == null)
			{
				r[CurrentX,CurrentY+i] = true;
			}
			else if( p.isRed != isRed)
			{
				r[CurrentX, CurrentY+i] =true; 
				break;
			}
		}
		//down
		for(int i = 1; i <= numMoves && CurrentY - i >= 0; i++)
		{
			p = BoardManager.Instance.Pieces[CurrentX,CurrentY-i];
			if(p == null)
			{
				r[CurrentX,CurrentY-i] = true;
			}
			else if( p.isRed != isRed)
			{
				r[CurrentX, CurrentY-i] =true; 
				break;
			}
		}
	
		
		
		return r;
	}
}
