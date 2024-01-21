using UnityEngine;
using UnityEngine.Audio;

namespace Sounds
{
    public class VolumeController : MonoBehaviour {
        [SerializeField] private AudioMixer mixer;

        public void SetLevel(float sliderValue) {
            mixer.SetFloat("MasterVol", Mathf.Log10(sliderValue) * 20);
        }
    }
}
