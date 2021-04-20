using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    [SerializeField]
    public GameObject PostFX;

    void Start()
    {
        DontDestroyOnLoad(PostFX);
    }

    public void StartGame()
    {
        anim.SetTrigger("StartGame");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
