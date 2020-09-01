A fifth sense belt for visually impaired people which helps them avoid obstacles while moving by providing them with haptic feedback. Used Depth Stream by Kinect’s NUISensor to do a semantic segmentation for obstacles in front of the user. Used Vibrating motors(LMR) for haptic feedback and Rpi for processing and IO.

Project structure:
```
.
├── Asset
│   ├── Materials
│   ├── Prefabs
│   ├── Shaders
│   ├── Textures
├── Asset.meta
├── audio/..
├── audio.meta
├── corutine.cs
├── corutine.cs.meta
├── README.md
├── Recordings
│   ├── playback0
│   └── playback0.meta
├── Recordings.meta
├── Scene
│   ├── Sight_Scene.unity
│   └── Sight_Scene.unity.meta
├── Scene.meta
├── Script
├── Kinect
│   │   ├── KinectImgControllers
│   │   ├── KinectModelControllers
│   │   ├── KinectWrapper

```