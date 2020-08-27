using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Completed
{
    using System.Collections.Generic;       //Allows us to use Lists. 
    using UnityEngine.UI;                   //Allows us to use UI.

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance = null;
        private BoardManager boardManager;
        //[HideInInspector]
        private int level = 1;
        public int healthPlayer = 30;


        private void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);

            boardManager = GetComponent<BoardManager>();
            InitGame();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static public void CallbackInitialization()
        {
            //register the callback to be called everytime the scene is loaded
            SceneManager.sceneLoaded += OnSceneLoaded;
        }


        static private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            instance.level++;
            instance.InitGame();
        }


        private void InitGame()
        {


            boardManager.SetupSccene(level, healthPlayer);
        }

        public void Restart()
        {
            level = 0;
            healthPlayer = 30;
            Completed.BoardManager.instance.enemyList.Clear();
        }
        
        public int GetLevel()
        {
            return level;
        }
    }
}
