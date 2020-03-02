using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    int _playerHealth = 0;
    [SerializeField]
    int _playerMaxHealth = 5;

    float _timeSinceHurt = -1;

    [SerializeField]
    float _healTimer = 5;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private AudioSource songAudio;

    [SerializeField]
    private AudioSource sfxAudio;

    [SerializeField]
    private List<AudioClip> soundEffects;

    [SerializeField]
    private Image _healthBar;

    bool doHurtPitch = false;
    public bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        _playerHealth = _playerMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        HurtTimer();
        HurtPitch(0.7f);
        _healthBar.fillAmount = Mathf.Lerp(_healthBar.fillAmount, (float)_playerHealth / (float)_playerMaxHealth, Time.deltaTime*5f);
        DoDeath();
    }

    private void HurtTimer()
    {
        if (_playerHealth < _playerMaxHealth)
        {
            if(_timeSinceHurt >= 0 && _timeSinceHurt < _healTimer)
            {
                _timeSinceHurt += Time.deltaTime;
            }else if (_timeSinceHurt >= 0 && _timeSinceHurt >= _healTimer)
            {
                Heal();
                if (_playerMaxHealth > _playerHealth)
                {
                    _timeSinceHurt = 0;
                }else
                {
                    _timeSinceHurt = -1;
                }
            }
        }
    }

    private void HurtPitch(float hurtPitch)
    {
        if (doHurtPitch && songAudio.pitch > hurtPitch)
        {
            songAudio.pitch = Mathf.Lerp(songAudio.pitch, hurtPitch, Time.deltaTime * 3);
            if (songAudio.pitch <= hurtPitch + 0.1f)
            {
                doHurtPitch = false;
            }
        }
        if (!doHurtPitch && songAudio.pitch < 1 && !isDead)
        {
            songAudio.pitch = Mathf.Lerp(songAudio.pitch, 1, Time.deltaTime * 5);
        }
    }

    public void Hurt()
    {
        _timeSinceHurt = 0;
        _playerHealth--;
        doHurtPitch = true;

        sfxAudio.pitch = Random.Range(0.8f, 1.25f);
        sfxAudio.PlayOneShot(soundEffects[1]);

        FindObjectOfType<MusicDelayer>().combo = 1;
    }

    public void DoDeath()
    {
        if (_playerHealth <= 0)
        {
            isDead = true;
            if (songAudio.pitch > 0.01f)
            {
                songAudio.pitch = Mathf.Lerp(songAudio.pitch, 0f, Time.deltaTime * 3);
                animator.SetInteger("AnimState", 2);
                if (!sfxAudio.isPlaying)
                {
                    sfxAudio.clip = soundEffects[0];
                   sfxAudio.PlayDelayed(10 * Time.deltaTime);
                }
            }else if (songAudio.pitch < 0.01f && animator.GetCurrentAnimatorStateInfo(0).IsName("Fall"))
            {
                songAudio.Pause();
                FindObjectOfType<LevelGenerator>().isGenerating = false;
                FindObjectOfType<MusicDelayer>().isPlaying = false;

                GameObject.FindGameObjectWithTag("Processor").GetComponent<AudioSource>().Pause();
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Pause();

                foreach (RotatePath path in FindObjectsOfType<RotatePath>())
                {
                    path.speed = 0;
                }
                //Time.timeScale = 0;
            }
        }
    }

    public void Heal(int healAmount = 1)
    {
        if (_playerHealth < _playerMaxHealth && !isDead)
        {
            _playerHealth++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Obstacle")
        {
            Hurt();
        }else if (other.transform.tag == "Combo")
        {
            FindObjectOfType<MusicDelayer>().combo++;
        }
    }

    public void GoToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
        
    }
}
