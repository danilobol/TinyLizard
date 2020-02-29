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
   // public Count foodCount = new Count(1, 5); //quantidades de comidas
    public GameObject exit; //portal de saida
    public GameObject[] floorTiles; //tipos de chãos
    public GameObject[] wallTiles; //tipos de muros internos
    //public GameObject[] foodTiles; // comidas
    //public GameObject[] forceTiles; //aumenta a força
    //public GameObject[] enemyTypes; // inimigos
    public GameObject[] outerWalls; //muros externos
    public Camera cameraTop;

    public GameObject[] floor;

    public Transform roomSetupTransform;

    [HideInInspector]
    public Transform boardHolder; // variavel para agrupar todos do tabuleiro
    private List<Vector3> grindPosition = new List<Vector3>(); //lista com possiveis posições para os tiles
    private int level;
    private bool startInstances = false;
    public GameObject player;

    //limpando a lista do grid, e preparando para gerar um novo tabuleiro
    private void InitialiseList()
    {
        //limpa as posições
        grindPosition.Clear();

        //Loop para navegar as colunas
        for (int x = 1; x < colunms - 1; x++)
        {
            for (int y = 1; y < rows - 1; y++)
            {
                grindPosition.Add(new Vector3(x, y, 0f));
            }
        }

        BoardSetup(1);
    }

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

                //verificar se é muro externo
                if (x == xp-1 || y == yp-1 || x == xp + colunms || y == yp + rows)
                {
                    toInstatiate = outerWalls[Random.Range(0, outerWalls.Length)];
                
                }
                GameObject instance = Instantiate(toInstatiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    //retorna um valor aleatorio da nossa grindposition
    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, grindPosition.Count);
        Vector3 randomPosition = grindPosition[randomIndex];
        grindPosition.RemoveAt(randomIndex);
        return randomPosition;
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
    
    public void SetupSccene(int level)
    {
        colunms = Random.Range(8, 16);
        rows = Random.Range(8, 16);
        //inicializou o grind
        InitialiseList();

        //instanciar um numero aleatorio de muros internos
        

        InstanceExit(level);
        if(startInstances == false)
            StartCoroutine(WaitAndPrint(2f));

    }
    

    public void InstanceExit(int openingDirection)
    {
        for (int x = -1; x < colunms + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {

                GameObject toInstatiate = floorTiles[Random.Range(0, floorTiles.Length)];

                //verificar se é muro externo
                if (x == -1 || y == -1 || x == colunms || y == rows)
                {
                    toInstatiate = outerWalls[Random.Range(0, outerWalls.Length)];

                }
                GameObject instance = Instantiate(toInstatiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(this.transform);
            }
        }

       
    }

    private void InstantiatePrefabs(GameObject mob)
    {
        floor = GameObject.FindGameObjectsWithTag("floor");
        int x = Random.Range(1000, floor.Length);
        GameObject p = Instantiate(mob, floor[x].transform.position, Quaternion.identity) as GameObject;
        floor = null;
        PlayerMovement pm = p.GetComponent<PlayerMovement>();
        if(pm != null)
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
/*
    IEnumerable CameraAnimation(float waitTime)
    {
        yield return WaitForSeconds(waitTime);
    }
    */
}
