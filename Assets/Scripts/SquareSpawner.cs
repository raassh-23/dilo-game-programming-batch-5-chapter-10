using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SquareSpawner : MonoBehaviour
{
    private static SquareSpawner _instance = null;

    public static SquareSpawner Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SquareSpawner>();

                if (_instance == null)
                {
                    Debug.LogError("Fatal Error: SquareSpawner not Found");
                }
            }

            return _instance;
        }
    }

    [SerializeField]
    private SquareConfig[] squareConfigs;

    [SerializeField]
    private SquareConfig[] powerUpConfigs;

    [SerializeField]
    private int minSquares = 5;

    [SerializeField]
    private int maxSquares = 10;

    [SerializeField]
    private float respawnDelay = 3f;

    [SerializeField]
    private int minPowerUpTime = 5;

    [SerializeField]
    private int maxPowerUpTime = 10;

    [SerializeField]
    private Transform ball;

    [SerializeField]
    private float minDistToBall = 0.3f;

    private Bounds spawnArea;

    private Dictionary<string, List<Square>> squaresPool;

    private float SquareTotalWeight;
    private float PowerUpTotalWeight;

    private void Start()
    {
        squaresPool = new Dictionary<string, List<Square>>();
        spawnArea = GetComponent<SpriteRenderer>().bounds;

        SquareTotalWeight = 0f;
        foreach (SquareConfig config in squareConfigs)
        {
            squaresPool.Add(config.squarePrefab.name, new List<Square>());
            SquareTotalWeight += config.probWeight;
        }

        PowerUpTotalWeight = 0f;
        foreach (SquareConfig config in powerUpConfigs)
        {
            squaresPool.Add(config.squarePrefab.name, new List<Square>());
            PowerUpTotalWeight += config.probWeight;
        }

        int spawnCount = Random.Range(minSquares, maxSquares);

        for (int i = 0; i < spawnCount; i++)
        {
            SpawnSquareAtRandomPosition();
        }

        Invoke("SpawnPowerUpAtRandomPosition", Random.Range(minPowerUpTime, maxPowerUpTime));
    }

    private Vector2 GetRandomPosition()
    {
        Vector2 position;

        do
        {
            float x = Random.Range(spawnArea.min.x, spawnArea.max.x);
            float y = Random.Range(spawnArea.min.y, spawnArea.max.y);

            position = new Vector2(x, y);
        } while (Vector2.Distance(ball.position, position) < minDistToBall);

        return position;
    }

    private Square getFromPool(int configIndex, SquareConfig[] configs)
    {
        Square square;
        string name = configs[configIndex].squarePrefab.name;

        if (squaresPool[name].Count == 0)
        {
            GameObject newSquare = Instantiate(configs[configIndex].squarePrefab);
            newSquare.name = newSquare.name.Replace("(Clone)", "");
            square = newSquare.GetComponent<Square>();
        }
        else
        {
            square = squaresPool[name][0];
            squaresPool[name].RemoveAt(0);
        }

        return square;
    }

    private int getRandomConfigIndex(SquareConfig[] configs, float totalWeigth)
    {
        float randomValue = Random.Range(0f, totalWeigth);
        float currentWeight = 0f;

        int length = configs.Length;

        for (int i = 0; i < length; i++)
        {
            currentWeight += configs[i].probWeight;

            if (randomValue <= currentWeight)
            {
                return i;
            }
        }

        return length - 1;
    }

    private void Spawn(Vector2 position, string type)
    {
        if (GameManager.Instance.IsGameOver)
        {
            return;
        }

        Square square;
        if (type == "powerup")
        {
            int randomSquare = getRandomConfigIndex(powerUpConfigs, PowerUpTotalWeight);
            square = getFromPool(randomSquare, powerUpConfigs);
        }
        else
        {
            int randomSquare = getRandomConfigIndex(squareConfigs, SquareTotalWeight);
            square = getFromPool(randomSquare, squareConfigs);
        }

        square.Activate(position);
    }

    private void SpawnSquareAtRandomPosition()
    {
        Vector2 spawnPosition = GetRandomPosition();

        Spawn(spawnPosition, "square");
    }

    private void SpawnPowerUpAtRandomPosition()
    {
        Vector2 spawnPosition = GetRandomPosition();

        Spawn(spawnPosition, "powerup");

        Invoke("SpawnPowerUpAtRandomPosition", Random.Range(minPowerUpTime, maxPowerUpTime));
    }

    public void ReturnToPool(Square square)
    {
        squaresPool[square.gameObject.name].Add(square);
        
        if (!(square is IPowerUp))
        {
            Invoke("SpawnSquareAtRandomPosition", respawnDelay);
        }
    }
}

[System.Serializable]
public struct SquareConfig
{
    public GameObject squarePrefab;
    public float probWeight;
}
