# Cisco Jabber Problem Reporting Server
## TL;DR
Provides a simple mechanism to allow jabber problem reports (PRT) to be uploaded; in order to accomplish this the web server listens for incoming POST requests that have a `multipart/form-data` content-type. Any received file is read by the server and stored in a destination as specified in the configuration

## Installation Instructions
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