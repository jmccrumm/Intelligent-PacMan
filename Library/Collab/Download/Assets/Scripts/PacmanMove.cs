using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PacmanMove : MonoBehaviour
{
    public float speed = 0.4f;
    public Vector2 _dest = new Vector2(14,14);
    Vector2 _dir = Vector2.zero;
    Vector2 _nextDir = Vector2.zero;

    void Start()
    {
        _dest = transform.position;
    }

    void FixedUpdate()
    {
        ReadInputAndMove();

        if ((Vector2)transform.position == _dest)
        {
            if (Input.GetKey(KeyCode.UpArrow) && valid(Vector2.up))
                _dest = (Vector2)transform.position + Vector2.up;
            if (Input.GetKey(KeyCode.RightArrow) && valid(Vector2.right))
                _dest = (Vector2)transform.position + Vector2.right;
            if (Input.GetKey(KeyCode.DownArrow) && valid(-Vector2.up))
                _dest = (Vector2)transform.position - Vector2.up;
            if (Input.GetKey(KeyCode.LeftArrow) && valid(-Vector2.right))
                _dest = (Vector2)transform.position - Vector2.right;
        }
        
        Animate();
        
    }

    bool valid(Vector2 dir)
    {
        // Cast Line from 'next to Pac-Man' to 'Pac-Man'
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
        return (hit.collider == GetComponent<Collider2D>());
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


    bool Valid(Vector2 direction)
    {
        // cast line from 'next to pacman' to pacman
        // not from directly the center of next tile but just a little further from center of next tile
        Vector2 pos = transform.position;
        direction += new Vector2(direction.x * 0.25f, direction.y * 0.25f);
        RaycastHit2D hit = Physics2D.Linecast(pos + direction, pos);
        return hit.collider.name == "pacdot" || (hit.collider == GetComponent<Collider2D>());
    }

    public void ResetDestination()
    {
        //GetComponent<Animator>().SetFloat("DirX", 1);
        //GetComponent<Animator>().SetFloat("DirY", 0);
        _dest = new Vector2(14,14);
        _dir = Vector2.zero;
        _nextDir = Vector2.zero;
        Animate();
        //transform.position.Set(15, 11, 0);
    }

    void ReadInputAndMove()
    {
        // move closer to destination
        Vector2 p = Vector2.MoveTowards(transform.position, _dest, speed);
        GetComponent<Rigidbody2D>().MovePosition(p);

        // get the next direction from keyboard
        if (Input.GetAxis("Vertical") > 0) _nextDir = Vector2.up;
        if (Input.GetAxis("Vertical") < 0) _nextDir = -Vector2.up;
        if (Input.GetAxis("Horizontal") > 0) _nextDir = Vector2.right;
        if (Input.GetAxis("Horizontal") < 0) _nextDir = -Vector2.right;


        // if pacman is in the center of a tile
        if (Vector2.Distance(_dest, transform.position) < 0.00001f)
        {
            if (Valid(_nextDir))
            {
                _dest = (Vector2)transform.position + _nextDir;
                _dir = _nextDir;
            }
            else   // if next direction is not valid
            {
                if (Valid(_dir))  // and the prev. direction is valid
                    _dest = (Vector2)transform.position + _dir;   // continue on that direction

                // otherwise, do nothing
            }
        }
    }

    public Vector2 getDir()
    {
        return _dir;
    }
}
