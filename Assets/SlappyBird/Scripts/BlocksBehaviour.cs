using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksBehaviour : ParallaxObject
{
    private GameManager _gameManager;
    private AudioSource _audioSource;

    void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _audioSource = GetComponent<AudioSource>();
    }

    protected override void Update()
    {
        if (startMovement)
            base.Update();
    }

    protected override void Reposition()
    {
        transform.position = new Vector2(transform.position.x, Random.Range(-2, 3));
        base.Reposition();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_gameManager.GameOverState)
        {
            _gameManager.GameScore++;
            _audioSource.Play();
            _gameManager.CalculateScore();
        }
    }
}
