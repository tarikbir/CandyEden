using UnityEngine;

public class AudioControl : SingletonMB<AudioControl>
{
    public AudioClip Chomp;
    public AudioClip TwoHand;
    public AudioClip GiveItALick;
    public AudioClip ExpressYourself;
    public AudioClip Shield;
    public AudioClip Eye;
    public AudioClip DrinkUp;

    public AudioClip EventDraw;
    public AudioClip SugarRush;

    public AudioClip CandyHit;
    public AudioClip CookieHit;
    public AudioClip IceCreamHit;
    public AudioClip PudkingHit;

    [SerializeField] private AudioSource _soundSource;
    [SerializeField] private AudioSource _musicSource;

    private void Start()
    {
        
    }

    public void PlaySound(string sound)
    {
        switch (sound)
        {
            case "Chomp":
                _soundSource.PlayOneShot(Chomp);
                break;
            case "TwoHand":
                _soundSource.PlayOneShot(TwoHand);
                break;
            case "GiveItALick":
                _soundSource.PlayOneShot(GiveItALick);
                break;
            case "ExpressYourself":
                _soundSource.PlayOneShot(ExpressYourself);
                break;
            case "Shield":
                _soundSource.PlayOneShot(Shield);
                break;
            case "Eye":
                _soundSource.PlayOneShot(Eye);
                break;
            case "DrinkUp":
                _soundSource.PlayOneShot(DrinkUp);
                break;
            case "EventDraw":
                _soundSource.PlayOneShot(EventDraw);
                break;
            case "SugarRush":
                _soundSource.PlayOneShot(SugarRush);
                break;
            case "CandyHit":
                _soundSource.PlayOneShot(CandyHit);
                break;
            case "CookieHit":
                _soundSource.PlayOneShot(CookieHit);
                break;
            case "IceCreamHit":
                _soundSource.PlayOneShot(IceCreamHit);
                break;
            case "PudkingHit":
                _soundSource.PlayOneShot(PudkingHit);
                break;
            default:
                break;
        }
    }
}
