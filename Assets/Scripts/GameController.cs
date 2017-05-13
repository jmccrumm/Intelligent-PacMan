using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public int score;
    public Text scoreText;
    float timer = 0.0f;
    public point[] decPoints = new point[82];

    public BlinkyMovement blinkyScript;
    public PinkyMovement pinkyScript;
    public PacmanMove pacScript;
    public CreatePacdots maze;

    public class point
    {
        public Vector2 location;
        public point up;
        public point down;
        public point right;
        public point left;
    }

    // Use this for initialization
    void Awake()
    {
        for (int i = 0; i < decPoints.Length; i++)
        {
            decPoints[i] = new point();
        }
        decPoints[0].location = new Vector2(2, 30);
        decPoints[1].location = new Vector2(7, 30);
        decPoints[2].location = new Vector2(13, 30);
        decPoints[3].location = new Vector2(16, 30);
        decPoints[4].location = new Vector2(22, 30);
        decPoints[5].location = new Vector2(27, 30);
        decPoints[6].location = new Vector2(13, 28);
        decPoints[7].location = new Vector2(16, 28);
        decPoints[8].location = new Vector2(2, 26);
        decPoints[9].location = new Vector2(7, 26);
        decPoints[10].location = new Vector2(10, 26);
        decPoints[11].location = new Vector2(13, 26);
        decPoints[12].location = new Vector2(16, 26);
        decPoints[13].location = new Vector2(19, 26);
        decPoints[14].location = new Vector2(22, 26);
        decPoints[15].location = new Vector2(27, 26);
        decPoints[16].location = new Vector2(2, 23);
        decPoints[17].location = new Vector2(4, 23);
        decPoints[18].location = new Vector2(7, 23);
        decPoints[19].location = new Vector2(10, 23);
        decPoints[20].location = new Vector2(13, 23);
        decPoints[21].location = new Vector2(16, 23);
        decPoints[22].location = new Vector2(19, 23);
        decPoints[23].location = new Vector2(22, 23);
        decPoints[24].location = new Vector2(25, 23);
        decPoints[25].location = new Vector2(27, 23);
        decPoints[26].location = new Vector2(2, 20);
        decPoints[27].location = new Vector2(4, 20);
        decPoints[28].location = new Vector2(7, 20);
        decPoints[29].location = new Vector2(10, 20);
        decPoints[30].location = new Vector2(13, 20);
        decPoints[31].location = new Vector2(16, 20);
        decPoints[32].location = new Vector2(19, 20);
        decPoints[33].location = new Vector2(22, 20);
        decPoints[34].location = new Vector2(25, 20);
        decPoints[35].location = new Vector2(27, 20);
        decPoints[36].location = new Vector2(2, 17);
        decPoints[37].location = new Vector2(7, 17);
        decPoints[38].location = new Vector2(22, 17);
        decPoints[39].location = new Vector2(27, 17);
        decPoints[40].location = new Vector2(2, 14);
        decPoints[41].location = new Vector2(4, 14);
        decPoints[42].location = new Vector2(10, 14);
        decPoints[43].location = new Vector2(19, 14);
        decPoints[44].location = new Vector2(25, 14);
        decPoints[45].location = new Vector2(27, 14);
        decPoints[46].location = new Vector2(2, 11);
        decPoints[47].location = new Vector2(4, 11);
        decPoints[48].location = new Vector2(7, 11);
        decPoints[49].location = new Vector2(10, 11);
        decPoints[50].location = new Vector2(13, 11);
        decPoints[51].location = new Vector2(16, 11);
        decPoints[52].location = new Vector2(19, 11);
        decPoints[53].location = new Vector2(22, 11);
        decPoints[54].location = new Vector2(25, 11);
        decPoints[55].location = new Vector2(27, 11);
        decPoints[56].location = new Vector2(2, 8);
        decPoints[57].location = new Vector2(4, 8);
        decPoints[58].location = new Vector2(7, 8);
        decPoints[59].location = new Vector2(10, 8);
        decPoints[60].location = new Vector2(13, 8);
        decPoints[61].location = new Vector2(16, 8);
        decPoints[62].location = new Vector2(19, 8);
        decPoints[63].location = new Vector2(22, 8);
        decPoints[64].location = new Vector2(25, 8);
        decPoints[65].location = new Vector2(27, 8);
        decPoints[66].location = new Vector2(2, 5);
        decPoints[67].location = new Vector2(4, 5);
        decPoints[68].location = new Vector2(7, 5);
        decPoints[69].location = new Vector2(10, 5);
        decPoints[70].location = new Vector2(13, 5);
        decPoints[71].location = new Vector2(16, 5);
        decPoints[72].location = new Vector2(19, 5);
        decPoints[73].location = new Vector2(22, 5);
        decPoints[74].location = new Vector2(25, 5);
        decPoints[75].location = new Vector2(27, 5);
        decPoints[76].location = new Vector2(2, 2);
        decPoints[77].location = new Vector2(7, 2);
        decPoints[78].location = new Vector2(13, 2);
        decPoints[79].location = new Vector2(16, 2);
        decPoints[80].location = new Vector2(22, 2);
        decPoints[81].location = new Vector2(27, 2);

        decPoints[0].right = decPoints[1];
        decPoints[0].down = decPoints[8];
        decPoints[1].left = decPoints[0];
        decPoints[1].right = decPoints[2];
        decPoints[1].down = decPoints[9];
        decPoints[2].left = decPoints[1];
        decPoints[2].down = decPoints[6];
        decPoints[3].right = decPoints[4];
        decPoints[3].down = decPoints[7];
        decPoints[4].left = decPoints[3];
        decPoints[4].right = decPoints[5];
        decPoints[4].down = decPoints[14];
        decPoints[5].left = decPoints[4];
        decPoints[5].down = decPoints[15];
        decPoints[6].right = decPoints[7];
        decPoints[6].up = decPoints[2];
        decPoints[6].down = decPoints[11];
        decPoints[7].left = decPoints[6];
        decPoints[7].up = decPoints[3];
        decPoints[7].down = decPoints[12];
        decPoints[8].right = decPoints[9];
        decPoints[8].up = decPoints[0];
        decPoints[8].down = decPoints[16];
        decPoints[9].left = decPoints[8];
        decPoints[9].right = decPoints[10];
        decPoints[9].up = decPoints[1];
        decPoints[9].down = decPoints[18];
        decPoints[10].left = decPoints[9];
        decPoints[10].right = decPoints[11];
        decPoints[10].down = decPoints[19];
        decPoints[11].left = decPoints[10];
        decPoints[11].up = decPoints[6];
        decPoints[12].right = decPoints[13];
        decPoints[12].up = decPoints[7];
        decPoints[13].left = decPoints[12];
        decPoints[13].right = decPoints[14];
        decPoints[13].down = decPoints[22];
        decPoints[14].left = decPoints[13];
        decPoints[14].right = decPoints[15];
        decPoints[14].up = decPoints[4];
        decPoints[14].down = decPoints[23];
        decPoints[15].left = decPoints[14];
        decPoints[15].up = decPoints[5];
        decPoints[15].down = decPoints[25];
        decPoints[16].right = decPoints[17];
        decPoints[16].up = decPoints[8];
        decPoints[17].left = decPoints[16];
        decPoints[17].right = decPoints[18];
        decPoints[17].down = decPoints[27];
        decPoints[18].left = decPoints[17];
        decPoints[18].up = decPoints[9];
        decPoints[18].down = decPoints[28];
        decPoints[19].right = decPoints[20];
        decPoints[19].up = decPoints[10];
        decPoints[20].left = decPoints[19];
        decPoints[20].down = decPoints[30];
        decPoints[21].right = decPoints[22];
        decPoints[21].down = decPoints[31];
        decPoints[22].left = decPoints[21];
        decPoints[22].up = decPoints[13];
        decPoints[23].right = decPoints[24];
        decPoints[23].up = decPoints[14];
        decPoints[23].down = decPoints[33];
        decPoints[24].left = decPoints[23];
        decPoints[24].right = decPoints[25];
        decPoints[24].down = decPoints[34];
        decPoints[25].left = decPoints[24];
        decPoints[25].up = decPoints[15];
        decPoints[26].right = decPoints[27];
        decPoints[26].down = decPoints[36];
        decPoints[27].left = decPoints[26];
        decPoints[27].up = decPoints[17];
        decPoints[28].right = decPoints[29];
        decPoints[28].up = decPoints[18];
        decPoints[28].down = decPoints[37];
        decPoints[29].left = decPoints[28];
        decPoints[29].right = decPoints[30];
        decPoints[29].down = decPoints[42];
        decPoints[30].left = decPoints[29];
        decPoints[30].right = decPoints[31];
        decPoints[30].up = decPoints[20];
        decPoints[31].left = decPoints[30];
        decPoints[31].right = decPoints[32];
        decPoints[31].up = decPoints[21];
        decPoints[32].left = decPoints[31];
        decPoints[32].right = decPoints[33];
        decPoints[32].down = decPoints[43];
        decPoints[33].left = decPoints[32];
        decPoints[33].up = decPoints[23];
        decPoints[33].down = decPoints[38];
        decPoints[34].right = decPoints[35];
        decPoints[34].up = decPoints[24];
        decPoints[35].left = decPoints[34];
        decPoints[35].down = decPoints[39];
        decPoints[36].right = decPoints[37];
        decPoints[36].up = decPoints[26];
        decPoints[36].down = decPoints[40];
        decPoints[37].left = decPoints[36];
        decPoints[37].up = decPoints[28];
        decPoints[37].down = decPoints[48];
        decPoints[38].right = decPoints[39];
        decPoints[38].up = decPoints[33];
        decPoints[38].down = decPoints[53];
        decPoints[39].left = decPoints[38];
        decPoints[39].up = decPoints[35];
        decPoints[39].down = decPoints[45];
        decPoints[40].right = decPoints[41];
        decPoints[40].up = decPoints[36];
        decPoints[41].left = decPoints[40];
        decPoints[41].down = decPoints[47];
        decPoints[42].right = decPoints[43];
        decPoints[42].up = decPoints[29];
        decPoints[42].down = decPoints[49];
        decPoints[43].left = decPoints[42];
        decPoints[43].up = decPoints[32];
        decPoints[43].down = decPoints[52];
        decPoints[44].right = decPoints[45];
        decPoints[44].down = decPoints[54];
        decPoints[45].left = decPoints[44];
        decPoints[45].up = decPoints[39];
        decPoints[46].right = decPoints[47];
        decPoints[46].down = decPoints[56];
        decPoints[47].left = decPoints[46];
        decPoints[47].right = decPoints[48];
        decPoints[47].up = decPoints[41];
        decPoints[48].left = decPoints[47];
        decPoints[48].right = decPoints[49];
        decPoints[48].up = decPoints[37];
        decPoints[48].down = decPoints[58];
        decPoints[49].left = decPoints[48];
        decPoints[49].right = decPoints[50];
        decPoints[49].up = decPoints[42];
        decPoints[50].left = decPoints[49];
        decPoints[50].right = decPoints[51];
        decPoints[50].down = decPoints[60];
        decPoints[51].left = decPoints[50];
        decPoints[51].right = decPoints[52];
        decPoints[51].down = decPoints[61];
        decPoints[52].left = decPoints[51];
        decPoints[52].right = decPoints[53];
        decPoints[52].up = decPoints[43];
        decPoints[53].left = decPoints[52];
        decPoints[53].right = decPoints[54];
        decPoints[53].up = decPoints[38];
        decPoints[53].down = decPoints[63];
        decPoints[54].left = decPoints[53];
        decPoints[54].right = decPoints[55];
        decPoints[54].up = decPoints[44];
        decPoints[55].left = decPoints[54];
        decPoints[55].down = decPoints[65];
        decPoints[56].right = decPoints[57];
        decPoints[56].up = decPoints[46];
        decPoints[57].left = decPoints[56];
        decPoints[57].down = decPoints[67];
        decPoints[58].right = decPoints[59];
        decPoints[58].up = decPoints[48];
        decPoints[58].down = decPoints[68];
        decPoints[59].left = decPoints[58];
        decPoints[59].right = decPoints[60];
        decPoints[59].down = decPoints[69];
        decPoints[60].left = decPoints[59];
        decPoints[60].up = decPoints[50];
        decPoints[61].right = decPoints[62];
        decPoints[61].up = decPoints[51];
        decPoints[62].left = decPoints[61];
        decPoints[62].right = decPoints[63];
        decPoints[62].down = decPoints[72];
        decPoints[63].left = decPoints[62];
        decPoints[63].up = decPoints[53];
        decPoints[63].down = decPoints[73];
        decPoints[64].right = decPoints[65];
        decPoints[64].down = decPoints[74];
        decPoints[65].left = decPoints[64];
        decPoints[65].up = decPoints[55];
        decPoints[66].right = decPoints[67];
        decPoints[66].down = decPoints[76];
        decPoints[67].left = decPoints[66];
        decPoints[67].right = decPoints[68];
        decPoints[67].up = decPoints[57];
        decPoints[68].left = decPoints[67];
        decPoints[68].up = decPoints[58];
        decPoints[68].down = decPoints[77];
        decPoints[69].right = decPoints[70];
        decPoints[69].up = decPoints[59];
        decPoints[70].left = decPoints[69];
        decPoints[70].down = decPoints[78];
        decPoints[71].right = decPoints[72];
        decPoints[71].down = decPoints[79];
        decPoints[72].left = decPoints[71];
        decPoints[72].up = decPoints[62];
        decPoints[73].right = decPoints[74];
        decPoints[73].up = decPoints[63];
        decPoints[73].down = decPoints[80];
        decPoints[74].left = decPoints[73];
        decPoints[74].right = decPoints[75];
        decPoints[74].up = decPoints[64];
        decPoints[75].left = decPoints[74];
        decPoints[75].down = decPoints[81];
        decPoints[76].right = decPoints[77];
        decPoints[76].up = decPoints[66];
        decPoints[77].left = decPoints[76];
        decPoints[77].right = decPoints[78];
        decPoints[77].up = decPoints[68];
        decPoints[78].left = decPoints[77];
        decPoints[78].right = decPoints[79];
        decPoints[78].up = decPoints[70];
        decPoints[79].left = decPoints[78];
        decPoints[79].right = decPoints[80];
        decPoints[79].up = decPoints[71];
        decPoints[80].left = decPoints[79];
        decPoints[80].right = decPoints[81];
        decPoints[80].up = decPoints[73];
        decPoints[81].left = decPoints[80];
        decPoints[81].up = decPoints[75];
    }

    public void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    void Update()
    {
        timer += Time.deltaTime;
    }

    public float currentSeconds()
    {
        return timer;
    }

    public void resetTimer()
    {
        timer = 0.0f;
    }

    public void restartGame()
    {
        pacScript.transform.position = new Vector3(14, 14, 0);
        pacScript.ResetDestination();

        blinkyScript.transform.position = new Vector3(16, 20, 0);
        blinkyScript.GetComponent<Animator>().SetFloat("DirX", 1);
        blinkyScript.GetComponent<Animator>().SetFloat("DirY", 0);

        pinkyScript.GetComponent<Animator>().SetFloat("DirX", 1);
        pinkyScript.GetComponent<Animator>().SetFloat("DirY", 0);
        pinkyScript.transform.position = new Vector3(16, 20, 0);

        resetTimer();
        print(score);
        //score = 0;
        UpdateScore();

        //maze.placeDots();

        // reload scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
