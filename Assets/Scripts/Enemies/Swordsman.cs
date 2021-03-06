﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordsman : MonoBehaviour {

    public GameObject damageBox;
    public float attackTime = 3f;
    public float attackDuration = 1f;
    public float velocity = 2f;

    float elapsedTime;
    bool attacking;

    Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
        int waveCount = EnemySpawner.instance.waveCount;
        if (waveCount > 3)
            attackDuration = 1.5f;
        if (waveCount > 5)
            attackDuration = 2f;
        if (waveCount > 10)
            attackDuration = 3f;
        if (waveCount > 20)
            attackDuration = 5f;
    }

    void Update() {
        elapsedTime += Time.deltaTime;
        if (attacking && elapsedTime >= attackDuration)
            EndAttack();
        else if (!attacking && elapsedTime >= attackTime)
            StartAttack();
        if (GetComponent<HitPoints>().Died()) {
            if (Katarina.instance) {
                Katarina.instance.GetComponent<KatarinaShunpo>().cooldown += Katarina.instance.killCDReduction;
                Katarina.instance.GetComponentInChildren<KatarinaDeathLotus>().cooldown += Katarina.instance.killCDReduction;
            }
            KillCount.instance.IncreaseCount();
            GetComponent<Collider2D>().enabled = false;
            enabled = false;
        }
    }

    void FixedUpdate() {
        if (attacking)
            MoveTowardsPlayer();
    }

    void StartAttack() {
        attacking = true;
        animator.SetTrigger("attack");
        elapsedTime = 0;
        damageBox.SetActive(true);
        GetComponent<HitPoints>().invincible = true;
    }

    void EndAttack() {
        attacking = false;
        animator.SetTrigger("endAttack");
        elapsedTime = 0;
        damageBox.SetActive(false);
        GetComponent<HitPoints>().invincible = false;
    }

    void MoveTowardsPlayer() {
        if (Katarina.instance) {
            var distance = Katarina.instance.transform.position.x - transform.position.x;
            var position = transform.position;
            position.x += (velocity * Time.deltaTime * Mathf.Sign(distance));
            transform.position = position;
        }
    }
}
