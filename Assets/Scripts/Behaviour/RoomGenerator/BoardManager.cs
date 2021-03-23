using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;



namespace Completed
{
    using System.Collections.Generic;       //Allows us to use Lists. 
    using UnityEngine.UI;                   //Allows us to use UI.

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


        public static BoardManager instance;
        // Start is called before the first frame update
        public int colunms; //quantidades de colunas do tabuleiro
        public int rows; //quantidades de linhas do tabuleiro
        [HideInInspector]
        public Count wallCount = new Count(1, 40); //quantidade aleatórias de paredes internas
                                                   // public Count foodCount = new Count(1, 5); //quantidades de comidas
        public GameObject exit; //portal de saida
        public GameObject[] floorTiles; //tipos de chãos
        public GameObject[] wallTiles; //tipos de muros internos
        public GameObject[] outerWalls; //muros externos
        public Camera cameraTop;
        public GameObject[] enemys;
        
        public GameObject[] floor;

        public Transform roomSetupTransform;

        public List<GameObject> enemyList;
        private bool enimiesActive = false;
        private GameObject imageLevel;


        [HideInInspector]
        public Transform boardHolder; // variavel para agrupar todos do tabuleiro
        private List<Vector3> grindPosition = new List<Vector3>(); //lista com possiveis posições para os tiles
        private int level;
        private int healthPlayer;


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

            BoardSetup();
        }

        //configurando muros externos e o chão do tabuleiro
        private void BoardSetup()
        {
            //inici aliza o tabuleiro e atribui o seu transform
            boardHolder = new GameObject("Board").transform;
            int yp = 0;
            int xp = 0;
            for (int x = xp - 1; x < xp + colunms + 1; x++)
            {
                for (int y = yp - 1; y < yp + rows + 1; y++)
                {

                    GameObject toInstatiate = floorTiles[Random.Range(0, floorTiles.Length)];

                    //verificar se é muro externo
                    if (x == xp - 1 || y == yp - 1 || x == xp + colunms || y == yp + rows)
                    {
                        toInstatiate = outerWalls[Random.Range(0, outerWalls.Length)];

                    }
                    GameObject instance = Instantiate(toInstatiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                    if (x == xp && y == yp)
                    {
                        instance.tag = "InicialPlay"; //marcar a posição inicial do player
                    }

                    instance.transform.SetParent(boardHolder);
                }
            }
            GameObject p = Instantiate(player, new Vector3(xp, yp, 0), Quaternion.identity);
            
            PlayerController play = p.GetComponent<PlayerController>();
            play.SetLevelPlayer(healthPlayer, level + 2);
            InstanceEnemy(level);
        }

        //This is called each time a scene is loaded.
        static private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            instance.level++;
            instance.InitialiseList();
        }

        //retorna um valor aleatorio da grindposition
        Vector3 RandomPosition()
        {
            int randomIndex = Random.Range(0, grindPosition.Count);
            Vector3 randomPosition = grindPosition[randomIndex];
            grindPosition.RemoveAt(randomIndex);
            return randomPosition;
        }

        public void SetupSccene(int level, int healthPlayer)
        {
            colunms = Random.Range(8, 16);
            rows = Random.Range(8, 16);
            this.level = level;
            instance = this;
            this.healthPlayer = healthPlayer;



            imageLevel = GameObject.FindGameObjectWithTag("LevelImage");
            LevelUI textLevel = imageLevel.GetComponent<LevelUI>();
            textLevel.ShowLevelText(level);
            StartCoroutine(TimeLoadScene());



            //inicializou o grind
            InitialiseList();
            
            


        }

        IEnumerator TimeLoadScene()
        {
            yield return new WaitForSeconds(2f);
            imageLevel.SetActive(false);
        }


        public void InstanceExit()
        {
            GameObject[] floor = GameObject.FindGameObjectsWithTag("floor");
            GameObject instance = Instantiate(exit, floor[Random.Range(0, floor.Length)].transform.position, Quaternion.identity) as GameObject;
           // instance.transform.SetParent(this.transform);                
        }

        public void InstanceEnemy(int level)
        {
            int enemyCount = (int)Mathf.Log(level, 2f) * 2 + 2;
            Debug.Log("São: "+ enemyCount+" Inimigos");
            GameObject[] floor = GameObject.FindGameObjectsWithTag("floor");
            for (int i = 0; i < enemyCount; i++)
            {
                if(i < enemyCount/2)
                    StartCoroutine(EnemyGenerate(floor[Random.Range(0, floor.Length)].transform, 1));
                else
                    StartCoroutine(EnemyGenerate(floor[Random.Range(0, floor.Length)].transform, 2));
            }


        }

        private IEnumerator EnemyGenerate(Transform enemyPos, int num)
        {
            yield return new WaitForSeconds(2f);
            if (num == 1)
            {
                PlayerController p = FindObjectOfType<PlayerController>();
                GameObject monster = Instantiate(enemys[Random.Range(0, enemys.Length)], enemyPos.position, Quaternion.identity);
                Slime slime = monster.GetComponent<Slime>();
                slime.health = Random.Range(p.attack - 1, p.attack * 4);
                slime.monsterAttack = Random.Range(p.attack - 2, p.attack + 2);
                slime.points = Random.Range(slime.health / 1 + 2, slime.health / 2 + 2);
                enemyList.Add(slime.gameObject);
                enimiesActive = true;
            }
            else
            {
                PlayerController p = FindObjectOfType<PlayerController>();
                GameObject monster = Instantiate(enemys[Random.Range(0, enemys.Length)], enemyPos.position, Quaternion.identity);
                Slime slime = monster.GetComponent<Slime>();
                slime.health = Random.Range(p.attack, p.attack * 9);
                slime.monsterAttack = Random.Range(p.attack - 2, p.attack + 4);
                slime.points = Random.Range(slime.health / 1, slime.health / 4);
                enemyList.Add(slime.gameObject);
                enimiesActive = true;
            }
        }


        private void Update()
        {
            if (enimiesActive)
            {
               
                if (enemyList.Count == 0)
                {
                    InstanceExit();
                    enimiesActive = false;
                }
                
            }
        }

        public void RemoveEnemyList(GameObject monster)
        {
            enemyList.Remove(monster);
        }
        

    }
}