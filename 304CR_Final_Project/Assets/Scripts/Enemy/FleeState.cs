﻿using UnityEngine;
using System.Collections;

public class FleeState : EnemyState
{
    
    public FleeState(Enemy_Controller enemyController) : base(enemyController)
    {
        enemy = enemyController;
    }

    public override void updateState()
    {
        //Debug.Log("FLEEING");
        distance += enemy.speed * Time.deltaTime;
        move();

        
    }

    public void flee()
    {
        int startX, startY, endX, endY;
        startX = Mathf.FloorToInt(enemy.transform.position.x);
        startY = Mathf.FloorToInt(enemy.transform.position.z);

        endX = 17; //17,18
        endY = 18; // middle of base // Hard code for now
        Location start = new Location(startX, startY);
        Location end = new Location(endX, endY);
        //set route
        pathfinder = new AStar(grid, start, end);
        route = pathfinder.createRoute(grid, pathfinder, start, end);
        routePos = route.First.Next;
        distance = 0;
        
        previousPos = enemy.transform.position;
    }

    public override void OnTriggerEnter(Collider other)
    {
        float test = Vector3.Distance(other.transform.position, enemy.transform.position);
        Debug.Log("HEALTH TEST: " + test);
        if (other.tag == Tags.Health && Vector3.Distance(other.transform.position, enemy.transform.position) < 2)
        {
            enemy.health = 100;
            GameObject.FindGameObjectWithTag(Tags.World).GetComponent<World>().healthTriggerReset(other.gameObject);
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        
    }

    public override void OnTriggerStay(Collider other)
    {
        
    }

    public override void playerheard()
    {

    }

}
