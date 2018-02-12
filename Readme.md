# BlipAjax

**_BlipAjax_** is an ASP.NET MVC case study solution to accompany two guides in the **PluralSight** [**HackGuides()**](https://www.pluralsight.com/guides/microsoft-net) collection for Microsoft .NET technologies.

## PluralSight Hack.Guides()

[ASP.NET MVC - Using Ajax helpers with Razor partial views](https://www.pluralsight.com/guides/microsoft-net/asp-net-mvc-using-ajax-helpers-with-razor-partial-views) - The case study presented in this guide uses one AjaxHelper class method, `BeginForm` to provide the asynchronous functionality needed to update a section of a web page without refreshing the entire page.

[ASP.NET MVC - Using JavaScript with Ajax and Razor partial views](https://www.pluralsight.com/guides/microsoft-net/asp-net-mvc-using-javascript-with-ajax-and-razor-partial-views) - Ajax helper methods and extensions in the **System.Web.Mvc** and **System.Web.Mvc.Ajax** namespaces can be combined with JavaScript and MVC partial views to create flexible interactive web pages with minimal code.

*Notice: PluralSight and the author disclaim any liability for errors or omissions in this code.*

## Solution Projects

| Project | Application Layer |
| :--- | :--- |
| Blip.Data | Data Context and Repositories |
| Blip.Entities | Data Entities |
| Blip.Web | User Interface (views) and Business Logic (controllers) |

## Technologies

| Dependency | Version*
| :--- | ---:
| .NET Framework | 4.6.2
| ASP.NET MVC | 5.2.3
| Bootstrap | 3.7
| Entity Framework | 6.1.3
| jQuery | 3.2.1
| jQuery Validation | 1.16.0
| Microsoft jQuery Unobtrusive Ajax | 3.2.3
| Microsoft jQuery Unobtrusive Validation | 3.2.3

&ast; As of the latest commit.

## Getting Started

1. Download or clone this repository.
1. Open the solution in Visual Studio 2017 or higher.
1. Select the **Blip.Data** project.
1. Open a Package Manager Console window.
1. Select "Blip.Data" for **Default Project**.
1. Run: `update-database`.

This will create the database, apply Entity Framework migrations, and run the `Seed` method to populate the database with values for the lookup tables.

## Configuration

* Two projects contain configuration strings which may require modification for the developer's specific environment:

    | Project | File
    | :--- | :---
    | Blip.Data | App.config
    | Blip.Web | Web.config

* The configuration strings specify the instance of SQL Server Express installed with Visual Studio 2017 as the target database server: `Data Source=(localdb)\ProjectsV13`. Developers using a different target database will have to change the connection strings in both projects.

## License

This project is licensed under the terms of the MIT license.