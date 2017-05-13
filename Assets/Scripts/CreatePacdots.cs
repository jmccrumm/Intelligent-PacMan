using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePacdots : MonoBehaviour
{

    public static List<Collider2D> pacdots = new List<Collider2D>();
    public Collider2D PacdotPrefab;
    public GameObject maze;


    // Use this for initialization
    void Start()
    {
        placeDots();
    }

    public void placeDots()
    {
        int x = 2;
        int y = 30;
        while (y > 1) // stop at bottom row before goes outside grid
        {
            Collider2D dotInstance = Instantiate(PacdotPrefab, new Vector2(x, y), new Quaternion(0, 0, 0, 0)) as Collider2D;
            pacdots.Add(dotInstance);
            x++;
            if (x >= 28) // go to next row
            {
                x = 2;
                y--;
            }
        }

        StartCoroutine(RemoveExcess()); // removes dots that would appear inside the walls

    }

    IEnumerator RemoveExcess()
    {
        yield return new WaitForFixedUpdate();
        // remove unnecessary dots
        Collider2D[] walls = maze.GetComponents<Collider2D>();
        pacdots.ForEach(delegate (Collider2D dot){
            for (int i = 0; i < walls.Length; i++)
            {
                if (dot != null && dot.IsTouching(walls[i]))
                { // pacdot is inside wall
                    Destroy(dot.gameObject);
                    //pacdots.Remove(dot);
                }
            }
        });

    }
}
