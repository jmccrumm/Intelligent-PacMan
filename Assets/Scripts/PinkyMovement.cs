using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PinkyMovement : MonoBehaviour
{
    int currentIndex;
    public float speed = 0.2f;

    protected Vector2 dest = Vector2.zero;
    protected Vector2 move = Vector2.zero;
    protected Vector2 current = Vector2.zero;
    //Vector2 prev_move = Vector2.zero;
    Vector2 pac_pos = new Vector2(10, 14);
    int numMoves = 0;

    public PacmanMoveAuto pacScript;
    public GameController gameController;


    protected bool inTransit = false;

    void FixedUpdate()
    {
        if (gameController.currentSeconds() < 1) // don't release pinky right away
            return;

        // recalculate next position of pacman when pinky gets close enough to previous position or makes too many consecutive moves
        if (numMoves > 10 || (System.Math.Abs(transform.position.x - pac_pos.x) < .25
                && System.Math.Abs(transform.position.y - pac_pos.y) < .25))
        {
            pac_pos = wheresPacman();
        }

        if (!inTransit)
        { // wait until ghost hits a decision point before calculating next move

            move = next();
            inTransit = true;
            numMoves++;
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

    Vector2 next()
    {
        Vector2 next_move = transform.position;
        Vector2 pac_position = pac_pos;
        //Vector2 ghost_position = gameController.decPoints[currentIndex].location;
        float minn = 1000;
        //Check valid spaces and calculate distances
        if (gameController.decPoints[currentIndex].up != null)
        {	//can move up. get distance
            Vector2 newUp = gameController.decPoints[currentIndex].up.location;

            float distanceUp = Vector2.Distance(newUp, pac_position);
            if (distanceUp < minn)
            {
                minn = distanceUp;
                next_move = newUp;
            }
        }
        if (gameController.decPoints[currentIndex].down != null)
        {	//can move down. get distance
            Vector2 newDown = gameController.decPoints[currentIndex].down.location;

            float distanceDown = Vector2.Distance(newDown, pac_position);
            if (distanceDown < minn)
            {
                minn = distanceDown;
                next_move = newDown;
            }
        }
        if (gameController.decPoints[currentIndex].right != null)
        {   //can move right. get distance
            Vector2 newRight = gameController.decPoints[currentIndex].right.location;

            float distanceRight = Vector2.Distance(newRight, pac_position);
            if (distanceRight < minn)
            {
                minn = distanceRight;
                next_move = newRight;
            }
        }
        if (gameController.decPoints[currentIndex].left != null)
        { //can move left. get distance
            Vector2 newLeft = gameController.decPoints[currentIndex].left.location;

            float distanceLeft = Vector2.Distance(newLeft, pac_position);
            if (distanceLeft < minn)
            {
                minn = distanceLeft;
                next_move = newLeft;
            }
        }

        return next_move;
    }

    Vector2 wheresPacman()
    {
        Vector2 pac_position = GameObject.Find("pacman").transform.position;
        // find decPoint pacman is going towards
        Vector2 pac_dest = pacScript._dest;
        Vector2 pac_dir = pac_dest - pac_position;
        if (pac_dir.x == 0 && pac_dir.y == 0) return pac_position;
        pac_dir = vectorRound(pac_dir);
        while (!isDecPoint(pac_dest))
        {
            pac_dest += pac_dir;
        }

        return pac_dest;
    }

    bool isDecPoint(Vector2 point)
    {
        for (int i = 0; i < gameController.decPoints.Length; i++)
        {
            if (gameController.decPoints[i].location == point)
            {
                return true;
            }
        }
        return false;
    }

    Vector2 vectorRound(Vector2 vect)
    {
        if (vect.x < 0)
            return Vector2.left;
        else if (vect.x > 0)
            return Vector2.right;
        else if (vect.y < 0)
            return Vector2.down;
        else if (vect.y > 0)
            return Vector2.up;
        else
            return Vector2.zero;
    }

    protected void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "pacman") // pacman caught
        {
            gameController.restartGame();
        }
    }
}
