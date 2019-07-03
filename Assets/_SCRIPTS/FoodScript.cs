using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Tail")
        {
            GameManager.gm.SpawnFood();
            Destroy(gameObject);
        }
    }
}
