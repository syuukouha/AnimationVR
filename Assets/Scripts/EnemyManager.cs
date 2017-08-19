using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager> {
    public Enemy[] Goblins;

    private bool timerStart = false;
    private float timer = 0;
    private float attackTimer = 5.0f;
    private int index = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            timerStart = true;
        }
        if (timerStart)
            timer += Time.deltaTime;
        if(timer>=attackTimer)
        {
            timer = 0;
            timerStart = false;
            Attack(1);
        }
	}
    public void Attack(int id)
    {
        switch (id)
        {
            case 1:
                Goblins[index++].Attack();
                if (index >= Goblins.Length)
                    index = 0;
                break;
            default:
                break;
        }
    }
}
