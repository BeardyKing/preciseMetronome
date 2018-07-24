using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class BPMController : MonoBehaviour
{ 
    private IEnumerator coroutine;

    [Header("Audio Data")]
    public AudioClip[] ac = new AudioClip[2];
    public AudioSource AudioS;

    [Header("BPM Data")]
    public float bpm = 120;
    public float lastBPM;
    public float stepsPerSecond;
    public float stepInterval;
    [Space(5)]
    public int maxStep;
    public int lastStep;
    public int currentStep;

    void Start()
    {
        CalculateBPM();

        coroutine = WaitForStep(stepInterval);
        StartCoroutine(coroutine); 
    }

    void CalculateBPM()
    {
        lastBPM = bpm;
        stepsPerSecond = bpm / 60;
        stepInterval = 1 / stepsPerSecond;
    }

    void Update()
    {
        if (lastBPM != bpm)
        {
            CalculateBPM();

            StopAllCoroutines();
            coroutine = WaitForStep(stepInterval);
            StartCoroutine(coroutine);
        }
    }

    // every stepInterval in seconds play audio()
    private IEnumerator WaitForStep(float stepInterval)
    {
        while (true)
        {
            yield return new WaitForSeconds(stepInterval);
            
            print("WaitAndPrint " + Time.time);
            if (currentStep < maxStep)
            {
                AudioS.PlayOneShot(ac[0], 1f);
                lastStep = currentStep;
                currentStep += 1;
            }
            else
            {
                lastStep = currentStep;
                currentStep = 1;
                AudioS.PlayOneShot(ac[1], 1f);
            }
            
        }
    }
}