using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject food;
    public Vector3 center;
    public Vector3 spawnAreaSize;

    public GameObject outline;
    public float colThickness = 4f;
    public float zPosition = 0f;
    private Vector2 screenSize;

    public static GameManager gm;

    private void Awake()
    {
        if (gm == null)
        {
            gm = this;
        }
        //else if (gm != this)
        //{
        //    Destroy(gameObject);
        //}
        //DontDestroyOnLoad(gameObject);

        SettingBorders();

        spawnAreaSize = new Vector3((screenSize.x * 2 - (colThickness*2)) - 0.5f, (screenSize.y * 2 - (colThickness * 2)) - 0.5f, colThickness);
    }

    void Start()
    {
        SpawnFood();
    }

    public void SpawnFood()
    {
        float posX = Mathf.Round(Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2));
        float posY = Mathf.Round(Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2));
        Vector3 pos = center + new Vector3(posX, posY, 1f);
        Instantiate(food, pos, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, spawnAreaSize);
    }

    void SettingBorders()
    {
        Vector3 cameraPos = Camera.main.transform.position;

        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;

        Dictionary<string, Transform> colliders = new Dictionary<string, Transform>();

        colliders.Add("Top", Instantiate(outline).transform);
        colliders.Add("Bottom", Instantiate(outline).transform);
        colliders.Add("Right", Instantiate(outline).transform);
        colliders.Add("Left", Instantiate(outline).transform);

        foreach (KeyValuePair<string, Transform> valPair in colliders)
        {
            valPair.Value.parent = transform;

            if (valPair.Key == "Left" || valPair.Key == "Right")
            {
                valPair.Value.localScale = new Vector3(colThickness, screenSize.y * 2, colThickness);
            }
            else
            {
                valPair.Value.localScale = new Vector3(screenSize.x * 2, colThickness, colThickness);
            }

        }

        colliders["Right"].position = new Vector3(cameraPos.x + screenSize.x - (colliders["Right"].localScale.x * 0.5f), cameraPos.y, zPosition);
        colliders["Left"].position = new Vector3(cameraPos.x - screenSize.x + (colliders["Left"].localScale.x * 0.5f), cameraPos.y, zPosition);
        colliders["Top"].position = new Vector3(cameraPos.x, cameraPos.y + screenSize.y - (colliders["Top"].localScale.y * 0.5f), zPosition);
        colliders["Bottom"].position = new Vector3(cameraPos.x, cameraPos.y - screenSize.y + (colliders["Bottom"].localScale.y * 0.5f), zPosition);
    }
}
