using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tetramino : MonoBehaviour {

    float lastMoveDown = 0;

    public static float speed = 1.0f;
    public static bool paused = false;
    public string highScoreText;
    public int highScore=0;

	// Use this for initialization
	void Start () {
        //PlayerPrefs.SetInt("HighScore", 0);
        Time.timeScale = 1;
        if (!IsInGrid()) {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.gameOver);
            

            Invoke("OpenGameOverScene", .5f);
        }

        getHighScore();

        //Debug.Log("Time.timeScale="+Time.timeScale);

        InvokeRepeating("IncreaseSpeed", 2.0f,2.0f);
	}

    void getHighScore() {
        var textUIComp = GameObject.Find("HighScore").GetComponent<Text>();
        textUIComp.text = PlayerPrefs.GetInt("HighScore").ToString();
        Debug.Log("HighScore=" + textUIComp.text);
        highScore = int.Parse(textUIComp.text);
    }

    void OpenGameOverScene() {
        var textUIComp = GameObject.Find("Score").GetComponent<Text>();
        int score = int.Parse(textUIComp.text);
        if (score > highScore) {
            PlayerPrefs.SetInt("HighScore", score);
        }
        
        Destroy(gameObject);
        SceneManager.LoadScene("GameOver");
    }

    void IncreaseSpeed() {
        Tetramino.speed -= .001f;
    }
	
	// Update is called once per frame
	void Update () {
        if (Tetramino.paused==false) {
            CheckUserInput();
        }
        //Debug.Log(paused);
        
	}

    void UpdateHighScore() {
        Debug.Log("This is running");
        var textUIComp = GameObject.Find("Score").GetComponent<Text>();
        int score = int.Parse(textUIComp.text);
        Debug.Log("Highscore:" + highScore);
        Debug.Log("textUIComp:" + textUIComp.text);
        if (score > highScore) {
            PlayerPrefs.SetInt("HighScore", score);
            textUIComp.text = score.ToString();
        }
    }
    

    void CheckUserInput() {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
           // Debug.Log(transform.position);

            if (!IsInGrid())
            {
                transform.position += new Vector3(-1, 0, 0);
            }
            else {
                UpdateGameBoard();

                SoundManager.Instance.PlayOneShot(SoundManager.Instance.shapeMove);
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            //Debug.Log(transform.position);

            if (!IsInGrid())
            {
                transform.position += new Vector3(1, 0, 0);
            }
            else {
                UpdateGameBoard();
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.shapeMove);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, 90);

            if (!IsInGrid())
            {
                transform.Rotate(0, 0, -90);
            }
            else {
                UpdateGameBoard();
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.rotateSound);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - lastMoveDown>=Tetramino.speed) {
            //Debug.Log("Time.time- lastMoveDown="+(Time.time - lastMoveDown));
            //Debug.Log("Tetramino.speed="+Tetramino.speed);
            //Debug.Log(transform.position);

            if (!IsInGrid())
            {
                transform.position += new Vector3(0, 1, 0);

                bool rowDeleted = Board.DeleteAllFullRows();

                


                if (rowDeleted) {
                    
                    IncreaseTextUIScore();
                    UpdateHighScore();
                    Board.DeleteAllFullRows();
                    //TODO Change Score on UI
                    //IncreaseTextUIScore();
                }

                enabled = false;
                FindObjectOfType<Game>().SpawnShape();
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.shapeStop);
            }
            else {
                UpdateGameBoard();
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.shapeMove);
            }
            transform.position += new Vector3(0, -1, 0);
            lastMoveDown = Time.time;
            

        }
    }

    public bool IsInGrid() {
        int childCount = 0;
        foreach(Transform childBlock in transform){
            Vector2 vect = childBlock.position;
            childCount++;
            //Debug.Log(childCount + " " + childBlock.position);

            if (!IsInBorder(vect)) {
                return false;
            }

            if (Board.gameBoard[(int)vect.x, (int)vect.y] != null && Board.gameBoard[(int)vect.x, (int)vect.y].parent != transform) {
                return false;
            }

        }
        return true;
    }

    public static bool IsInBorder(Vector2 pos) {
        //Debug.Log("position.x=" + pos.x);
        //Debug.Log("position.y=" + pos.y);
        return ((int)pos.x>0 && (int)pos.x<11 && (int)pos.y>0 && (int)pos.y<19);
    }

    public void UpdateGameBoard() {
        for (int y = 0; y < 21; ++y) {
            for (int x = 0; x < 11; ++x) {
                if (Board.gameBoard[x, y] != null && Board.gameBoard[x, y].parent == transform) {
                    Board.gameBoard[x, y] = null;
                }
            }
        }
        foreach (Transform chidBlock in transform) {
            Vector2 vect = chidBlock.position;
            
                Board.gameBoard[(int)vect.x, (int)vect.y] = chidBlock;
                //Debug.Log("Cube At : " + vect.x + " " + vect.y);
            
        }

        Board.PrintArray();
    }

    void IncreaseTextUIScore() {
        var textUIComp = GameObject.Find("Score").GetComponent<Text>();
        int score = int.Parse(textUIComp.text);

        score++;

        textUIComp.text = score.ToString();
    }
}
