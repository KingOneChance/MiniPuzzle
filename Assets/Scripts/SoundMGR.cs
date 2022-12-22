using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMGR : MonoBehaviour
{
    private AudioSource audioSource = null;

    //[SerializeField] private AudioClip bgm = null;
    [SerializeField] private AudioClip correctSound = null;
    [SerializeField] private AudioClip inCorrectSound = null;
    [SerializeField] private AudioClip buttonClickSound = null;
    [SerializeField] private AudioClip feverTimeSound = null;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void On_ClickBtnSound()
    {
        audioSource.PlayOneShot(buttonClickSound);
    }

    public void CorrectSound()
    {
        audioSource.PlayOneShot(correctSound);
    }

    public void InCorrectSound()
    {
        if (audioSource.isPlaying == true)
            return;
        else
        {
            audioSource.PlayOneShot(inCorrectSound);
        }
    }
    public void FeverTimeSound()
    {
        audioSource.PlayOneShot(feverTimeSound);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
