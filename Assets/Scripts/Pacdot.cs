using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pacdot : MonoBehaviour {

    public GameController gameController;

    void Start()
    {
        gameController.UpdateScore();
    }

    void OnTriggerEnter2D(Collider2D co)
    {
        if (co.name == "pacman")
        {
            Destroy(gameObject);
            gameController.score++;
            gameController.UpdateScore();
        }
    }

}
