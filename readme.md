# Cisco Jabber Problem Reporting Server
## TL;DR
Provides a simple mechanism to allow jabber problem reports (PRT) to be uploaded; in order to accomplish this the web server listens for incoming POST requests that have a `multipart/form-data` content-type. Any received file is read by the server and stored in a destination as specified in the configuration

## Installation Instructions
* .NET Framework 4.6.1
* Installed IIS web server
* Create a new AppPool

## Configuration Instructions

## Testing
You can test the script by adapting the following request to your liking:
```
curl -X POST \
  http://localhost:1794/ExtensionMobility/UploadCiscoProblemRPT \
  -H 'Cache-Control: no-cache' \
  -H 'Content-Type: application/x-www-form-urlencoded' \
  -H 'content-type: multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW' \
  -F 'zipFileName=@C:\temp\test.txt'
```

## Further references
* http://blog.warcop.com/2014/12/10/configuring-cisco-jabber-problem-reporting/
* https://github.com/benpolzin/gobahnhof
* https://www.cisco.com/c/en/us/td/docs/voice_ip_comm/jabber/11_9/cjab_b_feature-configuration-for-cisco-jabber/cjab_b_feature-configuration-for-cisco-jabber_chapter_0100.html?bookSearch=true#CJAB_TK_C1B3C9BA_00