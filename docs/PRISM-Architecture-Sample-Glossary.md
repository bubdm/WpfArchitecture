#Glossary#
List of main terms that appear in the project's documentation.

* **Bootstrapper**. The class responsible for the initialization of an application built using the Prism Library.
* **DelegateCommand**. Allows delegating the commanding handling logic to selected methods instead of requiring a handler in the code-behind. It uses .NET Framework delegates as the method of invoking a target handling method.
* **EventAggregator**. A service that is primarily a container for events that allows publishers and subscribers to be decoupled so they can evolve independently. This decoupling is useful in modularized applications because new modules can be added that respond to events defined by the shell or other modules.
* **Model**. Encapsulates the application's business logic and data.
* **ModuleCatalog**. Defines the modules that the end user needs to run the application. The module catalog knows where the modules are located and the module's dependencies.
* **ModuleManager**. The main class that manages the process of validating the module catalog, retrieving modules if they are remote, loading the modules into the application domain, and invoking the module's Initialize method.
module management phases. The phases that lead to a module being initialized. These phases are module discovery, module loading, and module initialization.
navigation. The process by which the application coordinates changes to its UI as a result of the user's interaction with the application, or as a result of internal application state changes.
* **Notifications**. Provide change notifications to any data-bound controls in the view when the underlying property value changes. This is required to implement the MVVM pattern and is implemented using the BindableBase class.
* **On-demand module**. A module that is retrieved and initialized only when it is explicitly requested by the application.
* **Region**. A named location that you can use to define where a view will appear. Modules can locate and add content to a region in the layout without exact knowledge of how and where the region is visually displayed. This allows the appearance and layout to change without affecting the modules that add the content to the layout.
* **RegionContext**. A technique that can be used to share context between a parent view and child views that are hosted in a region. The RegionContext can be set through code or by using data binding XAML.
* **RegionManager**. The class responsible for maintaining a collection of regions and creating new regions for controls. The RegionManager finds an adapter mapped to a WPF control and associates a new region to that control. The RegionManager also supplies the attached property that can be used for simple region creation from XAML.
* **Shell**. The main window of a WPF application where the primary UI content is contained.
* **Service**. A service provides functionality to other modules in a loosely coupled way through an interface and is often a singleton.
* **View**. The main unit of UI construction within a composite UI application. The view encapsulates the UI and UI logic that you would like to keep as decoupled as possible from other parts of the application. You can define a view as a user control, data template, or even a custom control.
* **View model**. Encapsulates the presentation logic and state for the view. It is responsible for coordinating the view's interaction with any model classes that are required.

