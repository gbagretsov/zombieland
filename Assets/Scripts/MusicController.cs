using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

    [System.Serializable]
    public class Track
    {
        public AudioClip audioTrack;
        private AudioSource audioSource;
        public AudioSource GetAudioSource
        {
            get { return audioSource; }
            set { audioSource = value; }
        }
        public string songName;
        public string artistName;
    }

    public Track[] tracks;
    public bool playMusicAtStart = true;
    public bool randomOrder = false;
    private System.Random rnd;
    private bool isNowPlaying;

    private int k; // Указатель на текущий трек
	
    void Start()
    {
        rnd = new System.Random();

        for (int i = 0; i < tracks.Length; i++)
        {
            tracks[i].GetAudioSource = gameObject.AddComponent<AudioSource>();
            tracks[i].GetAudioSource.loop = false;
            tracks[i].GetAudioSource.clip = tracks[i].audioTrack;
            tracks[i].GetAudioSource.volume = 1; 
        }

        isNowPlaying = playMusicAtStart;
        if (playMusicAtStart)
        {
            k = 0;
            tracks[k].GetAudioSource.Play();
        }
        else
            k = -1;        
    }

	void Update ()
    {
        if (Input.GetButtonUp("Next Track"))
        {
            if (k != -1 && tracks[k].GetAudioSource.isPlaying)
                tracks[k].GetAudioSource.Stop();

            PlayNextTrack();
        }

        else if (Input.GetButtonUp("Stop Playing"))
        {
            isNowPlaying = false;
            if (tracks[k].GetAudioSource.isPlaying)
                tracks[k].GetAudioSource.Stop();
            
        } 

        if (isNowPlaying && !tracks[k].GetAudioSource.isPlaying)
            PlayNextTrack();

	}

    void PlayNextTrack()
    {
        k = randomOrder ? rnd.Next(tracks.Length) : k == tracks.Length - 1 ? 0 : k + 1;
        tracks[k].GetAudioSource.Play();
        isNowPlaying = true;

        // Вывод исполнителя и названия
        Debug.Log(k + ". " + tracks[k].artistName + " - " + tracks[k].songName + " @ " + System.DateTime.Now.ToString());
    }
}
