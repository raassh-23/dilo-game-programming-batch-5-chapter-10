using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject squarePrefab;

    [SerializeField]
    private int minSquares = 5;
    [SerializeField]
    private int maxSquares = 10;

    private Bounds spawnArea;

    private List<GameObject> squaresPool = new List<GameObject>();

    private void Start() {
        spawnArea = GetComponent<SpriteRenderer>().bounds;

        int spawnCount = Random.Range(minSquares, maxSquares);

        for (int i = 0; i < spawnCount; i++) {
            Vector2 spawnPosition = GetRandomPosition();
            SpawnSquare(spawnPosition);
        }
    }

    private Vector2 GetRandomPosition() {
        float x = Random.Range(spawnArea.min.x, spawnArea.max.x);
        float y = Random.Range(spawnArea.min.y, spawnArea.max.y);

        return new Vector2(x, y);
    }

    private GameObject getFromPool() {
        if (squaresPool.Count == 0) {
            return Instantiate(squarePrefab);
        } else {
            GameObject square = squaresPool[0];
            squaresPool.RemoveAt(0);
            return square;
        }
    }

    private void SpawnSquare(Vector2 position) {
        GameObject square = getFromPool();
        square.transform.position = position;
        square.SetActive(true);
    }
}
