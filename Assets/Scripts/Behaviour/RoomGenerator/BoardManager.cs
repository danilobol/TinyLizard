using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    [Serializable]
    public class Count
    {
        public int mininum;
        public int maxnum;

        public Count(int min, int max)
        {
            mininum = min;
            maxnum = max;
        }
    }



    // Start is called before the first frame update
    public int colunms; //quantidades de colunas do tabuleiro
    public int rows; //quantidades de linhas do tabuleiro
    [HideInInspector]
    public Count wallCount = new Count(1, 40); //quantidade aleatórias de paredes internas

    public GameObject[] floorTiles; //tipos de chãos
    public GameObject[] wallTiles; //tipos de muros internos

    public GameObject[] outerWalls; //muros externos
    public Camera cameraTop;

    public GameObject[] floor;

    public Transform roomSetupTransform;

    [HideInInspector]
    public Transform boardHolder; // variavel para agrupar todos do tabuleiro
    private bool startInstances = false;
    public GameObject player;

    

    //configurando muros externos e o chão do tabuleiro
    private void BoardSetup(int openingDirection)
    {
        //inicializa o tabuleiro e atribui o seu transform
        boardHolder = new GameObject("Board").transform;
        int yp = (int)roomSetupTransform.position.y;
        int xp = (int)roomSetupTransform.position.x;


        for (int x = xp -1; x < xp + colunms + 1; x++)
                {
                    for (int y = yp -1; y < yp + rows + 1; y++)
                    {



                         GameObject toInstatiate = floorTiles[Random.Range(0, floorTiles.Length)];
                            

                         //verificar se é muro externo e a direção do muro externo

                         if (x == xp - 1 || y == yp - 1 || x == xp + colunms || y == yp + rows)
                         {
                             toInstatiate = outerWalls[Random.Range(0, outerWalls.Length)];

                         }

                         if (openingDirection == 3)
                         {
                             if (x == xp -1)
                             {
                                 toInstatiate = floorTiles[Random.Range(0, floorTiles.Length)];
                             }
                         }
                         else if(openingDirection==4)
                         {
                             if (x == xp + colunms -1)
                             {
                                 toInstatiate = floorTiles[Random.Range(0, floorTiles.Length)];
                             }
                         }
                         else if (openingDirection == 2)
                         {
                             if (y == yp + rows -2)
                             {
                                 toInstatiate = floorTiles[Random.Range(0, floorTiles.Length)];
                             }
                         }
                         else if (openingDirection == 1)
                         {
                             if (y == yp)
                             {
                                 toInstatiate = floorTiles[Random.Range(0, floorTiles.Length)];
                             }
                         }

                         GameObject instance = Instantiate(toInstatiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                         instance.transform.SetParent(boardHolder);
                       


                    }
                }
        
            }

   

    //pega o arrey de muros internos, junto com o valor minimo e maximo que vai ser adicionado daquele numero de objeto
    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        int obejctCount = Random.Range(minimum, maximum);


        
        floor = GameObject.FindGameObjectsWithTag("floor");

        

        for (int i = 0; i < obejctCount; i++)
        {
            int num = Random.Range(floor.Length, 0);
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            GameObject instance = Instantiate(tileChoice, floor[num].gameObject.transform.position, Quaternion.identity);
            instance.transform.SetParent(boardHolder);
            floor[num].tag = "floorX";
        }



        floor = null;

    }
    
    public void SetupSccene(int openingDirection)
    {
        colunms = Random.Range(8, 16);
        rows = Random.Range(8, 16);
        BoardSetup(openingDirection);

        //instanciar um numero aleatorio de muros internos


        if (startInstances == false)
        {
            startInstances = true;
            StartCoroutine(WaitAndPrint(4f));
        }
    }
    

    private void InstantiatePrefabs(GameObject mob)
    {
        floor = GameObject.FindGameObjectsWithTag("floor");
        int x = Random.Range(1000, floor.Length);
        GameObject p = Instantiate(mob, floor[x].transform.position, Quaternion.identity) as GameObject;
        floor = null;
        PlayerMovement pm = p.GetComponent<PlayerMovement>();
        if(pm != null && cameraTop != null)
        {
            cameraTop.gameObject.SetActive(false);
        }

    }
    

    IEnumerator WaitAndPrint(float waitTime)
    {
        startInstances = true;
        yield return new WaitForSeconds(waitTime);
        LayoutObjectAtRandom(wallTiles, wallCount.mininum, wallCount.maxnum);
        InstantiatePrefabs(player);
    }

}
