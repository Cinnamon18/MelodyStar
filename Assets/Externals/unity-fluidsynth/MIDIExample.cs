using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluidSynth;

public class MIDIExample : MonoBehaviour {
    MiddlewareAPI midiSys;

    // Max # of channels in General MIDI.
    const int max_chn = 16;
    
    // Current channel to play on.
    int chn = 0;

    public String streamingSoundFontPath = "soundfonts/*.sf2";

    public int[] channelInstruments = new int[16] {
        0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15
    };
    
    void OnEnable() {
        midiSys = new Middleware();
        
        var sfont = midiSys.LoadSoundFont(streamingSoundFontPath);

        var n = channelInstruments.Length;
        for (var i = 0; i < n; i++)
            midiSys.SetChannelInstrument(i, sfont, 0, channelInstruments[i]);

        OnMIDI onMIDI = (MIDINote midi, object env) => {
            midi.chn = ((MIDIExample) env).chn;
            return midi;
        };

        midiSys.SetOnMIDIDevicePress(onMIDI, this);
        midiSys.SetOnMIDIDeviceRelease(onMIDI, this);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            if (chn < max_chn - 1) {
                chn++;
                Debug.Log(String.Format("MIDI channel changed to: {0}", chn));
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            if (chn > 0) {
                chn--;
                Debug.Log(String.Format("MIDI channel changed to: {0}", chn));
            }
        }
    }
}
