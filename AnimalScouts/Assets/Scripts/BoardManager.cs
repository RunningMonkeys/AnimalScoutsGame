using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BoardManager : MonoBehaviour
{ 
	private AudioSource aud;

	public int xSize = 8;
	public int ySize = 16;

	public static BoardManager Instance{set;get;}
	private bool[,] allowedMoves{set;get;}
	public Tile[,] tileGrid{set;get;}

	public Piece[,] Pieces{set;get;}
	private Piece selectedPiece;
	public GameObject SelectionPrefab;
	private GameObject SelectionObject;
	
	public bool isRedTurn = true;
	 
	public List<GameObject> playerPrefabs;
	public List<GameObject> activePlayer;
	public List<GameObject> tilePrefabs;
	
	private int redPlayerNumber = 5;
	private int bluePlayerNumber = 5;
	
	
	private const float TILE_SIZE = 1.0f;
	private const float TILE_OFFEST = 0.5f;
	private const float PLAY_HEIGHT = 0f;
	
	private int selectionX = -1;
	private int selectionY = -1;
	
	private void SpawnBoard()
	{
		
		//red side
		
		SpawnTile(2,1,0);
		SpawnTile(2,1,1);
		SpawnTile(2,1,6);
		SpawnTile(2,5,1);
		SpawnTile(2,5,3);
		SpawnTile(2,6,2);
		SpawnTile(3,0,3);
		SpawnTile(3,1,3);
		SpawnTile(3,2,3);
		SpawnTile(3,2,2);
		SpawnTile(3,3,2);
		SpawnTile(4,4,5);
		SpawnTile(4,5,5);
		SpawnTile(4,6,6);
		SpawnTile(4,6,7);
		SpawnTile(4,7,7);
		
		//blue side
		SpawnTile(2,6,15);
		SpawnTile(2,6,14);
		SpawnTile(2,6,9);
		SpawnTile(2,2,14);
		SpawnTile(2,2,12);
		SpawnTile(2,1,13);
		SpawnTile(3,6,12);
		SpawnTile(3,6,12);
		SpawnTile(3,5,12);
		SpawnTile(3,5,13);
		SpawnTile(3,4,13);
		SpawnTile(4,3,10);
		SpawnTile(4,2,10);
		SpawnTile(4,1,9);
		SpawnTile(4,1,8);
		SpawnTile(4,0,8);
		
		
		for(int i = 0; i < xSize; i++)
		{
			for(int j = 0; j< ySize; j++)
			{
				if(tileGrid[i,j] == null)
				{
					SpawnTile(1,i,j);
				}
			}
		}
		
		//randomFlag locations for now.
		int x = Random.Range(0,7);
		int y = Random.Range(0,7);
		tileGrid[x,y].isFlag = true;
		x = Random.Range(0,7);
		y = Random.Range(8,15);
		tileGrid[x,y].isFlag = true;
	}
	private void SpawnAllPlayers()
	{
		Pieces = new Piece[xSize,ySize];
		activePlayer = new List<GameObject>();
		//owls
		SpawnPlayer(0,0,0);
		SpawnPlayer(1,7,15);
		//beavers
		SpawnPlayer(2,1,1);
		SpawnPlayer(3,6,14);
		//cougars
		SpawnPlayer(4,4,1);
		SpawnPlayer(5,3,14);
		//Geckos
		SpawnPlayer(6,7,1);
		SpawnPlayer(7,0,14);
		//wolves
		SpawnPlayer(8,7,0);
		SpawnPlayer(9,0,15);
		
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
		Vector3 pVector = GetTileCenter(x,y);
		pVector.y += PLAY_HEIGHT;
		if(index % 2 == 1)
		{
			Quaternion reverse = new Quaternion(0, 180, 0,0);
			go = Instantiate(playerPrefabs[index], pVector, reverse) as GameObject;
		}
		else
		{
			go = Instantiate(playerPrefabs[index], pVector, Quaternion.identity) as GameObject;
		}
		Pieces[x,y] = go.GetComponent<Piece>();
		Pieces[x,y].setPosition(x,y);
		go.transform.SetParent(transform);
		activePlayer.Add(go);
	}
	
	private void SpawnTile(int index, int x, int y){
		GameObject go;
		go = Instantiate(tilePrefabs[index], GetTileCenter(x,y), Quaternion.identity) as GameObject;
		tileGrid[x,y] = go.GetComponent<Tile>();
		if(y< 8){
			tileGrid[x,y].redSide = true;
		}
		else{
			tileGrid[x,y].redSide = false;
		}
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
    	aud = GetComponent<AudioSource>();

		SelectionObject = Instantiate(SelectionPrefab, GetTileCenter(0,0) , Quaternion.identity) as GameObject;
		Instance = this;
		tileGrid = new Tile[xSize,ySize];
		SpawnBoard();
		SpawnAllPlayers();
    }
	
	private void SelectPiece(int x, int y)
	{
		if(Pieces[x,y] == null)
			return;
		else if(Pieces[x,y].isRed != isRedTurn)
			return;
		
		Pieces[x,y].playSelectNoise();
		
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
				if(isRedTurn)
				{
					bluePlayerNumber--;
				}
				else{
					redPlayerNumber--;
				}
			}
			
			Pieces[selectedPiece.CurrentX, selectedPiece.CurrentY] = null;
			selectedPiece.transform.position = GetTileCenter(x,y);
			Pieces[x,y] = selectedPiece;
			Pieces[x,y].setPosition(x,y);
			
			if(tileGrid[x,y].flipTile(isRedTurn))
			{
				if(isRedTurn)
				{
					SceneManager.LoadScene("RedWins", LoadSceneMode.Additive);
					
				}
				else{
					
					SceneManager.LoadScene("BlueWins", LoadSceneMode.Additive);
				}
			}
			
			if(bluePlayerNumber == 0)
			{
				SceneManager.LoadScene("RedWins", LoadSceneMode.Additive);
				
			}
			else if (redPlayerNumber ==0)
			{
				
				SceneManager.LoadScene("BlueWins", LoadSceneMode.Additive);
			}
			
			isRedTurn = !isRedTurn;
			aud.Play();
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
