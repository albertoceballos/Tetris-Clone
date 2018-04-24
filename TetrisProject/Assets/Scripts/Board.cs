using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour {

    public static Transform[,] gameBoard = new Transform[11, 21];

    public static bool DeleteAllFullRows() {
        for (int row = 1; row < 21; ++row) {
            if (IsRowFull(row)) {
                DeleteGBRow(row);
                //TODO make sound
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.rowDelete);
                return true;
            }
        }
        return false;
    }

    public static bool IsRowFull(int row) {
        for (int col = 1; col < 11; ++col) {
            if (gameBoard[col, row] == null)
            {
                return false;
            }
        }
        return true;
    }

    public static void DeleteGBRow(int row) {
        for (int col = 1; col < 11; ++col) {
            Destroy(gameBoard[col, row].gameObject);
            gameBoard[col, row] = null;
        }

        row++;

        for (int j = row; j < 21; ++j) {
            for (int col = 1; col <11; ++col) {
                if (gameBoard[col, j] != null) {
                    gameBoard[col, j - 1] = gameBoard[col, j];
                    gameBoard[col, j] = null;
                    gameBoard[col, j - 1].position += new Vector3(0, -1, 0);
                }
            }
        }
    }

    /*public static void PrintArray()
    {
        string arrayOutput = "";

        // Gets size of gameboard array and then subtract
        // 1 because the array starts with 0
        int iMax = gameBoard.GetLength(0) - 1;
        int jMax = gameBoard.GetLength(1) -1;

        // Cycle through the array and print N or X 
        // depending on if you have a null or transform
        for (int j = jMax; j >= 1; j--)
        {
            for (int i = 1; i <= iMax; i++)
            {

                if (gameBoard[i, j] == null)
                {
                    arrayOutput += "N ";
                }
                else
                {
                    arrayOutput += "X ";
                }
            }

            arrayOutput += "\n \n";

        }

        // Get a reference to the Text component
        // and change its value
        //var myArrayComp = GameObject.Find("MyArray").GetComponent<Text>();
        //myArrayComp.text = arrayOutput;

    }*/

}
