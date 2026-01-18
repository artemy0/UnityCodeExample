# UIAnimation

`UIAnimation` is a base abstraction for UI animations in Unity.

It defines a common animation contract while keeping UI logic independent from a specific animation system.

---

## How It Is Used

- Inherit from `UIAnimation` to implement a concrete UI animation
- Implement animation logic using any system (DOTween, Feel, custom, etc.)
- Control playback through the base animation API
- Use animation events to synchronize UI logic with animation lifecycle
- UI code interacts only with `UIAnimation`, not with the underlying animation framework