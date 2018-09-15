using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    //public AudioSource sfxClip;
    //public AudioSource musicClip;

    //public double sfxVolSlider;
    //public double musicVolSlider;
    //public double mainVolSlider; // if lower than sliders above then use this instead.

    public Slider mainVolSliderGO;
    public Slider sfxVolSliderGO;
    public Slider musicVolSliderGO;

    // public float lowPitch = .95f;
    // public float highPitch = 1.05f;

    /// <summary>
    /// //////////////////////////////////
    /// </summary>

    public AudioMixer MasterMixer; // exposed: MasterVolume, MusicMixerGroupVolume, SFXMixerGroupVolume
    public AudioMixerGroup MusixMixerGroup;
    public AudioMixerGroup SFXMixerGroup;

    public AudioClip objectConstructionClip;
    public AudioClip objectDeconstructionClip;

	// Use this for initialization
	void Start () {
        mainVolSliderGO.onValueChanged.AddListener(delegate { MainVolChanged(); });
        sfxVolSliderGO.onValueChanged.AddListener(delegate { SfxVolChanged(); });
        musicVolSliderGO.onValueChanged.AddListener(delegate { MusicVolChanged(); });

        //MasterMixer.SetFloat("MusicVolume", 3);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void MainVolChanged()
    {
        MasterMixer.SetFloat("MasterVolume", (mainVolSliderGO.value));

    }
    public void SfxVolChanged()
    {
        MasterMixer.SetFloat("SFXMixerGroupVolume", (sfxVolSliderGO.value));

    }
    public void MusicVolChanged()
    {
        MasterMixer.SetFloat("MusicMixerGroupVolume", (musicVolSliderGO.value));
    }


    public void ObjectConstruction(GameObject source)
    {
        
        //print("start" + source.name);
        if (source.GetComponent<AudioSource>() == null)
        {
          //  print("made audiosource");
            AudioSource tempSource = source.AddComponent<AudioSource>();
            tempSource.outputAudioMixerGroup = SFXMixerGroup;
            tempSource.PlayOneShot(objectConstructionClip, 0.1f);
        }
        source.GetComponent<AudioSource>().outputAudioMixerGroup = SFXMixerGroup;
        source.GetComponent<AudioSource>().PlayOneShot(objectConstructionClip, 0.1f);
        //Destroy(tempSource);
       // print("stop");
    }
    public void ObjectDeconstruction(GameObject source)
    {

        if (source.GetComponent<AudioSource>() == null)
        {
            //print("made audiosource");
            AudioSource tempSource = source.AddComponent<AudioSource>();
            tempSource.outputAudioMixerGroup = SFXMixerGroup;
            tempSource.PlayOneShot(objectDeconstructionClip, 0.25f);
        }
        source.GetComponent<AudioSource>().outputAudioMixerGroup = SFXMixerGroup;
        source.GetComponent<AudioSource>().PlayOneShot(objectDeconstructionClip, 0.25f);



       
    }
}
