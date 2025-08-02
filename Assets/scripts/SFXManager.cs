using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip punchSound;
    public AudioClip punchSound2;
    public AudioClip punchDefendSound;
    public AudioClip bellSound;
    public AudioClip hypeSound;
    public AudioClip ahhSound;
    public AudioClip knockedOutSound;
    public AudioClip outchSound;
    public AudioClip vulneSound;
    public AudioClip chargeSound;
    public AudioClip confusedSound;

    public void PlayPunchSound()
    {
        audioSource.PlayOneShot(punchSound);
    }

    public void PlayPunchSound2()
    {
        audioSource.PlayOneShot(punchSound2);
    }

    public void PlayPunchDefendSound()
    {
        audioSource.PlayOneShot(punchDefendSound);
    }

    public void PlayBellSound()
    {
        audioSource.PlayOneShot(bellSound);
    }

    public void PlayAhhSound()
    {
        audioSource.PlayOneShot(ahhSound);
    }

    public void PlayHypeSound()
    {
        audioSource.PlayOneShot(hypeSound);
    }

    public void PlayKnockedOutSound()
    {
        audioSource.PlayOneShot(knockedOutSound);
    }

    public void PlayOutchSound()
    {
        audioSource.PlayOneShot(outchSound);
    }

    public void PlayVulneSound()
    {
        audioSource.PlayOneShot(vulneSound);
    }

    public void PlayChargeSound()
    {
        audioSource.PlayOneShot(chargeSound);
    }

    public void PlayConfusedSound()
    {
        audioSource.PlayOneShot(confusedSound);
    }
}
