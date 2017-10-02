using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeButton : MonoBehaviour {

    private AudioSource clickSe;
    public AudioSource audioSource;

	// Use this for initialization
	void Start () {
		//AudioSource[] audioSource = GetComponents<AudioSource>();
        audioSource = gameObject.GetComponent<AudioSource>();
		//clickSe = audioSource;
	}

    public void returnHome(){
		//clickSe.PlayOneShot(clickSe.clip);
        audioSource.Play();
		StartCoroutine("GoToHomeScene");
    }
	IEnumerator GoToHomeScene()
	{
		yield return new WaitForSeconds(0.5f);
		//Homeへ
		SceneManager.LoadScene(1);
	}
}
