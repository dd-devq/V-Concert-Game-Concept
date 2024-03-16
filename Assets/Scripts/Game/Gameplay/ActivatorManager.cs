using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.MusicTheory;
using Melanchall.DryWetMidi.Interaction;
using UnityEngine.Serialization;

public class ActivatorManager : ManualSingletonMono<ActivatorManager>
{
    public List<Activator> activators;
}