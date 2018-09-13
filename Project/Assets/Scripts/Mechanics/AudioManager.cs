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
    public AudioMixer MusicMixer;
    public AudioMixer SFXMixer;
        


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
        //  mainVolSlider = System.Math.Round(mainVolSliderGO.value, 2);

        MasterMixer.SetFloat("MasterVolume", (mainVolSliderGO.value));




    }
    public void SfxVolChanged()
    {
        //if (sfxVolSliderGO.value < mainVolSliderGO.value)
        //{
        //    //if sfx vol is lower than main vol then set sfx to slider value else set sfx to main vol value
        //    sfxVolSlider = System.Math.Round(sfxVolSliderGO.value, 2);
        //    print("allowed change");
        //}
        //else
        //{
        //    print("main volume restricted");
        //    sfxVolSlider = mainVolSlider;
        //}


        MasterMixer.SetFloat("SFXMixerGroupVolume", (sfxVolSliderGO.value));





    }
    public void MusicVolChanged()
    {
        //mainVolSlider = System.Math.Round(mainVolSliderGO.value, 2);


        MasterMixer.SetFloat("MusicMixerGroupVolume", (musicVolSliderGO.value));



    }
}
