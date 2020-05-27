﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    private bool _playerCollide;
    private bool _notTranslate;
    public GameObject player;
    void Start()
    {
        _playerCollide = false;
        _notTranslate = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerCollide)
        {
            var i = 0;
            bool allCutoff1 = true;
            while (i < 4 && _notTranslate)
            {
                player.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials[i].SetFloat( "_Cutoff", Mathf.Lerp(player.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials[i].GetFloat("_Cutoff"), 1f, Time.deltaTime * speed));
                if (player.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials[i].GetFloat("_Cutoff") < 0.15f)
                {
                    allCutoff1 = false;
                }
                i += 1;
            }

            if (allCutoff1)
            {
                player.transform.position = new Vector3(20f, 0f, 15f);
                _playerCollide = false;
                _notTranslate = false;
            }
        }
        else if (!_notTranslate && player.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials[2].GetFloat("_Cutoff") > 0f)
        {
            int i = 0;
            while (i < 4)
            {
                player.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials[i].SetFloat( "_Cutoff", Mathf.Lerp(player.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials[i].GetFloat("_Cutoff"), 0f, Time.deltaTime * 10*speed));
                i += 1;
            }

            if (player.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials[2].GetFloat("_Cutoff") -
                0.03f < 0)
            {
                player.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials[2].SetFloat("_Cutoff", 0f);
            }
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collide gate"); 
        if (other.gameObject == player)
        {
            // other.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials[2].shader = Shader.Find("Unlit/Transparent");
            _playerCollide = true;
            // other.gameObject.GetComponent<Renderer>().material.color = Color.Lerp(other.gameObject.GetComponent<Renderer>().material.color, new Color(1, 1, 1, 1), Time.deltaTime * Speed);

        }
    }
}