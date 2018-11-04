using UnityEngine;
using UnityEditor;
using System;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour {
    private static T m_instance;

    public static T Instance
    {
        get
        {
            if(m_instance == null) {
                Type t = typeof(T);
                m_instance = (T)FindObjectOfType(t);
            }

            return m_instance;
        }
    }

    virtual protected void Awake() {
        if (m_instance != null) {
            Destroy(this);
            return;
        }
    }
}
