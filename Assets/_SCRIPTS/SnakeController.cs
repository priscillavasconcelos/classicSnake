using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class SnakeController : MonoBehaviour
{
    public float speed;
    public GameObject tailPrefab;
    Vector2 dir = Vector2.right;

    float timeToMoveCount, currentTimeToMove, initialTimeToMove = 0.3f;

    List<Transform> tail = new List<Transform>();

    bool ate;
    void Start()
    {
        currentTimeToMove = initialTimeToMove;
        timeToMoveCount = initialTimeToMove;
        //InvokeRepeating("Movement", 0.3f, 0.3f);
    }

    void Update()
    {
        timeToMoveCount -= Time.deltaTime;
        if (timeToMoveCount < 0)
        {
            Movement();
            timeToMoveCount = currentTimeToMove;
        }
        
        float dirX = Input.GetAxis("Horizontal");
        float dirY = Input.GetAxis("Vertical");

        if (dirX > 0)
        {
            dir = Vector2.right;
        }
        else if (dirX < 0)
        {
            dir = -Vector2.right;
        }
        else if (dirY > 0)
        {
            dir = Vector2.up;
        }
        else if (dirY < 0)
        {
            dir = -Vector2.up;
        }
    }

    void Movement()
    {
        Vector2 currentPosition = transform.position;
        transform.Translate(dir);

        if (ate)
        {
            GameObject newTail = (GameObject)Instantiate(tailPrefab, currentPosition, Quaternion.identity);
            tail.Insert(0, newTail.transform);
            ate = false;
            GameManager.gm.SpawnFood();
            currentTimeToMove -= 0.01f;
        }

        if (tail.Count > 0)
        {
            tail.Last().position = currentPosition;

            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            ate = true;

            Destroy(collision.gameObject);
        }
        else
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
