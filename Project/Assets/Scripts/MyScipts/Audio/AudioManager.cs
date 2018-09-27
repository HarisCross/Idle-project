using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public Slider mainVolSliderGO;
    public Slider sfxVolSliderGO;
    public Slider musicVolSliderGO;

    public bool masterMute;//toggled to mute and unmute
    private float masterMuteFloat;//stores old volume to reset too after unmute
    public bool MusicMute;
    private float musixMuteFloat;
    public bool SFXMute;
    private float SFXMuteFloat;

    public AudioMixer MasterMixer; // exposed: MasterVolume, MusicMixerGroupVolume, SFXMixerGroupVolume
    public AudioMixerGroup MusixMixerGroup;
    public AudioMixerGroup SFXMixerGroup;

    public AudioClip objectConstructionClip; // clips to play on object made or destroyed
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
    public void ToggleMainMute()
    {
        if (masterMute == true)
        {
            masterMute = !masterMute;

            MasterMixer.SetFloat("MasterVolume", masterMuteFloat);
        }
        else
        {
            masterMute = !masterMute;
            MasterMixer.GetFloat("MasterVolume", out masterMuteFloat);
            MasterMixer.SetFloat("MasterVolume", -80);

        }

    }
    public void ToggleMusicMute()
    {

        if (MusicMute == true)
        {
            MusicMute = !MusicMute;

            MasterMixer.SetFloat("MusicMixerGroupVolume", musixMuteFloat);
        }
        else
        {
            MusicMute = !MusicMute;
            MasterMixer.GetFloat("MusicMixerGroupVolume", out musixMuteFloat);
            MasterMixer.SetFloat("MusicMixerGroupVolume", -80);

        }

    }
    public void ToggleSFXMute()
    {
        if (SFXMute == true)
        {
            SFXMute = !SFXMute;

            MasterMixer.SetFloat("SFXMixerGroupVolume", SFXMuteFloat);
        }
        else
        {
            SFXMute = !SFXMute;
            MasterMixer.GetFloat("SFXMixerGroupVolume", out SFXMuteFloat);
            MasterMixer.SetFloat("SFXMixerGroupVolume", -80);

        }

        
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
