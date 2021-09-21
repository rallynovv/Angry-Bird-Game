﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public SlingShooter slingShooter;
    public TrailController trailController;
    public BoxCollider2D tapCollider;

    public List<Bird> birds;
    public List<Enemy> enemies;

    private Bird _shotBird;
    public BoxCollider2D TapCollider;
    private bool _isGameEnded = false;

    private void Start()
    {

        for (int i = 0; i < birds.Count; i++)
        {
            birds[i].OnBirdDestroyed += ChangeBird;
            birds[i].OnBirdShot += AssignTrail;
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].OnEnemyDestroyed += CheckGameEnd;
        }

        


    }

    public void ChangeBird()
    {
        tapCollider.enabled = false;
        if (_isGameEnded) return;

        birds.RemoveAt(0);

        if (birds.Count > 0)
        {
            
        }
    }

    public void CheckGameEnd(GameObject destroyedEnemy)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].gameObject == destroyedEnemy)
            {
                enemies.RemoveAt(i);
                break;
            }

            if (enemies.Count == 0)
            {
                _isGameEnded = true;
            }
        }
    }

    public void AssignTrail(Bird bird)
    {
        trailController.targetBird = bird;
        StartCoroutine(trailController.SpawnTrail());
        tapCollider.enabled = true;
    }

    void OnMouseUp()
    {
        if (_shotBird != null)
        {
            _shotBird.OnTap();
        }
    }
}