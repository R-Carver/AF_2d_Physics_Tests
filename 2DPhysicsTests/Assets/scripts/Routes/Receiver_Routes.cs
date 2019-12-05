using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receiver_Routes
{   
    public static Receiver_Routes Instance;

    public Receiver_Routes()
    {
        if(Instance == null)
        {
            Instance = this;
        }else
        {
            Debug.LogError("There shouldnt ever be 2 Receiver_Ropoute classes");
        }
        InitRoutes();
    }

    //make a dict from enum to route
    public Dictionary<RouteName, Route> routes = new Dictionary<RouteName, Route>();

    void InitRoutes()
    {   
        //forward in our world is +x and left is +y
        //use those vectors to construct the routes
        Vector2 forward = new Vector2(1, 0);
        Vector2 forward_left = new Vector2(1, 1);
        Vector2 forward_right = new Vector2(1, -1);
        Vector2 left = new Vector2(0, 1);
        Vector2 right = new Vector2(0, -1);



        //slant-left
        Vector2[] slantPoints = new Vector2[2];

        slantPoints[0] = forward;
        slantPoints[1] = forward_left;
        
        Route slantRoute = new Route(RouteName.Slant_Left, slantPoints);

        routes.Add(RouteName.Slant_Left, slantRoute);

        //--------------------------------------------------------------


    }


}

public class Route
{   
    RouteName routeName;

    //the vectors are relative
    public Vector2[] routePoints;
    int currentRoutePoint = 0;

    public Route(RouteName name, Vector2[] routePoints)
    {   
        this.routeName = name;
        this.routePoints = routePoints;
    }

    public Vector2 GetFirstRoutePoint()
    {
        currentRoutePoint++;
        return routePoints[0];
    }

    public Vector2 GetNextRoutePoint()
    {
        if(currentRoutePoint < routePoints.Length)
        {
            Vector2 outPoint = routePoints[currentRoutePoint];
            currentRoutePoint++;
            return outPoint;
        }else
        {
            return Vector2.zero;
        }
    }
    
}

public enum RouteName{Post, Slant_Left, Slant_Right, Inside_Left, Inside_Right};
