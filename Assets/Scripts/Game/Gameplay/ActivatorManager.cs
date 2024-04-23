using System;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorManager : ManualSingletonMono<ActivatorManager>
{
    public Material DefaultMaterial = null;
    public Material HitMaterial = null;
    private List<Activator> _activators = new();
    private Dictionary<NoteName, int> _pitchNameDict = new();
    public List<Activator> Activators
    {
        get => _activators;
        set => _activators = value;
    }
    public override void Awake()
    {
        base.Awake();
        GetListActivators();
    }
    private void Start()
    {
        foreach (Activator activator in _activators)
        {
            var endZone = activator.EndZone;
            var playerModel = GamePlayManager.Instance.PlayerModel;
            var distance = Vector3.Distance(activator.StartZone.transform.position, activator.EndZone.transform.position);
            var direction = (activator.EndZone.transform.position - activator.StartZone.transform.position).normalized;
            endZone.transform.position = activator.transform.position + direction * distance * 0.5f;
            //endZone.SetActive(false);
        }
    }

    private void Update()
    {
        //throw new NotImplementedException();
    }
}