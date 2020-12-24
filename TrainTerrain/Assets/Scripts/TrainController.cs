using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour
{
    public float speed = 10;
    bool startTrain = false;
    public ParticleSystem chimney;
    public AudioSource trainChime;

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
            startTrain = true;
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

    public IEnumerator SmokeControl()
    {
        speed += 20;
        if(!trainChime.isPlaying)
        {
            Debug.Log("In here");
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

