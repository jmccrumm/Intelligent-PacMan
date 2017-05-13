using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PacmanMoveAuto : MonoBehaviour
{
    public float speed = 0.4f;
    public int pacStrength = 10;
    public Vector2 _dest = new Vector2(14, 14);
    Vector2 prev_dest;
    Vector2 _dir = Vector2.zero;
    float FEAR_DIST = 3;
    //bool hitWall = false;
    bool isStuck = false;
    bool inTransit = false;

    public GameController gameController;
    //public BlinkyMovement blinkyScript;

    void Start()
    {
        _dest = transform.position;
        GetComponent<Animator>().SetFloat("DirX", 1);
        GetComponent<Animator>().SetFloat("DirY", 0);
    }

    void FixedUpdate()
    {

        //Vector2 _current = _dest;
        if ((Vector2)transform.position == _dest)
        {
            prev_dest = _dest;
            _dest = next();
            if (prev_dest == _dest)
                isStuck = true;
        }
        Vector2 test = Vector2.MoveTowards(transform.position, _dest, speed);
        GetComponent<Rigidbody2D>().MovePosition(test);

        for (int i = 0; i < gameController.decPoints.Length; i++)
        {
            if (System.Math.Abs(transform.position.x - gameController.decPoints[i].location.x) < .25
                && System.Math.Abs(transform.position.y - gameController.decPoints[i].location.y) < .25)
            {
                inTransit = false;
            }
        }
        Animate();
    }

    void Animate()
    {
        Vector2 dir = _dest - (Vector2)transform.position;
        if (transform.position.x == 14 && transform.position.y == 14)
        {
            GetComponent<Animator>().SetFloat("DirX", 1);
            GetComponent<Animator>().SetFloat("DirY", 0);
        }
        else 
        {
            GetComponent<Animator>().SetFloat("DirX", dir.x);
            GetComponent<Animator>().SetFloat("DirY", dir.y);
        }
    }

    protected bool valid(Vector2 dir)
    {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
        return (hit.collider == GetComponent<Collider2D>());
    }

    protected float distPacDot(Vector2 direction)
    {
        Vector2 pos = transform.position;
        direction = new Vector2(direction.x * 0.25f, direction.y * 0.25f);
        RaycastHit2D hit = Physics2D.Linecast(pos, pos + direction);

        Vector2 start = Vector2.zero;
        while (hit.collider.tag == "Player")
        {
            start += new Vector2(direction.x * 1.2f, direction.y * 1.2f);
            hit = Physics2D.Linecast(pos + start, pos + direction);
        }

        if (hit.collider.tag == "pacdot")
        {
            return Vector2.Distance((Vector2)hit.transform.position, pos);
        }
        else
            return 100;
    }
    
    bool Valid(Vector2 direction)
    {
        // cast line from 'next to pacman' to pacman
        // not from directly the center of next tile but just a little further from center of next tile
        Vector2 pos = transform.position;
        direction += new Vector2(direction.x * 0.25f, direction.y * 0.25f);
        RaycastHit2D hit = Physics2D.Linecast(pos + direction, pos);
        return hit.collider.tag == "pacdot" || (hit.collider == GetComponent<Collider2D>());
    }
    

    public void ResetDestination()
    {
        GetComponent<Animator>().SetFloat("DirX", 1);
        GetComponent<Animator>().SetFloat("DirY", 0);
        _dest = new Vector2(14, 14);
        _dir = Vector2.zero;
        //_nextDir = Vector2.zero;
        Animate();
        //transform.position.Set(15, 11, 0);
    }

    public Vector2 getDir()
    {
        return _dir;
    }

    Vector2 closestGhost()
    {
        GameObject[] allGhosts = GameObject.FindGameObjectsWithTag("ghost");
        Vector2 closest = allGhosts[0].transform.position;
        float minDistance = 100;
        for (int i = 0; i < allGhosts.Length; i++)
        {
            float distance = Vector2.Distance(transform.position, allGhosts[i].transform.position);
            if (distance < minDistance)
            {
                closest = allGhosts[i].transform.position;
                minDistance = distance;
            }
        }

        return closest;
    }

    Vector2 randomDot()
    {
        GameObject[] allDots = GameObject.FindGameObjectsWithTag("pacdot");
        return allDots[Random.Range(0, allDots.Length- 1)].transform.position;
        /*
        Vector2 closest = allDots[0].transform.position;
        float minDistance = 100;
        for (int i = 0; i < allDots.Length; i++)
        {
            float distance = Vector2.Distance(transform.position, allDots[i].transform.position);
            if (distance < minDistance)
            {
                closest = allDots[i].transform.position;
                minDistance = distance;
            }
        }

        return closest;
        */
    }

    Vector2 next()
    {
        if (inTransit)
            return _dest + _dir;
        //Debug.Log ("test");
        Vector2 next_move = transform.position;
        //Vector2 ghost_position = GameObject.Find("inky").transform.position;
        Vector2 ghost_position = closestGhost();
        float distanceUp = 0, distanceDown = 0, distanceLeft = 0, distanceRight = 0;
        Vector2 newUp = Vector2.zero, newDown = Vector2.zero, newLeft = Vector2.zero, newRight = Vector2.zero;
        Vector2 farthest = transform.position;
        float min = 100;
        float max = Vector2.Distance(next_move, ghost_position);

        bool toPacDot = false;
        Vector2 nearest = transform.position;
        Vector2 dot_dest = transform.position;

        List<Vector2> dirProb = new List<Vector2>();
        //if pacman is certain distance away from closest ghost, don't worry about running away. Focus on pacdots
        if (max > FEAR_DIST)
        {
            Vector2 dir = (Vector2)transform.position + Vector2.left; // start with assuming closest dot is to the left
            float closestDot = distPacDot(Vector2.left);

            if (distPacDot(Vector2.right) < closestDot) { // now closest dot is right
                dir = (Vector2)transform.position + Vector2.right;
                closestDot = distPacDot(Vector2.right);
            }
            if (distPacDot(Vector2.up) < closestDot) { // ...up
                dir = (Vector2)transform.position + Vector2.up;
                closestDot = distPacDot(Vector2.up);
            }
            if (distPacDot(Vector2.down) < closestDot) { // ...down
                dir = (Vector2)transform.position + Vector2.down;
                closestDot = distPacDot(Vector2.down);
            }
            _dir = dir;
            if (closestDot != 100)
                return dir;
            else if (isStuck && !inTransit)// no dots in sight, can't move
            {
                nearest = randomDot();
                toPacDot = true;
                
                // pick random valid direction
                List<Vector2> randDir = new List<Vector2>();
                if (Valid(Vector2.up))
                    randDir.Add((Vector2)transform.position + Vector2.up);
                if (Valid(Vector2.down))
                    randDir.Add((Vector2)transform.position + Vector2.down);
                if (Valid(Vector2.left))
                    randDir.Add((Vector2)transform.position + Vector2.left);
                if (Valid(Vector2.right))
                    randDir.Add((Vector2)transform.position + Vector2.right);

                //hitWall = false;
                isStuck = false;
                inTransit = true;
                //return randDir[Random.Range(0, randDir.Count - 1)];
                dot_dest = nearest;
                
                
            }

        }

        //Check valid spaces and calculate distances

        if (Valid(Vector2.up))
        {	//can move up. get distance
            newUp = (Vector2)transform.position + Vector2.up;
            distanceUp = Vector2.Distance(newUp, ghost_position);
            dirProb.Add(newUp);
            
            if (distanceUp > max) { farthest = newUp; max = distanceUp; _dest = newUp; }
            if (toPacDot)
            {
                distanceUp = Vector2.Distance(newUp, nearest);
                if (distanceUp < min) { min = distanceUp; dot_dest = newUp; }
            }
        }
        if (Valid(Vector2.down))
        {	//can move down. get distance
            newDown = (Vector2)transform.position + Vector2.down;
            distanceDown = Vector2.Distance(newDown, ghost_position);
            dirProb.Add(newDown);
            //if (distanceDown < min) { min = distanceDown; }
            if (distanceDown > max) { farthest = newDown;  max = distanceDown; _dest = newDown; }
            if (toPacDot)
            {
                distanceDown = Vector2.Distance(newDown, nearest);
                if (distanceDown < min) { min = distanceDown; dot_dest = newDown; }
            }
        }
        if (Valid(Vector2.right))
        {   //can move right. get distance
            newRight = (Vector2)transform.position + Vector2.right;
            distanceRight = Vector2.Distance(newRight, ghost_position);
            dirProb.Add(newRight);
            //if (distanceRight < min) { min = distanceRight; }
            if (distanceRight > max) { farthest = newRight; max = distanceRight; _dest = newRight; }
            if (toPacDot)
            {
                distanceRight = Vector2.Distance(newRight, nearest);
                if (distanceRight < min) { min = distanceRight; dot_dest = newRight; }
            }
        }
        if (Valid(Vector2.left))
        { //can move left. get distance
            newLeft = (Vector2)transform.position + Vector2.left;
            distanceLeft = Vector2.Distance(newLeft, ghost_position);
            dirProb.Add(newLeft);
            //if (distanceLeft < min) { min = distanceLeft; }
            if (distanceLeft > max) { farthest = newLeft; max = distanceLeft; _dest = newLeft; }
            if (toPacDot)
            {
                distanceLeft = Vector2.Distance(newLeft, nearest);
                if (distanceLeft < min) { min = distanceLeft; dot_dest = newLeft; }
            }
        }

        /*
        // duplicate shorter distances to make more likely
        for (int i = 0; i < pacStrength; i++)
            dirProb.Add(farthest);

        // randomly select next move with shorter more likely
        Vector2 select = dirProb[Random.Range(0, dirProb.Count - 1)];
        next_move = select;
        */
        if (toPacDot)
            next_move = dot_dest;
        else
            next_move = farthest;

        _dir = next_move - (Vector2)transform.position;
        return next_move;
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

}
