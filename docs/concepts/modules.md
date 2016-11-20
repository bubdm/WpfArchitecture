# Modules 

A [Module](/docs/glossary/module.md) that is developed independently to other modules or the Shell. Prism's Modules are an implementation 
of modular design, that allows for modular composition of an application. 

Modules are nothing new to programming, nor are they specific to Prism. Here I will try to 
discuss Prims modules, and the application of modular programming with them.

### Module Composition

A stated, applications can be composed of losely coupled modules. TO acheive this kind of 
composability, the modules must be very indepedent of each other. Modules can contain 
any kind of logic to functionality. Modules are however capabile of communicating between 
each other. 

### Creating and using modules with Prism
 
Prism provides features for modular appication development and runtime management of modules.
Prism provides the follow ing features: 

#### [Module Catalog](/docs/glossary/module_catalog.md)

- The defition of modules in code and in Extensible Markup Language (XAML)
- The discovery of modules in defined locations, including file system locations.
- Module definition via configuration files. 

#### [On-demand Modules](/docs/gossary/module_ondemand.md)

- Dependency management, cycle detection, and module loading infrastructure.
- Support for thrid party Dependency Injection containers. 

### IModule interface

[IModule](), is thecorei nterface for defining modules. It allows for seperation of development, ttesting, and deployment. 
To initialize a module it must implement **IModule** interface. The IModule interface supports initialization and integration in the application.

In fact, implementing IModule on a class is enough to identify the class/type as a module. 
IModule has a single method called **Initialize** . This method contains any logic required to initialize and integrate
the module into the application space at large. 

In the Initialize, method, a developer can register views, make domain services available in the application,
and other functionality to extend the application.

### Module Lifecycle 

Prism maintains a module lifecycle for managing modules. 

1. Registration - runtime dicsocvery from a [module catalog](/docs/glossary/module_catalog.md)
2. Loading - After discovery, the module is loaded from a configured location, assembly, or other remote location into memory.
3. Initialization - At this point modules are initialized. After an instance is created, the IModule.Initialize method is called.

### Module Integration

While Prism provides class that can bootstrap your application, other frmaaeworkscould of course be used. 
Prism will work very well both [Unity](https://github.com/unitycontainer/unity) by providing the [UnityBootstrapper](https://msdn.microsoft.com/en-us/library/microsoft.practices.composite.unityextensions.unitybootstrapper.aspx), 
or [Mef](https://mef.codeplex.com) via the [MefBootstrapper](https://msdn.microsoft.com/en-us/library/microsoft.practices.prism.mefextensions.mefbootstrapper(v=pandp.50).aspx).