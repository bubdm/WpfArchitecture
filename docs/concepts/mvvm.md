# MVVM

[MVVM]() is a pattern or composite set of patterns, who's purpose is to provide a clean way
to abtract over the various parts of logic to build a GUI driven application in a modular and testable way. 

Using this pattern, the UI of an application, the presetnation logic, and underlying domain logic 
are seperated into the following types: 

1. [View]() - Encapsulates the UI and UI relted logic.
2. [ViewModel]() - Encapsualtes presentation lgoic and state.
3. [Model]() - Which provides the domain specific logic and data.

## Interactions between the MVVM Types

View Types (or classes), interact with View Model Types. This interaction is perfomed via 
data-binding, commands, and change notification events. 

View-Model types, interacts by querying, observations, and cooridation of updates to model.
View-Models also handle the conversation, validation, and aggregation of data as needed for proper
rendering in the View. 

View-Models subscribe to changes in models. View-models also notify the view that there is a change in the data (the model),
and the View is able to render this change. this is thefundaemtnal way the data-binding in the View can occur. 

Commands provide a wa to send user actions to the ViewModel from the view, that can cause changes in the data, and thus changes to the View. 

## Views

Views define the strucutre of what the user sees and interacts with. (UI). View types, can 
represent windows, pages, user controls, template-based data, and other visual elements.

Views maintain a reference to the ViewModel via the DataContext member. UI Elements are data-bound to the members and behaviors (properties and commands) provided by the View-model.

## View Models

View-model classes do not dervie from any WPF Base class. View-moels are an intersitng type, because they represent a discrete 
use case or user task in the application. For this reason, View-models are testable independeny of any Views or Models. To maintain this independence, 
the view-model should not directly reference the View. It does however implement members such as Properties or Commands, that a View can bind to. 
View-models can notify a View, of any changes in state (data), via the events provided in INotifyPropertyChanged  and INotifyCollectionChanged interfaces. 
This mechanism provides the oppurtunity to a developer to implement states and state transitions that are represented to a User via the View.

View-models also facilitate interaction between the View and the Model. Besides the obvious purposes of 
data manipulation, the View-model represnets an abstraction that allows the Model to be truly independent of a View. In my opinion, 
the unique benefits of MvvM are possible because of this. 

## Models

The purpose of Models are immediately understood by anyone who has ever implemented other patterns such as MVP or MVC. 
In MVVM, a model can have other purposes. Models encapsualte domain logic and data structures. In this context, 
Dom,ain logic is really anything that concerns the capabilities of managing domain data. In am MVVM (or especially in a WPF application), 
the Model is a representation of the client-side domain model. 

To compose a Model, there are a few helpful interfaces. Like we have seen before, the interfaces INotifyPropertyChanged and INotifyCollectionChanged (for aggregates),
are used. Models that implement these interfaces allow notifications that make it easy to bind to the view. In addtion to these intrefaces, a Model can inherit ObservableCollection<T> (which gives you an implementation of INotifyCollectionChanged ). 

Models can provide logic in addtion to data. For example a validation and error checking can be 
acheived with the implementation of the interfaces IDataErrorInfo or INotifyDataErrorInfo. 

Model classes do not directly reference Views or Models, and can be implemented in any way. 
This loose compling again provides the oppuruntiy to indenpdently test the implementation of the 
domain logic. 