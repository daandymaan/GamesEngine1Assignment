using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour
{
    public float speed = 10;
    bool startTrain = false;
    public ParticleSystem chimney;
    public AudioSource trainChime;
    public AudioSource trainTrack;

    void Start()
    {
        var emission = chimney.emission;
        emission.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.W))
        {
            playAudio();
            startTrain = true;  
            trainTrack.loop = true;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(SmokeControl());
        }

        if(startTrain)
        {
            Vector3 targetPos = transform.position;
            targetPos += (Vector3.forward * speed * Time.deltaTime);
            transform.position = targetPos;
        }

        
    }

    void playAudio()
    {
        trainTrack.Play();
    }

    public IEnumerator SmokeControl()
    {
        speed += 20;
        if(!trainChime.isPlaying)
        {
            trainChime.Play();
        }
        SmokeRelease();
        yield return new WaitForSeconds(1f);
        SmokeStop();
        yield return new WaitForSeconds(1f);
        SmokeRelease();
        yield return new WaitForSeconds(1f);
        SmokeStop();
        speed -= 20;
    }

    void SmokeRelease()
    {
        var emission = chimney.emission;
        emission.enabled = true;
    }

    void SmokeStop()
    {
        var emission = chimney.emission;
        emission.enabled = false;
    }
}

