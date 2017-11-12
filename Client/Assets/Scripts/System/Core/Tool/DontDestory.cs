using UnityEngine;
using System.Collections;

public class DontDestory : MonoBehaviour {
    void Start () {
        DontDestroyOnLoad(this);
    }
}
