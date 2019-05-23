﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : generalManager
{
    public GameObject[] tilePrefabList;
    public GameObject tilePrefab;               // Ref. MasterTile prefab
    public GameObject currentTile;              // Ref. Clones de MasterTile 

    public bool isFirst = true;
    public bool coca = true;


    private tileManagerMode stageMode;          // Ref. para pillar los valores del mundo


    public enum platType
    {
        classicY,
        classicX,
        classicZ,
        camChanger,
    }

    public platType tipo;

    public Vector3 gravitiy = new Vector3( 0, 9.8f, 0);

    public Texture[] cosmicTex;


    // Start is called before the first frame update
    void Start()
    {
        if (mode.ContainsKey("horizontal"))
            stageMode = mode["horizontal"];


        for (int i = 0; i < 5; i++) Spawner();
        isFirst = false;


        tipo = platType.classicY;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
            coca = false;
    }

    public void Spawner(){


        int rndPrefab = Random.Range(0, tilePrefabList.Length);

        if (coca)
        {
            GameObject aux = currentTile;

            int rnd = Random.Range(0, 3);
            int rndTex = Random.Range(0, 6);

            if (isFirst)
            {
                currentTile = (GameObject)Instantiate(tilePrefabList[rndPrefab], currentTile.transform.GetChild(rnd).transform.position, Quaternion.identity);

                currentTile.GetComponent<TileScript>().setPos(currentTile.transform.position);

            }
            else
            {
              
                Vector3 posOrigin = currentTile.transform.GetChild(rnd).position + new Vector3(0.0f, 4.0f, 0.0f);

                currentTile = (GameObject)Instantiate(tilePrefabList[rndPrefab], posOrigin, Quaternion.identity);
                currentTile.transform.GetComponent<TileScript>().setMode(stageMode);

                currentTile.GetComponent<TileScript>().setType(tipo);

                currentTile.GetComponent<TileScript>().setTile(aux);
                currentTile.GetComponent<TileScript>().setAttachIndex(rnd);

            }
        }
        else
        {
            // Attach al que se une la nueva pieza
            int rnd = Random.Range(3, 6);

            GameObject aux = currentTile;
            Vector3 posOrigin = currentTile.transform.GetChild(rnd).position + new Vector3(0.0f, 4.0f, 0.0f);

            currentTile = (GameObject)Instantiate(tilePrefabList[rndPrefab], posOrigin, Quaternion.identity);
            currentTile.transform.GetComponent<TileScript>().setMode(stageMode);

            currentTile.GetComponent<TileScript>().setTile(aux);
            currentTile.GetComponent<TileScript>().setAttachIndex(rnd);

            Debug.Log(rnd + " en fin " + aux.transform.GetChild(rnd).name);

            // Al marcarlo como camChanger provoca el cambio de modo
            currentTile.GetComponent<TileScript>().setType(platType.camChanger);

            // En caso de que sea 3 se generará a la izquierda, si es 4 o 5 por delante
            if (rnd == 3)
                tipo = platType.classicZ;
            else
                tipo = platType.classicX;


            coca = true; 
        }

    }
}
