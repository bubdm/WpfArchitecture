# Module Catalog

Defines the modules that the end user needs to run the application. 
The module catalog knows where the modules are located and 
the module's dependencies are aswell.

A module catalog is composed of [ModuleInfo](https://msdn.microsoft.com/en-us/library/microsoft.practices.prism.modularity.moduleinfo(v=pandp.50).aspx) classes.

This composition is acheived by registration and discovery. This can occur in the following ways:

1. Module registration in Code.
2. Module registration in XAML markup.
3. Module registration via app.config.
4. Discovery from disk, or remote storage location. 

If your modules are registered via configuration or xaml files, 
your project will not require references to the module implementation.

Directory discovery, can also remove the need to explicit module configuration via 
code or files. Any assemblies in the directory that contain types that implement IModule, are loadable. 
