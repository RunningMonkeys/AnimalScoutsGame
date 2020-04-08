using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
	public int[,] tileGrid{set;get;}
	private int xSize = 8;
	private int ySize = 8;
	private const float TILE_SIZE = 1.0f;
	private const float TILE_OFFEST = 0.5f;
	public List<GameObject> tilePrefabs;
	
	
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < xSize; i++)
		{
			for(int j = 0; j  < ySize; j++)
			{
				if(i <= 3 && j < 5)
				{
					tileGrid[i,j] = i;
				}
				else{
					tileGrid[i,j] = 0;
				}
			}
		}
		SpawnBoard();
    }

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
	private void SpawnTile(int index, int x, int y){
		GameObject go;
		go = Instantiate(tilePrefabs[index], GetTileCenter(x,y), Quaternion.identity) as GameObject;
	}
	
	private Vector3 GetTileCenter(int x, int y)
	{
		Vector3 origin = Vector3.zero;
		origin.x += (TILE_SIZE * x) + TILE_OFFEST;
		
		origin.z += (TILE_SIZE * y) + TILE_OFFEST;
		return origin;
	}
    // Update is called once per frame
    void Update()
    {
        
    }
}
