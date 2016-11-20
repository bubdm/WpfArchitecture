# WPF Architecture

The intent of this repository is to provide an explanation of MVVM, WPF architecture, and implementation of these concepts with Prism. 
 
Prism is a framework for building loosely coupled, maintainable, and testable XAML applications in WPF, Windows 10 UWP, and Xamarin Forms.
 
## Concepts 

1. [Modules](/docs/concepts/modules.md)
2. [Models, Views, and View Models](/docs/concepts/mvvm.md)
3. [Navigation](/docs/concepts/navigation.md)
4. [Event Aggregation](/docs/concepts/eventAggregator.md) (in progress)
5. [Test Driven Development](/docs/concepts/tdd.md) (in progress)


## Dependencies

 All dependencies for the project will be resolved by Nuget package manager once a build is done in Visual Studio, but you can download those manually if you want.
 
 The main dependencies for the project are listed below:
 
 * [Prism Core 6](https://www.nuget.org/packages/Prism.Core/). **Nuget Command**: Install-Package Prism.Core -Version 6.1.0 
 
 * [Prism Wpf 6](https://www.nuget.org/packages/Prism.Wpf/). **Nuget Command**: Install-Package Prism.Wpf -Version 6.1.0 
 
 * [Prism Unity 6](https://www.nuget.org/packages/Prism.Unity/). **Nuget Command**: Install-Package Prism.Unity -Version 6.1.1 

## Links

1. [Prism](https://github.com/PrismLibrary/Prism). 