# Upload File Microservice
### Overview
**The Upload File project is a microservice application developed using C#.NET 8 and React.JS. It offers various features aimed at enhancing security, scalability, and testability.**

## Key Features
* **Security:** Implements JWT Token for enhanced security and API key for communication between the file-upload-server and notification-server.
* **Scalability:** Supports multi-file uploads and can send multiple emails for improved scalability.
* **Testability:** Includes unit tests in UploadFileServer for robust testability.
* **API Standard (RESTful):** Comprises three distinct parts - Upload Client, Upload Server, and Notification Server, adhering to API standards.

### Setup Instructions
1. **Download Visual Studio Community 2022:**
   - Visit Visual Studio Community 2022 and download the installer(https://visualstudio.microsoft.com/vs/community/).
   - Install Visual Studio Community 2022.
   - In the Workloads section, ensure that .NET Desktop Development is selected.
   - In the Individual Components section, ensure that .NET 8.0 Runtime (Long Term Support) is selected.
   - Proceed with the installation.
2. **Download Visual Studio Code (https://code.visualstudio.com/)**
3. **Clone the Following 3 Repositories:**
   * upload-file-client **(Can be add email more than 1 email. by using comma "," for example : example_1_@gmail.com , example_2_@gmail.com)**
     * Open CMD and navigate to the FileUploadClient directory.
     * Execute `code .` to open Visual Studio Code.
     * Run `npm run dev` to start the development server.
   * upload-file-server **(https://github.com/SurachaThongmanee/file-upload-server)**
     * open the solution. Ensure the following packages are installed:
       * IdentityModel.AspNetCore.OAuth2Introspection
       * Microsoft.AspNetCore.Authentication.JwtBearer
       * Microsoft.AspNetCore.Cors
       * Microsoft.AspNetCore.Mvc.NewtonsoftJson
       * Newtonsoft.Json
       * Run the project.
     * To run tests in project UploadFileServer.Test:
       * Right-click on => `UploadServiceTests.cs` in **FileUploadService.Tests**.
       * Select => **Run Tests** to execute the unit tests.
   * notification-server **(https://github.com/SurachaThongmanee/notification-server)**
     * Open the solution.
     * Run the project.
