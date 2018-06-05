# Cisco Jabber Problem Reporting Server
## TL;DR
Provides a simple mechanism to allow jabber problem reports (PRT) to be uploaded; in order to accomplish this the web server listens for incoming POST requests that have a `multipart/form-data` content-type. Any received file that is smaller than 3 MB (configurable) is read by the server and stored in a destination as specified in the configuration.
Note that already existing problem reports are overridden if a new file gets uploaded with the same name.

## Installation Instructions
* .NET Framework 4.6.1
* Installed IIS web server
* Create a new AppPool, name it e.g. `Cisco Problem RPT`
* Assign the desired user to the App Pool: `Advanced Settings...` > Identity > `...`; make sure that the account has write permissions on your desired target directory
* Create a new WebSite with your desired binding, e.g. `Cisco Problem RPT` on Port `80`; configure the physical path to point to your local web server directory, e.g. `d:\dev\Cisco Prolem RPT`
* Now that this one is done head over to the `Configuration Instructions`

## Configuration Instructions
* in your web directory, herein abbreviated to `~/` (in the above example this was `d:\dev\Cisco Problem RPT`) you find the following files of interest:
	1. `~/HostMap.config` - enter the current machine's host name and let it point to a configuration that resides in the `~/Config` folder, for example the following example maps the host name `wdat250135` to the `HWO.Operations.config` configuration files:
	```xml
    <add host="wdat250135" alias="HWO"/>
	```
	1. `~/Config/` in this directory you find the aforementioned configuration files; you can either use one of the already existing ones and configure these to your liking (don't forget the host mapping!) or create your new ones (by also adding it in the configuration of step 1.); when you open such a configuration file you find the `ProblemUploadDestinationDirectory` property that you can adjust to your desired target destination directory; in this example this points to `d:\temp\ProblemRPT`
	```xml
	<?xml version="1.0" encoding="utf-8"?>
	<configuration>
	  <appSettings>
	    <add key="ProblemUploadDestinationDirectory" value="d:\temp\ProblemRPT"/>
	  </appSettings>
	</configuration>
	```
	1. `~/web.config` - you actually only have to touch this file if you want to increase/restrict the maximum size of a problem report which can be uploaded. The default is configured to allow files of up to 3 MB. If you want to change these values change the following two lines:
	```xml
	<!-- max request length: https://stackoverflow.com/a/3853785 -->
	<system.web>
		<httpRuntime targetFramework="4.6.1" maxRequestLength="3072" /> <!-- 3 MB = 3072 kB; use e.g. http://whatsabyte.com/P1/byteconverter.htm for conversion; .NET default: 4MB -->
	</system.web>
	<system.webServer>
	  <security>
	    <requestFiltering>
	        <requestLimits maxAllowedContentLength="3145728" /> <!-- 3 MB = 3145728 B; use e.g. http://whatsabyte.com/P1/byteconverter.htm for conversion -->
	    </requestFiltering>
	  </security>
	</system.webServer>
	```

## Testing
You can test the script by (1) either using the built-in upload functionality by browsing to `http://localhost:26456/ProblemRPT` or (2) adapting the following request to your liking:
```
curl -X POST -F 'zipFileName=@c:\temp\Jabber-iOS-12.0.0.261399-20180522_224538.zip.esc' http://localhost:26456/ProblemRPT/UploadCiscoProblemRPT
```

Note that the file name needs to match the regex defined in `~/Config/Operations.config > UploadeeRegexExpr` (usually: `Jabber-.*.zip.e[ns][ck]`) as upload is prohibited otherwise.

## Troubleshooting
* Make sure that your App Pool user has permissions to create files in the configured target directory
* If you change the config you have to restart the app pool for the changes to take effect
* Make sure your uploaded file matches the regex that is used for validation which is defined in `~/Config/Operations.config > UploadeeRegexExpr` (usually: `Jabber-.*.zip.e[ns][ck]`)
* Check the `~/Logs` directory to see what's going wrong and why
* Install [Loginator](https://github.com/dabeku/Loginator) and re-do your failing requests, it should provide trace logs that should help you troubleshoot

## Further references
* http://blog.warcop.com/2014/12/10/configuring-cisco-jabber-problem-reporting/
* https://github.com/benpolzin/gobahnhof
* https://www.cisco.com/c/en/us/td/docs/voice_ip_comm/jabber/11_9/cjab_b_feature-configuration-for-cisco-jabber/cjab_b_feature-configuration-for-cisco-jabber_chapter_0100.html?bookSearch=true#CJAB_TK_C1B3C9BA_00