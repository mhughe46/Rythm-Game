using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    int _playerHealth = 0;
    [SerializeField]
    int _playerMaxHealth = 5;

    float _timeSinceHurt = -1;

    [SerializeField]
    float _healTimer = 5;
    // Start is called before the first frame update
    void Start()
    {
        _playerHealth = _playerMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        HurtTimer();
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

    public void Hurt()
    {
        _timeSinceHurt = 0;
        _playerHealth--;
    }

    public void Heal(int healAmount = 1)
    {
        if (_playerHealth < _playerMaxHealth)
        {
            _playerHealth++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Obstacle")
        {
            Hurt();
        }
    }
}
