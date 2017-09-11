using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour {

    public static Managers ins;

    void Awake()
    {
        ins = this;
        path = GetComponent<PathManager>();
        grid = GetComponent<GridManager>();
        fx = GetComponent<FXManager>();
        gui = GetComponent<GUIManager>();
        log = GetComponent<LogicManager>();
    }

    public PathManager path;
    public GridManager grid;
    public FXManager fx;
    public GUIManager gui;
    public LogicManager log;
}
