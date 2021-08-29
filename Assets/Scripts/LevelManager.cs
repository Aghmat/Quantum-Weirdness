using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Light directionalLight;
    [SerializeField] private Camera mainCamera;
    private AudioSource audioSource;
    [SerializeField] public AudioClip[] clips;
    private bool[] hasClipPlayed;
    private Dictionary<String, bool> events;

    public bool[] HasClipPlayed => hasClipPlayed;

    [SerializeField] private GameObject floor;

    [SerializeField] private GameObject[] firstTwoQuarks;
    [SerializeField] private GameObject quarkSpawner;
    [SerializeField] private GameObject electron;

    private bool isRunning;

    private void OnEnable()
    {
        events = new Dictionary<string, bool>();
        events.Add("FirstTwoQuarks", false);
        events.Add("Proton", false);
        events.Add("Electron", false);
    }

    void Start()
    {
        floor.SetActive(true);
        quarkSpawner.SetActive(false);
        foreach (var quark in firstTwoQuarks)
        {
            quark.SetActive(false);
        }

        audioSource = mainCamera.GetComponent<AudioSource>();
        directionalLight.intensity = 0;
        audioSource.clip = clips[0];
        audioSource.Play();
        hasClipPlayed = new bool[clips.Length];
        hasClipPlayed[0] = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (directionalLight.intensity < 1)
        {
            directionalLight.intensity = Mathf.MoveTowards(directionalLight.intensity, 1, 0.01f);
        }
        
        //Free cam
        PlayAudioClip(3, 1);
        PlayAudioClip(2, 2);
        //Orbit cam
        PlayAudioClip(4, 3);
        //Switching cams
        PlayAudioClip(2, 4);
        

        // var testingIndex = 8;
        // for (int i = 0; i < testingIndex ; i++)
        // {
        //     hasClipPlayed[i] = true;
        // }
        
        PlayAudioClip(3, 5);

        if (hasClipPlayed[5] && !events["FirstTwoQuarks"])
        {
            mainCamera.GetComponent<FreeFlyCamera>().ResetCamera();
            mainCamera.GetComponent<OrbitCamera>().ResetCamera();
            
            floor.SetActive(false);
            for (int i = 0; i < firstTwoQuarks.Length; i++)
            {
                firstTwoQuarks[i].SetActive(true);
            }
        
        }
        
        if (firstTwoQuarks[0] == null)
        {
            PlayAudioClip(2, 6);
        }
        
        if (events["Proton"])
        {
            PlayAudioClip(2,7);
            quarkSpawner.SetActive(true);
        }

        var containers = GameObject.FindGameObjectsWithTag("Container");
        if (containers.Length > 0)
        {
            foreach (var container in containers)
            {
                var numberOfUpQuarks = Array.FindAll(container.GetComponentsInChildren<QuarkType>(), q => q.qType.Equals(QuarkType.QType.Up)).Length;
                if (container.transform.childCount == 3 && numberOfUpQuarks == 2)
                {
                    PlayAudioClip(2, 8);

                    if (events["Electron"])
                    {
                        GameObject.Instantiate(electron,  new Vector3(6, 3, 0), Quaternion.identity, container.transform);
                    }
                }

                if (container.transform.childCount == 4)
                {
                    PlayAudioClip(2, 9);
                }
            }
        }
    }

    void PlayAudioClip(float seconds, int clipIndex)
    {
        if (!audioSource.isPlaying && !isRunning && !hasClipPlayed[clipIndex])
        {
            StartCoroutine(WaitAndPlayClip(seconds, clipIndex));
        }
    }

    IEnumerator WaitAndPlayClip(float seconds, int clipIndex)
    {
        isRunning = true;
        hasClipPlayed[clipIndex] = true;
        yield return new WaitForSeconds(seconds);
        audioSource.clip = clips[clipIndex];
        audioSource.Play();

        if (clipIndex == 5)
            events["FirstTwoQuarks"] = true;
        else if (clipIndex == 6)
            events["Proton"] = true;
        else if (clipIndex == 8)
            events["Electron"] = true;
        
        
        isRunning = false;
    }
    
    private bool IsArrayEmpty<T>(T[] array)
    {
        return Array.TrueForAll(array, x => x == null);
    }
}
