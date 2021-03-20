# Dotnet.Sample.Api   ![GitHub release](https://img.shields.io/github/release/ajeetx/Dotnet.Sample.Api.svg?style=for-the-badge)  ![GitHub Release Date](https://img.shields.io/github/release-date/ajeetx/dotnet.sample.api?style=for-the-badge)  ![Maintenance](https://img.shields.io/maintenance/yes/2021.svg?style=for-the-badge)                                                                                                                          

![Centralized Logging](https://img.shields.io/badge/Centralized-LOGGING-ff69b4.svg?style=for-the-badge)  |![GitHub repo size in bytes](https://img.shields.io/github/repo-size/ajeetx/Dotnet.Sample.Api.svg?style=for-the-badge) | ![GitHub top language](https://img.shields.io/github/languages/top/ajeetx/Dotnet.Sample.Api.svg?style=for-the-badge)   | ![Lines of code](https://img.shields.io/tokei/lines/github/ajeetx/dotnet.sample.api?style=for-the-badge)| [![Codacy Badge](https://app.codacy.com/project/badge/Grade/1502c313d0a14671bcefac0d221a005c)](https://www.codacy.com/gh/AJEETX/Dotnet.Sample.Api/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=AJEETX/Dotnet.Sample.Api&amp;utm_campaign=Badge_Grade) 
|  ---          | ---        |  ---        | --- |  --- |
 ![Localization](https://img.shields.io/badge/LOCALIZATION-CULTURE-yellow.svg?style=for-the-badge)   | ![Api Versioning](https://img.shields.io/badge/WEB_API-VERSIONING-orange.svg?style=for-the-badge)  | ![Claim-based-token](https://img.shields.io/badge/JWT-TOKEN-red.svg?style=for-the-badge) | ![Feature Toggling](https://img.shields.io/badge/FEATURE-TOGGLE-blue.svg?style=for-the-badge) | [![Maintainability](https://api.codeclimate.com/v1/badges/345eb92c5ece914b7c0d/maintainability)](https://codeclimate.com/github/AJEETX/Dotnet.Sample.Api/maintainability) 
![Central Logging](https://img.shields.io/badge/EXCEPTION-HANDLING-critical.svg?style=for-the-badge) |  ![Swagger Support](https://img.shields.io/badge/SWAGGER-SUPPORT-green.svg?style=for-the-badge)  | [![Visual Studio 2019](https://img.shields.io/badge/V_S-2019-blue.svg?style=for-the-badge)](https://visualstudio.microsoft.com/downloads/) |  [![.Net Framework](https://img.shields.io/badge/DotNet-5.0-blue.svg?style=for-the-badge)](https://dotnet.microsoft.com/download/dotnet/5.0)   |     ![GitHub language count](https://img.shields.io/github/languages/count/ajeetx/Dotnet.Sample.Api.svg?style=flat)


---------------------------------------


<img src='swagger.png' alt='swagger documentation' />

## Repository codebase
 
The repository consists of projects as below:


| # |Project Name | Project detail | location| Environment |
| ---| ---  | ---           | ---          | --- |
| 1 | Dotnet.Sample.Api | Dotnet5 WebApi as backend  |  **src\Api** folder | [![.Net Framework](https://img.shields.io/badge/DotNet-5.0-blue.svg?style=plastic)](https://dotnet.microsoft.com/download/dotnet/5.0)|
| 2 | Dotnet.Sample.Api.Tests | Tests for webapi |  **src\Api.Test** folder | [![.Net Framework](https://img.shields.io/badge/DotNet-5.0-blue.svg?style=plastic)](https://dotnet.microsoft.com/download/dotnet/5.0)| 


## Application Folder structure

---------------------------------------


<img src='folders.png' alt='application folder structure' />

---------------------------------------


### Summary

The overall objective of the applications :


Through the ![login](https://img.shields.io/badge/login-green.svg?style=for-the-badge) endpoint A user can Login and jwt authentication is used. 

Once authorized, user can do "CRUD" operation on the ![products](https://img.shields.io/badge/products-blue.svg?style=for-the-badge) endpoint


### Setup detail

##### Environment Setup detail

> Download/install   	
>	1.	[![.Net Framework](https://img.shields.io/badge/DotNet-5.0-blue.svg?style=plastic)](https://dotnet.microsoft.com/download/dotnet/5.0) to run webapi project
>   
>   2. [![VS2019](https://img.shields.io/badge/VS-2019-blue.svg?style=plastic)](https://visualstudio.microsoft.com/downloads//) to run/debug the applications
>   Or [![VSCode](https://img.shields.io/badge/VS-Code-blue.svg?style=plastic)](https://code.visualstudio.com/) to run/debug the applications
>	
>   

##### Project Setup detail

>   1. Please clone or download the repository from [![github](https://img.shields.io/badge/git-hub-blue.svg?style=plastic)](https://github.com/AJEETX/Dotnet.Sample.Api) 
>   
>   2. Create a folder and place the downloaded repository and unzip if downloaded.
>   
>   3. Open the solution file through **Visual Studio 2019** or through **Visual Studio Code** open the newly created folder where the repository is downloaded
>   
##### (a) To start the webapi
   
>   1. Through **Visual Studio 2019**, click **F5** button to run the webapi, Please make sure the webapi project is selected as startup project.
>    
>   2. Or through **Visual Studio Code**, open a command terminal by pressing the computer keyboard buttons `Control` and `~`
>
>       (a) Within the terminal, browse to folder location named as **"src\Api"** 
>  
>       (b) Restore the dependencies, type `dotnet restore` on the terminal
>
>       (c) Run the webapi project, type `dotnet run` on the terminal
>   
>   3. **WebApi** shall start running on port **5000**

```
For better experience please use chrome browser
```

##### (b) To run the unit test project
>   
>   1. Through **Visual Studio 2019**, open the **Test Explorer** and run the tests.
>   
>   2. Or through **Visual Studio Code** Open a new command terminal
>   
>       (a) Within the new terminal, browse to the folder named as **"src\Api.Test"**
>   
>       (b) To run the tests, type `dotnet test` on the terminal


-----------------------------------------------------------------------
#### To-do:
> caching
> 
> endpoint throttling/rate limit

-----------------------------------------------------------------------
![Visitor Badge](https://visitor-badge.laobi.icu/badge?page_id=ajeetx/dotnet.sample.api)  | ![GitHub contributors](https://img.shields.io/github/contributors/ajeetx/Dotnet.Sample.Api.svg?style=plastic)|![license](https://img.shields.io/github/license/ajeetx/Dotnet.Sample.Api.svg?style=plastic)|
 | --- | --- | ---|



<a href="https://info.flagcounter.com/JDVj"><img src="https://s01.flagcounter.com/countxl/JDVj/bg_FFFFFF/txt_000000/border_CCCCCC/columns_8/maxflags_12/viewers_Visits/labels_0/pageviews_0/flags_0/percent_0/" alt="Flag Counter" border="0"></a>
