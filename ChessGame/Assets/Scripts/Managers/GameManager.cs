//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class GameManager : MonoBehaviour
//{
//    public GameObject controller;
//    public GameObject movePlate;

//    //Variable to keep tracks of "black" or "white" player
//    private string player;
//    private void OnMouseUp()
//    {
//        if (!controller.GetComponent<Game>().IsGameOver() && controller.GetComponent<Game>().GetCurrentPlayer() == player)
//        {
//            DestroyMovePlates();

//            InitiateMovePlates();
//        }

//    }
//    public void DestroyMovePlates()
//    {
//        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
//        for (int i = 0; i < movePlates.Length; i++)
//        {
//            Destroy(movePlates[i]);
//        }
//    }
//    public void InitiateMovePlates()
//    {
        

      

//    }
//}
