using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleContainer : MonoBehaviour
{
    private Combiner combiner;
    bool coroutineRunning;

    private void Start()
    {
        combiner = GameObject.Find("GameManager").GetComponent<Combiner>();
    }

    void Update()
    {
        if (!coroutineRunning)
        {
            StartCoroutine(CheckBaryonValidity());
        }
    }
    
    IEnumerator CheckBaryonValidity()
    {
        coroutineRunning = true;
        if (transform.childCount >= 2)
        {
            yield return new WaitForSeconds(5);
            var particles = transform.GetComponentsInChildren<ParticleType>();
            var quarks = new List<ParticleType>(particles).FindAll(p => p.pType.Equals(ParticleType.PType.Quark));
            if (quarks.Count == 2)
            {
                combiner.lastClicked.Clear();
                Destroy(gameObject);
            }
            else if (quarks.Count == 3)
            {
                var upQuarks = quarks.FindAll(q => q.gameObject.GetComponent<QuarkType>().qType.Equals(QuarkType.QType.Up));
                var downQuarks = quarks.FindAll(q => q.gameObject.GetComponent<QuarkType>().qType.Equals(QuarkType.QType.Down));
                if (!(upQuarks.Count == 2 && downQuarks.Count == 1))
                {
                    combiner.lastClicked.Clear();
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            yield return null;
        }
        coroutineRunning = false;
    }
}
