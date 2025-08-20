using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Insstance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<T>();
                if (_instance == null)
                {
                    return null;
                }
            }
            return _instance;
        }
    }
}
