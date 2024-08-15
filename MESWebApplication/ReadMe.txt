NECESSARY FOR START:
	Packages: 
	- Microsoft.AspNetCore.Negotiate
	- Microsoft.AspNetCore.Components
	- Microsoft.EntityFrameworksCore
	- Microsoft.EntityFrameworksCore.Design
	- Microsoft.EntityFrameworksCore.SqlServer
	- Microsoft.EntityFrameworksCore.Tools
	- Microsoft.Extensions.Logging.Debug
	- Microsoft.Extensions.Logging.Log4Net.AspNetCore
	- Microsoft.Windows.Compatability
	- Telerik.UI.for.Blazor
	dotnet Version: 7.0.0

UTILIZING:
	1. Access the program off of the server or the run button after selecting MESWebApplication
	2. Select the application which you want to view off the navigation bar on the left
	3. After application page starts, select filter conditions
	4. Press reload to run search with given conditions

DEPENDENCIES:
	DatabaseSettings.razor (displays server info) is dependent on DatabaseSettingsService.cs (storing state) and NavMenu.razor (providing state)
	Each system has dependices between their MessageHubService, InterfaceDefinition, MessageList, and XMlPopup files respectively.
	Each MessageHubService creates a MasterDataModel on injection. Each service calls getters for Interface and MessageState attributes of MasterDataModel. Interface is dependent on that specific model's ToBiz and FromBiz tables, and MessageState is dependent on its MessageState table. 

SECURITY:
	Accessing the site needs "G_MES_MsgHub_Users" grouping on user account
	In NavMenu.razor access groups are checked on start using a call from Security in the Authoization folder. 
	If a user is in the appropriate access group for a system the dropdown element for the  app is shown, if not it does not.

ADDING NEW SYSTEMS:
	1. Make a models folder inside MessageHub for your system using EF Powertools, importing MessageState, ToBiz, and FromBiz tables. Add additional context statements if necessary.
	2. Add connection string to appsettings.json
	3. Go into ExtensionMethods within MessageHub and add methods that will massage FromBiz and ToBiz objects into InterfaceModel objects.
	4. Make copies of any MessageHubService, InterfaceDefinition, MessageList, and XMLPopup files. Change context references to fit your application
	and references to MessageHubService inside razor files accordingly. Add new MessageHubService to Program file.
	5. Add relevant functions in Helper.cs for system in MESWebApplication
	6. Add additional dropdown option and security protocols for new system in NavMenu as needed.
	7. Add reference of system to About page

Possibly necessary context statements:
trigger statement:
            entity.ToTable("FromBiz", tb => tb.HasTrigger("trg_dboFromBiz_LastUpdate"));

timeout extension statement:
            this.Database.SetCommandTimeout(TimeSpan.FromSeconds(60 * 6));