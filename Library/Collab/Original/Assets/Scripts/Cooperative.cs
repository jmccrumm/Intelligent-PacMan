using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Cooperative : MonoBehaviour
{
    int currentIndex;
    public float speed = 0.2f;

    public int ghostStrength = 4;

    protected Vector2 dest = Vector2.zero;
    protected Vector2 move = Vector2.zero;
    protected Vector2 current = Vector2.zero;
    //Vector2 pac_pos = new Vector2(10, 14);

    public PacmanMove pacScript;
    public GameController gameController;

    protected bool inTransit = false;

    void Start()
    {
        for (int i = 0; i < gameController.decPoints.Length; i++)
        {
            if (gameController.decPoints[i].location == (Vector2)transform.position)
            {
                currentIndex = i;
            }
        }
    }

    void FixedUpdate()
	{
		if(gameController.currentSeconds() < 4) // don't release blinky right away
			return;


		if (!inTransit) { // wait until ghost hits a decision point before calculating next move
            move = next();
            inTransit = true;
		}

		dest = move;
		Vector2 test = Vector2.MoveTowards(transform.position, dest, speed);
		GetComponent<Rigidbody2D>().MovePosition(test);

		// check array of decision points to see if ghost is at one
		for (int i = 0; i < gameController.decPoints.Length; i++)
		{
			if (System.Math.Abs(transform.position.x - gameController.decPoints[i].location.x) < .25 
                && System.Math.Abs(transform.position.y - gameController.decPoints[i].location.y) < .25)
			{
				// made it to next decision point, now free to make next move
				if (current != gameController.decPoints[i].location)
				{
					current = gameController.decPoints[i].location;
                    currentIndex = i;
					transform.position = gameController.decPoints[i].location;
					inTransit = false;
				}
			}
		}

		//Animation
		Vector2 dir = dest - (Vector2)transform.position;
		GetComponent<Animator>().SetFloat("DirX", dir.x);
		GetComponent<Animator>().SetFloat("DirY", dir.y);
	}

	bool valid(Vector2 dir)
	{
		Vector2 pos = transform.position;
		RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
		return hit.collider.tag == "pacdot" || (hit.collider == GetComponent<Collider2D>());
	}

	bool searchArray(int value, int[] array)
	{
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] == value)
			{
				return true;
			}
		}
		return false;
	}


	Vector2 next()
	{
		//Debug.Log ("test");
		Vector2 next_move = transform.position;
		Vector2 pac_position = GameObject.Find("pacman").transform.position;
        Vector2 ghost_position = gameController.decPoints[currentIndex].location;
        float distanceUp = 0, distanceDown = 0, distanceLeft = 0, distanceRight = 0;
        Vector2 newUp = Vector2.zero, newDown = Vector2.zero, newLeft = Vector2.zero, newRight = Vector2.zero;
        Vector2 shortest = Vector2.zero;
        float min = 100;
        float max = 0;

        //List<float> weights = new List<float>();
        List<Vector2> dirProb = new List<Vector2>();
        //Check valid spaces and calculate distances
        if (gameController.decPoints[currentIndex].up != null)
        {	//can move up. get distance
            newUp = gameController.decPoints[currentIndex].up.location;
            distanceUp = Vector2.Distance(newUp, pac_position);
            dirProb.Add(newUp);
            if (distanceUp < min) { shortest = newUp; min = distanceUp; }
            if (distanceUp > max) { max = distanceUp; }
        }
        if (gameController.decPoints[currentIndex].down != null)
        {	//can move down. get distance
            newDown = gameController.decPoints[currentIndex].down.location;
            distanceDown = Vector2.Distance(newDown, pac_position);
            dirProb.Add(newDown);
            if (distanceDown < min) { shortest = newDown; min = distanceDown; }
            if (distanceDown > max) { max = distanceDown; }
        }
        if (gameController.decPoints[currentIndex].right != null)
        {   //can move right. get distance
            newRight = gameController.decPoints[currentIndex].right.location;
            distanceRight = Vector2.Distance(newRight, pac_position);
            dirProb.Add(newRight);
            if (distanceRight < min) { shortest = newRight; min = distanceRight; }
            if (distanceRight > max) { max = distanceRight; }
        }
        if (gameController.decPoints[currentIndex].left != null)
        { //can move left. get distance
            newLeft = gameController.decPoints[currentIndex].left.location;
            distanceLeft = Vector2.Distance(newLeft, pac_position);
            dirProb.Add(newLeft);
            if (distanceLeft < min) { shortest = newLeft; min = distanceLeft; }
            if (distanceLeft > max) { max = distanceLeft; }
        }

        // duplicate shorter distances to make more likely
        for (int i = 0; i < ghostStrength; i++)
            dirProb.Add(shortest);

        /*
        max++;
        for (int i = 0; i < max; i++)
        {
            if (distanceLeft != 0 && (max - distanceLeft) < i)
            {
                int insertions = (int)max - 3*i;
                while (insertions > 0)
                {
                    dirProb.Add(newLeft);
                    insertions--;
                }
            }
            if (distanceRight != 0 && (max - distanceRight) < i)
            {
                int insertions = (int)max - 3*i;
                while (insertions > 0)
                {
                    dirProb.Add(newRight);
                    insertions--;
                }
            }
            if (distanceUp != 0 && (max - distanceUp) < i)
            {
                int insertions = (int)max - 3*i;
                while (insertions > 0)
                {
                    dirProb.Add(newUp);
                    insertions--;
                }
            }
            if (distanceDown != 0 && (max - distanceDown) < i)
            {
                int insertions = (int)max - 3*i;
                while (insertions > 0)
                {
                    dirProb.Add(newDown);
                    insertions--;
                }
            }
        }
        */

        Vector2 select = dirProb[Random.Range(0, dirProb.Count - 1)];
        next_move = select;
        
        return next_move;
    }

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.name == "pacman") // pacman caught
		{
            gameController.restartGame();
        }
	}

}
