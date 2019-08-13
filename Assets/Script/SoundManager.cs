using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioClip[][] sound = new AudioClip[2][];

    AudioSource myAudio;

    public static SoundManager instance;

    private void Awake()
    {
        if (SoundManager.instance == null)
            SoundManager.instance = this;

        sound[0] = new AudioClip[3];
        sound[1] = new AudioClip[11];

        sound[0][0] = Resources.Load<AudioClip>(@"SoundData\WhosThatSexyThing");
        sound[0][1] = Resources.Load<AudioClip>(@"SoundData\IThankGodEveryday");
        sound[0][2] = Resources.Load<AudioClip>(@"SoundData\Okay");

        sound[1][0] = Resources.Load<AudioClip>(@"SoundData\Hello1");
        sound[1][1] = Resources.Load<AudioClip>(@"SoundData\Hello2");
        sound[1][2] = Resources.Load<AudioClip>(@"SoundData\Hello3");

        sound[1][3] = Resources.Load<AudioClip>(@"SoundData\How1");
        sound[1][4] = Resources.Load<AudioClip>(@"SoundData\How2");
        sound[1][5] = Resources.Load<AudioClip>(@"SoundData\How3");

        sound[1][6] = Resources.Load<AudioClip>(@"SoundData\ThankYou1");
        sound[1][7] = Resources.Load<AudioClip>(@"SoundData\ThankYou2");
        sound[1][8] = Resources.Load<AudioClip>(@"SoundData\ThankYou3");

        sound[1][9] = Resources.Load<AudioClip>(@"SoundData\Christmas");

        sound[1][10] = Resources.Load<AudioClip>(@"SoundData\Okay");
    }

    // Use this for initialization
    void Start () {
        myAudio = GetComponent<AudioSource>();
	}
	
    public void PlaySound(int scenario, int sequence)
    {
        myAudio.PlayOneShot(sound[scenario - 1][sequence]);
    }

}
