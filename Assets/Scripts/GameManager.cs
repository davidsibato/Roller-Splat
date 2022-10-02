using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

public class GameManager : MonoBehaviour
{

    public static GameManager singleton;

    private GroundPiece[] allGroundPieces;

    // Start is called before the first frame update
    void Start()
    {
        SetupNewLevel();
    }

    private void SetupNewLevel()
    {
        allGroundPieces = FindObjectsOfType<GroundPiece>();
    }

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }else if (singleton!= this)
        {
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += onLevelFinishedLoading;
    }
    private void onLevelFinishedLoading(Scene scene,LoadSceneMode mode)
    {
        SetupNewLevel();
    }
    public void CheckComplete()
    {
        bool isFinished = true;

        for (int i = 0; i < allGroundPieces.Length; i++)
        {
            if (allGroundPieces[i].isColored== false)
            {
                isFinished = false;
                Debug.Log("ground is: " + i + " of " + allGroundPieces.Length);
                break;
            }
        }

        if (isFinished)
        {
            
            NextLevel();
        }
    }
    private void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 10)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
