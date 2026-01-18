# BaseViewModel (MVVM ViewModel Layer)

`BaseViewModel` is the **ViewModel layer** in the MVVM architecture used in this project.  
It exposes observable properties and bindable methods for UI to consume via binders.

The code generator is included in a separate DLL. It works automatically,  
generating the necessary observable properties and method references without manual intervention.

---

## How It Is Used

- Inherit from `BaseViewModel` to create a concrete ViewModel for a UI element
- Define observable properties and bindable methods
- ViewModel is attached to a GameObject along with a `UIBinderController`
- UI binders use the ViewModel to read state and invoke actions