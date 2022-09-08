using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class RandomColor : MonoBehaviour
{
    public int[] array = new int[24];
    private System.Random _random = new System.Random();
    public GameObject objects;
    public List<OldBlue> allObjects = new List<OldBlue>();
    private bool introEnded = false;
    public int score, currentLevel = 10, counter = 0,i;
    public TextMeshProUGUI tmproScore, tmproTiles, tmproWin;
    [SerializeField] private float leftTime= 5;

    
    

    // Start is called before the first frame update
    void Start()
    {
        for (i = 0; i < 25; i++)
        {
            array[i] = i;
        }
        Shuffle(array);
        tmproWin.enabled = false;
        DOTween.Init();
        tmproScore.SetText("SCORE:" + 0);
        RandomChoose(currentLevel);
    }

    // Update is called once per frame
    void Update()
    {
        leftTime -= Time.deltaTime;
        if (leftTime <= 0 && !introEnded)
        {
            SetWhite();
            introEnded = true;
        }
        Raycast();
    }
    public void RandomChoose(int nextLevel)
    {
        Shuffle(array);
        SetWhite();
        int i;
        for (i = 0; i < nextLevel; i++)
        {
            allObjects[array[i]].SetBlue();
        }
        Debug.Log("counter"+counter);
        tmproTiles.SetText("TILES: {0} / {1}" +counter, + i);
    }


    public bool IsObjectChosen(GameObject clickedObject)
    {

        for (int i = 0; i < currentLevel; i++)
        {
            if (allObjects[array[i]] == clickedObject && clickedObject.GetComponent<Renderer>().material.color==Color.white)
            {
                return true;
            }
        }
        return false;
    }

    public void SetWhite()
    {
        for (int j = 0; j < 25; j++)
        {
            allObjects[j].GetComponent<Renderer>().material.color = Color.white;
        }
    }

    public void Raycast()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, 100))
            {
                ObjectHit(hit.transform);
            }
        }
    }

    void Shuffle(int[] array)
    {
        int p = array.Length;
        for (int n = p - 1; n > 0; n--)
        {
            int r = _random.Next(1, n);
            int t = array[r];
            array[r] = array[n];
            array[n] = t;
        }
    }

    void StartNewLevel(int levelDifficulty)
    {
        tmproWin.enabled = false;
        introEnded = false;
        leftTime = 5;
        SetWhite();
        RandomChoose(levelDifficulty);
    }

    void ObjectHit(Transform hit)
    {
        if(IsObjectChosen(hit.gameObject))
        {
            score += 50;
            counter++;
            hit.GetComponent<Renderer>().material.DOColor(Color.blue, 2);
            hit.DORotate(new Vector3(0, 0, hit.eulerAngles.z +180), 1, RotateMode.Fast).SetEase(Ease.InOutQuart);
            
            if (counter == currentLevel)
            {
                StartCoroutine(Sleep());
                currentLevel++;
                tmproWin.enabled = true;
            }

        }
        else
        {
            Debug.Log("else içinde");
            score -= 100;
        }
        tmproScore.SetText("SCORE:" + score);
        Debug.Log("counter" + counter);
        tmproTiles.SetText("TILES: {0} / {1}" + counter, +currentLevel);
 }
    public IEnumerator Sleep()
    {
        counter = 0;
        yield return new WaitForSecondsRealtime(5);
        StartNewLevel(currentLevel);
    }
}

