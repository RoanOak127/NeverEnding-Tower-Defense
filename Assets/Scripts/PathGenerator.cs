using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    [SerializeField] private GameObject waypointPrefab;
    [SerializeField] private GameObject towerLocationPrefab;
    [SerializeField] private Canvas mapCanvas;
    [SerializeField] private int size = 5;
    private int[,] grid;
    public static List<GameObject> path;

    private void Awake()
    {
        grid = new int[size, size];
        path = new List<GameObject>();
        CreateAndFillGrid();
    }

    private void CreateAndFillGrid()
    {
        int x = 0, z = UnityEngine.Random.Range(0, size), r, rPrev = -1;
        path.Add(Instantiate(waypointPrefab, new Vector3(x, FindTerrainHeight(x, z), z), Quaternion.identity, transform));
        path[path.Count - 1].name = (path.Count - 1).ToString();
        grid[x, z] = 1;
        AddTowerLocations(x, z);
        // End goal is the right wall, x = size
        while (x < size - 1)
        {
            // 0-up, 1-down, 2-right
            r = UnityEngine.Random.Range(0, 3);
            while ((r == 0 && rPrev == 1) || (r == 1 && rPrev == 0))
                r = UnityEngine.Random.Range(0, 3);
            rPrev = r;
            switch (r)
            {
                case 0:
                    if (z < size - 1)
                        z++;
                    else
                        x++;
                    break;
                case 1:
                    if (z > 0)
                        z--;
                    else
                        x++;
                    break;
                case 2:
                    x++;
                    break;
            }
            path.Add(Instantiate(waypointPrefab, new Vector3(x, FindTerrainHeight(x, z), z), Quaternion.identity, transform));
            path[path.Count - 1].name = (path.Count - 1).ToString();
            grid[x, z] = 1;
            AddTowerLocations(x, z);
        }
        // Instantiate towers at allowable locations
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (grid[i, j] == 2)
                    Instantiate(towerLocationPrefab, new Vector3(i, FindTerrainHeight(i, j), j), towerLocationPrefab.transform.rotation, mapCanvas.transform);
            }
        }
    }

    private float FindTerrainHeight(int x, int z)
    {
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(x, 0, z), Vector3.down, out hit))
            return hit.point.y + 1.0f;
        return 0.2f;
    }

    private void AddTowerLocations(int x, int z)
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                int a = x + j;
                int b = z + i;
                if (a >= 0 && b >= 0 && a < size && b < size && grid[a, b] == 0)
                    grid[a, b] = 2;
            }
        }
    }
}
