using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioSource theMusic;
    public AudioSource theMusicBoost;
    public AudioSource oof;
    public AudioSource victory;

    public bool startPlaying;

    public BeatScroller bs;

    public static GameManager instance;

    public Slider slider;

    public int currentScore = 0;
    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;

    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;

    public Text scoreText;
    public Text multiText;

    public GameObject noteHolder;
    public GameObject[] notes;

    public GameObject spongebob;
    
    private bool hasSpawnStarted = false;

    public GameObject camera;
    
    public float goodShakeAmt = .05f;
    public float perfectShakeAmt = .15f;
    public float shakeDuration = 0.5f;
    public float noteSpawnPosition = 3.30f;

    public Animator[] anims;

    public int numberOfNotes;

    public float songTimeLeft;
    public float songLength;

    public bool isGameOver;

    public Text wellDoneText;

    public bool gameOverHasTriggered;

    public Animator sqAnimator;
    public Animator patAnimator;
    private Animator sbAnimator;
    private CameraShake shakeScript;
   

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        songTimeLeft = theMusic.clip.length;
        songLength = theMusic.clip.length;
        numberOfNotes = 0;
        scoreText.text = "Score: " + 0;
        currentMultiplier = 1;
        instance = this;
        sbAnimator = spongebob.GetComponent<Animator>();
        shakeScript = camera.GetComponent<CameraShake>();
        shakeScript.shakeDuration = 0;
        gameOverHasTriggered = false;
        foreach (Animator anim in anims)
        {
            anim.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Play vicory scene when song ends
        if (isGameOver && !gameOverHasTriggered)
        {
            CancelInvoke();
            Invoke("VictoryScene", 2);
            gameOverHasTriggered = true;
            return;
        }

        if (!startPlaying)
        {
            // Any key to start level
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                sbAnimator.SetBool("hasStarted", true);
                bs.hasStarted = true;
                theMusic.Play();
                // Start squidward and patrick animations
                foreach (Animator anim in anims)
                {
                    anim.enabled = true;
                }
            }
        }
        else if (!hasSpawnStarted)
        {
            // Randomly generate notes with beats per second
            var rate = (1 / noteHolder.GetComponent<BeatScroller>().tempo);
            InvokeRepeating("SpawnRandomNotes", 0, rate);
            hasSpawnStarted = true;
        }

        if (startPlaying)
        {
            // Check if song has ended
            if (songTimeLeft <= 0)
            {
                isGameOver = true;
                
            }
            songTimeLeft -= Time.deltaTime;
            slider.value = songTimeLeft / songLength;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("LevelSelector", LoadSceneMode.Single);
        }
    }

    void VictoryScene()
    {
        // Plays animations and music for when song ends
        theMusic.Stop();
        theMusicBoost.Stop();
        victory.Play();
        wellDoneText.enabled = true;
        sbAnimator.SetBool("hasMissed", false);
        sbAnimator.SetTrigger("victory");
        foreach (Animator a in anims)
        {
            a.SetTrigger("victory");
        }
        Invoke("EndScene", victory.clip.length);
    }

    void EndScene()
    {
        SceneManager.LoadScene("LevelSelector", LoadSceneMode.Single);
    }

    public void SpawnRandomNotes()
    {
        // 25% chance to spawn note for each button
        for (int i = 0; i < notes.Length; i++)
        {
            var rand = Random.Range(0f, 1f);
            if (rand < 0.25f)
            {
                numberOfNotes += 1;
                var note = notes[i];
                var xPos = note.transform.position.x;
                var yPos = noteSpawnPosition;
                Instantiate(note, new Vector3(xPos, yPos), note.transform.rotation, noteHolder.transform);
            }
        }
        /* var randNote = notes[Random.Range(0, notes.Length)];
        var xPos = randNote.transform.position.x;
        var yPos = 3.3f + Random.Range(1, 5);
        Instantiate(randNote, new Vector3(xPos, yPos), randNote.transform.rotation, noteHolder.transform); */
    }

    public void shakeCamera(float intensity)
    {
        shakeScript.shakeAmount = intensity;
        shakeScript.shakeDuration = shakeDuration;
    }


    public void NoteHit()
    {
        // Move up multiplier based on thresholds
        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker += 1;
            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier += 1;
            }

        }

        multiText.text = "Multiplier: x" + currentMultiplier;
        scoreText.text = "Score: " + currentScore;
        sbAnimator.SetBool("hasMissed", false);
        sbAnimator.SetInteger("animationNumber", Random.Range(0, 6));
    }

    public void NormalHit()
    {
        theMusicBoost.Stop();
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();
    }

    public void GoodHit()
    {
        theMusicBoost.Stop();
        currentScore += scorePerGoodNote * currentMultiplier;
        shakeCamera(goodShakeAmt);
        NoteHit();
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        shakeCamera(perfectShakeAmt);
        theMusicBoost.time = theMusic.time;
        theMusicBoost.Play();
        NoteHit();
    }

    public void NoteMissed()
    {
        theMusicBoost.Stop();
        oof.PlayOneShot(oof.clip);
        currentMultiplier = 1;
        multiplierTracker = 0;
        multiText.text = "Multiplier: x" + currentMultiplier;
        sbAnimator.SetBool("hasMissed", true);
    }
}
