using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MixLevels : MonoBehaviour {

	public AudioMixer masterMixer;

	public void SetSfxLvl(float sfxLvl)
	{
		masterMixer.SetFloat("sfxVol", sfxLvl);
	}

	public void SetMusicLvl (float musicLvl)
	{
		masterMixer.SetFloat ("musicVol", musicLvl);
	}

	public void IsMuted(bool isMuted)
	{
		if(isMuted==false)
		{
			masterMixer.SetFloat("masterVol",-80f);
		}
		else
		{
			masterMixer.SetFloat("masterVol",0);
		}
	}
}
