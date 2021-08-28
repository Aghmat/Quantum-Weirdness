using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager;
using UnityEngine;

public class Combiner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject container;
    public Stack<GameObject> lastClicked;
    void Start()
    {
        lastClicked = new Stack<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.GetComponent<ParticleType>().pType.Equals(ParticleType.PType.Quark))
                {
                    if (hit.transform.parent == null)
                    {
                        if (lastClicked.Count >= 1)
                        {
                            var last = lastClicked.Pop();
                            hit.transform.parent = last.transform.parent;

                            if (last.transform.parent.childCount == 2)
                            {
                                //text
                                last.transform.parent.GetChild(0).GetChild(1).transform.GetChild(0).gameObject.SetActive(true); 
                                //right
                                last.transform.parent.GetChild(0).GetChild(1).transform.GetChild(2).gameObject.GetComponent<ParticleSystem>().Play();
                                
                                var position = last.transform.position;
                                hit.transform.position = new Vector3(position.x + 5,position.y,position.z);
                            }
                            else if (last.transform.parent.childCount == 3)
                            {
                                //upRight
                                last.transform.parent.GetChild(0).GetChild(1).transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play(); 
                                //downRight
                                last.transform.parent.GetChild(0).GetChild(1).transform.GetChild(3).gameObject.GetComponent<ParticleSystem>().Play();
                                var position = last.transform.position;
                                hit.transform.position = new Vector3(position.x - 2.5f,position.y + 5,position.z);
                            }
                     
                        }
                        else
                        {                    
                            var iContainer = Instantiate(container);
                            hit.transform.parent = iContainer.transform;
                        }
                    }
                    else
                    {
                    
                    }
                
                    if (!lastClicked.Contains(hit.transform.gameObject))
                    {
                        lastClicked.Push(hit.transform.gameObject);
                    }

                    //Only 3 Quarks allowed in container
                    if (lastClicked.Count >= 1 && lastClicked.Peek().transform.parent.childCount == 3)
                    {
                        lastClicked.Clear();
                    }
                }
            }

        }
        
    }
}
