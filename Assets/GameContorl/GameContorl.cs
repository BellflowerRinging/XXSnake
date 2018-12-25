using UnityEngine;
using System.Collections.Generic;

public class GameContorl : MonoBehaviour
{
    bool gameing = false;
    public TextContorl TextContorl;
    public SnakeContorl SnakeContorl;
    public FoodContorl FoodContorl;

    public GameObject MenuGameObject;
    public GameObject HelpGameObject;

    public GameObject NewGameButton;
    public GameObject ExitGameButton;

    public GameObject AgainTextGameObject;


    public GameObject HelpAllTextGameObject;


    public void NewGame()
    {
        gameing = true;

        if (FoodContorl.FoodList != null)
        {
            foreach (var item in FoodContorl.FoodList)
            {
                GameObject.Destroy(item.GameObject);
            }
        }

        if (SnakeContorl.BlackBodyList != null)
        {
            foreach (var item in SnakeContorl.BlackBodyList)
            {
                GameObject.Destroy(item.GameObject);
            }
        }

        if (SnakeContorl.Head != null)
        {
            foreach (var item in SnakeContorl.Head.BodyList)
            {
                GameObject.Destroy(item.GameObject);
            }

            GameObject.Destroy(SnakeContorl.Head.GameObject);
        }

        FoodContorl.FoodList = new List<Food>();
        SnakeContorl.BlackBodyList = new List<Body>();

        InitSnake();

        MenuGameObject.SetActive(false);
    }

    public void InitSnake()
    {
        SnakeContorl.Head = new Head(10);
        SnakeContorl.HeadGameObject = SnakeContorl.Head.GameObject;
        SnakeContorl.BodyList = SnakeContorl.Head.BodyList;

        SnakeContorl.ForwordVector = Vector3.zero;
        SnakeContorl.transform.forward = Vector3.forward;
    }

    public void GoodGame()
    {
        gameing = false;
        MenuGameObject.SetActive(true);

    }

    public bool Gameing
    {
        get { return gameing; }
        //set { gameing = value; }
    }

    private void Start()
    {
        InitSnake();
        SnakeContorl.LookContorlInit();
    }

    public void HelpButtonOnClick()
    {
        NewGameButton.SetActive(HelpAllTextGameObject.activeSelf);
        ExitGameButton.SetActive(HelpAllTextGameObject.activeSelf);
        HelpAllTextGameObject.SetActive(!HelpAllTextGameObject.activeSelf);
    }

    public void ExitGameButtonOnClick()
    {
        if (inLastTimg)
        {
            Application.Quit();
        }
        LastTime = Time.time;
    }

    bool inLastTimg=false;
    float LastTime = -1;

    private void Update()
    {
        if (LastTime != -1 && Time.time - LastTime < 2f)
        {
            inLastTimg = true;
        }
        else
        {
            inLastTimg = false;
        }

        AgainTextGameObject.SetActive(inLastTimg);


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGameButtonOnClick();
        }

    }

}