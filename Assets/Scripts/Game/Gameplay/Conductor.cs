public class Conductor : ManualSingletonMono<Conductor>
{
    public float bpm;
    public float crotchet;
    public float offset;
    public float errorThreshold;
    public float songPosition;
    
    private bool _songStarted;


    private void Start()
    {
        _songStarted = false;
    }

    private void Update()
    {
        if (_songStarted)
        {
        }
    }
}