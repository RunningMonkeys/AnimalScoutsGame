using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	public bool isFlag{set;get;}
	//0 Fliped
	//1 Plains
	//2 Forest
	//3 Water
	//4 Mountain
	public int tileType;
	private bool fliped;
	public Material flippedMat; 
	public bool redSide{set;get;}
	
	public int CurrentX{set;get;}
	public int CurrentY{set;get;}
	
	
    // Start is called before the first frame update
    void Start()
    {
        fliped = false;
    }
	
	public bool flipTile(bool isRed){
		if(isRed == redSide)
		{
			return false;
		}
		else if(fliped){
			return false;
		}
		fliped = true;
		gameObject.GetComponent<Renderer>().material = flippedMat;
		tileType = 0;
		return isFlag;
	}
	
	public bool checkTile(){
		return isFlag;
	}
	

    // Update is called once per frame
    void Update()
    {
        
    }
}
