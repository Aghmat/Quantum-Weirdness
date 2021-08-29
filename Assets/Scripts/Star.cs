using System.Collections;
using UnityEngine;

public class Star : MonoBehaviour
{
    private bool black;
    private bool end;
    [SerializeField] private GameObject canvas;
    private void Start()
    {
        StartCoroutine(ToBlackHole());
    }

    void Update()
    {
        if (black)
        {
            gameObject.transform.GetChild(0).localScale = Vector3.MoveTowards(gameObject.transform.GetChild(0).localScale, Vector3.zero, 0.05f);
            gameObject.transform.GetChild(2).localScale = Vector3.MoveTowards(gameObject.transform.GetChild(2).localScale , Vector3.zero, 0.005f);
            gameObject.transform.GetChild(2).GetChild(0).localScale = Vector3.MoveTowards(gameObject.transform.GetChild(2).GetChild(0).localScale, Vector3.zero, 0.005f); 
        }

        if (gameObject.transform.GetChild(0).localScale == Vector3.zero && !end)
        {
            StartCoroutine(End());
        }
    }

    IEnumerator ToBlackHole()
    {
        yield return new WaitForSeconds(25f);
        black = true;
    }
    
    IEnumerator End()
    {
        yield return new WaitForSeconds(3f);
        end = true;
        canvas.SetActive(true);
    }
}
