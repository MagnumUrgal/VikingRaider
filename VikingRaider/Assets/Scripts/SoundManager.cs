using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SoundManager : Singleton<SoundManager>
{
    /// <summary>
    /// lien vers la source sonore (pour la musique)
    /// </summary>
    private static AudioSource source;
    /// <summary>
    /// lien vers la source sonore (pour les bruitages)
    /// </summary>
    private static AudioSource sourceBruitage;
    /// <summary>


    /// <summary>
    /// Coroutine utilisé pour le FadeIn/FadeOut du barde
    /// </summary>
    private IEnumerator maCoroutine;
    /// <summary>
    /// Retient si la coroutine est lancée ou pas
    /// </summary>
    private bool used;

    //************************
    //********Musiques********
    //************************
    public AudioClip menu;
    public AudioClip main;

    //************************
    //********Bruitages*******
    //************************
    /// <summary>
    /// Lien vers le bruitage du logo
    /// </summary>
    public AudioClip attaque;
    public AudioClip autre_event;
    public AudioClip chaton;
    public AudioClip knights;
    public AudioClip echec;
    public AudioClip spy;
    public AudioClip dutchman;
    public AudioClip wedding;
    public AudioClip priest;
    public AudioClip win;


    /// <summary>
    /// Initialisation
    /// </summary>
    public void Awake()
    {
        used = false;

        GameObject sourceGO = GameObject.Instantiate(new GameObject());
        sourceGO.AddComponent<AudioSource>();
        sourceGO.name = "Musique";

        GameObject sourceBruitageGO = GameObject.Instantiate(new GameObject());
        sourceBruitageGO.AddComponent<AudioSource>();
        sourceBruitageGO.name = "Bruitage";

        sourceGO.transform.SetParent(this.transform);
        sourceBruitageGO.transform.SetParent(this.transform);
        source = sourceGO.GetComponent<AudioSource>();
        sourceBruitage = sourceBruitageGO.GetComponent<AudioSource>();
    }

    /// <summary>
    /// Change le volume de la musique
    /// </summary>
    /// <param name="newVolume">Nouveau volume</param>
    public static void ChangeVolume(int newVolume)
    {
        Instance.InstanceChangeVolume(newVolume);
    }
    /// <summary>
    /// Comme ChangeVolume mais pour une instance
    /// </summary>
    /// <param name="newVolume">Nouveau volume</param>
    void InstanceChangeVolume(int newVolume)
    {
        source.volume = ((float)newVolume) / 100;
    }

    /// <summary>
    /// Change le volume des bruitages
    /// </summary>
    /// <param name="newVolume">Nouveau volume</param>
    public static void ChangeBruitage(int newVolume)
    {
        Instance.InstanceChangeBruitage(newVolume);
    }
    /// <summary>
    /// Comme ChangeBruitage mais pour une instance
    /// </summary>
    /// <param name="newVolume">Nouveau volume</param>
    void InstanceChangeBruitage(int newVolume)
    {
        sourceBruitage.volume = ((float)newVolume) / 100;
    }

    //*************************
    //********Bruitages********
    //*************************
    /// <summary>
    /// Joue un son sur la piste de bruitage
    /// </summary>
    /// <param name="name">Nom du son à jouer</param>
    public static void PlayBruitage(string name)
    {
        Instance.InstancePlayBruitage(name);
    }
    /// <summary>
    /// Comme PlayBruitage mais pour une instance
    /// </summary>
    /// <param name="name">{Logo}Nom du son à jouer</param>
    void InstancePlayBruitage(string name)
    {
        AudioClip originalClip;
        originalClip = null;
        switch (name)
        {
            case "event":
                originalClip = autre_event;
                break;
            case "cat":
                originalClip = chaton;
                break;
            case "pillage":
                originalClip = attaque;
                break;
            case "knights":
                originalClip = knights;
                break;
            case "echec":
                originalClip = echec;
                break;
            case "espionnage":
                originalClip = spy;
                break;
            case "hollandais":
                originalClip = dutchman;
                break;
            case "mariage":
                originalClip = wedding;
                break;
            case "priest":
                originalClip = priest;
                break;
            case "victory":
                originalClip = win;
                break;
        }
        sourceBruitage.PlayOneShot(originalClip);
    }


    //************************
    //********Musiques********
    //************************
    /// <summary>
    /// Fait un fade depuis l'ancienne musique vers la nouvelle
    /// </summary>
    /// <param name="name">Nom de la musique à jouer</param>
    public static void ChangeMusique(string name)
    {
        Instance.InstanceChangeMusique(name);
    }
    /// <summary>
    /// Comme ChangeMusique mais pour une instance
    /// </summary>
    /// <param name="name">Nom de la musique à jouer</param>
    void InstanceChangeMusique(string name)
    {
        StartCoroutine(FadeMusique(name));
      
    }

    /// <summary>
    /// Fait un fade depuis l'ancienne musique vers la nouvelle
    /// </summary>
    /// <param name="name">Nom de la nouvelle musique</param>
    private IEnumerator FadeMusique(string name)
    {
        bool played = source.isPlaying;
        float tmp = source.volume;
        if (played)
        {
            for (int i = 4; i > -1; i--)
            {
                source.volume = tmp * i * 0.2f;
                yield return new WaitForSeconds(0.1f);
            }
            source.Stop();
        }

        PlayMusique(name);

        if (played)
        {
            for (int i = 1; i < 6; i++)
            {
                source.volume = tmp * i * 0.2f;
                yield return new WaitForSeconds(0.1f);
            }
            //source.volume = OptionFileManager.GetVolume() / 100f;
        }
    }

    /// <summary>
    /// Lance la couroutine <see cref="StopMusiqueCoroutine"/> qui stope la musique avec un fade
    /// </summary>
    public static void StopMusique()
    {
        Instance.InstanceStopMusique();
    }
    /// <summary>
    /// Comme StopMusique mais pour une instance
    /// </summary>
    void InstanceStopMusique()
    {
        StartCoroutine(StopMusiqueCoroutine());
    }

    /// <summary>
    /// Stope la musique avec un fade
    /// </summary>
    private IEnumerator StopMusiqueCoroutine()
    {
        float tmp = source.volume;
        for (int i = 4; i > -1; i--)
        {
            source.volume = tmp * i * 0.2f;
            yield return new WaitForSeconds(0.1f);
        }
        source.Stop();
    }

    /// <summary>
    /// Joue un son sur la piste de musique
    /// </summary>
    /// <param name="name">Nom de la musique à jouer</param>
    public static void PlayMusique(string name)
    {
        Instance.InstancePlayMusique(name);
    }
    /// <summary>
    /// Comme PlayMusique mais pour une instance
    /// </summary>
    /// <param name="name">{""}Nom de la musique à jouer</param>
    void InstancePlayMusique(string name)
    {
        source.Stop();
        AudioClip originalClip = null;
        switch (name)
        {
            case "main":
                originalClip = main;
                break;
            case "menu":
                originalClip = menu;
                break;
        }
        source.clip = originalClip;
        source.Play();
    }
}



