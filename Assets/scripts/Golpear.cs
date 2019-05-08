using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class Golpear : MonoBehaviour, ITrackableEventHandler
{
    
    int vida = 100;
    Slider sliderVida;
    Animator anim;
    public AudioClip dolor;
    public AudioClip jab;
    AudioSource audioSource;
    [SerializeField] GameObject botonGolpe;
    TrackableBehaviour tb;

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED || newStatus == TrackableBehaviour.Status.TRACKED || newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log("Me han detectado");
        }
        else
        {
            vida = 100;
            sliderVida.value = 1f;
            anim.SetBool("golpe", false);
            anim.SetBool("morir", false);
            botonGolpe.SetActive(true);
        }
    }

    private void Start()
    {
        sliderVida = GetComponentInChildren<Slider>();
        anim = GetComponentInChildren<Animator>();
        audioSource = GetComponentInChildren<AudioSource>();
        tb = GetComponent<TrackableBehaviour>();
        if (tb)
            tb.RegisterTrackableEventHandler(this);
    }

    public void Golpe()
    {
       
        StartCoroutine(golpearProcess());
    }

    public void MyURL(string url)
    {
        Application.OpenURL(url);
    }

    IEnumerator golpearProcess()
    {
        audioSource.PlayOneShot(jab, 0.7F);
        vida -= 10;
        sliderVida.value = (float)vida / 100;
        botonGolpe.SetActive(false);
        if (vida <= 0) { 
            audioSource.PlayOneShot(dolor, 0.7F);
            anim.SetBool("morir", true);
        } else
            anim.SetBool("golpe", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("golpe", false);
        if (vida <= 0)
            botonGolpe.SetActive(false);
        else
            botonGolpe.SetActive(true);
    }
}
