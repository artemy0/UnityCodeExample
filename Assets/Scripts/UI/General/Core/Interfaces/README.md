# IUIFactory

`IUIFactory` is an abstraction for creating UI elements in Unity.

It defines a single entry point for instantiating screens, popups, and widgets while hiding asset loading and dependency resolution details.

---

## How It Is Used

- UI flow systems request screens, popups, or widgets from the factory
- The factory implementation loads UI assets via Addressables
- Dependencies are resolved internally using VContainer
- Widgets are explicitly released through the factory when no longer needed

UI code depends only on `IUIFactory`, not on concrete creation logic.
