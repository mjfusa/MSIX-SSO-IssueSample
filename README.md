# MSIX-SSO-IssueSample
Sample demoing difference in behavior in running a desktop Win32 app using MSAL in an MSIX context (Windows Application Packaging Project.)

You'll need to clone this repo https://github.com/AzureAD/microsoft-authentication-library-for-dotnet to use with the sample.

To Repro:
1. Clear the Internet Explorer cache

1. Run the app as Win32 (MSIX-SSO-IssueSample as active project)

1. Note login prompt.

1. Close app from the debugger.

1. Clear the Internet Explorer cacher

1. Run the app as Win32/MSIX (MSIX-SSO-IssueSample-Installer as active project)

1. Note login prompt.

1. Close app from the debugger.

Expected results:
Login forms are the same.
Actual results:
Login forms are the different as below:





