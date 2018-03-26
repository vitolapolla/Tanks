// Code adapted from
// https://github.com/patrickhimes/microphone-demo


using UnityEngine;
using UnityEngine.UI; //for accessing Sliders and Dropdown
using System.Collections.Generic; // So we can use List<>


namespace Complete // Added namespace to get access to TankShooting
{

    [RequireComponent(typeof(AudioSource))]
    public class MicrophoneInput : MonoBehaviour
    {
        public TankShooting m_TankShooting;

        public float minThreshold = 0;
        public float frequency = 0.0f;
        public int audioSampleRate = 44100;
        public string microphone;
        public FFTWindow fftWindow;
        public Dropdown micDropdown;
        public Slider thresholdSlider;

        private List<string> options = new List<string>();
        private int samples = 8192;
        private AudioSource audioSource;

        void Start()
        {


            //get components you'll need
            audioSource = GetComponent<AudioSource>();

            // get all available microphones
            foreach (string device in Microphone.devices)
            {
                if (microphone == null)
                {
                    //set default mic to first mic found.
                    microphone = device;
                }
                options.Add(device);
            }

            UpdateMicrophone();
        }

        void UpdateMicrophone()
        {
            audioSource.Stop();
            //Start recording to audioclip from the mic
            audioSource.clip = Microphone.Start(microphone, true, 10, audioSampleRate);
            audioSource.loop = true;
            // Mute the sound with an Audio Mixer group becuase we don't want the player to hear it
            Debug.Log(Microphone.IsRecording(microphone).ToString());

            if (Microphone.IsRecording(microphone))
            { //check that the mic is recording, otherwise you'll get stuck in an infinite loop waiting for it to start
                while (!(Microphone.GetPosition(microphone) > 0))
                {
                } // Wait until the recording has started. 

                Debug.Log("recording started with " + microphone);

                // Start playing the audio source
                audioSource.Play();
            }
            else
            {
                //microphone doesn't work for some reason

                Debug.Log(microphone + " doesn't work!");
            }
        }

        // ADDED to demonstrate a simple application (shooting on a threshold)
        void Update()
        {
            // Check the averaged volume
            if (GetAveragedVolume() > 0.05)
            {
                // If it's above the threshold, fire the gun
                m_TankShooting.Fire(15f, 1f);
            }
        }


        public float GetAveragedVolume()
        {
            float[] data = new float[256];
            float a = 0;
            audioSource.GetOutputData(data, 0);
            foreach (float s in data)
            {
                a += Mathf.Abs(s);
            }
            return a / 256;
        }

        public float GetFundamentalFrequency()
        {
            float fundamentalFrequency = 0.0f;
            float[] data = new float[samples];
            audioSource.GetSpectrumData(data, 0, fftWindow);
            float s = 0.0f;
            int i = 0;
            for (int j = 1; j < samples; j++)
            {
                if (data[j] > minThreshold) // volumn must meet minimum threshold
                {
                    if (s < data[j])
                    {
                        s = data[j];
                        i = j;
                    }
                }
            }
            fundamentalFrequency = i * audioSampleRate / samples;
            frequency = fundamentalFrequency;
            return fundamentalFrequency;
        }
    }
}