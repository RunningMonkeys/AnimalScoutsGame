using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owl : Piece
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
		}
		//left
		for(int i = 1; i <= numMoves && CurrentX-i >=0; i++)
		{
			p = BoardManager.Instance.Pieces[CurrentX-i,CurrentY];
			if(p == null)
			{
				r[CurrentX-i,CurrentY] = true;
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
		}
			
		//down
		for(int i = 1; i <= numMoves && CurrentY - i >=0; i++)
		{
			p = BoardManager.Instance.Pieces[CurrentX,CurrentY-i];
			if(p == null)
			{
				r[CurrentX,CurrentY-i] = true;
			}
		}
			
			//up right
		for(int i = 1; i <= numMoves && CurrentX+i< xSize&& CurrentY+i < ySize; i++)
		{
			p = BoardManager.Instance.Pieces[CurrentX+i,CurrentY+i];
			if(p == null)
			{
				r[CurrentX+i,CurrentY+i] = true;
			}
		}
			
			//up left
		for(int i = 1; i <= numMoves && CurrentX-i >=0&& CurrentY+i < ySize; i++)
		{
			p = BoardManager.Instance.Pieces[CurrentX-i,CurrentY+i];
			if(p == null)
			{
				r[CurrentX-i,CurrentY+i] = true;
			}
		}
			
		//down left
		for(int i = 1; i <= numMoves && CurrentX-i >=0&& CurrentY-i >=0; i++)
		{
			p = BoardManager.Instance.Pieces[CurrentX-i,CurrentY-i];
			if(p == null)
			{
				r[CurrentX-i,CurrentY-i] = true;
			}
		}
			
		//down right
		for(int i = 1; i <= numMoves && CurrentY-i >=0&& CurrentX+i < xSize; i++)
		{
			p = BoardManager.Instance.Pieces[CurrentX+i,CurrentY-i];
			if(p == null)
			{
				r[CurrentX+i,CurrentY-i] = true;
			}
		}
			
		
		
		return r;
	}
}
