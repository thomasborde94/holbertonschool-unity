using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public static ObstacleSpawner Instance { get; private set; }
    [SerializeField] private GameObject[] obstacleArray = new GameObject[10];
    [SerializeField] private int numberOfObstacles = 3;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        DisableObstacles();
    }
    private void Update()
    {
        if (!VRControlSwitcher.XRisPresent())
        {
            if (PlayerController.Instance.ball != null)
            {
                if (PlayerController.Instance.ball.GetComponent<BallScript>().isInAlley && !IsAnyObstacleSpawned)
                {
                    shouldSpawnObstacles = true;
                }
                if (!PlayerController.Instance.ball.GetComponent<BallScript>().isInAlley)
                {
                    DisableObstacles();
                }
            }
        }

        else if (VRControlSwitcher.XRisPresent())
        {
            if (VRInteractions.Instance.ball != null)
            {
                if (VRInteractions.Instance.ball.GetComponent<BallScript>().isInAlley && !IsAnyObstacleSpawned)
                {
                    shouldSpawnObstacles = true;
                }
                if (!VRInteractions.Instance.ball.GetComponent<BallScript>().isInAlley)
                {
                    DisableObstacles();
                }
            }
        }
            

        if (shouldSpawnObstacles)
            SpawnObstacles ();
    }

    private void SpawnObstacles()
    {
        for (int i = 0; i < numberOfObstacles; i++)
        {
            
            int y = Random.Range(0, obstacleArray.Length);
            for (int j = 0; j < alreadySpawnedIndex.Length; j++)
            {
                if (alreadySpawnedIndex[j] == y)
                {
                    break;
                }
                    
            }
            obstacleArray[y].SetActive(true);
            alreadySpawnedIndex[i] = y;
        }
        shouldSpawnObstacles = false;
    }

    public void DisableObstacles()
    {
        if (IsAnyObstacleSpawned)
        {
            for (int i = 0; i < obstacleArray.Length; i++)
                obstacleArray[i].SetActive(false);
        }
    }

    private bool IsAnyObstacleSpawned
    {
        get
        {
            return obstacleArray.Any(obj => obj.activeSelf);
        }
    }

    private bool shouldSpawnObstacles = false;
    private int[] alreadySpawnedIndex = new int[10];
}
