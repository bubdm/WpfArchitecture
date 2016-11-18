#PRISM Architecture Sample#
The intent of this document is to follow the flow of the commits done in the PRISM Architecture sample application in the repository located at [Prism Architecture Sample](https://acrowire.visualstudio.com/_git/PRISM%20Architecture%20Sample?path=%2FTimesheet&version=GBmaster&_a=contents).

Commits ** *Initial structure* ** and ** *Add initial projects* ** just initialize a basic project and folder structures to build the test application.

It is highly recommended to clean the solution and build again every time a commit is checkout in order to avoid old dlls and see the new functionality working for the commit.

The [Glossary of Principal Terms](/docs/PRISM-Architecture-Sample-Glossary.md) is a list of main definitions that appear in this document. A complete list of terms can be found [here](https://github.com/PrismLibrary/Prism/blob/master/Documentation/WPF/AppendixA-Glossary.md#glossary-for-the-prism-library-for-wpf).

##Create a Shell##
The first step in the Prism Architecture is create a [Shell](https://github.com/PrismLibrary/Prism/blob/master/Documentation/WPF/10-InitializingPrismApplications.md#creating-the-shell) window that will hold Regions inside. The Shell is similar to the MainWindow in a classic WPF application.
1. Commit [Add and initialize shell](https://acrowire.visualstudio.com/_git/PRISM Architecture Sample/commit/ada0a956186801eb4eb54169e103a16d57444755).

    * The **Timesheet.Application** project defines a Window called Shell. This window only has a simple TextBlock on it with the text “Initial Shell”.
    
    * The **TimesheetBootstrapper** class is a simple class that derives from **UnityBootstrapper** and in order to initialize the Shell, it overrides **CreateShell** and **InitializeShell** methods. (In future commits this Bootstrapper also will override more methods). Since it derives from UnityBootstrapper, we can use the Container to resolve the Shell in the CreateShell method. More information about Bootstrapper [here](https://github.com/PrismLibrary/Prism/blob/master/Documentation/WPF/10-InitializingPrismApplications.md#what-is-a-bootstrapper).
    
    * The initialization of the Shell is done by the InitializeShell method. This method sets the Current MainWindow of the application and shows it.
    
    * Finally, in the **App.xaml** file, the `StartupUri="MainWindow.xaml"` needs to be removed.
    
    * The result of this commit is a Simple Shell initialized in the Prism style.
    
    ![Create Shell](/docs/images/CreateShell.png)
 
##Define Regions and Add Regions to the Shell##
Regions are placeholders where UI content can be put. Detailed information about Regions can be found [here](https://github.com/PrismLibrary/Prism/blob/master/Documentation/WPF/50-ComposingtheUserInterface.md#regions). 
1. Commit [Add regions to the shell](https://acrowire.visualstudio.com/_git/PRISM Architecture Sample/commit/0a3ba109128b522db2b71f7c2333469189bf55f5).

    * In this commit the content of the Shell is replaced by two Regions.
    
    * To define a Region inside the Shell, the Window adds a namespace for the Prism Library.  
        `xmlns:prism="http://prismlibrary.com/"`
        
    * Regions are defined using the prism namespace like this:  
        `prism:RegionManager.RegionName="{x:Static NAME_OF_THE_REGION}"`
        
    * This commit also includes a Timesheet.Infrastructure project where we will store the name of the Regions like constants and to be commonly used by other project/modules.  

      We use the namespace for the infrastructure project in the Shell like this:  
        `xmlns:inf="clr-namespace:Timesheet.Infrastructure;assembly=Timesheet.Infrastructure"`  

      And finally defined two Regions, like this:  
        `<ContentControl prism:RegionManager.RegionName="{x:Static inf:RegionNames.ToolbarRegion}" />`  
        `<ContentControl prism:RegionManager.RegionName="{x:Static inf:RegionNames.ContentRegion}" Grid.Row="1" />`
        
    * The result of this commit is a Simple Shell with two Regions on it.
    
    ![Define Regions](/docs/images/CreateRegions.png)
 

##Define a Module##
A [Module](https://github.com/PrismLibrary/Prism/blob/master/Documentation/WPF/30-ModularApplicationDevelopment.md#imodule-the-building-block-of-modular-applications) is a piece of code (library, user control library, etc.) that is developed independently to other modules or the Shell.

1. Commit [Add MainContent module](https://acrowire.visualstudio.com/_git/PRISM Architecture Sample/commit/4cb0e9e0aa83752db5f8841e49a1b44b24536ad8).

    * This commit has defined a Module in the Modules folder. This Module is a project of type User Control class library that has two Views(User Controls) in the Views folder.
    
    * The project contains also a **MainContentModule** class that will be the responsible to Initialize the Module when it is needed. The class must Implement **IModule** interface.
    
    * The result of this commit is a Simple Shell with two Regions on it and since it only defines the module, nothing is different from the previous result.

##Module Initialization##
To initialize a module it must implement **IModule** interface. Complete information can be found [here](https://github.com/PrismLibrary/Prism/blob/master/Documentation/WPF/30-ModularApplicationDevelopment.md#defining-a-module).
 
1. Commit [Initalize module and register views into regions of the shell](https://acrowire.visualstudio.com/_git/PRISM Architecture Sample/commit/a824ca79a05d5fcaab9687457a228aea26f4d778).

    * The **MainContentModule** class defines a constructor with two parameters that will be resolved by Unity. The important dependency at this point is the **RegionManager**, because it will provide us a mechanism to register the Module Views inside Regions.
    
    * In the Initialize method, the Module registers two Views into the Regions, matching by Region Name property through the **RegisterViewWithRegion** method. 
        `_regionManager.RegisterViewWithRegion(RegionNames.ToolbarRegion, typeof(Views.Toolbar));`
        `_regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(Views.GridContent));`
    
    * The result of this commit is a Simple Shell with two Regions on it and since it only defines the module, and registers the Views into the Regions, nothing is different from the previous result.

##Discover and Load Modules##
There four ways to Discover a Module. Discover modules from directory and Discover Modules from app.config are the most loose coupling approaches. A complete information of all the methods to discover a module can be found [here](https://github.com/PrismLibrary/Prism/blob/master/Documentation/WPF/30-ModularApplicationDevelopment.md#registering-and-discovering-modules).

1. Commit [Discover module and load module](https://acrowire.visualstudio.com/_git/PRISM Architecture Sample/commit/cc183f7f7b33b54769a986712a4c02bba34859fe).

    * To Discover a Module at Runtime using the app.config approach, we need to add a modules section like this:  
        `<configSections>`  
        `<section name="modules" type="Prism.Modularity.ModulesConfigurationSection, Prism.Wpf"/>`  
        `</configSections>`

    * Then we need to add the desired Modules adding the path of the assembly file, the fully qualified name, the assembly version and the Module Name.  
        `<modules>`  
        `<module assemblyFile="Modules/MainContent.dll" moduleType="MainContent.MainContentModule, MainContent, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" moduleName="MainContentModule" startupLoaded="true"/>`        
        `</modules>`

    * The **startupLoaded** option at the end, tells the application to load the Module when it is available, otherwise, it will load the Module **OnDemand** mode. More information for On-Deman Loading [here](https://github.com/PrismLibrary/Prism/blob/master/Documentation/WPF/30-ModularApplicationDevelopment.md#loading-modules-on-demand).
    
    * Since the assemblyFile option is set to the Modules directory, we need to copy the dll files from the Module once the build is done to the Modules directory.
        In order to automatize this copy, the Module defines a PostBuild command.
        `xcopy "$(TargetDir)*.*" "$(SolutionDir)Src\Timesheet.Application\bin\$(ConfigurationNam*\Modules\" /Y`

    * Finally, the TimesheetBootstrapper class needs to override the CreateModuleCatalog method in order to tell the application we will discover modules from app.config, like this:  
        `return new ConfigurationModuleCatalog();`

    * The result of this commit is a Simple Shell Window with two Regions on it both of them showing the Views from MainContent Module.
    
    ![Create Module](/docs/images/CreateModules.png)
 
##Add StatusBar Module##
At this point, a StatusBar functionality is added to the application.

1. Commit [Add StatusBar module](https://acrowire.visualstudio.com/_git/PRISM Architecture Sample/commit/eef3a6d9fee2cec01b2b57d8c3aa23af1fa32a34).

    * A StatusBar User Control library project is added to the Modules folder in the Solution.
    
    * This project represents another module with just one View in the Views folder. The content of the view is just a simple text “Initial Status”.
    
    * Like steps done before, it defines a **StatusBarModule** class which implements **IModule** interface, in which the Initialize method registers the View in a Region called **StatusBarRegion**.
    
    * The Shell window needs to define a new Region with the name **StatusBarRegion**.
    
    * Next, the app.config must have another entry in the modules section in order to discover the Module and Load it, like this:  
        `<module assemblyFile="Modules/StatusBar.dll" moduleType="StatusBar.StatusBarModule, StatusBar, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" moduleName="StatusBarModule" startupLoaded="true"/>`

    * Finally, this new StatusBar project has the PostBuild command, like this:  
        `xcopy "$(TargetDir)*.*" "$(SolutionDir)Src\Timesheet.Application\bin\$(ConfigurationNam*\Modules\" /Y`
        
    * The result of this commit is a Simple Shell Window with three Regions on it all of them showing the Views from MainContent Module and StatusBar Module.
    
    ![Create Module](/docs/images/CreateStatusBarModule.png)
 
##Create View Models and Attach them to the Views.##
Prism has by convention a simple way to attach Views with his ViewModels.

1. Commit [Create ViewModels and attach to the Views by Prism convention](https://acrowire.visualstudio.com/_git/PRISM Architecture Sample/commit/8774e6fbbaa8141b388becb71230af491f59afc5).

    * Following Prism conventions, each view must go to the Views Folder and each correspondent view model must go to the ViewModels folder. Technically, the namespace matters here.
    
    * The name of the views does not matter but the names of the view models must end in “ViewModel” text. For example: 
        * GridContent (View)
        * GridContentViewModel (ViewModel)
         
    * Using this conventions allow us to use the **ViewModelLocator.AutoWireViewModel** feature of Prism.  
        In the View, we need to define the prism namespace, like this:  
        `xmlns:prism="http://prismlibrary.com/"` 
         
        Then use the feature, like this:  
        `prism:ViewModelLocator.AutoWireViewModel="True"`  
        
        Then Prism automatically will hook up the View and the ViewModel by convention.
        
    * Each ViewModel class should inherit from **BindableBase** class, which provide us the implementation of INotificationPropertyChanged and useful shortcuts to work with ViewModels.
    
    * At this point, the application should have a ViewModel for each View in each Module and they should be using the convention name of Prism.
    
    * The result of this commit is a Shell with three Regions in there with data coming from ViewModels, which show us the relationship between Views and ViewModels were successful done.
    
    ![Create Module](/docs/images/CreateViewViewModels.png) 

##Communicate Modules using Event Aggregator feature of Prism##
An easy way to communicate modules in a loose coupled way is to use the event aggregator of Prism, which is basically publish and subscribe.

1. Commit [Attach command to toolbar button and use event aggregator of Prism to communicate modules](https://acrowire.visualstudio.com/_git/PRISM Architecture Sample/commit/914c14b45911dffdc7ec761a179e31f90b5df7fe).

    * First of all, Timesheet.Infrastructure project must define a **StatusUpdatedEvent** class that inherits from **PubSubEvent<T>** where T will be the type of the payload.
    
    * The **ToolbarViewModel** needs the **IEventAggregator** dependency in his constructor. Since we are using Unity, it will resolve the dependency automatically because it knows that dependency inside of Prism.
    
    * In the **ToolbarViewModel** a **DelegateCommand** is defined, with the name **RequestData**, which will call the **GetTimesheetData** method every time the Request Button in the view is clicked.
     
    * The GetTimesheetData method uses the eventAggregator member to retrieve the **StatusUpdatedEvent** event and the Publish to the subscribers with the payload defined, in this case a simple string.
    
    * In the **StatusBarViewModel** class, we also need the **IEventAggregator** dependency and once it’s assigned in the constructor, the ViewModel subscribes to the Event of type **StatusUpdatedEvent**, which means every time a Publish occurs for this type of Event, StatusBarViewModel will be informed and then raise the attached method, in this case **StatusUpdated** method receiving the payload sent.
    
    * The result of this commit is a Shell with three Regions in there with data coming from ViewModels, and once the toolbar Request button is clicked, the information in the Status bar will change.
    
    ![Create Module](/docs/images/ComunicateModules.png)  

##Communicate with Timesheet Acrowire web service and update Grid using Event Aggregator##
With the Event Aggregator in place for the status module, the application communicates with the web api of Acrowire and retrieves the timesheet data for a given Email using the Request button of the toolbar. More information about Event Aggregation can be found [here](https://github.com/PrismLibrary/Prism/blob/master/Documentation/WPF/70-CommunicatingBetweenLooselyCoupledComponents.md#event-aggregation).

1. Commit [Add data retrieve logic from service](https://acrowire.visualstudio.com/_git/PRISM Architecture Sample/commit/fc955d365df964dc9e580953e18c21f83968efd8). 

    * At this point the application has a Timesheet.Data project responsible to execute the request and retrieve data from web api service. Timesheet.Infrastructure also defines needed interface and module for communication to the server (ITimesheetService, TimesheetData).
    
    * Like before, Timesheet.Infrastructure defines a new Event of type **TimesheetUpdatedEvent** with a payload of type **TimesheetData**, which we will use to communicate the GridContentViewModel once a request is done using event aggregator approach.
    
    * **GetTimesheetData** in the **ToolbarViewModel** class does the Request for data and then Publish to subscribers of type **TimesheetUpdatedEvent** and **StatusUpdatedEvent**.
    
    * GridContentViewModel is subscribed to the Events of type **TimesheetUpdatedEvent** in his constructor, so once a request is done, the Grid is updated with the data.
    
    * Of course we need to resolve the dependency for the **TimesheetService**, in the ToolbarViewModel class. That’s done in the TimesheetBootstrapper where we tell the containter how to resolve it.
    
    * The result of this commit is a Shell with three Regions in there with a functional “Request” button that makes a call to the service and then through the Event Aggregator infrastructure, notify the Grid to update.
    
    ![Create Module](/docs/images/EventAggregator.png)  

##Use new features of Prism 6.0 for validation##
Some features changed from Prism 5.0 to 6.0 and one very useful is the **ObservesProperty**.

1. Commit [Add validation to make a request using ObservesProperty of Prism](https://acrowire.visualstudio.com/_git/PRISM Architecture Sample/commit/5718acd919f68b4d2049d7c8dfb4e0ee3e01ac69).

    * In the **ToolbarViewModel**, the DelegateCommand RequestData now implements a mechanism to validate if it can execute the command or not, observing desired properties, so instead of just passing the method to execute, we also pass the method to determine if it can execute the command with a custom logic and moreover we set the ObservesProperty for Email, StartDate and EndDate because the logic for CanExecuteRequest method will need to evaluated every time these properties changed their values.
    
    * The result of this commit is a Shell with three Regions in there with a functional “Request” button that is disabled by default because of logic implemented to validate the Request.
    
    ![Create Module](/docs/images/NewFeatures.png)
 

##Implement Notifications to the user using the Notification infrastructure of Prism##
In order to show messages, interaction with the user with yes/no modals or to implement a custom view to show notifications, we use the Interactivity.InteractionRequest namespace of Prism.

1. Commit [Implement notification use the notification infrastructure of Prism](https://acrowire.visualstudio.com/_git/PRISM Architecture Sample/commit/550d43de8b65a7792af610576ee743abf248aecd).

    * The **ToolbarViewModel** now implements a mechanism to inform the user no data came from server. This approach does not break the MVVM pattern and it is easy to implement.
    
    * First we define a member of type `InteractionRequest<INotification>`
    
    * Then initialize it in the constructor.
    
    * In the **GetTimesheetData** method, if the data is 0, we raise a Notification with a Title and a Content to the user.
    
    * In the Toolbar View we need to define the interactivity namespace, like this:  
        `xmlns:i="http://schemas.microsoft.com/expression/2010/interactivity"`
        
    * Next, we create a trigger to tie to the NotificationRequest of the ViewModel and lastly raise a modal with some options, like this:  
        `<i:Interaction.Triggers>`  
        `<prism:InteractionRequestTrigger SourceObject="{Binding NotificationRequest}">`  
        `<prism:PopupWindowAction IsModal="True" CenterOverAssociatedObject="True"/>`  
        `</prism:InteractionRequestTrigger>`  
        `</i:Interaction.Triggers>`

    * The result of this commit is a Shell with three Regions in there with a functional “Request” that will inform the user if data coming from server is empty.
    
    ![Create Module](/docs/images/Notifications.png)
 

##Implement Nagivation functionality using NavigationAware interface of Prism.##
A key concept in Prism is the Navigation. Complete information about Navigation in Prism can be found [here](https://github.com/PrismLibrary/Prism/blob/master/Documentation/WPF/60-Navigation.md#navigation-using-the-prism-library-for-wpf).
1. Commit [Implement nagivation functionality using NavigationAware interface of Prism](https://acrowire.visualstudio.com/_git/PRISM Architecture Sample/commit/1f723c12cede0e3d18693319b9e6229638d921b8).

    * At this point, in the MainContent module we create a couple of Views called **InitialToolbar** and Members. Both of them with his ViewModels defined by convention **InitialToolbarViewModel** and **MembersViewModel**.
    
    * We define an Interface and implement it in **TimesheetMemberService** class to retrieve Members from web api service of Acrowire.
    
    * We also define a model for the TimesheetMember and we create a **ViewNames** static class to define View names just like RegionNames done before.
    
    * In the **Initialize** method of MainContentModule class, instead of register a view with a Region, we register View Names to the Views and then Navigate to the desired ones. (i.e. I want to navigate to InitialToolbar First and Members View first).  
        ** *A key concept of Regions is that it can store many views on it, but only one will be shown if there are many. The Activated view of the Views collection will be the one shown.* **  
        In the next lines basically we are telling the app to Navigate to the InitialToolbar View for the ToolbarRegion and MembersView for the ContentRegion. If they don’t exists, they will be created and inject to the Regions.  

        `_regionManager.RequestNavigate(RegionNames.ToolbarRegion, ViewNames.InitialToolbarView);`  
        `_regionManager.RequestNavigate(RegionNames.ContentRegion, ViewNames.MembersView);`
        
    * The **InitialToolbarViewModel** has a DelegateCommand member that is bound to the “Request Info” button of the View. This command performs the **RequestInformation** method and inside of it another couple of Navigation happens through the regionManager with the second one sending information about the Member selected.  
        `_regionManager.RequestNavigate(RegionNames.ContentRegion, new Uri(ViewNames.GridContentView, UriKind.Relativ*);`  
        `_regionManager.RequestNavigate(RegionNames.ToolbarRegion, new Uri(ViewNames.ToolbarView, UriKind.Relativ*, parameters);`
        
    * In order to capture the sent information in the navigation, a class must implement **INavigationAware**.
    
    * ToolbarViewModel implements INavigationAware which has three methods on it.
        * **OnNavigatedTo**: Occurs when a navigation is coming from outside the class.
        * **IsNavigationTarget**: Used to determine if the instance will be shown basic in a custom logic.
        * **OnNavigatedFrom**: Occurs when a navigation is made from the class to outside.
        
        At this point, just the OnNavigatedTo is implemented and this just captures the information sent by the InitialToolbar.
        
    * The result of this commit is a Shell with three Regions in there with a functional “Request Info” button that navigate to next views.
    
    ![Create Module](/docs/images/Nagivation_1.png)
    
    ![Create Module](/docs/images/Nagivation_2.png)

##Implement navigation back to the List of members.##
Scenario: If I selected the wrong member, I need to go back to the list of the Members.

1. Commit [Implement navigation back to the List of members](https://acrowire.visualstudio.com/_git/PRISM Architecture Sample/commit/438fc05a44238f6aa2566c3e2fe00117bf6859c9).

    * In order to go back to the list of Members, we define a “List of Members” button in the Toolbar View.
    
    * The button is bound to the **MembersCommand** DelegateCommand member.
    
    * Once the **NagivateToMembers** method is called by the MembersCommand, two navigations are performed through regionManager and one notification to the **StatusUpdatedEvent** subscribers.
    
    * All the classes involved in navigation should implement **INavigationAware**.
    
    * We can determine if the result of the navigation will be a new instance of the requested view or just reuse one instance using the **IRegionMemberLifetime** interface.
    
    * The result of this commit is a Shell with three Regions in there with a functional “Request Info” button that navigate to next views and a “List of Members” button to go back.
    
    ![Create Module](/docs/images/NavigationBack_1.png)
    
    ![Create Module](/docs/images/NavigationBack_2.png)

##Communicate two Views in the same Region using Region Context of Prism##
Prism provides another way to communicate two Views that shares the same Region. This example shows a way to make the master/detail functionality using [RegionContext](https://github.com/PrismLibrary/Prism/blob/master/Documentation/WPF/50-ComposingtheUserInterface.md#sharing-data-between-multiple-regions).

Commit [Define region inside a UserControl and inject a Detailed Information module in the new region](https://acrowire.visualstudio.com/_git/PRISM%20Architecture%20Sample/commit/ed5db7e7ddc91c923947a503fc0681da7a1f9d44).

1. At this point, we added a new module to the application following the same steps done before.

2. The DetailedUserData Module defines just a View and his ViewModel by convention.

3. This new module is registered in the **DetailsRegion**, which is defined in the **Members** View of the MainContent Module.

4. This definition of **DetailsRegion** is slightly different from others done so far. This has the **RegionContext** bound to the **SelectedItem** property of the **_listOfMembers** listbox. So every time the SelectedItem changes, the RegionContext will change also.

5. In the code behind of **Details** view that is in **DetailedUserData** Module, we use the RegionContext static class to bound the View to the RegionContext. So every time the RegionContext changes, we will update the Member property of the ViewModel bound to the Details View, which in this case is **DetailsViewModel**.

6. The result of this commit is a Shell with three Regions in there with an Initial View that shows the List of Members and his detailed data for each.
 
 (Prism recommends to not use the DataContext to communicate parents and children. Instead of pass all the DataContext is recommended by Prism to use this mechanism.)
 
 ![Create Module](/docs/images/RegionContext.png)

##Use IsNavigationTarget method to reuse existing views.##
Did you noticed so far that If I set a date for a Member, go back to the List of Members, the same date is shown? IsNavigationTarget allows to put some logic to reuse the view when the Member match. Documentation for this scenario [here](https://github.com/PrismLibrary/Prism/blob/master/Documentation/WPF/60-Navigation.md#navigating-to-existing-views).

Commit [Use IsNavigationTarget method to re use existing views](https://acrowire.visualstudio.com/_git/PRISM%20Architecture%20Sample/commit/d14ad213d02a21276a37450db8b2b7565dab0cb2).

1. To determine if the ViewModel (and his View) is matching to the requested Member, we use the **IsNavigationTarget** method. So far we just returned the true value for practices purposes.
 
2. Remember that the order of execution for a navigation is this:
    * **OnNavigatedFrom** from the source.
    * **IsNavigationTarget** in the target.
    * If **IsNavigationTarget**  passes then OnNavigatedTo in the target.
        * There is another step to cancel the navigation, but this interface is not the example here.
        
3. So, because we are sending the Member information in the NavigationContext parameter for each of the stages detailed before, we can determine if the ViewModel match the Email with the passed parameter, like this:  
    `if ((navigationContext.Parameters["TeamMember"] as TeamMember).Email.Equals(EmailAddress))   
      return true;  
     return false;`
    
4. In this way if the ViewModel does not match, the Navigation process will create a new instance for the requested Member, If match, it will reuse the existing one.

5. The result of this commit is a Shell with three Regions in there with a navigation functionality that will create a new instance of the Request Info View if it’s needed.
    * At this point, if you run in some exception, please uncomment the line inside the OnNavigatedTo method of the **GridContentViewModel** since it is an ongoing experiment and learning of the Navigation Journal.

