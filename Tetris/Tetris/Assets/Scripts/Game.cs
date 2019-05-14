using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Game : MonoBehaviour
{
    public static int gridWidth = 10;
    public static int gridHeight = 20;

    public static Transform[,] array = new Transform[gridWidth, gridHeight];

    // Start is called before the first frame update
    void Start()
    {
        SpawnTetrimino();
    }

    public bool CheckIsAboveGrid(Tetrimino tetrimino) 
    {
        for(int x = 0; x < gridWidth; ++x)
        {
            foreach(Transform mino in tetrimino.transform)
            {
                Vector2 pos = Round(mino.position);

                if (pos.y > gridHeight - 1)
                {
                    return true;

                }
            }
        }
    return false;
    }

    public bool IsRowFull (int y) 
    {
        for(int x = 0; x < gridWidth; ++x)
        {
            if(array[x,y] == null)
            {
                return false;
            }
        }
        return true;
    }

    public void DeleteMino (int y) 
    {
        for (int x = 0; x < gridWidth; ++x)
        {
            Destroy (array[x, y].gameObject);
            array[x, y] = null;
        }
    }

    public void MoveRowDown (int y) 
    {
        for(int x = 0; x < gridWidth; ++x)
        {
            if(array[x,y] != null)
            {
                array[x, y -1] = array[x,y];
                array[x,y] = null;
                array[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public void MoveAllRowsDown(int y)
    {
        for(int i = y; i < gridHeight; ++i)
        {
            MoveRowDown(i);
        }
    }

    public void DeleteRow()
    {
        for (int y = 0; y < gridHeight; ++y) 
        {
            if (IsRowFull(y))
            {
                DeleteMino(y);

                MoveAllRowsDown(y + 1);
                --y;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateGrid(Tetrimino tetrimino)
    {
        //Transform[,] array = new Transform[gridWidth, gridHeight];

        for (int y = 0; y < gridHeight; ++y) 
        {
            for (int x = 0; x < gridWidth; ++x) 
            {
                if (array[x, y] != null)
                {
                    if (array[x, y].parent == tetrimino.transform)
                    {
                        array[x, y] = null;
                    }
                }
            }
        }
        
        foreach (Transform mino in tetrimino.transform)
        {
            Vector2 pos = Round(mino.position);
            if (pos.y < gridHeight)
            {
                array[(int)pos.x, (int)pos.y] = mino;
            }
        }
    }

    public void SpawnTetrimino() 
    {
        GameObject nextTetrimino = (GameObject)Instantiate(Resources.Load(GetRandomTetrimino(), typeof(GameObject)), new Vector2(5.0f, 20.0f), Quaternion.identity);
    }
    
    public bool CheckIsInsideGrid (Vector2 pos) {
        return ((int)pos.x >= 0 && (int)pos.x < gridWidth &&(int)pos.y >= 0);
    }

    public Vector2 Round (Vector2 pos) {
        return new Vector2 (Mathf.Round(pos.x), Mathf.Round(pos.y));
    }

    public Transform GetTransformAtGridPosition(Vector2 pos)
    {
        // Transform[,] array = new Transform[gridWidth, gridHeight];
        if (pos.y > gridHeight -1)
        {
            return null;
        }
        else 
        {
            return array[(int)pos.x, (int)pos.y];
        }
    }
    
    string GetRandomTetrimino() 
    {
        int randomTetrimino = Random.Range(1,8);

        string randomTetriminoName = "Prefabs/Tetrimino T";
        switch (randomTetrimino) {
            
            case 1:
            randomTetriminoName = "Prefabs/Tetrimino T";
            break;
            
            case 2:
            randomTetriminoName = "Prefabs/Tetrimino_Long";
            break;

            case 3:
            randomTetriminoName = "Prefabs/Tetrimino Square";
            break;

            case 4:
            randomTetriminoName = "Prefabs/Tetrimino J";
            break;

            case 5:
            randomTetriminoName = "Prefabs/Tetrimino L";
            break;

            case 6:
            randomTetriminoName = "Prefabs/Tetrimino S";
            break;

            case 7:
            randomTetriminoName = "Prefabs/Tetrimino Z";
            break;
        }
      return randomTetriminoName;
    } 

public void GameOver()
{
    Application.LoadLevel("GameIsOver");
}

}
