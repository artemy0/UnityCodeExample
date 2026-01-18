# UI Binders (MVVM View Layer)

UI Binders act as the View layer in MVVM.

They connect ViewModels to Unity UI components while keeping ViewModels independent of Unity-specific code.

---

## How It Is Used

- Place binder components on the object or its children
- Each binder is responsible for a single binding
- To create a binder for any type, inherit from `BaseBinderComponent` or one of its more specific base classes
- The controller automatically binds and unbinds binders at runtime