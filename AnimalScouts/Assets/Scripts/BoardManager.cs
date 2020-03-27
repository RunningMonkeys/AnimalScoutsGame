using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoardManager : MonoBehaviour
{ 


	public int xSize = 8;
	public int ySize = 16;

	public static BoardManager Instance{set;get;}
	private bool[,] allowedMoves{set;get;}
	private int[,] tileGrid{set;get;}

	public Piece[,] Pieces{set;get;}
	private Piece selectedPiece;
	public GameObject SelectionPrefab;
	private GameObject SelectionObject;
	
	public bool isRedTurn = true;
	 
	public List<GameObject> playerPrefabs;
	public List<GameObject> activePlayer;
	public List<GameObject> tilePrefabs;
	
	private const float TILE_SIZE = 1.0f;
	private const float TILE_OFFEST = 0.5f;
	
	private int selectionX = -1;
	private int selectionY = -1;
	
	private void SpawnBoard()
	{
		for(int i = 0; i < xSize; i++)
		{
			for(int j = 0; j< ySize; j++)
			{
				SpawnTile(tileGrid[i,j],i,j);
			}
		}
	}
	private void SpawnAllPlayers()
	{
		Pieces = new Piece[xSize,ySize];
		activePlayer = new List<GameObject>();
		SpawnPlayer(0,0,0);
		SpawnPlayer(1,7,15);
		SpawnPlayer(2,5,1);
		SpawnPlayer(3,6,14);
		SpawnPlayer(4,4,1);
		SpawnPlayer(5,6,15);
		SpawnPlayer(6,6,1);
		SpawnPlayer(7,0,14);
		SpawnPlayer(8,1,1);
		SpawnPlayer(9,1,15);
		
	}
	
	private Vector3 GetTileCenter(int x, int y)
	{
		Vector3 origin = Vector3.zero;
		origin.x += (TILE_SIZE * x) + TILE_OFFEST;
		
		origin.z += (TILE_SIZE * y) + TILE_OFFEST;
		return origin;
	}
	
	private void SpawnPlayer(int index, int x, int y)
	{
		GameObject go;
		if(index % 2 == 1)
		{
			Quaternion reverse = new Quaternion(0, 180, 0,0);
			go = Instantiate(playerPrefabs[index], GetTileCenter(x,y), reverse) as GameObject;
		}
		else
		{
			go = Instantiate(playerPrefabs[index], GetTileCenter(x,y) , Quaternion.identity) as GameObject;
		}
		Pieces[x,y] = go.GetComponent<Piece>();
		Pieces[x,y].setPosition(x,y);
		go.transform.SetParent(transform);
		activePlayer.Add(go);
	}
	
	private void SpawnTile(int index, int x, int y){
		GameObject go;
		go = Instantiate(tilePrefabs[index], GetTileCenter(x,y), Quaternion.identity) as GameObject;
	}
	
	
	private void UpdateSelection()
	{
		if(!Camera.main) return;
		
		RaycastHit hit;
		if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit,25.0f,LayerMask.GetMask("Board Plane")))
		{
			selectionX = (int)hit.point.x;
			selectionY = (int)hit.point.z;
			//move selection to that location
			SelectionObject.transform.position = GetTileCenter(selectionX,selectionY);
			SelectionObject.SetActive(true);
		}
		else{
			selectionX = -1;
			selectionY = -1;
			//hide selection
			SelectionObject.SetActive(false);
		}
	}
	
	
    // Start is called before the first frame update
    void Start()
    {
		SelectionObject = Instantiate(SelectionPrefab, GetTileCenter(0,0) , Quaternion.identity) as GameObject;
		Instance = this;
		tileGrid = new int[xSize,ySize];
		for(int i = 0; i < xSize; i++)
		{
			for(int j = 0; j < ySize; j++)
			{
				tileGrid[i,j]= 0;
			}
		}
		SpawnBoard();
		SpawnAllPlayers();
    }
	
	private void SelectPiece(int x, int y)
	{
		if(Pieces[x,y] == null)
			return;
		else if(Pieces[x,y].isRed != isRedTurn)
			return;
		
		allowedMoves = Pieces[x,y].PossibleMove();
		
		
		selectedPiece = Pieces[x,y];
		
		BoardHighlights.Instance.HighlightAllowedMoves(allowedMoves);
		
	}
	
	private void MovePiece(int x, int y)
	{
		if(allowedMoves[x,y])
		{
			Piece p = Pieces[x,y];
			
			if(p != null && p.isRed != isRedTurn)
			{
				//capture the piece
				activePlayer.Remove(p.gameObject);
				Destroy(p.gameObject);
			}
			
			Pieces[selectedPiece.CurrentX, selectedPiece.CurrentY] = null;
			selectedPiece.transform.position = GetTileCenter(x,y);
			Pieces[x,y] = selectedPiece;
			Pieces[x,y].setPosition(x,y);
			isRedTurn = !isRedTurn;
		}
		BoardHighlights.Instance.Hidehighlights();
		selectedPiece = null;
			
			
	}

    // Update is called once per frame
    void Update()
    {
		UpdateSelection();
        //DrawBoard();
		
		if(Input.GetMouseButtonDown(0))
		{
			if(selectionX >=0 && selectionY>=0)
			{
				if(selectedPiece == null)
				{
					SelectPiece(selectionX,selectionY);
				}
				else{
					MovePiece(selectionX,selectionY);
				}
			}
		}
		
		else if(Input.GetMouseButtonDown(1))
		{
			BoardHighlights.Instance.Hidehighlights();
			selectedPiece = null;
		}
    }
}
