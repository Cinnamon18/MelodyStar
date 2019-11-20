# unity-fluidsynth

A Unity3D wrapper for [FluidSynth](http://www.fluidsynth.org/).
Designed with love for the 
[MelodyStar](https://github.com/Cinnamon18/MelodyStar)
rhythm game!

---

### Installation

Drag this package into a Unity project and it should import itself.

Currently only available on Windows, but FluidSynth is cross-platform
so we can add other binaries in the future.

---

### Usage

To produce sound, either a SoundFont2 or SoundFont3 file must be loaded
from a `StreamingAssets` directory. We tested with
[GeneralUser GS 1.471](http://www.schristiancollins.com/generaluser.php).

The API is located in [`src/FSMiddlewareAPI.cs`](src/FSMiddlewareAPI.cs).

An example MonoBehaviour is included: [`MIDIExample.cs`](MIDIExample.cs).
Also included is an example scene, but make sure that the component
references the correct path to the sound font.

---

### Significance

This wrapper for the FluidSynth C library provides a reliable method to
receive MIDI input, manage sound fonts, and render/output sound.

---

### How

We collected the FluidSynth C library and its dependencies,
imported them as plugins, and wrote a C# wrapper for the API.

The wrapper manages the lifecycle of any memory pointers
to prevent memory leaks.

---

### TODO

* MacOS, Linux libraries

---

### Authors

Name | Email
-----|------
Lilly Ada Rizvi | <m.rizvi@gatech.edu>
