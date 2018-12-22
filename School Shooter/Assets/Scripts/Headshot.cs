using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Headshot : MonoBehaviour {

    public Text headshotText;
    private AudioSource audioSource;
    public AudioClip headshotSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ActivateHeadshot()
    {
        audioSource.clip = headshotSound;
        audioSource.Play();
        audioSource.Play();
        headshotText.gameObject.SetActive(true);
        StartCoroutine(UnactivateHeadshot());
    }

    private IEnumerator UnactivateHeadshot()
    {
        yield return new WaitForSeconds(1f);
        headshotText.gameObject.SetActive(false);
    }
}
