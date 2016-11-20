# Navigation

In this context I will discuss the conceptof navigation in a 
WPF Prism application. Specfically, what I mean by navigation is the ability for the application 
to respond to changes in UI, based on user activities.In a WPF application, we will need to continously update the UI, to enable the user 
to perfom a task, and to "touch" the data that the user is working with. 

We can further abstract the concept of navigation, to transition functions 
of a state machine, that represents the membe states bound in a View-model. 
That is to say, View-models represent indepdent and discrete state-machines.

## Navigation with View-models as State-machines

In this context, the view presents state changes (or state transition functions),
in the View-model, sometimes through User interactions with the View itself. 

The idea here, is that instead of replacing the view, view's state is simply changed. This behavior
of the application feels like navigation from a User's point of view. 

### Scenarios 

1. Displaying similar data or capability in various formats and styles.
2. Changing the layout based on a state-transition in the View-model.
3. State changes in the View-model require a type of modal interaction with the user, while keeping the same View context. 

To get the full beneift of this approach, its important to remember thata View-model represents 
a discrete use case, task, or activity.  When an application needs to display a different 
type of domain data, or when the user needs to perform a completly diffrerent task, other Views and View-models should 
be created. 

At this level, its important walk up to the next level of abstraction, which is View-based navigation.

## View-based navigation 

If we agree that each VIew-model represents a discrete use case, then what do we do with our 
many View-models representing many use-cases?  Enter View-based navgation. 

In View-based navgation, navigation in the application is replaced with a different view.
Complexity in UI and use cases is managed by thinking about the Views as States, and then applying 
state transitions in the Host Control. The Host COntrol or view, will swap out views for others. Managing 
stae at this level allows a developer to implement familiar forward, back, and other navigation metaphors. 

## Regions 

Regions cna be defined in modules, and allow a module to define specific View functionality without havinga direct reference or knowledge 
of the rest of the application space. 

Prism Regions support the composition of complex applications. That is to say, this furhters modular principles 
of lose coupling and ease of testing, when compostion an application of modules. 

Prism regions are named placeholders that can display Views. A prism region is simply a 
control that hasa RegionName attached to it. 

Region navigation is possible with the use of view injection and view discovery. 
Since Prism 4.0, regions have been extended to support more generalize routing and with URI's.
Navigation with a region is the capability of swapping a regions's view with another. The viewis identified by a 
URI, which refers to the name of the view. 

Programmatic navagation is possible in this context, by use ofthe RequestNavigate method, provided by 
the interface INavigateAsync. This interfce should be implemented on a Region class. 

![Region-navigation-sequence](https://i-msdn.sec.s-msft.com/dynimg/IC584404.png)

** [Source: MSDN Patterns and practices](https://msdn.microsoft.com/en-us/library/gg430861(v=pandp.40).aspx)

## View-models and Views with Navigation 

To implement navigation capabilites, View-models (sometimes Views), should implement the 
[INavigationAware](https://msdn.microsoft.com/en-us/library/microsoft.practices.prism.regions.inavigationaware(v=pandp.50).aspx) interface.

During navigation, Prism will check for the implementation of INavigationAware, on Views, ViewModels, or if the 
interface is implemented on the DataContext member. Prism will call the methods exposed by INavigationAware interface
during navigation. 










