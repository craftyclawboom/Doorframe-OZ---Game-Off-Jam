using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] furnitureObjects; //made an array for now since we're gonna have a lot of different options later

    public Vector3 lastPos;
    public int numberOfRooms;

    ///note to self - add some type of field that knows the lastly generated object and adds onto it, continuing until reaching a number where it creates a staircase
    ///add random height to each object to make it feel more natural
    ///maake a wall on the side that doesn't generate.

    private bool right;
    private int randDir;

    void Start()
    {
        GenerateFloor();
    }
    public void SetupGen()
    {
        numberOfRooms = Random.Range(7, 15);
        lastPos = transform.position;
        int randDir = Random.Range(0, 100);

        if (randDir >= 50)
        {
            right = true;
        }
    }

    public void GenerateFloor()
    {
        SetupGen();

        for(int i=0; i<numberOfRooms;i++)
        {
            if (right)
            {
                CreateFurnitureObject(true);
            }
            else
            {
                CreateFurnitureObject(false);
            }
        }
    }

    public void CreateFurnitureObject(bool positive)
    {
        if(positive)
        {
            int addAmt = Random.Range(8, 14);
            Vector3 pos = lastPos + new Vector3(addAmt, 0f, 0f);

            GameObject obj = Instantiate(furnitureObjects[Random.Range(0, furnitureObjects.Length)], pos, Quaternion.identity);//add random rotation later
            lastPos = obj.transform.position;
        }
        else
        {
            int addAmt = Random.Range(-14, -8);
            Vector3 pos = lastPos + new Vector3(addAmt, 0f, 0f);

            GameObject obj = Instantiate(furnitureObjects[Random.Range(0, furnitureObjects.Length)], pos, Quaternion.identity);//add random rotation later
            lastPos = obj.transform.position;
        }
    }
}
